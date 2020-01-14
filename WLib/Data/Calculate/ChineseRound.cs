/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/08/22
// desc： 参考：https://www.cnblogs.com/renzhiwei/p/4229384.html
          参考：https://mp.weixin.qq.com/s?__biz=MzAxOTc0NzExNg==&mid=2665513140&idx=1&sn=565517e977ac56904305a4a9f9d65012#rd 
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Data.Calculate
{
    /// <summary>
    /// 提供对数值四舍五入保留指定小数位数的方法
    /// </summary>
    public static class ChineseRound
    {
        /// <summary>
        /// 使用四舍五入方式保留指定小数位数
        /// <para>浮点型在某些数值的表示上是不精确的，在高精度计算时，应使用decimal类型替换double和float </para>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static double ToChineseRound(this double value, int precision)
        {
            value = Convert.ToDouble(value.ToString());
            int tmpNum = value > 0 ? 5 : -5;
            return Math.Truncate((Math.Truncate(value * Math.Pow(10, precision + 1)) + tmpNum) / 10) / Math.Pow(10, precision);
        }
    }
}
