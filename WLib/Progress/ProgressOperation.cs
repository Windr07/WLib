/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Threading;
using WLib.Progress.ProEventArgs;

namespace WLib.Progress
{
    /// <summary>
    /// 数据批量操作的信息处理和简单进度控制
    /// </summary>
    public class ProgressOperation : ProgressMessages
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
        /// 已中止操作时显示的信息：“操作中止。”
        /// </summary>
        public string STOPPED_MESSAGE = "操作中止。";
        /// <summary>
        /// 操作结束时显示的信息：“操作完成！”
        /// </summary>
        public string FINISHED_MESSAGE = "操作完成！";
        /// <summary>
        /// 操作出错时显示的信息：“执行过程发生错误，停止运行。”
        /// </summary>
        public string ERROR_MESSAGE = "执行过程发生错误，停止运行。";
        /// <summary>
        /// 操作成功时显示的信息：“生成成功：”
        /// </summary>
        public string SUCCESS_MESSAGE = "生成成功：";
        #endregion

        #region 属性
        /// <summary>
        /// 调用RunByThread方法时，在内部启用的用于执行批量操作的子线程
        /// </summary>
        private Thread _thread;
        /// <summary>
        /// 是否中止执行批量操作
        /// </summary>
        private bool _stopRunning;
        /// <summary>
        /// 是否中止执行批量操作
        /// </summary>
        public bool StopRunning
        {
            get => this._stopRunning;
            set
            {
                this._stopRunning = value;
                if (value)
                    OnOperationStopping();
            }
        }
        /// <summary>
        /// 对操作的描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 成果输出目录
        /// </summary>
        public string OutputDirectory { get; set; }
        /// <summary>
        /// 输出对象（文件或文件夹）名称
        /// </summary>
        public string OutputName { get; set; }
        /// <summary>
        /// 操作的名称，用于标识操作
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 进行批量操作的数据列表
        /// </summary>
        public object[] OperationData { get; set; }
        /// <summary>
        /// 是否覆盖原文件（默认值为true）
        /// </summary>
        public bool OverrideFile { get; set; }
        /// <summary>
        /// 是否正在运行批量操作
        /// （在Run或RunByThread方法执行期间，包括暂停期间都视为正在运行(True)，否则为False）
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
        /// 当前批量操作包含的子批量操作
        /// （这些子操作应在MainOperation中进行处理）
        /// </summary>
        public List<ProgressOperation> SubProgressOperations { get; set; }
        #endregion

        #region 事件
        /// <summary>
        /// 批量操作开始时的事件
        /// （在Run方法执行时，在IsRunning = true之前触发）
        /// </summary>
        public event EventHandler OperationStart;
        /// <summary>
        /// 批量操作已被中止的事件
        /// </summary>
        public event EventHandler OperationStoped;
        /// <summary>
        /// 中止批量操作的事件
        /// </summary>
        public event EventHandler OperationStopping;
        /// <summary>
        /// 批量操作正常执行完毕的事件
        /// </summary>
        public event EventHandler OperationFinished;
        /// <summary>
        /// 批量操作执行出现错误的事件
        /// </summary>
        public event EventHandler<OptErrorEventArgs> OperationError;
        /// <summary>
        /// 执行进度改变的事件
        /// </summary>
        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;
        /// <summary>
        /// 执行进度+1的事件
        /// </summary>
        public event EventHandler ProgressAdd;
        #endregion


