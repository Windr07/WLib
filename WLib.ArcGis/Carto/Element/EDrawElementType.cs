/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/3
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using WLib.Attributes.Description;

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
        [DescriptionEx("无")]
        None,
        /// <summary>
        /// 点
        /// </summary>
        [DescriptionEx("点")]
        Point,
        /// <summary>
        /// 线
        /// </summary>
        [DescriptionEx("线")]
        Polyline,
        /// <summary>
        /// 多边形
        /// </summary>
        [DescriptionEx("多边形")]
        Polygon,
        /// <summary>
        /// 圆
        /// </summary>
        [DescriptionEx("圆")]
        Circle,
        /// <summary>
        /// 矩形
        /// </summary>
        [DescriptionEx("矩形")]
        Rectangle,
        /// <summary>
        /// 文本
        /// </summary>
        [DescriptionEx("文本")]
        Text
    }
}
