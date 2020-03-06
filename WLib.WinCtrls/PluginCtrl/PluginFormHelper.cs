using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WLib.Plugins;
using WLib.Plugins.Interface;
using WLib.Plugins.Model;
using WLib.WinCtrls.MessageCtrl;

namespace WLib.WinCtrls.PluginCtrl
{
    /// <summary>
    /// 提供在Ribbon窗体加载使用插件的方法
    /// </summary>
    public static class PluginFormHelper
    {
        /// <summary>
        /// 以菜单形式加载插件
        /// <para>将<see cref="IPluginView"/>中的插件以<see cref="MenuStrip"/>的菜单形式来加载， 在点击菜单、窗口加载、窗口关闭时调用相应插件</para>
        /// </summary>
        /// <param name="menuStrip"></param>
        /// <param name="view"></param>
        /// <param name="form"></param>
        /// <param name="cmdData">插件命令的传入参数</param>
        /// <param name="bingdingPluginCommandEvents">插件命令事件处理方法</param>
        public static void LoadPluginView<TCmdData>(this MenuStrip menuStrip,
            Form form, IPluginView view, TCmdData cmdData, Action<IPluginView> bingdingPluginCommandEvents)
        {
            try
            {
                var container = view.QueryContainer(menuStrip);
                menuStrip.LoadPluginContainer(container);
                container.LoadPluginCommands(cmdData);
                bingdingPluginCommandEvents(view);//绑定插件命令事件处理

                //在窗口加载时、窗口关闭时触发相应插件
                view.InvokePlugins(EPluginInvokeType.OnViewLoad, form);
                form.FormClosing += (sender, e) => view.InvokePlugins(EPluginInvokeType.OnViewClose, form);
            }
            catch (Exception ex) { MessageBoxEx.ShowError(ex); }
        }


        /// <summary>
        /// 将<see cref="MenuStrip"/>作为插件容器
        /// </summary>
        /// <param name="menuStrip"></param>
        /// <returns></returns>
        public static List<IPluginContainer> ConvertToContainer(this MenuStrip menuStrip)
        {
            var containers = new List<IPluginContainer>();
            containers.Add(new PluginContainer()
            {
                Name = menuStrip.Name,
                Text = menuStrip.Text,
                Type = EPluginContainerType.RibbonMenu,
            });
            return containers;
        }
        /// <summary>
        /// 从插件方案中查找<see cref="MenuStrip"/>对应的插件，加载插件
        /// </summary>
        /// <param name="menuStrip"></param>
        /// <param name="pluginPlan"></param>
        /// <param name="cmdData">插件命令的传入参数，泛型对象</param>
        public static void LoadPlugins<TCmdData>(this MenuStrip menuStrip, IPluginPlan pluginPlan, TCmdData cmdData)
        {
            var form = menuStrip.FindForm();
            var view = pluginPlan.QueryViews(form);            //找到窗体对应的插件视图
            var container = QueryContainer(view, menuStrip);   //找到控件对应的插件容器
            menuStrip.LoadPluginContainer(container);          //向控件加载插件容器包含的插件
            container.LoadPluginCommands(cmdData);             //创建容器包含的插件对应的命令
        }


        /// <summary>
        /// 从插件方案中，查找与指定窗体关联的插件视图
        /// </summary>
        /// <param name="form"></param>
        /// <param name="pluginPlan"></param>
        /// <returns></returns>
        private static IPluginView QueryViews(this IPluginPlan pluginPlan, Form form)
        {
            var name = form.Name;
            var text = form.Text;
            var view = pluginPlan.Views.FirstOrDefault(v => v.Name == name && v.Text == text);
            if (view == null)
                throw new Exception($"在插件方案“{pluginPlan.Name}”中找不到窗体“{text}”（{name}）的对应的插件配置！");
            return view;
        }
        /// <summary>
        /// 从插件方案中，查找与指定<see cref="MenuStrip"/>关联的插件容器
        /// </summary>
        /// <param name="pluginView"></param>
        /// <param name="menuStrip"></param>
        /// <returns></returns>
        public static IPluginContainer QueryContainer(this IPluginView pluginView, MenuStrip menuStrip)
        {
            var name = menuStrip.Name;
            var text = menuStrip.Text;
            var container = pluginView.Containers.FirstOrDefault(v => v.Name == name && v.Text == text);
            if (container == null)
                throw new Exception($"在插件视图“{pluginView.Name}”中找不到容器控件“{text}”（{name}）的对应的插件配置！");
            return container;
        }
        /// <summary>
        /// 向插件窗口的<see cref="MenuStrip"/>加载插件
        /// </summary>
        /// <param name="container"></param>
        /// <param name="menuStrip"></param>
        public static void LoadPluginContainer(this MenuStrip menuStrip, IPluginContainer container)
        {
            var form = menuStrip.FindForm();
            foreach (var subContainer in container.SubContainers)
            {
                var menuItem = CreateMenuItem(container);
                menuStrip.Items.Add(menuItem);

                foreach (var container1 in subContainer.SubContainers)
                    LoadPluginContainer(subContainer, menuItem, form);
            }

            foreach (var plugin in container.Plugins)
                menuStrip.Items.Add(CreateMenuItem(plugin, form));
        }
        /// <summary>
        /// 向菜单项加载子菜单项（插件）
        /// </summary>
        /// <param name="container"></param>
        /// <param name="menuItem"></param>
        /// <param name="form"></param>
        private static void LoadPluginContainer(IPluginContainer container, ToolStripMenuItem menuItem, Form form)
        {
            foreach (var subContainer in container.SubContainers)
            {
                var subMenuItem = CreateMenuItem(container);
                menuItem.DropDownItems.Add(subMenuItem);

                foreach (var container1 in subContainer.SubContainers)
                    LoadPluginContainer(container1, subMenuItem, form);
            }

            foreach (var plugin in container.Plugins)
                menuItem.DropDownItems.Add(CreateMenuItem(plugin, form));
        }
        /// <summary>
        /// 将插件作为菜单，创建菜单
        /// </summary>
        /// <param name="plugin">插件</param>
        /// <param name="form">插件/菜单所在窗口</param>
        /// <returns></returns>
        private static ToolStripMenuItem CreateMenuItem(IPluginItem plugin, Form form)
        {
            var menuItem = new ToolStripMenuItem
            {
                Text = plugin.Text,
                ToolTipText = plugin.Tips,
                Tag = plugin,
                Image = plugin.GetIcon(),
                ShortcutKeys = string.IsNullOrWhiteSpace(plugin.ShortcutKeys) ? Keys.None : (Keys)Enum.Parse(typeof(Keys), plugin.ShortcutKeys)
            };
            menuItem.Click += (sender, e) => plugin.Command?.Invoke(form);
            return menuItem;
        }
        /// <summary>
        /// 将插件容器作为菜单，创建菜单
        /// </summary>
        /// <param name="container">插件容器</param>
        /// <returns></returns>
        private static ToolStripMenuItem CreateMenuItem(IPluginContainer container)
        {
            return new ToolStripMenuItem
            {
                Text = container.Text,
                Name = container.Name,
                ShortcutKeys = string.IsNullOrWhiteSpace(container.ShortcutKeys) ? Keys.None : (Keys)Enum.Parse(typeof(Keys), container.ShortcutKeys)
            };
        }
    }
}
