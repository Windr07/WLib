/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace WLib.Database.TableInfo
{
    /// <summary>
    /// 表示一个表或表结构
    /// </summary>
    [Serializable]
    public class TableItem
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 表别名
        /// </summary>
        public string AliasName { get; set; }
        /// <summary>
        /// 字段集
        /// </summary>
        public List<FieldItem> Fields { get; set; }


        /// <summary>
        /// 表示一个表或表结构
        /// </summary>
        public TableItem() => this.Fields = new List<FieldItem>();
        /// <summary>
        /// 表示一个表或表结构
        /// </summary>
        /// <param name="name">表名</param>
        public TableItem(string name) : this() => this.Name = name;
        /// <summary>
        /// 表示一个表或表结构
        /// </summary>
        /// <param name="name">表名</param>
        /// <param name="aliasName">表格的别名</param>
        public TableItem(string name, string aliasName) : this(name) => this.AliasName = aliasName;
        /// <summary>
        /// 表示一个表或表结构
        /// </summary>
        /// <param name="name">表名</param>
        /// <param name="aliasName">表格的别名</param>
        /// <param name="fields">字段集</param>
        public TableItem(string name, string aliasName, IEnumerable<FieldItem> fields) : this(name, aliasName) => this.Fields.AddRange(fields);


        /// <summary>
        /// 向表结构中添加字段信息
        /// </summary>
        /// <param name="field"></param>
        public void AddField(FieldItem field) => this.Fields.Add(field);
        /// <summary>
        /// 向表结构中添加字段信息
        /// </summary>
        /// <param name="fieldClass"></param>
        public void AddFields(IEnumerable<FieldItem> fields) => this.Fields.AddRange(fields);
        /// <summary>
        /// 判断改表结构是否包含指定名称/别名的字段
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public bool ContainsField(string fieldName) => Fields.Any(f => f.Name == fieldName || f.AliasName == fieldName);
        /// <summary>
        /// 根据字段名获取字段
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public FieldItem GetField(string fieldName) => Fields.First(v => v.Name == fieldName);
        /// <summary>
        /// 根据字段别名获取字段
        /// </summary>
        /// <param name="fieldAliasName"></param>
        /// <returns></returns>
        public FieldItem GetField2(string fieldAliasName) => Fields.First(v => v.AliasName == fieldAliasName);
        /// <summary>
        /// 获取主键字段
        /// </summary>
        /// <returns></returns>
        public FieldItem GetKeyField() => Fields.First(v => v.IsPrimaryKey);
        /// <summary>
        /// 获取指定字段对应字典表的指定名称对应的编码
        /// </summary>
        /// <param name="fieldName">表中的字段，该字段关联字典表</param>
        /// <param name="name">字典表中的值（即名称）</param>
        /// <returns></returns>
        public string GetDictionaryCode(string fieldName, string name)
        {
            return Fields.First(v => v.Name.Equals(fieldName)).DictionaryTable.FirstOrDefault(v => v.Value.Equals(name)).Key;
        }
        /// <summary>
        /// 获取指定字段对应字典表的全部值（名称）
        /// </summary>
        /// <param name="fieldName">表中的字段，该字段关联字典表</param>
        /// <returns></returns>
        public string[] GetDictionaryValues(string fieldName)
        {
            return Fields.First(v => v.Name.Equals(fieldName)).DictionaryTable.Values.ToArray();
        }

        /// <summary>
        /// 输出表的别名
        /// </summary>
        /// <returns></returns>
        public override string ToString() => AliasName;
    }
}
