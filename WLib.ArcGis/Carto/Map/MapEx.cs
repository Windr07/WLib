/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using WLib.ArcGis.Carto.Layer;

namespace WLib.ArcGis.Carto.Map
{
    /// <summary>
    /// 提供在地图中查询图层、表格、获取选中要素等方法
    /// </summary>
    public static partial class MapEx
    {
        #region 获取多个图层
        /// <summary>
        /// 得到地图上的所有矢量图层，包括图层组中的矢量图层
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static List<IFeatureLayer> GetFeatureLayers(this IMap map)
        {
            var featureLayers = new List<IFeatureLayer>();
            IEnumLayer layers = map.get_Layers(LayerUid.IFeatureLayer.CreateUid(), true);//recursive为true时候返回group layers里面的图层
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
        public static List<IFeatureLayer> GetFeatureLayersWithoutAnno(this IMap map)
        {
            var featureLayers = new List<IFeatureLayer>();
            for (int i = 0; i < map.LayerCount; i++)
            {
                AddFeatureLayerWithOutAnno(map.Layer[i], ref featureLayers);
            }
            return featureLayers;
        }
        /// <summary>
        /// 获取地图中所有存储元素的Graphics图层
        /// （通过不同的graphicslayer分类管理元素）
        /// （通过graphicslayer as IGraphicsContainer来增删改查元素，参考：https://www.cnblogs.com/bobird/articles/3169592.html）
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static List<IGraphicsLayer> GetGraphicsLayers(this IMap map)
        {
            var resultGraphicsLayers = new List<IGraphicsLayer>();
            var compositeLayer = map.BasicGraphicsLayer as ICompositeGraphicsLayer as ICompositeLayer;//存储元素(Element)的图层
            for (int i = 0; i < compositeLayer.Count; i++)
            {
                resultGraphicsLayers.Add(compositeLayer.get_Layer(i) as IGraphicsLayer);
            }
            return resultGraphicsLayers;
        }
        /// <summary>
        /// 根据UID字符串（参考<see cref="LayerUid"/>）获取指定类型的图层
        /// </summary>
        /// <param name="map"></param>
        /// <param name="uidValue">图层类型UID字符串，建议通过<see cref="LayerUid"/>来指定</param>
        /// <returns></returns>
        public static List<ILayer> GetLayersByUid(this IMap map, string uidValue)
        {
            var layers = new List<ILayer>();
            IEnumLayer enumLayer = map.get_Layers(uidValue.CreateUid(), true);
            enumLayer.Reset();
            ILayer layer = null;
            while ((layer = enumLayer.Next()) != null)
            {
                layers.Add(layer);
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
            IEnumLayer layers = map.get_Layers(LayerUid.IFeatureLayer.CreateUid(), true);//recursive为true时候返回group layers里面的图层
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
        /// <param name="layerName">图层名或要素类名称</param>
        /// <returns></returns>
        public static IFeatureLayer GetFeatureLayer2(this IMap map, string layerName)
        {
            IEnumLayer layers = map.get_Layers(LayerUid.IFeatureLayer.CreateUid(), true);//recursive为true时候返回group layers里面的图层
            layers.Reset();
            ILayer layer;
            while ((layer = layers.Next()) != null)
            {
                if (layer is IFeatureLayer featureLayer)
                {
                    if (featureLayer.Name == layerName)
                        return featureLayer;

                    if (featureLayer.FeatureClass != null && (featureLayer.FeatureClass as IDataset).Name == layerName)
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
            IEnumLayer layers = map.get_Layers(LayerUid.IFeatureLayer.CreateUid(), true);//recursive为true时候返回group layers里面的图层
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
        /// <param name="name">图层名或要素类名称</param>
        /// <returns></returns>
        public static IFeatureLayer GetFeatureLayerWithOutAnno(this IMap map, string name)
        {
            List<IFeatureLayer> featureLayers = GetFeatureLayersWithoutAnno(map);
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
                    AddFeatureLayerWithOutAnno(comLayer.Layer[i], ref layerCollection);
            }
            else if (layer is IFeatureLayer && !(layer is IAnnotationLayer))//过滤掉注记层，只将点、线、面层加入到集合中
            {
                layerCollection.Add(layer as IFeatureLayer);
            }
        }

        /// <summary>
        /// 根据图层名称，在地图上查找图层（默认查找IDataLayer），找不到则返回Null
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layerName">要查找的图层的名称</param>
        /// <param name="uidValue">要查找的图层类型的UID字符串，建议通过<see cref="LayerUid"/>来指定，默认为IDataLayer的UID</param>
        /// <returns></returns>
        public static ILayer GetLayer(this IMap map, string layerName, string uidValue = LayerUid.IDataLayer)
        {
            IEnumLayer layers = map.get_Layers(uidValue.CreateUid(), true);
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
        /// 根据图层名称关键字，在地图上查找图层（默认查找IDataLayer），找不到则返回Null
        /// </summary>
        /// <param name="map"></param>
        /// <param name="keyword">要查找的图层名关键字</param>
        /// <param name="uidValue">要查找的图层类型的UID字符串，建议通过<see cref="LayerUid"/>来指定，默认为IDataLayer的UID</param>
        /// <returns></returns>
        public static ILayer GetLayerByKeyword(this IMap map, string keyword, string uidValue = LayerUid.IDataLayer)
        {
            IEnumLayer layers = map.get_Layers(uidValue.CreateUid(), true);
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
        /// 获取地图中指定名称的存储元素的Graphics图层，找不到则返回Null
        /// （通过graphicslayer as IGraphicsContainer来增删改查元素，参考：https://www.cnblogs.com/bobird/articles/3169592.html）
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


        #region 其他图层操作
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
        public static string GetLayerPath(this IMap map, string layerName)
        {
            IEnumLayer layers = map.get_Layers(LayerUid.IDataLayer.CreateUid(), true);
            layers.Reset();
            ILayer layer;
            while ((layer = layers.Next()) != null)
            {
                if (layer.Name == layerName)
                {
                    IDatasetName datasetName = (IDatasetName)(layer as IDataLayer2).DataSourceName;
                    return datasetName.WorkspaceName.Category;
                }
            }
            return string.Empty;
        }
        #endregion


        #region 获取地图
        /// <summary>
        /// 查找地图(map)
        /// </summary>
        /// <param name="maps"></param>
        /// <param name="mapName">地图名称</param>
        /// <returns></returns>
        public static IMap GetMapFromMaps(this IMaps maps, string mapName)
        {
            for (int i = 0; i < maps.Count; i++)
            {
                if (maps.get_Item(i).Name == mapName)
                    return maps.get_Item(i);
            }
            return null;
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
    }
}
