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
        /// 候选项
        /// </summary>
        public string[] Items { get; set; }
        /// <summary>
        /// 表示字段拥有的候选项
        /// </summary>
        /// <param name="items"></param>
        public CandidateAttribute(params string[] items) => Items = items;
    }
}
