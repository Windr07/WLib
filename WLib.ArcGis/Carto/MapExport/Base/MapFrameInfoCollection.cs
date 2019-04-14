/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/5 14:44:14
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;

namespace WLib.ArcGis.Carto.MapExport.Base
{
    /// <summary>
    /// 地图框信息集合
    /// </summary>
    public class MapFrameInfoCollection : List<MapFrameInfo>
    {
        /// <summary>
        /// 地图框信息集合
        /// </summary>
        public MapFrameInfoCollection() { }
        /// <summary>
        /// 地图框信息集合
        /// </summary>
        /// <param name="collection"></param>
        public MapFrameInfoCollection(IEnumerable<MapFrameInfo> collection) : base(collection) { }
        /// <summary>
        /// 根据指定地图框名称获取对应地图框信息
        /// </summary>
        /// <param name="mapFrameName">地图框名称（即地图名称）</param>
        /// <returns></returns>
        public MapFrameInfo this[string mapFrameName] => this.First(v => v.MapFrameName == mapFrameName);
        /// <summary>
        /// 添加一个地图框信息
        /// </summary>
        /// <param name="mapFrameName">地图框名称（即地图名称）</param>
        public void Add(string mapFrameName)
            => base.Add(new MapFrameInfo(mapFrameName));
        /// <summary>
        /// 添加一个地图框信息
        /// </summary>
        /// <param name="mapFrameName">地图框名称（即地图名称）</param>
        /// <param name="scale">地图应设定的比例尺</param>
        public void Add(string mapFrameName, double scale)
            => base.Add(new MapFrameInfo(mapFrameName, scale));
        /// <summary>
        /// 添加一个地图框信息
        /// </summary>
        /// <param name="mapFrameName">地图框名称（即地图名称）</param>
        /// <param name="scale">地图应设定的比例尺</param>
        /// <param name="layerInfos">地图框中包含的图层信息</param>
        public void Add(string mapFrameName, double scale, params LayerInfo[] layerInfos)
            => base.Add(new MapFrameInfo(mapFrameName, scale, layerInfos));
    }
}
