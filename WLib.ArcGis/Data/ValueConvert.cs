/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using ESRI.ArcGIS.Geodatabase;

namespace WLib.ArcGis.Data
{
    /// <summary>
    /// 将要素或表格行的字段值，转换成指定类型数据
    /// </summary>
    public static class ValueConvert
    {
        /// <summary>
        /// 图层
        /// </summary>
        private static string STR_LYR = "图层";
        /// <summary>
        /// 表格
        /// </summary>
        private static string STR_TBL = "表格";
        /// <summary>
        /// "【{0}】{1}，OID={2}的行，【{3}】没有填写，请先完善数据！"
        /// </summary>
        private static string STR_NULL = "【{0}】{1}，OID={2}的行，【{3}】没有填写，请先完善数据！";
        /// <summary>
        /// "【{0}】{1}，OID={2}的行，【{3}】没有填写，或者填写了空格，请先完善数据！"
        /// </summary>
        private static string STR_NULL_EMPTY = "【{0}】{1}，OID={2}的行，【{3}】没有填写，或者填写了空格，请先完善数据！";
        /// <summary>
        /// "【{0}】{1}，OID={2}的行，【{3}】的字段值“{4}”无法转换成{5}，请检查数据！"
        /// </summary>
        private static string STR_CONVERT = "【{0}】{1}，OID={2}的行，【{3}】的字段值“{4}”无法转换成{5}，请检查数据！";


