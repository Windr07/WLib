﻿/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WLib.Attributes.Table
{
    /// <summary>
    /// 根据表实体类属性特性，获取相关表字段信息的帮助类
    /// </summary>
    public static class TableAttributeHelper
    {
        /// <summary>
        /// 判断表格字段是否为主键
        /// </summary>
        /// <param name="type">表格对应的实体对象</param>
        /// <param name="fieldName">对象属性名</param>
        /// <returns></returns>
        public static bool IsKey(this Type type, string fieldName) => type.GetAttribute<KeyAttribute>(fieldName) != null;
        /// <summary>
        /// 判断表格字段是否应设置为只读
        /// </summary>
        /// <param name="type">表格对应的实体对象</param>
        /// <param name="fieldName">对象属性名</param>
        /// <returns></returns>
        public static bool IsReadOnly(this Type type, string fieldName) => type.GetAttribute<ReadOnlyAttribute>(fieldName) != null;
        /// <summary>
        /// 判断表格字段是否应设置为隐藏
        /// </summary>
        /// <param name="type">表格对应的实体对象</param>
        /// <param name="fieldName">对象属性名</param>
        /// <returns></returns>
        public static bool IsHidden(this Type type, string fieldName) => type.GetAttribute<HiddenAttribute>(fieldName) != null;
        /// <summary>
        /// 判断表格字段是否允许值为空
        /// </summary>
        /// <param name="type">表格对应的实体对象</param>
        /// <param name="fieldName">对象属性名</param>
        /// <returns></returns>
        public static bool IsNullable(this Type type, string fieldName) => type.GetAttribute<NullableAttribute>(fieldName) != null;
        /// <summary>
        /// 获取表格字段的别名
        /// </summary>
        /// <param name="type">表格对应的实体对象</param>
        /// <param name="fieldName">对象属性名</param>
        /// <returns></returns>
        public static string GetAliasName(this Type type, string fieldName) => type.GetAttribute<AliasNameAttribute>(fieldName)?.AliasName;
        /// <summary>
        /// 获取表格字段值的候选项列表
        /// </summary>
        /// <param name="type">表格对应的实体对象</param>
        /// <param name="fieldName">对象属性名</param>
        /// <returns></returns>
        public static string[] GetCandidates(this Type type, string fieldName) => type.GetAttribute<CandidateAttribute>(fieldName)?.Items;
        /// <summary>
        /// 获取表格字段值的候选项列表
        /// </summary>
        /// <param name="type">表格对应的实体对象</param>
        /// <param name="fieldName">对象属性名</param>
        /// <param name="mustbeInRange">字段是否必须符合候选项，不能设置其他值</param>
        /// <returns></returns>
        public static string[] GetCandidates(this Type type, string fieldName, out bool mustbeInRange)
        {
            var attr = type.GetAttribute<CandidateAttribute>(fieldName);
            mustbeInRange = attr == null ? false : attr.MustBeInRange;
            return attr?.Items;
        }


        /// <summary>
        /// 判断字段是否为主键
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static bool IsKey(this PropertyInfo propertyInfo) => propertyInfo.GetAttribute<KeyAttribute>() != null;
        /// <summary>
        /// 判断字段是否应设置为只读
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static bool IsReadOnly(this PropertyInfo propertyInfo) => propertyInfo.GetAttribute<ReadOnlyAttribute>() != null;
        /// <summary>
        /// 判断字段是否应设置为隐藏
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static bool IsHidden(this PropertyInfo propertyInfo) => propertyInfo.GetAttribute<HiddenAttribute>() != null;
        /// <summary>
        /// 判断字段是否允许值为空
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static bool IsNullable(this PropertyInfo propertyInfo) => propertyInfo.GetAttribute<NullableAttribute>() != null;
        /// <summary>
        /// 获取字段的别名（若别名为null则返回字段名）
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static string GetAliasName(this PropertyInfo propertyInfo) => propertyInfo.GetAttribute<AliasNameAttribute>()?.AliasName ?? propertyInfo.Name;
        /// <summary>
        /// 获取字段值的候选项列表
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static string[] GetCandidates(this PropertyInfo propertyInfo) => propertyInfo.GetAttribute<CandidateAttribute>()?.Items;
        /// <summary>
        /// 获取字段值的候选项列表
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="mustbeInRange">字段是否必须符合候选项，不能设置其他值</param>
        /// <returns></returns>
        public static string[] GetCandidates(this PropertyInfo propertyInfo, out bool mustbeInRange)
        {
            var attr = propertyInfo.GetAttribute<CandidateAttribute>();
            mustbeInRange = attr == null ? false : attr.MustBeInRange;
            return attr?.Items;
        }


        /// <summary>
        /// 获取主键字段
        /// </summary>
        /// <param name="type">表格对应的实体对象</param>
        /// <returns></returns>
        public static string GetKeyProperty(this Type type) => type.GetPropertyInfos<KeyAttribute>().Select(v => v.Name).FirstOrDefault();
        /// <summary>
        /// 获取主键字段
        /// </summary>
        /// <param name="type">表格对应的实体对象</param>
        /// <returns></returns>
        public static IEnumerable<string> GetKeyProperties(this Type type) => type.GetPropertyInfos<KeyAttribute>().Select(v => v.Name);
        /// <summary>
        /// 获取只读字段
        /// </summary>
        /// <param name="type">表格对应的实体对象</param>
        /// <returns></returns>
        public static IEnumerable<string> GetReadOnlyProperties(this Type type) => type.GetPropertyInfos<ReadOnlyAttribute>().Select(v => v.Name);
        /// <summary>
        /// 获取隐藏字段
        /// </summary>
        /// <param name="type">表格对应的实体对象</param>
        /// <returns></returns>
        public static IEnumerable<string> GetHiddenProperties(this Type type) => type.GetPropertyInfos<HiddenAttribute>().Select(v => v.Name);
        /// <summary>
        /// 获取可空字段
        /// </summary>
        /// <param name="type">表格对应的实体对象</param>
        /// <returns></returns>
        public static IEnumerable<string> GetNullableProperties(this Type type) => type.GetPropertyInfos<NullableAttribute>().Select(v => v.Name);
        /// <summary>
        /// 获取拥有别名的字段及其别名
        /// <para>Key:字段名， Value:字段别名</para>
        /// </summary>
        /// <param name="type">表格对应的实体对象</param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, string>> GetNameAndAliasName(this Type type)
        {
            foreach (var property in type.GetProperties())
            {
                var attribute = property.GetAttribute<AliasNameAttribute>();
                if (attribute != null)
                    yield return new KeyValuePair<string, string>(property.Name, attribute.AliasName);
            }
        }
        /// <summary>
        /// 获取拥有外键的字段的信息
        /// </summary>
        /// <param name="type">表格对应的实体对象</param>
        /// <returns></returns>
        public static IEnumerable<ForeignKeyInfo> GetForeignKeyInfos(this Type type)
        {
            foreach (var property in type.GetProperties())
            {
                var attribute = property.GetAttribute<ForeignKeyAttribute>();
                if (attribute != null)
                {
                    var foreignField = attribute.ForeignField == null ? property.Name : attribute.ForeignField;
                    yield return new ForeignKeyInfo(property.Name, attribute.ForeignTable, foreignField);
                }
            }
        }


        /// <summary>
        /// 获取对象包含指定特性的属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetPropertyInfos<T>(this Type type) where T : Attribute
        {
            var properties = type.GetProperties();
            return properties.Where(p => p.GetAttribute<T>() != null);
        }
        /// <summary>
        /// 获取对象指定属性的指定特性，不含该特性则返回null
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="type">对象</param>
        /// <param name="fieldName">对象的属性</param>
        /// <returns></returns>
        public static T GetAttribute<T>(this Type type, string fieldName) where T : Attribute
        {
            return type.GetProperty(fieldName).GetAttribute<T>();
        }
        /// <summary>
        /// 从属性信息中查找指定特性，返回找到的第一个特性，不含该特性则返回null
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(this PropertyInfo propertyInfo) where T : Attribute
        {
            var attributes = propertyInfo.GetCustomAttributes(typeof(T), false);
            return attributes.Length == 0 ? null : (T)attributes[0];
        }
        ///// <summary>
        ///// 获取字段的外键信息
        ///// </summary>
        ///// <param name="type">表格对应的实体对象</param>
        ///// <param name="fieldName">表格中拥有外键的字段</param>
        ///// <returns>返回一个元组，包含外键所关联的表名、外键所关联的字段名</returns>
        //public static (string ForeignTable, string ForeignKey) GetForeignKey(this object value, string fieldName)
        //{
        //    var attribute = value.GetAttribute<ForeignKeyAttribute>(fieldName);
        //    if (attribute == null)
        //        return (null, null);
        //    return (attribute.ForeignTable, attribute.ForeignField == null ? fieldName : attribute.ForeignField);
        //}
    }
}
