/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using WLib.Attributes.Description;

namespace WLib.Plugins.Enum
{
    /// <summary>
    /// 插件调用类型
    /// </summary>
    public enum EPluginInvokeType
    {
        /// <summary>
        /// 在单击时调用插件，默认
        /// </summary>
        [DescriptionEx("在单击时调用插件")]
        OnClick = 0,
        /// <summary>
        /// 在视图加载时调用插件
        /// </summary>
        [DescriptionEx("在视图加载时调用插件")]
        OnViewLoad = 1,
        /// <summary>
        /// 在视图关闭时调用插件
        /// </summary>
        [DescriptionEx("在视图关闭时调用插件")]
        OnViewClose = 2,
        /// <summary>
        /// 自定义插件调用时机
        /// </summary>
        [DescriptionEx("自定义插件调用时机")]
        Custom,
    }
}
