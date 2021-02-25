/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Text;
using System.Windows.Forms;
using WLib.Plugins;
using WLib.Plugins.Interface;

namespace WLib.WinCtrls.PluginCtrl
{
    /// <summary>
    /// 提供在Ribbon窗体加载使用插件的方法
    /// </summary>
    public static class PluginFormHelper
    {
        /// <summary>
        /// 向插件窗口的MenuStrip控件加载插件
        /// </summary>
        /// <typeparam name="TCmdData"></typeparam>
        /// <param name="menuStrip">加载插件的MenuStrip控件</param>
        /// <param name="caller">调用插件的控件，一般是窗体、菜单栏、RibbonControl、XtraTabControl、DcokControl、各类容器控件等</param>
        /// <param name="view">加载的插件视图</param>
        /// <param name="cmdData">命令输入参数</param>
        /// <param name="imageDir"></param>
        /// <param name="bindingEvent"></param>
        public static void LoadPlugins<TCmdData>(this MenuStrip menuStrip, IPluginView view, object caller, TCmdData cmdData, string imageDir, Action<ICommand> bindingEvent)
        {
            var container = view.QueryContainer(menuStrip.Name, menuStrip.Text);

            var sbErrorCmds = new StringBuilder();
            var imageCollection = menuStrip.ImageList == null ? new ImageList() : menuStrip.ImageList;
            menuStrip.ImageList = imageCollection;
            foreach (var subContainer in container.SubContainers)//菜单页
            {
                var menuItem = CreateMenuItem(container);
                menuStrip.Items.Add(menuItem);

                foreach (var container1 in subContainer.SubContainers)
                    LoadPluginContainer(container1, menuItem, caller, sbErrorCmds);
            }
            if (sbErrorCmds.Length > 0)
            {
                sbErrorCmds.Insert(0, "以下插件命令创建失败（找不到程序集或命令，或加载过程出错，请注意程序集路径、名称、后缀(dll、exe)、类名是否正确）：");
                throw new Exception(sbErrorCmds.ToString());
            }
        }
        /// <summary>
        /// 向菜单项加载子菜单项（插件）
        /// </summary>
        /// <param name="container"></param>
        /// <param name="menuItem"></param>
        /// <param name="caller"></param>
        private static void LoadPluginContainer(IPluginContainer container, ToolStripMenuItem menuItem, object caller, StringBuilder sbErrorCmds)
        {
            foreach (var subContainer in container.SubContainers)
            {
                var subMenuItem = CreateMenuItem(container);
                menuItem.DropDownItems.Add(subMenuItem);

                foreach (var container1 in subContainer.SubContainers)
                    LoadPluginContainer(container1, subMenuItem, caller, sbErrorCmds);
            }

            foreach (var plugin in container.Plugins)
            {
                var subMenuItem = CreateMenuItem(plugin, caller, sbErrorCmds);
                if (subMenuItem != null)
                    menuItem.DropDownItems.Add(subMenuItem);
            }
        }
        /// <summary>
        /// 将插件作为菜单，创建菜单
        /// </summary>
        /// <param name="plugin">插件</param>
        /// <param name="caller">插件/菜单所在窗口</param>
        /// <returns></returns>
        private static ToolStripMenuItem CreateMenuItem(IPluginItem plugin, object caller, StringBuilder sbErrorCmds)
        {
            try
            {
                var menuItem = new ToolStripMenuItem
                {
                    Text = plugin.Text,
                    ToolTipText = plugin.Tips,
                    Tag = plugin,
                    Image = plugin.GetIcon(),
                    ShortcutKeys = string.IsNullOrWhiteSpace(plugin.ShortcutKeys) ? Keys.None : (Keys)Enum.Parse(typeof(Keys), plugin.ShortcutKeys)
                };
                menuItem.Click += (sender, e) => plugin.Command?.Invoke(caller);
                return menuItem;
            }
            catch (Exception ex) { sbErrorCmds.AppendLine($"程序集：{ plugin.AssemblyPath}\t命令：{plugin.TypeName}\t{ex.Message}"); return null; }
            finally { Application.DoEvents(); }
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
