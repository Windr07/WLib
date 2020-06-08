/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/6
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using WLib.ExtProgress.ProEventArgs;

namespace WLib.ExtProgress.Core
{
    /// <summary>
    /// 进度事件
    /// </summary>
    public interface IProgressEvents
    {
        /// <summary>
        /// 操作开始时的事件
        /// </summary>
        event EventHandler OperationStart;
        /// <summary>
        /// 操作已被中止的事件
        /// </summary>
        event EventHandler OperationStoped;
        /// <summary>
        /// 中止操作的事件
        /// </summary>
        event EventHandler OperationStopping;
        /// <summary>
        /// 操作正常执行完毕的事件
        /// </summary>
        event EventHandler OperationFinished;
        /// <summary>
        /// 操作执行出现错误的事件
        /// </summary>
        event EventHandler<ProErrorEventArgs> OperationError;
        /// <summary>
        /// 执行进度改变的事件
        /// </summary>
        event EventHandler<ProChangedEventArgs> ProgressChanged;
        /// <summary>
        /// 执行进度+1的事件
        /// </summary>
        event EventHandler ProgressAdd;
        /// <summary>
        /// 操作执行过程向外部输出实时数据的事件
        /// </summary>
        event EventHandler<ProDataOutputEventArgs> DataOutput;
    }
}
