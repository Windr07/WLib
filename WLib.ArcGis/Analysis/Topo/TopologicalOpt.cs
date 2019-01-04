/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace WLib.ArcGis.Analysis.Topo
{
    /// <summary>
    /// 提供拓扑空间分析操作，例如创建缓冲区、相交等
    /// </summary>
    public class TopologicalOpt
    {
        /// <summary>
        /// 创建指定图形的缓冲区
        /// </summary>
        /// <param name="shape">要缓冲区的图形</param>
        /// <param name="tolerance">缓冲间隔</param>
        /// <returns>生成的缓冲区图形</returns>
        public static IGeometry GetBuffer(IGeometry shape, double tolerance)
        {
            ITopologicalOperator topo = shape as ITopologicalOperator;
            if (topo == null ||
                (tolerance < 0 && shape.GeometryType != esriGeometryType.esriGeometryPolygon))
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
        /// <summary>
        /// 将多个要素分别与指定图形进行相交，获得相交面积最大的要素
        /// </summary>
        /// <param name="features">进行相交筛选多个要素</param>
        /// <param name="geometry">作为筛选条件的图形</param>
        /// <param name="maxInsectArea">相交面积最大的要素的面积</param>
        /// <returns></returns>
        public static IFeature GetMaxIntersectFeature(IEnumerable<IFeature> features, IGeometry geometry, out double maxInsectArea)
        {
            if (!(geometry is ITopologicalOperator logicalOpt))
                throw new Exception("参数geometry（作为筛选条件的图形）不能为空！");

            IFeature maxInsectFeature = null; //相交面积最大的辅助地块
            maxInsectArea = 0.0;
            foreach (var feature in features)
            {
                IGeometry intersectGeo = logicalOpt.Intersect(feature.Shape, esriGeometryDimension.esriGeometry2Dimension);
                if (intersectGeo == null || intersectGeo.IsEmpty ||
                    intersectGeo.Dimension != esriGeometryDimension.esriGeometry2Dimension)
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
        public static IGeometry GetMaxIntersectGeometry(IEnumerable<IGeometry> geometries, IGeometry geometry, out double maxInsectArea)
        {
            if (!(geometry is ITopologicalOperator logicalOpt))
                throw new Exception("参数geometry（作为筛选条件的图形）不能为空！");

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
    }
}
