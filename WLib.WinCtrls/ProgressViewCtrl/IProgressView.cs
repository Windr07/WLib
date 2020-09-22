using WLib.ExtProgress.Core;

namespace WLib.WinCtrls.ProgressViewCtrl
{
    /// <summary>
    /// 表示使用<see cref="ProgressViewManager"/>管理<see cref="IProgressOperation"/>操作的界面
    /// </summary>
    public interface IProgressView
    {
        /// <summary>
        /// 进度操作的管理与界面交互
        /// </summary>
        ProgressViewManager ProViewManager { get; }
    }
}
