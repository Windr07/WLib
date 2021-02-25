/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020/9
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Model
{
    /// <summary>
    /// 表示二维平面中的点，提供双精度浮点 x 和 y 坐标的有序对
    /// <para>由于<see cref="System.Drawing.Point"/>和<see cref="System.Drawing.PointF"/>的X和Y属性为
    /// <see cref="int"/>或<see cref="float"/>类型而非<see cref="double"/>类型，因此编写此类代替之</para>
    /// </summary>
    public struct PointD
    {
        /// <summary>
        /// X坐标值
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Y坐标值
        /// </summary>
        public double Y { get; set; }


        /// <summary>
        /// 表示二维平面中的点，提供双精度浮点 x 和 y 坐标的有序对
        /// </summary>
        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// 表示二维平面中的点，提供双精度浮点 x 和 y 坐标的有序对
        /// </summary>
        public PointD(float x, float y) : this((double)x, (double)y) { }
        /// <summary>
        /// 表示二维平面中的点，提供双精度浮点 x 和 y 坐标的有序对
        /// </summary>
        public PointD(int x, int y) : this((double)x, (double)y) { }
        /// <summary>
        /// 表示二维平面中的点，提供双精度浮点 x 和 y 坐标的有序对
        /// </summary>
        public PointD(System.Drawing.Point pt) : this(pt.X, pt.Y) { }
        /// <summary>
        /// 表示二维平面中的点，提供双精度浮点 x 和 y 坐标的有序对
        /// </summary>
        public PointD(System.Drawing.PointF pt) : this(pt.X, pt.Y) { }
        /// <summary>
        /// 表示二维平面中的点，提供双精度浮点 x 和 y 坐标的有序对
        /// </summary>
        public PointD(System.Drawing.Size size) : this(size.Width, size.Height) { }
    }
}
