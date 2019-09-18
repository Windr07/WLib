using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using WLib.Plugins.Interface;
using WLib.Plugins.Model;
using WLib.Reflection;

namespace WLib.Plugins
{
    /// <summary>
    /// 提供插件功能的扩展方法
    /// </summary>
    public static class PluginHelper
    {
        #region 插件视图扩展
        /// <summary>
        /// 调用相应类型的插件
        /// </summary>
        /// <param name="view"></param>
        /// <param name="invokeType"></param>
        public static void InvokePlugins(this IPluginView view, IPluginPlan pluginPlan, EPluginInvokeType invokeType)
        {
            if (pluginPlan == null)
                return;
            var pluginView = pluginPlan.Views.First(v => v.Name == view.Name && v.Text == view.Text);
            var plugins = pluginView.QueryPlugins(invokeType).ToList();
            plugins.ForEach(p => p.Command.Invoke(view));
        }
        #endregion


        #region 插件项扩展
        /// <summary>
        /// 通过反射创建插件中的命令（ICommand），设置命令的传入参数
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static ICommand CreateCommand(this IPluginItem plugin, object inputData)
        {
            var cmd = Assembly.LoadFrom(plugin.AssemblyPath).CreateInstance(plugin.TypeName) as ICommand;
            cmd.InputData = inputData;
            return plugin.Command = cmd;
        }
        /// <summary>
        /// 通过反射创建插件中的命令（ICommand），设置命令的传入参数
        /// </summary>
        /// <typeparam name="TCmdData"></typeparam>
        /// <param name="plugin"></param>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static ICommand CreateCommand<TCmdData>(this IPluginItem plugin, TCmdData inputData)
        {
            var cmd = Assembly.LoadFrom(plugin.AssemblyPath).CreateInstance(plugin.TypeName) as ICommand<TCmdData>;
            cmd.InputData = inputData;
            return plugin.Command = cmd;
        }
        /// <summary>
        /// 获取插件图标
        /// </summary>
        /// <param name="plugin"></param>
        /// <returns></returns>
        public static Image GetImage(this IPluginItem plugin)
        {
            if (!string.IsNullOrWhiteSpace(plugin.IconPath) && !Path.IsPathRooted(plugin.IconPath))
                plugin.IconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, plugin.IconPath);
            var image = File.Exists(plugin.IconPath) ? Image.FromFile(plugin.IconPath) : null;
            return image;
        }
        #endregion


        #region 插件容器扩展
        /// <summary>
        /// 从插件容器中筛选指定的调用类型的插件
        /// </summary>
        /// <param name="container">插件容器</param>
        /// <param name="filter">过滤条件委托</param>
        /// <returns></returns>
        public static IEnumerable<IPluginItem> QueryPlugins(this IPluginContainer container, Func<IPluginItem, bool> filter)
        {
            var plugins = container.Plugins.Where(filter).ToList();
            foreach (var sbuContainer in container.SubContainers)
                plugins.AddRange(QueryPlugins(sbuContainer, filter));

            return plugins;
        }
        /// <summary>
        /// 从插件容器中筛选指定的调用类型的插件
        /// </summary>
        /// <param name="container">插件容器</param>
        /// <param name="invokeType">插件调用类型</param>
        /// <returns></returns>
        public static IEnumerable<IPluginItem> QueryPlugins(this IPluginContainer container, EPluginInvokeType invokeType)
        {
            return QueryPlugins(container, plugin => plugin.InvokType == invokeType);
        }
        /// <summary>
        /// 从插件视图中筛选指定的调用类型的插件
        /// </summary>
        /// <param name="view">插件视图</param>
        /// <param name="invokeType">插件调用类型</param>
        /// <returns></returns>
        public static IEnumerable<IPluginItem> QueryPlugins(this IPluginView view, EPluginInvokeType invokeType)
        {
            return view.Containers.SelectMany(container => QueryPlugins(container, invokeType));
        }

        /// <summary>
        /// 通过反射创建插件容器中的插件的命令（ICommand），设置命令的传入参数
        /// </summary>
        /// <param name="container"></param>
        /// <param name="cmdData"></param>
        public static void LoadPluginCommands(this IPluginContainer container, object cmdData)
        {
            foreach (var pageContainer in container.SubContainers)
                foreach (var groupContainer in pageContainer.SubContainers)
                    foreach (var plugin in groupContainer.Plugins)
                        plugin.CreateCommand(cmdData);
        }
        /// <summary>
        /// 通过反射创建插件容器中的插件的命令（ICommand），设置命令的传入参数
        /// </summary>
        /// <param name="container"></param>
        /// <param name="cmdData"></param>
        public static void LoadPluginCommands<TCmdData>(this IPluginContainer container, TCmdData cmdData)
        {
            foreach (var pageContainer in container.SubContainers)
                foreach (var groupContainer in pageContainer.SubContainers)
                    foreach (var plugin in groupContainer.Plugins)
                        plugin.CreateCommand<TCmdData>(cmdData);
        }

