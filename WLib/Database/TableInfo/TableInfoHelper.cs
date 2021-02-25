/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using WLib.Attributes.Table;
using WLib.Database.DbBase;

namespace WLib.Database.TableInfo
{
    /// <summary>
    /// 
    /// </summary>
    public static class TableInfoHelper
    {
        #region 创建FieldItem或EditItem
        /// <summary>
        /// 将<see cref="DataTable"/>中的列集合转成<see cref="FieldItem"/>
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static IEnumerable<FieldItem> CreateFieldItems(this DataTable dataTable) => CreateFieldItems(dataTable.Columns.Cast<DataColumn>());
        /// <summary>
        /// 将<see cref="DataColumn"/>列集合转成<see cref="FieldItem"/>集合
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static IEnumerable<FieldItem> CreateFieldItems(this IEnumerable<DataColumn> columns)
        {
            foreach (DataColumn column in columns)
            {
                yield return new FieldItem
                {
                    Name = column.ColumnName,
                    AliasName = column.Caption,
                    FieldType = column.DataType,
                    Nullable = column.AllowDBNull,
                    Editable = column.ReadOnly == false,
                };
            }
        }
        /// <summary>
        /// 将<see cref="DataTable"/>中的指定行的数据转成<see cref="EditItem"/>
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="rowIndex">表格中要编辑的数据行的行号，若行号小于0，则代表创建新行，
        /// 即创建<see cref="EditItem.FieldType"/>为null的各个<see cref="EditItem"/></param>
        /// <returns></returns>
        public static IEnumerable<EditItem> CreateEditItems(this DataTable dataTable, int rowIndex, string idFieldName = null)
        {
            bool hasFieldValue = rowIndex >= 0;
            DataRow dataRow = hasFieldValue ? dataTable.Rows[rowIndex] : null;
            foreach (DataColumn column in dataTable.Columns.Cast<DataColumn>())
            {
                var value = hasFieldValue == false || dataRow[column] == null || dataRow[column] == DBNull.Value ? null : dataRow[column].ToString();
                yield return new EditItem
                {
                    Name = column.ColumnName,
                    AliasName = column.Caption,
                    Value = value,
                    NewValue = value,
                    FieldType = column.DataType,
                    Nullable = column.AllowDBNull,
                    Editable = column.ReadOnly == false,
                    IsPrimaryKey = idFieldName == column.ColumnName
                };
            }
        }
        /// <summary>
        /// 将指定对象的公共属性转成<see cref="EditItem"/>
        ///<para>注意：对象的属性的类型应为简单类型（值类型、DateTime、字符串等），不支持属性类型为类、结构、接口、泛型等</para>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="canRead">获取的属性是否要求可读</param>
        /// <param name="canWrite">获取的属性是否要求可写</param>
        /// <returns></returns>
        public static IEnumerable<EditItem> CreateEditItems(this object obj, bool canRead = true, bool canWrite = true)
        {
            var type = obj.GetType();
            var propties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            propties = propties.Where(v => v.CanWrite == canRead && v.CanWrite == canWrite).ToArray();
            foreach (PropertyInfo propInfo in propties)
            {
                yield return new EditItem(propInfo.Name, propInfo.Name, propInfo.GetValue(obj, null)?.ToString(), propInfo.PropertyType)
                {
                    Editable = propInfo.CanWrite,
                };
            }
        }
        /// <summary>
        /// 将指定对象的公共属性转成<see cref="EditItem"/>，其中部分信息来自属性是否拥有以下特性：
        /// <para><see cref="EditItem.Visible"/>来自特性<see cref="WLib.Attributes.Table.HiddenAttribute"/></para>
        /// <para><see cref="EditItem.AliasName"/>来自特性<see cref="WLib.Attributes.Table.AliasNameAttribute"/></para>
        /// <para><see cref="EditItem.Nullable"/>来自特性<see cref="WLib.Attributes.Table.NullableAttribute"/></para>
        /// <para><see cref="EditItem.EditAble"/>来自特性<see cref="WLib.Attributes.Table.ReadOnlyAttribute"/></para>
        /// <para><see cref="EditItem.CandidatesItems"/>和<see cref="EditItem.Check"/>来自特性<see cref="WLib.Attributes.Table.CandidateAttribute"/></para>
        ///<para>注意：对象的属性的类型应为简单类型（值类型、DateTime、字符串等），不支持属性类型为类、结构、接口、泛型等</para>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="canRead">获取的属性是否要求可读</param>
        /// <param name="canWrite">获取的属性是否要求可写</param>
        /// <returns></returns>
        public static IEnumerable<EditItem> CreateEditItemWithAttributes(this object obj, bool canRead = true, bool canWrite = true)
        {
            var type = obj.GetType();
            var propties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            propties = propties.Where(v => v.CanWrite == canRead && v.CanWrite == canWrite).ToArray();
            foreach (PropertyInfo propInfo in propties)
            {
                var value = propInfo.GetValue(obj, null)?.ToString();
                var editItem = new EditItem(propInfo.Name, propInfo.GetAliasName(), value, propInfo.PropertyType)
                {
                    Nullable = propInfo.IsNullable(),
                    Editable = propInfo.IsReadOnly() == false,
                    Visible = propInfo.IsHidden() == false,
                    IsPrimaryKey = propInfo.IsKey()
                };
                var candidates = propInfo.GetCandidates(out var mustbeInRange);
                if (candidates != null)
                    editItem.CandidatesItems = candidates;

                if (mustbeInRange && candidates != null && candidates.Length > 0)
                    editItem.Check += (fieldValue, fieldType) => candidates.Contains(fieldValue) ? null : $"字段值“{value}”不在可选条件范围内";

                yield return editItem;
            }
        }
        /// <summary>
        /// 复制<see cref="FieldItem"/>对象，并且转成<see cref="EditItem"/>对象
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static EditItem ToEditItem(this FieldItem field) => EditItem.FromFieldItem(field);
        /// <summary>
        /// 复制<see cref="FieldItem"/>对象，并且转成<see cref="EditItem"/>对象
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static IEnumerable<EditItem> ToEditItems(this IEnumerable<FieldItem> fields) => EditItem.FromFieldItems(fields);
        #endregion


