namespace WLib.ArcGis.DataCheck.Compare
{
    /// <summary>
    /// 表示一个对比项信息
    /// </summary>
    public abstract class CompareItem
    {
        /// <summary>
        /// 数据对比时，对数据进行匹配的方式
        /// </summary>
        public EMatchRuleType MatchType { get; }
        /// <summary>
        /// 数据筛选条件
        /// </summary>
        public string WhereClause { get; set; }
    }
}
