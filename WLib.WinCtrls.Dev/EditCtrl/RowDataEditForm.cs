/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WLib.Attributes.Description;
using WLib.Database.TableInfo;
using WLib.Reflection;

namespace WLib.WinCtrls.Dev.EditCtrl
{
    /// <summary>
    /// 编辑或新增一条数据记录的窗体
    /// </summary>
    public partial class RowDataEditForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 被编辑的一条记录
        /// </summary>
        public List<EditItem> EditItems { get; protected set; } = new List<EditItem>();
        /// <summary>
        /// 字段列（第一列）显示的宽度
        /// </summary>
        public int FieldColumnLength { get => colFieldName.Width; set => colFieldName.Width = value; }
        /// <summary>
        /// 用于操作该行的ID字段
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// ID字段
        /// </summary>
        public string IdField { get; set; }
        /// <summary>
        /// 数据操作状态，0为编辑，1为新增
        /// </summary>
        public EOptState OptState { get; set; }
        /// <summary>
        /// 保存编辑事件，即点击保存按钮且通过字段验证后激活的事件
        /// </summary>
        public event EventHandler<SaveEditEventArgs> SaveEditEvent;
        /// <summary>
        /// 编辑或新增一条数据记录的窗体
        /// </summary>
        public RowDataEditForm()
        {
            InitializeComponent();
            this.gridRowData.Controls.Add(this.dateTimePicker1);
            this.gridRowData.Controls.Add(this.comboBox1);
            this.gridRowData.Controls.Add(this.textEdit1);
          
            this.timer1.Tick += (sender, e) =>
            {
                this.panelTips.Visible = false;
                this.timer1.Enabled = false;
            };
        }


        /// <summary>
        /// 对一条数据记录进行编辑的窗体
        /// </summary>
        /// <param name="id">该记录的ID</param>
        /// <param name="editItems">被编辑的一条记录</param>
        public void LoadData(List<EditItem> editItems, string id = null, EOptState optState = 0)
        {
            this.EditItems = editItems;
            this.Id = id;
            this.OptState = optState;
            ShowDataToControl();
        }
        /// <summary>
        /// 对一条数据记录进行编辑的窗体
        /// </summary>
        /// <param name="id">该记录的ID</param>
        /// <param name="obj">被编辑的一条记录</param>
        public void LoadData(object obj, string id = null, EOptState optState = 0)
        {
            this.EditItems = TableInfoHelper.CreateEditItemWithAttributes(obj).ToList();
            this.Id = id;
            this.OptState = optState;
            ShowDataToControl();
        }
        /// <summary>
        /// 对一条数据记录进行编辑的窗体
        /// </summary>
        /// <param name="id">该记录的ID</param>
        /// <param name="dataTable">被编辑的表格</param>
        /// <param name="rowIndex">表格中需要编辑的行的索引</param>
        public void LoadData(DataTable dataTable, int rowIndex, string id = null, EOptState optState = 0)
        {
            this.EditItems = TableInfoHelper.CreateEditItems(dataTable, rowIndex, IdField).ToList();
            this.Id = id;
            this.OptState = optState;
            ShowDataToControl();
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
                    ? ESavedState.Success.GetDescriptionEx()
                    : ESavedState.Fail.GetDescriptionEx();
            else
                this.lblTips.Text = message;
            this.timer1.Interval = second * 1000;
            this.timer1.Enabled = true;
        }
        /// <summary>
        /// 把数据加载显示到控件中
        /// </summary>
        private void ShowDataToControl()
        {
            this.gridRowData.Rows.Clear();
            this.btnSave.Enabled = true;
            if (OptState == EOptState.New)
            {
                this.btnSave.Visible = true;
                this.gridRowData.Columns[1].ReadOnly = false;
                this.btnSave.Text = "新增(&S)";
            }
            else if (OptState == EOptState.Edit)
            {
                this.btnSave.Visible = true;
                this.gridRowData.Columns[1].ReadOnly = false;
                this.btnSave.Text = "保存修改(&S)";
            }
            else if (OptState == EOptState.ReadOnly)
            {
                this.btnSave.Visible = false;
                this.gridRowData.Columns[1].ReadOnly = true;
            }

            if (!string.IsNullOrWhiteSpace(Id))
            {
                var idItem = EditItems.FirstOrDefault(v => v.IsPrimaryKey);
                if (idItem != null && idItem.Value == null) idItem.NewValue = Id;
            }
            for (int i = 0; i < EditItems.Count; i++)
            {
                var editItem = EditItems[i];
                string value = editItem.NewValue;
                //if (editItem.DictionaryTable != null && value != null && editItem.DictionaryTable.ContainsKey(editItem.NewValue))
                //    value = editItem.DictionaryTable[value];
                int rowIndex = this.gridRowData.Rows.Add(editItem.AliasName, value);
                this.gridRowData.Rows[rowIndex].Tag = editItem;
                if (!editItem.Editable)
                {
                    this.gridRowData.Rows[i].Cells[1].ReadOnly = true;
                    this.gridRowData.Rows[i].Cells[1].Style = new DataGridViewCellStyle() { BackColor = Color.LightGray };
                }
            }
            if (OptState == EOptState.New)
            {
                foreach (DataGridViewRow row in this.gridRowData.Rows)
                    row.Cells[1].Style.ForeColor = Color.DarkRed;
            }
            this.gridRowData.ClearSelection();
            this.gridRowData.Refresh();
        }

