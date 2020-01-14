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
    /// 插件方案
    /// </summary>
    public interface IPluginPlan : IItemBase
    {
        /// <summary>
        /// 是否选用此方案
        /// </summary>
        bool Selected { get; set; }
        /// <summary>
        /// 可加载插件的视图
        /// </summary>
        IList<IPluginView> Views { get; set; }
        /// <summary>
        /// 插件方案序列化后的字符串
        /// </summary>
        string StrPluginPlan { get; set; }
    }
}
