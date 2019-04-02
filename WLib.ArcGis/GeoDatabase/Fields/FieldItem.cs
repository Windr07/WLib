/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017/1/6 16:20:43
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Geodatabase;

namespace WLib.ArcGis.GeoDatabase.Fields
{
    /// <summary>
    /// 表示字段
    /// </summary>
    public class FieldItem
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 字段别名
        /// </summary>
        public string AliasName { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public esriFieldType FieldType { get; set; }
        /// <summary>
        /// 确定ToString方法输出的内容
        /// （N-字段名，A-字段别名，F-字段类型，例如Format="N,A,(F)"，则ToString()结果为"字段名,字段别名,(字段类型)"）
        /// </summary>
        public string Format { get; set; } = "A";
        /// <summary>
        /// 获得字段类型的文字描述
        /// </summary>
        /// <returns></returns>
        public string FieldTypeDesciption => FieldType.GetFieldTypeDesciption();
        /// <summary>
        /// 获得字段类型的中文文字描述
        /// </summary>
        /// <returns></returns>
        public string FieldTypeDesciptionCn => FieldType.GetFieldTypeDesciptionCn();


        /// <summary>
        /// 表示字段
        /// </summary>
        public FieldItem() { }
        /// <summary>
        /// 表示字段
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="aliasName">字段别名</param>
        /// <param name="fieldType">字段类型</param>
        public FieldItem(string name, string aliasName, esriFieldType fieldType)
        {
            Name = name;
            AliasName = aliasName;
            FieldType = fieldType;
        }


        /// <summary>
        /// 按照Format指定的格式，格式化输出字段信息
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Format.Replace("N", Name).Replace("A", AliasName).Replace("F", FieldTypeDesciption);
        }
    }
}
