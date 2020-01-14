/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/7
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;

namespace WLib.Plugins.Interface
{
    /// <summary>
    /// 可加载插件的视图
    /// <para>实现该接口的类型一般是窗口或网页，可通过<see cref="PluginHelper"/>类进行插件管理</para>
    /// </summary>
    public interface IPluginView : IItemBase
    {
        /// <summary>
        /// 视图包含的插件容器
        /// <para>即窗口或页面包含的菜单栏、工具栏</para>
        /// </summary>
        IList<IPluginContainer> Containers { get; }
    }
}
