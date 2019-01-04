/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Geometry;

namespace WLib.ArcGis.Geomtry
{
    public class GeometryCompare
    {
        /// <summary>
        /// 判断两点是否重合
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        public static bool IsEqual(IPoint pt1, IPoint pt2)
        {
            return pt1.X == pt2.X && pt1.Y == pt2.Y;
        }
    }
}
