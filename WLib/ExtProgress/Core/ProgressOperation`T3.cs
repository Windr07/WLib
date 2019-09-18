/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/6
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.ExtProgress.Core
{
    /// <summary>
    /// 可进行进度控制的操作
    /// </summary>
    /// <typeparam name="TData">进度操作所需的数据</typeparam>
    /// <typeparam name="TMsgGroup">进度信息的分组，
    /// <see cref="TMsgGroup"/>必须实现等值比较<see cref="IEquatable{T}"/>且具有无参构造函数，<see cref="TMsgGroup"/>可以为数值类型</typeparam>
    /// <typeparam name="TResult">进度操作的结果</typeparam>
    public abstract class ProgressOperation<TData, TMsgGroup, TResult> :
        ProgressOperation<TData, TMsgGroup>, IProgressOperation<TData, TResult> where TMsgGroup : IEquatable<TMsgGroup>, new()
    {
        /// <summary>
        /// 可进行进度控制的操作
        /// </summary>
        protected ProgressOperation() : base() { }
        /// <summary>
        /// 可进行进度控制的操作
        /// </summary>
        /// <param name="name">操作的名称</param>
        protected ProgressOperation(string name) : base(name) { }
        /// <summary>
        /// 可进行进度控制的操作
        /// </summary>
        /// <param name="name">操作的名称</param>
        /// <param name="inputData">进度操作所需的数据</param>
        protected ProgressOperation(string name, TData inputData) : base(name, inputData) { }
        /// <summary>
        /// 可进行进度控制的操作
        /// </summary>
        /// <param name="name">操作的名称</param>
        /// <param name="description">对操作的描述</param>
        protected ProgressOperation(string name, TData inputData, string description) : base(name, inputData, description) { }


        /// <summary>
        /// 进度操作的结果
        /// </summary>
        public TResult Result { get; protected set; }
    }
}
