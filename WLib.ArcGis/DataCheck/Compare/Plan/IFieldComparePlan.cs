/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.ArcGis.DataCheck.Compare.Plan
{
    public interface IFieldComparePlan
    {
        /// <summary>
        /// 匹配的字段1
        /// </summary>
        string MatchField1 { get; set; }
        /// <summary>
        /// 匹配的字段2
        /// </summary>
        string MatchField2 { get; set; }
        /// <summary>
        /// 判断<see cref="MatchField1"/>是否为表达式（True）还是单纯的字段或空值（False）
        /// </summary>
        bool MatchField1_IsExpression { get; }
        /// <summary>
        /// 判断<see cref="MatchField2"/>是否为表达式（True）还是单纯的字段或空值（False）
        /// </summary>
        bool MatchField2_IsExpression { get; }
    }
}
