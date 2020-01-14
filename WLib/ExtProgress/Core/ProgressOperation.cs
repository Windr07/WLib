/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/6
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using WLib.ExtProgress.ProEventArgs;

namespace WLib.ExtProgress.Core
{
    /// <summary>
    /// 可进行进度控制的操作
    /// </summary>
    /// <typeparam name="TData">进度操作所需的数据</typeparam>
    public abstract class ProgressOperation : IProgressOperation
    {
        #region 提示信息
        /// <summary>
        /// 开始操作时显示的信息，默认：“开始执行...”
        /// </summary>
        public string START_MESSAGE = "开始执行...";
        /// <summary>
        /// 中止操作时显示的信息，默认：“正在停止运行，请稍后...”
        /// </summary>
        public string STOPPING_MESSAGE = "正在停止运行，请稍后...";
        /// <summary>
        /// 已中止操作时显示的信息，默认：“操作中止！”
        /// </summary>
        public string STOPPED_MESSAGE = "操作中止！";
        /// <summary>
        /// 操作结束时显示的信息，默认：“操作完成！”
        /// </summary>
        public string FINISHED_MESSAGE = "操作完成！";
        /// <summary>
        /// 操作出错时显示的信息，默认：“执行过程发生错误！”
        /// </summary>
        public string ERROR_MESSAGE = "执行过程发生错误！";
        #endregion


        #region 属性
        /// <summary>
        /// 调用RunByThread方法时，在内部启用的用于执行操作的子线程
        /// </summary>
        private Thread _thread;
        /// <summary>
        /// 是否中止操作
        /// </summary>
        private bool _stopRunning;

        /// <summary>
        /// 操作的名称，用于标识操作
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 对操作的描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否中止操作
        /// </summary>
        public bool StopRunning
        {
            get => _stopRunning;
            protected set { _stopRunning = value; if (value) OnOperationStopping(); }
        }
        /// <summary>
        /// 是否正在运行操作
        /// </summary>
        public bool IsRunning { get; protected set; }
        /// <summary>
        /// 是否已暂停操作
        /// </summary>
        public bool IsPause { get; protected set; }
        /// <summary>
        /// 是否已启用子线程且在运行阶段
        /// </summary>
        public bool IsOnSubThread => _thread != null && _thread.ThreadState == ThreadState.Running;
        /// <summary>
        /// 操作开始时间
        /// </summary>
        public DateTime StartTime { get; protected set; }
        /// <summary>
        /// 操作结束时间
        /// </summary>
        public DateTime EndTime { get; protected set; }
        /// <summary>
        /// 进度信息
        /// </summary>
        public IProgressMsgs Msgs { get; protected set; }

        /// <summary>
        /// 进度操作的输入数据
        /// </summary>
        public object InputData { get; set; }
        /// <summary>
        /// 进度操作的输入数据
        /// </summary>
        object IProgressOperation.InputData { get => InputData; set => InputData = value; }
        /// <summary>
        /// 进度操作的结果数据
        /// </summary>
        public object ResultData { get; protected set; }

        /// <summary>
        /// 当前操作包含的子操作
        /// <para>这些子操作应当在外部或者构造函数中创建和加入，在<see cref="MainOperation"/>方法中运行</para>
        /// </summary>
        public List<IProgressOperation> SubProgressOperations { get; protected set; }
        #endregion


        #region 事件和事件处理
        /// <summary>
        /// 操作开始时的事件
        /// （在Run方法执行时，在IsRunning = true之前触发）
        /// </summary>
        public event EventHandler OperationStart;
        /// <summary>
        /// 操作已被中止的事件
        /// </summary>
        public event EventHandler OperationStoped;
        /// <summary>
        /// 中止操作的事件
        /// </summary>
        public event EventHandler OperationStopping;
        /// <summary>
        /// 操作正常执行完毕的事件
        /// </summary>
        public event EventHandler OperationFinished;
        /// <summary>
        /// 操作执行出现错误的事件
        /// </summary>
        public event EventHandler<ProErrorEventArgs> OperationError;
        /// <summary>
        /// 执行进度改变的事件
        /// </summary>
        public event EventHandler<ProChangedEventArgs> ProgressChanged;
        /// <summary>
        /// 执行进度+1的事件
        /// </summary>
        public event EventHandler ProgressAdd;
        /// <summary>
        /// 操作执行过程向外部输出实时数据的事件
        /// </summary>
        public event EventHandler<ProDataOutputEventArgs> ProgressDataOuput;
        /// <summary>
        /// 执行<see cref="OperationStart"/>事件处理
        /// </summary>
        protected void OnOperationStart() => OperationStart?.Invoke(this, new EventArgs());
        /// <summary>
        /// 执行<see cref="OperationStopping"/>事件处理
        /// </summary>
        protected void OnOperationStopping() => OperationStopping?.Invoke(this, new EventArgs());
        /// <summary>
        /// 执行<see cref="OperationStoped"/>事件处理
        /// </summary>
        protected void OnStopFinished() => OperationStoped?.Invoke(this, new EventArgs());
        /// <summary>
        /// 执行<see cref="ExportFinished"/>事件处理
        /// </summary>
        protected void OnOperationFinished() => OperationFinished?.Invoke(this, new EventArgs());
        /// <summary>
        /// 执行<see cref="OperationError"/>事件处理
        /// </summary>
        /// <param name="ex"></param>
        protected void OnOperationError(Exception ex) => OperationError?.Invoke(this, new ProErrorEventArgs(ex));
        /// <summary>
        /// 执行<see cref="ProgressChanged"/>事件处理，设置当前进度值和总进度值
        /// </summary>
        /// <param name="i">当前进度值</param>
        /// <param name="sum">总进度值</param>
        protected void OnProgressChanged(int i, int sum) => ProgressChanged?.Invoke(this, new ProChangedEventArgs(i, sum));
        /// <summary>
        /// 执行<see cref="ProgressChanged"/>事件处理，设置当前进度值、总进度值、当前进度信息
        /// </summary>
        /// <param name="i">当前进度值</param>
        /// <param name="sum">总进度值</param>
        /// <param name="msg">当前进度信息</param>
        protected void OnProgressChanged(int i, int sum, string msg) => ProgressChanged?.Invoke(this, new ProChangedEventArgs(i, sum, msg));
        /// <summary>
        /// 执行<see cref="ProgressAdd"/>事件处理，当前进度值+1
        /// </summary>
        protected void OnProgressAdd() => ProgressAdd?.Invoke(this, new EventArgs());
        /// <summary>
        /// 执行<see cref="ProgressDataOuput"/>事件处理，设置向外部输出的数据
        /// </summary>
        protected void OnProgressDataOutput(object data) => ProgressDataOuput?.Invoke(this, new ProDataOutputEventArgs(data));
        #endregion


