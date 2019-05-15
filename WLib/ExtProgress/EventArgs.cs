/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Progress
{
    /// <summary>
    /// 为YYGISLib.Progress.GroupMessages的MessageChanged事件提供数据（信息变化后的当前信息及其分组）
    /// </summary>
    public class GroupMsgChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 当前信息所在分组
        /// </summary>
        public string Group { get; private set; }
        /// <summary>
        /// 当前信息
        /// </summary>
        public string Message { get; private set; }
        /// <summary>
        /// 为GroupMessages的MessageChanged事件提供数据
        /// </summary>
        /// <param name="group">当前信息所在分组</param>
        /// <param name="msg">当前信息</param>
        public GroupMsgChangedEventArgs(string group, string msg)
        {
            this.Group = group;
            this.Message = msg;
        }
    }

    /// <summary>
    /// 为YYGISLib.Progress.ProgressMessages的MessageChanged事件提供数据（当前进度信息）
    /// </summary>
    public class MsgChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 当前信息
        /// </summary>
        public string Message { get; private set; }
        /// <summary>
        /// 为ProgressMessages的MessageChanged事件提供数据
        /// </summary>
        /// <param name="msg">前信息</param>
        public MsgChangedEventArgs(string msg)
        {
            this.Message = msg;
        }
    }

    /// <summary>
    /// 为YYGISLib.Progress.ProgressOperation的OperationError事件提供数据（异常信息）
    /// </summary>
    public class OptErrorEventArgs : EventArgs
    {
        /// <summary>
        /// 异常
        /// </summary>
        public Exception OptException { get; private set; }
        /// <summary>
        /// 为ProgressOperation的OperationError事件提供数据
        /// </summary>
        /// <param name="optException">异常</param>
        public OptErrorEventArgs(Exception optException)
        {
            this.OptException = optException;
        }
    }

    /// <summary>
    /// 为YYGISLib.Progress.ProgressOperation的ProgressChanged事件提供数据（进度值信息）
    /// </summary>
    public class ProgressChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 当前进度值
        /// </summary>
        public int CurValue { get; private set; }
        /// <summary>
        /// 总进度值
        /// </summary>
        public int MaxValue { get; private set; }
        /// <summary>
        /// 完成进度占总进度的比值（0-1之间）
        /// </summary>
        public double Rate => MaxValue == 0 ? 0 : CurValue / MaxValue;

        /// <summary>
        /// 与当前进度相关的信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 为ProgressOperation的ProgressChanged事件提供数据
        /// </summary>
        /// <param name="curValue">当前进度值</param>
        /// <param name="maxValue">总进度值</param>
        public ProgressChangedEventArgs(int curValue, int maxValue)
        {
            this.CurValue = curValue;
            this.MaxValue = maxValue;
        }
        /// <summary>
        /// 为ProgressOperation的ProgressChanged事件提供数据
        /// </summary>
        /// <param name="curValue">当前进度值</param>
        /// <param name="maxValue">总进度值</param>
        /// <param name="msg">与当前进度相关的信息</param>
        public ProgressChangedEventArgs(int curValue, int maxValue, string msg)
        {
            this.CurValue = curValue;
            this.MaxValue = maxValue;
            this.Message = msg;
        }
    }
}
