/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2016
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Windows.Forms;
using WLib.ExtProgress;
using WLib.ExtProgress.Core;
using WLib.ExtProgress.ProEventArgs;

namespace WLib.WinCtrls.Dev
{
    /// <summary>
    /// （数据库建库/成果输出/出图等）批量处理操作的管理与界面交互
    /// </summary>
    public class DevProgressViewManager : ProgressViewManager
    {
        /// <summary>
        /// 进度条
        /// </summary>
        protected new ProgressBarControl ProgressBarCtrl { get; set; }
        /// <summary>
        /// 显示进度信息的GridView控件
        /// </summary>
        protected GridView GridView { get; set; }
        /// <summary>
        /// 存储进度信息DataTable
        /// </summary>
        protected DataTable ProDataTable { get; set; }
        /// <summary>
        /// 存储进度信息DataTable的列数
        /// </summary>
        protected int DataTableColumnCount { get; set; }


        /// <summary>
        /// 设置进度条改变、进度信息改变、窗口关闭、执行结束、执行中止、执行异常事件的事件处理
        /// </summary>
        /// <param name="formCtrl">执行操作的窗体</param>
        /// <param name="messageCtrl">显示进度信息的控件，可为null</param>
        /// <param name="progressBarCtrl">DevExpress进度条，可为null</param>
        /// <param name="changeViews">改变界面显示状态的委托，bool值参数代表开始批量操作(true)还是结束批量操作(false)</param>
        public void BindEvent(Form formCtrl, Control messageCtrl, ProgressBarControl progressBarCtrl, Action<bool> changeViews)
        {
            FormCtrl = formCtrl;
            MessageCtrl = messageCtrl;
            ProgressBarCtrl = progressBarCtrl;
            ChangeViews = changeViews;
            FormCtrl.FormClosing -= OptForm_Closing;
            FormCtrl.FormClosing += OptForm_Closing;
            Opts.ForEach(opt => BindEventByOneOpt(opt, true));
        }
        /// <summary>
        /// 设置进度条改变、进度信息改变、窗口关闭、执行结束、执行中止、执行异常事件的事件处理
        /// </summary>
        /// <param name="optForm">操作窗体</param>
        /// <param name="progressBar">DevExpress进度条，可为null</param>
        /// <param name="gridView">显示进度信息的GridView控件</param>
        /// <param name="dataTable">存储进度信息DataTable</param>
        /// <param name="changeViews">改变界面显示状态的委托，bool值参数代表开始批量操作(true)还是结束批量操作(false)</param>
        public void BindEvent(Form optForm, ProgressBarControl progressBar, GridView gridView, DataTable dataTable, Action<bool> changeViews)
        {
            FormCtrl = optForm;
            ProgressBarCtrl = progressBar;
            GridView = gridView;
            ProDataTable = dataTable;
            DataTableColumnCount = dataTable.Columns.Count;
            ChangeViews = changeViews;
            FormCtrl.FormClosing += OptForm_Closing;
            Opts.ForEach(opt => BindEventByOneOpt(opt, true));
        }
        /// <summary>
        /// 对指定进度操作对象及其子操作添加进度条改变、进度信息改变、执行结束、执行中止、执行异常事件的事件处理
        /// </summary>
        /// <param name="opt"></param>
        /// <param name="isTopOperation"></param>
        protected new void BindEventByOneOpt(IProgressOperation opt, bool isTopOperation)
        {
            if (isTopOperation)
            {
                opt.OperationStart += ProgressOperation_OperationStart;
                opt.OperationFinished += ProgressOperation_OperationFinished;
                opt.OperationStoped += ProgressOperation_OperationStoped;
                opt.OperationError += ProgressOperation_OperationError;
            }

            if (MessageCtrl != null) opt.Msgs.MessageChanged += ProgressOperation_MessageChanged;
            if (GridView != null) opt.Msgs.MessageChanged += ProgressOperation_MessageChanged_GridView;
            if (ProgressBarCtrl != null) opt.ProgressChanged += ProgressOperation_ProgressChanged;
            if (ProgressBarCtrl != null) opt.ProgressAdd += ProgressOperation_ProgressAdd;

            opt.SubProgressOperations.ForEach(subOpt => BindEventByOneOpt(subOpt, false));
        }


        protected override void OptForm_Closing(object sender, FormClosingEventArgs e)
        {
            base.OptForm_Closing(sender, e);
        }

        protected override void ProgressOperation_MessageChanged(object sender, ProMsgChangedEventArgs e)
        {
            FormCtrl.Invoke(new Action(() => { MessageCtrl.Text = MessageCtrl.Text.Insert(0, e.CurMessage + Environment.NewLine); Application.DoEvents(); }));
        }

        protected void ProgressOperation_MessageChanged_GridView(object sender, ProMsgChangedEventArgs e)
        {
            ProDataTable.Rows.Add(e.CurMessage.Split(new char[] { ',' }, DataTableColumnCount));
            GridView.FocusedRowHandle = this.GridView.DataRowCount - 1;
            Application.DoEvents();
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
                    MessageCtrl.Text = MessageCtrl.Text.Insert(0, errorMsg + Environment.NewLine);

                var opt = sender as IProgressOperation;
                LogManager.WriteLog(opt, e.OptException.ToString());
                MessageBox.Show(errorMsg, opt.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ChangeViews(false);
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
    }
}
