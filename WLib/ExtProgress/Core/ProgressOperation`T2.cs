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
    public abstract class ProgressOperation<TData, TMsgGroup> : ProgressOperation<TData>, IProgressOperation<TData> where TMsgGroup : IEquatable<TMsgGroup>, new()
    {
        /// <summary>
        /// 进度信息
        /// </summary>
        public new IProgressMsgs<TMsgGroup> Msgs { get; protected set; }


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
        /// 清空消息等，以准备执行操作
        /// </summary>
        protected override void InitToOperation()
        {
            if (Msgs == null)
                Msgs = new ProgressMsgs<TMsgGroup>();

            Msgs.Clear();
            StopRunning = false;
        }
    }
}
