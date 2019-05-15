/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017/5/16 11:03:45
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WLib.ExtProgress.ProEventArgs;

namespace WLib.ExtProgress
{
    /// <summary>
    /// 数据批量操作的管理与界面交互
    /// </summary>
    public class ProgressOptManager
    {
        /// <summary>
        /// 执行ProgressOperation操作的窗体
        /// </summary>
        protected Form OptForm;
        /// <summary>
        /// 显示ProgressOperation进度信息的TextBox控件
        /// </summary>
        protected TextBox MessageTextBox;
        /// <summary>
        /// 表示ProgressOperation操作进度的进度条
        /// </summary>
        protected ProgressBar OptProgressBar;
        /// <summary>
        /// 改变界面显示状态的委托，bool值参数代表开始批量操作(true)还是结束批量操作(false)
        /// </summary>
        protected Action<bool> ChangeViews;

        /// <summary>
        /// 是否中止操作
        /// </summary>
        protected bool StopRunning { get; set; }
        /// <summary>
        /// 多项长时间批量处理操作
        /// </summary>
        public List<ProgressOperation> Opts { get; set; }
        /// <summary>
        /// 某项长时间批量处理操作（如果有多个操作，则返回第一个操作）
        /// </summary>
        public ProgressOperation Opt { get => this.Opts.FirstOrDefault(); set { this.Opts.Clear(); this.Opts.Add(value); } }
        /// <summary>
        /// 正在运行的批量处理操作
        /// </summary>
        public ProgressOperation RunningOpt { get; protected set; }
        /// <summary>
        /// 对YYGISLib.Progress.ProgressOperation实例生成的操作信息的日志管理，日志默认存放在“程序目录\Log”目录下
        /// </summary>
        public ProLogManager LogManager { get; protected set; }
        /// <summary>
        /// 数据批量操作的管理与界面交互
        /// </summary>
        public ProgressOptManager()
        {
            Opts = new List<ProgressOperation>();
            LogManager = new ProLogManager();
        }


