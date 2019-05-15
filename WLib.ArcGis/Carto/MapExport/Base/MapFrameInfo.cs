/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/4 14:54:27
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/


using System;
using ESRI.ArcGIS.Geometry;

namespace WLib.ArcGis.Carto.MapExport.Base
{
    /// <summary>
    /// 表示一个地图框中的信息
    /// </summary>
    [Serializable]
    public class MapFrameInfo
    {
        /// <summary>
        /// 地图框名称
        /// </summary>
        public string MapFrameName { get; set; }
        /// <summary>
        /// 地图应设定的比例尺
        /// </summary>
        public double Scale { get; set; }
        /// <summary>
        /// 地图框中包含的图层信息
        /// </summary>
        public LayerInfoCollection LayerInfos { get; set; }
        /// <summary>
        /// 表示地图的显示范围（缩放范围）
        /// </summary>
        public IEnvelope MapExtent { get; set; }


        /// <summary>
        /// 表示一个地图框中的信息
        /// </summary>
        public MapFrameInfo()
        {
            LayerInfos = new LayerInfoCollection();
        }
        /// <summary>
        /// 表示一个地图框中的信息
        /// </summary>
        /// <param name="mapFrameName">地图框名称</param>
        public MapFrameInfo(string mapFrameName)
        {
            MapFrameName = mapFrameName;
            LayerInfos = new LayerInfoCollection();
        }
        /// <summary>
        /// 表示一个地图框中的信息
        /// </summary>
        /// <param name="mapFrameName">地图框名称</param>
        /// <param name="scale">地图应设定的比例尺</param>
        public MapFrameInfo(string mapFrameName, double scale)
        {
            MapFrameName = mapFrameName;
            Scale = scale;
            LayerInfos = new LayerInfoCollection();
        }
        /// <summary>
        /// 表示一个地图框中的信息
        /// </summary>
        /// <param name="mapFrameName">地图框名称</param>
        /// <param name="scale">地图应设定的比例尺</param>
        /// <param name="layerInfos">地图框中包含的图层信息</param>
        public MapFrameInfo(string mapFrameName, double scale,  params LayerInfo[] layerInfos)
        {
            MapFrameName = mapFrameName;
            Scale = scale;
            LayerInfos = new LayerInfoCollection();
            LayerInfos.AddRange(layerInfos);
        }
    }
}
