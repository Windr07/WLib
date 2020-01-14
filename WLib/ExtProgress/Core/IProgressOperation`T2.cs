/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/6
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.ExtProgress.Core
{
    /// <summary>
    /// 可进行进度控制的操作
    /// </summary>
    /// <typeparam name="TData">进度操作所需的数据</typeparam>
    /// <typeparam name="TResult">>进度操作的结果</typeparam>
    public interface IProgressOperation<TData, TResult> : IProgressOperation<TData>
    {
        /// <summary>
        /// 进度操作的结果
        /// </summary>
        TResult Result { get; }
    }
}
