/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WLib.ArcGis.DataCheck.Compare.Plan
{
    /// <summary>
    /// 两个表格或图层之间，通过字段进行匹配的对比的方案
    /// </summary>
    [Serializable]
    public class EachFieldComparePlan : EachComparePlan, IFieldComparePlan
    {
        /// <summary>
        /// 对比表格（右表）
        /// <para>当<see cref="EachComparePlan.TablePath2"/>的值为空时，使用该属性</para>
        /// </summary>
        public DataTable Table2 { get; set; }
        /// <summary>
        /// 表格1与表格2进行记录匹配的字段（或表达式）
        /// </summary>
        public string MatchField1 { get; set; }
        /// <summary>
        /// 表格2与表格1进行记录匹配的字段（或表达式）
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
        /// 两个表格或图层之间，通过字段进行匹配的对比的方案
        /// </summary>
        public EachFieldComparePlan() { }


        /// <summary>
        /// 获取对比方案涉及的字段
        /// </summary>
        /// <param name="leftFields">（表连接中的）左表的字段</param>
        /// <param name="rightFields">（表连接中的）右表的字段</param>
        public override void GetCompareFields(out List<string> leftFields, out List<string> rightFields)
        {
            if (string.IsNullOrWhiteSpace(IdField))
                throw new Exception($"ID字段（参数{nameof(IdField)}）不能为空！请对{nameof(IdField)}进行赋值！");
            if (string.IsNullOrWhiteSpace(IdField2))
                throw new Exception($"ID字段（参数{nameof(IdField2)}）不能为空！请对{nameof(IdField2)}进行赋值！");

            CompareItems.GetCompareFields(out leftFields, out rightFields);
            leftFields.Insert(0, IdField);
            rightFields.Insert(0, IdField2);

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
            sb.AppendLine("右表：" + TablePath2);
            sb.AppendLine("左表筛选：" + WhereClause);
            sb.AppendLine("右表筛选：" + WhereClause2);
            sb.AppendLine("左表ID：" + IdField);
            sb.AppendLine("右表ID：" + IdField2);
            sb.AppendLine("左表匹配项：" + MatchField1);
            sb.AppendLine("右表匹配项：" + MatchField2);
            foreach (var line in CompareItems.GetAllInfo())
                sb.AppendLine(line);

            return sb.ToString();
        }
    }
}
