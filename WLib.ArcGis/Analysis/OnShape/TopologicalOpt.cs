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
using WLib.ArcGis.Geometry;

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
        /// 将多个面要素分别与指定面图形进行相交，获得相交面积最大的要素
        /// </summary>
        /// <param name="features">进行相交筛选多个要素</param>
        /// <param name="geometry">作为筛选条件的图形</param>
        /// <param name="maxInsectArea">相交面积最大的要素的面积</param>
        /// <returns></returns>
        public static IFeature GetMaxAreaIntersectFeature(this IEnumerable<IFeature> features, IGeometry geometry, out double maxInsectArea)
        {
            var logicalOpt = ValidateGeometryParams(features, geometry);
            IFeature maxInsectFeature = null; //相交面积最大的辅助地块
            maxInsectArea = 0.0;
            foreach (var feature in features)
            {
                IGeometry intersectGeo = logicalOpt.Intersect(feature.Shape, esriGeometryDimension.esriGeometry2Dimension);
                if (intersectGeo == null || intersectGeo.IsEmpty)
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
        /// 将多个面图形分别与指定面图形进行相交，获得相交面积最大的要素
        /// </summary>
        /// <param name="geometries">进行相交筛选多个图形</param>
        /// <param name="geometry">作为筛选条件的图形</param>
        /// <param name="maxInsectArea">相交面积最大的图形的面积</param>
        /// <returns></returns>
        public static IGeometry GetMaxAreaIntersectGeometry(this IEnumerable<IGeometry> geometries, IGeometry geometry, out double maxInsectArea)
        {
            var logicalOpt = ValidateGeometryParams(geometries, geometry);
            IGeometry maxInsectGeometry = null; //相交面积最大的辅助地块
            maxInsectArea = 0.0;
            foreach (var tmpGeometry in geometries)
            {
                IGeometry intersectGeo = logicalOpt.Intersect(tmpGeometry, esriGeometryDimension.esriGeometry2Dimension);
                if (intersectGeo == null || intersectGeo.IsEmpty)
                    continue;

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


        #region 获取相交长度
        /// <summary>
        /// 将多个图形分别与指定线图形进行相交，获得相交长度最大的要素
        /// </summary>
        /// <param name="features">进行相交筛选多个要素</param>
        /// <param name="geometry">作为筛选条件的图形</param>
        /// <param name="maxInsectLength">相交长度最大的图形的长度</param>
        /// <returns></returns>
        public static IFeature GetMaxLengthIntersectFeature(this IEnumerable<IFeature> features, IGeometry geometry, out double maxInsectLength)
        {
            var logicalOpt = ValidateGeometryParams(features, geometry);
            IFeature maxInsectFeature = null; //相交面积最大的辅助地块
            maxInsectLength = 0.0;
            foreach (var feature in features)
            {
                IGeometry intersectGeo = logicalOpt.Intersect(feature.Shape, esriGeometryDimension.esriGeometry1Dimension);
                if (intersectGeo == null || intersectGeo.IsEmpty)
                    continue;

                double length = (intersectGeo as IPolyline).Length;
                if (length > maxInsectLength)
                {
                    maxInsectLength = length;
                    maxInsectFeature = feature;
                }
            }
            return maxInsectFeature;
        }
        /// <summary>
        /// 将多个图形分别与指定线图形进行相交，获得相交长度最大的要素
        /// </summary>
        /// <param name="geometries">进行相交筛选多个图形</param>
        /// <param name="geometry">作为筛选条件的图形</param>
        /// <param name="maxInsectLength">相交长度最大的图形的长度</param>
        /// <returns></returns>
        public static IGeometry GetMaxLengthIntersectGeometry(this IEnumerable<IGeometry> geometries, IGeometry geometry, out double maxInsectLength)
        {
            var logicalOpt = ValidateGeometryParams(geometries, geometry);
            IGeometry maxInsectGeometry = null; //相交面积最大的辅助地块
            maxInsectLength = 0.0;
            foreach (var tmpGeometry in geometries)
            {
                IGeometry intersectGeo = logicalOpt.Intersect(tmpGeometry, esriGeometryDimension.esriGeometry1Dimension);
                if (intersectGeo == null || intersectGeo.IsEmpty)
                    continue;

                double length = (intersectGeo as IPolyline).Length;
                if (length > maxInsectLength)
                {
                    maxInsectLength = length;
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
        [Obsolete("建议改用UnionGeometryEx方法以提高合并效率")]
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
        /// 将查询获取的多个图形合并(Union)成一个图形（使用GeometryBag提高合并效率）
        /// <para>若查询获得的图形为0个，则返回null</para>
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

            if (geometryCollection.GeometryCount == 0) return null;
            return UnionGeometryEx(geometryBag, featureClass.ShapeType);
        }
        /// <summary>
        /// 将多个图形合并(Union)成一个图形（使用GeometryBag提高合并效率）
        /// <para>若合并的图形集为空或数量为0，则返回null</para>
        /// </summary>
        /// <param name="geometries">需要合并的几何图形（注意这些图形必须是相同的几何类型）</param>
        /// <returns></returns>
        public static IGeometry UnionGeometryEx(this IEnumerable<IGeometry> geometries)
        {
            if (geometries == null || geometries.Count() == 0) return null;

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
        /// <para>若输入的图形集为空或数量为0，则返回null</para>
        /// </summary>
        /// <param name="features">需要合并的几何图形所在的要素（注意这些要素的图形必须是相同的几何类型）</param>
        /// <returns></returns>
        public static IGeometry UnionGeometryEx(this IEnumerable<IFeature> features)
        {
            return UnionGeometryEx(features.Select(v => v.Shape));
        }
        /// <summary>
        /// 将多个图形合并(Union)成一个图形（使用GeometryBag提高合并效率）
        /// </summary>
        /// <param name="geometryBag"></param>
        /// <param name="geometryType">几何类型</param>
        /// <returns></returns>
        private static IGeometry UnionGeometryEx(this IGeometry geometryBag, esriGeometryType geometryType)
        {
            ITopologicalOperator geomerty;
            switch (geometryType)
            {
                case esriGeometryType.esriGeometryPoint: geomerty = new PointClass(); break;
                case esriGeometryType.esriGeometryPolyline: geomerty = new PolylineClass(); break;
                case esriGeometryType.esriGeometryPolygon: geomerty = new PolygonClass(); break;
                default: throw new NotImplementedException($"几何类型({nameof(geometryType)})应是点(point)、线(polyline)、多边形(polygon)之一，未实现{geometryType}类型的图形合并（Union）！");
            }

            geomerty.ConstructUnion(geometryBag as IEnumGeometry);
            return (IGeometry)geomerty;
        }
        #endregion


        /// <summary>
        /// 验证几何参数是否正确，包括参数不能为空、图形不能为空、坐标系一致
        /// </summary>
        /// <param name="geometries"></param>
        /// <param name="geometry"></param>
        /// <returns></returns>
        private static ITopologicalOperator ValidateGeometryParams(IEnumerable<IGeometry> geometries, IGeometry geometry)
        {
            if (!(geometry is ITopologicalOperator logicalOpt))
                throw new Exception($"参数{nameof(geometry)}作为筛选条件的图形）不能为空！");
            if (geometries == null || geometries.Count() == 0)
                throw new ArgumentException($"参数{nameof(geometries)}为空，或者元素个数为0！");
            if (!geometry.SpatialReference.CheckSpatialRef(geometries.First().SpatialReference, out var message))//验证坐标系是否一致
                throw new Exception(message);
            return logicalOpt;
        }
        /// <summary>
        /// 验证几何参数是否正确，包括参数不能为空、图形不能为空、坐标系一致
        /// </summary>
        /// <param name="features"></param>
        /// <param name="geometry"></param>
        /// <returns></returns>
        private static ITopologicalOperator ValidateGeometryParams(IEnumerable<IFeature> features, IGeometry geometry)
        {
            if (!(geometry is ITopologicalOperator logicalOpt))
                throw new Exception($"参数{nameof(geometry)}（作为筛选条件的图斑）不能为空！");
            if (features == null || features.Count() == 0)
                throw new ArgumentException($"参数{nameof(features)}为空，或者元素个数为0！");
            if (!geometry.SpatialReference.CheckSpatialRef(features.First().Shape.SpatialReference, out var message))//验证坐标系是否一致
                throw new Exception(message);
            return logicalOpt;
        }
    }
}
