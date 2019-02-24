/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Progress.ProEventArgs
{
    /// <summary>
    /// 为YYGISLib.Progress.GroupMessages的MessageChanged事件提供数据（信息变化后的当前信息及其分组）
    /// </summary>
    public class GroupMsgChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 当前信息所在分组
        /// </summary>
        public string Group { get; }
        /// <summary>
        /// 当前信息
        /// </summary>
        public string Message { get; }
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
}
