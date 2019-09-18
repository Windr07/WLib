/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/6
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Threading;
using WLib.ExtProgress.ProEventArgs;

namespace WLib.ExtProgress.Core
{
    /// <summary>
    /// 可进行进度控制的操作
    /// </summary>
    /// <typeparam name="TData">进度操作所需的数据</typeparam>
    public abstract class ProgressOperation<TData> : IProgressOperation<TData>
    {
        #region 提示信息
        /// <summary>
        /// 开始操作时显示的信息：“开始执行...”
        /// </summary>
        public string START_MESSAGE = "开始执行...";
        /// <summary>
        /// 中止操作时显示的信息：“正在停止运行，请稍后...”
        /// </summary>
        public string STOPPING_MESSAGE = "正在停止运行，请稍后...";
        /// <summary>
        /// 已中止操作时显示的信息：“操作中止！”
        /// </summary>
        public string STOPPED_MESSAGE = "操作中止！";
        /// <summary>
        /// 操作结束时显示的信息：“操作完成！”
        /// </summary>
        public string FINISHED_MESSAGE = "操作完成！";
        /// <summary>
        /// 操作出错时显示的信息：“执行过程发生错误，停止运行。”
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
        /// 是否中止操作
        /// </summary>
        public bool StopRunning
        {
            get => _stopRunning;
            protected set { _stopRunning = value; if (value) OnOperationStopping(); }
        }
        /// <summary>
        /// 进度信息
        /// </summary>
        public IProgressMsgs Msgs { get; protected set; }
        /// <summary>
        /// 普通信息
        /// </summary>
        public string Info { set => Msgs.Info(value); }
        /// <summary>
        /// 进度操作所需的数据
        /// </summary>
        public TData InputData { get; set; }
        /// <summary>
        /// 操作的名称，用于标识操作
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 对操作的描述
        /// </summary>
        public string Description { get; set; }
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
        /// 当前操作包含的子操作
        /// <para>这些子操作应在<see cref="MainOperation"/>方法中进行处理</para>
        /// </summary>
        public List<IProgressOperation> SubProgressOperations { get; protected set; }
        /// <summary>
        /// 进行操作的输入数据
        /// </summary>
        object IProgressOperation.InputData { get => InputData; set => InputData = (TData)value; }
        #endregion


        #region 事件
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
        #endregion


        #region 执行事件处理
        /// <summary>
        /// 执行OperationStart事件处理
        /// </summary>
        protected void OnOperationStart() => OperationStart?.Invoke(this, new EventArgs());
        /// <summary>
        /// 执行OperationStopping事件处理
        /// </summary>
        protected void OnOperationStopping() => OperationStopping?.Invoke(this, new EventArgs());
        /// <summary>
        /// 执行OperationStoped事件处理
        /// </summary>
        protected void OnStopFinished() => OperationStoped?.Invoke(this, new EventArgs());
        /// <summary>
        /// 执行ExportFinished事件处理
        /// </summary>
        protected void OnOperationFinished() => OperationFinished?.Invoke(this, new EventArgs());
        /// <summary>
        /// 执行OperationError事件处理
        /// </summary>
        /// <param name="ex"></param>
        protected void OnOperationError(Exception ex) => OperationError?.Invoke(this, new ProErrorEventArgs(ex));
        /// <summary>
        /// 执行ProgressChanged事件处理
        /// </summary>
        /// <param name="i">当前进度值</param>
        /// <param name="sum">总进度值</param>
        protected void OnProgressChanged(int i, int sum) => ProgressChanged?.Invoke(this, new ProChangedEventArgs(i, sum));
        /// <summary>
        /// 执行ProgressChanged事件处理
        /// </summary>
        /// <param name="i">当前进度值</param>
        /// <param name="sum">总进度值</param>
        /// <param name="msg">当前进度信息</param>
        protected void OnProgressChanged(int i, int sum, string msg) => ProgressChanged?.Invoke(this, new ProChangedEventArgs(i, sum, msg));
        /// <summary>
        /// 执行ProgressAdd事件处理
        /// </summary>
        protected void OnProgressAdd() => ProgressAdd?.Invoke(this, new EventArgs());
        #endregion


        #region 构造函数
        /// <summary>
        /// 可进行进度控制的操作
        /// </summary>
        protected ProgressOperation()
        {
            IsRunning = false;
            IsPause = false;
            OperationStart = (sender, e) => { InitToOperation(); IsRunning = true; Info = START_MESSAGE; StartTime = DateTime.Now; };
            OperationStoped = (sender, e) => { Info = STOPPED_MESSAGE; IsRunning = false; EndTime = DateTime.Now; };
            OperationFinished = (sender, e) => { Info = FINISHED_MESSAGE; IsRunning = false; EndTime = DateTime.Now; };
            OperationError = (sender, e) => { EndTime = DateTime.Now; };
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
        protected ProgressOperation(string name, TData inputData) : this(name) => InputData = inputData;
        /// <summary>
        /// 可进行进度控制的操作
        /// </summary>
        /// <param name="name">操作的名称</param>
        /// <param name="description">对操作的描述</param>
        protected ProgressOperation(string name, TData inputData, string description) : this(name, inputData) => Description = description;
        #endregion


        #region 进度控制方法
        /// <summary>
        /// 清空消息等，以准备执行操作
        /// </summary>
        protected virtual void InitToOperation()
        {
            if (Msgs == null)
                Msgs = new ProgressMsgs();

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
    }
}
