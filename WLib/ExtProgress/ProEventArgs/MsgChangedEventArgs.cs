using System;

namespace WLib.ExtProgress.ProEventArgs
{
    /// <summary>
    /// 为YYGISLib.Progress.ProgressMessages的MessageChanged事件提供数据（当前进度信息）
    /// </summary>
    public class MsgChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 当前信息
        /// </summary>
        public string Message { get; }
        /// <summary>
        /// 为ProgressMessages的MessageChanged事件提供数据
        /// </summary>
        /// <param name="msg">前信息</param>
        public MsgChangedEventArgs(string msg)
        {
            this.Message = msg;
        }
    }
}