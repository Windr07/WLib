/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Data;
using System.Linq;
using ESRI.ArcGIS.Geodatabase;

namespace WLib.ArcGis.Sql
{
    /// <summary>
    /// 提供构造、使用SQL增删改查表格数据的方法
    /// </summary>
    public class SqlQuery
    {
        /// <summary>
        /// 构造Sql Update语句的“set 字段1 = {0}, 字段2 = {1}, ...”格式部分
        /// </summary>
        /// <param name="table">表格</param>
        /// <param name="fieldNames">需要更新的字段</param>
        /// <returns></returns>
        public static string InitSetValueSqlFormat(ITable table, string[] fieldNames)
        {
            string[] setValueSqlFormats = new string[fieldNames.Length];
            for (int i = 0; i < fieldNames.Length; i++)
            {
                var fieldName = fieldNames[i];
                var field = table.Fields.get_Field(table.FindField(fieldName));
                setValueSqlFormats[i] = field.Type == esriFieldType.esriFieldTypeString
                    ? $" {fieldName} = '{{{i}}}' "
                    : $" {fieldName} = {{{i}}} ";
            }
            return setValueSqlFormats.Aggregate((a, b) => a + ',' + b);
        }
        /// <summary>
        /// 构造Sql Where关键字后面的“xx = {0}”格式部分
        /// </summary>
        /// <param name="table">表格</param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string InitWhereSqlFormat(ITable table, string fieldName)
        {
            string whereClauseFormat = fieldName + " = {0} ";
            var idField = table.Fields.get_Field(table.FindField(fieldName));
            if (idField.Type == esriFieldType.esriFieldTypeString)
                whereClauseFormat = fieldName + " = '{0}' ";

            return whereClauseFormat;
        }
        /// <summary>
        /// 依照ID字段，将DataTable的数据对应更新到ITable中
        /// </summary>
        /// <param name="updateTable"></param>
        /// <param name="updateDataTable"></param>
        /// <param name="idFieldName">OID字段或其他符合唯一值规范的字段</param>
        public static void Update(ITable updateTable, DataTable updateDataTable, string idFieldName)
        {
            var updateFieldNames = updateDataTable.Columns.Cast<DataColumn>().Select(v => v.ColumnName).ToArray();

            //构造update语句的“set xxx = xxx”的部分
            string setValueSqlFormat = InitSetValueSqlFormat(updateTable, updateFieldNames);

            //构造update语句的where子句
            string whereClauseFormat = InitWhereSqlFormat(updateTable, idFieldName);

            //拼接完整的update语句，并执行
            var workspace = (updateTable as IDataset)?.Workspace;
            for (int i = 0; i < updateDataTable.Rows.Count; i++)
            {
                object[] values = new object[updateFieldNames.Length];
                var row = updateDataTable.Rows[i];
                for (int j = 0; j < updateFieldNames.Length; j++)
                {
                    values[j] = row[updateFieldNames[j]];
                }
                string strValueSql = string.Format(setValueSqlFormat, values);
                string strWhereSql = string.Format(whereClauseFormat, row[idFieldName]);
                string sql = $"update {updateDataTable.TableName} set {strValueSql} where {strWhereSql};";

                workspace.ExecuteSQL(sql);
            }
        }
    }
}