        /// <summary>
        /// 数据批量操作的信息处理和简单进度控制
        /// </summary>
        public ProgressOperation()
        {
            Init();
        }
        /// <summary>
        /// 数据批量操作的信息处理和简单进度控制
        /// </summary>
        /// <param name="outputDir">导出文件的目录</param>
        public ProgressOperation(string outputDir)
        {
            Init();
            OutputDirectory = outputDir;
        }
        /// <summary>
        /// 数据批量操作的信息处理和简单进度控制
        /// </summary>
        /// <param name="outputDir">成果输出目录</param>
        /// <param name="name">操作的名称</param>
        public ProgressOperation(string outputDir, string name)
        {
            Init();
            OutputDirectory = outputDir;
            Name = name;
            OutputName = name;
            Description = name;
        }
        /// <summary>
        /// 数据批量操作的信息处理和简单进度控制
        /// </summary>
        /// <param name="outputDir">成果输出目录</param>
        /// <param name="name">操作的名称</param>
        /// <param name="description">对操作的描述</param>
        public ProgressOperation(string outputDir, string name, string description)
        {
            Init();
            OutputDirectory = outputDir;
            Name = name;
            OutputName = name;
            Description = description;
        }
        /// <summary>
        /// 初始化各变量
        /// </summary>
        private void Init()
        {
            OverrideFile = true;
            IsRunning = false;
            IsPause = false;
            OperationStart = (sender, e) => { InitToOperation(); IsRunning = true; ProgressMessage = START_MESSAGE; StartTime = System.DateTime.Now; };
            OperationStoped = (sender, e) => { ProgressMessage = STOPPED_MESSAGE; IsRunning = false; EndTime = System.DateTime.Now; };
            OperationFinished = (sender, e) => { ProgressMessage = FINISHED_MESSAGE; IsRunning = false; EndTime = System.DateTime.Now; };
            OperationError = (sender, e) => { EndTime = System.DateTime.Now; };
            SubProgressOperations = new List<ProgressOperation>();
        }


        /// <summary>
        /// 获取提供给外部操作的数据
        /// </summary>
        /// <returns></returns>
        public virtual object GetData()
        {
            return null;//在子类中重写该方法
        }
        /// <summary>
        /// 清空消息等，以准备执行操作
        /// </summary>
        protected void InitToOperation()
        {
            base.ClearMessage();
            StopRunning = false;
        }

        /// <summary>
        /// 批量操作的主体业务处理
        /// </summary>
        protected virtual void MainOperation()
        {
            //在子类中重写该方法
        }

        /// <summary>
        /// 清空消息，执行批量操作
        /// </summary>
        public virtual void Run()
        {
            OnOperationStart();
            try
            {
                MainOperation(); //批量操作的主体业务处理，在子类中重写该方法

                if (StopRunning)
                    OnStopFinished();
                else
                    OnOperationFinished();
            }
            catch (Exception ex) { OnOperationError(ex); IsRunning = false; }
        }
        /// <summary>
        /// 清空消息，创建新线程并在线程中执行批量操作
        /// </summary>
        public virtual void RunByThread()
        {
            OnOperationStart();
            _thread = new Thread(() =>
            {
                try
                {
                    MainOperation(); //批量操作的主体业务处理，在子类中重写该方法

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
            ProgressMessage = STOPPING_MESSAGE;
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

        /// <summary>
        /// 执行OperationStart事件处理
        /// </summary>
        protected void OnOperationStart()
        {
            OperationStart?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// 执行OperationStopping事件处理
        /// </summary>
        protected void OnOperationStopping()
        {
            OperationStopping?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// 执行OperationStoped事件处理
        /// </summary>
        protected void OnStopFinished()
        {
            OperationStoped?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// 执行ExportFinished事件处理
        /// </summary>
        protected void OnOperationFinished()
        {
            OperationFinished?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// 执行OperationError事件处理
        /// </summary>
        /// <param name="ex"></param>
        protected void OnOperationError(Exception ex)
        {
            OperationError?.Invoke(this, new OptErrorEventArgs(ex));
        }
        /// <summary>
        /// 执行ProgressChanged事件处理
        /// </summary>
        /// <param name="i">当前进度值</param>
        /// <param name="sum">总进度值</param>
        protected void OnProgressChanged(int i, int sum)
        {
            ProgressChanged?.Invoke(this, new ProgressChangedEventArgs(i, sum));
        }
        /// <summary>
        /// 执行ProgressChanged事件处理
        /// </summary>
        /// <param name="i">当前进度值</param>
        /// <param name="sum">总进度值</param>
        /// <param name="msg">当前进度信息</param>
        protected void OnProgressChanged(int i, int sum, string msg)
        {
            ProgressChanged?.Invoke(this, new ProgressChangedEventArgs(i, sum, msg));
        }
        /// <summary>
        /// 执行ProgressAdd事件处理
        /// </summary>
        protected void OnProgressAdd()
        {
            ProgressAdd?.Invoke(this, new EventArgs());
        }
    }
}
