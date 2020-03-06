using System;
using System.Collections.Generic;
using WLib.ArcGis.DataCheck.Compare.Enum;

namespace WLib.ArcGis.DataCheck.Compare.Plan
{
    /// <summary>
    /// 两个图层之间，通过空间关系进行匹配的对比的方案
    /// </summary>
    [Serializable]
    public class EachSpatialComparePlan : EachComparePlan
    {
        /// <summary>
        /// 数据的空间关系匹配方式，详情如下：
        /// <para>不同枚举值元素之间代表“并且”关系，例如判断两图形完全重叠（即互相包含)：
        ///     SpatialMatchTypes = new []{ ESpatialMatchTypes.Within, ESpatialMatchTypes.Contains }</para>
        /// <para>通过"|"连接枚举值表示“或者”关系，例如获取相交面积最大或者相交长度最长的图斑：
        ///     SpatialMatchTypes = new []{ ESpatialMatchTypes.MaxInteresctArea | ESpatialMatchTypes.MaxInteresctLength }</para>
        /// </summary>
        public ESpatialMatchTypes[] SpatialMatchTypes { get; set; }


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
        }
    }
}
