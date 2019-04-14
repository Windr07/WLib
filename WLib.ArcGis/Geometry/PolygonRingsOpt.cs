/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017/5/2 11:49:10
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using ESRI.ArcGIS.Geometry;

namespace WLib.ArcGis.Geometry
{
    //Polylgon对象是由一个或多个Ring对象的有序集合，
    //其中的Ring可以分为Outer Ring(外环)和Inner Ring(内环)之分，内环通常代表多边形内挖空的部分，
    //外环的方向是顺时针（即外环的点集按顺时针排序），内环的方向是逆时针（即内环的点集按逆时针排序）

    /// <summary>
    /// 多边形与内外环、点集之间的转换
    /// </summary>
    public static class PolygonRingsOpt
    {
        /// <summary>
        /// 获取多边形的外环
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static List<IRing> GetExteriorRings(this IPolygon4 polygon)
        {
            List<IRing> rings = new List<IRing>();
            IGeometryBag exteriorRingGeometryBag = polygon.ExteriorRingBag;//全部外部环
            IGeometryCollection exteriorRingGeometryCollection = (IGeometryCollection)exteriorRingGeometryBag;
            for (int i = 0; i < exteriorRingGeometryCollection.GeometryCount; i++)
            {
                IGeometry exteriorRingGeometry = exteriorRingGeometryCollection.get_Geometry(i);
                rings.Add(exteriorRingGeometry as IRing);
            }
            return rings;
        }
        /// <summary>
        /// 获取多边形指定外环所包含的内环
        /// </summary>
        /// <param name="polygon">多边形</param>
        /// <param name="exteriorRing">外部环，此外部环必须是指定多边形的</param>
        /// <returns></returns>
        public static List<IRing> GetInteriorRings(this IPolygon4 polygon, IRing exteriorRing)
        {
            List<IRing> rings = new List<IRing>();
            IGeometryBag interiorRingGeometryBag = polygon.get_InteriorRingBag(exteriorRing);
            IGeometryCollection interiorRingGeometryCollection = (IGeometryCollection)interiorRingGeometryBag;
            for (int k = 0; k < interiorRingGeometryCollection.GeometryCount; k++)
            {
                IGeometry interiorRingGeometry = interiorRingGeometryCollection.get_Geometry(k);
                rings.Add(interiorRingGeometry as IRing);
            }
            return rings;
        }
        /// <summary>
        /// 获取多边形指定外环所包含的内环的点集
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="exteriorRing"></param>
        /// <returns></returns>
        public static List<List<IPoint>> GetInteriorRingPoints(this IPolygon4 polygon, IRing exteriorRing)
        {
            List<List<IPoint>> rings = new List<List<IPoint>>();
            IGeometryBag interiorRingGeometryBag = polygon.get_InteriorRingBag(exteriorRing);
            IGeometryCollection interiorRingGeometryCollection = (IGeometryCollection)interiorRingGeometryBag;
            for (int k = 0; k < interiorRingGeometryCollection.GeometryCount; k++)
            {
                IGeometry interiorRingGeometry = interiorRingGeometryCollection.get_Geometry(k);
                var interiorPoints = (interiorRingGeometry as IPointCollection).GetPointList();
                rings.Add(interiorPoints);
            }
            return rings;
        }


