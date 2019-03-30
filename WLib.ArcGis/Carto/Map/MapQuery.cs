/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.Carto.Layer;

namespace WLib.ArcGis.Carto.Map
{
    /// <summary>
    /// 对地图、图层、表格、图形等的查询操作
    /// </summary>
    public static class MapQuery
    {
        #region 获取多个图层
        /// <summary>
        /// 得到地图上的所有矢量图层，包括图层组中的矢量图层（以图层UID查找的方式）
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static IEnumLayer GetEnumFeatureLayers(this IMap map)
        {
            //{6CA416B1-E160-11D2-9F4E-00C04F6BC78E} IDataLayer （all）
            //{40A9E885-5533-11d0-98BE-00805F7CED21} IFeatureLayer
            //{E156D7E5-22AF-11D3-9F99-00C04F6BC78E} IGeoFeatureLayer
            //{34B2EF81-F4AC-11D1-A245-080009B6F22B} IGraphicsLayer
            //{5CEAE408-4C0A-437F-9DB3-054D83919850} IFDOGraphicsLayer
            //{0C22A4C7-DAFD-11D2-9F46-00C04F6BC78E} ICoverageAnnotationLayer
            //{EDAD6644-1810-11D1-86AE-0000F8751720} IGroupLayer
            //UID uid = new UIDClass();
            //uid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";//FeatureLayer
            IEnumLayer layers = map.get_Layers(LayerUid.IFeatureLayer, true);//recursive为true时候返回group layers里面的图层
            return layers;
        }
        /// <summary>
        /// 得到地图上的所有矢量图层，包括图层组中的矢量图层
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static List<IFeatureLayer> GetFeatureLayers(this IMap map)
        {
            List<IFeatureLayer> featureLayers = new List<IFeatureLayer>();
            IEnumLayer layers = GetEnumFeatureLayers(map);
            layers.Reset();
            ILayer layer;
            while ((layer = layers.Next()) != null)
            {
                featureLayers.Add(layer as IFeatureLayer);
            }
            return featureLayers;
        }
        /// <summary>
        /// 得到地图上的所有矢量图层，包括图层组中的矢量图层，过滤掉注记图层
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static List<IFeatureLayer> GetFeatureLayers2(this IMap map)
        {
            List<IFeatureLayer> featureLayers = new List<IFeatureLayer>();
            for (int i = 0; i < map.LayerCount; i++)
            {
                AddFeatureLayerWithOutAnno(map.Layer[i], ref featureLayers);
            }
            return featureLayers;
        }
        /// <summary>
        /// 获取所有Graphics图层
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static List<IGraphicsLayer> GetAllGraphicsLayers(this IMap map)
        {
            var resultGraphicsLayers = new List<IGraphicsLayer>();
            var compositeGrapLayer = map.BasicGraphicsLayer as ICompositeGraphicsLayer;
            var compositeLayer = compositeGrapLayer as ICompositeLayer;
            for (int i = 0; i < compositeLayer.Count; i++)
            {
                ILayer layer = compositeLayer.get_Layer(i);
                resultGraphicsLayers.Add(layer as IGraphicsLayer);
            }
            return resultGraphicsLayers;
        }
        /// <summary>
        /// 根据不同类型的图层的<see cref="LayerUid"/>来获取图层列表
        /// </summary>
        /// <param name="map"></param>
        /// <param name="uid">图层UID，参考<see cref="LayerUid"/></param>
        /// <returns></returns>
        public static List<ILayer> GetLayersByUid(this IMap map, UID uid)
        {
            List<ILayer> layers = new List<ILayer>();
            IEnumLayer enumLayer = map.get_Layers(uid, true);
            enumLayer.Reset();
            ILayer player = enumLayer.Next();
            while (player != null)
            {
                layers.Add(player);
                player = enumLayer.Next();
            }
            return layers;
        }
        #endregion


