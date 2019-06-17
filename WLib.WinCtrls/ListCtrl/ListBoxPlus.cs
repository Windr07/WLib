/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： 参考自XiaoJiaMing写的同类型控件
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WLib.Data;
using WLib.Files.Excel.AppLibExcel;
using WLib.WinCtrls.InputCtrl;
using WLib.WinForm;

namespace WLib.WinCtrls.ListCtrl
{
    /// <summary>
    /// 列表控件，基于CheckedListBox控件扩展对列表的选择查询等操作
    /// </summary>
    public partial class ListBoxPlus : UserControl
    {
        /// <summary>
        /// 指示是否只允许浏览数据
        /// </summary>
        private bool _viewOnly;
        /// <summary>
        /// 输入框窗体
        /// </summary>
        private InputForm _searchForm;

        /// <summary>
        /// 指示是否只允许浏览数据
        /// </summary>
        public bool ViewOnly
        {
            get => _viewOnly;
            set
            {
                _viewOnly = value;
                bool untiValue = !value;
                sBtnSelectOK.Enabled = untiValue;
                sbtSelAll.Enabled = untiValue;
                sbtSelectClear.Enabled = untiValue;
                sbtSelOppo.Enabled = untiValue;
                sbtRefreshRegionList.Enabled = untiValue;
                checkedListBox1.CheckOnClick = untiValue;
                全选ToolStripMenuItem.Enabled = untiValue;
                反选ToolStripMenuItem.Enabled = untiValue;
                清空ToolStripMenuItem.Enabled = untiValue;
                导出列表EToolStripMenuItem.Enabled = untiValue;
                checkedListBox1.BackColor = _viewOnly ? Color.Snow : Color.White;
            }
        }
        /// <summary>
        /// 点击刷新按钮时发生
        /// </summary>
        public event EventHandler RefrshListEvent;
        /// <summary>
        /// 是否显示刷新按钮，默认隐藏(false)
        /// </summary>
        public bool ShowRefreshButton { get => sbtRefreshRegionList.Visible; set => sbtRefreshRegionList.Visible = value; }
        /// <summary>
        /// 列表内容的描述
        /// </summary>
        public string Description { get => lblDescription.Text; set => lblDescription.Text = value; }
        /// <summary>
        /// 列表标题（不显示在界面上）
        /// </summary>
        public string ListTitle { get; set; }
        /// <summary>
        /// 是否显示范围选取面板
        /// </summary>
        public bool ShowSelectRegion
        {
            get => panelSelectRegion.Visible;
            set
            {
                panelSelectRegion.Visible = value;
                选择域ToolStripMenuItem.Checked = value;
                checkedListBox1.Height = 100;
            }
        }
        /// <summary>
        /// 查找框的文字描述
        /// </summary>
        public string SearchDescription { get; set; } = "请输入要查找的项";
        /// <summary>
        /// 列表项
        /// </summary>
        public CheckedListBox.ObjectCollection Items => checkedListBox1.Items;
        /// <summary>
        /// 响应界面一次时，向列表添加的项数
        /// </summary>
        public int ResponseCount { get; set; } = 30;


