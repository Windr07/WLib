/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace WLib.ArcGis.Carto.Map
{
    /// <summary>
    /// 获取地图中选中的要素的操作
    /// </summary>
    public static class FeatureSelection
    {
        /// <summary>
        /// 获取地图中选中的要素
        /// </summary>
        /// <param name="map">地图</param>
        /// <param name="layerName">图层名或要素类名，返回地图中指定要素图层中的选中要素，默认返回所有要素图层的选中要素</param>
        /// <returns></returns>
        public static List<IFeature> GetSelectionFeatures(this IMap map, string layerName)
        {
            var featureLayer = map.GetFeatureLayer2(layerName);
            return GetSelectionFeatures(map, featureLayer.FeatureClass);
        }
        /// <summary>
        /// 获取地图中选中的要素
        /// </summary>
        /// <param name="map">地图</param>
        /// <param name="featureLayer">要素图层，返回地图中指定要素图层中的选中要素，默认返回所有要素图层的选中要素</param>
        /// <returns></returns>
        public static List<IFeature> GetSelectionFeatures(this IMap map, IFeatureLayer featureLayer)
        {
            return GetSelectionFeatures(map, featureLayer.FeatureClass);
        }
        /// <summary>
        /// 获取地图中选中的要素
        /// </summary>
        /// <param name="map">地图</param>
        /// <param name="featureClass">要素类，返回地图中指定要素类中的选中要素，默认返回所有要素类的选中要素</param>
        /// <param name="type">几何类型，返回地图中指定几何类型的选中要素，默认返回所有几何类型的选中要素</param>
        /// <returns></returns>
        public static List<IFeature> GetSelectionFeatures(this IMap map, IFeatureClass featureClass = null, esriGeometryType type = esriGeometryType.esriGeometryNull)
        {
            List<IFeature> features = new List<IFeature>();//用于存储选中的要素
            IEnumFeature enumFeature = map.FeatureSelection as IEnumFeature;
            IFeature feature;
            while ((feature = enumFeature.Next()) != null)
            {
                features.Add(feature);
            }
            if (featureClass != null)
                features = features.Where(v => v.Class == featureClass).ToList();

            if (type != esriGeometryType.esriGeometryNull)
                features = features.Where(v => v.Shape.GeometryType == type).ToList();

            return features;
        }
    }
}
