/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using WLib.Files.Excel.AppLibExcel;
using WLib.UserCtrls.Dev.InputCtrl;

namespace WLib.UserCtrls.Dev.ListCtrl
{
    /// <summary>
    /// 列表控件，基于CheckedListBox控件扩展对列表的选择查询等操作
    /// </summary>
    public partial class ListBoxPlusControl : UserControl
    {
        private bool _viewOnly = false;
        private string _searchDescription = "请输入要查找的项";
        private InputDevForm _searchForm;
        private int _responseCount = 30;
        private List<int> _coloredItemIndexs = new List<int>();

        /// <summary>
        /// 指示是否只允许浏览数据
        /// </summary>
        public bool ViewOnly
        {
            get => this._viewOnly;
            set
            {
                this._viewOnly = value;
                bool untiValue = !value;
                this.sBtnSelectOK.Enabled = untiValue;
                this.sbtSelAll.Enabled = untiValue;
                this.sbtSelectClear.Enabled = untiValue;
                this.sbtSelOppo.Enabled = untiValue;
                this.sbtRefreshRegionList.Enabled = untiValue;
                this.checkedListBox1.CheckOnClick = untiValue;
                this.全选ToolStripMenuItem.Enabled = untiValue;
                this.反选ToolStripMenuItem.Enabled = untiValue;
                this.清空ToolStripMenuItem.Enabled = untiValue;
                this.导出列表EToolStripMenuItem.Enabled = untiValue;
                this.checkedListBox1.HighlightedItemStyle = _viewOnly ? HighlightStyle.Skinned : HighlightStyle.Default;
                this.checkedListBox1.BackColor = _viewOnly ? Color.Snow : Color.White;
            }
        }
        /// <summary>
        /// 点击刷新按钮时发生
        /// </summary>
        public event EventHandler RefrshListEvent;
        /// <summary>
        /// 是否显示刷新按钮，默认隐藏(false)
        /// </summary>
        public bool ShowRefreshButton { get => this.sbtRefreshRegionList.Visible;
            set => this.sbtRefreshRegionList.Visible = value;
        }
        /// <summary>
        /// 列表内容的描述
        /// </summary>
        public string Description { get => this.lblDescription.Text;
            set => this.lblDescription.Text = value;
        }
        /// <summary>
        /// 列表标题（不显示在界面上）
        /// </summary>
        public string ListTitle { get; set; }
        /// <summary>
        /// 是否显示范围选取面板
        /// </summary>
        public bool ShowSelectRegion
        {
            get => this.panelSelectRegion.Visible;
            set
            {
                this.panelSelectRegion.Visible = value;
                this.选择域ToolStripMenuItem.Checked = value;
                this.checkedListBox1.Height = 100;
            }
        }
        /// <summary>
        /// 查找框的文字描述
        /// </summary>
        public string SearchDescription { get => _searchDescription;
            set => this._searchDescription = value;
        }
        /// <summary>
        /// 列表项
        /// </summary>
        public CheckedListBoxItemCollection Items => this.checkedListBox1.Items;

        /// <summary>
        /// 响应界面一次时，向列表添加的项数
        /// </summary>
        public int ResponseCount { get => this._responseCount;
            set => this._responseCount = value;
        }
        /// <summary>
        /// 列表的右键菜单
        /// </summary>
        public ContextMenuStrip ListMenuStrip => this.cMenuCheckList;

        /// <summary>
        /// 选中项
        /// </summary>
        public DevExpress.XtraEditors.Controls.CheckedListBoxItem SelectedItem
        {
            get => this.checkedListBox1.SelectedItem as DevExpress.XtraEditors.Controls.CheckedListBoxItem;
            set => this.checkedListBox1.SelectedItem = value;
        }
        /// <summary>
        /// 选中项索引
        /// </summary>
        public int SelecteIndex { get => this.checkedListBox1.SelectedIndex;
            set => this.checkedListBox1.SelectedIndex = value;
        }
        /// <summary>
        /// 显示不同字体颜色的项（索引）
        /// </summary>
        public List<int> ColoredItemIndexs { get => _coloredItemIndexs;
            set => this._coloredItemIndexs = value;
        }
        /// <summary>
        /// 显示不同字体颜色的项的颜色
        /// </summary>
        public Color ColoredItemColor { get; set; }

        /// <summary>
        /// 列表控件，基于ListBox控件扩展对列表的选择查询等操作
        /// </summary>
        public ListBoxPlusControl()
        {
            InitializeComponent();  

            ColoredItemColor = Color.Indigo;// Color.FromArgb(102, 97, 0);
            RefrshListEvent = (object sender, EventArgs e) => { };
        }

        /// <summary>
        /// 清空并添加列表项，并将添加的列表项全部设为选中
        /// </summary>
        /// <param name="valueList"></param>
        public void Init(object[] valueList)
        {
            checkedListBox1.Items.Clear();
            _coloredItemIndexs.Clear();
            labelSelectedState.Text = "0";
            if (valueList == null)
                return;
            for (int i = 0; i < valueList.Length; i++)
            {
                checkedListBox1.Items.Add(valueList[i], true);
                if (i % ResponseCount == 0)
                {
                    this.labelSelectedState.Text = (i + 1).ToString();
                    Application.DoEvents();
                }
            }
            this.labelSelectedState.Text = valueList.Length.ToString();
            this.numUpDwEnd.Maximum = valueList.Length;
            this.numUpDwStart.Maximum = valueList.Length;
            this.numUpDwEnd.Value = valueList.Length;
        }

