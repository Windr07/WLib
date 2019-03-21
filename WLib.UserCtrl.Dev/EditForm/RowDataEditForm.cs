/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using WLib.Attributes;

namespace WLib.UserCtrls.Dev.EditForm
{
    /// <summary>
    /// 对一条数据记录进行编辑的窗体
    /// </summary>
    public partial class RowDataEditForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 被编辑的一条记录
        /// </summary>
        public List<EditItem> EditRowData { get; protected set; }
        /// <summary>
        /// 用于操作该行的ID字段,初始化时赋值
        /// </summary>
        public int MainOpId { get; }


        /// <summary>
        /// 点击保存后激活的事件，有改变的属性值保存在事件参数中
        /// </summary>
        public event EventHandler<SaveEditEventArgs> SaveEditEvent;
        /// <summary>
        /// 激活点击保存事件的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSaveEvent(object sender, SaveEditEventArgs e)
        {
            SaveEditEvent?.Invoke(sender, e);
        }
        /// <summary>
        /// 对一条数据记录进行编辑的窗体
        /// </summary>
        /// <param name="id">该记录的ID</param>
        /// <param name="editRowData">被编辑的一条记录</param>
        public RowDataEditForm(int id, List<EditItem> editRowData)
        {
            InitializeComponent();
            this.EditRowData = editRowData;
            this.MainOpId = id;
        }

        /// <summary>
        /// 显示保存成功或失败的提示
        /// </summary>
        /// <param name="savedState">标示保存成功或失败的枚举</param>
        /// <param name="second">显示成功或失败的提示的时间，若值为小于等于0表示一直保持显示</param>
        /// <param name="message">显示的提示信息，若值为null则按默认显示"保存成功"或"保存失败"</param>
        public void ShowSavedState(ESavedState savedState, int second = 3, string message = null)
        {
            if (savedState == ESavedState.UnSaved)
                return;
            this.panelTips.Visible = true;
            this.panelTips.BackColor = savedState == ESavedState.Success ? Color.LightGreen : Color.Orange;
            if (string.IsNullOrEmpty(message))
                this.lblTips.Text = savedState == ESavedState.Success
                    ? ESavedState.Success.GetDescription()
                    : ESavedState.Fail.GetDescription();
            else
                this.lblTips.Text = message;

            if (second > 0)
            {
                new Thread(() =>
                {
                    Thread.Sleep(second * 1000);
                    Invoke(new Action(() => { this.panelTips.Visible = false; }));
                }).Start();
            }
        }


        private void RowDataEditForm_Load(object sender, EventArgs e)//把数据加载显示到控件中
        {
            for (int i = 0; i < EditRowData.Count; i++)
            {
                this.gridRowData.Rows.Add(EditRowData[i].FieldAliasName, EditRowData[i].FieldValue);
                if (!EditRowData[i].EditAble)
                {
                    this.gridRowData.Rows[i].Cells[1].ReadOnly = true;
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.BackColor = System.Drawing.Color.LightGray;
                    this.gridRowData.Rows[i].Cells[1].Style = style;
                }
            }
            this.gridRowData.ClearSelection();
            this.gridRowData.Refresh();
        }

        private void btnSave_Click(object sender, EventArgs e)//保存
        {
            try
            {
                bool result = true;
                List<EditItem> newEditItems = new List<EditItem>();
                for (int i = 0; i < this.gridRowData.Rows.Count; i++)
                {
                    var value = this.gridRowData.Rows[i].Cells[1].Value?.ToString();
                    var item = EditRowData[i];
                    if (item.FieldType != typeof(string) && value != null)
                        value = value.Trim() == string.Empty ? null : value.Trim();

                    newEditItems.Add(new EditItem(item.FieldName, item.FieldAliasName, value, item.FieldType));
                    var message = item.CheckValue();
                    if (message != null)
                        result = false;
                    this.gridRowData.Rows[i].Cells[1].Style.BackColor = message == null ? Color.White : Color.Orange;
                }
                if (result)
                    OnSaveEvent(this, new SaveEditEventArgs(newEditItems));
                else
                    ShowSavedState(ESavedState.Fail, 3, "保存失败，数据填写有误！");
            }
            catch (Exception ex)
            {
                ShowSavedState(ESavedState.Fail, 3, "保存失败：" + ex.Message);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)//取消
        {
            this.Close();
        }

        private void gridRowData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                    this.gridRowData.ClearSelection();
                else if (e.ColumnIndex == 1 && EditRowData[e.RowIndex].FieldType == typeof(DateTime))
                {
                    var rectangle = this.gridRowData.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                    this.dateTimePicker1.Location = rectangle.Location;
                    this.dateTimePicker1.Size = rectangle.Size;
                    this.dateTimePicker1.Show();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void gridRowData_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gridRowData_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    var item = EditRowData[e.RowIndex];
                    var value = e.FormattedValue.ToString();
                    if (item.FieldType != typeof(string))
                        value = value.Trim() == string.Empty ? null : value.Trim();
                    item.FieldValue = value;
                    var message = item.CheckValue();
                    this.gridRowData.Rows[e.RowIndex].Cells[1].ErrorText = message;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            try
            {
                this.dateTimePicker1.Hide();
                this.gridRowData.SelectedCells[0].Value = this.dateTimePicker1.Value;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
