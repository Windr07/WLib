using System;
using System.Collections.Generic;

namespace WLib.ArcGis.DataCheck.Compare.Plan
{
    /// <summary>
    /// 两个表格或图层之间，通过字段进行匹配的对比的方案
    /// </summary>
    [Serializable]
    public class EachFieldComparePlan : EachComparePlan
    {
        /// <summary>
        /// 表格1与表格2进行记录匹配的字段
        /// </summary>
        public string MatchField1 { get; set; }
        /// <summary>
        /// 表格2与表格1进行记录匹配的字段
        /// </summary>
        public string MatchField2 { get; set; }


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
    }
}
