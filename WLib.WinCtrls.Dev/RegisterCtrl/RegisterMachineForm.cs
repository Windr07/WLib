using System;
using System.Linq;
using System.Windows.Forms;
using WLib.Register;

namespace WLib.WinCtrls.Dev.RegisterCtrl
{
    /// <summary>
    /// 注册机窗体
    /// </summary>
    public partial class RegisterMachineForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 注册机窗体
        /// </summary>
        /// <param name="regApps">需要授权的软件信息。
        /// 其中的<see cref="AppInfo.Company"/>和<see cref="AppInfo.Key"/>的值将参与机器码和注册码的生成，请确保这两个属性值的正确性</param>
        public RegisterMachineForm(params AppInfo[] regApps)
        {
            InitializeComponent();
            this.cmbSoftware.Properties.Items.AddRange(regApps.ToArray());
            if (this.cmbSoftware.Properties.Items.Count > 0)
                this.cmbSoftware.SelectedIndex = 0;
        }


        /// <summary>
        /// 验证机器码、授权时长是否有误
        /// </summary>
        /// <returns></returns>
        private bool ValidateInput()
        {
            if (this.txtMachineCode.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入机器码", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.txtMachineCode.Text.Trim().Length != 64)
            {
                MessageBox.Show("输入机器码有误，机器码是32位字符！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.numYear.Value == 0 && this.numMonth.Value == 0 && this.numDay.Value == 0 &&
                this.numHour.Value == 0 && this.numMinute.Value == 0)
            {
                MessageBox.Show("注册时长应该大于1分钟", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnGetRegisterCode_Click(object sender, EventArgs e)//注册
        {
            try
            {
                if (!ValidateInput()) return;

                int year = (int)this.numYear.Value;
                int month = (int)this.numMonth.Value;
                int day = (int)this.numDay.Value;
                int hour = (int)this.numHour.Value;
                int minute = (int)this.numMinute.Value;
                var startTime = DateTime.Now.AddMinutes(-2);
                var endTime = startTime.AddYears(year).AddMonths(month).AddDays(day).AddHours(hour).AddMinutes(minute);
                var hardwareCode = this.txtMachineCode.Text.Trim();
                var comment = this.txtComment.Text.Trim();

                var appInfo = (AppInfo)this.cmbSoftware.SelectedItem;
                var regCode = new SoftRegister(appInfo).CreateRegCode(hardwareCode, startTime, endTime);
                this.lblStartTime.Text = startTime.ToString();
                this.lblEndTime.Text = endTime.ToString();
                this.txtRegisterCode.Text = regCode;

                var regInfoPath = AppDomain.CurrentDomain.BaseDirectory + "reg.bat";
                new RegAppInfo(appInfo.Name, appInfo.Id, appInfo.Version, appInfo.Company, hardwareCode, regCode, startTime, endTime, comment).
                    WriteToFile(regInfoPath);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void hyperlinkLabelControl1_Click(object sender, EventArgs e)
        => new RegInfoForm().ShowDialog();//显示已生成过的注册信息
    }
}