        /// <summary>
        /// 判断容器是否为指定类型之一
        /// </summary>
        /// <param name="eTypes"></param>
        /// <returns></returns>
        public static bool IsTypeOf(this IPluginContainer container, params EPluginContainerType[] eTypes)
        {
            return eTypes.Any(t => t == container.Type);
        }
        /// <summary>
        /// 是否为顶层插件容器
        /// <para>包括菜单栏、工具栏、快捷菜单、Ribbon风格的菜单栏</para>
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static bool IsTopContainer(this IPluginContainer container)
        {
            return IsTypeOf(container, EPluginContainerType.ContextMenu, EPluginContainerType.MenuStrip, EPluginContainerType.ToolBar, EPluginContainerType.RibbonMenu);
        }
        /// <summary>
        /// 是否为插件项的直接容器
        /// <para>包括工具栏、快捷菜单、子菜单、Ribbon风格的菜单组</para>
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static bool IsItemContainer(this IPluginContainer container)
        {
            return IsTypeOf(container, EPluginContainerType.ContextMenu, EPluginContainerType.ToolBar, EPluginContainerType.SubMenu, EPluginContainerType.RibbonGroup);
        }
        #endregion


        #region 插件方案获取
        /// <summary>
        /// 根据指定路径的程序集创建插件方案
        /// </summary>
        /// <param name="appAssemblyPath">程序集路径</param>
        /// <param name="planName">插件方案名称</param>
        /// <returns></returns>
        public static IPluginPlan CreatePluginPlan(string appAssemblyPath, string planName = "默认方案", bool selected = true)
        {
            if (Directory.Exists(appAssemblyPath))
                appAssemblyPath = Directory.GetFiles(appAssemblyPath, "*.exe").First();

            var assembly = Assembly.LoadFrom(appAssemblyPath);
            var pluginViews = assembly.GetInterfaceAchieveTypes<IPluginView>();
            pluginViews = pluginViews.Select(v => new PluginView { Name = v.Name, Text = v.Text, Containers = v.Containers });
            return new PluginPlan { Name = planName, Text = planName, Selected = selected, Views = pluginViews.ToList() };
        }
        /// <summary>
        /// 插件接口与默认实现类的键值对
        /// </summary>
        private static Dictionary<Type, Type> InterfaceToClassDict = new Dictionary<Type, Type>
        {
           { typeof(ICommand),          typeof(Command) } ,
           { typeof(ICommand<>),        typeof(Command<>) } ,
           { typeof(ICommand<,,>),      typeof(Command<,,>) } ,
           { typeof(IPluginItem),       typeof(PluginItem) } ,
           { typeof(IPluginContainer),  typeof(PluginContainer) },
           { typeof(IPluginView),       typeof(PluginView) } ,
           { typeof(IPluginPlan),       typeof(PluginPlan) } ,
           { typeof(IPluginPlanSystem), typeof(PluginPlanSystem) } ,
           { typeof(ISystemInfo),       typeof(SystemInfo) } ,
        };
        /// <summary>
        /// 将JSON序列化成插件方案对象
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static IPluginPlan DeserializeToPlan(string json)
        {
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new JsonConverterEx(InterfaceToClassDict));
            return serializer.Deserialize<PluginPlan>(new JsonTextReader(new StringReader(json)));
        }
        #endregion


        #region 程序集与插件信息
        /// <summary>
        /// 从程序集目录中，获取各个程序集的应用软件插件方案系统信息
        /// </summary>
        /// <param name="assemblyDiretory"></param>
        /// <returns></returns>
        public static IEnumerable<IPluginPlanSystem> CreatePluginPlanSystems(string assemblyDiretory)
        {
            var assemblies = Directory.GetFiles(assemblyDiretory, "*.exe").Select(path => Assembly.LoadFile(path));
            foreach (var assembly in assemblies)
                yield return CreatePluginPlanSystem(assembly);
        }
        /// <summary>
        /// 从程序集中获取应用软件插件方案系统信息
        /// </summary>
        /// <param name="assembly">程序集，若为null则获取当前程序集信息</param>
        /// <returns></returns>
        public static IPluginPlanSystem CreatePluginPlanSystem(Assembly assembly = null)
        {
            var pluginPlanSystem = new PluginPlanSystem();
            pluginPlanSystem.SysInfo = CreateAssemblySystemInfo(assembly);
            pluginPlanSystem.Plans.Add(CreatePluginPlan(pluginPlanSystem.SysInfo.AppPath));
            return pluginPlanSystem;
        }
        /// <summary>
        /// 从程序集中获取应用软件的信息
        /// </summary>
        /// <param name="assembly">程序集，若为null则获取当前程序集信息</param>
        /// <returns></returns> 
        public static ISystemInfo CreateAssemblySystemInfo(Assembly assembly = null)
        {
            if (assembly == null)
                assembly = Assembly.GetEntryAssembly();
            var assName = assembly.GetName();
            return new SystemInfo(assembly.Location, assName.Name, assName.FullName);
        }
        /// <summary>
        /// 获取指定目录及子目录下全部程序集文件的路径，默认筛选dll和exe文件
        /// </summary>
        /// <param name="assemblyDir">程序集文件所在目录</param>
        /// <param name="assemblyFileFilter">文件过滤条件</param>
        /// <param name="extensions">程序集文件的扩展名，默认筛选dll和exe文件</param>
        /// <returns></returns>
        public static IEnumerable<string> GetAssemblyFiles(string assemblyDir, string assemblyFileFilter, string extensions = ".dll|.exe")
        {
            if (!Directory.Exists(assemblyDir))
                throw new Exception($"指定的程序集目录{assemblyDir}不存在！");

            var exts = extensions.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            var filePaths = Directory.EnumerateFiles(assemblyDir, "*.*", SearchOption.AllDirectories).Where(v => exts.Contains(Path.GetExtension(v)));
            if (!string.IsNullOrWhiteSpace(assemblyFileFilter))
                filePaths = filePaths.Where(v => Path.GetFileName(v).Contains(assemblyFileFilter));
            return filePaths;
        }
        #endregion
    }
}
