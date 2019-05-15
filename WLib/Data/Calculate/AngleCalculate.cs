/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Drawing;

namespace WLib.Data.Calculate
{
    /// <summary>
    /// 提供角度的相关操作
    /// </summary>
    public class AngleCalculate
    {
        /// <summary>
        ///获取已知三点构成的角的弧度
        /// </summary>
        /// <param name="center">中间点，即角的顶点</param>
        /// <param name="first">角的一个端点</param>
        /// <param name="second">角的另一个端点</param>
        /// <returns></returns>
        public static float GetRadian(Point center, Point first, Point second)
        {
            float dx1 = first.X - center.X;
            float dy1 = first.Y - center.Y;
            float dx2 = second.X - center.X;
            float dy2 = second.Y - center.Y;

            float c = (float)Math.Sqrt(dx1 * dx1 + dy1 * dy1) * (float)Math.Sqrt(dx2 * dx2 + dy2 * dy2);
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
        public static float GetAngle(Point center, Point first, Point second)
        {
            return GetRadian(center, first, second) * 180f / (float)Math.PI;
        }
    }
}
