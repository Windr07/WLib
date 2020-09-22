/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using WLib.ArcGis.DataCheck.Core;

namespace WLib.ArcGis.DataCheck.Compare.Plan
{
    public interface ISpatialComparePlan
    {
        /// <summary>
        /// 数据的空间关系匹配方式，详情如下：
        /// <para>不同枚举值元素之间代表“并且”关系，例如判断两图形完全重叠（即互相包含)：
        ///     SpatialMatchTypes = new []{ ESpatialMatchTypes.Within, ESpatialMatchTypes.Contains }</para>
        /// <para>通过"|"连接枚举值表示“或者”关系，例如获取相交面积最大或者相交长度最长的图斑：
        ///     SpatialMatchTypes = new []{ ESpatialMatchTypes.MaxInteresctArea | ESpatialMatchTypes.MaxInteresctLength }</para>
        /// </summary>
        ESpatialMatchTypes[] SpatialMatchTypes { get; set; }
        /// <summary>
        /// 进行空间关系匹配的容差值
        /// </summary>
        double Tolerance { get; set; }
    }
}
