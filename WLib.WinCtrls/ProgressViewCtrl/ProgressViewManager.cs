/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017/5/16 11:03:45
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WLib.ExtProgress;
using WLib.ExtProgress.Core;
using WLib.ExtProgress.ProEventArgs;
using WLib.WinCtrls.MessageCtrl;

namespace WLib.WinCtrls.ProgressViewCtrl
{
    /// <summary>
    /// 进度操作的管理与界面交互
    /// </summary>
    public class ProgressViewManager
    {
        /// <summary>
        /// 执行进度操作的窗体
        /// </summary>
        public Form FormCtrl { get; set; }
        /// <summary>
        /// 显示进度信息的控件
        /// </summary>
        public Control MessageCtrl { get; set; }
        /// <summary>
        /// 显示进度操作实时数据输出数据的表格控件
        /// </summary>
        public DataGridView GridView { get; set; }
        /// <summary>
        /// 表示进度操作进度条
        /// </summary>
        public ProgressBar ProgressBarCtrl { get; set; }

        /// <summary>
        /// 表示以追加（True）还是替换（False）的方式，将信息显示到消息控件<see cref="MessageCtrl"/>中
        /// <para>默认为True，即追加信息</para>
        /// </summary>
        public bool MessageAppend { get; set; } = true;
        /// <summary>
        /// 当消息以追加（<see cref="MessageAppend"/> = true）方式显示时，追加的消息是插入到原消息的开头（true）还是结尾（false，默认）
        /// </summary>
        public bool AppendBefore { get; set; }
        /// <summary>
        /// 改变界面显示状态的委托，bool值参数代表开始操作(true)还是结束操作(false)
        /// </summary>
        public Action<bool> ChangeView { get; set; }
        /// <summary>
        /// 是否中止操作
        /// </summary>
        public bool StopRunning { get; protected set; }
        /// <summary>
        /// 进度操作输出的实时数据
        /// </summary>
        public List<object> Datas { get; } = new List<object>();
        /// <summary>
        /// 多项可进度控制的操作
        /// </summary>
        public List<IProgressOperation> Opts { get; set; } = new List<IProgressOperation>();
        /// <summary>
        /// 可进度控制的操作（如果有多个操作，则返回第一个操作）
        /// </summary>
        public IProgressOperation Opt { get => Opts.FirstOrDefault(); set { Opts.Clear(); Opts.Add(value); } }
        /// <summary>
        /// 正在运行的操作
        /// </summary>
        public IProgressOperation RunningOpt { get; protected set; }
        /// <summary>
        /// 是否在操作完成、中止出现时弹出提示框
        /// </summary>

        public bool ShowInfoBox { get; set; } = true;
        /// <summary>
        /// 是否在操作异常出现时弹出提示框
        /// </summary>

        public bool ShowErrorBox { get; set; } = true;


        /// <summary>
        /// 进度操作的管理与界面交互
        /// </summary>
        public ProgressViewManager() { }
        /// <summary>
        /// 设置与进度操作相关的控件，设置进度条改变、进度信息改变、窗口关闭、执行结束、执行中止、执行异常事件的事件处理
        /// </summary>
        /// <param name="fromCtrl">执行操作的窗体</param>
        /// <param name="progressBarCtrl">进度条，可为null</param>
        /// <param name="messageCtrl">显示进度信息的控件，可为null</param>
        /// <param name="gridView">显示进度操作实时数据输出数据的表格控件</param>
        /// <param name="changeView">改变界面显示状态的委托</param>
        public virtual void BindEvent(Form fromCtrl, ProgressBar progressBarCtrl, Control messageCtrl, DataGridView gridView, Action<bool> changeView)
        {
            if (Opt == null)
                throw new Exception($"“{nameof(Opt)}”对象为空，请先对“{nameof(Opt)}”对象赋值，然后再调用“{nameof(BindEvent)}”方法对事件进行绑定！");

            FormCtrl = fromCtrl;
            MessageCtrl = messageCtrl;
            ProgressBarCtrl = progressBarCtrl;
            GridView = gridView;
            ChangeView += changeView;

            FormCtrl.FormClosing -= OptForm_Closing;
            FormCtrl.FormClosing += OptForm_Closing;
            Opts.ForEach(opt => BindEventByOneOpt(opt, true));
        }
        /// <summary>
        /// 移除事件绑定
        /// </summary>
        public virtual void UnBindEvent()
        {
            FormCtrl.FormClosing -= OptForm_Closing;
            Opts.ForEach(UnBindEventByOneOpt);
        }
        /// <summary>
        /// 执行操作
        /// </summary>
        public virtual void Run()
        {
            StopRunning = false;
            foreach (var opt in Opts)
            {
                if (StopRunning) break;
                (RunningOpt = opt).Run();
            }
        }
        /// <summary>
        /// 异步执行操作
        /// </summary>
        public async virtual Task RunAsync()
        {
            StopRunning = false;
            foreach (var opt in Opts)
            {
                if (StopRunning) break;
                await (RunningOpt = opt).RunAsync();
            }
        }

