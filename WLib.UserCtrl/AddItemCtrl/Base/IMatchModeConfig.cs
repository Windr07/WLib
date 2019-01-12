/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.UserCtrls.AddItemCtrl.Base
{
    /// <summary>
    /// 文本匹配或筛选配置项
    /// </summary>
    public interface IMatchModeConfig
    {
        /// <summary>
        /// 匹配模式（0-Split-根据分隔符分割文本，获得分隔后的文本项； 1-Regex-根据正则表达式获得匹配项）
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
