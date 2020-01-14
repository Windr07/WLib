using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WLib.Plugins;
using WLib.Plugins.Interface;
using WLib.Plugins.Model;
using WLib.WinCtrls.Dev.Extension;

namespace WLib.WinCtrls.Dev.PluginCtrl
{
    /// <summary>
    /// 提供在Ribbon窗体加载使用插件的方法
    /// </summary>
    public static class PluginRibbonFormHelper
    {
        /// <summary>
        /// 将<see cref="RibbonControl"/>作为插件容器
        /// </summary>
        /// <param name="ribbonCtrl"></param>
        /// <returns></returns>
        public static List<IPluginContainer> ConvertToContainer(this RibbonControl ribbonCtrl)
        {
            return new List<IPluginContainer>
            {
                new PluginContainer()
                {
                    Name = ribbonCtrl.DefaultPageCategory.Name,
                    Text = ribbonCtrl.DefaultPageCategory.Text,
                    Type = EPluginContainerType.RibbonMenu,
                }
            };
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
                throw new Exception($"在插件视图“{pluginView.Name}”中找不到容器控件“{text}”（{name}）的对应的插件配置！");
            return container;
        }


        /// <summary>
        /// 向插件窗口的RibbonControl控件加载插件
        /// </summary>
        /// <param name="ribbonCtrl">加载插件的RibbonControl控件</param>
        /// <param name="caller">调用插件的控件，一般是窗体、菜单栏、RibbonControl、XtraTabControl、DcokControl、各类容器控件等</param>
        /// <param name="view">加载的插件视图</param>
        /// <param name="cmdData">命令输入参数</param>
        /// <param name="imageDir"></param>
        /// <param name="bindingEvent"></param>
        public static void LoadPlugins<TCmdData>(this RibbonControl ribbonCtrl, IPluginView view, object caller, TCmdData cmdData, string imageDir, Action<ICommand> bindingEvent)
        {
            var container = view.QueryContainer(ribbonCtrl.DefaultPageCategory.Name, ribbonCtrl.DefaultPageCategory.Text);

            var sbErrorCmds = new StringBuilder();
            var imageCollection = ribbonCtrl.LargeImages == null ? new ImageCollection() : (ImageCollection)ribbonCtrl.LargeImages;
            ribbonCtrl.LargeImages = imageCollection;
            foreach (var pageContainer in container.SubContainers)//菜单页
            {
                var page = new RibbonPage { Text = pageContainer.Text, Name = pageContainer.Name, Visible = pageContainer.Visible, Tag = pageContainer };
                ribbonCtrl.DefaultPageCategory.Pages.Add(page);
                foreach (var groupContainer in pageContainer.SubContainers)//菜单组
                {
                    var group = new RibbonPageGroup { Text = groupContainer.Text, Name = groupContainer.Name, Visible = groupContainer.Visible, Tag = groupContainer };
                    page.Groups.Add(group);
                    var plugins = groupContainer.Plugins.OrderBy(v => v.Index);
                    foreach (var plugin in plugins)//菜单项（插件项）
                    {
                        try
                        {
                            var item = PluginToButtonItem(plugin, imageCollection, caller, imageDir, cmdData);
                            bindingEvent(plugin.Command);
                            group.ItemLinks.Add(item);
                        }
                        catch(Exception ex) { sbErrorCmds.AppendLine($"程序集：{ plugin.AssemblyPath}\r\n命令：{plugin.TypeName}"); }
                        Application.DoEvents();
                    }
                }
            }
            if (sbErrorCmds.Length > 0)
            {
                sbErrorCmds.Insert(0, "以下插件命令创建失败（找不到程序集或命令，或加载过程出错，请注意程序集路径、名称、后缀(dll、exe)、类名是否正确）：");
                throw new Exception(sbErrorCmds.ToString());
            }
        }
        /// <summary>
        /// 根据插件创建调用插件的<see cref="BarButtonItem"/>
        /// </summary>
        /// <typeparam name="TCmdData"></typeparam>
        /// <param name="plugin"></param>
        /// <param name="imageCollection"></param>
        /// <param name="caller"></param>
        /// <param name="cmdData"></param>
        /// <returns></returns>
        private static BarButtonItem PluginToButtonItem<TCmdData>(IPluginItem plugin, ImageCollection imageCollection, object caller, string imageDir, TCmdData cmdData)
        {
            var bitmap = plugin.GetIcon(imageDir);
            int imageIndex = bitmap == null ? -1 : imageCollection.Images.Add(bitmap);
            var item = new BarButtonItem
            {
                Caption = plugin.Text,
                SuperTip = SuperTipsEx.CreateSuperTips(plugin.Tips, plugin.Text),
                RibbonStyle = RibbonItemStyles.All,
                LargeImageIndex = imageIndex,
                Tag = plugin,
                Visibility = plugin.Visible ? BarItemVisibility.Always : BarItemVisibility.Never,
            };
            if (!string.IsNullOrWhiteSpace(plugin.ShortcutKeys))
                item.ItemShortcut = new BarShortcut((Keys)Enum.Parse(typeof(Keys), plugin.ShortcutKeys));

            plugin.CreateCommand(cmdData);
            item.ItemClick += (sender, e) => plugin.Command?.Invoke(caller);
            return item;
        }


        /// <summary>
        /// 清空插件
        /// </summary>
        /// <param name="ribbonCtrl"></param>
        public static void ClearAllPlugins(this RibbonControl ribbonCtrl)
        {
            foreach (RibbonPage page in ribbonCtrl.DefaultPageCategory.Pages)
            {
                if (page.Tag is IPluginContainer)
                {
                    foreach (RibbonPageGroup group in page.Groups)
                    {
                        if (group.Tag is IPluginContainer)
                        {
                            var ownerItems = group.ItemLinks.OfType<BarButtonItemLink>().Select(v => v.Item);
                            var items = ownerItems.Where(v => v is BarButtonItem barButtonItem && barButtonItem.Tag is IPluginItem).ToArray();
                            foreach (var item in items)
                                group.ItemLinks.Remove(item);
                        }
                    }
                    var groups = page.Groups.Cast<RibbonPageGroup>().Where(v => v.ItemLinks.Count == 0).ToArray();
                    foreach (var group in groups)
                        page.Groups.Remove(group);
                }
            }
            var emptyPages = ribbonCtrl.DefaultPageCategory.Pages.Cast<RibbonPage>().Where(v => v.Groups.Count == 0).ToArray();
            foreach (var emptyPage in emptyPages)
                ribbonCtrl.DefaultPageCategory.Pages.Remove(emptyPage);
        }
    }
}