        /// <summary>
        /// 获取所有选中的列表项
        /// </summary>
        /// <returns></returns>
        public List<object> GetCheckedItems()
        {
            List<object> results = new List<object>();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                    results.Add(checkedListBox1.Items[i].Value);
            }
            return results;
        }

        private void checkedListBox1_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            int i = Convert.ToInt32(labelSelectedState.Text);
            if (e.State == CheckState.Checked)
                labelSelectedState.Text = (i + 1).ToString();
            else
                labelSelectedState.Text = (i - 1).ToString();
        }

        private void checkedListBox1_ItemChecking(object sender, DevExpress.XtraEditors.Controls.ItemCheckingEventArgs e)
        {
            if (ViewOnly)
                e.Cancel = true;
        }

        private void btSelAll_Click(object sender, EventArgs e)
        {
            checkedListBox1.CheckAll();
        }

        private void btSelectClear_Click(object sender, EventArgs e)
        {
            checkedListBox1.UnCheckAll();
        }

        private void btSelOppo_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, !checkedListBox1.GetItemChecked(i));
            }
        }

        private void btRefreshRegionList_Click(object sender, EventArgs e)
        {
            RefrshListEvent(sender, e);
        }

        private void picSearch_Click(object sender, EventArgs e)
        {
            _searchForm = new InputDevForm("查找" + ListTitle, "查找", SearchDescription);
            if (_searchForm.ShowDialog() == DialogResult.OK)
            {
                string keyWord = _searchForm.KeyWord;
                for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
                {
                    var item = this.checkedListBox1.Items[i];
                    if (item.ToString().Contains(keyWord))
                    {
                        this.checkedListBox1.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void sBtnSelectOK_Click(object sender, EventArgs e)
        {
            int cnt = this.checkedListBox1.Items.Count;
            if ((int)this.numUpDwStart.Value > (int)this.numUpDwEnd.Value)
            {
                int tmp = (int)this.numUpDwEnd.Value;
                this.numUpDwEnd.Value = this.numUpDwStart.Value;
                this.numUpDwStart.Value = tmp;
            }
            if (this.numUpDwStart.Value < 1) this.numUpDwStart.Value = 1;
            if (this.numUpDwEnd.Value < 1) this.numUpDwEnd.Value = 1;
            if (cnt == 0) return;
            if (this.numUpDwEnd.Value > cnt) this.numUpDwEnd.Value = cnt;

            int min = (int)this.numUpDwStart.Value;
            int max = (int)this.numUpDwEnd.Value;

            bool isSelected = this.labelSelectOption.Text == "选择：" ? true : false;
            if (this.checkedListBox1.Items.Count > 0)
            {
                for (int i = min; i <= max; i++)
                {
                    this.checkedListBox1.SetItemChecked(i - 1, isSelected);
                }
            }
        }

        private void labelShowOptions_Click(object sender, EventArgs e)
        {
            this.labelSelectedState.Text = this.checkedListBox1.CheckedItems.Count.ToString();
            this.panelSelectRegion.Visible = !this.panelSelectRegion.Visible;
            this.选择域ToolStripMenuItem.Checked = this.panelSelectRegion.Visible;
        }

        private void labelSelectOption_Click(object sender, EventArgs e)
        {
            if (this.labelSelectOption.Text == "选择：")
                this.labelSelectOption.Text = "不选：";
            else
                this.labelSelectOption.Text = "选择：";
        }

        private void 复制CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.checkedListBox1.SelectedItems.Count > 0)
            {
                var item = this.checkedListBox1.SelectedItems[0];
                if (item != null)
                    Clipboard.SetDataObject(item.ToString());
            }
        }

        private void 导出列表EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.FileName = this.lblDescription.Text;
            sv.Filter = "文本文件（*.txt）|*.txt|Excel97-2003文件（*.xls）|*.xls";
            if (sv.ShowDialog() == DialogResult.OK)
            {
                string extension = System.IO.Path.GetExtension(sv.FileName);
                if (extension == ".txt")
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (CheckedListBoxItem item in this.checkedListBox1.Items)
                    {
                        string strChecked = item.CheckState == CheckState.Checked ? "√" : "  ";
                        sb.AppendFormat("{0} {1}{2}", strChecked, item.Value.ToString(), System.Environment.NewLine);
                    }
                    System.IO.File.WriteAllText(sv.FileName, sb.ToString());
                }
                else if (extension == ".xls")
                {
                    DataTable dt = new DataTable(this.ListTitle);
                    dt.Columns.Add("选中");
                    dt.Columns.Add(this.ListTitle);
                    foreach (CheckedListBoxItem item in this.checkedListBox1.Items)
                    {
                        string strChecked = item.CheckState == CheckState.Checked ? "√" : "×";
                        dt.Rows.Add(strChecked, item.Value.ToString());
                    }
                    string dir = System.IO.Path.GetDirectoryName(sv.FileName);
                    string name = System.IO.Path.GetFileName(sv.FileName);
                    DataToExcel.DataTableToExcel(dt, dir, name);
                }
            }
        }

        private void label_MouseEnter(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            ctrl.ForeColor = Color.DodgerBlue;
        }

        private void label_MouseLeave(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            ctrl.ForeColor = Color.Blue;
        }

        private void checkedListBox1_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            if (_coloredItemIndexs.Contains(e.Index))
                e.Appearance.ForeColor = ColoredItemColor;
            else
                e.Appearance.ForeColor = SystemColors.WindowText;
        }
    }
}
