using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WLib.ArcGis.DataCheck.Compare.Item
{
    /// <summary>
    /// 对比项集合
    /// </summary>
    public class CompareItemCollection : List<CompareItem>
    {
        /// <summary>
        /// 特殊字段（面积字段、长度字段）
        /// </summary>
        private string[] _specialFields = new string[] { "@area", "@length" };
        /// <summary>
        /// SQL函数和关键字
        /// </summary>
        private string[] _sqlKeywords = new string[] { "convert", "substring ", "len ", "trim ",
                "count", "and", "between", "child", "false", "in", "is", "like", "not", "null", "or", "parent", "true", "sum" };
        /// <summary>
        /// 对比项集合
        /// </summary>
        public CompareItemCollection() { }
        /// <summary>
        /// 对比项集合
        /// </summary>
        /// <param name="compareItems"></param>
        public CompareItemCollection(params CompareItem[] compareItems) => this.AddRange(compareItems);



        /// <summary>
        /// 是否包含特殊字段（面积字段、长度字段）
        /// </summary>
        /// <param name="containArea"> 是否包含面积字段</param>
        /// <param name="containLength">是否包含长度字段</param>
        /// <returns></returns>
        public bool ContainSpecialFields(out bool containArea, out bool containLength)
        {
            var fieldsCompares = this.OfType<FieldCompare>().ToArray();
            containArea = fieldsCompares.Any(v => v.CaculateArea);
            containLength = fieldsCompares.Any(v => v.CaculateLength);
            return containArea || containLength;
        }
        /// <summary>
        /// 从对各对比项中，筛选出表达式中包含的字段名
        /// </summary>
        /// <returns></returns>
        public List<string> GetCompareFields()
        {
            var quoteRegex = new Regex(@"\'.*?\'"); //匹配单引号内的内容
            var nameRegx = new Regex(@"[\[`]?\$?\@?[a-zA-Z_]\w*[\]`]?");//匹配以字母、“_”、“$”开头，或用“[]”、“`”括起来的字段名或函数名

            var resultFields = new List<string>();
            var fieldExpressions = this.OfType<FieldCompare>().Select(v => v.FieldExpression).ToArray();
            foreach (var expression in fieldExpressions)
                resultFields.AddRange(GetExpressionFields(quoteRegex, nameRegx, expression));

            //移除重复字段、特殊字段、关键字、系统函数
            var removeFields = _sqlKeywords.Concat(_specialFields).ToArray();
            resultFields = resultFields.Distinct().Where(v => !removeFields.Contains(v.ToLower())).ToList();
            return resultFields;
        }
        /// <summary>
        /// 从对各对比项中，筛选出表达式中包含的字段名
        /// </summary>
        /// <param name="leftFields">筛选出的代表（表连接中的）左表的字段</param>
        /// <param name="rightFields">筛选出的代表（表连接中的）右表的字段</param>
        /// <returns></returns>
        public List<string> GetCompareFields(out List<string> leftFields, out List<string> rightFields)
        {
            var fields = GetCompareFields();
            leftFields = fields.Where(v => !v.StartsWith("$")).ToList();
            rightFields = fields.Where(v => v.StartsWith("$")).Select(v => v.TrimStart('$')).ToList();
            return fields;
        }
        /// <summary>
        /// 筛选表达式中包含的字段名
        /// </summary>
        /// <param name="nameRegex"></param>
        /// <param name="quoteRegex"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IEnumerable<string> GetExpressionFields(Regex quoteRegex, Regex nameRegex, string expression)
        {
            //查找并且替换掉表达式中的单引号的内容 
            var tmpExpression = expression;
            var quoteContent = quoteRegex.Match(expression).Value;
            if (!string.IsNullOrEmpty(quoteContent))
                tmpExpression = expression.Replace(quoteContent, "");

            //筛选出表达式中的字段名
            return nameRegex.Matches(tmpExpression).Cast<Match>().Select(match => match.Value).Distinct();
        }
    }
}
