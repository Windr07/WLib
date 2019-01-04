/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Gdal
{
    /// <summary>
    /// 表示一个点，用于解析或转换WKT字符串
    /// </summary>
    public class WktPoint
    {
        /// <summary>
        /// X坐标/横坐标
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Y坐标/纵坐标
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// 表示一个点，用于解析或转换WKT字符串
        /// </summary>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        public WktPoint(double x, double y)
        {
            X = x;
            Y = y;
        }


        /// <summary>
        /// 解析WKT字符串转成点对象
        /// </summary>
        /// <param name="wktPoint"></param>
        /// <returns></returns>
        public static WktPoint FormWkt(string wktPoint)
        {
            var xyPair = wktPoint.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            return xyPair.Length == 2 ? new WktPoint(Convert.ToDouble(xyPair[0]), Convert.ToDouble(xyPair[1])) : null;
        }
        /// <summary>
        /// 输出点对象的WKT字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{X:F8} {Y:F8}";
        }
    }
}
