/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WLib.WinCtrls.AddItemCtrl.Base;

namespace WLib.WinCtrls.Dev.AddItemCtrl
{
    /// <summary>
    /// 把输入的字符串按分隔符或正则表达式筛选出所需的列表的窗体
    /// </summary>
    public partial class AddItemForm : DevExpress.XtraEditors.XtraForm, IMatchModeConfig
    {
        #region 接口属性
        /// <summary>
        /// 匹配模式（0-Split-根据分隔符分隔文本以获得文本项； 1-Regex-根据正则表达式获得匹配的文本项）
        /// </summary>
        public EMatchMode MatchMode { get; set; }
        /// <summary>
        /// 正则表达式
        /// </summary>
        public string RegexString { get; set; }
        /// <summary>
        /// 默认可在下拉框中选择的分隔符
        /// </summary>
        public string[] DefaultSplitArray { get; set; }
        /// <summary>
        /// 分隔符数组
        /// </summary>
        public string[] SplitStringArray { get; set; }
        #endregion


        #region 其他属性
        /// <summary>
        /// 提示信息（默认："从以下文字中筛选："）
        /// </summary>
        public string Tip { get => this.lblTips.Text; set => this.lblTips.Text = value; }
        /// <summary>
        /// 筛选所得项的提示信息（默认："获取的项："）
        /// </summary>
        public string FilterItemTip { get => this.lblFilterItemTips.Text; set => this.lblFilterItemTips.Text = value; }
        /// <summary>
        /// 是否显示“筛选规则设置”按钮（设置分隔符或正则表达式）
        /// </summary>
        public bool ShowSettingButton { get => this.sBtnSettings.Visible; set => this.sBtnSettings.Visible = value; }
        /// <summary>
        /// 获取的项
        /// </summary>
        public string[] Items
        {
            get => this.listBoxCtrlItems.Items.Cast<string>().ToArray();
            set
            {
                this.listBoxCtrlItems.Items.Clear();
                this.listBoxCtrlItems.Items.AddRange(value);
                if (this.listBoxCtrlItems.Items.Count > 0)
                    this.sBtnOK.Enabled = true;
            }
        }
        #endregion


        #region 构造函数
        /// <summary>
        /// 将输入的文本按分隔符或正则表达式筛选出所需的列表的窗体，默认按换行符筛选
        /// </summary>
        public AddItemForm()
        {
            InitializeComponent();

            this.MatchMode = EMatchMode.Split;
            this.SplitStringArray = new[] { Environment.NewLine };
        }
        /// <summary>
        /// 将输入的文本按正则表达式筛选出所需的列表的窗体
        /// </summary>
        /// <param name="title">窗体的标题</param>
        /// <param name="tip">提示信息</param>
        /// <param name="regexString">获取匹配项的正则表达式</param>
        public AddItemForm(string title, string tip, string regexString)
        {
            InitializeComponent();

            this.Text = title;
            this.Tip = tip;
            this.MatchMode = EMatchMode.Regex;
            this.RegexString = regexString;
        }
        /// <summary>
        /// 将输入的字符串按分隔符筛选出所需的列表的窗体
        /// </summary>
        /// <param name="title">窗体的标题</param>
        /// <param name="tip">提示信息</param>
        /// <param name="splitStringArray">分割筛选匹配项的分隔符数组</param>
        public AddItemForm(string title, string tip, string[] splitStringArray)
        {
            InitializeComponent();

            this.Text = title;
            this.Tip = tip;
            this.MatchMode = EMatchMode.Split;
            this.SplitStringArray = splitStringArray;
        }
        #endregion


        /// <summary>
        /// 启用/禁用添加删除的相关控件
        /// </summary>
        /// <param name="enable"></param>
        public void EnableAddRemoveCtrl(bool enable)
        {
            this.移除全部CToolStripMenuItem.Enabled = enable;
            this.移除选中项DToolStripMenuItem.Enabled = enable;
            this.复制选中项ToolStripMenuItem.Enabled = enable;
            this.复制全部ToolStripMenuItem.Enabled = enable;
            this.移除重复项RToolStripMenuItem.Enabled = enable;
            this.sBtnRemoveRepeat.Enabled = enable;
            this.sBtnClear.Enabled = enable;
            this.sBtnOK.Enabled = enable;
        }

        private void sBtnAdd_Click(object sender, EventArgs e)//添加
        {
            if (MatchMode == EMatchMode.Regex)
            {
                foreach (Match m in Regex.Matches(this.txtInput.Text, RegexString))
                {
                    foreach (Group g in m.Groups)
                    {
                        this.listBoxCtrlItems.Items.Add(g.Value);
                    }
                }
            }
            else if (MatchMode == EMatchMode.Split)
            {
                var items = this.txtInput.Text.Split(SplitStringArray, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in items)
                {
                    this.listBoxCtrlItems.Items.Add(item);
                }
            }
            if (this.listBoxCtrlItems.Items.Count > 0)
                EnableAddRemoveCtrl(true);
        }

        private void sBtnClear_Click(object sender, EventArgs e)//清除
        {
            this.listBoxCtrlItems.Items.Clear();
            EnableAddRemoveCtrl(false);
        }

        private void sBtnRemoveRepeat_Click(object sender, EventArgs e)//移除重复项
        {
            if (this.listBoxCtrlItems.Items.Count > 0)
            {
                this.listBoxCtrlItems.Items.Clear();
                this.listBoxCtrlItems.Items.AddRange(this.listBoxCtrlItems.Items.Cast<string>().Distinct().ToArray());
            }
        }

        private void sBtnSettings_Click(object sender, EventArgs e)//设置
        {
            AddItemConfigForm form = new AddItemConfigForm(MatchMode, RegexString, SplitStringArray);
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.MatchMode = form.MatchMode;
                this.RegexString = form.RegexString;
                this.SplitStringArray = form.SplitStringArray;
            }
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

        private void listBoxCtrlItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.移除选中项DToolStripMenuItem.Enabled = this.复制选中项ToolStripMenuItem.Enabled =
                this.listBoxCtrlItems.SelectedIndex > -1;
        }

        private void 移除选中项DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listBoxCtrlItems.Items.Remove(this.listBoxCtrlItems.SelectedItem);
            if (this.listBoxCtrlItems.Items.Count == 0)
                EnableAddRemoveCtrl(false);
        }

        private void 复制选中项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.listBoxCtrlItems.SelectedItem.ToString());
        }

        private void 复制全部ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.listBoxCtrlItems.Items.Cast<string>().Aggregate((a, b) => a + Environment.NewLine + b));
        }
    }
}