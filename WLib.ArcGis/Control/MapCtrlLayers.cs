/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using System.IO;
using System.Linq;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.GeoDatabase.FeatClass;
using WLib.ArcGis.GeoDatabase.WorkSpace;

namespace WLib.ArcGis.Control
{
    /// <summary>
    /// 向地图控件获取或加载图层的操作
    /// </summary>
    public static class MapCtrlLayers
    {
        #region 获取图层
        /// <summary>
        /// 根据图层名称，在地图控件上查找对应图层，找不到则返回Null
        /// </summary>
        /// <param name="mapControl"></param>
        /// <param name="layerName">图层名称</param>
        /// <returns></returns>
        public static ILayer GetLayer(this AxMapControl mapControl, string layerName)
        {
            for (int i = 0; i < mapControl.LayerCount; i++)
            {
                if (mapControl.get_Layer(i).Name.Equals(layerName))
                    return mapControl.get_Layer(i);
            }
            return null;
        }
        /// <summary>
        /// 根据图层名称，在地图控件上查找对应图层，找不到则返回Null
        /// </summary>
        /// <param name="mapControl"></param>
        /// <param name="layerName">图层名称</param>
        /// <param name="layerIndex">图层索引</param>
        /// <returns></returns>
        public static ILayer GetLayer(this AxMapControl mapControl, string layerName, out int layerIndex)
        {
            for (layerIndex = 0; layerIndex < mapControl.LayerCount; layerIndex++)
            {
                if (mapControl.get_Layer(layerIndex).Name.Equals(layerName))
                    return mapControl.get_Layer(layerIndex);
            }
            layerIndex = -1;
            return null;
        }
        /// <summary>
        /// 获取地图控件上的所有图层
        /// </summary>
        /// <param name="mapControl"></param>
        /// <returns></returns>
        public static List<ILayer> GetLayers(this AxMapControl mapControl)
        {
            List<ILayer> layers = new List<ILayer>();
            for (int i = 0; i < mapControl.LayerCount; i++)
            {
                layers.Add(mapControl.get_Layer(i));
            }
            return layers;
        }
        /// <summary>
        /// 获取地图控件上的所有图层的图层名
        /// </summary>
        /// <param name="mapControl"></param>
        /// <returns></returns>
        public static List<string> GetLayerNames(this AxMapControl mapControl)
        {
            return GetLayers(mapControl).Select(v => v.Name).ToList();
        }
        #endregion


