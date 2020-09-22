using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using WLib.ArcGis.DataCheck.Compare;
using WLib.ArcGis.DataCheck.Compare.Plan;
using WLib.ArcGis.DataCheck.Core;
using WLib.ExtProgress;
using WLib.ExtProgress.Core;
using WLib.ExtProgress.ProEventArgs;

namespace WLib.WinCtrls.Dev.ProgressViewCtrl
{
    /// <summary>
    /// 检核操作（<see cref="Checker"/>）的管理与界面交互
    /// </summary>
    public class CheckProViewManager : DevProgressViewManager
    {
        /// <summary>
        /// 对指定进度操作对象及其子操作添加进度条改变、进度信息改变、执行结束、执行中止、执行异常事件的事件处理
        /// </summary>
        /// <param name="opt"></param>
        /// <param name="isTopOperation">标识是否为顶层进度操作（非子操作）</param>
        protected override void BindEventByOneOpt(IProgressOperation opt, bool isTopOperation)
        {
            if (isTopOperation)//只有顶层进度操作才绑定指定的开始、中止、结束、异常事件处理
            {
                opt.OperationStart += ProgressOperation_OperationStart;
                opt.OperationStoped += ProgressOperation_OperationStoped;
                opt.OperationFinished += ProgressOperation_OperationFinished;
                opt.OperationError += ProgressOperation_OperationError;
            }
            else//只有子操作才绑定的事件处理
            {
                opt.OperationError += (sender, e) => { opt.Msgs.Error = e.OptException.ToString(); opt.Msgs.Info(e.OptException.Message); };
            }

            if (MessageCtrl != null)
            {
                if (opt is Checker checker)
                    checker.Msgs.MessageChanged += this.ProgressOperation_MessageChanged;
                else if (opt is ProLogOperation<IEnumerable<ComparePlan>, CompareRecord> compare)
                    compare.Msgs.MessageChanged += this.ProgressOperation_MessageChanged;
                else
                    opt.Msgs.MessageChanged += base.ProgressOperation_MessageChanged;
            }
            if (GridCtrl != null) opt.DataOutput += ProgressOperation_DataOutput;
            if (ProgressBarCtrl != null) opt.ProgressChanged += ProgressOperation_ProgressChanged;
            if (ProgressBarCtrl != null) opt.ProgressAdd += ProgressOperation_ProgressAdd;

            opt.SubProgressOperations.ForEach(subOpt => BindEventByOneOpt(subOpt, false));
        }

        /// <summary>
        /// 检核过程信息显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ProgressOperation_MessageChanged(object sender, ProMsgChangedEventArgs<ProLogGroup> e)
        {
            FormCtrl.Invoke(new Action(() =>
            {
                if (MessageAppend && e.Group == ProLogGroup.Info)
                {
                    if (AppendBefore)
                        MessageCtrl.Text.Insert(0, e.CurMessage + Environment.NewLine);
                    else
                        MessageCtrl.Text += e.CurMessage + Environment.NewLine;
                }
                else
                    MessageCtrl.Text = e.CurMessage;
                Application.DoEvents();
            }));
        }

        /// <summary>
        /// 检核结果信息实时显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ProgressOperation_DataOutput(object sender, ProDataOutputEventArgs e)
        {
            Datas.Add(e.Data);
            if (Datas.Count == 1)
            {
                GridCtrl.Invoke(new Action(() =>
                {
                    GridCtrl.DataSource = null;
                    GridCtrl.DataSource = new BindingList<object>(Datas);
                    var gridView = GridCtrl.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                    gridView.Columns[nameof(CheckRecord.检查名)].VisibleIndex = 0;
                    gridView.Columns[nameof(CheckRecord.错误级别)].Visible = false;
                    gridView.Columns[nameof(CheckRecord.错误描述)].ColumnEdit = new RepositoryItemMemoEdit();
                }));
            }
            GridCtrl.RefreshDataSource();
        }
    }
}
