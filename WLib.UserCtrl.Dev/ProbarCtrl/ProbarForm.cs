using System;
using System.Windows.Forms;

namespace WLib.UserCtrls.Dev.ProbarControl
{
    public partial class ProbarForm : Form
    {
        /// <summary>
        /// 进度条的文字提示
        /// </summary>
        public string Tips { get => this.progressBarControl1.Tips; set => this.progressBarControl1.Tips = value; }

        private readonly Action _closeHanler;
        public ProbarForm()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            _closeHanler = CloseMe;
        }
        private void CloseMe()
        {
            this.Close();
        }

        /// <summary>
        /// 从创建窗体的线程调用关闭窗体的方法
        /// </summary>
        public void InvokeCloseForm()
        {
            this.Invoke(_closeHanler);
        }
    }
}
