/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2016/12/22 17:28:54
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.UserCtrls.PathCtrl
{
    /// <summary>
    /// 路径选择框按钮显示选项
    /// </summary>
    public enum EShowButtonOption
    {
        /// <summary>
        /// 同时显示浏览按钮、选择按钮、保存按钮
        /// </summary>
        All,
        /// <summary>
        /// 同时显示浏览按钮、选择按钮
        /// </summary>
        ViewSelect,
        /// <summary>
        /// 同时显示浏览按钮、保存按钮
        /// </summary>
        ViewSave,
        /// <summary>
        /// 不显示按钮
        /// </summary>
        None,
        /// <summary>
        /// 只显示选择按钮
        /// </summary>
        Select,
        /// <summary>
        /// 只显示浏览按钮
        /// </summary>
        View,
        /// <summary>
        /// 只显示操作按钮
        /// </summary>
        Opt,
    }
}
