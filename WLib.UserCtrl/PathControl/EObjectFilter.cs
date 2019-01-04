namespace WLib.UserCtrls.PathControl
{
    /// <summary>
    /// 表示从工作空间中筛选获得哪些类型的数据（表格、要素类等）
    /// </summary>
    public enum EObjectFilter
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,
        /// <summary>
        /// 表格
        /// </summary>
        Tables = 1,
        /// <summary>
        /// 图层（要素类）
        /// </summary>
        FeatureClasses = 2,
        ///// <summary>
        ///// 数据集
        ///// </summary>
        //Dataset = 3,
    }
}
