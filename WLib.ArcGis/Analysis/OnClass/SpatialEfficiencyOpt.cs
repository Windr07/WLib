/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace WLib.ArcGis.Analysis.OnClass
{
    /// <summary>
    /// 提供提高空间查询效率的方法
    /// （参考：http://resources.arcgis.com/en/help/arcobjects-net/componenthelp/index.html#//002500000831000000）
    /// （参考：http://blog.csdn.net/hellolib/article/details/70227756）
    /// </summary>
    public static class SpatialEfficiencyOpt
    {
        #region 空间缓存
        /// <summary>
        /// 对要素类创建空间缓存
        /// </summary>
        /// <param name="featureClass"></param>
        /// <returns></returns>
        public static ISpatialCacheManager CreateCache(this IFeatureClass featureClass)
        {
            ISpatialCacheManager spatialCacheManager = (ISpatialCacheManager)((IDataset)featureClass).Workspace;//填充Spatial Cache
            IEnvelope cacheExtent = ((IGeoDataset)featureClass).Extent;
            
            if (!spatialCacheManager.CacheIsFull)//检测是否存在缓存
                spatialCacheManager.FillCache(cacheExtent);  //不存在，则创建缓存

            return spatialCacheManager;
        }
        /// <summary>
        /// 对要素类创建空间缓存，执行指定操作，然后清空空间缓存
        /// </summary>
        /// <param name="featureClass"></param>
        /// <param name="action"></param>
        public static void CreateCache(this IFeatureClass featureClass, Action action)
        {
            var spatialCacheManager = CreateCache(featureClass);
            action();
            spatialCacheManager.EmptyCache(); //清空空间缓存
        }
        #endregion

        #region GeometryBag和空间索引
        /// <summary>
        /// 将要素类中的图形存放到GeometryBag
        /// </summary>
        /// <param name="featureClass"></param>
        /// <returns></returns>
        public static IGeometryBag AddToGeometryBag(this IFeatureClass featureClass)
        {
            //创建GoemetryBag
            IGeometryBag geometryBag = new GeometryBagClass();
            IGeometryCollection geometryCollection = (IGeometryCollection)geometryBag;

            //给GeometryBag赋空间参考
            ISpatialReference spatialReference = ((IGeoDataset)featureClass).SpatialReference;
            geometryBag.SpatialReference = spatialReference;

            //遍历面要素类，逐一获取Geometry并添加到GeometryBag中
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.SubFields = "Shape";//Search如果返回属性值的话设置SubFields会提高效率
            IFeatureCursor cursor = featureClass.Search(queryFilter, true);
            IFeature feature;
            while ((feature = cursor.NextFeature()) != null)
            {
                geometryCollection.AddGeometry(feature.ShapeCopy);
            }
            return geometryBag;
        }
        /// <summary>
        /// 为GeometryBag生成空间索引（提高查询效率）
        /// </summary>
        /// <param name="geometryBag"></param>
        public static void CreateSpatialIndex(this IGeometryBag geometryBag)
        {
            ISpatialIndex spatialIndex = (ISpatialIndex)geometryBag;
            spatialIndex.AllowIndexing = true;
            spatialIndex.Invalidate();
        }
        #endregion
    }
}
