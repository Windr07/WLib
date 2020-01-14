/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/4 15:48:07
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.ArcGis.Carto.MapExport.Base
{
    /// <summary>
    /// 出图的元素信息
    /// </summary>
    [Serializable]
    public class ElementInfo
    {
        /// <summary>
        /// 元素名称或元素文本标识
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 元素类别
        /// </summary>
        public EPageElementType Type { get; set; }
        /// <summary>
        /// 元素内容
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 表示设置元素的哪一类属性（默认设置元素文本）
        /// </summary>
        public EElementValueType ValueType { get; set; } = EElementValueType.Text;


        /// <summary>
        /// 出图的元素信息
        /// </summary>
        public ElementInfo() { }
        /// <summary>
        /// 出图的元素信息
        /// </summary>
        /// <param name="name">元素名称或元素文本标识</param>
        /// <param name="type">元素类别</param>
        /// <param name="value">元素内容</param>
        /// <param name="setValueType">表示设置元素的哪一类属性</param>
        public ElementInfo(string name, EPageElementType type, object value, EElementValueType setValueType = EElementValueType.Text)
        {
            Name = name;
            Type = type;
            Value = value;
            ValueType = setValueType;
        }
    }
}
