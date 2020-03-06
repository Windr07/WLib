using System;

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
        /// 是否包含面积对比
        /// </summary>
        public bool CaculateArea => FieldExpression.Contains("@Area");
        /// <summary>
        /// 是否包含长度对比
        /// </summary>
        public bool CaculateLength => FieldExpression.Contains("@Length");

        /// <summary>
        /// 添加字段对比项
        /// </summary>
        public FieldCompare()
        {

        }
        /// <summary>
        /// 添加字段对比项
        /// </summary>
        /// <param name="fieldExpression">表的对比列的表达式</param>
        /// <param name="leftColumnName">左对比字段</param>
        /// <param name="rightColumnName">右对比字段</param>
        /// <param name="description">不满足对比要求时的表述信息</param>
        /// <param name="whereClause">数据筛选条件</param>
        /// <param name="areaColumnName">面积字段名</param>
        /// <param name="lengthColumnName">长度字段名</param>
        public FieldCompare(string fieldExpression, string leftColumnName, string rightColumnName, string description)
        {
            Description = description;
            FieldExpression = fieldExpression;
            LeftColumnName = leftColumnName;
            RightColumnName = rightColumnName;
        }
        /// <summary>
        /// 输出"{Description}: {FieldExpression}"
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Description}: {FieldExpression}";
    }
}
