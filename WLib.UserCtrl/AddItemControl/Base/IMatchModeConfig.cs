namespace WLib.UserCtrls.AddItemControl.Base
{
    public interface IMatchModeConfig
    {
        /// <summary>
        /// 匹配模式（0-Split-根据分隔符分割字符串，获得分隔后的文本项； 1-Regex-根据正则表达式获得匹配项）
        /// </summary>
        EMatchMode MatchMode { get; }
        /// <summary>
        /// 正则表达式
        /// </summary>
        string RegexString { get; set; }
        /// <summary>
        /// 默认可在下拉框中选择的分隔符
        /// </summary>
        string[] DefaultSplitArray { get; set; }
        /// <summary>
        /// 分隔符数组
        /// </summary>
        string[] SplitStringArray { get; set; }
    }
}
