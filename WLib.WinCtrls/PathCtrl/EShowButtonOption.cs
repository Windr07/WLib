/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2016/12/22 17:28:54
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.WinCtrls.PathCtrl
{
    /// <summary>
    /// 路径选择框按钮显示选项
    /// </summary>
    public enum EShowButtonOption
    {
        /// <summary>
        /// 不显示按钮
        /// </summary>
        None = 0,
        /// <summary>
        /// 只显示浏览按钮
        /// </summary>
        View = 1,
        /// <summary>
        /// 只显示选择按钮
        /// </summary>
        Select = 2,
        /// <summary>
        /// 只显示操作按钮
        /// </summary>
        Opt = 4,
        /// <summary>
        /// 同时显示浏览按钮、选择按钮
        /// </summary>
        ViewSelect = View | Select,
        /// <summary>
        /// 同时显示浏览按钮、操作按钮
        /// </summary>
        ViewOpt = View | Opt,
        /// <summary>
        /// 同时显示浏览按钮、选择按钮、操作按钮
        /// </summary>
        All = View | Select | Opt,
    }
}
