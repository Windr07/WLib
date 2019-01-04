/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Format
{
    /// <summary>
    /// 提供对日期进行格式化输出的方法
    /// </summary>
    public static class DateTimeFormat
    {
        /// <summary>
        /// 分别在"yyyy年"、"M月"、"d日"的左侧用空白字符填充以达到指定的总长度，
        /// 最后整合并返回整个日期字符串
        /// </summary>
        /// <param name="dateTime">需要格式化输出的日期</param>
        /// <param name="totalWidth">填充年份或月份或天数的总长度</param>
        /// <returns></returns>
        public static string PadLeftWhiteSpace(this DateTime dateTime, int totalWidth)
        {
            string strDay = dateTime.Day.ToString().PadLeft(totalWidth);
            string strMonth = dateTime.Month.ToString().PadLeft(totalWidth);
            string strYear = dateTime.Year.ToString().PadLeft(totalWidth);

            return $"{strYear}年{strMonth}月{strDay}日";
        }
    }
}
