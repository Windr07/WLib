/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Linq;
using System.Windows.Forms;
using WLib.WinCtrls.AddItemCtrl.Base;

namespace WLib.WinCtrls.AddItemCtrl
{
    /// <summary>
    /// 数据筛选设置窗口
    /// </summary>
    public partial class AddItemConfigForm : Form, IMatchModeConfig
    {
        #region 接口属性
        /// <summary>
        /// 匹配模式（0-Split-根据分隔符分割字符串，获得分隔后的文本项； 1-Regex-根据正则表达式获得匹配项）
        /// </summary>
        public EMatchMode MatchMode => this.radioSplitString.Checked ? EMatchMode.Split : EMatchMode.Regex;
        /// <summary>
        /// 正则表达式
        /// </summary>
        public string RegexString { get => this.txtRegex.Text.Trim(); set => this.txtRegex.Text = value; }
        /// <summary>
        /// 默认可在下拉框中选择的分隔符
        /// </summary>
        public string[] DefaultSplitArray
        {
            get => this.cmbFilterString.Items.Cast<string>().ToArray();
            set { this.cmbFilterString.Items.Clear(); this.cmbFilterString.Items.AddRange(value); }
        }
        /// <summary>
        /// 分隔符数组
        /// </summary>
        public string[] SplitStringArray
        {
            get { return this.listBoxSplitItems.Items.Cast<SplitStrDef>().Select(v => v.Value).ToArray(); }
            set
            {
                this.listBoxSplitItems.Items.Clear();
                if (value != null && value.Length > 0)
                    this.listBoxSplitItems.Items.AddRange(value.Select(v => new SplitStrDef(v)).ToArray());
            }
        }
        #endregion


        #region 构造函数
        /// <summary>
        /// 初始化数据筛选设置窗口
        /// </summary>
        private void InitForm(EMatchMode mode, string regexString, string[] splitStringArray)
        {
            InitializeComponent();
            this.cmbFilterString.Items.AddRange(SplitStrDef.SourceSplits);
            this.radioSplitString.Checked = mode == EMatchMode.Split;
            this.radioRegex.Checked = mode == EMatchMode.Regex;
            this.RegexString = regexString;
            this.SplitStringArray = splitStringArray;
        }
        /// <summary>
        /// 数据筛选设置窗口
        /// </summary>
        /// <param name="mode"> 匹配模式（0-Split-根据分隔符分割字符串，所添加的项是分割后的字符串； 1-Regex-根据正则表达式获得匹配项）</param>
        /// <param name="regexString"></param>
        /// <param name="splitStringArray"></param>
        public AddItemConfigForm(EMatchMode mode, string regexString, string[] splitStringArray)
        {
            InitForm(mode, regexString, splitStringArray);
        }
        /// <summary>
        /// 数据筛选设置窗口
        /// </summary>
        /// <param name="regexString">正则表达式</param>
        public AddItemConfigForm(string regexString)
        {
            InitForm(EMatchMode.Regex, regexString, null);
        }
        /// <summary>
        /// 数据筛选设置窗口
        /// </summary>
        /// <param name="splitStringArray">分隔字符串数组</param>
        public AddItemConfigForm(string[] splitStringArray)
        {
            InitForm(EMatchMode.Split, null, splitStringArray);
        }
        #endregion


        private void checkFilterString_CheckedChanged(object sender, EventArgs e)//选中按分隔符，还是按正则表达式筛选
        {
            this.groupBoxSplitString.Enabled = this.radioSplitString.Checked;
            this.groupBoxRegex.Enabled = !this.radioSplitString.Checked;
        }

        private void sBtnOK_Click(object sender, EventArgs e)//确定
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void sBtnCancel_Click(object sender, EventArgs e)//取消
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void sBtnAdd_Click(object sender, EventArgs e)//添加分隔符
        {
            if (!string.IsNullOrEmpty(this.cmbFilterString.Text))
                this.listBoxSplitItems.Items.Add(new SplitStrDef(this.cmbFilterString.Text));
        }

        private void 移除ToolStripMenuItem_Click(object sender, EventArgs e)//移除分隔符
        {
            if (this.listBoxSplitItems.SelectedIndex > -1)
                this.listBoxSplitItems.Items.Remove(this.listBoxSplitItems.SelectedItem);
        }

        private void 移除全部ToolStripMenuItem_Click(object sender, EventArgs e)//移除全部分隔符
        {
            this.listBoxSplitItems.Items.Clear();
        }
    }
}