        #region 获取图层索引
        /// <summary>
        /// 根据图层名称，在地图控件上查找第一个对应图层，返回图层索引，找不到则返回-1
        /// </summary>
        /// <param name="mapControl"></param>
        /// <param name="layerName">图层名称</param>
        /// <returns></returns>
        public static int GetLayerIndex(this AxMapControl mapControl, string layerName)
        {
            for (int i = 0; i < mapControl.LayerCount; i++)
            {
                if (mapControl.get_Layer(i).Name.Equals(layerName))
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// 在地图控件上查找对应图层，返回图层索引，找不到则返回-1
        /// </summary>
        /// <param name="mapControl"></param>
        /// <param name="layer">图层</param>
        /// <returns></returns>
        public static int GetLayerIndex(this AxMapControl mapControl, ILayer layer)
        {
            for (int i = 0; i < mapControl.LayerCount; i++)
            {
                if (mapControl.get_Layer(i) == layer)
                    return i;
            }
            return -1;
        }
        #endregion


        #region 获取矢量图层
        /// <summary>
        /// 根据图层名称，在地图控件上查找对应矢量图层，找不到则返回Null
        /// </summary>
        /// <param name="mapControl"></param>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public static IFeatureLayer GetFeatureLayer(this AxMapControl mapControl, string layerName)
        {
            for (int i = 0; i < mapControl.LayerCount; i++)
            {
                ILayer tmpLayer = mapControl.get_Layer(i);
                if (tmpLayer is IFeatureLayer featureLayer && tmpLayer.Name.Equals(layerName))
                    return featureLayer;
            }
            return null;
        }
        /// <summary>
        /// 得到地图控件上的所有矢量图层
        /// </summary>
        /// <param name="mapControl"></param>
        /// <returns></returns>
        public static List<IFeatureLayer> GetFeatureLayers(this AxMapControl mapControl)
        {
            List<IFeatureLayer> featureLayers = new List<IFeatureLayer>();
            for (int i = 0; i < mapControl.LayerCount; i++)
            {
                ILayer tmpLayer = mapControl.get_Layer(i);
                if (tmpLayer is IFeatureLayer layer)
                    featureLayers.Add(layer);
            }
            return featureLayers;
        }
        /// <summary>
        /// 获取地图控件上的所有矢量图层的图层名
        /// </summary>
        /// <param name="mapControl"></param>
        /// <returns></returns>
        public static List<string> GetFeatureLayerNames(this AxMapControl mapControl)
        {
            return GetFeatureLayers(mapControl).Select(v => v.Name).ToList();
        }
        #endregion


        #region 加载矢量图层
        /// <summary>
        /// 将指定路径下的要素数据源数据，加载到地图控件中
        /// <para>可加载lyr/shp/mdb/gdb/dwg等多种数据源数据，允许的路径形式参考<see cref="FeatureClassEx.AllFromPath"/>方法</para>
        /// </summary>
        /// <param name="mapControl">地图控件</param>
        /// <param name="connStrOrPath">要素数据源路径，允许的路径形式参考<see cref="FeatureClassEx.AllFromPath(string)"/>方法</param>
        public static void LoadFeatureLayers(this AxMapControl mapControl, string connStrOrPath)
        {
            if (Path.GetExtension(connStrOrPath) == ".lyr" && File.Exists(connStrOrPath))
            {
                mapControl.AddLayerFromFile(connStrOrPath);
                return;
            }
            var featureClasses = FeatureClassEx.AllFromPath(connStrOrPath);
            foreach (var featureClass in featureClasses)
                mapControl.AddLayer(new FeatureLayerClass { FeatureClass = featureClass, Name = featureClass.AliasName });
        }
        /// <summary>
        /// 将指定路径下的要素图层加载到地图控件中
        /// <para>可加载lyr/shp/mdb/gdb/dwg等多种数据源图层，允许的路径形式参考<see cref="FeatureClassEx.FromPath"/>方法</para>
        /// </summary>
        /// <param name="mapControl">地图控件</param>
        /// <param name="layerPath">要素类图层的完整路径，允许的路径形式参考<see cref="FeatureClassEx.FromPath"/>方法</param>
        public static void LoadFeatureLayer(this AxMapControl mapControl, string layerPath)
        {
            if (Path.GetExtension(layerPath) == ".lyr" && File.Exists(layerPath))
            {
                mapControl.AddLayerFromFile(layerPath);
                return;
            }
            var featureClass = FeatureClassEx.FromPath(layerPath);
            mapControl.AddLayer(new FeatureLayerClass { FeatureClass = featureClass, Name = featureClass.AliasName });
        }
        /// <summary>
        /// 将工作空间的所有要素类加载到地图控件中，图层名使用要素类别名
        /// </summary>
        /// <param name="mapControl">地图控件</param>
        /// <param name="strConOrPath">工作空间的路径，或者是sde的连接字符串</param>
        /// <param name="zoomToClass">指定要素类名称或别名，地图将缩放到此要素类的显示范围</param>
        public static void LoadAllFeatureLayers(this AxMapControl mapControl, string strConOrPath, string zoomToClass = null)
        {
            var workspace = WorkspaceEx.GetWorkSpace(strConOrPath);
            LoadAllFeatureLayers(mapControl, workspace, zoomToClass);
        }
        /// <summary>
        /// 将工作空间的所有要素类加载到地图控件中，图层名使用要素类别名
        /// </summary>
        /// <param name="mapControl">地图控件</param>
        /// <param name="workspace">工作空间</param>
        /// <param name="zoomToClass">指定要素类名称或别名，地图将缩放到此要素类的显示范围</param>
        public static void LoadAllFeatureLayers(this AxMapControl mapControl, IWorkspace workspace, string zoomToClass = null)
        {
            var featureClasses = workspace.GetFeatureClasses();

            featureClasses = featureClasses.SortByGeoType();

            //将要素类作为图层加入地图控件中
            for (int i = featureClasses.Count - 1; i >= 0; i--)
            {
                var featureClass = featureClasses[i];
                IFeatureLayer layer = new FeatureLayerClass
                {
                    FeatureClass = featureClass,
                    Name = featureClass.AliasName
                };
                if (!string.IsNullOrEmpty(zoomToClass))
                {
                    if (featureClass.AliasName == zoomToClass || ((IDataset)featureClass).Name == zoomToClass)
                        mapControl.ActiveView.Extent = layer.AreaOfInterest;
                }
                mapControl.AddLayer(layer);
            }
        }
        /// <summary>
        /// 将工作空间的指定要素类加载到地图控件中，图层名使用要素类别名
        /// </summary>
        /// <param name="mapControl">地图控件</param>
        /// <param name="workspace">工作空间</param>
        /// <param name="zoomToClass">指定要素类名称或别名，地图将缩放到此要素类的显示范围（此值可空，若非空，则应包含在loadClasses中）</param>
        /// <param name="loadClasses">指定要加载的要素类集合</param>
        public static void LoadFeatureLayers(this AxMapControl mapControl, IWorkspace workspace, string zoomToClass, params string[] loadClasses)
        {
            var featureClasses = workspace.GetFeatureClasses();
            for (int i = 0; i < featureClasses.Count; i++)
            {
                var featureClass = featureClasses[i];
                if (loadClasses.Contains(featureClass.AliasName) || loadClasses.Contains(((IDataset)featureClass).Name))
                {
                    IFeatureLayer layer = new FeatureLayerClass
                    {
                        FeatureClass = featureClass,
                        Name = featureClass.AliasName
                    };
                    mapControl.AddLayer(layer);
                    if (!string.IsNullOrEmpty(zoomToClass))
                    {
                        if (featureClass.AliasName == zoomToClass || ((IDataset)featureClass).Name == zoomToClass)
                            mapControl.ActiveView.Extent = layer.AreaOfInterest;
                    }
                }
            }
        }
        #endregion
    }
}