        /// <summary>
        /// 创建新线程并在线程中执行操作
        /// </summary>
        public virtual void RunByThread()
        {
            StopRunning = false;
            foreach (var opt in Opts)
            {
                if (StopRunning) break;
                (RunningOpt = opt).RunByThread();
                Thread.Sleep(60);

                while (true)//设置while循环，目的是等待当前操作（RunningOpt）执行完毕之后，再执行下一个操作
                {
                    Application.DoEvents();
                    if (StopRunning || RunningOpt == null || !RunningOpt.IsRunning)
                        break;
                }
            }
        }
        /// <summary>
        /// 停止操作
        /// </summary>
        public virtual void Stop()
        {
            StopRunning = true;
            Opts.ForEach(StopOperation);
        }


        /// <summary>
        /// 设置状态为中止操作
        /// </summary>
        /// <param name="opt"></param>
        protected virtual void StopOperation(IProgressOperation opt)
        {
            opt.Stop();
            opt.SubProgressOperations.ForEach(StopOperation);
        }
        /// <summary>
        /// 对指定进度操作对象及其子操作添加进度条改变、进度信息改变、执行结束、执行中止、执行异常事件的事件处理
        /// </summary>
        /// <param name="opt"></param>
        /// <param name="isTopOperation">标识是否为顶层进度操作（非子操作）</param>
        protected virtual void BindEventByOneOpt(IProgressOperation opt, bool isTopOperation)
        {
            if (isTopOperation)//只有顶层进度操作才绑定开始、中止、结束、异常事件处理
            {
                opt.OperationStart += ProgressOperation_OperationStart;
                opt.OperationStoped += ProgressOperation_OperationStoped;
                opt.OperationFinished += ProgressOperation_OperationFinished;
                opt.OperationError += ProgressOperation_OperationError;
            }
            else//子操作事件处理
            {
                opt.OperationError += Sub_ProgressOperation_OperationError;
            }

            if (MessageCtrl != null) opt.Msgs.MessageChanged += ProgressOperation_MessageChanged;
            if (GridView != null) opt.DataOutput += ProgressOperation_DataOutput;
            if (ProgressBarCtrl != null) opt.ProgressChanged += ProgressOperation_ProgressChanged;
            if (ProgressBarCtrl != null) opt.ProgressAdd += ProgressOperation_ProgressAdd;

            opt.SubProgressOperations.ForEach(subOpt => BindEventByOneOpt(subOpt, false));
        }


        /// <summary>
        /// 对指定进度操作对象及其子操作移除进度条改变、进度信息改变、执行结束、执行中止、执行异常事件的事件处理
        /// </summary>
        /// <param name="opt"></param>
        protected virtual void UnBindEventByOneOpt(IProgressOperation opt)
        {
            opt.OperationStart -= ProgressOperation_OperationStart;
            opt.OperationFinished -= ProgressOperation_OperationFinished;
            opt.OperationStoped -= ProgressOperation_OperationStoped;
            opt.OperationError -= ProgressOperation_OperationError;
            opt.DataOutput -= ProgressOperation_DataOutput;
            opt.Msgs.MessageChanged -= ProgressOperation_MessageChanged;
            opt.ProgressChanged -= ProgressOperation_ProgressChanged;
            opt.ProgressAdd -= ProgressOperation_ProgressAdd;
            opt.OperationError -= Sub_ProgressOperation_OperationError;

            opt.SubProgressOperations.ForEach(UnBindEventByOneOpt);
        }

