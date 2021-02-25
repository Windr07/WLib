using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WLib.Data;
using WLib.Register;
using WLib.WinCtrls.Dev._Extension;

namespace WLib.WinCtrls.Dev.RegisterCtrl
{
    /// <summary>
    /// 列出每次注册授权信息的窗体
    /// </summary>
    public partial class RegInfoForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 记录注册信息的文件的路径
        /// <para>默认路径为程序目录下的rec.bat文件</para>
        /// </summary>
        public string RegInfoPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory + "reg.bat";
        /// <summary>
        /// 显示已生成过的注册信息的窗体（列出每次授权情况的窗体）
        /// </summary>
        public RegInfoForm() => InitializeComponent();

        private void RegInfoForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (!System.IO.File.Exists(RegInfoPath))
                {
                    MessageBox.Show($"找不到记录注册信息的文件“{RegInfoPath}”", "获取注册信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                RegAppInfo[] regInfos = RegAppInfo.ReadFromFile(RegInfoPath);
                this.gridControl1.DataSource = regInfos.AsEnumerable().ToDataTable();
                var count = regInfos.Select(v => v.MachineCode).Distinct().Count();
                this.lblTips.Text = string.Format("总共{0}个机器码，{1}个注册码", count, regInfos.Length);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            this.gridView1.ShowRowNumber();
        }

        private void btnCopyAll_Click(object sender, EventArgs e)
        {
            var text = (this.gridView1.DataSource as DataTable).ToText("\t");
            Clipboard.SetDataObject(text);
        }
    }
}
