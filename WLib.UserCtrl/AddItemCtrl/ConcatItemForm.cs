/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Linq;
using WLib.UserCtrls.AddItemCtrl.Base;

namespace WLib.UserCtrls.AddItemCtrl
{
    /// <summary>
    /// 把输入的文本按分隔符或正则表达式筛选出所需的列表，再拼接列表项的窗体
    /// </summary>
    public partial class ConcatItemForm : AddItemForm, IConcatStrConfig
    {
        #region 接口属性
        /// <summary>
        /// 是否每一项都添加前缀和后缀
        /// </summary>
        public bool IsAddPrefixSuffix { get => this.cbAddPrefixSuffix.Checked; set => this.cbAddPrefixSuffix.Checked = value; }
        /// <summary>
        /// 对每一项文本所添加前缀
        /// </summary>
        public string Prefix { get => this.txtPrefix.Text; set => this.txtPrefix.Text = value; }
        /// <summary>
        /// 对每一项文本所添加后缀
        /// </summary>
        public string Suffix { get => this.txtSuffix.Text; set => this.txtSuffix.Text = value; }
        /// <summary>
        /// 是否将全部项串联成一项
        /// </summary>
        public bool IsConcatAll { get => this.cbConcatAll.Checked; set => this.cbConcatAll.Checked = value; }
        /// <summary>
        /// 串联每一项的连接符
        /// </summary>
        public string ConStr { get => this.txtConStr.Text; set => this.txtConStr.Text = value; }
        #endregion


        #region 其他属性
        /// <summary>
        /// 拼接前缀和后缀之后的每一项文本
        /// </summary>
        public string[] ConcatItems => Items.Select(v => Prefix + v + Suffix).ToArray();
        /// <summary>
        /// 根据连接符连接全部项之后的文本
        /// </summary>
        public string ResultString => (IsAddPrefixSuffix ? ConcatItems : Items).Aggregate((a, b) => a + ConStr + b);
        #endregion


        #region 构造函数
        /// <summary>
        /// 把输入的文本按分隔符或正则表达式筛选出所需的列表，再拼接列表项的窗体
        /// </summary>
        public ConcatItemForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 把输入的文本按正则表达式筛选出所需的列表，再拼接列表项的窗体
        /// </summary>
        /// <param name="title">窗体的标题</param>
        /// <param name="tip"></param>
        /// <param name="regexString">获取匹配项的正则表达式</param>
        public ConcatItemForm(string title, string tip, string regexString) : base(title, tip, regexString)
        {
            InitializeComponent();
        }
        /// <summary>
        /// 把输入的文本按分隔符筛选出所需的列表，再拼接列表项的窗体
        /// </summary>
        /// <param name="title">窗体的标题</param>
        /// <param name="tip"></param>
        /// <param name="splitStringArray">分割筛选匹配项的分隔符数组</param>
        public ConcatItemForm(string title, string tip, string[] splitStringArray) : base(title, tip, splitStringArray)
        {
            InitializeComponent();
        }
        #endregion


        private void cbAddPrefixSuffix_CheckedChanged(object sender, EventArgs e)
        {
            this.tblPanelPreSubffix.Enabled = this.cbAddPrefixSuffix.Checked;
        }

        private void cbConcatAll_CheckedChanged(object sender, EventArgs e)
        {
            this.tblPanelConcatStr.Enabled = this.cbConcatAll.Checked;
        }
    }
}
