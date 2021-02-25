using System;
using System.ComponentModel;

namespace WLib.Attributes.Description
{
    /// <summary>
    /// 提供获取类型及其字段或属性上的<see cref="System.ComponentModel.DescriptionAttribute.Description"/>的方法
    /// </summary>
    public static class ClassDescriptionHelper
    {
        /// <summary>
        /// 获取类型的<see cref="DescriptionAttribute.Description"/>的值
        /// <para>如果不存在<see cref="DescriptionAttribute"/>特性，则返回类型名称</para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetClassDescription(this Type type)
        {
            var attributes = (DescriptionAttribute[])type.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length <= 0)
                return type.Name;
            return attributes[0]?.Description;
        }
        /// <summary>
        /// 获取对象指定字段的类型的<see cref="DescriptionAttribute.Description"/>的值
        /// <para>如果不存在<see cref="DescriptionAttribute"/>特性，则返回字段名称</para>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string GetFieldDescription(this Type type, string fieldName)
        {
            var fieldInfo = type.GetField(fieldName);
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length <= 0)
                return fieldName;
            return attributes[0]?.Description;
        }
        /// <summary>
        /// 获取对象指定属性的<see cref="DescriptionAttribute.Description"/>的值
        /// <para>如果不存在<see cref="DescriptionAttribute"/>特性，则返回属性名称</para>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string GetPropertyDescription(this Type type, string propertyName)
        {
            var propertyInfo = type.GetProperty(propertyName);
            var attributes = (DescriptionAttribute[])propertyInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length <= 0)
                return propertyName;
            return attributes[0]?.Description;
        }
        /// <summary>
        /// 获取枚举值的<see cref="DescriptionAttribute.Description"/>的值
        /// <para>如果不存在<see cref="DescriptionAttribute"/>特性，则返回枚举值名称</para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetEnumValueDescription(this Enum value)
        {
            if (value == null)
                throw new ArgumentException($"枚举参数{nameof(value)}为空，请确认参数{nameof(value)}为枚举类型！");

            var name = value.ToString();
            var fieldInfo = value.GetType().GetField(name);
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length <= 0)
                return name;
            return attributes[0]?.Description;
        }
    }
}
