/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： WKT(Well-known text) https://en.wikipedia.org/wiki/Well-known_text
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WLib.Gdal
{
    //点、线、面等各类几何图形的WKT字符串示例：
    //POINT(6 10)
    //LINESTRING(3 4,10 50,20 25)
    //POLYGON((1 1,5 1,5 5,1 5,1 1),(2 2,2 3,3 3,3 2,2 2))
    //MULTIPOINT(3.5 5.6, 4.8 10.5)
    //MULTILINESTRING((3 4,10 50,20 25),(-5 -8,-10 -8,-15 -4))
    //MULTIPOLYGON(((1 1,5 1,5 5,1 5,1 1),(2 2,2 3,3 3,3 2,2 2)),((6 3,9 2,9 4,6 3)))
    //GEOMETRYCOLLECTION(POINT(4 6),LINESTRING(4 6,7 10))
    //POINT ZM(1 1 5 60)
    //POINT M(1 1 80)
    //POINT EMPTY
    //MULTIPOLYGON EMPTY

    /// <summary>
    /// 提供GDAL多边形的环和环方向的相关操作
    /// </summary>
    public static class GdalRingHelper
    {
        /// <summary>
        /// 判断环的是否为顺时针方向
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static bool IsRingClockwise(List<WktPoint> points)
        {
            double t = 0;
            for (var i = 1; i < points.Count; i++)
            {
                t += points[i - 1].X * points[i].Y - points[i].X * points[i - 1].Y;
            }

            t += points[points.Count - 1].X * points[0].Y - points[0].X * points[points.Count - 1].Y;
            return t < 0;
        }
        /// <summary>
        /// 判断环是否闭合（即最后一点与第一点重合）
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static bool IsRingClosed(List<WktPoint> points)
        {
            var t = points.Count - 1;
            return (t > 2 && points[0].X == points[t].X && points[0].Y == points[t].Y);
        }
        /// <summary>
        /// 根据输入的图斑WKT字符串判断图斑的环方向，纠正为SqlServer要求的环方向并返回新WKT字符串
        /// </summary>
        /// <param name="wktGeometry"></param>
        /// <returns></returns>
        public static string ModifyGeometryDirection(string wktGeometry)
        {
            if (Regex.IsMatch(wktGeometry, "MULTIPOLYGON"))
            {
                var matches = Regex.Matches(wktGeometry, @"\(\([^(].+?\)\)");
                if (matches.Count > 0)
                {
                    var resultGeometry = new StringBuilder();
                    for (var i = 0; i < matches.Count; i++)
                    {
                        resultGeometry.AppendFormat("({0})", ModifyPolygonDirection(matches[i].ToString()));
                        if (i < matches.Count - 1)
                            resultGeometry.Append(",");
                    }
                    return string.Format("MULTIPOLYGON({0})", resultGeometry);
                }
            }
            else if (Regex.IsMatch(wktGeometry, "POLYGON"))
            {
                return string.Format("POLYGON({0})", ModifyPolygonDirection(wktGeometry));
            }

            return wktGeometry;
        }
        /// <summary>
        /// 根据输入的多边形WKT字符串判断多边形环方向，纠正为SqlServer要求的环方向并返回新WKT字符串
        /// </summary>
        /// <param name="wktPolygon">多边形的WKT字符串</param>
        /// <returns></returns>
        private static string ModifyPolygonDirection(string wktPolygon)
        {
            //按SqlServer的Geography规则，多边形外环应该逆时针，内环顺时针
            var matches = Regex.Matches(wktPolygon, @"\([^()]+\)");
            if (matches.Count > 0)
            {
                var sbResultRing = new StringBuilder();
                for (var i = 0; i < matches.Count; i++)
                {
                    var strRing = matches[i].ToString().TrimStart('(').TrimEnd(')');
                    var resultRing = ModifyRingDirection(strRing, i == 0);//POLYGON(())中第一个()中的是外环，其它的是内环

                    sbResultRing.AppendFormat("({0})",resultRing);
                    if (i < matches.Count - 1)
                        sbResultRing.Append(",");
                }
                wktPolygon = sbResultRing.ToString();
            }
            return wktPolygon;
        }
        /// <summary>
        /// 判断环方向，并根据内外环要求，纠正为SqlServer要求的环方向并返回新WKT字符串
        /// </summary>
        /// <param name="wktRing">环的WKT字符串</param>
        /// <param name="isOutRing">是否为外环</param>
        /// <returns></returns>
        private static string ModifyRingDirection(string wktRing, bool isOutRing)
        {
            var strPoints = wktRing.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var points = strPoints.Select(p => WktPoint.FormWkt(p)).ToList();

            if (!IsRingClosed(points))
                throw new Exception("多边形的环没有闭合");

            string resultRing = wktRing;
            if (isOutRing && IsRingClockwise(points)) //外环，如果是顺时针则逆转
                resultRing = string.Join(",", points.Select(v => v.ToString()).Reverse().ToArray());

            else if (!isOutRing && !IsRingClockwise(points)) //内环，如果是逆时针则逆转
                resultRing = string.Join(",", points.Select(v => v.ToString()).Reverse().ToArray());

            return resultRing;
        }
    }
}
