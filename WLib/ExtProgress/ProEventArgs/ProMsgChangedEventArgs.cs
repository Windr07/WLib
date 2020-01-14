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
    /// 为<see cref="IProgressMsgs.MessageChanged"/>事件提供数据（当前进度信息）
    /// </summary>
    public class ProMsgChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 当前进度信息
        /// </summary>
        public string CurMessage { get; set; }
        /// <summary>
        /// 为<see cref="IProgressMsgs.MessageChanged"/>事件提供数据（当前进度信息）
        /// </summary>
        /// <param name="curMessage">当前进度信息</param>
        public ProMsgChangedEventArgs(string curMessage) => CurMessage = curMessage;
    }
}
