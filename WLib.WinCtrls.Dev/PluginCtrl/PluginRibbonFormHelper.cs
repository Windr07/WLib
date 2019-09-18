using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var containers = new List<IPluginContainer>();
            containers.Add(new PluginContainer()
            {
                Name = ribbonCtrl.DefaultPageCategory.Name,
                Text = ribbonCtrl.DefaultPageCategory.Text,
                Type = EPluginContainerType.RibbonMenu,
            });
            return containers;
        }
        /// <summary>
        /// 从插件方案中查找RibbonControl对应的插件，加载插件
        /// </summary>
        /// <param name="ribbonCtrl"></param>
        /// <param name="pluginPlan"></param>
        /// <param name="cmdData">插件命令的传入参数</param>
        public static void LoadPlugins(this RibbonControl ribbonCtrl, IPluginPlan pluginPlan, object cmdData)
        {
            var form = ribbonCtrl.FindForm();
            var view = pluginPlan.QueryViews(form);             //找到窗体对应的插件视图
            var container = QueryContainer(view, ribbonCtrl);   //找到控件对应的插件容器
            container.LoadPluginContainer(ribbonCtrl);          //向控件加载插件容器包含的插件
            container.LoadPluginCommands(cmdData);              //创建容器包含的插件对应的命令
        }
        /// <summary>
        /// 从插件方案中查找RibbonControl对应的插件，加载插件
        /// </summary>
        /// <param name="ribbonCtrl"></param>
        /// <param name="pluginPlan"></param>
        /// <param name="cmdData">插件命令的传入参数，泛型对象</param>
        public static void LoadPlugins<TCmdData>(this RibbonControl ribbonCtrl, IPluginPlan pluginPlan, TCmdData cmdData)
        {
            var form = ribbonCtrl.FindForm();
            var view = pluginPlan.QueryViews(form);             //找到窗体对应的插件视图
            var container = QueryContainer(view, ribbonCtrl);   //找到控件对应的插件容器
            container.LoadPluginContainer(ribbonCtrl);          //向控件加载插件容器包含的插件
            container.LoadPluginCommands(cmdData);              //创建容器包含的插件对应的命令
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
        /// 从插件方案中，查找与指定Ribbon控件关联的插件容器
        /// </summary>
        /// <param name="pluginView"></param>
        /// <param name="ribbonCtrl"></param>
        /// <returns></returns>
        private static IPluginContainer QueryContainer(this IPluginView pluginView, RibbonControl ribbonCtrl)
        {
            var name = ribbonCtrl.DefaultPageCategory.Name;
            var text = ribbonCtrl.DefaultPageCategory.Text;
            var container = pluginView.Containers.FirstOrDefault(v => v.Name == name && v.Text == text);
            if (container == null)
                throw new Exception($"在插件视图“{pluginView.Name}”中找不到容器控件“{text}”（{name}）的对应的插件配置！");
            return container;
        }
        /// <summary>
        /// 向插件窗口的RibbonControl控件加载插件
        /// </summary>
        /// <param name="container"></param>
        /// <param name="ribbonCtrl"></param>
        private static void LoadPluginContainer(this IPluginContainer container, RibbonControl ribbonCtrl)
        {
            var form = ribbonCtrl.FindForm();
            var imageCollection = ribbonCtrl.Images == null ? new ImageCollection() : (ImageCollection)ribbonCtrl.LargeImages;
            ribbonCtrl.LargeImages = imageCollection;
            foreach (var pageContainer in container.SubContainers)
            {
                var page = new RibbonPage { Text = pageContainer.Text, Name = pageContainer.Name };
                ribbonCtrl.DefaultPageCategory.Pages.Add(page);
                foreach (var groupContainer in pageContainer.SubContainers)
                {
                    var group = new RibbonPageGroup { Text = groupContainer.Text, Name = groupContainer.Name };
                    page.Groups.Add(group);
                    var plugins = groupContainer.Plugins.OrderBy(v => v.Index);
                    foreach (var plugin in plugins)
                    {
                        var image = plugin.GetImage();
                        int imageIndex = image == null ? -1 : imageCollection.Images.Add(image);
                        var item = new BarButtonItem
                        {
                            Caption = plugin.Text,
                            SuperTip = SuperTipsEx.CreateSuperTips(plugin.Tips, plugin.Text),
                            RibbonStyle = RibbonItemStyles.All,
                            LargeImageIndex = imageIndex,
                            Tag = plugin
                        };
                        if (!string.IsNullOrWhiteSpace(plugin.ShortcutKeys))
                            item.ItemShortcut = new BarShortcut((Keys)Enum.Parse(typeof(Keys), plugin.ShortcutKeys));

                        item.ItemClick += (sender, e) => plugin.Command.Invoke(form);
                        group.ItemLinks.Add(item);
                    }
                }
            }
        }
    }
}
