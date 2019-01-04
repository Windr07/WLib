using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
        public List<EditItem> EditRowData { get; set; }
        /// <summary>
        /// 用于操作该行的ID字段,初始化时赋值
        /// </summary>
        public int MainOpId { get; private set; }

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
            List<EditItem> newEditItem = new List<EditItem>();
            for (int i = 0; i < this.gridRowData.Rows.Count; i++)
            {
                object o = this.gridRowData.Rows[i].Cells[1].Value;
                string stringValue = o == null ? "" : o.ToString();
                var item = EditRowData[i];
                if (item.FieldValue != stringValue)
                    newEditItem.Add(new EditItem(item.FieldName, item.FieldAliasName, stringValue));
            }
            OnSaveEvent(this, new SaveEditEventArgs(newEditItem)); 
        }

        private void btnCancel_Click(object sender, EventArgs e)//取消
        {
            this.Close();
        }

        private void gridRowData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
                this.gridRowData.ClearSelection();
        }
    }
}
