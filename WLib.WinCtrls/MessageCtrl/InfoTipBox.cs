/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Windows.Forms;

namespace WLib.WinCtrls.MessageCtrl
{
    /// <summary>
    /// 信息提示框
    /// </summary>
    public partial class InfoTipBox : Form
    {
        /// <summary>
        /// 表示总共多少秒后自动关闭当前窗口
        /// </summary>
        private int _sumSecond;
        /// <summary>
        /// 表示还剩多少秒后关闭自动当前窗口
        /// </summary>
        private int _second;
        /// <summary>
        /// 信息提示框关闭后执行的操作
        /// </summary>
        public event EventHandler AfterFormClosed;

        /// <summary>
        /// 信息提示框
        /// <para>该提示框默认会在显示若干秒之后关闭</para>
        /// </summary>
        /// <param name="info">提示信息</param>
        /// <param name="title">提示标题</param>
        /// <param name="sumCloseSecond">总共多少秒后自动关闭当前窗口，值小于等于0则不会自动关闭</param>
        /// <param name="afterFormClosed">信息提示框关闭后执行的操作</param>
        public InfoTipBox(string info, string title = null, int sumCloseSecond  = 3, EventHandler afterFormClosed = null)
        {
            InitializeComponent();
            lblInfo.Text = info;
            Text = title;
            _second = _sumSecond = sumCloseSecond;
            AfterFormClosed = afterFormClosed;
        }

        private void BtnTopClose_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Close();
            AfterFormClosed?.Invoke(this, new EventArgs());
        }

        private void InfoTipBox_Load(object sender, EventArgs e) => timer1.Enabled = _sumSecond > 0;

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (_second >= 0)
                btnClose.Text = $"   关闭({_second--})";
            else
            {
                timer1.Enabled = false;
                Close();
                AfterFormClosed?.Invoke(this, new EventArgs());
            }
        }
    }
}
