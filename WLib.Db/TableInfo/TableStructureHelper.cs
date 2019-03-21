/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Data;
using System.Linq;
using System.Text;

namespace WLib.Db.TableInfo
{
    /// <summary>
    /// 
    /// </summary>
    public static class TableStructureHelper
    {
        #region 获取字典表信息
        /// <summary>
        /// 获取指定字段对应字典表的指定名称对应的编码
        /// </summary>
        /// <param name="tableFields"></param>
        /// <param name="fieldName">表中的字段，该字段关联字典表</param>
        /// <param name="name">字典表中的值（即名称）</param>
        /// <returns></returns>
        public static string GetDictionaryCode(this TableStructure tableFields, string fieldName, string name)
        {
            return tableFields.Fields.First(v => v.Name.Equals(fieldName)).DictionaryTable.CodeNameDict.FirstOrDefault(v => v.Value.Equals(name)).Key;
        }
        /// <summary>
        /// 获取指定字段对应字典表的全部值（名称）
        /// </summary>
        /// <param name="tableFields">表</param>
        /// <param name="fieldName">表中的字段，该字段关联字典表</param>
        /// <returns></returns>
        public static string[] GetDictionaryValues(this TableStructure tableFields, string fieldName)
        {
            return tableFields.Fields.First(v => v.Name.Equals(fieldName)).DictionaryTable.CodeNameDict.Values.ToArray();
        }
        #endregion


        /// <summary>
        ///  根据条件查询表
        /// </summary>
        /// <param name="dbHelper">数据库帮助类</param>
        /// <param name="tableStructure">表结构</param>
        /// <param name="whereClause">筛选条件，可空</param>
        /// <param name="orderByString">排序语句，此值为null时根据UPDATEDATE排序</param>
        /// <returns></returns>
        public static DataTable QueryTable(DbHelper dbHelper, TableStructure tableStructure, string whereClause, string orderByString)
        {
            if (string.IsNullOrWhiteSpace(whereClause))
                whereClause = "1=1";
            if (string.IsNullOrWhiteSpace(orderByString) && tableStructure.ContainsFieldName("UPDATEDATE"))
                orderByString = "order by CDate(UPDATEDATE) desc";

            string fields = tableStructure.Fields.Select(v => v.Name).Aggregate((a, b) => a + "," + b);
            string sql = $"select {fields} from {tableStructure.TableName} where {whereClause} {orderByString};";

            //执行查询并返回结果
            return dbHelper.GetDataTable(sql);
        }
        /// <summary>
        /// 根据条件查询表
        /// </summary>
        /// <param name="dbHelper">数据库帮助类</param>
        /// <param name="tableStructure">表结构</param>
        /// <param name="fieldClass">条件筛选的字段</param>
        /// <param name="fieldValue">条件筛选的字段值，可空</param>
        /// <param name="orderByString">排序语句，此值为null时根据UPDATEDATE排序</param>
        /// <returns></returns>
        public static DataTable QueryTable(DbHelper dbHelper, TableStructure tableStructure, FieldClass fieldClass, string fieldValue, string orderByString)
        {
            string whereClause = null;
            if (!string.IsNullOrEmpty(fieldValue))
            {
                if (fieldClass.FieldType == typeof(string))
                    whereClause = $"{fieldClass.Name} like '%{fieldValue}%'";
                else if (fieldClass.FieldType == typeof(DateTime))
                    whereClause = $"{fieldClass.Name} = #{fieldValue}#";
                else
                    whereClause = $"{fieldClass.Name} = {fieldValue}";
            }
            return QueryTable(dbHelper, tableStructure, whereClause, orderByString);
        }
        /// <summary>
        /// 根据条件查询表
        /// </summary>
        /// <param name="dbHelper">数据库帮助类</param>
        /// <param name="tableStructure">表结构</param>
        /// <param name="fieldName">条件筛选的字段</param>
        /// <param name="fieldValue">条件筛选的字段值，可空</param>
        /// <param name="orderbyString">排序语句，此值为null时根据UPDATEDATE排序</param>
        /// <returns></returns>
        public static DataTable QueryTable(DbHelper dbHelper, TableStructure tableStructure, string fieldName, string fieldValue, string orderbyString)
        {
            FieldClass fieldClass = tableStructure.Fields.FirstOrDefault(v => v.Name == fieldName);
            return QueryTable(dbHelper, tableStructure, fieldClass, fieldValue, orderbyString);
        }


        /// <summary>
        /// 根据表结构构造插入语句Format（Insert）
        /// </summary>
        /// <param name="tableFields"></param>
        /// <returns></returns>
        public static string CreateInsertSqlFormat(this TableStructure tableFields)
        {
            var fields = tableFields.Fields;
            string strFields = fields.Select(v => v.Name).Aggregate((a, b) => a + "," + b);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < fields.Count; i++)
            {
                if (fields[i].FieldType == typeof(string) || fields[i].FieldType == typeof(DateTime))
                    sb.AppendFormat("'{{{0}}}',", i);
                else
                    sb.AppendFormat("{{{0}}},", i);
            }
            string values = sb.ToString();
            values = values.Substring(0, values.Length - 1);
            string sqlFormat = $"insert into {tableFields.TableName} ({strFields}) values ({values});";
            return sqlFormat;
        }
        /// <summary>
        /// 根据表结构构造更新语句Format（Update）
        /// </summary>
        /// <param name="tableFields"></param>
        /// <returns></returns>
        public static string CreateUpdateSqlFormat(this TableStructure tableFields)
        {
            var idFields = tableFields.Fields.FirstOrDefault(v => v.IsPrimaryKey);
            var fields = tableFields.Fields.Where(v => !v.IsPrimaryKey).ToArray();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i].FieldType == typeof(string) || fields[i].FieldType == typeof(DateTime))
                    sb.AppendFormat("{0}='{{{1}}}',", fields[i].Name, i + 1);
                else
                    sb.AppendFormat("{0}={{{1}}},", fields[i].Name, i + 1);
            }
            string values = sb.ToString();
            values = values.Substring(0, values.Length - 1);
            if (idFields != null)
                return $"update {tableFields.TableName} set {values} where {idFields.Name} =" + "'{0}';";

            return null;
        }


        /// <summary>
        /// 将表格中的38位或36位格式的GUID改成32位
        /// </summary>
        /// <param name="dataTable">包含GUID的DataTable</param>
        /// <param name="guidFieldName">表中的GUID字段</param>
        public static void FormatGuid(DataTable dataTable, string guidFieldName)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                row[guidFieldName] = row[guidFieldName].ToString().Replace("{", "").Replace("}", "").Replace("-", "");
            }
        }
        /// <summary>
        /// 将表格中的38位或36位格式的GUID改成32位
        /// </summary>
        /// <param name="dataTable">包含GUID的DataTable</param>
        /// <param name="guidFieldName1">表中的第一个GUID字段</param>
        /// <param name="guidFieldName2">表中的第一个GUID字段</param>
        public static void FormatGuid(DataTable dataTable, string guidFieldName1, string guidFieldName2)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                row[guidFieldName1] = row[guidFieldName1].ToString().Replace("{", "").Replace("}", "").Replace("-", "");
                row[guidFieldName2] = row[guidFieldName2].ToString().Replace("{", "").Replace("}", "").Replace("-", "");
            }
        }
    }
}
