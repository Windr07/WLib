/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using WLib.Data;

namespace WLib.Database.TableInfo
{
    /// <summary>
    /// 表示被编辑的字段值对象，包括：
    /// <para>字段值（原字段值、新设定的字段值）</para>
    /// <para>基本属性（字段名、别名、类型）</para>
    /// <para>常用属性（长度、精度、小数位数、是否可空、 是否必需、是否可编辑、是否主键、是否显示、备注、默认值、是否忽略）</para>
    /// <para>取值约束（值域、对应字典表、候选列表）等</para>
    /// </summary>
    [Serializable]
    public class EditItem : FieldItem
    {
        /// <summary>
        /// 字段值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 新设定的字段值
        /// </summary>
        public string NewValue { get; set; }
        /// <summary>
        /// 字段值是否被修改
        /// </summary>
        public bool Changed => Value != NewValue;
        /// <summary>
        /// 字段值是否使用了字典表
        /// </summary>
        public bool UseDict { get; set; }
        /// <summary>
        /// 被编辑的字段值对象，包括
        /// <para>字段值（原字段值、新设定的字段值）</para>
        /// <para>基本属性（字段名、别名、类型）</para>
        /// <para>常用属性（长度、精度、小数位数、是否可空、 是否必需、是否可编辑、是否主键、是否显示、备注、默认值、是否忽略）</para>
        /// <para>取值约束（值域、对应字典表、候选列表）等</para>
        /// </summary>
        public EditItem() { }
        /// <summary>
        /// 被编辑的字段值对象，包括
        /// <para>字段值（原字段值、新设定的字段值）</para>
        /// <para>基本属性（字段名、别名、类型）</para>
        /// <para>常用属性（长度、精度、小数位数、是否可空、 是否必需、是否可编辑、是否主键、是否显示、备注、默认值、是否忽略）</para>
        /// <para>取值约束（值域、对应字典表、候选列表）等</para>
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldAliasName">字段别名</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="fieldType">字段类型</param>
        public EditItem(string fieldName, string fieldAliasName, string fieldValue, Type fieldType)
        {
            this.Name = fieldName;
            this.AliasName = fieldAliasName;
            this.NewValue = this.Value = fieldValue;
            this.FieldType = fieldType;
        }
        /// <summary>
        /// 被编辑的字段值对象，包括
        /// <para>字段值（原字段值、新设定的字段值）</para>
        /// <para>基本属性（字段名、别名、类型）</para>
        /// <para>常用属性（长度、精度、小数位数、是否可空、 是否必需、是否可编辑、是否主键、是否显示、备注、默认值、是否忽略）</para>
        /// <para>取值约束（值域、对应字典表、候选列表）等</para>
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldAliasName">字段别名</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="fieldType">字段类型</param>
        /// <param name="check">对字段值进行检查的方法 (第一个参数为输入的字段值；第二个参数为字段类型；第三个参数为返回值，返回检查结果信息，若为null表示通过检查)</param>
        public EditItem(string fieldName, string fieldAliasName, string fieldValue, Type fieldType, Func<string, Type, string> check)
        {
            this.Name = fieldName;
            this.AliasName = fieldAliasName;
            this.NewValue = this.Value = fieldValue;
            this.FieldType = fieldType;
            this.Editable = true;
            this.Nullable = false;
            this.Check = check;
        }


        /// <summary>
        /// 验证输入值是否符合字段规范
        /// <para>包括：非空验证、长度验证、类型验证、值域验证、小数位数验证、自定义验证（<see cref="Check"/>）</para>
        /// </summary>
        /// <param name="value">输入值</param>
        /// <returns>返回验证结果信息，若通过验证则返回null或string.Empty</returns>
        public string CheckValue() => base.CheckValue(NewValue);
        /// <summary>
        /// 复制<see cref="FieldItem"/>对象，并且转成<see cref="EditItem"/>对象
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static EditItem FromFieldItem(FieldItem field) => field.CopyByExpTree<FieldItem, EditItem>();
        /// <summary>
        /// 复制<see cref="FieldItem"/>对象，并且转成<see cref="EditItem"/>对象
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static IEnumerable<EditItem> FromFieldItems(IEnumerable<FieldItem> fields)
        {
            foreach (var field in fields)
                yield return field.CopyByExpTree<FieldItem, EditItem>();
        }
    }
}