        #region 构造函数
        /// <summary>
        /// 可进行进度控制的操作
        /// </summary>
        protected ProgressOperation()
        {
            IsRunning = false;
            IsPause = false;
            Msgs = new ProgressMsgs();
            OperationStart = (sender, e) => { InitToOperation(); IsRunning = true; Msgs.Info(START_MESSAGE); StartTime = DateTime.Now; };
            OperationStoped = (sender, e) => { Msgs.Info(STOPPED_MESSAGE); IsRunning = false; EndTime = DateTime.Now; GetMsgs(this); };
            OperationFinished = (sender, e) => { Msgs.Info(FINISHED_MESSAGE); IsRunning = false; EndTime = DateTime.Now; GetMsgs(this); };
            OperationError = (sender, e) => { Msgs.Info(ERROR_MESSAGE); EndTime = DateTime.Now; GetMsgs(this); };
            SubProgressOperations = new List<IProgressOperation>();
        }
        /// <summary>
        /// 可进行进度控制的操作
        /// </summary>
        /// <param name="name">操作的名称</param>
        protected ProgressOperation(string name) : this() => Description = Name = name;
        /// <summary>
        /// 可进行进度控制的操作
        /// </summary>
        /// <param name="name">操作的名称</param>
        /// <param name="inputData">进度操作所需的数据</param>
        protected ProgressOperation(string name, object inputData) : this(name) => InputData = inputData;
        /// <summary>
        /// 可进行进度控制的操作
        /// </summary>
        /// <param name="name">操作的名称</param>
        /// <param name="description">对操作的描述</param>
        protected ProgressOperation(string name, object inputData, string description) : this(name, inputData) => Description = description;
        #endregion


        #region 进度控制方法
        /// <summary>
        /// 清空消息等，以准备执行操作
        /// </summary>
        protected virtual void InitToOperation()
        {
            Msgs.Clear();
            StopRunning = false;
        }
        /// <summary>
        /// 操作的主体业务处理
        /// </summary>
        protected abstract void MainOperation();//在子类中重写该方法
        /// <summary>
        /// 清空消息，执行操作
        /// </summary>
        public virtual void Run()
        {
            OnOperationStart();
            try
            {
                MainOperation(); //操作的主体业务处理，在子类中重写该方法

                if (StopRunning)
                    OnStopFinished();
                else
                    OnOperationFinished();
            }
            catch (Exception ex) { OnOperationError(ex); IsRunning = false; }
        }
        /// <summary>
        /// 清空消息，创建新线程并在线程中执行操作
        /// </summary>
        public virtual void RunByThread()
        {
            OnOperationStart();
            _thread = new Thread(() =>
            {
                try
                {
                    MainOperation(); //操作的主体业务处理，在子类中重写该方法

                    if (StopRunning)
                        OnStopFinished();
                    else
                        OnOperationFinished();
                }
                catch (Exception ex) { OnOperationError(ex); IsRunning = false; }
                _thread.Abort();
                _thread = null;
            });
            _thread.Start();
        }
        /// <summary>
        /// 停止操作
        /// </summary>
        public virtual void Stop()
        {
            StopRunning = true;
        }
        /// <summary>
        /// 暂停操作（仅在RunByThread方法执行线程操作期间有效）
        /// </summary>
        public virtual void Pause()
        {
            if (_thread != null && _thread.ThreadState == ThreadState.Running)
            {
                IsPause = true;
                _thread.Suspend();
            }
        }
        /// <summary>
        /// 继续操作（仅在Pause方法执行期间有效）
        /// </summary>
        public virtual void GoOn()
        {
            if (_thread != null && _thread.ThreadState == ThreadState.Suspended)
            {
                IsPause = false;
                _thread.Resume();
            }
        }
        /// <summary>
        /// 立即停止操作（仅在RunByThread方法执行线程操作期间有效）
        /// </summary>
        public virtual void Abort()
        {
            if (_thread != null && _thread.ThreadState == ThreadState.Running)
                _thread.Abort();
        }
        #endregion


        /// <summary>
        /// 从<see cref="IProgressOperation"/>获取基本的操作信息，同时获取全部进度信息
        /// </summary>
        protected virtual void GetMsgs(IProgressOperation opt)
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
