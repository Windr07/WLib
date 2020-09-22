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
    /// <typeparam name="TGroup">进度信息的分组，
    /// <see cref="TGroup"/>必须实现等值比较<see cref="IEquatable{T}"/>且具有无参构造函数，<see cref="TGroup"/>可以为值类型</typeparam>
    public interface IProgressMsgs<TGroup> : IProgressMsgs where TGroup : IEquatable<TGroup>, new()
    {
        /// <summary>
        /// 进度信息的默认分组
        /// </summary>
        TGroup DefaultGroup { get; }
        /// <summary>
        /// 进度信息的分组
        /// </summary>
        TGroup[] Groups { get; }
        /// <summary>
        /// 设置当前进度信息
        /// </summary>
        /// <param name="curMessage">当前进度信息</param>
        /// <param name="curGroup">当前进度信息所属的分组</param>
        /// <returns></returns>
        void Info(string curMessage, TGroup curGroup);
        /// <summary>
        /// 进度信息发生变化的事件
        /// </summary>
        new event EventHandler<ProMsgChangedEventArgs<TGroup>> MessageChanged;
    }
}
