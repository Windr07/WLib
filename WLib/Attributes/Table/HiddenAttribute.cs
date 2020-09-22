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
    /// 表示隐藏字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class HiddenAttribute : Attribute
    {
    }
}
