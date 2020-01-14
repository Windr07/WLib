namespace WLib.ArcGis.DataCheck.Compare
{
    /// <summary>
    /// 字段对比信息
    /// </summary>
    public class FieldsCompare : CompareItem
    {
        /// <summary>
        /// 表的对比列的表达式
        /// <para>表达式规则参考“<see cref="System.Data.DataColumn.Expression"/>”的MSDN文档：</para>
        /// <para>https://docs.microsoft.com/zh-cn/dotnet/api/system.data.datacolumn.expression?view=netframework-4.8</para>
        /// </summary>
        public string FieldExpression { get; set; }
        /// <summary>
        /// 表的对比列是否使用了聚合函数
        /// </summary>
        public bool IsTable1FieldGroup { get; set; }
    }
}
