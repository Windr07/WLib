/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.GeoDatabase.Fields;

namespace WLib.WinCtrls.Dev.EditForm
{
    /// <summary>
    /// 被编辑的字段值对象，包含字段名、别名、类型、值、是否可编辑
    /// </summary>
    public class EditItem
    {
        private Type _fieldType;
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 字段别名
        /// </summary>
        public string FieldAliasName { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public Type FieldType
        {
            get => _fieldType;
            set
            {
                _fieldType = value;
                Check -= CheckValueOnType;
                Check += CheckValueOnType;
            }
        }
        /// <summary>
        /// 字段值
        /// </summary>
        public string FieldValue { get; set; }
        /// <summary>
        /// 字段是否可以修改
        /// </summary>
        public bool EditAble { get; set; }
        /// <summary>
        /// 输入提示
        /// </summary>
        public string InputTip { get; set; }
        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool Nullable { get; set; }
        /// <summary>
        /// 对字段值进行检查的方法
        /// (第一个参数为输入的字段值；第二个参数为字段类型；第三个参数为返回值，返回检查结果信息，若为null表示通过检查)
        /// </summary>
        public Func<string, Type, string> Check { get; set; }


        /// <summary>
        /// 被编辑的字段值对象，包含字段名、别名、类型、值、是否可编辑
        /// </summary>
        public EditItem()
        {

        }

        /// <summary>
        /// 被编辑的字段值对象，包含字段名、别名、类型、值、是否可编辑
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldAliasName">字段别名</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="fieldType">字段类型</param>
        /// <param name="nullable"></param>
        /// <param name="editable">字段是否可以修改</param>
        public EditItem(string fieldName, string fieldAliasName, string fieldValue, Type fieldType, bool nullable = true, bool editable = true)
        {
            this.FieldName = fieldName;
            this.FieldAliasName = fieldAliasName;
            this.FieldValue = fieldValue;
            this.FieldType = fieldType;
            this.Nullable = nullable;
            this.EditAble = editable;
        }

        /// <summary>
        /// 被编辑的字段值对象，包含字段名、别名、类型、值、是否可编辑
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldAliasName">字段别名</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="fieldType">字段类型</param>
        /// <param name="check">对字段值进行检查的方法 (第一个参数为输入的字段值；第二个参数为字段类型；第三个参数为返回值，返回检查结果信息，若为null表示通过检查)</param>
        public EditItem(string fieldName, string fieldAliasName, string fieldValue, Type fieldType, Func<string, Type, string> check)
        {
            this.FieldName = fieldName;
            this.FieldAliasName = fieldAliasName;
            this.FieldValue = fieldValue;
            this.FieldType = fieldType;
            this.EditAble = true;
            this.Nullable = false;
            this.Check = check;
        }
        /// <summary>
        /// 检查字段值是否符合要求
        /// </summary>
        /// <returns></returns>
        public string CheckValue()
        {
            return Nullable && string.IsNullOrEmpty(FieldValue) ? null : Check(FieldValue, FieldType);
        }

        /// <summary>
        /// 检查字段值是否符合字段类型要求，返回null表示符合要求，否则返回提示信息
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected string CheckValueOnType(string fieldValue, Type type)
        {
            DefaultInpuTips tips = new DefaultInpuTips();
            if (type == typeof(int)) return int.TryParse(fieldValue, out _) ? null : tips.IntMsg;
            if (type == typeof(uint)) return uint.TryParse(fieldValue, out _) ? null : tips.UintMsg;
            if (type == typeof(short)) return short.TryParse(fieldValue, out _) ? null : tips.ShortMsg;
            if (type == typeof(ushort)) return ushort.TryParse(fieldValue, out _) ? null : tips.UshortMsg;
            if (type == typeof(byte)) return byte.TryParse(fieldValue, out _) ? null : tips.ByteMsg;
            if (type == typeof(sbyte)) return sbyte.TryParse(fieldValue, out _) ? null : tips.SbyteMsg;
            if (type == typeof(long)) return long.TryParse(fieldValue, out _) ? null : tips.LongMsg;
            if (type == typeof(ulong)) return ulong.TryParse(fieldValue, out _) ? null : tips.UlongMsg;
            if (type == typeof(float)) return float.TryParse(fieldValue, out _) ? null : tips.FloatMsg;
            if (type == typeof(double)) return double.TryParse(fieldValue, out _) ? null : tips.DoubleMsg;
            if (type == typeof(bool)) return bool.TryParse(fieldValue, out _) ? null : tips.BoolMsg;
            if (type == typeof(decimal)) return decimal.TryParse(fieldValue, out _) ? null : tips.DecimalMsg;
            if (type == typeof(char)) return char.TryParse(fieldValue, out _) ? null : tips.CharMsg;
            if (type == typeof(DateTime)) return DateTime.TryParse(fieldValue, out _) ? null : tips.DateTimeMsg;
            if (type == typeof(string)) return null;
            return $"未实现类型{type}的字段的字段值检查";
        }
        /// <summary>
        /// 将IField转成EditItem
        /// </summary>
        /// <param name="field"></param>
        /// <param name="fieldValue"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        public static EditItem FromIField(IField field, string fieldValue = null, Func<string, Type, string> check = null)
        {
            return new EditItem(field.Name, field.AliasName, fieldValue,
                FieldOpt.FieldTypeToDoNetType(field.Type), field.Editable)
            { Check = check };
        }
        /// <summary>
        /// 将FieldItem转成EditItem
        /// </summary>
        /// <param name="fieldItem"></param>
        /// <param name="fieldValue"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        public static EditItem FromFieldItem(FieldItem fieldItem, string fieldValue = null, Func<string, Type, string> check = null)
        {
            return new EditItem(fieldItem.Name, fieldItem.AliasName,
                 fieldValue, FieldOpt.FieldTypeToDoNetType(fieldItem.FieldType))
            { Check = check };
        }

        /// <summary>
        /// 表示调用CheckValueOnType方法检查字段值不符合要求时返回的提示信息
        /// </summary>
        public class DefaultInpuTips
        {
            /// <summary>
            /// 默认值为：请输入整型数值
            /// </summary>
            public string IntMsg = "请输入整型数值";
            /// <summary>
            /// 默认值为：请输入非负整型数值
            /// </summary>
            public string UintMsg = "请输入非负整型数值";
            /// <summary>
            /// 默认值为：请输入短整型数值
            /// </summary>
            public string ShortMsg = "请输入短整型数值";
            /// <summary>
            /// 默认值为：请输入非负短整型数值
            /// </summary>
            public string UshortMsg = "请输入非负短整型数值";
            /// <summary>
            /// 默认值为：请输入小于255的非负整型数值
            /// </summary>
            public string ByteMsg = "请输入小于255的非负整型数值";
            /// <summary>
            /// 默认值为：请输入小于127的整型数值
            /// </summary>
            public string SbyteMsg = "请输入小于127的整型数值";
            /// <summary>
            /// 默认值为：请输入整型数值
            /// </summary>
            public string LongMsg = "请输入整型数值";
            /// <summary>
            /// 默认值为：请输入非负整型数值
            /// </summary>
            public string UlongMsg = "请输入非负整型数值";
            /// <summary>
            /// 默认值为：请输入数值
            /// </summary>
            public string FloatMsg = "请输入数值";
            /// <summary>
            /// 默认值为：请输入数值
            /// </summary>
            public string DoubleMsg = "请输入数值";
            /// <summary>
            /// 默认值为：请输入True或False
            /// </summary>
            public string BoolMsg = "请输入True或False";
            /// <summary>
            /// 默认值为：请输入数值
            /// </summary>
            public string DecimalMsg = "请输入数值";
            /// <summary>
            /// 默认值为：请输入单个字符
            /// </summary>
            public string CharMsg = "请输入单个字符";
            /// <summary>
            /// 默认值为：请输入符合格式的正确日期
            /// </summary>
            public string DateTimeMsg = "请输入符合格式的正确日期";
        }
    }
}
