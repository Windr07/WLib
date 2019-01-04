namespace WLib.UserCtrls.Dev.EditForm
{
    /// <summary>
    /// 被编辑的字段值对象，包含字段名、别名、值、是否可编辑
    /// </summary>
    public class EditItem
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 字段别名
        /// </summary>
        public string FieldAliasName { get; set; }
        /// <summary>
        /// 字段值
        /// </summary>
        public string FieldValue { get; set; }
        /// <summary>
        /// 字段是否可以修改
        /// </summary>
        public bool EditAble { get; set; }

        /// <summary>
        /// 被编辑的字段值对象，包含字段名、别名、值、是否可编辑
        /// </summary>
        public EditItem()
        {
        }
        /// <summary>
        /// 被编辑的字段值对象，包含字段名、别名、值、是否可编辑
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldAliasName">字段别名</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="editable">字段是否可以修改</param>
        public EditItem(string fieldName, string fieldAliasName, string fieldValue, bool editable = true)
        {
            this.FieldName = fieldName;
            this.FieldAliasName = fieldAliasName;
            this.FieldValue = fieldValue;
            this.EditAble = editable;
        }
    }
}
