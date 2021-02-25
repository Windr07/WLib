using System;

namespace WLib.Attributes.Table
{
    /// <summary>
    /// 表示字段值可空
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NullableAttribute : Attribute
    {
    }
}
