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
    /// 进度信息
    /// </summary>
    public interface IProgressMsgs
    {
        /// <summary>
        /// 是否在进度信息中追加当前时间
        /// </summary>
        bool AppendTime { get; set; }
        /// <summary>
        /// 全部进度信息
        /// </summary>
        string AllMessage { get; }
        /// <summary>
        /// 反序输出的全部进度信息
        /// </summary>
        string AllMessageReverse { get; }
        /// <summary>
        /// 设置当前进度信息
        /// </summary>
        /// <param name="curMessage">当前进度信息</param>
        /// <returns></returns>
        void Info(string curMessage);
        /// <summary>
        /// 清除全部的进度信息
        /// </summary>
        void Clear();
        /// <summary>
        /// 进度信息发生变化的事件
        /// </summary>
        event EventHandler<ProMsgChangedEventArgs> MessageChanged;
    }
}
