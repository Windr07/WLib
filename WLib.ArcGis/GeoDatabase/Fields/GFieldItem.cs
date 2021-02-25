/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017/1/6 16:20:43
// desc： 由于ArcGIS的IField是COM组件对象，不能反射且难于调试，通过将IField信息转入此类，以方便反射和调试等操作
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Geodatabase;
using System;
using System.ComponentModel;
using WLib.ArcGis.Data;
using WLib.Database.TableInfo;

namespace WLib.ArcGis.GeoDatabase.Fields
{
    /// <summary>
    /// 表示字段
    /// </summary>
    [Serializable]
    public class GFieldItem : FieldItem
    {
        /// <summary>
        /// ESRI字段类型
        /// </summary>
        [Description("类型")]
        public esriFieldType eFieldType { get; set; }
        /// <summary>
        /// 获得字段类型的文字描述
        /// </summary>
        /// <returns></returns>
        public string FieldTypeDesciption => eFieldType.GetFieldTypeDesciption();
        /// <summary>
        /// 获得字段类型的中文文字描述
        /// </summary>
        /// <returns></returns>
        public string FieldTypeDesciptionCn => eFieldType.GetFieldTypeDesciptionCn();


        /// <summary>
        /// 表示字段
        /// </summary>
        public GFieldItem() { }
        /// <summary>
        /// 表示字段
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="aliasName">字段别名</param>
        /// <param name="fieldType">字段类型</param>
        public GFieldItem(string name, string aliasName, esriFieldType fieldType)
        {
            Name = name;
            AliasName = aliasName;
            eFieldType = fieldType;
            FieldType = fieldType.ToCSharpType();
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
        public GFieldItem(string name, string aliasName, esriFieldType fieldType,
            int length, int precision, int scale, bool isNullable, bool required, bool editable) : this(name, aliasName, fieldType)
        {
            Length = length;
            Precision = precision;
            Scale = scale;
            Nullable = isNullable;
            Required = required;
            Editable = editable;
        }


        /// <summary>
        /// 按照Format指定的格式，格式化输出字段信息
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Format.Replace("N", Name).Replace("A", AliasName).Replace("F", FieldTypeDesciption);
    }
}