        private void btnSave_Click(object sender, EventArgs e)//保存
        {
            try
            {
                this.btnSave.Enabled = false;
                bool result = true;
                foreach (DataGridViewRow row in this.gridRowData.Rows)
                {
                    var value = row.Cells[1].Value == DBNull.Value ? null : row.Cells[1].Value?.ToString();
                    var item = row.Tag as EditItem;
                    item.NewValue = value;

                    var message = item.CheckValue();
                    if (message != null)
                        result = false;
                    row.Cells[1].Style.BackColor = message == null ? Color.White : Color.Orange;
                    row.Cells[1].ErrorText = message;
                }

                if (result)
                {
                    var args = new SaveEditEventArgs(EditItems, OptState);
                    SaveEditEvent?.Invoke(this, args);
                    this.btnSave.Enabled = args.EnableSaveOperation;
                    ShowSavedState(ESavedState.Success, 2, "保存成功！");
                }
                else
                {
                    this.btnSave.Enabled = true;
                    ShowSavedState(ESavedState.Fail, 3, "保存失败，数据填写有误！");
                }
            }
            catch (Exception ex) { this.btnSave.Enabled = true; ShowSavedState(ESavedState.Fail, 3, "保存失败：" + ex.Message); }
        }

        private void btnClse_Click(object sender, EventArgs e) => this.Close();//关闭

