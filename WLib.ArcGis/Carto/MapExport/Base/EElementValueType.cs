/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/8 10:55:43
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.ArcGis.Carto.MapExport.Base
{
    /// <summary>
    /// 表示获取或设置元素的哪一类属性
    /// </summary>
    public enum EElementValueType
    {
        /// <summary>
        /// 元素标题
        /// </summary>
        Name,
        /// <summary>
        /// 元素文本
        /// </summary>
        Text,
        /// <summary>
        /// 元素大小（宽度和高度）
        /// </summary>
        Size,
        /// <summary>
        /// 元素位置（X坐标和Y坐标）
        /// </summary>
        Location,
        /// <summary>
        /// 元素锚点
        /// </summary>
        Anchor
    }
}
