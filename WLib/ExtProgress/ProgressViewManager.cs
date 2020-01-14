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
using WLib.ExtProgress.Core;
using WLib.ExtProgress.ProEventArgs;

namespace WLib.ExtProgress
{
    /// <summary>
    /// 进度操作的管理与界面交互
    /// </summary>
    public class ProgressViewManager
    {
        /// <summary>
        /// 执行进度操作的窗体
        /// </summary>
        protected Form FormCtrl { get; set; }
        /// <summary>
        /// 显示进度信息的控件
        /// </summary>
        protected Control MessageCtrl { get; set; }
        /// <summary>
        /// 表示进度操作进度条
        /// </summary>
        protected ProgressBar ProgressBarCtrl { get; set; }
        /// <summary>
        /// 改变界面显示状态的委托，bool值参数代表开始操作(true)还是结束操作(false)
        /// </summary>
        protected Action<bool> ChangeViews { get; set; }

        /// <summary>
        /// 是否中止操作
        /// </summary>
        public bool StopRunning { get; protected set; }
        /// <summary>
        /// 多项可进度控制的操作
        /// </summary>
        public List<IProgressOperation> Opts { get; set; } = new List<IProgressOperation>();
        /// <summary>
        /// 某项可进度控制的操作（如果有多个操作，则返回第一个操作）
        /// </summary>
        public IProgressOperation Opt { get => Opts.FirstOrDefault(); set { Opts.Clear(); Opts.Add(value); } }
        /// <summary>
        /// 正在运行的操作
        /// </summary>
        public IProgressOperation RunningOpt { get; protected set; }
        /// <summary>
        /// 进度信息的日志管理，日志默认存放在“程序目录\Log”目录下
        /// </summary>
        public ProLogManager LogManager { get; protected set; } = new ProLogManager();
        /// <summary>
        /// 进度操作的管理与界面交互
        /// </summary>
        public ProgressViewManager() { }


        /// <summary>
        /// 设置进度条改变、进度信息改变、窗口关闭、执行结束、执行中止、执行异常事件的事件处理
        /// </summary>
        /// <param name="fromCtrl">执行操作的窗体</param>
        /// <param name="messageCtrl">显示进度信息的控件，可为null</param>
        /// <param name="progressBarCtrl">进度条，可为null</param>
        /// <param name="changeViews">改变界面显示状态的委托</param>
        public virtual void BindEvent(Form fromCtrl, Control messageCtrl, ProgressBar progressBarCtrl, Action<bool> changeViews)
        {
            FormCtrl = fromCtrl;
            MessageCtrl = messageCtrl;
            ProgressBarCtrl = progressBarCtrl;
            ChangeViews = changeViews;
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
        protected void StopOperation(IProgressOperation opt)
        {
            opt.Stop();
            opt.SubProgressOperations.ForEach(StopOperation);
        }
        /// <summary>
        /// 对指定进度操作对象及其子操作添加进度条改变、进度信息改变、执行结束、执行中止、执行异常事件的事件处理
        /// </summary>
        /// <param name="opt"></param>
        /// <param name="isTopOperation"></param>
        protected void BindEventByOneOpt(IProgressOperation opt, bool isTopOperation)
        {
            if (isTopOperation)
            {
                opt.OperationStart += ProgressOperation_OperationStart;
                opt.OperationFinished += ProgressOperation_OperationFinished;
                opt.OperationStoped += ProgressOperation_OperationStoped;
                opt.OperationError += ProgressOperation_OperationError;
            }

            if (MessageCtrl != null) opt.Msgs.MessageChanged += ProgressOperation_MessageChanged;
            if (ProgressBarCtrl != null) opt.ProgressChanged += ProgressOperation_ProgressChanged;
            if (ProgressBarCtrl != null) opt.ProgressAdd += ProgressOperation_ProgressAdd;

            opt.SubProgressOperations.ForEach(subOpt => BindEventByOneOpt(subOpt, false));
        }
        /// <summary>
        /// 对指定进度操作对象及其子操作移除进度条改变、进度信息改变、执行结束、执行中止、执行异常事件的事件处理
        /// </summary>
        /// <param name="opt"></param>
        protected void UnBindEventByOneOpt(IProgressOperation opt)
        {
            opt.OperationStart -= ProgressOperation_OperationStart;
            opt.OperationFinished -= ProgressOperation_OperationFinished;
            opt.OperationStoped -= ProgressOperation_OperationStoped;
            opt.OperationError -= ProgressOperation_OperationError;
            opt.Msgs.MessageChanged -= ProgressOperation_MessageChanged;
            opt.ProgressChanged -= ProgressOperation_ProgressChanged;
            opt.ProgressAdd -= ProgressOperation_ProgressAdd;

            opt.SubProgressOperations.ForEach(UnBindEventByOneOpt);
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
            ChangeViews(true);
        }

        protected virtual void ProgressOperation_MessageChanged(object sender, ProMsgChangedEventArgs e)
        {
            FormCtrl.Invoke(new Action(() =>
            {
                MessageCtrl.Text = MessageCtrl.Text.Insert(0, e.CurMessage + Environment.NewLine);
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
                    MessageCtrl.Text = MessageCtrl.Text.Insert(0, errorMsg + Environment.NewLine);

                var opt = sender as IProgressOperation;
                LogManager.WriteLog(opt, e.OptException.ToString());
                MessageBox.Show(errorMsg, opt.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ChangeViews(false);
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
                LogManager.WriteLog(opt);
                ChangeViews(false);
                MessageBox.Show("用户中止操作！", opt.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                RunningOpt = null;
            }));
        }

        protected virtual void ProgressOperation_OperationFinished(object sender, EventArgs e)
        {
            FormCtrl.Invoke(new Action(() =>
            {
                FormCtrl.FormClosing -= OptForm_Closing;
                var opt = sender as IProgressOperation;
                LogManager.WriteLog(opt);
                ChangeViews(false);
                MessageBox.Show("全部处理完成！", opt.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                RunningOpt = null;
            }));
        }
    }
}