        private void gridRowData_CellClick(object sender, DataGridViewCellEventArgs e)//点击单元格时，根据字段显示相应控件
        {
            try
            {
                if (e.RowIndex < 0) return;
                var editItem = EditItems[e.RowIndex];
                bool editable = e.ColumnIndex == 1 && editItem.Editable;
                if (!editable) return;
                var value = this.gridRowData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                if (editItem.FieldType == typeof(DateTime))
                {
                    if (DateTime.TryParse(value, out var time))
                    {
                        var rectangle = this.gridRowData.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                        this.dateTimePicker1.Location = rectangle.Location;
                        this.dateTimePicker1.Value = time;
                        this.dateTimePicker1.Size = rectangle.Size;
                        this.dateTimePicker1.Show();
                    }
                }
                else if (editItem.CandidatesItems != null || editItem.DictionaryTable != null)
                {
                    var rectangle = this.gridRowData.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                    this.comboBox1.Items.Clear();
                    if (editItem.CandidatesItems != null)
                    {
                        this.comboBox1.Items.AddRange(editItem.CandidatesItems);
                    }
                    else
                    {
                        this.comboBox1.Items.AddRange(editItem.DictionaryTable.ToArray());
                        editItem.UseDict = true;
                    }
                    this.comboBox1.SelectedItem = value;
                    this.comboBox1.Text = value;
                    this.comboBox1.Location = rectangle.Location;
                    this.comboBox1.Size = rectangle.Size;
                    this.comboBox1.Show();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void gridRowData_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)//验证单元格中输入的数据
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    var row = this.gridRowData.Rows[e.RowIndex];
                    var value = e.FormattedValue?.ToString();
                    var editItem = row.Tag as EditItem;
                    if (!editItem.Editable) return;
                    var message = editItem.CheckValue(value);
                    row.Cells[1].ErrorText = message;
                    row.Cells[1].Style.BackColor = message == null ? Color.White : Color.Orange;
                    row.Cells[1].Style.ForeColor = row.Cells[1].Value?.ToString() != editItem.Value ? Color.DarkRed : SystemColors.WindowText;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void gridRowData_Scroll(object sender, ScrollEventArgs e)
        {
            this.dateTimePicker1.Hide();
            this.comboBox1.Hide();
            this.textEdit1.Hide();
        }

        private void gridRowData_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = this.gridRowData.Rows[e.RowIndex];
            var editItem = row.Tag as EditItem;
            if (e.ColumnIndex == 0 || !editItem.Editable) return;

            if (editItem.FieldType == typeof(DateTime))
            {
                this.gridRowData.Rows[e.RowIndex].Cells[1].Value = this.dateTimePicker1.Value.ToString();
                this.dateTimePicker1.Hide();
            }
            else if (editItem.CandidatesItems != null)
            {
                this.gridRowData.Rows[e.RowIndex].Cells[1].Value = this.comboBox1.Text;
                this.comboBox1.Hide();
            }
            else if (editItem.DictionaryTable != null)
            {
                if (this.comboBox1.SelectedIndex > -1)
                {
                    var pair = this.comboBox1.SelectedItem as DictionaryPair;
                    this.gridRowData.Rows[e.RowIndex].Cells[1].Value = editItem.UseDictKey ? pair.Key : pair.Value;
                }
                this.comboBox1.Hide();
            }

            row.Cells[1].Style.ForeColor = row.Cells[1].Value?.ToString() != editItem.Value ? Color.DarkRed : SystemColors.WindowText;
        }

        private void gridRowData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var editItem = EditItems[e.RowIndex];
            bool editable = e.ColumnIndex == 1 && editItem.Editable;
            if (!editable) return;
            if (editItem.FieldType.IsNumericType())
            {
            }
        }




        public DataGridViewTextBoxEditingControl CellEdit = null;

        private void gridRowData_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)//限制数值字段只能输入数值
        {
            if (this.gridRowData.CurrentCellAddress.X == 1)//获取当前处于活动状态的单元格索引
            {
                CellEdit = (DataGridViewTextBoxEditingControl)e.Control;
                CellEdit.KeyPress -= Cells_KeyPress; //绑定事件
                CellEdit.KeyPress += Cells_KeyPress; //绑定事件
            }
        }

        private void Cells_KeyPress(object sender, KeyPressEventArgs e)//限制数值字段只能输入数值
        {
            var cell = this.gridRowData.SelectedCells[0];
            var editItem = EditItems[cell.RowIndex];
            if (cell.ColumnIndex == 0 || !editItem.Editable) return;

            var a = (int)e.KeyChar;
            if (editItem.FieldType.IsIntegerType())//只能输入数值
            {
                if ((a < 48 || a > 57) && a != 8) e.Handled = true;
            }
            else if (editItem.FieldType.IsFloatType()) //只能输入数值和小数点
            {
                if ((a < 48 || a > 57) && a != 46 && a != 8) e.Handled = true;
            }
        }
    }
}
