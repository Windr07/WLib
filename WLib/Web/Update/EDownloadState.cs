/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020/6
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using WLib.Attributes.Description;

namespace WLib.Web.Update
{
    /// <summary>
    /// 软件自动更新的更新结果状态
    /// </summary>
    public enum EDownloadState
    {
        /// <summary>
        /// 下载更新包成功
        /// </summary>
        [DescriptionEx("下载更新包成功")]
        Dowloaded,

        /// <summary>
        /// 当前版本为最新版本，不需要更新
        /// </summary>
        [DescriptionEx("当前版本为最新版本")]
        NoNeedToUpdate,

        /// <summary>
        /// 更新失败
        /// </summary>
        [DescriptionEx("更新失败")]
        Fail,
    }
}
