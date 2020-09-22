/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WLib.ArcGis.DataCheck.Compare
{
    /// <summary>
    /// 字段对比信息
    /// </summary>
    [Serializable]
    public class FieldCompare : CompareItem
    {
        /// <summary>
        /// 表的对比列的表达式
        /// <para>表达式规则参考“<see cref="System.Data.DataColumn.Expression"/>”的MSDN文档：</para>
        /// <para>https://docs.microsoft.com/zh-cn/dotnet/api/system.data.datacolumn.expression?view=netframework-4.8</para>
        /// <para>注意：若表达式中的字段名与关键字（<see cref="SqlKeywords"/>）同名，需要使用[]或``进行引用</para>
        /// </summary>
        public string FieldExpression { get; set; }
        /// <summary>
        /// 左对比字段
        /// </summary>
        public string LeftColumnName { get; set; }
        /// <summary>
        /// 右对比字段
        /// </summary>
        public string RightColumnName { get; set; }


        /// <summary>
        /// 字段对比信息
        /// </summary>
        public FieldCompare() { }
        /// <summary>
        /// 字段对比信息
        /// </summary>
        public FieldCompare(string fieldExpression)
        {
            FieldExpression = fieldExpression;
            GetCompareFields(out var leftField, out var rightField);
            LeftColumnName = leftField;
            RightColumnName = rightField;
            Description = LeftColumnName + "不一致";
        }


        /// <summary>
        /// 字段对比信息
        /// </summary>
        /// <param name="fieldExpression">表的对比列的表达式</param>
        /// <param name="leftColumnName">左对比字段</param>
        /// <param name="rightColumnName">右对比字段</param>
        /// <param name="description">不满足对比要求时的表述信息</param>
        public FieldCompare(string fieldExpression, string leftColumnName, string rightColumnName, string description)
        {
            FieldExpression = fieldExpression;
            LeftColumnName = leftColumnName;
            RightColumnName = rightColumnName;
            Description = description;
        }
        /// <summary>
        /// 是否包含指定字段
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public bool ContainField(string fieldName) => FieldExpression.ToLower().Contains(fieldName.ToLower());
        /// <summary>
        /// 输出"{Description}: {FieldExpression}"
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Description}: {FieldExpression}";
        /// <summary>
        /// 获取两个对比的字段
        /// </summary>
        /// <param name="leftField"></param>
        /// <param name="rightField"></param>
        public void GetCompareFields(out string leftField, out string rightField)
        {
            var fields = GetExpressionObjectNames();
            fields = fields.Distinct().Where(v => !SpecialKeywords.Contains(v.ToLower())).ToList();
            leftField = fields.FirstOrDefault(v => !v.StartsWith("$"));
            rightField = fields.FirstOrDefault(v => v.StartsWith("$"));
        }
        /// <summary>
        /// 筛选表达式中包含的对象名（包括关键字、函数名、字段等）
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetExpressionObjectNames() => GetExpressionObjectNames(FieldExpression);


        #region 静态成员和方法
        /// <summary>
        /// 匹配<see cref="FieldExpression"/>中的单引号内容的正则表达式
        /// </summary>
        internal static Regex QuoteRegex { get; } = new Regex(@"\'.*?\'");
        /// <summary>
        /// 匹配<see cref="FieldExpression"/>中以字母、“_”、“$”、“@”开头，或用“[]”、“`”括起来的字段名、关键字或函数名的正则表达式
        /// </summary>
        internal static Regex NameRegex { get; } = new Regex(@"[\[`]?\$?\@?[a-zA-Z_]\w*[\]`]?");


        /// <summary>
        /// 需要额外创建和计算的特殊字段：面积字段"@area"
        /// </summary>
        internal const string _AREA = "@area";
        /// <summary>
        /// 需要额外创建和计算的特殊字段：长度字段"@length"
        /// </summary>
        internal const string _LENGTH = "@length";
        /// <summary>
        /// 需要额外创建和计算的特殊字段：记录数字段"@count"
        /// </summary>
        internal const string _COUNT = "@count";
        /// <summary>
        /// <see cref="FieldExpression"/>中的特殊字段（面积字段、长度字段、记录数字段）
        /// </summary>
        internal static string[] CaculateFields { get; } = new string[] { _AREA, _LENGTH, _COUNT, "$" + _AREA, "$" + _LENGTH, "$" + _COUNT };
        /// <summary>
        /// <see cref="FieldExpression"/>中的SQL函数名和关键字
        /// </summary>
        internal static string[] SqlKeywords { get; } = new string[] { "convert", "substring ", "len ", "trim ",
                "count", "and", "between", "child", "false", "in", "is", "like", "not", "null", "or", "parent", "true", "sum" };
        /// <summary>  
        /// <see cref="FieldExpression"/>中的预设关键字，包括特殊字段（面积字段、长度字段、记录数字段）、SQL函数名和关键字
        /// </summary>
        internal static string[] SpecialKeywords { get; } = SqlKeywords.Concat(CaculateFields).ToArray();
        /// <summary>
        /// 筛选表达式中包含的对象名（包括关键字、函数名、字段等）
        /// </summary>
        /// <param name="fieldExpression"></param>
        /// <returns></returns>
        internal static IEnumerable<string> GetExpressionObjectNames(string fieldExpression)
        {
            //查找并且替换掉表达式中的单引号的内容 
            var tmpExpression = fieldExpression;
            var quoteContent = QuoteRegex.Match(fieldExpression).Value;
            if (!string.IsNullOrEmpty(quoteContent))
                tmpExpression = fieldExpression.Replace(quoteContent, "");

            //筛选出表达式中的对象名
            return NameRegex.Matches(tmpExpression).Cast<Match>().Select(match => match.Value).Distinct();
        }
        #endregion
    }
}
