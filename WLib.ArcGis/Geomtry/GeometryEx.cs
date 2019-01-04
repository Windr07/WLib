/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using WLib.ArcGis.GeoDb.FeatClass;

namespace WLib.ArcGis.Geomtry
{
    /// <summary>
    /// 点线面等几何的操作
    /// </summary>
    public class GeometryEx
    {
        /// <summary>
        /// 获取两个几何图形的最小距离
        /// </summary>
        /// <param name="geometryA">几何图形A</param>
        /// <param name="geometryB">几何图形B</param>
        /// <returns>两个几何图形的距离</returns>
        public static double GetTwoGeometryDistance(IGeometry geometryA, IGeometry geometryB)
        {
            IProximityOperator proOperator = (IProximityOperator)geometryA;
            if (geometryA != null && geometryB != null)
                return proOperator.ReturnDistance(geometryB);

            return 0;
        }
        /// <summary>
        /// 获取一个多边形的中心点
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static IPoint GetPolygonCenter(IPolygon polygon)
        {
            IArea area = polygon as IArea;
            return new PointClass
            {
                SpatialReference = polygon.SpatialReference,
                X = area.Centroid.X,
                Y = area.Centroid.Y
            };
        }


        #region 获取点集
        /// <summary>
        /// 获取构成几何图形的点集
        /// </summary>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public static List<IPoint> GetPointList(IGeometry geometry)
        {
            return GetPointList(geometry as IPointCollection);
        }
        /// <summary>
        /// 获取构成几何图形的点集
        /// </summary>
        /// <param name="pointCollection"></param>
        /// <returns></returns>
        public static List<IPoint> GetPointList(IPointCollection pointCollection)
        {
            List<IPoint> points = new List<IPoint>();
            for (int i = 0; i < pointCollection.PointCount; i++)
            {
                points.Add(pointCollection.get_Point(i));
            }
            return points;
        }
        /// <summary>
        /// 获取构成几何图形的点集
        /// </summary>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public static IPoint[] GetPointArray(IGeometry geometry)
        {
            return GetPointArray(geometry as IPointCollection);
        }
        /// <summary>
        /// 获取构成几何图形的点集
        /// </summary>
        /// <param name="pointCollection"></param>
        /// <returns></returns>
        public static IPoint[] GetPointArray(IPointCollection pointCollection)
        {
            int cnt = pointCollection.PointCount;
            IPoint[] points = new IPoint[cnt];
            for (int i = 0; i < cnt; i++)
            {
                points[i] = pointCollection.get_Point(i);
            }
            return points;
        }
        #endregion


        #region 平移图形
        /// <summary>
        /// 平移图形元素
        /// </summary>
        /// <param name="feature">要平移的对象</param>
        /// <param name="dx">在x方向上的平移量</param>
        /// <param name="dy">在y方向上的平移量</param>
        public static void MoveGeometry(IFeature feature, double dx, double dy)
        {
            ITransform2D transform2D = feature.Shape as ITransform2D;
            transform2D.Move(dx, dy);
        }
        /// <summary>
        /// 平移图形要素
        /// </summary>
        /// <param name="element">要平移的对象</param>
        /// <param name="dx">在x方向上的平移量</param>
        /// <param name="dy">在y方向上的平移量</param>
        public static void MoveGeometry(IElement element, double dx, double dy)
        {
            ITransform2D transform2D = element.Geometry as ITransform2D;
            transform2D.Move(dx, dy);
            element.Geometry = transform2D as IGeometry;//移动元素(Element)后，需再次将Geometry赋值给元素，而对要素(Feature)无需此操作
        }
        /// <summary>
        /// 平移图形
        /// </summary>
        /// <param name="geometry">要平移的对象</param>
        /// <param name="dx">在x方向上的平移量</param>
        /// <param name="dy">在y方向上的平移量</param>
        public static void MoveGeometry(IGeometry geometry, double dx, double dy)
        {
            ITransform2D transform2D = geometry as ITransform2D;
            transform2D.Move(dx, dy);
        }
        #endregion


