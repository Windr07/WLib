/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.ExtProgress.Core
{
    /// <summary>
    /// 可进行进度控制的操作
    /// </summary>
    /// <typeparam name="TInput">进度操作所需的数据</typeparam>
    /// <typeparam name="TMsgGroup">进度信息的分组，
    /// <see cref="TMsgGroup"/>必须实现等值比较<see cref="IEquatable{T}"/>且具有无参构造函数，<see cref="TMsgGroup"/>可以为数值类型</typeparam>
    /// <typeparam name="TResult">进度操作的结果</typeparam>
    public interface IProgressOperation<TInput, TMsgGroup, TResult> : IProgressOperation where TMsgGroup : IEquatable<TMsgGroup>, new()
    {
        /// <summary>
        /// 进度操作所需的数据
        /// </summary>
        new TInput InputData { get; set; }
        /// <summary>
        /// 进度信息
        /// </summary>
        new IProgressMsgs<TMsgGroup> Msgs { get; }
        /// <summary>
        /// 进度操作的结果
        /// </summary>
        new TResult ResultData { get; }
    }
}
