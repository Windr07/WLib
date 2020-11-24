using WLib.Attributes.Description;

namespace WLib.ArcGis.Control.MapAssociation
{
    /// <summary>
    /// 地图文档操作类型
    /// </summary>
    public enum EMapDocOperate
    {
        /// <summary>
        /// 新建地图文档
        /// </summary>
        [DescriptionEx("新建地图文档")]
        NewEmptyDoc = 1,
        /// <summary>
        ///选择地图文档
        /// </summary>
        [DescriptionEx("选择地图文档")]
        OpenDocDialog = 2,
        /// <summary>
        /// 打开地图文档
        /// </summary>
        [DescriptionEx("打开地图文档")]
        OpenDoc = 3,
        /// <summary>
        /// 添加数据
        /// </summary>
        [DescriptionEx("添加数据")]
        AddData = 4,
        /// <summary>
        /// 保存地图文档
        /// </summary>
        [DescriptionEx("保存地图文档")]
        Save = 5,
        /// <summary>
        /// 另存地图文档
        /// </summary>
        [DescriptionEx("另存地图文档")]
        SaveAs = 6,
    }
}
