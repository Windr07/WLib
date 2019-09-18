using System;

namespace WLib.Attributes.Table
{
    /// <summary>
    /// 表示主键
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class KeyAttribute : Attribute
    {
    }
}