        #region 几何构建
        /// <summary>
        /// 多边形是否由多部分组成（即是否多个外环）
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static bool IsMultiPart(IPolygon4 polygon)
        {
            IGeometryBag exteriorRingGeometryBag = polygon.ExteriorRingBag;
            IGeometryCollection exteriorRingGeometryCollection = exteriorRingGeometryBag as IGeometryCollection;
            return exteriorRingGeometryCollection.GeometryCount > 1;
        }
        /// <summary>
        /// 多部分(多外环)的多边形转成多个单部分的多边形
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static IPolygon[] MultiPartToSinglePart(IPolygon4 polygon)
        {
            List<IPolygon> polygons = new List<IPolygon>();

            //外部环
            IGeometryBag exteriorRingGeometryBag = polygon.ExteriorRingBag;
            IGeometryCollection exteriorRingGeometryCollection = exteriorRingGeometryBag as IGeometryCollection;

            for (int i = 0; i < exteriorRingGeometryCollection.GeometryCount; i++)
            {
                IGeometry exteriorRingGeometry = exteriorRingGeometryCollection.get_Geometry(i);
                IRing ring = exteriorRingGeometry as IRing;
                ring.Close();
                IGeometryCollection pGeometryColl = new PolygonClass();
                pGeometryColl.AddGeometry(ring);

                //内部环
                IGeometryBag interiorRingGeometryBag = polygon.get_InteriorRingBag(exteriorRingGeometry as IRing);
                IGeometryCollection interiorRingGeometryCollection = interiorRingGeometryBag as IGeometryCollection;
                for (int k = 0; k < interiorRingGeometryCollection.GeometryCount; k++)
                {
                    IGeometry interiorRingGeometry = interiorRingGeometryCollection.get_Geometry(k);
                    IRing ring2 = interiorRingGeometry as IRing;
                    ring2.Close();
                    pGeometryColl.AddGeometry(ring2);
                }

                ITopologicalOperator pTopological = pGeometryColl as ITopologicalOperator;
                pTopological.Simplify();
                IPolygon p = pGeometryColl as IPolygon;
                polygons.Add(p);
            }
            return polygons.ToArray();
        }
        /// <summary>
        /// 获取几何类型(esriGeometryType)的中文描述
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetGeometryTypeCnName(esriGeometryType type)
        {
            string typeName;
            switch (type)
            {
                case esriGeometryType.esriGeometryPoint: typeName = "点"; break;
                case esriGeometryType.esriGeometryPolyline: typeName = "折线"; break;
                case esriGeometryType.esriGeometryPolygon: typeName = "面"; break;
                case esriGeometryType.esriGeometryMultipoint: typeName = "多点"; break;
                default: typeName = type.ToString().Replace("esriGeometry", ""); break;
            }
            return typeName;
        }
        #endregion


        #region 合并图形（Union）
        /// <summary>
        /// 将多个图形合并(Union)成一个图形
        /// </summary>
        /// <param name="geoList"></param>
        /// <returns></returns>
        public static IGeometry UnionGeometry(IEnumerable<IGeometry> geoList)
        {
            IGeometry unionGeo = null;
            foreach (IGeometry geo in geoList)
            {
                if (unionGeo == null)
                    unionGeo = geo;
                else
                    unionGeo = ((ITopologicalOperator)unionGeo).Union(geo);
            }
            return unionGeo;
        }
        /// <summary>
        /// 将多个图形合并(Union)成一个图形（使用GeometryBag提高合并效率）
        /// </summary>
        /// <param name="featureClass">从中查询图形的要素类</param>
        /// <param name="whereCluase">查询条件</param>
        /// <returns></returns>
        public static IGeometry UnionGeometryEx(IFeatureClass featureClass, string whereCluase = null)
        {
            IGeometry geometryBag = new GeometryBagClass();
            geometryBag.SpatialReference = (featureClass as IGeoDataset).SpatialReference;
            IGeometryCollection geometryCollection = geometryBag as IGeometryCollection;

            FeatClassOpt.QueryFeatures(featureClass, whereCluase, f =>
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
        public static IGeometry UnionGeometryEx(IEnumerable<IGeometry> geometries)
        {
            IGeometry geometryBag = new GeometryBagClass();
            geometryBag.SpatialReference = geometries.First().SpatialReference;
            IGeometryCollection geometryCollection = geometryBag as IGeometryCollection;

            foreach (var geo in geometries)
            {
                object missing = Type.Missing;
                geometryCollection.AddGeometry(geo, ref missing, ref missing);
            }
            return UnionGeometryEx(geometryBag, geometries.First().GeometryType);
        }
        /// <summary>
        /// 将多个图形合并(Union)成一个图形（使用GeometryBag提高合并效率）
        /// </summary>
        /// <param name="geometryBag"></param>
        /// <param name="geometryType">几何类型</param>
        /// <returns></returns>
        private static IGeometry UnionGeometryEx(IGeometry geometryBag, esriGeometryType geometryType)
        {
            ITopologicalOperator unionedPolygon = new PolygonClass();
            switch (geometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    unionedPolygon = new PointClass();
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    unionedPolygon = new PolylineClass();
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    unionedPolygon = new PolygonClass();
                    break;
                default:
                    throw new NotImplementedException("几何类型(geometryType)应是点(point)、线(polyline)、多边形(polygon)之一，其他类型暂未实现！");
            }

            unionedPolygon.ConstructUnion(geometryBag as IEnumGeometry);
            return unionedPolygon as IGeometry;
        }
        #endregion
    }
}
