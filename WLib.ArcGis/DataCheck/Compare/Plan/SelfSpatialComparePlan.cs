/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Linq;
using System.Text;
using WLib.ArcGis.DataCheck.Core;
using WLib.Attributes.Description;

namespace WLib.ArcGis.DataCheck.Compare.Plan
{
    /// <summary>
    /// 单个图层自身信息，通过空间关系进行匹配的对比的方案
    /// </summary>
    [Serializable]
    public class SelfSpatialComparePlan : SelfComparePlan, ISpatialComparePlan
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
        /// <summary>
        /// 进行空间关系匹配的容差值（默认值0.001）
        /// </summary>
        public double Tolerance { get; set; } = 0.001;


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
            sb.AppendLine("空间匹配方式：" + SpatialMatchTypes.Select(v => v.GetDescriptionEx()).Aggregate((a, b) => a + " and " + b));
            sb.AppendLine("空间匹配容差：" + Tolerance);
            foreach (var line in CompareItems.GetAllInfo())
                sb.AppendLine(line);

            return sb.ToString();
        }
    }
}
