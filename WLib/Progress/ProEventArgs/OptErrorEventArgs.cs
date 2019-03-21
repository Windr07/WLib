using System;

namespace WLib.Progress.ProEventArgs
{
    /// <summary>
    /// 为YYGISLib.Progress.ProgressOperation的OperationError事件提供数据（异常信息）
    /// </summary>
    public class OptErrorEventArgs : EventArgs
    {
        /// <summary>
        /// 异常
        /// </summary>
        public Exception OptException { get; }
        /// <summary>
        /// 为ProgressOperation的OperationError事件提供数据
        /// </summary>
        /// <param name="optException">异常</param>
        public OptErrorEventArgs(Exception optException)
        {
            this.OptException = optException;
        }
    }
}