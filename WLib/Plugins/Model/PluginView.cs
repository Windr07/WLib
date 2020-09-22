using System;
using System.Collections.Generic;
using WLib.Plugins.Interface;

namespace WLib.Plugins.Model
{
    /// <summary>
    /// 可加载插件的视图
    /// <para>该类型一般指代窗口或网页，可通过<see cref="PluginHelper"/>类进行插件管理</para>
    /// </summary>
    [Serializable]
    public class PluginView : IPluginView
    {
        /// <summary>
        /// 视图ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 视图名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 视图标题
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 视图包含的插件容器
        /// <para>即窗口或页面包含的菜单栏、工具栏</para>
        /// </summary>
        public IList<IPluginContainer> Containers { get; set; } = new List<IPluginContainer>();
        /// <summary>
        /// 加载插件
        /// </summary>
        public virtual void LoadPlugins() { }
        /// <summary>
        /// 输出<see cref="Text"/>的值
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Text;
    }
}
