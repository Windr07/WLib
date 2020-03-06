namespace WLib.ArcGis.DataCheck.Compare
{
    /// <summary>
    /// 地理数据对比时，对数据进行匹配的方式
    /// </summary>
    public enum EMatchRuleType
    {
        /// <summary>
        /// 无匹配规则
        /// （例如对比图斑总数、判断单个图层记录重复时不需要匹配规则）
        /// </summary>
        None,
        /// <summary>
        /// 按照字段进行匹配
        /// </summary>
        Field,
        /// <summary>
        /// 按照图斑空间关系进行匹配
        /// </summary>
        SpatialRelation,
    }
}
