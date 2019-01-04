/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;

namespace WLib.Db.TableInfo
{
    /// <summary>
    /// 表结构
    /// </summary>
    public class TableStructure
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 表别名
        /// </summary>
        public string TableAliasName { get; set; }
        /// <summary>
        /// 字段集
        /// </summary>
        public List<FieldClass> Fields { get; set; }


        /// <summary>
        /// 表结构
        /// </summary>
        public TableStructure()
        {
            this.Fields = new List<FieldClass>();
        }
        /// <summary>
        /// 表结构
        /// </summary>
        /// <param name="tableName">表名</param>
        public TableStructure(string tableName)
        {
            this.TableName = tableName;
            this.TableAliasName = tableName;
            this.Fields = new List<FieldClass>();
        }
        /// <summary>
        /// 表结构
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="aliasName">表格的别名</param>
        public TableStructure(string tableName, string aliasName)
        {
            this.TableName = tableName;
            this.TableAliasName = aliasName;
            this.Fields = new List<FieldClass>();
        }


        /// <summary>
        /// 向表结构中添加字段信息
        /// </summary>
        /// <param name="fieldClass"></param>
        public void AddField(FieldClass fieldClass)
        {
            this.Fields.Add(fieldClass);
        }
        /// <summary>
        /// 判断改表结构是否包含指定名称/别名的字段
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public bool ContainsFieldName(string fieldName)
        {
            foreach (var field in Fields)
            {
                if (field.Name == fieldName || field.AliasName == fieldName)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 输出表的别名
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return TableAliasName;
        }
    }
}
