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
    /// 提供将阿拉伯数字转换为中文表示的方法
    /// </summary>
    public static class NumberConversion
    {
        /// <summary>
        /// "〇一二三四五六七八九"
        /// </summary>
        public static string ChnNum = "〇一二三四五六七八九";
        /// <summary>
        /// "十百千万十百千亿"
        /// </summary>
        public static string ChnUnit = "十百千万十百千亿";
        /// <summary>
        /// 被替换的字符串
        /// </summary>
        private static readonly string[] BeforeReplace = { "〇〇", "亿〇万", "〇万", "〇亿", "亿万", "〇〇" };
        /// <summary>
        /// 替换后的新字符串
        /// </summary>
        private static readonly string[] AfterReplace = { "〇", "亿〇", "万〇", "亿〇", "亿〇", "〇", };

        /// <summary>
        /// 将阿拉伯数字（整数）转换为中文数字字符串（〇一二三四五六七八九）
        /// </summary>
        /// <param name="number">阿拉伯数字字符串</param>
        /// <param name="useUnit">是否使用计数单位（十百千万亿）</param>
        /// <returns></returns>
        public static string ToChineseNumWord(this int number, bool useUnit = false)
        {
            string result = string.Empty;
            if (number < 0)
            {
                number = Math.Abs(number);
                result = "负";
            }
            result += ToChineseNumWord(number, useUnit);
            return result;
        }

        /// <summary>
        /// 将阿拉伯数字（整数）字符串转换为中文数字字符串（〇一二三四五六七八九）
        /// </summary>
        /// <param name="strNumber">阿拉伯数字字符串</param>
        /// <param name="useUnit">是否使用计数单位（十百千万亿）</param>
        /// <returns></returns>
        public static string ToChineseNumWord(this string strNumber, bool useUnit = false)
        {
            string strNum = strNumber;

            for (int i = 0; i < ChnNum.Length; i++)
            {
                strNum = strNum.Replace(i.ToString(), ChnNum[i].ToString());
            }

            if (useUnit)
            {
                int intNumLength = strNum.Length;
                for (int i = intNumLength - 1; i > 0; i--)
                {
                    //根据位数把单位加上,如果是零则不加单位，但是 万  和  亿 需要加上
                    if (strNum[i - 1] == ChnNum[0] && (intNumLength - i + 7) % 8 != 3 && (intNumLength - i + 7) % 8 != 7) continue;
                    strNum = strNum.Insert(i, ChnUnit[(intNumLength - i + 7) % 8].ToString());
                }

                //替换  零零 -> 零  亿零万  ->  亿零，零万  ->  万零，零亿 -> 亿零，亿万 -> 亿零，零角零分 - > ""，零分 - > ""，零零 -> 零，再调用一次，确保 亿零万 替换后的情况
                for (int i = 0; i < BeforeReplace.Length; i++)
                {
                    while (strNum.IndexOf(BeforeReplace[i]) > -1)
                    {
                        strNum = strNum.Replace(BeforeReplace[i], AfterReplace[i]);
                    }
                }
                //最后的 零 去掉
                if (strNum.EndsWith(ChnNum[0].ToString())) strNum = strNum.Substring(0, strNum.Length - 1);

                //一十  -> 十
                if ((strNum.Length == 3 || strNum.Length == 2) && strNum[0] == ChnNum[1] && strNum[1] == ChnUnit[0])
                    strNum = strNum.Remove(0, 1);
            }
            return strNum;
        }

    }
}
