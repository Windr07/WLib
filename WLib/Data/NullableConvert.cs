/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Data
{
    /// <summary>
    /// 将object对象转成可空的值类型对象
    /// （注意object必须为DBNull.Value或对应的值类型）
    /// </summary>
    public class NullableConvert
    {
        /// <summary>
        /// 转成可空的double型对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double? NullableDouble(object value)
        {
            return Convert.IsDBNull(value) ? null : new double?(Convert.ToDouble(value));
        }
        /// <summary>
        /// 转成可空的double型对象，并指定保留几位小数
        /// </summary>
        /// <param name="value"></param>
        /// <param name="digits">小数位数</param>
        /// <returns></returns>
        public static double? NullableDouble(object value, int digits)
        {
            return Convert.IsDBNull(value) ? null : new double?(Math.Round(Convert.ToDouble(value), digits));
        }
        /// <summary>
        /// 转成可空的decimal型对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal? NullableDecimal(object value)
        {
            return Convert.IsDBNull(value) ? null : new decimal?(Convert.ToDecimal(value));
        }
        /// <summary>
        /// 转成可空的decimal型对象，并指定保留几位小数
        /// </summary>
        /// <param name="value"></param>
        /// <param name="digits">小数位数</param>
        /// <returns></returns>
        public static decimal? NullableDecimal(object value, int digits)
        {
            return Convert.IsDBNull(value) ? null : new decimal?(Math.Round(Convert.ToDecimal(value), digits));
        }
        /// <summary>
        /// 转成可空的float型对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float? NullableSingle(object value)
        {
            return Convert.IsDBNull(value) ? null : new float?(Convert.ToSingle(value));
        }
        /// <summary>
        /// 转成可空的int型对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int? NullableInt32(object value)
        {
            return Convert.IsDBNull(value) ? null : new int?(Convert.ToInt32(value));
        }
        /// <summary>
        /// 转成可空的bool型对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool? NullableBoolean(object value)
        {
            return Convert.IsDBNull(value) ? null : new bool?(Convert.ToBoolean(value));
        }
        /// <summary>
        /// 转成可空的DateTime型对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime? NullableDateTime(object value)
        {
            return Convert.IsDBNull(value) ? null : new DateTime?(Convert.ToDateTime(value));
        }
    }
}
