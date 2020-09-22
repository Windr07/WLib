/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.ArcGis.Analysis.GpEnum
{
    /// <summary>
    /// 要素转点工具中，指示输出点的位置
    /// </summary>
    public enum EFpPointLocation
    {
        /// <summary>
        /// 使用输入要素的代表中心作为输出点位置。这是默认设置。此位置并不总是包含在输入要素中
        /// </summary>
        CENTROID,
        /// <summary>
        /// 使用包含在输入要素中的位置作为输出点位置
        /// </summary>
        INSIDE,
    }
}
