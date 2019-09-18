/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/6
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using WLib.ExtProgress.Core;

namespace WLib.ExtProgress.ProEventArgs
{
    /// <summary>
    /// 为<see cref="ProgressOperation{TData, TMsgGroup}.OperationError"/>事件提供数据（异常信息）
    /// </summary>
    public class ProErrorEventArgs : EventArgs
    {
        /// <summary>
        /// 异常
        /// </summary>
        public Exception OptException { get; }
        /// <summary>
        /// 为<see cref="ProgressOperation{TData, TMsgGroup}.OperationError"/>事件提供数据（异常信息）
        /// </summary>
        /// <param name="optException">异常</param>
        public ProErrorEventArgs(Exception optException) => OptException = optException;
    }
}