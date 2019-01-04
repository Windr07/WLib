/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace WLib.ArcGis.Analysis
{
    /// <summary>
    /// 空间过滤器
    /// </summary>
    public static class SpatialFilterOpt
    {
        /// <summary>
        /// 筛选要素类中，与指定图形满足一定空间关系的要素
        /// (参考：http://blog.csdn.net/lanpy88/article/details/7173063)
        /// </summary>
        /// <param name="featureLayer">从中筛选要素的图层</param>
        /// <param name="geometry">过滤条件图形</param>
        /// <param name="spatialRefEnum">空间关系类型（举例：esriSpatialRelContains表示A包含B）</param>
        /// <param name="whereClause">查询条件</param>
        /// <returns></returns>
        public static List<IFeature> FilterFeatures(this IFeatureLayer featureLayer, IGeometry geometry, esriSpatialRelEnum spatialRefEnum, string whereClause = "")
        {
            return FilterFeatures(featureLayer.FeatureClass, geometry, spatialRefEnum, whereClause);
        }
        /// <summary>
        /// 筛选要素类中，与指定图形满足一定空间关系的要素
        /// (参考：http://blog.csdn.net/lanpy88/article/details/7173063)
        /// </summary>
        /// <param name="featureClass">从中筛选要素的要素类(A)</param>
        /// <param name="geometry">过滤条件图形(B)</param>
        /// <param name="spatialRefEnum">空间关系类型（举例：esriSpatialRelContains表示A包含B）</param>
        /// <param name="whereClause">查询条件</param>
        /// <returns></returns>
        public static List<IFeature> FilterFeatures(this IFeatureClass featureClass, IGeometry geometry, esriSpatialRelEnum spatialRefEnum, string whereClause = "")
        {
            List<IFeature> result = new List<IFeature>();
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = geometry;
            spatialFilter.SpatialRel = spatialRefEnum;
            spatialFilter.WhereClause = whereClause;
            IFeatureCursor cursor = featureClass.Search(spatialFilter, false);
            IFeature touchFeature = cursor.NextFeature();
            while (touchFeature != null)
            {
                result.Add(touchFeature);
                touchFeature = cursor.NextFeature();
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
            return result;
        }
        /// <summary>
        /// 筛选要素类中，与指定图形满足一定空间关系的要素（空间筛选时添加空间索引）
        /// (参考：http://blog.csdn.net/lanpy88/article/details/7173063)
        /// </summary>
        /// <param name="featureClass">从中筛选要素的要素类(A)</param>
        /// <param name="geometry">过滤条件图形(B)</param>
        /// <param name="spatialRefEnum">空间关系类型（举例：esriSpatialRelContains表示A包含B）</param>
        /// <param name="whereClause">查询条件</param>
        /// <returns></returns>
        public static List<IFeature> FilterFeaturesEx(this IFeatureClass featureClass, IGeometry geometry, esriSpatialRelEnum spatialRefEnum, string whereClause = "")
        {
            IGeometryBag geometryBag = new GeometryBagClass();
            IGeometryCollection geometryCollection = (IGeometryCollection)geometryBag;

            geometryBag.SpatialReference = ((IGeoDataset)featureClass).SpatialReference; //一定要给GeometryBag赋空间参考
            geometryCollection.AddGeometry(geometry);

            //为GeometryBag生成空间索引，以提高效率
            ISpatialIndex spatialIndex = (ISpatialIndex)geometryBag;
            spatialIndex.AllowIndexing = true;
            spatialIndex.Invalidate();

            return FilterFeatures(featureClass, geometry, spatialRefEnum, whereClause);
        }
    }
}