        #region 要素
        /// <summary>
        /// 若字段值为DBNull，抛出异常；否则返回字段值
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static object ToNotDBNull(this IFeature feature, string fieldName)
        {
            object value = feature.get_Value(feature.Fields.FindField(fieldName));
            if (value == DBNull.Value)
                throw new Exception(string.Format(STR_NULL, feature.Class.AliasName, STR_LYR, feature.OID, fieldName));
            return value;
        }
        /// <summary>
        /// 若字段值为DBNull，空或仅包含空白字符，抛出异常；否则返回去除空白字符的值
        /// </summary>
        /// <returns></returns>
        public static string ToStringNotWhiteSpace(this IFeature feature, string fieldName)
        {
            object value = feature.get_Value(feature.Fields.FindField(fieldName));
            if (value == DBNull.Value || value.ToString().Trim() == string.Empty)
                throw new Exception(string.Format(STR_NULL_EMPTY, feature.Class.AliasName, STR_LYR, feature.OID, fieldName));
            return value.ToString().Trim();
        }
        /// <summary>
        /// 指定字段值转为去除前后空白的字符串，当值为DBNull或Empty或空白字符时输出replaceStr
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="fieldName"></param>
        /// <param name="replaceStr">当值为DBNull或Empty或空白字符时输出此字符串，可以为null或Empty等</param>
        /// <returns></returns>
        public static string ToStringTrim(this IFeature feature, string fieldName, string replaceStr = "")
        {
            string value = feature.get_Value(feature.Fields.FindField(fieldName)).ToString().Trim();
            if (value == string.Empty)
                value = replaceStr;
            return value;
        }
        /// <summary>
        /// 指定字段值转为double，转换失败则抛出具体提示信息的异常
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static double ToDouble(this IFeature feature, string fieldName)
        {
            object value = ToNotDBNull(feature, fieldName);
            if (!double.TryParse(value.ToString().Trim(), out var result))
                throw new Exception(string.Format(STR_CONVERT, feature.Class.AliasName, STR_LYR, feature.OID, fieldName, value.ToString(), "双精度数值"));
            return result;
        }
        /// <summary>
        /// 指定字段值转为double，转换失败则抛出具体提示信息的异常
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static float ToSingle(this IFeature feature, string fieldName)
        {
            object value = ToNotDBNull(feature, fieldName);
            if (!float.TryParse(value.ToString().Trim(), out var result))
                throw new Exception(string.Format(STR_CONVERT, feature.Class.AliasName, STR_LYR, feature.OID, fieldName, value.ToString(), "单精度数值"));
            return result;
        }
        /// <summary>
        /// 指定字段值转为decimal，转换失败则抛出具体提示信息的异常
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this IFeature feature, string fieldName)
        {
            object value = ToNotDBNull(feature, fieldName);
            if (!decimal.TryParse(value.ToString().Trim(), out var result))
                throw new Exception(string.Format(STR_CONVERT, feature.Class.AliasName, STR_LYR, feature.OID, fieldName, value.ToString(), "小数值"));
            return result;
        }
        /// <summary>
        /// 指定字段值转为int，转换失败则抛出具体提示信息的异常
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static int ToInt32(this IFeature feature, string fieldName)
        {
            object value = ToNotDBNull(feature, fieldName);
            if (!int.TryParse(value.ToString().Trim(), out var result))
                throw new Exception(string.Format(STR_CONVERT, feature.Class.AliasName, STR_LYR, feature.OID, fieldName, value.ToString(), "整型数值"));
            return result;
        }
        /// <summary>
        /// 指定字段值转为long，转换失败则抛出具体提示信息的异常
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static long ToInt64(this IFeature feature, string fieldName)
        {
            object value = ToNotDBNull(feature, fieldName);
            if (!long.TryParse(value.ToString().Trim(), out var result))
                throw new Exception(string.Format(STR_CONVERT, feature.Class.AliasName, STR_LYR, feature.OID, fieldName, value.ToString(), "长整型数值"));
            return result;
        }
        /// <summary>
        /// 指定字段值转为DateTime，转换失败则抛出具体提示信息的异常
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this IFeature feature, string fieldName)
        {
            string value = ToStringNotWhiteSpace(feature, fieldName);
            if (!DateTime.TryParse(value.Trim(), out var result))
                throw new Exception(string.Format(STR_CONVERT, feature.Class.AliasName, STR_LYR, feature.OID, fieldName, value.ToString(), "日期"));
            return result;
        }
        #endregion


        #region 表格
        /// <summary>
        /// 若字段值为DBNull，抛出异常；否则返回字段值
        /// </summary>
        /// <param name="r"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static object ToNotDBNull(this IRow r, string fieldName)
        {
            object value = r.get_Value(r.Fields.FindField(fieldName));
            if (value == DBNull.Value)
                throw new Exception(string.Format(STR_NULL, (r.Table as IObjectClass)?.AliasName, STR_TBL, r.OID, fieldName));
            return value;
        }
        /// <summary>
        /// 若字段值为DBNull，空或仅包含空白字符，抛出异常；否则返回去除空白字符的值
        /// </summary>
        /// <returns></returns>
        public static string ToStringNotWhiteSpace(this IRow r, string fieldName)
        {
            object value = r.get_Value(r.Fields.FindField(fieldName));
            if (value == DBNull.Value || value.ToString().Trim() == string.Empty)
                throw new Exception(string.Format(STR_NULL_EMPTY, (r.Table as IObjectClass)?.AliasName, STR_TBL, r.OID, fieldName));
            return value.ToString().Trim();
        }
        /// <summary>
        /// 指定字段值转为去除前后空白的字符串，当值为null或Empty或空白字符时输出replaceStr
        /// </summary>
        /// <param name="r"></param>
        /// <param name="fieldName"></param>
        /// <param name="replaceStr">当值为null或Empty或空白字符时输出此字符串，可以为null或Empty等</param>
        /// <returns></returns>
        public static string ToStringTrim(this IRow r, string fieldName, string replaceStr = "")
        {
            string value = r.get_Value(r.Fields.FindField(fieldName)).ToString().Trim();
            if (string.IsNullOrEmpty(value))
                value = replaceStr;
            return value;
        }
        /// <summary>
        /// 指定字段值转为double，转换失败则抛出具体提示信息的异常
        /// </summary>
        /// <param name="r"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static double ToDouble(this IRow r, string fieldName)
        {
            object value = ToNotDBNull(r, fieldName);
            if (!double.TryParse(value.ToString().Trim(), out var result))
                throw new Exception(string.Format(STR_CONVERT, (r.Table as IObjectClass)?.AliasName, STR_TBL, r.OID, fieldName, value.ToString(), "双精度数值"));
            return result;
        }
        /// <summary>
        /// 指定字段值转为double，转换失败则抛出具体提示信息的异常
        /// </summary>
        /// <param name="r"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static float ToSingle(this IRow r, string fieldName)
        {
            object value = ToNotDBNull(r, fieldName);
            if (!float.TryParse(value.ToString().Trim(), out var result))
                throw new Exception(string.Format(STR_CONVERT, (r.Table as IObjectClass)?.AliasName, STR_TBL, r.OID, fieldName, value.ToString(), "单精度数值"));
            return result;
        }
        /// <summary>
        /// 指定字段值转为decimal，转换失败则抛出具体提示信息的异常
        /// </summary>
        /// <param name="r"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this IRow r, string fieldName)
        {
            object value = ToNotDBNull(r, fieldName);
            if (!decimal.TryParse(value.ToString().Trim(), out var result))
                throw new Exception(string.Format(STR_CONVERT, (r.Table as IObjectClass)?.AliasName, STR_TBL, r.OID, fieldName, value.ToString(), "小数值"));
            return result;
        }
        /// <summary>
        /// 指定字段值转为int，转换失败则抛出具体提示信息的异常
        /// </summary>
        /// <param name="r"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static int ToInt32(this IRow r, string fieldName)
        {
            object value = ToNotDBNull(r, fieldName);
            if (!int.TryParse(value.ToString().Trim(), out var result))
                throw new Exception(string.Format(STR_CONVERT, (r.Table as IObjectClass)?.AliasName, STR_TBL, r.OID, fieldName, value.ToString(), "整型数值"));
            return result;
        }
        /// <summary>
        /// 指定字段值转为long，转换失败则抛出具体提示信息的异常
        /// </summary>
        /// <param name="r"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static long ToInt64(this IRow r, string fieldName)
        {
            object value = ToNotDBNull(r, fieldName);
            if (!long.TryParse(value.ToString().Trim(), out var result))
                throw new Exception(string.Format(STR_CONVERT, (r.Table as IObjectClass)?.AliasName, STR_TBL, r.OID, fieldName, value.ToString(), "长整型数值"));
            return result;
        }
        /// <summary>
        /// 指定字段值转为DateTime，转换失败则抛出具体提示信息的异常
        /// </summary>
        /// <param name="r"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this IRow r, string fieldName)
        {
            string value = ToStringNotWhiteSpace(r, fieldName);
            if (!DateTime.TryParse(value.ToString().Trim(), out var result))
                throw new Exception(string.Format(STR_CONVERT, (r.Table as IObjectClass)?.AliasName, STR_TBL, r.OID, fieldName, value.ToString(), "日期"));
            return result;
        }
        #endregion
    }
}
