/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/3
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using WLib.Attributes;

namespace WLib.ArcGis.Carto.Element
{
    /** Element元素分类，请参考：
     *  (1)<see cref="ElementQuery"/>.cs的注释部分
     *  (2)官方文档：https://desktop.arcgis.com/en/arcobjects/10.4/net/webframe.htm#IElement.htm
     */

    /// <summary>
    /// 在地图中绘制的元素类别
    /// </summary>
    public enum EDrawElementType
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None,
        /// <summary>
        /// 点
        /// </summary>
        [Description("点")]
        Point,
        /// <summary>
        /// 线
        /// </summary>
        [Description("线")]
        Polyline,
        /// <summary>
        /// 面
        /// </summary>
        [Description("面")]
        Polygon,
        /// <summary>
        /// 圆
        /// </summary>
        [Description("圆")]
        Circle,
        /// <summary>
        /// 矩形
        /// </summary>
        [Description("矩形")]
        Rectangle,
        /// <summary>
        /// 文本
        /// </summary>
        [Description("文本")]
        Text
    }
}
