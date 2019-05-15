/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017/5/9 9:43:00
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WLib.Data
{
    /// <summary>
    /// 将表格数据转换成指定类型的对象
    /// </summary>
    public static class ModelConvert
    {
        #region 表格数据转泛型
        /// <summary>
        /// 将DataRow的数据转化为 T 类型的对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="row">DataRow对象，遍历T对象的属性名，能在row中找到同名的列(ColumnName)则赋值该属性</param>
        /// <returns></returns>
        public static T ConvertToObject<T>(this DataRow row) where T : class
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            T model = Activator.CreateInstance<T>();

            var columns = row.Table.Columns;
            for (int i = 0; i < properties.Length; i++)
            {
                var propertyInfo = properties[i];
                for (int j = 0; j < columns.Count; j++)
                {
                    if (propertyInfo.Name == columns[j].ColumnName || propertyInfo.Name == columns[j].Caption)//判断属性的名称和列名是否相同
                        propertyInfo.SetValue(model, ChangeType(row[j], propertyInfo.PropertyType), null);
                }
            }
            return model;
        }
        /// <summary>
        /// 从 reader 对象中逐行读取记录并将记录转化为 T 类型对象的集合
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="reader">实现 IDataReader 接口的对象。</param>
        /// <returns>指定类型的对象集合。</returns>
        public static List<T> ConvertToObject<T>(this IDataReader reader) where T : class
        {
            List<T> list = new List<T>();
            T obj = default(T);
            Type t = typeof(T);
            Assembly ass = t.Assembly;

            Dictionary<string, PropertyInfo> propertys = GetFields<T>(reader);
            PropertyInfo propertyInfo;
            if (reader != null)
            {
                while (reader.Read())
                {
                    obj = ass.CreateInstance(t.FullName) as T;
                    foreach (string key in propertys.Keys)
                    {
                        propertyInfo = propertys[key];
                        propertyInfo.SetValue(obj, ChangeType(reader[key], propertyInfo.PropertyType), null);
                    }
                    list.Add(obj);
                }
            }
            return list;
        }
        /// <summary>
        /// 从 DataTale 对象中逐行读取记录并将记录转化为 T 类型对象的集合
        /// </summary>
        /// <typeparam name="T">目标类型参数</typeparam>
        /// <param name="table">DataTale 对象。</param>
        /// <returns>指定类型的对象集合。</returns>
        public static List<T> ConvertToObject<T>(this DataTable table) where T : class
        {
            return table == null ? new List<T>() : ConvertToObject<T>(table.CreateDataReader());
        }
        /// <summary>
        /// 将数据转换为指定类型的对象，注意当object可以为DBNull.Value或null时，T应为可空类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="value">要转换的数据</param>
        /// <returns></returns>
        public static T ConvertToObject<T>(this object value) where T : class
        {
            return ChangeType(value, typeof(T)) as T;
        }
        #endregion


        #region 泛型转表格数据
        /// <summary>
        /// 将指定类型对象的属性与表格列对应，将对象属性值转为表格数据
        /// </summary>
        /// <typeparam name="T">要转换的类型，注意类型应包含可读属性（get）</typeparam>
        /// <param name="value">要转换的对象，注意对象应包含可读属性（get）</param>
        /// <param name="dataTable">保存对象属性值的表格，表格列应允许空值（column.AllowDBNull = true）</param>
        /// <returns>返回新增了一行的表格数据</returns>
        public static DataTable ConvertToDataTable<T>(this T value, DataTable dataTable)
        {
            Type t = value.GetType();
            PropertyInfo[] properties = t.GetProperties();
            if (properties != null)
            {
                properties = properties.Where(v => v.CanRead).ToArray(); //可读属性
                var columns = dataTable.Columns;
                object[] cellValues = new object[columns.Count];
                for (int i = 0; i < columns.Count; i++)//判断属性的名称和字段的名称是否相同
                {
                    var propertyInfo = properties.FirstOrDefault(v => v.Name == columns[i].Caption || v.Name == columns[i].ColumnName);
                    cellValues[i] = propertyInfo == null ? null : propertyInfo.GetValue(value, null);
                }
                dataTable.Rows.Add(cellValues);
            }
            return dataTable;
        }
        /// <summary>
        /// 将指定类型对象的属性与表格列对应，将对象属性值转为表格数据
        /// </summary>
        /// <typeparam name="T">要转换的类型，注意类型应包含可读属性（get）</typeparam>
        /// <param name="values">要转换的对象，注意对象应包含可读属性（get）</param>
        /// <param name="dataTable">保存对象属性值的表格，表格列应允许空值（column.AllowDBNull = true）</param>
        /// <returns>返回新增了多行的表格数据</returns>
        public static DataTable ConvertToDataTable<T>(this IEnumerable<T> values, DataTable dataTable)
        {
            Type t = typeof(T);
            PropertyInfo[] properties = t.GetProperties();
            if (properties != null)
            {
                properties = properties.Where(v => v.CanRead).ToArray();//可读属性
                var columns = dataTable.Columns;

                var columnToPropertyDict = new Dictionary<DataColumn, PropertyInfo>();
                foreach (DataColumn column in columns)
                {
                    var propertyInfo = properties.FirstOrDefault(v => v.Name == column.Caption || v.Name == column.ColumnName);
                    if (propertyInfo != null)
                        columnToPropertyDict.Add(column, propertyInfo);
                }

                foreach (var value in values)
                {
                    object[] cellValues = new object[columns.Count];
                    for (int i = 0; i < columns.Count; i++)
                    {
                        if (columnToPropertyDict.ContainsKey(columns[i]))
                            cellValues[i] = columnToPropertyDict[columns[i]].GetValue(value, null);
                        else
                            cellValues[i] = null;
                    }
                    dataTable.Rows.Add(cellValues);
                }
            }
            return dataTable;
        }
        #endregion


        #region 泛型转新表格数据
        /// <summary>
        /// 创建新的DataTable，将指定类型的对象转换为表格数据
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="value">要转换的对象</param>
        /// <param name="tableName">表格名称</param>
        /// <returns>返回一行表格数据</returns>
        public static DataTable ConvertToDataTable<T>(this T value, string tableName = null)
        {
            var dataTable = new DataTable(tableName);
            Type t = value.GetType();
            PropertyInfo[] properties = t.GetProperties();
            if (properties != null)
            {
                properties = properties.Where(v => v.CanRead).ToArray();//可读属性

                foreach (var propertyInfo in properties)//添加列
                    dataTable.Columns.Add(new DataColumn(propertyInfo.Name, propertyInfo.DeclaringType));

                object[] values = new object[properties.Length];//添加行
                for (int i = 0; i < properties.Length; i++)
                    values[i] = properties[i].GetValue(value, null);
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        /// <summary>
        /// 创建新的DataTable，将指定类型的对象转换为表格数据
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="values">要转换的对象</param>
        /// <param name="tableName">表格名称</param>
        /// <returns>返回多行表格数据</returns>
        public static DataTable ConvertToDataTable<T>(this IEnumerable<T> values, string tableName = null)
        {
            var dataTable = new DataTable(tableName);

            Type t = typeof(T);
            PropertyInfo[] properties = t.GetProperties();
            if (properties != null)
            {
                properties = properties.Where(v => v.CanRead).ToArray();//可读属性

                foreach (var propertyInfo in properties)//添加列
                    dataTable.Columns.Add(new DataColumn(propertyInfo.Name, propertyInfo.DeclaringType));

                foreach (var value in values)//添加行
                {
                    object[] cellValues = new object[properties.Length];
                    for (int i = 0; i < properties.Length; i++)
                        cellValues[i] = properties[i].GetValue(value, null);
                    dataTable.Rows.Add(cellValues);
                }
            }
            return dataTable;
        }
        #endregion


        /// <summary>
        /// 将数据转化为指定类型的对象
        /// </summary>
        /// <param name="value">要转化的值</param>
        /// <param name="type">目标类型</param>
        /// <returns>转化为目标类型的 Object 对象</returns>
        private static object ChangeType(object value, Type type)
        {
            if (type.FullName == typeof(string).FullName)
            {
                return Convert.ChangeType(Convert.IsDBNull(value) ? null : value, type);
            }
            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                NullableConverter convertor = new NullableConverter(type);
                return Convert.IsDBNull(value) ? null : convertor.ConvertFrom(value);
            }
            return value;
        }
        /// <summary>
        /// 获取T的属性中可写的、且属性名与reader中的字段名相同的属性。
        /// </summary>
        /// <param name="reader">可写域的集合</param>
        /// <returns>返回键值对，键是T类型的属性名，值是属性信息(PropertyInfo)</returns>
        private static Dictionary<string, PropertyInfo> GetFields<T>(IDataReader reader)
        {
            Dictionary<string, PropertyInfo> result = new Dictionary<string, PropertyInfo>();

            Type t = typeof(T);
            PropertyInfo[] properties = t.GetProperties();
            if (properties != null)
            {
                List<string> readerFields = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    readerFields.Add(reader.GetName(i));
                }

                IEnumerable<PropertyInfo> resList = properties.Where(v => v.CanWrite && readerFields.Contains(v.Name));
                foreach (PropertyInfo p in resList)
                {
                    result.Add(p.Name, p);
                }
            }
            return result;
        }
    }
}
