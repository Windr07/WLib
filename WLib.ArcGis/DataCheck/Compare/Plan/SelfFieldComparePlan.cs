/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WLib.ArcGis.DataCheck.Compare.Plan
{
    /// <summary>
    /// 单个表格或图层自身信息，通过字段进行匹配对比的方案
    /// </summary>
    [Serializable]
    public class SelfFieldComparePlan : SelfComparePlan, IFieldComparePlan
    {
        /// <summary>
        /// 用于匹配的数据范围（筛选条件）
        /// </summary>
        public string WhereClause2 { get; set; }
        /// <summary>
        /// 匹配的字段1
        /// </summary>
        public string MatchField1 { get; set; }
        /// <summary>
        /// 匹配的字段2
        /// </summary>
        public string MatchField2 { get; set; }
        /// <summary>
        /// 判断<see cref="MatchField1"/>是否为表达式（True）还是单纯的字段或空值（False）
        /// </summary>
        public bool MatchField1_IsExpression => !string.IsNullOrWhiteSpace(MatchField1) && FieldCompare.GetExpressionObjectNames(MatchField1).ToArray().Length > 1;
        /// <summary>
        /// 判断<see cref="MatchField2"/>是否为表达式（True）还是单纯的字段或空值（False）
        /// </summary>
        public bool MatchField2_IsExpression => !string.IsNullOrWhiteSpace(MatchField2) && FieldCompare.GetExpressionObjectNames(MatchField2).ToArray().Length > 1;

        /// <summary>
        /// 获取对比方案涉及的字段
        /// </summary>
        /// <param name="leftFields">（表连接中的）左表的字段</param>
        /// <param name="rightFields">（表连接中的）右表的字段</param>
        public override void GetCompareFields(out List<string> leftFields, out List<string> rightFields)
        {
            base.GetCompareFields(out leftFields, out rightFields);

            if (string.IsNullOrWhiteSpace(MatchField1)) throw new Exception($"未指定匹配字段{nameof(MatchField1)}");
            else leftFields.Add(MatchField1);

            if (string.IsNullOrWhiteSpace(MatchField2)) MatchField2 = MatchField1;
            rightFields.Add(MatchField2);
        }

        /// <summary>
        /// 获取对比方案中包含的全部信息
        /// </summary>
        /// <returns></returns>
        public override string GetAllInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"--------{Name}---------");
            sb.AppendLine("左表：" + TablePath);
            sb.AppendLine("右表：" + TablePath);
            sb.AppendLine("左表筛选：" + WhereClause);
            sb.AppendLine("右表筛选：" + WhereClause2);
            sb.AppendLine("左表ID：" + IdField);
            sb.AppendLine("右表ID：" + IdField);
            sb.AppendLine("左表匹配项：" + MatchField1);
            sb.AppendLine("右表匹配项：" + MatchField2);
            foreach (var line in CompareItems.GetAllInfo())
                sb.AppendLine(line);

            return sb.ToString();
        }
    }
}