        #region 获取单个图层
        /// <summary>
        /// 根据图层名称，在地图上查找对应矢量图层,找不到则返回Null
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public static IFeatureLayer GetFeatureLayer(this IMap map, string layerName)
        {
            IEnumLayer layers = GetEnumFeatureLayers(map);
            layers.Reset();
            ILayer layer;
            while ((layer = layers.Next()) != null)
            {
                if (layer.Name == layerName)
                    return layer as IFeatureLayer;
            }
            return null;
        }
        /// <summary>
        /// 根据图层名或要素类名称，在地图上查找对应矢量图层,找不到则返回Null
        /// </summary>
        /// <param name="map"></param>
        /// <param name="name">图层名或要素类名称</param>
        /// <returns></returns>
        public static IFeatureLayer GetFeatureLayer2(this IMap map, string name)
        {
            IEnumLayer layers = GetEnumFeatureLayers(map);
            layers.Reset();
            ILayer layer;
            while ((layer = layers.Next()) != null)
            {
                if (layer is IFeatureLayer featureLayer)
                {
                    if (featureLayer.Name == name)
                        return featureLayer;

                    if (featureLayer.FeatureClass != null && (featureLayer.FeatureClass as IDataset).Name == name)
                        return featureLayer;
                }
            }
            return null;
        }
        /// <summary>
        /// 根据图层名或要素类名称关键字，在地图上查找对应矢量图层,找不到则返回Null
        /// </summary>
        /// <param name="map"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static IFeatureLayer GetFeatureLayerByKeyword(this IMap map, string keyword)
        {
            IEnumLayer layers = GetEnumFeatureLayers(map);
            layers.Reset();
            ILayer layer;
            while ((layer = layers.Next()) != null)
            {
                if (layer is IFeatureLayer featureLayer)
                {
                    if (featureLayer.Name.Contains(keyword))
                        return featureLayer;

                    if (featureLayer.FeatureClass != null && (featureLayer.FeatureClass as IDataset).Name.Contains(keyword))
                        return featureLayer;
                }
            }
            return null;
        }

        /// <summary>
        /// 根据图层名或要素类名称，在地图上查找对应矢量图层，过滤掉注记图层，找不到则返回Null
        /// </summary>
        /// <param name="map"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IFeatureLayer GetFeatureLayerWithOutAnno(this IMap map, string name)
        {
            List<IFeatureLayer> featureLayers = GetFeatureLayers2(map);
            for (int i = 0; i < featureLayers.Count; i++)
            {
                IFeatureLayer featureLayer = featureLayers[i] as IFeatureLayer;
                if (featureLayer != null)
                {
                    if (featureLayer.Name == name)
                        return featureLayer;
                    if (featureLayer.FeatureClass != null &&
                        (featureLayer.FeatureClass as IDataset).Name == name)
                        return featureLayer;
                }
            }
            return null;
        }
        /// <summary>
        /// 若图层为要素图层则加入图层列表，若图层为组合图层则获取其子要素图层，过滤掉注记图层
        /// </summary>
        /// <param name="layer">图层（或是组合图层）</param>
        /// <param name="layerCollection"></param>
        private static void AddFeatureLayerWithOutAnno(ILayer layer, ref List<IFeatureLayer> layerCollection)
        {
            if (layer is IGroupLayer)
            {
                ICompositeLayer comLayer = layer as ICompositeLayer;
                for (int i = 0; i < comLayer.Count; i++)
                {
                    AddFeatureLayerWithOutAnno(comLayer.Layer[i], ref layerCollection);
                }
                layer.Visible = true;
            }
            else
            {
                //过滤掉注记层，只将点、线、面层加入到集合中
                if (layer is IFeatureLayer && !(layer is IAnnotationLayer))
                {
                    layerCollection.Add(layer as IFeatureLayer);
                }
            }
        }

        /// <summary>
        /// 根据图层名称，在地图上查找图层，找不到则返回Null
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public static ILayer GetDataLayer(this IMap map, string layerName)
        {
            IEnumLayer layers = map.get_Layers(LayerUid.IDataLayer, true);
            layers.Reset();
            ILayer layer;
            while ((layer = layers.Next()) != null)
            {
                if (layer.Name == layerName)
                    return layer;
            }
            return null;
        }
        /// <summary>
        /// 根据图层名称关键字，在地图上查找图层，找不到则返回Null
        /// </summary>
        /// <param name="map"></param>
        /// <param name="keyword">图层名关键字</param>
        /// <returns></returns>
        public static ILayer GetDataLayerByKeyword(this IMap map, string keyword)
        {
            IEnumLayer layers = map.get_Layers(LayerUid.IDataLayer, true);
            layers.Reset();
            ILayer layer;
            while ((layer = layers.Next()) != null)
            {
                if (layer.Name.Contains(keyword))
                    return layer;
            }
            return null;
        }
        /// <summary>
        /// 获取指定的Graphics图层，找不到则返回Null
        /// </summary>
        /// <param name="map">地图</param>
        /// <param name="layerName">图层名</param>
        /// <returns></returns>
        public static IGraphicsLayer GetGraphicsLayer(this IMap map, string layerName)
        {
            ICompositeGraphicsLayer compositeGrapLayer = map.BasicGraphicsLayer as ICompositeGraphicsLayer;
            ICompositeLayer compositeLayer = compositeGrapLayer as ICompositeLayer;
            for (int i = 0; i < compositeLayer.Count; i++)
            {
                if (compositeLayer.get_Layer(i).Name.Equals(layerName))
                    return compositeLayer.get_Layer(i) as IGraphicsLayer;
            }
            return null;
        }
        #endregion


