using WLib.Attributes;

namespace WLib.ArcGis.Control.MapAssociation
{
    /// <summary>
    /// 地图导航工具类别
    /// </summary>
    public enum EMapTools
    {
        [Description("无")]
        None = -1,

        [Description("全图")]
        FullExtent = 0,

        [Description("放大")]
        ZoomIn = 1,

        [Description("缩小")]
        ZoomOut = 2,

        [Description("平移")]
        Pan = 3,

        [Description("上一视图")]
        PreView = 4,

        [Description("下一视图")]
        NextView = 5,

        [Description("测量距离")]
        MeasureDistance = 6,

        [Description("测量面积")]
        MeasureArea = 7,

        [Description("卷帘")]
        Swipe = 8,

        [Description("识别")]
        Identify = 9,

        [Description("选择")]
        Selection = 10,
    }
}
