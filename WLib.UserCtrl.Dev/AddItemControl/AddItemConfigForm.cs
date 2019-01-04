using System;
using System.Linq;
using System.Windows.Forms;
using WLib.UserCtrls.AddItemControl.Base;

namespace WLib.UserCtrls.Dev.AddItemControl
{
    /// <summary>
    /// 数据筛选设置窗口
    /// </summary>
    public partial class AddItemConfigForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 默认可在下拉框中选择的分隔符
        /// </summary>
        public string[] DefaultSplitArray
        {
            get => this.cmbFilterString.Properties.Items.Cast<string>().ToArray();
            set
            {
                this.cmbFilterString.Properties.Items.Clear();
                this.cmbFilterString.Properties.Items.AddRange(value);
            }
        }
        /// <summary>
        /// 匹配模式（0-Split-根据分隔符分割字符串，所添加的项是分割后的字符串； 1-Regex-根据正则表达式获得匹配项）
        /// </summary>
        public EMatchMode MatchMode => this.radioSplitString.Checked ? EMatchMode.Split : EMatchMode.Regex;
        /// <summary>
        /// 正则表达式
        /// </summary>
        public string RegexString { get => this.txtRegex.Text.Trim(); set => this.txtRegex.Text = value; }
        /// <summary>
        /// 分隔符数组
        /// </summary>
        public string[] SplitStringArray
        {
            get
            {
                return this.listBoxSplitItems.Items.Cast<SplitStrDef>().Select(v => v.Value).ToArray();
            }
            set
            {
                this.listBoxSplitItems.Items.Clear();
                if (value != null && value.Length > 0)
                    this.listBoxSplitItems.Items.AddRange(value.Select(v => new SplitStrDef(v)).ToArray());
            }
        }


        /// <summary>
        /// 数据筛选设置窗口
        /// </summary>
        /// <param name="mode"> 匹配模式（0-Split-根据分隔符分割字符串，所添加的项是分割后的字符串； 1-Regex-根据正则表达式获得匹配项）</param>
        public AddItemConfigForm(EMatchMode mode = EMatchMode.Split)
        {
            InitializeComponent();
            this.cmbFilterString.Properties.Items.AddRange(SplitStrDef.SourceSplits);
            this.radioSplitString.Checked = mode == EMatchMode.Split;
            this.radioRegex.Checked = mode == EMatchMode.Regex;
        }
        /// <summary>
        /// 数据筛选设置窗口
        /// </summary>
        /// <param name="regexString">正则表达式</param>
        public AddItemConfigForm(string regexString)
        {
            InitializeComponent();
            this.cmbFilterString.Properties.Items.AddRange(SplitStrDef.SourceSplits);
            this.radioRegex.Checked = true;
            this.RegexString = regexString;
        }
        /// <summary>
        /// 数据筛选设置窗口
        /// </summary>
        /// <param name="splitStringArray">分隔字符串数组</param>
        public AddItemConfigForm(string[] splitStringArray)
        {
            InitializeComponent();
            this.cmbFilterString.Properties.Items.AddRange(SplitStrDef.SourceSplits);
            this.radioSplitString.Checked = true;
            this.SplitStringArray = splitStringArray;
        }


        private void checkFilterString_CheckedChanged(object sender, EventArgs e)//选中按分隔符，还是按正则表达式筛选
        {
            bool isSplitString = this.radioSplitString.Checked;
            this.groupBoxSplitString.Enabled = isSplitString;
            this.groupBoxRegex.Enabled = !isSplitString;
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
                this.listBoxSplitItems.Items.Add(this.cmbFilterString.EditValue.ToString());
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