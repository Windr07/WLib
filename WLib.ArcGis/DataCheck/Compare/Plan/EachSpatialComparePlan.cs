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
using WLib.ArcGis.DataCheck.Core;
using WLib.Attributes.Description;

namespace WLib.ArcGis.DataCheck.Compare.Plan
{
    /// <summary>
    /// 两个图层之间，通过空间关系进行匹配的对比的方案
    /// </summary>
    [Serializable]
    public class EachSpatialComparePlan : EachComparePlan, ISpatialComparePlan
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
        /// 进行空间关系匹配的容差值（默认值0.001）
        /// </summary>
        public double Tolerance { get; set; } = 0.001;


        /// <summary>
        /// 两个图层之间，通过空间关系进行匹配的对比的方案
        /// </summary>
        public EachSpatialComparePlan() { }
        /// <summary>
        /// 两个图层之间，通过空间关系进行匹配的对比的方案
        /// </summary>
        /// <param name="tablePath1"> 被对比表格（左表）的路径</param>
        /// <param name="tablePath2">对比表格（右表）路径</param>
        /// <param name="idField1">被对比表格（左表）的作为ID标识的列（例如FID,单元编号等）</param>
        /// <param name="idField2">对比表格（右表）的作为ID标识的列（例如FID,单元编号等）</param>
        public EachSpatialComparePlan(string tablePath1, string tablePath2, string idField1, string idField2)
        {
            TablePath = tablePath1;
            TablePath2 = tablePath2;
            IdField = idField1;
            IdField2 = idField2;
        }


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
            sb.AppendLine("空间匹配方式：" + SpatialMatchTypes.Select(v => v.GetDescription()).Aggregate((a, b) => a + " and " + b));
            sb.AppendLine("空间匹配容差：" + Tolerance);
            foreach (var line in CompareItems.GetAllInfo())
                sb.AppendLine(line);

            return sb.ToString();
        }
    }
}
