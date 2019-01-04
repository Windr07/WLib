namespace WLib.UserCtrls.AddItemControl.Base
{
    /// <summary>
    /// 分隔符定义
    /// </summary>
    public class SplitStrDef
    {
        /// <summary>
        /// 分隔符实际的值（eg:"\t"）
        /// </summary>
        public string Value;
        /// <summary>
        /// 分隔符的文字表述（eg:"[制表符]"）
        /// </summary>
        public string Description;

        /// <summary>
        /// 分隔符定义
        /// </summary>
        /// <param name="value">分隔符实际的值（eg:"\t"）</param>
        public SplitStrDef(string value)
        {
            this.Value = value;
            this.Description = value;
        }
        /// <summary>
        /// 分隔符定义
        /// </summary>
        /// <param name="description">分隔符实际的值（eg:"\t"）</param>
        /// <param name="value">分隔符的文字表述（eg:"[制表符]"）</param>
        public SplitStrDef(string value, string description)
        {
            this.Value = value;
            this.Description = description;
        }
        /// <summary>
        /// 输出分隔符的文字表述
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Description;
        }

        /// <summary>
        /// 常用的分隔符定义数组（换行、空白、制表、中文逗号、英文逗号、中文分号、英文分号）
        /// </summary>
        public static SplitStrDef[] SourceSplits = new SplitStrDef[]
        {
            new SplitStrDef(STR_NEWLINE_VAL, STR_NEWLINE_DES),
            new SplitStrDef(STR_WHITESPACE_VAL, STR_WHITESPACE_DES),
            new SplitStrDef(STR_TAB_VAL, STR_TAB_DES),
            new SplitStrDef(STR_COMMA_EN, STR_COMMA_EN),
            new SplitStrDef(STR_COMMA_CN, STR_COMMA_CN),
            new SplitStrDef(STR_SEMICOLON_EN, STR_SEMICOLON_EN),
            new SplitStrDef(STR_SEMICOLON_CN, STR_SEMICOLON_CN),
        };

        /// <summary>
        /// 常用的分隔符数组（换行、空白、制表、中文逗号、英文逗号、中文分号、英文分号）
        /// </summary>
        public static string[] SourceSplitsStr = new string[]
        {
            STR_NEWLINE_VAL,
            STR_WHITESPACE_VAL,
            STR_TAB_VAL,
            STR_COMMA_EN,
            STR_COMMA_CN,
            STR_SEMICOLON_EN,
            STR_SEMICOLON_CN,
        };

        #region 字符串常量
        /// <summary>
        /// 英文逗号：","
        /// </summary>
        public const string STR_COMMA_EN = ",";
        /// <summary>
        /// 中文逗号："，"
        /// </summary>
        public const string STR_COMMA_CN = "，";
        /// <summary>
        /// 英文分号：";"
        /// </summary>
        public const string STR_SEMICOLON_EN = ";";
        /// <summary>
        /// 中文分号："；"
        /// </summary>
        public const string STR_SEMICOLON_CN = "；";
        /// <summary>
        /// "[空格]"
        /// </summary>
        public const string STR_WHITESPACE_DES = "[空格]";
        /// <summary>
        /// "[换行]"
        /// </summary>
        public const string STR_NEWLINE_DES = "[换行]";
        /// <summary>
        /// "[制表符]"
        /// </summary>
        public const string STR_TAB_DES = "[制表符]";
        /// <summary>
        /// " "
        /// </summary>
        public const string STR_WHITESPACE_VAL = " ";
        /// <summary>
        /// "\r\n"
        /// </summary>
        public const string STR_NEWLINE_VAL = "\r\n";
        /// <summary>
        /// "\t"
        /// </summary>
        public const string STR_TAB_VAL = "\t";
        #endregion
    }
}
