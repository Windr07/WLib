/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Attributes
{
    /// <summary>
    /// 表示对象的字段或属性能够转成节点的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class NodeAttribute : Attribute
    {
        /// <summary>
        /// 字段或属性转成节点后，节点显示文本的格式
        /// <para>默认值为"N: V"，"N"代表属性名，"V"代表属性值</para>
        /// </summary>
        public string TextFormat { get; set; }

        /// <summary>
        /// 表示对象的字段或属性能够转成节点的特性
        /// </summary>
        public NodeAttribute() => TextFormat = "N: V";
        /// <summary>
        /// 表示对象的字段或属性能够转成节点的特性
        /// </summary>
        /// <param name="textFormat">字段或属性转成节点后，节点显示文本的格式</param>
        public NodeAttribute(string textFormat) => TextFormat = textFormat;
    }
}
