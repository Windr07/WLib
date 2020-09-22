using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WLib.Data;
using WLib.Register;

namespace WLib.WinCtrls.RegisterCtrl
{
    /// <summary>
    /// 列出每次注册授权信息的窗体
    /// </summary>
    public partial class RegInfoForm : Form
    {
        /// <summary>
        /// 记录注册信息的文件的路径
        /// <para>默认路径为程序目录下的rec.bat文件</para>
        /// </summary>
        public string RegInfoPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory + "reg.bat";
        /// <summary>
        /// 显示已生成过的注册信息的窗体（列出每次授权情况的窗体）
        /// </summary>
        public RegInfoForm()
        {
            InitializeComponent();
        }

        private void RegInfoForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (!System.IO.File.Exists(RegInfoPath))
                {
                    MessageBox.Show($"找不到记录注册信息的文件“{RegInfoPath}”", "获取注册信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.dataGridView1.Rows.Clear();
                RegAppInfo[] regInfos = RegAppInfo.ReadFromFile(RegInfoPath);
                var dataTable = regInfos.AsEnumerable().ConvertToDataTable();
                this.dataGridView1.DataSource = dataTable;

                var count = regInfos.Select(v => v.MachineCode).Distinct().Count();
                this.lblTips.Text = string.Format("总共{0}个机器码，{1}个注册码", count, regInfos.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCopyAll_Click(object sender, EventArgs e)
        {
            var dataTable = this.dataGridView1.DataSource as DataTable;
            var text = dataTable.ToText("\t");
            Clipboard.SetDataObject(text);
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < e.RowCount; i++)
                this.dataGridView1.Rows[e.RowIndex + i].HeaderCell.Value = (e.RowIndex + i + 1).ToString();
            for (int i = e.RowIndex + e.RowCount; i < this.dataGridView1.Rows.Count; i++)
                this.dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
        }
    }
}
