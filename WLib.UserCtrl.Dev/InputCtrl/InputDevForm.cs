/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Windows.Forms;

namespace WLib.UserCtrls.Dev.InputCtrl
{
    /// <summary>
    /// 输入框窗体
    /// </summary>
    public partial class InputDevForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 提示文字
        /// </summary>
        public string Tips { get => this.lblTips.Text; set => this.lblTips.Text = value; }
        /// <summary>
        /// 确定按钮显示的文字
        /// </summary>
        public string SearchButtonText { get => this.btnSearch.Text; set => this.btnSearch.Text = value; }
        /// <summary>
        /// 用户输入的值
        /// </summary>
        public string KeyWord { get => this.textBox1.Text.Trim(); private set => this.textBox1.Text = value; }


        /// <summary>
        /// 实例化输入框窗体
        /// </summary>
        public InputDevForm()
        {
            InitializeComponent();
            this.textBox1.Select();
        }
        /// <summary>
        /// 实例化输入框窗体，设置窗口标题、确定按钮的文字
        /// </summary>
        /// <param name="title"></param>
        /// <param name="btnText"></param>
        public InputDevForm(string title, string btnText)
        {
            InitializeComponent();
            this.Text = title;
            this.btnSearch.Text = btnText;
            this.textBox1.Select();
        }
        /// <summary>
        /// 实例化输入框窗体，设置窗口标题、确定按钮的文字、提示文字、默认值
        /// </summary>
        /// <param name="title">窗口标题</param>
        /// <param name="tips">提示文字</param>
        /// <param name="btnText">确定按钮的文字</param>
        /// <param name="defaultValue">默认值</param>
        public InputDevForm(string title, string btnText, string tips, string defaultValue = null)
        {
            InitializeComponent();
            this.Text = title;
            this.SearchButtonText = btnText;
            this.Tips = tips;
            this.KeyWord = defaultValue;
            this.textBox1.Select();
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void InputDevForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
