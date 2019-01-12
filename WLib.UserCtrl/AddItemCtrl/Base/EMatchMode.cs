/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.UserCtrls.AddItemCtrl.Base
{
    /// <summary>
    /// 匹配模式
    /// </summary>
    public enum EMatchMode
    {
        /// <summary>
        /// 根据分隔符分割字符串，获得分隔后的文本项
        /// </summary>
        Split = 0,
        /// <summary>
        /// 根据正则表达式获得匹配项
        /// </summary>
        Regex = 1
    }
}
