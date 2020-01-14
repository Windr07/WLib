/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2016
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using WLib.ExtProgress;
using WLib.ExtProgress.Core;
using WLib.ExtProgress.ProEventArgs;
using WLib.WinCtrls.ProgressViewCtrl;
using WLib.WinCtrls.MessageCtrl;
using DevExpress.XtraGrid;
using System.Collections.Generic;
using System.ComponentModel;

namespace WLib.WinCtrls.Dev.ProgressViewCtrl
{
    /// <summary>
    /// 进度操作的管理与界面交互
    /// </summary>
    public class DevProgressViewManager : ProgressViewManager
    {
        /// <summary>
        /// 进度条
        /// </summary>
        public new ProgressBarControl ProgressBarCtrl { get; set; }
        /// <summary>
        /// 进度操作输出的实时数据
        /// </summary>
        public List<object> Datas { get; } = new List<object>();
        /// <summary>
        /// 显示进度操作实时数据输出数据的表格控件
        /// </summary>
        public GridControl GridCtrl { get; set; }


        /// <summary>
        /// 存储进度信息DataTable的列数
        /// </summary>
        public int DataTableColumnCount { get; set; }


        /// <summary>
        /// 设置与进度操作相关的控件，设置进度条改变、进度信息改变、窗口关闭、执行结束、执行中止、执行异常事件的事件处理
        /// </summary>
        /// <param name="formCtrl">执行操作的窗体</param>
        /// <param name="progressBarCtrl">DevExpress进度条，可为null</param>
        /// <param name="messageCtrl">显示进度信息的控件，可为null</param>
        /// <param name="changeView">改变界面显示状态的委托，bool值参数代表开始批量操作(true)还是结束批量操作(false)</param>
        public void BindEvent(Form formCtrl, ProgressBarControl progressBarCtrl, Control messageCtrl, Action<bool> changeView)
        {
            FormCtrl = formCtrl;
            MessageCtrl = messageCtrl;
            ProgressBarCtrl = progressBarCtrl;
            ChangeView += changeView;
            FormCtrl.FormClosing -= OptForm_Closing;
            FormCtrl.FormClosing += OptForm_Closing;
            Opts.ForEach(opt => BindEventByOneOpt(opt, true));
        }
        /// <summary>
        /// 设置与进度操作相关的控件，设置进度条改变、进度信息改变、实时数据输出、窗口关闭、执行结束、执行中止、执行异常事件的事件处理
        /// </summary>
        /// <param name="formCtrl">执行操作的窗体</param>
        /// <param name="progressBarCtrl">DevExpress进度条，可为null</param>
        /// <param name="gridCtrl">用于显示输出实时数据的表格控件</param>
        /// <param name="changeView">改变界面显示状态的委托，bool值参数代表开始批量操作(true)还是结束批量操作(false)</param>
        public void BindEvent(Form formCtrl, ProgressBarControl progressBarCtrl, Control messageCtrl, GridControl gridCtrl, Action<bool> changeView)
        {
            GridCtrl = gridCtrl;
            BindEvent(formCtrl, progressBarCtrl, messageCtrl, changeView);
        }
        /// <summary>
        /// 对指定进度操作对象及其子操作添加进度条改变、进度信息改变、执行结束、执行中止、执行异常事件的事件处理
        /// </summary>
        /// <param name="opt"></param>
        /// <param name="isTopOperation">标识是否为顶层进度操作（非子操作）</param>
        protected new void BindEventByOneOpt(IProgressOperation opt, bool isTopOperation)
        {
            if (isTopOperation)//只有顶层进度操作才绑定指定的开始、中止、结束、异常事件处理
            {
                opt.OperationStart += ProgressOperation_OperationStart;
                opt.OperationStoped += ProgressOperation_OperationStoped;
                opt.OperationFinished += ProgressOperation_OperationFinished;
                opt.OperationError += ProgressOperation_OperationError;
            }
            else//子操作事件处理
            {
                opt.OperationError += (sender, e) => { opt.Msgs.Error = e.OptException.ToString(); opt.Msgs.Info(e.OptException.Message); };
            }

            if (MessageCtrl != null) opt.Msgs.MessageChanged += ProgressOperation_MessageChanged;
            if (GridCtrl != null) opt.ProgressDataOuput += ProgressOperation_ProgressDataOuput;
            if (ProgressBarCtrl != null) opt.ProgressChanged += ProgressOperation_ProgressChanged;
            if (ProgressBarCtrl != null) opt.ProgressAdd += ProgressOperation_ProgressAdd;

            opt.SubProgressOperations.ForEach(subOpt => BindEventByOneOpt(subOpt, false));
        }



        protected override void OptForm_Closing(object sender, FormClosingEventArgs e)
        {
            base.OptForm_Closing(sender, e);
        }

        protected void ProgressOperation_ProgressDataOuput(object sender, ProDataOutputEventArgs e)
        {
            Datas.Add(e.Data);
            if (Datas.Count == 1)
            {
                GridCtrl.Invoke(new Action(() =>
                {
                    GridCtrl.DataSource = null;
                    GridCtrl.DataSource = new BindingList<object>(Datas);
                }));
            }
            GridCtrl.RefreshDataSource();
        }

        protected override void ProgressOperation_OperationError(object sender, ProErrorEventArgs e)
        {
            if (!FormCtrl.IsHandleCreated)
                return;
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
                MessageBoxEx.ShowError(e.OptException);
                ChangeView?.Invoke(false);
                RunningOpt = null;
            }));
        }

        protected override void ProgressOperation_ProgressChanged(object sender, ProChangedEventArgs e)
        {
            FormCtrl.Invoke(new Action(() =>
            {
                ProgressBarCtrl.Properties.Maximum = e.MaxValue;
                ProgressBarCtrl.Position = e.CurValue;
                Application.DoEvents();
            }));
        }

        protected override void ProgressOperation_ProgressAdd(object sender, EventArgs e)
        {
            FormCtrl.Invoke(new Action(() =>
            {
                ProgressBarCtrl.Position++;
                Application.DoEvents();
            }));
        }

        protected override void ProgressOperation_OperationFinished(object sender, EventArgs e)
        {
            FormCtrl.Invoke(new Action(() =>
            {
                FormCtrl.FormClosing -= OptForm_Closing;
                var opt = sender as IProgressOperation;
                opt.WriteLogFile();
                ChangeView?.Invoke(false);
                MessageBoxEx.ShowInfo(FormCtrl, "全部处理完成！");
                RunningOpt = null;
            }));
        }
    }
}
