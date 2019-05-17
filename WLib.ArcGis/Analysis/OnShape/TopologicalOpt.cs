/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using WLib.ArcGis.GeoDatabase.FeatClass;

namespace WLib.ArcGis.Analysis.OnShape
{
    /// <summary>
    /// 提供拓扑空间分析操作，例如创建缓冲区、相交等
    /// </summary>
    public static class TopologicalOpt
    {
        #region 创建缓冲区
        /// <summary>
        /// 创建指定图形的缓冲区
        /// </summary>
        /// <param name="shape">要缓冲区的图形</param>
        /// <param name="tolerance">缓冲间隔</param>
        /// <returns>生成的缓冲区图形</returns>
        public static IGeometry GetBuffer(this IGeometry shape, double tolerance)
        {
            ITopologicalOperator topo = shape as ITopologicalOperator;
            if (topo == null || (tolerance < 0 && shape.GeometryType != esriGeometryType.esriGeometryPolygon))
                return null;

            if (!topo.IsSimple)//若是不是简单图形，进行简化处理
                topo.Simplify();

            int iTry = 0;//测验测验10次
            IGeometry buffer = null;
            while (buffer == null && iTry <= 10)
            {
                try
                {
                    //若是调用不成功，将缓冲间隔加0.01倍再试（若是第10次成功，则缓冲间隔比本来大了0.09倍）
                    //若是对缓冲间隔请求更严格，可以削减增量的倍数
                    //按照测试经验，一般最多测验测验三次就可以成功
                    buffer = topo.Buffer(tolerance * (1.0 + 0.01 * iTry));
                }
                catch
                {
                    buffer = null;
                    iTry = iTry + 1;
                }
            }
            return buffer;
        }
        #endregion


        #region 获取相交面积
        /// <summary>
        /// 将多个要素分别与指定图形进行相交，获得相交面积最大的要素
        /// </summary>
        /// <param name="features">进行相交筛选多个要素</param>
        /// <param name="geometry">作为筛选条件的图形</param>
        /// <param name="maxInsectArea">相交面积最大的要素的面积</param>
        /// <returns></returns>
        public static IFeature GetMaxIntersectFeature(this IEnumerable<IFeature> features, IGeometry geometry, out double maxInsectArea)
        {
            if (!(geometry is ITopologicalOperator logicalOpt))
                throw new Exception($"参数{nameof(geometry)}（作为筛选条件的图形）不能为空！");

            IFeature maxInsectFeature = null; //相交面积最大的辅助地块
            maxInsectArea = 0.0;
            foreach (var feature in features)
            {
                IGeometry intersectGeo = logicalOpt.Intersect(feature.Shape, esriGeometryDimension.esriGeometry2Dimension);
                if (intersectGeo == null || intersectGeo.IsEmpty || intersectGeo.Dimension != esriGeometryDimension.esriGeometry2Dimension)
                    continue;

                double area = ((IArea)intersectGeo).Area;
                if (area >= maxInsectArea)
                {
                    maxInsectArea = area;
                    maxInsectFeature = feature;
                }
            }
            return maxInsectFeature;
        }
        /// <summary>
        /// 将多个图形分别与指定图形进行相交，获得相交面积最大的要素
        /// </summary>
        /// <param name="geometries">进行相交筛选多个图形</param>
        /// <param name="geometry">作为筛选条件的图形</param>
        /// <param name="maxInsectArea">相交面积最大的图形的面积</param>
        /// <returns></returns>
        public static IGeometry GetMaxIntersectGeometry(this IEnumerable<IGeometry> geometries, IGeometry geometry, out double maxInsectArea)
        {
            if (!(geometry is ITopologicalOperator logicalOpt))
                throw new Exception($"参数{nameof(geometry)}作为筛选条件的图形）不能为空！");

            IGeometry maxInsectGeometry = null; //相交面积最大的辅助地块
            maxInsectArea = 0.0;
            foreach (var tmpGeometry in geometries)
            {
                IGeometry intersectGeo = logicalOpt.Intersect(tmpGeometry, esriGeometryDimension.esriGeometry2Dimension);
                double area = ((IArea)intersectGeo).Area;
                if (area > maxInsectArea)
                {
                    maxInsectArea = area;
                    maxInsectGeometry = tmpGeometry;
                }
            }
            return maxInsectGeometry;
        }
        #endregion


        #region 合并图形（Union）
        /// <summary>
        /// 将多个图形合并(Union)成一个图形
        /// </summary>
        /// <param name="geometries"></param>
        /// <returns></returns>
        public static IGeometry UnionGeometry(this IEnumerable<IGeometry> geometries)
        {
            IGeometry unionGeometry = null;
            foreach (IGeometry geometry in geometries)
            {
                unionGeometry = unionGeometry == null ? geometry : ((ITopologicalOperator)unionGeometry).Union(geometry);
            }
            return unionGeometry;
        }
        /// <summary>
        /// 将多个图形合并(Union)成一个图形（使用GeometryBag提高合并效率）
        /// </summary>
        /// <param name="featureClass">从中查询图形的要素类</param>
        /// <param name="whereCluase">查询条件</param>
        /// <returns></returns>
        public static IGeometry UnionGeometryEx(this IFeatureClass featureClass, string whereCluase = null)
        {
            IGeometry geometryBag = new GeometryBagClass();
            geometryBag.SpatialReference = ((IGeoDataset)featureClass).SpatialReference;
            IGeometryCollection geometryCollection = geometryBag as IGeometryCollection;

            featureClass.QueryFeatures(whereCluase, f =>
            {
                object missing = Type.Missing;
                geometryCollection.AddGeometry(f.ShapeCopy, ref missing, ref missing);
            });
            return UnionGeometryEx(geometryBag, featureClass.ShapeType);
        }
        /// <summary>
        /// 将多个图形合并(Union)成一个图形（使用GeometryBag提高合并效率）
        /// </summary>
        /// <param name="geometries">需要合并的几何图形（注意这些图形必须是相同的几何类型）</param>
        /// <returns></returns>
        public static IGeometry UnionGeometryEx(this IEnumerable<IGeometry> geometries)
        {
            IGeometry geometryBag = new GeometryBagClass();
            geometryBag.SpatialReference = geometries.First().SpatialReference;
            IGeometryCollection geometryCollection = geometryBag as IGeometryCollection;

            foreach (var geometry in geometries)
            {
                object missing = Type.Missing;
                geometryCollection.AddGeometry(geometry, ref missing, ref missing);
            }
            return UnionGeometryEx(geometryBag, geometries.First().GeometryType);
        }
        /// <summary>
        /// 将多个图形合并(Union)成一个图形（使用GeometryBag提高合并效率）
        /// </summary>
        /// <param name="geometryBag"></param>
        /// <param name="geometryType">几何类型</param>
        /// <returns></returns>
        private static IGeometry UnionGeometryEx(this IGeometry geometryBag, esriGeometryType geometryType)
        {
            ITopologicalOperator unionedGeometry;
            switch (geometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    unionedGeometry = new PointClass();
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    unionedGeometry = new PolylineClass();
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    unionedGeometry = new PolygonClass();
                    break;
                default:
                    throw new NotImplementedException($"几何类型({nameof(geometryType)})应是点(point)、线(polyline)、多边形(polygon)之一，未实现{geometryType}类型的图形合并（Union）！");
            }

            unionedGeometry.ConstructUnion(geometryBag as IEnumGeometry);
            return (IGeometry)unionedGeometry;
        }
        #endregion
    }
}
