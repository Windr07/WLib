/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/05/06 19:02
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using WLib.Model;

namespace WLib.Data.Calculate
{
    /// <summary>
    /// 回归分析
    /// </summary>
    public static class RegressionAnalysis
    {
        /// <summary>
        /// 根据统计样点计算一元线性回归方程 y = kx + d 的参数k和d
        /// （公式参考 https://baike.baidu.com/item/一元线性回归方程/6953911）
        /// </summary>
        /// <param name="points">一元线性回归的统计样点</param>
        /// <param name="k">回归方程 y = kx + d 的斜率参数</param>
        /// <param name="d">回归方程 y = kx + d 的截距参数</param>
        public static void UnaryLinearRegression(PointD[] points, out double k, out double d)
        {
            double n = points.Length;
            double ySum = points.Sum(pt => pt.Y);
            double xSum = points.Sum(pt => pt.X);
            double xSquare = points.Sum(pt => pt.X * pt.X);
            double xySum = points.Sum(pt => pt.X * pt.Y);
            k = (n * xySum - ySum * xSum) / (n * xSquare - xSum * xSum);

            double xAvg = xSum / n;
            double yAvg = ySum / n;
            d = yAvg - k * xAvg;
        }
        /// <summary>
        /// 根据统计样点计算一元线性回归方程 y = kx + d 的参数k和d
        /// （公式参考 https://baike.baidu.com/item/一元线性回归方程/6953911）
        /// </summary>
        /// <param name="points">一元线性回归的统计样点</param>
        /// <param name="k">回归方程 y = kx + d 的斜率参数</param>
        /// <param name="d">回归方程 y = kx + d 的截距参数</param>
        public static void UnaryLinearRegression(IEnumerable<double[]> points, out double k, out double d)
        {
            double n = points.Count();
            double ySum = points.Sum(pt => pt[1]);
            double xSum = points.Sum(pt => pt[0]);
            double xSquare = points.Sum(pt => pt[0] * pt[0]);
            double xySum = points.Sum(pt => pt[0] * pt[1]);
            k = (n * xySum - ySum * xSum) / (n * xSquare - xSum * xSum);

            double xAvg = xSum / n;
            double yAvg = ySum / n;
            d = yAvg - k * xAvg;
        }
    }
}
