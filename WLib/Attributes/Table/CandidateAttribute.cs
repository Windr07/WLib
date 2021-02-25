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
    /// 表示字段拥有的候选项
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CandidateAttribute : Attribute
    {
        /// <summary>
        /// 字段是否必须符合候选项，不能设置其他值
        /// </summary>
        public bool MustBeInRange { get; set; }
        /// <summary>
        /// 候选项
        /// </summary>
        public string[] Items { get; set; }
        /// <summary>
        /// 表示字段拥有的候选项
        /// </summary>
        /// <param name="mustInRange"></param>
        /// <param name="items"></param>
        public CandidateAttribute(bool mustInRange, params string[] items)
        {
            this.MustBeInRange = mustInRange;
            Items = items;
        }
    }
}