        #region 获取地图
        /// <summary>
        /// 从地图文档中查找地图(map)
        /// </summary>
        /// <param name="mapDoc"></param>
        /// <param name="mapName">地图名称</param>
        /// <returns></returns>
        public static IMap GetMapFromMapDocument(this IMapDocument mapDoc, string mapName)
        {
            IMap map = null;
            for (int i = 0; i < mapDoc.MapCount; i++)
            {
                if (mapDoc.Map[i].Name == mapName)
                {
                    map = mapDoc.Map[i];
                    break;
                }
            }
            return map;
        }
        /// <summary>
        /// 查找地图(map)
        /// </summary>
        /// <param name="maps"></param>
        /// <param name="mapName">地图名称</param>
        /// <returns></returns>
        public static IMap GetMapFromMaps(this IMaps maps, string mapName)
        {
            IMap map = null;
            for (int i = 0; i < maps.Count; i++)
            {
                if (maps.get_Item(i).Name == mapName)
                {
                    map = maps.get_Item(i);
                    break;
                }
            }
            return map;
        }
        /// <summary>
        /// 获取地图文档中的所有地图(map)
        /// </summary>
        /// <param name="mapDoc"></param>
        /// <returns></returns>
        public static List<IMap> GetMapsFromMapDocument(this IMapDocument mapDoc)
        {
            List<IMap> maps = new List<IMap>();
            for (int i = 0; i < mapDoc.MapCount; i++)
            {
                maps.Add(mapDoc.Map[i]);
            }
            return maps;
        }
        #endregion


        #region 获取表格
        /// <summary>
        /// 根据表名，在地图上查找对应表,找不到则返回Null
        /// </summary>
        /// <param name="map"></param>
        /// <param name="name">表名</param>
        /// <returns></returns>
        public static ITable GetTable(this IMap map, string name)
        {
            return GetTable(map as ITableCollection, name);
        }
        /// <summary>
        /// 根据表名，在表集合上查找对应表,找不到则返回Null
        /// </summary>
        /// <param name="tableCollection">表集合（map as ITableCollection）</param>
        /// <param name="name">表名</param>
        /// <returns></returns>
        public static ITable GetTable(this ITableCollection tableCollection, string name)
        {
            ITable table = null;
            for (int i = 0; i < tableCollection.TableCount; i++)
            {
                ITable tmpTable = tableCollection.get_Table(i);
                if ((tmpTable as IDataset).Name == name)
                {
                    table = tmpTable;
                    break;
                }
            }
            return table;
        }
        /// <summary>
        /// 得到地图上的所有表
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static ITable[] GetTables(this IMap map)
        {
            return GetTables(map as ITableCollection);
        }
        /// <summary>
        /// 得到地图上的所有表
        /// </summary>
        /// <param name="tableCollection">表集合（map as ITableCollection）</param>
        /// <returns></returns>
        public static ITable[] GetTables(this ITableCollection tableCollection)
        {
            List<ITable> tableList = new List<ITable>();
            for (int i = 0; i < tableCollection.TableCount; i++)
            {
                tableList.Add(tableCollection.get_Table(i));
            }
            return tableList.ToArray();
        }
        #endregion


        /// <summary>
        /// 遍历地图的所有图层，对图层执行操作
        /// </summary>
        /// <param name="map"></param>
        /// <param name="action">对图层执行的操作</param>
        public static void EnumLayer(this IMap map, Action<ILayer> action)
        {
            for (int i = 0; i < map.LayerCount; i++)
            {
                ILayer layer = map.get_Layer(i);
                if (layer is IGroupLayer)
                    layer.EnumLayerInGroupLayer(action);
                else
                    action(layer);
            }
        }
        /// <summary>
        /// 通过图层名在地图中查找图层，并返回图层文件存放路径
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layerName">图层名</param>
        /// <returns></returns>
        public static string GetFileCategory(this IMap map, string layerName)
        {
            UID uid = LayerUid.IFeatureLayer;
            IEnumLayer layers = map.get_Layers(uid, true);
            layers.Reset();
            ILayer layer;
            while ((layer = layers.Next()) != null)
            {
                if (layer.Name == layerName)
                {
                    IDataLayer2 dataLayer2 = layer as IDataLayer2;
                    IDatasetName datasetName = (IDatasetName)dataLayer2.DataSourceName;
                    return datasetName.WorkspaceName.Category;
                }
            }
            return string.Empty;
        }
    }
}
