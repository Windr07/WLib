/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017/5/23 14:03:07
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Attributes.Description
{
    /// <summary>
    /// 对程序元素进行描述的特性
    /// <para>此特性继承自<see cref="System.ComponentModel.DescriptionAttribute"/>，添加<see cref="DescriptionTag"/>属性，且允许同一程序元素使用多个此特性</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public sealed class DescriptionExAttribute : System.ComponentModel.DescriptionAttribute
    {
        /// <summary>
        /// 对程序元素的文字描述
        /// </summary>
        public new string Description { get; set; }
        /// <summary>
        /// 对程序元素的文字描述的分类标签
        /// （由于一个字段值可以拥有多个描述特性，此值一般设定为对描述特性进行分组，默认值为0）
        /// </summary>
        public int DescriptionTag { get; set; }
        /// <summary>
        /// 对程序元素进行描述的特性
        /// <para>允许同一程序元素使用多个此特性</para>
        /// </summary>
        /// <param name="description">对字段的文字描述</param>
        public DescriptionExAttribute(string description)
            : base()
        {
            this.Description = description;
        }
        /// <summary>
        /// 对程序元素进行描述的特性
        /// <para>允许同一程序元素使用多个此特性</para>
        /// </summary>
        /// <param name="description">对程序元素的文字描述</param>
        /// <param name="descriptionTag">对程序元素的文字描述的分类标签，默认为0</param>
        public DescriptionExAttribute(string description, int descriptionTag)
            : base()
        {
            this.Description = description;
            this.DescriptionTag = descriptionTag;
        }
    }
}
