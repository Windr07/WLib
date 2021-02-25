/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using WLib.Database.TableInfo.FieldDomain;
using WLib.Reflection;

namespace WLib.Database.TableInfo
{
    /// <summary>
    /// 表示字段，包括：
    /// <para>基本属性（字段名、别名、类型）</para>
    /// <para>常用属性（长度、精度、小数位数、是否可空、 是否必需、是否可编辑、是否主键、是否显示、备注、默认值、是否忽略）</para>
    /// <para>取值约束（值域、对应字典表、候选列表）等</para>
    /// </summary>
    [Serializable]
    public class FieldItem
    {
        #region 基本属性
        /// <summary>
        /// 字段名
        /// </summary>
        [Description("名称")]
        public string Name { get; set; }
        /// <summary>
        /// 字段别名
        /// </summary>
        [Description("别名")]
        public string AliasName { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        [Description("类型")]
        public Type FieldType { get; set; }
        #endregion


        #region 常用属性
        /// <summary>
        /// 字段长度
        /// </summary>
        [Description("长度")]
        public int Length { get; set; }
        /// <summary>
        /// 字段精度
        /// <para>表示数值类型字段除小数点外的总长度，即整数位数加上小数位数</para>
        /// <para>例如精度为10，小数位数为5，则整数位数最多可填5位，即使小数位数没有写，整数位数也最多可填5位</para>
        /// <para>*若涉及GIS数据中的个人地理数据库(Access)可能无法获取精度、小数位数等信息，可能需要从获取Number Format中获取</para>
        /// </summary>
        [Description("精度")]
        public int Precision { get; set; }
        /// <summary>
        /// 小数位数
        /// <para>*若涉及GIS数据中的个人地理数据库(Access)可能无法获取精度、小数位数等信息，可能需要从获取Number Format中获取</para>
        /// </summary>
        [Description("小数位数")]
        public int Scale { get; set; }
        /// <summary>
        /// 是否为主键
        /// </summary>
        [Description("是否主键")]
        public bool IsPrimaryKey { get; set; }
        /// <summary>
        /// 字段是否可空
        /// </summary>
        [Description("允许为空")]
        public bool Nullable { get; set; } = true;
        /// <summary>
        /// 是否显示字段
        /// </summary>
        [Description("是否显示")]
        public bool Visible { get; set; } = true;
        /// <summary>
        /// 字段值是否可以修改
        /// </summary>
        [Description("可编辑")]
        public bool Editable { get; set; } = true;
        /// <summary>
        /// 字段是否必需，例如ID字段是必需的
        /// </summary>
        [Description("必需字段")]
        public bool Required { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public string Comment { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        [Description("默认值")]
        public string DefaultValue { get; set; }
        /// <summary>
        /// 字段是否忽略，不参与数据的增删改等操作
        /// </summary>
        [Description("忽略字段")]
        public bool Ignore { get; set; }
        #endregion


        #region 值域与候选
        /// <summary>
        /// 字段值域范围
        /// </summary>
        [Description("值域范围")]
        public IFieldDomain Domain { get; set; }
        /// <summary>
        /// 字段对应的字典表
        /// </summary>
        [Description("字典表")]
        public DictionaryTable DictionaryTable { get; set; }
        /// <summary>
        /// 字段值候选列表
        /// </summary>
        [Description("候选列表")]
        public string[] CandidatesItems { get; set; }
        /// <summary>
        /// 必须字典表或候选列表范围
        /// </summary>
        [Description("必须字典或候选列表范围")]
        public bool MustInList { get; set; }
        /// <summary>
        /// 使用字典表键，True表示实际字段值应当为字典表的键（编码），False表示实际字段值应当为字典表的值（描述）
        /// </summary>
        [Description("使用字典表键")]
        public bool UseDictKey { get; set; } = true;
        #endregion


        #region 构造函数
        /// <summary>
        /// 表示字段
        /// </summary>
        public FieldItem() { }
        /// <summary>
        /// 表示字段
        /// </summary>
        /// <param name="name">字段名</param>
        public FieldItem(string name) => this.Name = name;
        /// <summary>
        /// 表示字段
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="aliasName">字段别名</param>
        /// <param name="fieldType">字段类型</param>
        public FieldItem(string name, string aliasName, Type fieldType) : this(name)
        {
            AliasName = aliasName;
            FieldType = fieldType;
        }
        /// <summary>
        /// 表示字段
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="aliasName">字段别名</param>
        /// <param name="fieldType">字段类型</param>
        /// <param name="length">字段长度</param>
        /// <param name="precision">字段精度，表示数值类型字段除小数点外的总长度</param>
        /// <param name="scale">小数位数</param>
        /// <param name="isNullable">是否允许为空</param>
        /// <param name="required">字段是否必需，例如ID字段是必需的</param>
        /// <param name="editable">字段是否可编辑</param>
        public FieldItem(string name, string aliasName, Type fieldType, int length, int precision, int scale, bool isNullable, bool required, bool editable) : this(name)
        {
            Name = name;
            AliasName = aliasName;
            FieldType = fieldType;
            Length = length;
            Precision = precision;
            Scale = scale;
            Nullable = isNullable;
            Required = required;
            Editable = editable;
        }
        #endregion


        #region 字段值验证
        /// <summary>
        /// 输入提示
        /// </summary>
        public string InputTip;
        /// <summary>
        /// 调用<see cref="CheckValueByType(string, Type)"/>方法检查字段值不符合要求时返回的提示信息
        /// </summary>
        public static DefaultInpuTips Tips = new DefaultInpuTips();
        /// <summary>
        /// 对字段值进行检查的方法
        /// <para>第一个参数为输入的字段值；</para>
        /// <para>第二个参数为字段类型；</para>
        /// <para>第三个参数为返回值，返回检查结果信息，若为null表示通过检查</para>
        /// </summary>
        public Func<string, Type, string> Check;
        /// <summary>
        /// 验证输入值是否符合字段规范
        /// <para>包括：非空验证、长度验证、类型验证、值域验证、小数位数验证、自定义验证（<see cref="Check"/>）</para>
        /// </summary>
        /// <param name="value">输入值</param>
        /// <returns>返回验证结果信息，若通过验证则返回null或string.Empty</returns>
        public string CheckValue(string value)
        {
            if (!Nullable && string.IsNullOrEmpty(value))//1、非空验证
                return "此项不允许为空";
            if (string.IsNullOrEmpty(value))//字段值为空，则不进行之后的验证
                return null;

            if (FieldType == typeof(string) && Length > 0 && value.Length > Length)//2、长度验证
                return "长度不得大于" + Length;

            var message = CheckValueByType(value, FieldType);//3、类型验证
            if (message != null) return message;

            Domain?.CheckValue(value, out message);//4、值域验证
            if (message != null) return message;

            if (FieldType.IsFloatType()) //5、小数位数验证
            {
                if (Length > 0 && Scale > 0)
                {
                    int pointTag = value.Contains(".") ? 1 : 0;//是否包含小数点
                    int correctLength = Length + pointTag + Scale;
                    if (value.Length > correctLength)
                        return $"要求数值的整数部分位数不能超过{(Length - Scale)}，小数位为{Scale}位";
                }
            }

            if (MustInList)//6、字典表或候选列表范围验证
            {
                if (DictionaryTable != null)
                {
                    if (DictionaryTable.ContainsKey(value) == false &&
                        DictionaryTable.ContainsValue(value) == false)
                        return $"字段值必须在字典表范围内";
                }
                if (CandidatesItems != null && CandidatesItems.Contains(value) == false)
                    return $"字段值必须在候选列表范围内";
            }

            if (Check != null)//7、自定义验证
                return Check(value, FieldType);

            return null;
        }
        /// <summary>
        /// 检查字段值是否符合字段类型要求，返回null表示符合要求，否则返回提示信息
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected string CheckValueByType(string fieldValue, Type type)
        {
            if (type == typeof(int)) return int.TryParse(fieldValue, out _) ? null : Tips.IntMsg;
            if (type == typeof(uint)) return uint.TryParse(fieldValue, out _) ? null : Tips.UintMsg;
            if (type == typeof(short)) return short.TryParse(fieldValue, out _) ? null : Tips.ShortMsg;
            if (type == typeof(ushort)) return ushort.TryParse(fieldValue, out _) ? null : Tips.UshortMsg;
            if (type == typeof(byte)) return byte.TryParse(fieldValue, out _) ? null : Tips.ByteMsg;
            if (type == typeof(sbyte)) return sbyte.TryParse(fieldValue, out _) ? null : Tips.SbyteMsg;
            if (type == typeof(long)) return long.TryParse(fieldValue, out _) ? null : Tips.LongMsg;
            if (type == typeof(ulong)) return ulong.TryParse(fieldValue, out _) ? null : Tips.UlongMsg;
            if (type == typeof(float)) return float.TryParse(fieldValue, out _) ? null : Tips.FloatMsg;
            if (type == typeof(double)) return double.TryParse(fieldValue, out _) ? null : Tips.DoubleMsg;
            if (type == typeof(bool)) return bool.TryParse(fieldValue, out _) ? null : Tips.BoolMsg;
            if (type == typeof(decimal)) return decimal.TryParse(fieldValue, out _) ? null : Tips.DecimalMsg;
            if (type == typeof(char)) return char.TryParse(fieldValue, out _) ? null : Tips.CharMsg;
            if (type == typeof(DateTime)) return DateTime.TryParse(fieldValue, out _) ? null : Tips.DateTimeMsg;
            if (type == typeof(string)) return null;
            return $"未实现类型{type}的字段的字段值检查";
        }
        #endregion


        #region 格式化显示
        /// <summary>
        /// 确定ToString方法输出的内容
        /// <para>N-字段名，A-字段别名，T-字段类型，例如Format="N,A,(T)"，则ToString()结果为"字段名,字段别名,(字段类型)"</para>
        /// </summary>
        [Description("输出格式")]
        public string Format { get; set; } = "A";
        /// <summary>
        /// 根据<see cref="Format"/>格式化输出字段信息
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var c in Format)
            {
                if (c == 'A') sb.Append(AliasName);
                else if (c == 'N') sb.Append(Name);
                else if (c == 'T') sb.Append(FieldType.Name);
                else sb.Append(c);
            }
            return sb.ToString();
            //return string.Concat(Format.Select(c =>
            //{
            //    if (c == 'A') return AliasName;
            //    else if (c == 'N') return Name;
            //    else if (c == 'T') return FieldType.Name;
            //    else return c.ToString();
            //}));
        }
        //public object Clone()
        //{
        //    var fieldItem = new FieldItem
        //    {
        //        Name = this.Name,
        //        AliasName = this.AliasName,
        //        FieldType = this.FieldType,
        //        Length = this.Length,
        //        Precision = this.Precision,
        //        Scale = this.Scale,
        //        Comment = this.Comment,
        //        DefaultValue = this.DefaultValue,
        //        IsPrimaryKey = this.IsPrimaryKey,
        //        Nullable = this.Nullable,
        //        Visible = this.Visible,
        //        Editable = this.Editable,
        //        Required = this.Required,
        //        Ignore = this.Ignore,
        //        Domain = this.Domain,
        //    };
        //    var dictTable = new DictionaryTable() { DictionaryName = this.DictionaryTable.DictionaryName };
        //    foreach (var item in this.DictionaryTable.CodeNameDict)
        //        dictTable.CodeNameDict.Add(item.Key, item.Value);
        //    fieldItem.DictionaryTable = dictTable;
        //    Array.Copy(this.CandidatesItems, fieldItem.CandidatesItems, this.CandidatesItems.Length);
        //    return fieldItem;
        //}
        #endregion
    }
}
