/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using WLib.Attributes;

namespace WLib.UserCtrls.Dev.EditForm
{
    /// <summary>
    /// 保存结果状态（0-保存失败，1-保存成功）
    /// </summary>
    public enum ESavedState
    {
        /// <summary>
        /// 未执行保存
        /// </summary>
        [Description("未执行保存")]
        UnSaved = -1,
        /// <summary>
        /// 保存失败
        /// </summary>
        [Description("保存失败")]
        Fail = 0,
        /// <summary>
        /// 保存成功
        /// </summary>
        [Description("保存成功")]
        Success = 1
    }
}
