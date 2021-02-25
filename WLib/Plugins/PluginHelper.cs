/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using WLib.Data;
using WLib.Drawing;
using WLib.Files;
using WLib.Plugins.Enum;
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
        public static void InvokePlugins(this IPluginView view, EPluginInvokeType invokeType, object caller = null)
        {
            var plugins = view.QueryPlugins(invokeType).ToList();
            plugins.ForEach(p => p.Command?.Invoke(caller));
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
        /// 获取插件视图中的全部插件
        /// </summary>
        /// <param name="view">插件视图</param>
        /// <param name="invokeType">插件调用类型</param>
        /// <returns></returns>
        public static IEnumerable<IPluginItem> QueryPlugins(this IPluginView view)
        {
            return view.Containers.SelectMany(container => QueryPlugins(container));
        }
        /// <summary>
        /// 从插件方案中，查找与指定Ribbon控件关联的插件容器
        /// </summary>
        /// <param name="pluginView"></param>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IPluginContainer QueryContainer(this IPluginView pluginView, string name, string text)
        {
            var container = pluginView.Containers.FirstOrDefault(v => v.Name == name && v.Text == text);
            if (container == null)
                throw new Exception($"插件视图【{pluginView.Name}】中找不到容器控件【{text}】（{name}）对应的插件配置，请检查配置一致性或控件语言版本一致性（汉化问题）");
            return container;
        }
        #endregion


        #region 插件容器扩展
        /// <summary>
        /// 获取插件容器（及其子容器）中的全部插件
        /// </summary>
        /// <param name="container">插件容器</param>
        /// <param name="filter">过滤条件委托</param>
        /// <returns></returns>
        public static IEnumerable<IPluginItem> QueryPlugins(this IPluginContainer container)
        {
            var plugins = container.Plugins.ToList();
            foreach (var sbuContainer in container.SubContainers)
                plugins.AddRange(QueryPlugins(sbuContainer));

            return plugins;
        }
        /// <summary>
        /// 从插件容器（及其子容器）中筛选指定的过滤条件的插件
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
        /// 从插件容器（及其子容器）中筛选指定的调用类型的插件
        /// </summary>
        /// <param name="container">插件容器</param>
        /// <param name="invokeType">插件调用类型</param>
        /// <returns></returns>
        public static IEnumerable<IPluginItem> QueryPlugins(this IPluginContainer container, EPluginInvokeType invokeType)
        {
            return QueryPlugins(container, plugin => plugin.InvokType == invokeType);
        }
        /// <summary>
        /// 通过反射创建插件容器中的插件的命令（ICommand），设置命令的传入参数
        /// </summary>
        /// <param name="container">插件容器</param>
        /// <param name="cmdData">插件命令的传入参数</param>
        public static void LoadPluginCommands<TCmdData>(this IPluginContainer container, TCmdData cmdData)
        {
            var sbErrorCmds = new StringBuilder();
            foreach (var plugin in container.QueryPlugins())
            {
                try
                {
                    plugin.CreateCommand<TCmdData>(cmdData);
                }
                catch { sbErrorCmds.AppendLine($"程序集：{ plugin.AssemblyPath}\r\n命令：{plugin.TypeName}"); }
            }
            if (sbErrorCmds.Length > 0)
            {
                sbErrorCmds.Insert(0, "以下插件命令创建失败（找不到程序集或命令，请注意程序集路径、名称、后缀(dll、exe)、类名是否正确）：");
                throw new Exception(sbErrorCmds.ToString());
            }
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


        #region 插件项扩展
        /// <summary>
        /// 通过反射创建插件中的命令（ICommand），设置命令的传入参数
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static ICommand CreateCommand(this IPluginItem plugin, object inputData)
        {
            var assemblyPath = PathEx.GetRootPath(plugin.AssemblyPath);
            var objCmd = Assembly.LoadFile(assemblyPath).CreateInstance(plugin.TypeName);
            var cmd = objCmd as ICommand;
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
            var assemblyPath = PathEx.GetRootPath(plugin.AssemblyPath);
            var objCmd = Assembly.LoadFile(assemblyPath).CreateInstance(plugin.TypeName);
            var cmd = objCmd as ICommand<TCmdData>;
            cmd.InputData = inputData;
            return plugin.Command = cmd;
        }
        /// <summary>
        /// 获取插件图标
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="imageFolderPath">默认插件图标目录，仅在<see cref="PluginItem.IconPath"/>为相对路径时使用该值</param>
        /// <returns></returns>
        public static Bitmap GetIcon(this IPluginItem plugin, string imageFolderPath = null)
        {
            var path = plugin.IconPath;
            if (!string.IsNullOrWhiteSpace(path) && !Path.IsPathRooted(path))
            {
                if (!string.IsNullOrWhiteSpace(imageFolderPath))
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imageFolderPath, path);
                else
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            }
            if (!File.Exists(path))
                return null;

            return GetBitmap.FromFile(path);
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

            var assembly = Assembly.LoadFile(appAssemblyPath);
            var pluginViews = assembly.GetInterfaceAchieveTypes<IPluginView>();
            if (pluginViews.Count() == 0)
                throw new Exception($"由于程序集（或软件）中没有实现插件视图“{nameof(IPluginView)}”接口的对象，该程序集无法加载插件。请先在程序集的至少一个窗体（或网页等）对象中实现“{nameof(IPluginView)}”接口");

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
            {
                var planSystem = new PluginPlanSystem();
                planSystem.SysInfo = CreateAssemblySystemInfo(assembly);
                planSystem.Plans.Add(CreatePluginPlan(assembly.Location));
                yield return planSystem;
            }
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
            return new SystemInfo(assName.Name, assName.FullName);
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


        /// <summary>
        /// 将插件方案的全部插件的图标，统一复制到指定图标目录中，存储图标的相对路径
        /// </summary>
        /// <param name="pluginPlans">插件方案集合</param>
        public static void SynPluginImages(this IEnumerable<IPluginPlan> pluginPlans, string appDir, string imageFolder)
        {
            var targetDir = Path.Combine(appDir, imageFolder);
            if (!Directory.Exists(targetDir))
                Directory.CreateDirectory(targetDir);

            targetDir = targetDir.Trim().ToLower();
            foreach (var plan in pluginPlans)
            {
                foreach (var view in plan.Views)
                {
                    var plugins = view.QueryPlugins();
                    foreach (var plugin in plugins)
                    {
                        var sourcePath = plugin.IconPath?.Trim();
                        if (!Path.IsPathRooted(sourcePath))//已经存储为相对路径的图标跳过
                            continue;

                        var sourceDir = Path.GetDirectoryName(sourcePath).ToLower();
                        var fileName = Path.GetFileName(sourcePath);
                        if (sourceDir != targetDir)//源目录与目标目录不同，则复制图标到目标目录
                        {
                            var targetPath = Path.Combine(targetDir, fileName);
                            if (File.Exists(targetPath))//源文件存在，则修改新图标文件的名称
                            {
                                fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(fileName);
                                targetPath = Path.Combine(targetDir, fileName);
                            }
                            File.Copy(plugin.IconPath, targetPath);
                        }
                        plugin.IconPath = fileName;//存储相对路径
                    }
                }
            }
        }

    }
}
