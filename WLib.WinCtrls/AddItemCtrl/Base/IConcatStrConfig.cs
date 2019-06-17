/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.WinCtrls.AddItemCtrl.Base
{
    /// <summary>
    /// 文本连接配置项
    /// </summary>
    public interface IConcatStrConfig
    {
        /// <summary>
        /// 是否添加前缀和后缀
        /// </summary>
        bool IsAddPrefixSuffix { get; set; }
        /// <summary>
        /// 对文本所添加前缀
        /// </summary>
        string Prefix { get; set; }
        /// <summary>
        /// 对文本所添加后缀
        /// </summary>
        string Suffix { get; set; }


        /// <summary>
        /// 是否将全部项串联成一项
        /// </summary>
        bool IsConcatAll { get; set; }
        /// <summary>
        /// 串联每一项的连接符
        /// </summary>
        string ConStr { get; set; }
    }
}
