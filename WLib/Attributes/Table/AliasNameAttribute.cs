/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Attributes.Table
{
    /// <summary>
    /// 表示字段别名
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class AliasNameAttribute : Attribute
    {
        /// <summary>
        /// 字段别名
        /// </summary>
        public string AliasName { get; set; }
        /// <summary>
        /// 表示字段别名
        /// </summary>
        /// <param name="aliasName"></param>
        public AliasNameAttribute(string aliasName) => AliasName = aliasName;
    }
}