        protected virtual void ProgressOperation_DataOutput(object sender, ProDataOutputEventArgs e)
        {
            Datas.Add(e.Data);
            if (Datas.Count == 1)
            {
                GridView.Invoke(new Action(() =>
                {
                    GridView.DataSource = null;
                    GridView.DataSource = new BindingList<object>(Datas);
                }));
            }
        }

        protected virtual void OptForm_Closing(object sender, FormClosingEventArgs e)
        {
            if (RunningOpt != null && sender is Form optForm && !optForm.IsDisposed)
            {
                if (MessageBox.Show($"确定要停止并关闭{RunningOpt.Name}的操作吗？", optForm.Text,
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    RunningOpt.OperationStoped += (sender2, e2) =>
                    {
                        UnBindEvent();
                        optForm.Invoke(new Action(() => optForm.Close()));
                    };
                    Stop();
                    if (RunningOpt.IsRunning)
                        e.Cancel = true;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = false;
            }
        }

        protected virtual void ProgressOperation_OperationStart(object sender, EventArgs e)
        {
            if (ChangeView != null) FormCtrl.Invoke(new Action(() => ChangeView.Invoke(true)));
            //ChangeView?.Invoke(true);
        }

        protected virtual void ProgressOperation_MessageChanged(object sender, ProMsgChangedEventArgs e)
        {
            FormCtrl.Invoke(new Action(() =>
            {
                if (MessageAppend)
                {
                    if (AppendBefore) MessageCtrl.Text.Insert(0, e.CurMessage + Environment.NewLine);
                    else MessageCtrl.Text += e.CurMessage + Environment.NewLine;
                }
                else MessageCtrl.Text = e.CurMessage;
                Application.DoEvents();
            }));
        }

        protected virtual void ProgressOperation_OperationError(object sender, ProErrorEventArgs e)
        {
            FormCtrl.Invoke(new Action(() =>
            {
#if DEBUG
                string errorMsg = e.OptException.ToString();
#else
                string errorMsg = e.OptException.Message;
#endif
                if (MessageCtrl != null)
                    MessageCtrl.Text = MessageAppend ? MessageCtrl.Text.Insert(0, errorMsg + Environment.NewLine) : errorMsg;

                var opt = sender as IProgressOperation;
                opt.Msgs.Error = e.OptException.ToString();
                opt.WriteLogFile();
                if (ShowErrorBox) MessageBoxEx.ShowError(e.OptException);
                ChangeView?.Invoke(false);
                RunningOpt = null;
            }));
        }

        protected virtual void ProgressOperation_ProgressChanged(object sender, ProChangedEventArgs e)
        {
            FormCtrl.Invoke(new Action(() =>
            {
                ProgressBarCtrl.Maximum = e.MaxValue;
                ProgressBarCtrl.Value = e.CurValue;
                Application.DoEvents();
            }));
        }

        protected virtual void ProgressOperation_ProgressAdd(object sender, EventArgs e)
        {
            FormCtrl.Invoke(new Action(() =>
            {
                ProgressBarCtrl.Value++;
                Application.DoEvents();
            }));
        }

        protected virtual void ProgressOperation_OperationStoped(object sender, EventArgs e)
        {
            FormCtrl.Invoke(new Action(() =>
            {
                FormCtrl.FormClosing -= OptForm_Closing;
                var opt = sender as IProgressOperation;
                opt.WriteLogFile();
                ChangeView?.Invoke(false);
                if (ShowInfoBox) MessageBoxEx.ShowInfo(FormCtrl, "用户中止操作！");
                RunningOpt = null;
            }));
        }

        protected virtual void ProgressOperation_OperationFinished(object sender, EventArgs e)
        {
            FormCtrl.Invoke(new Action(() =>
            {
                FormCtrl.FormClosing -= OptForm_Closing;
                var opt = sender as IProgressOperation;
                opt.WriteLogFile();
                ChangeView?.Invoke(false);
                if (ShowInfoBox) MessageBoxEx.ShowInfo(FormCtrl, "全部处理完成！");
                RunningOpt = null;
            }));
        }

        protected virtual void Sub_ProgressOperation_OperationError(object sender, ProErrorEventArgs e)
        {
            var opt = (IProgressOperation)sender;
            opt.Msgs.Error = e.OptException.ToString();
            opt.Msgs.Info(e.OptException.Message);
        }
    }
}
