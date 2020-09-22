/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using static WLib.ArcGis.DataCheck.Compare.FieldCompare;

namespace WLib.ArcGis.DataCheck.Compare.Item
{
    /// <summary>
    /// 对比项集合
    /// </summary>
    [Serializable]
    public class CompareItemCollection : List<CompareItem>
    {
        /// <summary>
        /// 对比项集合
        /// </summary>
        public CompareItemCollection() { }
        /// <summary>
        /// 对比项集合
        /// </summary>
        /// <param name="compareItems"></param>
        public CompareItemCollection(params CompareItem[] compareItems) => AddRange(compareItems);

        /// <summary>
        /// 是否包含需要额外创建和计算的字段（面积字段、长度字段、记录数字段）
        /// </summary>
        /// <param name="containArea"> 是否包含需要额外创建和计算的面积字段"@area"</param>
        /// <param name="containLength">是否包含需要额外创建和计算的长度字段"@length"</param>
        /// <param name="containCount">是否包含需要额外创建和计算的记录数字段"@count"</param>
        /// <returns></returns>
        public bool ContainCaculateFields(out bool containArea, out bool containLength, out bool containCount)
        {
            var fieldsCompares = this.OfType<FieldCompare>().ToArray();
            containArea = fieldsCompares.Any(v => v.ContainField(_AREA) || v.ContainField("$" + _AREA));
            containLength = fieldsCompares.Any(v => v.ContainField(_LENGTH) || v.ContainField("$" + _LENGTH));
            containCount = fieldsCompares.Any(v => v.ContainField(_COUNT) || v.ContainField("$" + _COUNT));
            return containArea || containLength || containCount;
        }
        /// <summary>
        /// 从对各对比项中，筛选出表达式中包含的字段名
        /// </summary>
        /// <param name="leftFields">筛选出的代表（表连接中的）左表的字段</param>
        /// <param name="rightFields">筛选出的代表（表连接中的）右表的字段</param>
        /// <returns></returns>
        public List<string> GetCompareFields(out List<string> leftFields, out List<string> rightFields)
        {
            var fields = this.OfType<FieldCompare>().SelectMany(v => v.GetExpressionObjectNames()).ToList();
            fields = fields.Distinct().Where(v => !SpecialKeywords.Contains(v.ToLower())).ToList();//移除重复字段、特殊字段、关键字、系统函数

            leftFields = fields.Where(v => !v.StartsWith("$")).ToList();
            rightFields = fields.Where(v => v.StartsWith("$")).Select(v => v.TrimStart('$')).ToList();
            return fields;
        }
        /// <summary>
        /// 获取对比项集合的全部信息
        /// </summary>
        /// <returns></returns>
        public virtual string[] GetAllInfo()
        {
            var lines = new string[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                switch (this[i])
                {
                    case FieldCompare fieldCompare:
                        lines[i] = $"左表字段【{fieldCompare.LeftColumnName}\t右表字段【{fieldCompare.RightColumnName}】\t表达式【{fieldCompare.FieldExpression}】\t{fieldCompare.Description}";
                        break;
                    default:
                        lines[i] = this[i].Description;
                        break;
                }
            }
            return lines.ToArray();
        }
    }
}
