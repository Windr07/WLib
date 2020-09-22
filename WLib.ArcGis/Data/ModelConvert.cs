/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017/6/1 10:51:34
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.GeoDatabase.FeatClass;
using WLib.ArcGis.GeoDatabase.Table;

namespace WLib.ArcGis.Data
{
    /// <summary>
    /// 表格或图层数据转换成指定类型的对象
    /// （类型包含的字段必须定义为包含get、set访问器的属性）
    /// </summary>
    public static class ModelConvert
    {
        /// <summary>
        /// 将IRow的数据转化为 T 类型的对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T ConvertToObject<T>(this IRow row) where T : class
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            T model = Activator.CreateInstance<T>();

            var fields = row.Fields;
            foreach (var property in properties)
            {
                for (int j = 0; j < fields.FieldCount; j++)
                {
                    if (property.Name == fields.get_Field(j).Name)//判断属性的名称和字段的名称是否相同
                    {
                        object value = row.get_Value(j);
                        property.SetValue(model, ChangeType(value, property.PropertyType), null); //将字段的值赋值给User中的属性
                    }
                }
            }
            return model;
        }
        /// <summary>
        /// 将IRow的数据转化为 T 类型的对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="feature"></param>
        /// <returns></returns>
        public static T ConvertToObject<T>(this IFeature feature) where T : class
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            T model = Activator.CreateInstance<T>();

            var fields = feature.Fields;
            foreach (var property in properties)
            {
                for (int j = 0; j < fields.FieldCount; j++)
                {
                    if (property.Name == fields.get_Field(j).Name)//判断属性的名称和字段的名称是否相同
                    {
                        object value = feature.get_Value(j);
                        property.SetValue(model, ChangeType(value, property.PropertyType), null);//将字段的值赋值给User中的属性
                    }
                }
            }
            return model;
        }
        /// <summary>
        /// 从 ITable 对象中逐行读取记录并将记录转化为 T 类型的集合
        /// </summary>
        /// <typeparam name="T">目标类型参数</typeparam>
        /// <param name="table">从中获取数据的表格</param>
        /// <param name="whereClause">筛选条件</param>
        /// <returns>指定类型的对象集合</returns>
        public static List<T> ConvertToObject<T>(this ITable table, string whereClause = null) where T : class
        {
            List<T> list = new List<T>();
            Type t = typeof(T);
            Assembly assembly = t.Assembly;

            Dictionary<string, PropertyInfo> propertys = GetFields<T>(table);
            if (table == null) return list;

            var rows = table.QueryRows(whereClause);
            foreach (IRow row in rows)
            {
                var obj = assembly.CreateInstance(t.FullName) as T;
                foreach (string key in propertys.Keys)
                {
                    var propertyInfo = propertys[key];
                    propertyInfo.SetValue(obj, ChangeType(row.get_Value(row.Fields.FindField(key)), propertys[key].PropertyType), null);
                }
                list.Add(obj);
            }
            return list;
        }
        /// <summary>
        /// 从 IFeatureClass 对象中逐行读取记录并将记录转化为 T 类型的集合
        /// </summary>
        /// <typeparam name="T">目标类型参数</typeparam>
        /// <param name="featureClass">从中获取数据的图层</param>
        /// <param name="whereClause">筛选条件</param>
        /// <returns>指定类型的对象集合</returns>
        public static List<T> ConvertToObject<T>(this IFeatureClass featureClass, string whereClause = null) where T : class
        {
            List<T> list = new List<T>();
            Type t = typeof(T);
            Assembly assembly = t.Assembly;

            Dictionary<string, PropertyInfo> propertys = GetFields<T>(featureClass as ITable);
            if (featureClass == null) return list;

            var features = featureClass.QueryFeatures(whereClause).ToList();
            foreach (IFeature feature in features)
            {
                var obj = assembly.CreateInstance(t.FullName) as T;
                foreach (string key in propertys.Keys)
                {
                    var peropertyInfo = propertys[key];
                    peropertyInfo.SetValue(obj, ChangeType(feature.get_Value(feature.Fields.FindField(key)), peropertyInfo.PropertyType), null);
                }
                list.Add(obj);
            }
            return list;
        }
        /// <summary>
        /// 从 ITable 对象中获得第一条记录并将记录转化为 T 类型的对象
        /// </summary>
        /// <typeparam name="T">目标类型参数</typeparam>
        /// <param name="table">从中获取数据的表格</param>
        /// <param name="whereClause">筛选条件</param>
        /// <returns></returns>
        public static T ConvertFirstRecordToObject<T>(this ITable table, string whereClause = null) where T : class
        {
            var row = table.QueryFirstRow(whereClause);
            return ConvertToObject<T>(row);
        }
        /// <summary>
        /// 从 IFeatureClass 对象中获得第一条记录并将记录转化为 T 类型的对象
        /// </summary>
        /// <typeparam name="T">目标类型参数</typeparam>
        /// <param name="featureClass">从中获取数据的图层</param>
        /// <param name="whereClause">筛选条件</param>
        /// <returns></returns>
        public static T ConvertFirstRecordToObject<T>(this IFeatureClass featureClass, string whereClause = null) where T : class
        {
            var feature = featureClass.QueryFirstFeature(whereClause);
            return ConvertToObject<T>(feature);
        }

        /// <summary>
        /// 将数据转化为 type 类型
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
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                NullableConverter convertor = new NullableConverter(type);
                return Convert.IsDBNull(value) ? null : convertor.ConvertFrom(value);
            }
            return value;
        }
        /// <summary>
        /// 获取ITable在 T 类中包含同名可写属性的集合
        /// </summary>
        /// <param name="table"></param>
        /// <returns>以属性名为键，PropertyInfo 为值得字典对象</returns>
        private static Dictionary<string, PropertyInfo> GetFields<T>(ITable table)
        {
            Dictionary<string, PropertyInfo> result = new Dictionary<string, PropertyInfo>();
            IFields fields = table.Fields;
            int columnCount = fields.FieldCount;
            Type t = typeof(T);

            List<string> readerFields = new List<string>();
            for (int i = 0; i < columnCount; i++)
            {
                readerFields.Add(fields.get_Field(i).Name);
            }
            var properties = t.GetProperties().Where(v => v.CanWrite && readerFields.Contains(v.Name));
            foreach (PropertyInfo propertyInfo in properties)
            {
                result.Add(propertyInfo.Name, propertyInfo);
            }
            return result;
        }
    }
}