        /// <summary>
        /// 获取构成多边形的所有环（包括外环和内环）
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static List<IRing> GetRings(this IPolygon4 polygon)
        {
            List<IRing> rings = new List<IRing>();
            IGeometryBag exteriorRingGeometryBag = polygon.ExteriorRingBag;//全部外部环
            IGeometryCollection exteriorRingGeometryCollection = (IGeometryCollection)exteriorRingGeometryBag;
            for (int i = 0; i < exteriorRingGeometryCollection.GeometryCount; i++)
            {
                IGeometry exteriorRingGeometry = exteriorRingGeometryCollection.get_Geometry(i);
                rings.Add(exteriorRingGeometry as IRing);//外部环
                rings.AddRange(GetInteriorRings(polygon, exteriorRingGeometry as IRing));//内部环
            }
            return rings;
        }
        /// <summary>
        /// 获取构成多边形的所有环的点集（包括外环和内环）
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static List<List<IPoint>> GetRingPoints(this IPolygon4 polygon)
        {
            List<List<IPoint>> rings = new List<List<IPoint>>();
            //外部环
            IGeometryBag exteriorRingGeometryBag = polygon.ExteriorRingBag;//全部外部环
            IGeometryCollection exteriorRingGeometryCollection = (IGeometryCollection)exteriorRingGeometryBag;
            for (int i = 0; i < exteriorRingGeometryCollection.GeometryCount; i++)
            {
                IGeometry exteriorRingGeometry = exteriorRingGeometryCollection.get_Geometry(i);
                IPointCollection exteriorRingPointCollection = exteriorRingGeometry as IPointCollection;
                var exteriorPoints = exteriorRingPointCollection.GetPointList();
                rings.Add(exteriorPoints);//外部环
                rings.AddRange(GetInteriorRingPoints(polygon, exteriorRingGeometry as IRing));//内部环
            }
            return rings;
        }
        /// <summary>
        /// 获取构成多边形的所有环的点集（包括外环和内环），返回的环内的点集统一按顺时针或逆时针排序
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="clockwise">时针方向，true为顺时针，false为逆时针</param>
        /// <returns></returns>
        public static List<List<IPoint>> GetRingPointsByClockwise(this IPolygon4 polygon, bool clockwise)
        {
            List<List<IPoint>> rings = new List<List<IPoint>>();
            //外部环
            IGeometryBag exteriorRingGeometryBag = polygon.ExteriorRingBag;//全部外部环
            IGeometryCollection exteriorRingGeometryCollection = (IGeometryCollection)exteriorRingGeometryBag;
            for (int i = 0; i < exteriorRingGeometryCollection.GeometryCount; i++)
            {
                IGeometry exteriorRingGeometry = exteriorRingGeometryCollection.get_Geometry(i);
                IPointCollection exteriorRingPointCollection = exteriorRingGeometry as IPointCollection;
                var exteriorPoints = exteriorRingPointCollection.GetPointList();
                if (!clockwise)//外环的点是顺时针排序的，要求逆时针排序时反序List
                    exteriorPoints.Reverse();
                rings.Add(exteriorPoints);//外部环

                var interiorRings = GetInteriorRingPoints(polygon, exteriorRingGeometry as IRing);//内部环
                if (clockwise)//内环的点是逆时针排序的，要求顺时针排序时反序List
                {
                    foreach (var ring in interiorRings)
                    {
                        ring.Reverse();
                    }
                }
                rings.AddRange(interiorRings);
            }
            return rings;
        }
        /// <summary>
        /// 获取构成环的所有点集
        /// </summary>
        /// <param name="ring"></param>
        /// <returns></returns>
        public static List<IPoint> GetRingPoints(this IRing ring)
        {
            IPointCollection pointCollection = ring as IPointCollection;
            return pointCollection.GetPointList();
        }


        /// <summary>
        /// 由环构成多边形
        /// </summary>
        /// <param name="rings"></param>
        /// <returns></returns>
        public static IPolygon CreatePolygon(IEnumerable<IRing> rings)
        {
            IPolygon polygon = new PolygonClass();
            IGeometryCollection geometryColl = (IGeometryCollection)polygon;
            foreach (var ring in rings)
            {
                geometryColl.AddGeometry(ring);
            }
            return polygon;
        }
        /// <summary>
        /// 由点集构成环
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static IRing CreateRing(IEnumerable<IPoint> points)
        {
            IRing ring = new RingClass();
            IPointCollection pointColl = (IPointCollection)ring;
            foreach (var point in points)
            {
                pointColl.AddPoint(point);
            }
            return ring;
        }
        /// <summary>
        /// 由环的点集构成多边形
        /// </summary>
        /// <param name="pointRings"></param>
        /// <returns></returns>
        public static IPolygon CreatePolygon(IEnumerable<IEnumerable<IPoint>> pointRings)
        {
            List<IRing> rings = new List<IRing>();
            foreach (var ptRing in pointRings)
            {
                rings.Add(CreateRing(ptRing));
            }
            return CreatePolygon(rings);
        }
    }
}
