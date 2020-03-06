using System;
using System.Collections.Generic;

namespace WLib.ArcGis.DataCheck.Compare.Plan
{
    /// <summary>
    /// 单个表格或图层自身信息，通过字段进行匹配对比的方案
    /// </summary>
    [Serializable]
    public class SelfFieldComparePlan : SelfComparePlan
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
    }
}
