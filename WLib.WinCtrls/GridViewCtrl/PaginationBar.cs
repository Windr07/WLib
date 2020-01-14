using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WLib.WinCtrls.GridViewCtrl
{
    /// <summary>
    /// 分页栏控件
    /// <para>辅助<see cref="DataGridView"/>等控件进行数据分页查询的控件</para>
    /// </summary>
    [DefaultEvent(nameof(PageChanged))]
    [DefaultProperty(nameof(PageRecordCnt))]
    public partial class PaginationBar : UserControl
    {
        private int _sumRecord = -1;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int SumRecord { get => _sumRecord; private set => this.lblPages.Text = $"共{_sumRecord = value}条记录"; }
        /// <summary>
        /// 每页显示记录数
        /// </summary>
        public int PageRecordCnt { get => (int)this.cmbRecordCnt.SelectedItem; set { this.cmbRecordCnt.SelectedItem = value; GotoPage(1); } }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; private set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage { get; private set; }
        /// <summary>
        /// 当前页开始行在总记录的行号
        /// </summary>
        public int StartRow { get; private set; }
        /// <summary>
        /// 当前页结束行在总记录的行号
        /// </summary>
        public int EndRow { get; private set; }
        /// <summary>
        /// 跳转分页事件
        /// </summary>
        public event EventHandler PageChanged;


        /// <summary>
        /// 分页栏控件
        /// <para>辅助<see cref="DataGridView"/>等控件进行数据分页查询的控件</para>
        /// </summary>
        public PaginationBar()
        {
            InitializeComponent();
            this.ToolStripPagination.BackColor = Color.Transparent;
            this.lblPages.Tag = 0;
            this.cmbRecordCnt.Items.AddRange(new object[] { 20, 30, 50, 100, 200, 300, 400, 500, 1000, 2000, 3000, 5000, 10000 });
            this.cmbRecordCnt.SelectedItem = 200;
        }
        /// <summary>
        /// 跳转到指定分页
        /// </summary>
        /// <param name="pageNum">跳转的分页</param>
        /// <param name="sumRecord">设置总记录数，若值小于0则不设置新的总记录数，保持原总记录数</param>
        public void GotoPage(int pageNum, int sumRecord = -1)
        {
            if (pageNum < 1)
                throw new Exception("跳转的分页不能小于1");

            if (sumRecord > -1)
                SumRecord = sumRecord;

            CurrentPage = pageNum;
            PageCount = SumRecord % PageRecordCnt == 0 ? SumRecord / PageRecordCnt : SumRecord / PageRecordCnt + 1;
            StartRow = PageRecordCnt * (CurrentPage - 1) + 1;
            EndRow = PageRecordCnt * CurrentPage;

            this.lblPages.Text = $"{CurrentPage} / {PageCount}";
            PageChanged?.Invoke(this, new EventArgs());
        }


        private void cmbRecordCount_SelectedIndexChanged(object sender, EventArgs e)//选择每页记录数后，加载数据
        {
            GotoPage(1);
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)//显示记录数、页码等文本框只能输入数字
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)//只有数字键和退格键有效
                e.Handled = true;
        }

        private void btnGoto_Click(object sender, EventArgs e)//跳转到指定分页
        {
            GotoPage(Convert.ToInt32(this.txtGotoPageNum.Text));
        }

        private void btnFirst_Click(object sender, EventArgs e)//第一页
        {
            if (CurrentPage != 1) GotoPage(1);
        }

        private void btnPre_Click(object sender, EventArgs e)//上一页
        {
            if (CurrentPage > 1) GotoPage(CurrentPage - 1);
        }

        private void btnNext_Click(object sender, EventArgs e)//下一页
        {
            if (CurrentPage < PageCount) GotoPage(CurrentPage + 1);
        }

        private void btnLast_Click(object sender, EventArgs e)//最后一页
        {
            if (CurrentPage != PageCount) GotoPage(PageCount);
        }

        private void btnAll_Click(object sender, EventArgs e)//显示全部记录
        {
            if (SumRecord > 10000)
            {
                var dr = MessageBox.Show("加载全部表格数据可能需要花费较多的时间，是否加载全部数据？", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr != DialogResult.Yes)
                    return;
            }
            if (!this.cmbRecordCnt.Items.Cast<int>().Contains(SumRecord))
                this.cmbRecordCnt.Items.Add(SumRecord);
            this.cmbRecordCnt.SelectedItem = SumRecord;
        }
    }
}
