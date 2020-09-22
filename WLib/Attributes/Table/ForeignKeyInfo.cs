/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Attributes.Table
{
    /// <summary>
    /// 字段外键信息
    /// </summary>
    public class ForeignKeyInfo
    {
        /// <summary>
        /// 拥有外键的字段
        /// </summary>
        public string CurrentField { get; set; }
        /// <summary>
        /// 外键所关联的表
        /// </summary>
        public string ForeignTable { get; set; }
        /// <summary>
        /// 外键所关联的表的字段
        /// </summary>
        public string ForeignField { get; set; }

        /// <summary>
        /// 字段外键信息
        /// </summary>
        public ForeignKeyInfo() { }
        /// <summary>
        /// 字段外键信息
        /// </summary>
        /// <param name="currentField">拥有外键的字段</param>
        /// <param name="foreignTable">外键所关联的表</param>
        /// <param name="foreignField">外键所关联的表的字段</param>
        public ForeignKeyInfo(string currentField, string foreignTable, string foreignField)
        {
            CurrentField = currentField;
            ForeignTable = foreignTable;
            ForeignField = foreignField;
        }
    }
}
