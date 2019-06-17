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
    /// 路径拼接或筛选窗体
    /// </summary>
    public partial class ConcatPathForm : Form
    {
        /// <summary>
        /// 区分是拼接路径，还是分隔路径
        /// </summary>
        public EPathSelectMode PathSelectMode
        {
            get => this.radioConcat.Checked ? EPathSelectMode.Concat : EPathSelectMode.Filter;
            set => this.radioConcat.Checked = value == EPathSelectMode.Concat;
        }
        /// <summary>
        /// 是否允许切换路径拼接/筛选
        /// </summary>
        public bool EnableSwitchSelectMode
        {
            set
            {
                if (value) { this.radioConcat.Enabled = this.radioFilter.Enabled = true; }
                else
                {
                    bool isConcat = PathSelectMode == EPathSelectMode.Concat;
                    this.radioConcat.Enabled = isConcat;
                    this.radioFilter.Enabled = !isConcat;
                }
            }
        }
        /// <summary>
        /// 路径拼接中所指定的目录
        /// </summary>
        public string Directory { get => this.pathBoxDir.Text; set => this.pathBoxDir.Text = value; }
        /// <summary>
        /// 拼接或筛选的结果（拼接后的全路径列表，或是筛选后的文件夹名/文件名）
        /// </summary>
        public string[] ResultItems => this.listBoxResult.Items.Cast<string>().ToArray();


        /// <summary>
        /// 路径筛选或拼接窗体
        /// </summary>
        /// <param name="ePathSelectMode">区分是拼接路径，还是分隔路径</param>
        /// <param name="suffix">在结果列表每一项中加入的后缀，可以为null或Empty</param>
        /// <param name="enableSwitchSelectMode"></param>
        public ConcatPathForm(EPathSelectMode ePathSelectMode = EPathSelectMode.Concat, string suffix = null, bool enableSwitchSelectMode = false)
        {
            InitializeComponent();

            this.splitContrInput.Panel2Collapsed = true;
            this.cListBoxSplitDef.Items.AddRange(SplitStrDef.SourceSplits);
            this.cListBoxSplitDef.SetItemChecked(0, true);
            this.PathSelectMode = ePathSelectMode;
            this.EnableSwitchSelectMode = enableSwitchSelectMode;
            if (!string.IsNullOrEmpty(suffix))
            {
                this.cbContainsSuffix.Checked = true;
                this.txtSuffix.Text = suffix;
            }
        }

        private void radioFilter_CheckedChanged(object sender, EventArgs e)//选中不同的选项(radioButton)，显示不同的界面信息
        {
            bool isFilter = this.radioFilter.Checked;
            this.panelDir.Enabled = !isFilter;
            this.lblInputTips.Text = isFilter ? "路径列表：" : "文件夹名/文件名列表：";
            this.lblResult.Text = isFilter ? "路径列表：" : "拼接结果：";
            this.btnConcat.Text = isFilter ? "筛选(&F)" : "拼接(&F)";
        }

        private void cbContainsSuffix_CheckedChanged(object sender, EventArgs e)//是否加入后缀
        {
            this.txtSuffix.Visible = this.cbContainsSuffix.Checked;
        }

        private void btnConcat_Click(object sender, EventArgs e)//拼接/筛选
        {
            try
            {
                this.listBoxResult.Items.Clear();
                var extension = this.cbContainsSuffix.Checked ? this.txtSuffix.Text.Trim() : string.Empty;//后缀
                var splitDefItems = this.cListBoxSplitDef.CheckedItems.Cast<SplitStrDef>().ToArray();
                var splitStrArray = splitDefItems.Select(v => v.Value).ToArray();//分隔符数组
                var items = this.txtInput.Text.Split(splitStrArray, StringSplitOptions.RemoveEmptyEntries);//按分隔符分隔后的项
                var dir = this.pathBoxDir.Text.Trim();
                if (items.Length > 0)
                {
                    if (PathSelectMode == EPathSelectMode.Concat)
                        this.listBoxResult.Items.AddRange(items.Select(v => System.IO.Path.Combine(dir, v + extension)).ToArray());
                    else if (PathSelectMode == EPathSelectMode.Filter)
                    {
                        foreach (var item in items)
                        {
                            int lastIndex = item.LastIndexOf(System.IO.Path.DirectorySeparatorChar);
                            if (lastIndex > -1)
                                this.listBoxResult.Items.Add(item.Substring(lastIndex) + extension);
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void cbSettings_CheckedChanged(object sender, EventArgs e)//是否展开分隔符设置列表
        {
            this.splitContrInput.Panel2Collapsed = !this.cbSettings.Checked;
        }

        private void btnOK_Click(object sender, EventArgs e)//确定
        {
            if (this.listBoxResult.Items.Count == 0)
                btnConcat_Click(null, null);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)//取消
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