        /// <summary>
        /// 从数据库中获取字典表
        /// </summary>
        /// <param name="dbHelper"></param>
        /// <param name="tableName"></param>
        /// <param name="codeField">编码字段名</param>
        /// <param name="nameField">名称字段名</param>
        /// <param name="whereCluase"></param>
        /// <returns></returns>
        public static DictionaryTable QueryDictionaryTable(this DbHelper dbHelper, string tableName,
            string codeField = "编码", string nameField = "名称", string whereCluase = null)
        {
            var strWhereClause = string.IsNullOrWhiteSpace(whereCluase) ? null : " where " + whereCluase;
            var sql = $"select {codeField},{nameField} from {tableName}{strWhereClause}";
            var dataTable = dbHelper.GetDataTable(sql);

            var dictTable = new DictionaryTable(tableName);
            foreach (DataRow row in dataTable.Rows)
                dictTable.Add(row[0].ToString(), row[1].ToString());

            return dictTable;
        }


        #region 根据条件查询表
        /// <summary>
        ///  根据条件查询表
        /// </summary>
        /// <param name="dbHelper">数据库帮助类</param>
        /// <param name="table">表结构</param>
        /// <param name="whereClause">筛选条件，可空</param>
        /// <param name="strOrderBy">排序语句</param>
        /// <returns></returns>
        public static DataTable QueryTable(this TableItem table, DbHelper dbHelper, string whereClause, string strOrderBy = null)
        {
            //if (string.IsNullOrWhiteSpace(strOrderBy) && table.ContainsField("UPDATEDATE")) strOrderBy = "order by CDate(UPDATEDATE) desc";
            if (string.IsNullOrWhiteSpace(whereClause))
                whereClause = "1=1";

            var strFields = string.Join(",", table.Fields.Select(v => v.Name));
            var sql = $"select {strFields} from {table.Name} where {whereClause} {strOrderBy};";
            return dbHelper.GetDataTable(sql);
        }
        /// <summary>
        /// 根据条件查询表
        /// </summary>
        /// <param name="dbHelper">数据库帮助类</param>
        /// <param name="table">表结构</param>
        /// <param name="fieldItem">条件筛选的字段</param>
        /// <param name="fieldValue">条件筛选的字段值，可空</param>
        /// <param name="strOrderBy">排序语句，此值为null时根据UPDATEDATE排序</param>
        /// <returns></returns>
        public static DataTable QueryTable(this TableItem table, DbHelper dbHelper, FieldItem fieldItem, string fieldValue, string strOrderBy = null)
        {
            string whereClause = null;
            if (!string.IsNullOrEmpty(fieldValue))
            {
                if (fieldItem.FieldType == typeof(string))
                    whereClause = $"{fieldItem.Name} like '%{fieldValue}%'";
                else if (fieldItem.FieldType == typeof(DateTime))
                    whereClause = $"{fieldItem.Name} = #{fieldValue}#";
                else
                    whereClause = $"{fieldItem.Name} = {fieldValue}";
            }
            return QueryTable(table, dbHelper, whereClause, strOrderBy);
        }
        /// <summary>
        /// 根据条件查询表
        /// </summary>
        /// <param name="dbHelper">数据库帮助类</param>
        /// <param name="table">表结构</param>
        /// <param name="fieldName">条件筛选的字段</param>
        /// <param name="fieldValue">条件筛选的字段值，可空</param>
        /// <param name="strOrderBy">排序语句，此值为null时根据UPDATEDATE排序</param>
        /// <returns></returns>
        public static DataTable QueryTable(this TableItem table, DbHelper dbHelper, string fieldName, string fieldValue, string strOrderBy)
        {
            FieldItem fieldItem = table.Fields.FirstOrDefault(v => v.Name == fieldName);
            return QueryTable(table, dbHelper, fieldItem, fieldValue, strOrderBy);
        }
        #endregion


