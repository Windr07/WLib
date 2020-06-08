/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/3
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/


using WLib.Attributes.Description;

namespace WLib.ArcGis.Control.MapAssociation
{
    /// <summary>
    /// 地图导航工具类别
    /// </summary>
    public enum EMapTools
    {
        [DescriptionEx("无")]
        None = -1,

        [DescriptionEx("全图")]
        FullExtent = 0,

        [DescriptionEx("放大")]
        ZoomIn = 1,

        [DescriptionEx("缩小")]
        ZoomOut = 2,

        [DescriptionEx("平移")]
        Pan = 3,

        [DescriptionEx("前一视图")]
        PreView = 4,

        [DescriptionEx("后一视图")]
        NextView = 5,

        [DescriptionEx("测量距离")]
        MeasureDistance = 6,

        [DescriptionEx("测量面积")]
        MeasureArea = 7,

        [DescriptionEx("卷帘")]
        Swipe = 8,

        [DescriptionEx("识别")]
        Identify = 9,

        [DescriptionEx("选择图斑")]
        Selection = 10,
    }
}
