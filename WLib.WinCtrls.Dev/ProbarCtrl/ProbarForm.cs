/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace WLib.WinCtrls.Dev.ProbarCtrl
{
    /// <summary>
    /// 进度条窗体
    /// </summary>
    public partial class ProbarForm : XtraForm
    {
        /// <summary>
        /// 进度条的文字提示
        /// </summary>
        public string Tips { get => this.progressBarControl1.Tips; set => this.progressBarControl1.Tips = value; }
        /// <summary>
        /// 进度条窗体
        /// </summary>
        public ProbarForm()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        /// <summary>
        /// 从创建窗体的线程调用关闭窗体的方法
        /// </summary>
        public void InvokeCloseForm() => this.Invoke(new Action(this.Close));
    }
}