        /// <summary>
        /// 根据字段集和表名称，构造插入语句（Insert）
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="tableName"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static string CreateInsertSql(this IEnumerable<EditItem> fields, string tableName, EDbProviderType dbType = EDbProviderType.SqlServer)
        {
            var strFields = fields.Where(v => v.Ignore == false).Select(v => v.Name).Aggregate((a, b) => a + "," + b);
            var sb = new StringBuilder();
            foreach (var f in fields)
            {
                var newValue = f.NewValue;
                if (f.UseDict)
                {
                    if (f.UseDictKey && f.DictionaryTable.ContainsValue(newValue))
                        newValue = f.DictionaryTable.First(v => v.Value == newValue).Key;
                }
                if (f.Nullable && newValue == null)
                    sb.AppendFormat("null,");
                else if (f.FieldType == typeof(string))
                    sb.AppendFormat("'{0}',", newValue);
                else if (f.FieldType == typeof(DateTime))
                {
                    if (dbType == EDbProviderType.SqlServer) sb.AppendFormat("'{0}',", newValue);
                    else if (dbType == EDbProviderType.Oracle) sb.AppendFormat("to_date('{0}', 'YYYY-MM-DD HH24:MI:SS'),", newValue);
                    else throw new NotImplementedException("未实现其他数据库类型的时间插入语句！");
                }
                else
                    sb.AppendFormat("{0},", newValue);
            }
            var values = sb.ToString(0, sb.Length - 1);
            return $"insert into {tableName} ({strFields}) values ({values})";
        }
        /// <summary>
        /// 根据字段集和表名称，构造更新语句（Update）
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="tableName"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static string CreateUpdateSql(this IEnumerable<EditItem> fields, string tableName, EDbProviderType dbType = EDbProviderType.SqlServer)
        {
            var idField = fields.FirstOrDefault(v => v.IsPrimaryKey);
            if (idField == null) throw new ArgumentException($"字段集中不包含ID字段，请先设置ID字段！", nameof(fields));
            var tmpFields = fields.Where(v => !v.IsPrimaryKey && v.Changed && v.Ignore == false).ToArray();
            if (tmpFields.Length == 0) throw new Exception($"没有要更新的字段值！");

            var sb = new StringBuilder();
            foreach (var f in tmpFields)
            {
                var newValue = f.NewValue;
                if (f.UseDict)
                {
                    if (f.UseDictKey && f.DictionaryTable.ContainsValue(newValue))
                        newValue = f.DictionaryTable.First(v => v.Value == newValue).Key;
                }
                if (f.Nullable && newValue == null)
                    sb.AppendFormat("{0}=null,", f.Name);
                else if (f.FieldType == typeof(string))
                    sb.AppendFormat("{0}='{1}',", f.Name, newValue);
                else if (f.FieldType == typeof(DateTime))
                {
                    if (dbType == EDbProviderType.SqlServer) sb.AppendFormat("{0}='{1}',", f.Name, newValue);
                    else if (dbType == EDbProviderType.Oracle) sb.AppendFormat("{0}=to_date('{1}', 'YYYY-MM-DD HH24:MI:SS'),", f.Name, newValue);
                    else throw new NotImplementedException("未实现其他数据库类型的时间修改语句！");
                }
                else
                    sb.AppendFormat("{0}={1},", f.Name, newValue);
            }
            var values = sb.ToString();
            values = values.Substring(0, values.Length - 1);
            return $"update {tableName} set {values} where {idField.Name} = '{idField.Value}'";
        }


        /// <summary>
        /// 将表格中的38位或36位格式的GUID改成32位
        /// </summary>
        /// <param name="dataTable">包含GUID的DataTable</param>
        /// <param name="guidFields">表中的GUID字段</param>
        public static void FormatGuid(DataTable dataTable, params string[] guidFields)
        {
            foreach (DataRow row in dataTable.Rows)
                foreach (var guidField in guidFields)
                    row[guidField] = row[guidField].ToString().Replace("{", "").Replace("}", "").Replace("-", "");
        }
    }
}
