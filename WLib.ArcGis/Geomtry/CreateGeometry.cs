/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using ESRI.ArcGIS.Geometry;

namespace WLib.ArcGis.Geomtry
{
    /// <summary>
    /// 提供创建点、线、面等几何图形的方法
    /// </summary>
    public class CreateGeometry
    {
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


        /// <summary>
        /// 通过点集构成多边形
        /// </summary>
        /// <param name="PointList"></param>
        /// <returns></returns>
        public static IPolygon CreatePolygon(List<IPoint> PointList)
        {
            if (PointList.Count < 3)
                throw new Exception("地块点数小于3，不能构成多边形！");

            IGeometryCollection pointPolygon = new PolygonClass();
            Ring ring = new RingClass();
            object missing = Type.Missing;
            for (int i = 0; i < PointList.Count; i++)
            {
                ring.AddPoint(PointList[i], ref missing, ref missing);
            }
            pointPolygon.AddGeometry(ring as IGeometry, ref missing, ref missing);
            IPolygon polygon = pointPolygon as IPolygon;
            polygon.SimplifyPreserveFromTo();

            return polygon;
        }

    }
}
