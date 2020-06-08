/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/7
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using WLib.Attributes.Description;

namespace WLib.Plugins
{
    /// <summary>
    /// 插件容器类别
    /// <para>菜单、工具条、Ribbon菜单、Ribbon菜单页、Ribbon菜单组、快捷菜单</para>
    /// </summary>
    public enum EPluginContainerType
    {
        /// <summary>
        /// 菜单栏
        /// </summary>
        [DescriptionEx("菜单")]
        MenuStrip = 0,

        /// <summary>
        /// 子菜单，可以包含无限层级的子菜单
        /// </summary>
        [DescriptionEx("子菜单")]
        SubMenu = 1,

        /// <summary>
        /// 工具条
        /// </summary>
        [DescriptionEx("工具条")]
        ToolBar = 2,

        /// <summary>
        /// 快捷菜单（下拉菜单，或鼠标右键菜单）
        /// </summary>
        [DescriptionEx("快捷菜单")]
        ContextMenu = 3,

        /// <summary>
        /// Ribbon风格的菜单
        /// </summary>
        [DescriptionEx("Ribbon菜单栏")]
        RibbonMenu = 4,

        /// <summary>
        /// Ribbon风格的菜单页
        /// </summary>
        [DescriptionEx("Ribbon菜单页")]
        RibbonPage = 5,

        /// <summary>
        /// Ribbon风格的菜单组
        /// </summary>
        [DescriptionEx("Ribbon菜单组")]
        RibbonGroup = 6,
    }
}
