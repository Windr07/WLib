/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/05/06 19:02
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Model
{
    /// <summary>
    /// 表示笛卡尔坐标系的一个点
    /// </summary>
    [Serializable]
    public class DcrPoint
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
        /// 表示笛卡尔坐标系的一个点
        /// </summary>
        public DcrPoint()
        {
        }
        /// <summary>
        /// 表示笛卡尔坐标系的一个点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public DcrPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
