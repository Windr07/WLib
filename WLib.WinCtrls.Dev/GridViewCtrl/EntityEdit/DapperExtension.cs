using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WLib.Reflection;

namespace WLib.Database
{
    /*
	 * 自定义对Dapper组件的扩展，添加减少SQL语句对数据库进行最基本的增删改查的方法、where条件语句构造方法
	 */

    public static partial class DapperExtension
    {
        #region 基本ORM增删改查
        /// <summary>
        /// 根据类名查询同名的表
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public static IEnumerable<TValue> QueryValue<TValue>(this IDbConnection dbConnection, string tableName, string fieldName = null, string whereClause = null)
        {
            whereClause = string.IsNullOrWhiteSpace(whereClause) ? string.Empty : "where " + whereClause;
            if (string.IsNullOrWhiteSpace(fieldName))
                fieldName = "*";
            var dynamicObjs = dbConnection.Query($"select {fieldName} from {tableName} {whereClause}");
            var enumDict = (IEnumerable<IDictionary<string, object>>)dynamicObjs;
            foreach (var dict in enumDict)
                yield return (TValue)dict.First().Value;
        }
        /// <summary>
        /// 根据实体类型查询对应的表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="type">实体类型</param>
        /// <param name="tableName">要映射的表的表名</param>
        /// <param name="whereClause">数据筛选条件</param>
        /// <returns></returns>
        public static IEnumerable<object> SimpleQuery(this IDbConnection dbConnection, Type type, string tableName = null, string whereClause = null)
        {
            tableName = string.IsNullOrWhiteSpace(tableName) ? type.Name : tableName;
            whereClause = string.IsNullOrWhiteSpace(whereClause) ? string.Empty : "where " + whereClause;
            return dbConnection.Query(type, $"select * from {tableName} {whereClause}");
        }
        /// <summary>
        /// 根据实体类型查询对应的表，转成指定类型实体数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="whereClause">数据筛选条件</param>
        /// <returns></returns>
        public static IEnumerable<T> SimpleQuery<T>(this IDbConnection dbConnection, string whereClause = null) where T : class
        {
            whereClause = string.IsNullOrWhiteSpace(whereClause) ? string.Empty : "where " + whereClause;
            return dbConnection.Query<T>($"select * from {typeof(T).Name} {whereClause}");
        }
        /// <summary>
        /// 根据实体类型向对应的表插入实体数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="value">要插入的对象或对象数组(IEnumerable)</param>
        /// <returns></returns>
        public static int SimpleInsert<T>(this IDbConnection dbConnection, T value)
        {
            var t = value.GetType();
            var @params = string.Join(",", t.GetProperties().Where(v => v.PropertyType.IsExtSimpleType()).Select(v => "@" + v.Name));
            return dbConnection.Execute($"Insert into {t.Name} values ({@params})", value);
        }
        /// <summary>
        /// 根据类名更新同名的表的指定数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="value">要更新的对象或对象数组(IEnumerable)</param>
        /// <returns></returns>
        public static int SimpleUpdate<T>(this IDbConnection dbConnection, T value, string idField, string tableName = null)
        {
            var t = value.GetType();
            var properties = t.GetProperties();
            var @params = properties.Select(v => $"{v.Name} = @{v.Name}").Aggregate((a, b) => a + "," + b);

            tableName = tableName ?? t.Name;
            var whereClause = $"{idField} = @{idField}";
            return dbConnection.Execute($"update {tableName} set {@params} where {whereClause}", value);
        }
        /// <summary>
        /// 根据类名删除同名的表的指定数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="value">要删除的对象或对象数组(IEnumerable)</param>
        /// <param name="idField">要删除的对象的ID</param>
        /// <returns></returns>
        public static int SimpleDelete<T>(this IDbConnection dbConnection, T value, string idField)
        {
            var t = value.GetType();
            var whereClause = $"{idField} = @{idField}";
            return dbConnection.Execute($"delete from {t.Name} where {whereClause}", value);
        }
        #endregion


        #region 条件查询语句构建
        public static string And(this string str, string fieldName) => $"{str} and {fieldName}";
        public static string Or(this string str, string fieldName) => $"{str} or {fieldName}";
        public static string Like(this string str, string value) => $"{str} like '{value}'";
        public static string In(this string str, params object[] values)
        {
            if (values == null || values.Length == 0)
                throw new System.ArgumentException($"参数{values}不能为null或数组个数为0！");

            bool isStr = values[0] is string;
            var inValues = string.Empty;
            if (isStr)
                inValues = values.Select(v => $"'{v}'").Aggregate((a, b) => a + "," + b);
            else
                inValues = values.Select(v => v.ToString()).Aggregate((a, b) => a + "," + b);
            return $"{str} In ({inValues})";
        }
        public static string Equal(this string str, object value) => value is string ? $"{str} = '{value}'" : $"{str} = {value}";
        public static string NotEqual(this string str, object value) => value is string ? $"{str} <> '{value}'" : $"{str} <> {value}";
        public static string GreaterThan(this string str, object value) => value is string ? $"{str} > '{value}'" : $"{str} > {value}";
        public static string GreaterThanEqual(this string str, object value) => value is string ? $"{str} >= '{value}'" : $"{str} >= {value}";
        public static string LessThan(this string str, object value) => value is string ? $"{str} < '{value}'" : $"{str} < {value}";
        public static string LessThanEqual(this string str, object value) => value is string ? $"{str} <= '{value}'" : $"{str} <= {value}";
        #endregion
    }
}
