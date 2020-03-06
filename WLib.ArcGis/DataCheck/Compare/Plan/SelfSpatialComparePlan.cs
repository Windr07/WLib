using System;
using WLib.ArcGis.DataCheck.Compare.Enum;

namespace WLib.ArcGis.DataCheck.Compare.Plan
{
    /// <summary>
    /// 单个图层自身信息，通过空间关系进行匹配的对比的方案
    /// </summary>
    [Serializable]
    public class SelfSpatialComparePlan : SelfComparePlan
    {
        /// <summary>
        /// 用于匹配的数据的范围（筛选条件）
        /// </summary>
        public string WhereClause2 { get; set; }
        /// <summary>
        /// 图斑空间关系匹配方式，赋值方式如下：
        /// <para>（1）不同枚举值元素之间代表“并且”关系，例如判断两图形完全重叠（即互相包含)：
        ///     SpatialMatchTypes = new []{ ESpatialMatchTypes.Within, ESpatialMatchTypes.Contains }</para>
        /// <para>（2）通过"|"连接枚举值表示“或者”关系，例如获取相交面积最大或者相交长度最长的图斑：
        ///     SpatialMatchTypes = new []{ ESpatialMatchTypes.MaxInteresctArea | ESpatialMatchTypes.MaxInteresctLength }</para>
        /// </summary>
        public ESpatialMatchTypes[] SpatialMatchTypes { get; set; }
    }
}
