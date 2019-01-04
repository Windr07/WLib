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
using System.Linq;
using System.Reflection;

namespace WLib.Data
{
    /// <summary>
    /// 将表格数据转换成指定类型的对象
    /// </summary>
    public class ModelConvert
    {
        /// <summary>
        /// 将DataRow的数据转化为 T 类型的对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="row">DataRow对象，遍历T对象的属性名，能在row中找到同名的列(ColumnName)则赋值该属性</param>
        /// <returns></returns>
        public static T ConvertToObject<T>(DataRow row) where T : class
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            T model = Activator.CreateInstance<T>();

            var columns = row.Table.Columns;
            for (int i = 0; i < properties.Length; i++)
            {
                for (int j = 0; j < columns.Count; j++)
                {
                    //判断属性的名称和字段的名称是否相同
                    if (properties[i].Name == columns[j].ColumnName)
                    {
                        Object value = row[j];
                        properties[i].SetValue(model, ChangeType(value, properties[i].PropertyType), null);
                    }
                }
            }
            return model;
        }
        /// <summary>
        /// 从 reader 对象中逐行读取记录并将记录转化为 T 类型对象的集合
        /// </summary>
        /// <typeparam name="T">目标类型参数</typeparam>
        /// <param name="reader">实现 IDataReader 接口的对象。</param>
        /// <returns>指定类型的对象集合。</returns>
        public static List<T> ConvertToObject<T>(IDataReader reader) where T : class
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
        public static List<T> ConvertToObject<T>(DataTable table) where T : class
        {
            return table == null
                ? new List<T>()
                : ConvertToObject<T>(table.CreateDataReader() as IDataReader);
        }
        /// <summary>
        /// 将数据转换为指定类型的对象，注意当object可以为DBNull.Value或null时，T应为可空类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ConvertToObject<T>(object value) where T : class
        {
            return ChangeType(value, typeof(T)) as T;
        }
        /// <summary>
        /// 将数据转化为指定类型的对象
        /// </summary>
        /// <param name="value">要转化的值</param>
        /// <param name="type">目标类型</param>
        /// <returns>转化为目标类型的 Object 对象</returns>
        public static object ChangeType(object value, Type type)
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
