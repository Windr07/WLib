/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using ESRI.ArcGIS.Geometry;

namespace WLib.ArcGis.Calculate
{
    /// <summary>
    /// 角度计算
    /// </summary>
    public class AngleCalculate
    {
        /// <summary>
        /// 获取已知三点构成的角的弧度
        /// </summary>
        /// <param name="center">中间点，即角的顶点</param>
        /// <param name="first">角的一个端点</param>
        /// <param name="second">角的另一个端点</param>
        /// <returns></returns>
        public static float GetRadian(IPoint center, IPoint first, IPoint second)
        {
            var dx1 = first.X - center.X;
            var dy1 = first.Y - center.Y;
            var dx2 = second.X - center.X;
            var dy2 = second.Y - center.Y;

            double c = Math.Sqrt(dx1 * dx1 + dy1 * dy1) * Math.Sqrt(dx2 * dx2 + dy2 * dy2);
            if (c == 0)
                return -1;

            return (float)Math.Acos((dx1 * dx2 + dy1 * dy2) / c);
        }

        /// <summary>
        ///  获取已知三点构成的角度
        /// </summary>
        /// <param name="center">中间点，即角的顶点</param>
        /// <param name="first">角的一个端点</param>
        /// <param name="second">角的另一个端点</param>
        /// <returns></returns>
        public static float GetAngle(IPoint center, IPoint first, IPoint second)
        {
            return GetRadian(center, first, second) * 180f / (float)Math.PI;
        }
    }
}
