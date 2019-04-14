/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/5 14:20:53
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;

namespace WLib.ArcGis.Carto.MapExport.Base
{
    /// <summary>
    /// 出图的元素信息集合
    /// </summary>
    public class ElementInfoCollection : List<ElementInfo>
    {
        /// <summary>
        /// 出图的元素信息集合
        /// </summary>
        public ElementInfoCollection() { }
        /// <summary>
        /// 出图的元素信息集合
        /// </summary>
        /// <param name="collection"></param>
        public ElementInfoCollection(IEnumerable<ElementInfo> collection) : base(collection) { }

        /// <summary>
        /// 获取指定元素名或元素标识的出图的元素信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ElementInfo this[string name] => this.First(v => v.Name == name);
        /// <summary>
        /// 添加出图元素信息
        /// </summary>
        /// <param name="name">元素名称或元素标识</param>
        /// <param name="type">元素类别</param>
        /// <param name="value">元素内容</param>
        public void Add(string name, EPageElementType type, object value) => base.Add(new ElementInfo(name, type, value));
}
}
