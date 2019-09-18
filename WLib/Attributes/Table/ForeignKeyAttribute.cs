using System;

namespace WLib.Attributes.Table
{
    /// <summary>
    /// 表示外键
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKeyAttribute : Attribute
    {
        /// <summary>
        /// 外键所关联的表
        /// </summary>
        public string ForeignTable { get; set; }
        /// <summary>
        /// 外键所关联的表的字段
        /// </summary>
        public string ForeignField { get; set; }
        /// <summary>
        /// 表示外键
        /// </summary>
        /// <param name="foreignKeyString">外键信息，格式为“ForeignTable.ForeginColumn”</param>
        public ForeignKeyAttribute(string foreignTable) => ForeignTable = foreignTable;
        /// <summary>
        /// 表示外键
        /// </summary>
        /// <param name="foreignTable">外键所关联的表</param>
        /// <param name="foreignField">外键所关联的表的字段</param>
        public ForeignKeyAttribute(string foreignTable, string foreignField)
        {
            ForeignTable = foreignTable;
            ForeignField = foreignField;
        }
    }
}