        /// <summary>
        /// 列表控件，基于ListBox控件扩展对列表的选择查询等操作
        /// </summary>
        public ListBoxPlus()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 清空并添加列表项，并将添加的列表项全部设为选中
        /// </summary>
        /// <param name="values"></param>
        public void Init(object[] values)
        {
            checkedListBox1.Items.Clear();
            labelSelectedState.Text = "0";
            if (values == null)
                return;
            for (int i = 0; i < values.Length; i++)
            {
                checkedListBox1.Items.Add(values[i], true);
                if (i % ResponseCount == 0)
                {
                    labelSelectedState.Text = (i + 1).ToString();
                    Application.DoEvents();
                }
            }
            labelSelectedState.Text = values.Length.ToString();
            numUpDwEnd.Maximum = values.Length;
            numUpDwStart.Maximum = values.Length;
            numUpDwEnd.Value = values.Length;
        }
        /// <summary>
        /// 获取所有选中的列表项
        /// </summary>
        /// <returns></returns>
        public List<object> GetCheckedItems()
        {
            var results = new List<object>();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                    results.Add(checkedListBox1.Items[i]);
            }
            return results;
        }


        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int i = Convert.ToInt32(labelSelectedState.Text);
            labelSelectedState.Text = e.NewValue == CheckState.Checked ? (i + 1).ToString() : (i - 1).ToString();
            if (ViewOnly)
                e.NewValue = e.CurrentValue;
        }

        private void btSelAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, true);
        }

        private void btSelectClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, false);
        }

        private void btSelOppo_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, !checkedListBox1.GetItemChecked(i));
        }

        private void btRefreshRegionList_Click(object sender, EventArgs e)
        {
            RefrshListEvent?.Invoke(sender, e);
        }

        private void picSearch_Click(object sender, EventArgs e)
        {
            _searchForm = new InputForm("查找" + ListTitle, "查找", SearchDescription);
            if (_searchForm.ShowDialog() == DialogResult.OK)
            {
                string keyWord = _searchForm.KeyWord;
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.Items[i].ToString().Contains(keyWord))
                    {
                        checkedListBox1.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void sBtnSelectOK_Click(object sender, EventArgs e)
        {
            int cnt = checkedListBox1.Items.Count;
            if ((int)numUpDwStart.Value > (int)numUpDwEnd.Value)
            {
                int tmp = (int)numUpDwEnd.Value;
                numUpDwEnd.Value = numUpDwStart.Value;
                numUpDwStart.Value = tmp;
            }
            if (numUpDwStart.Value < 1) numUpDwStart.Value = 1;
            if (numUpDwEnd.Value < 1) numUpDwEnd.Value = 1;
            if (cnt == 0) return;
            if (numUpDwEnd.Value > cnt) numUpDwEnd.Value = cnt;

            int min = (int)numUpDwStart.Value;
            int max = (int)numUpDwEnd.Value;

            bool isSelected = labelSelectOption.Text == "选择：";
            if (checkedListBox1.Items.Count > 0)
            {
                for (int i = min; i <= max; i++)
                    checkedListBox1.SetItemChecked(i - 1, isSelected);
            }
        }

        private void labelShowOptions_Click(object sender, EventArgs e)
        {
            labelSelectedState.Text = checkedListBox1.CheckedItems.Count.ToString();
            panelSelectRegion.Visible = !panelSelectRegion.Visible;
            选择域ToolStripMenuItem.Checked = panelSelectRegion.Visible;
        }

        private void labelSelectOption_Click(object sender, EventArgs e)
        {
            labelSelectOption.Text = labelSelectOption.Text == "选择：" ? "不选：" : "选择：";
        }

        private void 复制CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedItems.Count <= 0) return;

            var item = checkedListBox1.SelectedItems[0];
            if (item != null)
                Clipboard.SetDataObject(item.ToString());
        }

        private void 导出列表EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var filePath = DialogOpt.ShowSaveFileDialog(
                @"文本文件（*.txt）|*.txt|Excel97-2003文件（*.xls）|*.xls", null, lblDescription.Text);
            if (filePath == null) return;

            var extension = Path.GetExtension(filePath);
            if (extension == ".txt")
            {
                var sb = new StringBuilder();
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    var strChecked = checkedListBox1.GetItemChecked(i) ? "√" : "  ";
                    sb.AppendFormat("{0} {1}{2}", strChecked, checkedListBox1.Items[i], Environment.NewLine);
                }
                File.WriteAllText(filePath, sb.ToString());
            }
            else if (extension == ".xls")
            {
                DataTable dt = DataTableOpt.InitDataTable(ListTitle, "选中", ListTitle);
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    var strChecked = checkedListBox1.GetItemChecked(i) ? "√" : "×";
                    dt.Rows.Add(strChecked, checkedListBox1.Items[i].ToString());
                }
                dt.DataTableToExcel(Path.GetDirectoryName(filePath), Path.GetFileName(filePath));
            }
        }

        private void label_MouseEnter(object sender, EventArgs e)
        {
            ((Control)sender).ForeColor = Color.DodgerBlue;
        }

        private void label_MouseLeave(object sender, EventArgs e)
        {
            ((Control)sender).ForeColor = Color.Blue;
        }
    }
}
