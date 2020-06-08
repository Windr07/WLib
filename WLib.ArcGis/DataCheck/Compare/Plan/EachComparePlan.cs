using System;
using System.Text;

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
        /// 对比表格（右表）路径
        /// </summary>
        public string TablePath2 { get; set; }
        /// <summary>
        /// 对比表格（右表）筛选条件
        /// </summary>
        public string WhereClause2 { get; set; }

        /// <summary>
        /// 显示对比方案名称
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Name;
        /// <summary>
        /// 获取对比方案中包含的全部信息
        /// </summary>
        /// <returns></returns>
        public override string GetAllInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"--------{Name}---------");
            sb.AppendLine("左表：" + TablePath);
            sb.AppendLine("右表：" + TablePath2);
            sb.AppendLine("左表筛选：" + WhereClause);
            sb.AppendLine("右表筛选：" + WhereClause2);
            sb.AppendLine("左表ID：" + IdField);
            sb.AppendLine("右表ID：" + IdField2);
            foreach (var line in CompareItems.GetAllInfo())
                sb.AppendLine(line);

            return sb.ToString();
        }
    }
}
