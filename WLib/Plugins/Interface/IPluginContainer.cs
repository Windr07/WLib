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
    /// 插件容器
    /// <para>通常对应一个菜单、工具条、Ribbon菜单、Ribbon菜单页、Ribbon菜单组、快捷菜单等</para>
    /// </summary>
    public interface IPluginContainer : IItemBase
    {
        /// <summary>
        /// 插件容器的快捷键
        /// </summary>
        string ShortcutKeys { get; set; }
        /// <summary>
        /// 插件容器类别
        /// <para>包括菜单、工具条、Ribbon菜单、Ribbon菜单页、Ribbon菜单组、快捷菜单等类别</para>
        /// </summary>
        EPluginContainerType Type { get; }
        /// <summary>
        /// 插件容器包含的子容器
        /// <para>例如菜单包含的子菜单</para>
        /// </summary>
        IList<IPluginContainer> SubContainers { get; set; }
        /// <summary>
        /// 插件容器包含的插件
        /// </summary>
        IList<IPluginItem> Plugins { get; }
    }
}
