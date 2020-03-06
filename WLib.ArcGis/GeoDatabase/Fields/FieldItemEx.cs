/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/8
// desc： 由于ArcGIS的IField是COM组件对象，不能反射且难于调试，通过将IField信息转入此类，以方便反射和调试等操作
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Geodatabase;
using System.ComponentModel;

namespace WLib.ArcGis.GeoDatabase.Fields
{
    /// <summary>
    /// 表示字段
    /// <para>继承和扩展<see cref="FieldItem"/>以包含更多字段信息</para>
    /// <para>包括字段名、别名、类型、长度、精度、小数位数、是否可空、是否必需、是否可编辑</para>
    /// </summary>
    public class FieldItemEx : FieldItem
    {
        /// <summary>
        /// 字段长度
        /// </summary>
        [Description("长度")]
        public int Length { get; set; }
        /// <summary>
        /// 字段精度
        /// <para>表示数值类型字段除小数点外的总长度，即整数位数加上小数位数</para>
        /// <para>例如精度为10，小数位数为5，则整数位数最多可填5位，即使小数位数没有写，整数位数也最多可填5位</para>
        /// <para>注意：个人地理数据库(mdb)可能无法获取精度、小数位数等信息，可能需要从获取Number Format中获取</para>
        /// </summary>
        [Description("精度")]
        public int Precision { get; set; }
        /// <summary>
        /// 小数位数
        /// <para>注意：个人地理数据库(mdb)可能无法获取精度、小数位数等信息，可能需要从获取Number Format中获取</para>
        /// </summary>
        [Description("小数位数")]
        public int Scale { get; set; }
        /// <summary>
        /// 是否允许为空
        /// </summary>
        [Description("允许为空")]
        public bool IsNullable { get; set; }
        /// <summary>
        /// 字段是否必需，例如ID字段是必需的
        /// </summary>
        [Description("字段必需")]
        public bool Required { get; set; }
        /// <summary>
        /// 字段是否可编辑
        /// </summary>
        [Description("字段可编辑")]
        public bool Editable { get; set; }


        /// <summary>
        /// 表示字段
        /// <para>继承和扩展<see cref="FieldItem"/>以包含更多字段信息</para>
        /// <para>包括字段名、别名、类型、长度、精度、小数位数、是否可空、是否必需、是否可编辑</para>
        /// </summary>
        public FieldItemEx() { }
        /// <summary>
        /// 表示字段
        /// <para>继承和扩展<see cref="FieldItem"/>以包含更多字段信息</para>
        /// <para>包括字段名、别名、类型、长度、精度、小数位数、是否可空、是否必需、是否可编辑</para>
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="aliasName">字段别名</param>
        /// <param name="fieldType">字段类型</param>
        public FieldItemEx(string name, string aliasName, esriFieldType fieldType) : base(name, aliasName, fieldType)
        {
        }
        /// <summary>
        /// 表示字段
        /// <para>继承和扩展<see cref="FieldItem"/>以包含更多字段信息</para>
        /// <para>包括字段名、别名、类型、长度、精度、小数位数、是否可空、是否必需、是否可编辑</para>
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
        public FieldItemEx(string name, string aliasName, esriFieldType fieldType,
            int length, int precision, int scale, bool isNullable, bool required, bool editable) : base(name, aliasName, fieldType)
        {
            Length = length;
            Precision = precision;
            Scale = scale;
            IsNullable = isNullable;
            Required = required;
            Editable = editable;
        }
    }
}
