/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using WLib.Register;

namespace WLib.WinCtrls.RegisterCtrl
{
    /// <summary>
    /// 软件注册窗体
    /// </summary>
    public partial class RegisterForm : Form
    {
        /// <summary>
        /// 需要授权的软件信息
        /// </summary>
        public AppInfo AppInfo { get; set; }
        /// <summary>
        /// 软件注册窗体
        /// </summary>
        /// <param name="appInfo">需要授权的软件信息</param>
        public RegisterForm(AppInfo appInfo)
        {
            InitializeComponent();
            this.AppInfo = appInfo;
            this.Text += " - " + appInfo.Name;
        }
        /// <summary>
        /// 验证输入的注册码并执行注册
        /// </summary>
        private void RegisterByRegCode(string hardwareCode, string regCode)
        {
            int registeState = -1;
            try
            {
                var softReg = new SoftRegister(AppInfo);
                registeState = softReg.CheckRegistered(hardwareCode, regCode);
                switch (registeState)
                {
                    case 0:
                        softReg.WriteToRegistry(hardwareCode, regCode);
                        MessageBox.Show("注册成功！", null, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Restart();
                        break;
                    case 1:
                        MessageBox.Show("注册码错误！", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 2:
                        MessageBox.Show("授权已过期", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex) { MessageBox.Show("代码-" + registeState + ex.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void RegisterForm_Load(object sender, EventArgs e)
            => this.txtMachineCode.Text = new SoftRegister(AppInfo.Company, AppInfo.Key).CreateHardwareCode();//生成机器码

        private void btnRegister_Click(object sender, EventArgs e)//注册
        {
            if (string.IsNullOrEmpty(this.txtRegCode.Text))
            {
                MessageBox.Show("请填写注册码！");
                return;
            }
            RegisterByRegCode(this.txtMachineCode.Text.Trim(), this.txtRegCode.Text.Trim());
        }

        private void btnCopy_Click(object sender, EventArgs e) => Clipboard.SetDataObject(this.txtMachineCode.Text);//复制机器码
    }
}
