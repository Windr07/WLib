using System;

namespace WLib.ArcGis.DataCheck.Compare.Plan
{
    /// <summary>
    /// 两个表格或图层之间对比的方案
    /// </summary>
    [Serializable]
    public abstract class EachComparePlan : ComparePlan
    {
        /// <summary>
        /// 被对比表格（右表）的作为ID标识的列（例如FID,单元编号等）
        /// </summary>
        public string IdField2 { get; set; }
        /// <summary>
        /// 表格2路径
        /// </summary>
        public string TablePath2 { get; set; }
        /// <summary>
        /// 表格2筛选条件
        /// </summary>
        public string WhereClause2 { get; set; }

        /// <summary>
        /// 显示对比方案名称
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Name;
    }
}
