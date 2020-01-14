/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017/5/11 15:35:29
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WLib.Data.Format
{
    /// <summary>
    /// 字符串的相关操作
    /// </summary>
    public static class StringUtility
    {
        /// <summary>
        /// 将一个中文字符长度当成2，获取字符串的长度
        /// </summary>
        /// <returns></returns>
        public static int GetLengthEx(this string str)
        {
            int length = 0;
            for (int i = 1; i < str.Length; i++)
                length += IsChinese(str[i]) || IsChinesePunctuation(str[i]) ? 2 : 1;

            return length;
        }

        /// <summary>
        /// 将字符串按固定显示长度换行（英文和数字字符长度为1，中文字符与中文符号长度为2）
        /// </summary>
        /// <param name="subjectString">需要按固定长度换行的字符串</param>
        /// <param name="lineLength">每一行的长度，英文和数字字符长度为1，中文字符与中文符号长度为2</param>
        /// <returns></returns>
        public static string BreakStringToLines(string subjectString, int lineLength)
        {
            if (lineLength < 2)
                throw new Exception("字符串按固定长度换行时，指定的长度（参数：lineLength）不能小于2！");

            StringBuilder sb = new StringBuilder(subjectString);
            int offset = 0;
            List<int> indexList = BuildInsertIndexList(subjectString, lineLength);
            foreach (var index in indexList)
            {
                sb.Insert(index + offset, '\n');
                offset++;
            }
            return sb.ToString();
        }
        /// <summary>
        /// 将字符串按固定显示长度分组（英文和数字字符长度为1，中文字符与中文符号长度为2）
        /// </summary>
        /// <param name="subjectString">需要按固定长度分组的字符串</param>
        /// <param name="lineLength">每一组的长度，英文和数字字符长度为1，中文字符与中文符号长度为2</param>
        /// <returns></returns>
        public static List<string> BreakString(string subjectString, int lineLength)
        {
            if (lineLength < 2)
                throw new Exception("字符串按固定长度分组时，指定的长度（参数：lineLength）不能小于2！");

            List<string> result = new List<string>();
            List<int> indexList = BuildInsertIndexList(subjectString, lineLength);
            indexList.Insert(0, 0);
            for (int i = 0; i < indexList.Count - 1; i++)
            {
                result.Add(subjectString.Substring(indexList[i], indexList[i + 1] - indexList[i]));
            }
            if (indexList.Last() + 1 < subjectString.Length)
                result.Add(subjectString.Substring(indexList.Last(), subjectString.Length - indexList.Last()));
            return result;
        }
        /// <summary>
        /// 判断字符是否为中文字符
        /// </summary>
        /// <param name="c"></param>
        /// <seealso cref="http://blog.csdn.net/superbigcupid/article/details/52183414"/>
        /// <returns></returns>
        public static bool IsChinese(this char c)
        {
            //基本汉字,20902字：4E00-9FA5
            //CJK兼容符号（竖排变体、下划线、顿号）：FE30-FE4F
            return (c >= 0x4E00 && c <= 0x9FA5) || (c >= 0xFE30 && c <= 0xFF4F);
        }
        /// <summary>
        /// 判断字符是否为中文标点符号
        /// （判断英文标点符号可用：char.IsPunctuation）
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsChinesePunctuation(this char c)
        {
            return ((int)c >= 0x2000 && (int)c <= 0x2FFF) ||
                   ((int)c >= 0x3000 && (int)c <= 0x3FFF) ||
                   ((int)c >= 0xFF00 && (int)c <= 0xFFFF) ||
                   ((int)c >= 0xFE30 && (int)c <= 0xFE3F) ||
                   ((int)c >= 0xFE10 && (int)c <= 0xFE1F);

            # region 各中文标点符号对应的Unicode编码
            //new int[]{3002,FF1F,FF01,FF0C,3001,FF1B,FF1A,300C,300D,300E,300F,2018,2019,201C,201D,FF08,FF09,3014,3015,3010,3011,2014,2026,2013,FF0E,300A,300B,3008,3009}
            //句号　　3002　　。
            //问号　　FF1F　　？
            //叹号　　FF01　　！
            //逗号　　FF0C　　，
            //顿号　　3001　　、
            //分号　　FF1B　　；
            //冒号　　FF1A　　：
            //引号　　300C　　「
            //        300D　　」
            //引号　　300E　　『
            //        300F　　』
            //引号　　2018　　‘
            //        2019　　’
            //引号　　201C　　“
            //        201D　　”
            //括号　　FF08　　（
            //        FF09　　）
            //括号　　3014　　〔
            //        3015　　〕
            //括号　　3010　　【
            //        3011　　】
            //破折号　2014　　—
            //省略号  2026　　…
            //连接号　2013　　–
            //间隔号　FF0E　　．
            //书名号  300A　 《
            //        300B　　》
            //书名号  3008　 〈
            //        3009　　〉
            #endregion
        }
        /// <summary>
        /// 将字符串按固定显示长度分组时，将获取字符串中应该分组的位置
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="maxLength">每组显示的字符串长度</param>
        /// <returns></returns>
        private static List<int> BuildInsertIndexList(string str, int maxLength)
        {
            int length = 0;
            List<int> list = new List<int>();
            for (int i = 1; i < str.Length; i++)
            {
                if (IsChinese(str[i]) || IsChinesePunctuation(str[i]))
                    length += 2;
                else
                    length++;

                if (length > maxLength)
                {
                    length = 0;
                    list.Add(i);
                }
            }
            return list;
        }
        /// <summary>
        /// 判断当前字符串(str)是否包含指定字符串数组中的一个或多个元素
        /// </summary>
        /// <param name="str"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool ContainsAny(this string str, params string[] values)
        {
            foreach (var value in values)
            {
                if (str.Contains(value))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 将字符数组转成字符串
        /// </summary>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static string CharsToString(this IEnumerable<char> chars)
        {
            return new string(chars.ToArray());
        }
    }
}
