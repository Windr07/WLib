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
    /// 页面布局导航工具类别
    /// </summary>
    public enum EPageTools
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

        [DescriptionEx("上一视图")]
        PreView = 4,

        [DescriptionEx("下一视图")]
        NextView = 5,
    }
}