        /// <summary>
        /// 设置进度条改变、进度信息改变、窗口关闭、执行结束、执行中止、执行异常事件的事件处理
        /// </summary>
        /// <param name="optForm">执行操作的窗体</param>
        /// <param name="messageTextBox">显示进度信息的TextBox控件，可为null</param>
        /// <param name="optProgressBar">DevExpress进度条，可为null</param>
        /// <param name="changeViews">改变界面显示状态的委托</param>
        public virtual void BindEvent(Form optForm, TextBox messageTextBox, ProgressBar optProgressBar, Action<bool> changeViews)
        {
            this.OptForm = optForm;
            this.MessageTextBox = messageTextBox;
            this.OptProgressBar = optProgressBar;
            this.ChangeViews = changeViews;

            this.OptForm.FormClosing += OptForm_Closing;
            this.Opts.ForEach(opt => BindEventByOneOpt(opt, true));
        }
        /// <summary>
        /// 移除事件绑定
        /// </summary>
        public virtual void UnBindEvent()
        {
            this.OptForm.FormClosing -= OptForm_Closing;
            this.Opts.ForEach(UnBindEventByOneOpt);
        }
        /// <summary>
        /// 执行批量操作
        /// </summary>
        public virtual void Run()
        {
            StopRunning = false;
            foreach (var opt in this.Opts)
            {
                if (StopRunning) break;
                (RunningOpt = opt).Run();
            }
        }
        /// <summary>
        /// 创建新线程并在线程中执行批量操作
        /// </summary>
        public virtual void RunByThread()
        {
            StopRunning = false;
            foreach (var opt in this.Opts)
            {
                if (StopRunning) break;
                (RunningOpt = opt).RunByThread();
                Thread.Sleep(60);

                //设置while循环，目的是等待当前操作（RunningOpt）执行完毕之后，再执行下一个操作
                while (true)
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
            this.StopRunning = true;
            this.Opts.ForEach(StopOperation);
        }

        protected void StopOperation(ProgressOperation opt)
        {
            opt.StopRunning = true;
            opt.SubProgressOperations.ForEach(StopOperation);
        }
        /// <summary>
        /// 对指定ProgressOperation对象及其子操作添加进度条改变、进度信息改变、执行结束、执行中止、执行异常事件的事件处理
        /// </summary>
        /// <param name="opt"></param>
        /// <param name="isTopOperation"></param>
        protected void BindEventByOneOpt(ProgressOperation opt, bool isTopOperation)
        {
            if (isTopOperation)
            {
                opt.OperationStart += ProgressOperation_OperationStart;
                opt.OperationFinished += ProgressOperation_OperationFinished;
                opt.OperationStoped += ProgressOperation_OperationStoped;
                opt.OperationError += ProgressOperation_OperationError;
            }

            if (MessageTextBox != null)
                opt.MessageChanged += ProgressOperation_MessageChanged;
            if (OptProgressBar != null)
                opt.ProgressChanged += ProgressOperation_ProgressChanged;
            if (OptProgressBar != null)
                opt.ProgressChanged += ProgressOperation_ProgressAdd;

            opt.SubProgressOperations.ForEach(subOpt => BindEventByOneOpt(subOpt, false));
        }
        /// <summary>
        ///  对指定ProgressOperation对象及其子操作移除进度条改变、进度信息改变、执行结束、执行中止、执行异常事件的事件处理
        /// </summary>
        /// <param name="opt"></param>
        protected void UnBindEventByOneOpt(ProgressOperation opt)
        {
            opt.OperationStart -= ProgressOperation_OperationStart;
            opt.OperationFinished -= ProgressOperation_OperationFinished;
            opt.OperationStoped -= ProgressOperation_OperationStoped;
            opt.OperationError -= ProgressOperation_OperationError;
            opt.MessageChanged -= ProgressOperation_MessageChanged;
            opt.ProgressChanged -= ProgressOperation_ProgressChanged;
            opt.ProgressAdd -= ProgressOperation_ProgressAdd;
            opt.ProgressGroupMessage.MessageChanged -= ProgressGroupMessage_MessageChanged;

            opt.SubProgressOperations.ForEach(UnBindEventByOneOpt);
        }


        protected virtual void OptForm_Closing(object sender, FormClosingEventArgs e)
        {
            if (RunningOpt != null && sender is Form optForm && !optForm.IsDisposed)
            {
                if (MessageBox.Show($"确定要停止并关闭{RunningOpt.Name}的操作吗？", "停止",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    RunningOpt.OperationStoped += (sender2, e2) =>
                    {
                        this.UnBindEvent();
                        optForm.Invoke(new Action(() => { optForm.Close(); }));
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
            ChangeViews(true);
        }

        protected virtual void ProgressOperation_MessageChanged(object sender, MsgChangedEventArgs e)
        {
            OptForm.Invoke(new Action(() =>
            {
                MessageTextBox.Text = MessageTextBox.Text.Insert(0, e.Message + Environment.NewLine);
                Application.DoEvents();
            }));
        }

        protected virtual void ProgressOperation_OperationError(object sender, OptErrorEventArgs e)
        {
            OptForm.Invoke(new Action(() =>
            {
#if DEBUG
                string errorMsg = e.OptException.ToString();
#else
                string errorMsg = e.OptException.Message;
#endif
                if (MessageTextBox != null)
                    MessageTextBox.Text = MessageTextBox.Text.Insert(0, errorMsg + Environment.NewLine);

                var opt = sender as ProgressOperation;
                LogManager.WriteLog(opt, e.OptException.ToString());
                MessageBox.Show(errorMsg, opt.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ChangeViews(false);
                RunningOpt = null;
            }));
        }

        protected virtual void ProgressOperation_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OptForm.Invoke(new Action(() =>
            {
                OptProgressBar.Maximum = e.MaxValue;
                OptProgressBar.Value = e.CurValue;
                Application.DoEvents();
            }));
        }

        protected virtual void ProgressOperation_ProgressAdd(object sender, EventArgs e)
        {
            OptForm.Invoke(new Action(() =>
            {
                OptProgressBar.Value++;
                Application.DoEvents();
            }));
        }

        protected virtual void ProgressOperation_OperationStoped(object sender, EventArgs e)
        {
            OptForm.Invoke(new Action(() =>
            {
                OptForm.FormClosing -= OptForm_Closing;
                var opt = sender as ProgressOperation;
                LogManager.WriteLog(opt);
                ChangeViews(false);
                MessageBox.Show("用户中止操作！", opt.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                RunningOpt = null;
            }));
        }

        protected virtual void ProgressOperation_OperationFinished(object sender, EventArgs e)
        {
            OptForm.Invoke(new Action(() =>
            {
                OptForm.FormClosing -= OptForm_Closing;
                var opt = sender as ProgressOperation;
                LogManager.WriteLog(opt);
                ChangeViews(false);
                MessageBox.Show("全部处理完成！", opt.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                RunningOpt = null;
            }));
        }

        protected virtual void ProgressGroupMessage_MessageChanged(object sender, GroupMsgChangedEventArgs e)
        {
            //此处暂无处理代码，可在子类中实现处理
        }
    }
}
