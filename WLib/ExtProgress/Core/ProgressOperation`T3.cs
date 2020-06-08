/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/6
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Reflection;

namespace WLib.ExtProgress.Core
{
    /// <summary>
    /// 可进行进度控制的操作
    /// </summary>
    /// <typeparam name="TInput">进度操作所需的数据</typeparam>
    /// <typeparam name="TMsgGroup">进度信息的分组，
    /// <see cref="TMsgGroup"/>必须实现等值比较<see cref="IEquatable{T}"/>且具有无参构造函数，<see cref="TMsgGroup"/>可以为数值类型</typeparam>
    /// <typeparam name="TResult">进度操作的结果</typeparam>
    public abstract class ProgressOperation<TInput, TMsgGroup, TResult> :
        ProgressOperation, IProgressOperation<TInput, TMsgGroup, TResult> where TMsgGroup : IEquatable<TMsgGroup>, new()
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
        protected ProgressOperation(string name, TInput inputData) : base(name, inputData) => InputData = inputData;
        /// <summary>
        /// 可进行进度控制的操作
        /// </summary>
        /// <param name="name">操作的名称</param>
        /// <param name="description">对操作的描述</param>
        protected ProgressOperation(string name, TInput inputData, string description) : base(name, inputData, description) => InputData = inputData;


        /// <summary>
        /// 进度操作的输入数据
        /// </summary>
        public new TInput InputData { get; set; }
        /// <summary>
        /// 进度操作的输入数据
        /// </summary>
        object IProgressOperation.InputData { get => this.InputData; set => this.InputData = (TInput)value; }

        /// <summary>
        /// 进度信息
        /// </summary>
        public new IProgressMsgs<TMsgGroup> Msgs { get; protected set; } = new ProgressMsgs<TMsgGroup>();
        /// <summary>
        /// 进度信息
        /// </summary>
        IProgressMsgs IProgressOperation.Msgs { get => this.Msgs; }

        /// <summary>
        /// 进度操作的结果
        /// </summary>
        public new TResult ResultData { get; protected set; }
        /// <summary>
        /// 进度操作的结果
        /// </summary>
        object IProgressOperation.ResultData { get => this.ResultData; }


        /// <summary>
        /// 从<see cref="IProgressOperation"/>获取基本的操作信息，同时获取全部进度信息
        /// </summary>
        /// <param name="opt"></param>
        protected override void GetMsgs(IProgressOperation opt)
        {
            Msgs.Name = opt.Name;
            Msgs.Description = opt.Description;
            Msgs.Code = opt.GetType().Name;
            var assemblyName = Assembly.GetEntryAssembly().GetName();
            Msgs.AssemblyName = assemblyName.Name;
            Msgs.AssemblyVersion = assemblyName.Version.ToString();
            Msgs.StartTime = opt.StartTime;
            Msgs.EndTime = opt.EndTime;
            Msgs.AllMessage = Msgs.GetAllMessage();
        }
    }
}
