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

namespace WLib.ArcGis.Geomtry
{
    /// <summary>
    /// 点线面等几何的操作
    /// </summary>
    public static class GeometryOpt
    {
        /// <summary>
        /// 判断两点是否重合
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static bool IsEqual(IPoint point1, IPoint point2)
        {
            return point1.X == point2.X && point1.Y == point2.Y;
        }
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
        public static IPoint GetPolygonCenter(this IPolygon polygon)
        {
            IArea area = (IArea)polygon;
            return new PointClass
            {
                SpatialReference = polygon.SpatialReference,
                X = area.Centroid.X,
                Y = area.Centroid.Y
            };
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


        #region 获取点集
        /// <summary>
        /// 获取构成几何图形的点集
        /// </summary>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public static List<IPoint> GetPointList(this IGeometry geometry)
        {
            return GetPointList(geometry as IPointCollection);
        }
        /// <summary>
        /// 获取构成几何图形的点集
        /// </summary>
        /// <param name="pointCollection"></param>
        /// <returns></returns>
        public static List<IPoint> GetPointList(this IPointCollection pointCollection)
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
        public static IPoint[] GetPointArray(this IGeometry geometry)
        {
            return GetPointArray(geometry as IPointCollection);
        }
        /// <summary>
        /// 获取构成几何图形的点集
        /// </summary>
        /// <param name="pointCollection"></param>
        /// <returns></returns>
        public static IPoint[] GetPointArray(this IPointCollection pointCollection)
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


        #region 多边形的多部分
        /// <summary>
        /// 多边形是否由多部分组成（即是否多个外环）
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static bool IsMultiPart(this IPolygon4 polygon)
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
        public static IPolygon[] MultiPartToSinglePart(this IPolygon4 polygon)
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
        #endregion


        #region 构造点
        /// <summary>
        /// 创建点集
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static IEnumerable<IPoint> CreatePoints(List<double[]> points)
        {
            return points.Select(v =>
            {
                IPoint pt = new PointClass { X = v[0], Y = v[1] };
                return pt;
            });
        }
        #endregion


        #region 构造线
        /// <summary>
        /// 通过起点坐标和终点坐标创建线段（IPolyline对象）
        /// </summary>
        /// <param name="x1">起点x坐标</param>
        /// <param name="y1">起点y坐标</param>
        /// <param name="x2">终点x坐标</param>
        /// <param name="y2">终点y坐标</param>
        /// <returns></returns>
        public static IPolyline CreatePolyline(double x1, double y1, double x2, double y2)
        {
            // 定义第一个点
            IPoint pt1 = new PointClass();
            pt1.X = x1;
            pt1.Y = y1;
            // 定义第二个点
            IPoint pt2 = new PointClass();
            pt2.X = x2;
            pt2.Y = y2;

            return CreatePolyline(pt1, pt2);
        }
        /// <summary>
        /// 通过起点和终点创建线段（IPolyline对象）
        /// </summary>
        /// <param name="pt1">起点</param>
        /// <param name="pt2">终点</param>
        /// <returns></returns>
        public static IPolyline CreatePolyline(IPoint pt1, IPoint pt2)
        {
            //a. 创建Line对象（也可是其他Segment对象），
            //b. QI到Segment对象
            //c. 创建Path对象，通过Path的addSegment，将最初的Line添加进Path中
            //d. 创建GeometryCollection对象，通过AddGeometry，将path添加进GeometryCollection中
            //e. 将GeometryCollection QI到 IPolyline


            ILine line = new LineClass();// 创建一个Line对象
            line.PutCoords(pt1, pt2);// 设置LIne对象的起始终止点
            ISegment segment = line as ISegment; // QI到ISegment
            ISegmentCollection path = new PathClass();// 创建一个Path对象

            object o = Type.Missing;
            path.AddSegment(segment, ref o, ref o);// 通过Isegmentcoletcion接口为Path对象添加Segment对象

            // 创建一个Polyline对象
            IGeometryCollection polyline = new PolylineClass();
            polyline.AddGeometry(path as IGeometry, ref o, ref o);

            IPolyline resultPolyline = polyline as IPolyline;
            return resultPolyline;
        }
        /// <summary>
        /// 通过点集创建线段（IPolyline对象）
        /// </summary>
        /// <param name="pts"></param>
        /// <returns></returns>
        public static IPolyline CreatePolyline(IPoint[] pts)
        {
            List<ISegment> segmentList = new List<ISegment>();//存放单条线的每一段(Line对象)
            for (int i = 0; i < pts.Length - 1; i++)
            {
                Line line = new LineClass();
                line.PutCoords(pts[i], pts[i + 1]);
                segmentList.Add((ISegment)line);
            }

            ISegment[] segmentArray = segmentList.ToArray();//存放单条线的每一段(Line对象)

            ISegmentCollection segmentCollection = new PolylineClass();
            IGeometryBridge geometryBridge = new GeometryEnvironmentClass();
            geometryBridge.AddSegments(segmentCollection, ref segmentArray);
            IPolyline polyLine = segmentCollection as IPolyline;
            return polyLine;
        }
        #endregion


        #region 构造面
        /// <summary>
        /// 通过点集构成多边形
        /// </summary>
        /// <param name="pointList"></param>
        /// <returns></returns>
        public static IPolygon CreatePolygon(List<IPoint> pointList)
        {
            if (pointList.Count < 3)
                throw new Exception("地块点数小于3，不能构成多边形！");

            IGeometryCollection pointPolygon = new PolygonClass();
            Ring ring = new RingClass();
            object missing = Type.Missing;
            for (int i = 0; i < pointList.Count; i++)
            {
                ring.AddPoint(pointList[i], ref missing, ref missing);
            }
            pointPolygon.AddGeometry(ring as IGeometry, ref missing, ref missing);
            IPolygon polygon = pointPolygon as IPolygon;
            polygon.SimplifyPreserveFromTo();

            return polygon;
        }
        #endregion
    }
}
