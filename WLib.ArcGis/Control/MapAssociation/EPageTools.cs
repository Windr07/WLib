using WLib.Attributes;

namespace WLib.ArcGis.Control.MapAssociation
{
    /// <summary>
    /// 页面布局导航工具类别
    /// </summary>
    public enum EPageTools
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
    }
}
