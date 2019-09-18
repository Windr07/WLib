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
    /// 为<see cref="IProgressMsgs{TGroup}.MessageChanged"/>事件提供数据（当前进度信息和所在分组）
    /// </summary>
    /// <typeparam name="TGroup"></typeparam>
    public class ProMsgChangedEventArgs<TGroup> : ProMsgChangedEventArgs where TGroup : IEquatable<TGroup>, new()
    {
        /// <summary>
        /// 当前进度信息所在分组
        /// </summary>
        public TGroup Group { get; set; }
        /// <summary>
        /// 为<see cref="IProgressMsgs{TGroup}.MessageChanged"/>事件提供数据（当前进度信息和所在分组）
        /// </summary>
        /// <param name="curGroup">当前进度信息</param>
        /// <param name="curMessage">当前进度信息所在分组</param>
        public ProMsgChangedEventArgs(TGroup curGroup, string curMessage) : base(curMessage) => Group = curGroup;
    }
}
