/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/5 14:38:11
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace WLib.ArcGis.Carto.MapExport.Base
{
    /// <summary>
    /// 出图时的图层的设置信息集合
    /// </summary>
    [Serializable]
    public class LayerInfoCollection : List<LayerInfo>
    {
        /// <summary>
        /// 出图时的图层的设置信息集合
        /// </summary>
        public LayerInfoCollection() { }
        /// <summary>
        /// 出图时的图层的设置信息集合
        /// </summary>
        /// <param name="collection"></param>
        public LayerInfoCollection(IEnumerable<LayerInfo> collection) : base(collection) { }

        /// <summary>
        /// 根据图层名获取图层信息
        /// </summary>
        /// <param name="layerName">图层名称</param>
        /// <returns></returns>
        public LayerInfo this[string layerName] => this.First(v => v.Name == layerName);
        /// <summary>
        /// 根据图层在地图中的位置索引获取图层信息
        /// </summary>
        /// <param name="layerIndex">图层在地图中的索引</param>
        /// <returns></returns>
        public LayerInfo GetByLayerIndex(int layerIndex) => this.First(v => v.Index == layerIndex);
    }
}
