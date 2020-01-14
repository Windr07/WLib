using WLib.ExtProgress.Core;

namespace WLib.WinCtrls.Dev.ProgressViewCtrl
{
    /// <summary>
    /// 表示使用<see cref="DevProgressViewManager"/>管理<see cref="IProgressOperation"/>操作的界面
    /// </summary>
    public interface IDevProgressView
    {
        /// <summary>
        /// 进度操作的管理与界面交互
        /// </summary>
        DevProgressViewManager ProViewManager { get; }
    }
}
