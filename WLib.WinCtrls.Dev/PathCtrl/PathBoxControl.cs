/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WLib.Files;
using WLib.WinCtrls.Extension;
using WLib.WinCtrls.PathCtrl;

namespace WLib.WinCtrls.Dev.PathCtrl
{
    /// <summary>
    /// 表示一个路径选择与显示的组合框
    /// </summary>
    [DefaultEvent(nameof(AfeterSelectPath))]
    [DefaultProperty(nameof(ShowButtonOption))]
    public partial class PathBoxControl : XtraUserControl
    {
        /// <summary>
        /// 文件筛选，等同于OpenFileDialog或SaveFileDialog控件的Filter属性
        /// </summary>
        private string _fileFilter;
        /// <summary>
        /// 在路径选择框中显示的默认提示
        /// </summary>
        private string _defultTips;
        /// <summary>
        /// 选择和操作按钮的宽度
        /// </summary>
        private int _buttonWidth = 80;
        /// <summary>
        /// 是否允许提供多个文件路径
        /// </summary>
        private bool _multiSelect = true;
        /// <summary>
        /// 路径选择框按钮显示选项
        /// </summary>
        private EShowButtonOption _showButtonOption;


        #region 属性
        /// <summary>
        /// 在路径选择框中显示的默认提示
        /// </summary>
        public string DefaultTips
        {
            get => _defultTips;
            set
            {
                if (string.IsNullOrEmpty(cmbPath.Text.Trim()) || cmbPath.Text == _defultTips)
                    cmbPath.Text = value;
                _defultTips = value;
                if (ReadOnly)
                    cmbPath.ForeColor = Color.Gray;
            }
        }
        /// <summary>
        /// 路径类别（文件路径、文件夹路径）
        /// </summary>
        public ESelectPathType SelectPathType { get; set; }
        /// <summary>
        /// 路径选择框按钮显示选项
        /// </summary>
        public EShowButtonOption ShowButtonOption
        {
            get => _showButtonOption;
            set
            {
                _showButtonOption = value;
                splitPathBox.PanelVisibility = SplitPanelVisibility.Both;
                switch (value)
                {
                    case EShowButtonOption.All: splitButtons.PanelVisibility = SplitPanelVisibility.Both; break;
                    case EShowButtonOption.ViewSelect: splitButtons.PanelVisibility = SplitPanelVisibility.Panel1; break;
                    case EShowButtonOption.ViewOpt: splitButtons.PanelVisibility = SplitPanelVisibility.Panel2; break;
                    case EShowButtonOption.None: splitPathBox.PanelVisibility = SplitPanelVisibility.Panel1; break;
                    case EShowButtonOption.Select: splitButtons.PanelVisibility = SplitPanelVisibility.Panel1; break;
                    case EShowButtonOption.View: splitPathBox.PanelVisibility = SplitPanelVisibility.Panel1; break;
                    case EShowButtonOption.Opt: splitButtons.PanelVisibility = SplitPanelVisibility.Panel2; break;
                }
                ButtonWidthChanged();
                OnResize(new EventArgs());
            }

        }
        /// <summary>
        /// 是否允许提供多个文件路径
        /// </summary>
        public bool MultiSelect
        {
            get => _multiSelect;
            set
            {
                if (!value && cmbPath.Properties.Items.Count > 1)
                {
                    var item = (string)cmbPath.Properties.Items[0];
                    cmbPath.Properties.Items.Clear();
                    Text = item;
                }
                _multiSelect = value;
            }
        }
        /// <summary>
        /// 文件筛选，等同于OpenFileDialog或SaveFileDialog控件的Filter属性
        /// </summary>
        public String FileFilter { get => _fileFilter; set => _fileFilter = value == null || value.Contains("|") ? value : value + "|" + value; }
        /// <summary>
        /// 选择文件或目录的标题或提示信息
        /// </summary>
        public string SelectTips { get; set; }
        /// <summary>
        /// 是否允许编辑路径
        /// </summary>
        public bool ReadOnly
        {
            get => cmbPath.Properties.TextEditStyle == TextEditStyles.DisableTextEditor;
            set => cmbPath.Properties.TextEditStyle = value ? TextEditStyles.DisableTextEditor : TextEditStyles.Standard;
        }
        /// <summary>
        /// 显示的路径
        /// </summary>
        public string Path { get => cmbPath.EditValue?.ToString(); set => cmbPath.EditValue = value; }
        /// <summary>
        /// 显示的路径
        /// </summary>
        public override string Text { get => cmbPath.EditValue?.ToString(); set => cmbPath.EditValue = value; }
        /// <summary>
        /// 获取或设置路径框中的所有路径
        /// </summary>
        public string[] Paths
        {
            get => cmbPath.Properties.Items.Cast<string>().ToArray();
            set
            {
                cmbPath.Properties.Items.Clear();
                cmbPath.Properties.Items.AddRange(value);
                if (cmbPath.Properties.Items.Count > 0)
                    cmbPath.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 标示除浏览按钮外，其余操作是否可用
        /// </summary>
        public bool OptEnable { get => cmbPath.Enabled; set => btnOperate.Enabled = btnSelect.Enabled = cmbPath.Enabled = value; }
        /// <summary>
        /// 路径下拉列表
        /// </summary>
        public ComboBoxEdit PathComboBoxEdit => cmbPath;
        /// <summary>
        /// 验证路径是否存在
        /// </summary>
        public bool PathValidate
        {
            get
            {
                switch (SelectPathType)
                {
                    case ESelectPathType.Folder: return Directory.Exists(Text);
                    case ESelectPathType.OpenFile: return File.Exists(Text);
                    case ESelectPathType.SaveFile:
                        return Directory.Exists(System.IO.Path.GetDirectoryName(Text)) && FileOpt.ValidFileName(System.IO.Path.GetFileNameWithoutExtension(Text));
                    default: return false;
                }
            }
        }
        /// <summary>
        /// 选择和操作按钮的宽度
        /// </summary>
        public int ButtonWidth { get => _buttonWidth; set { _buttonWidth = value < 0 ? 0 : value; ButtonWidthChanged(); } }
        /// <summary>
        /// 选择按钮上显示的文本
        /// </summary>
        public string SelectButtonText { get => btnSelect.Text; set => btnSelect.Text = value; }
        /// <summary>
        /// 操作按钮上显示的文本
        /// </summary>
        public string OperateButtonText { get => btnOperate.Text; set => btnOperate.Text = value; }
        #endregion


        #region 事件
        /// <summary>
        /// 当输入的路径文本改变时发生
        /// </summary>
        public event EventHandler OnPathTextChanged;
        /// <summary>
        /// 点击操作按钮事件
        /// </summary>
        public event EventHandler OperateButtonClick;
        /// <summary>
        /// 选择或设置路径后的事件
        /// </summary>
        public event EventHandler AfeterSelectPath;
        /// <summary>
        /// 自定义路径选择事件
        /// </summary>
        public event EventHandler CustomizeSelectPath;
        #endregion

        /// <summary>
        /// 表示一个路径选择与显示的组合框
        /// </summary>
        public PathBoxControl()
        {
            InitializeComponent();
            BindingControlEvents();

            ShowButtonOption = EShowButtonOption.ViewSelect;
            FileFilter = "全部文件(*.*)|*.*";
            DefaultTips = "粘贴路径于此并按下回车，或点击选择按钮以选择路径";
        }
        /// <summary>
        /// 选择和操作按钮的宽度改变
        /// </summary>
        private void ButtonWidthChanged()
        {
            int buttonSplitWidth = splitButtons.Width - btnSelect.Width - btnOperate.Width;
            int pathToButtonSplitWidth = splitPathBox.Width - cmbPath.Width - splitButtons.Width;
            switch (ShowButtonOption)
            {
                case EShowButtonOption.All:
                    var splitterDisAll = _buttonWidth * 2 + buttonSplitWidth + pathToButtonSplitWidth;
                    splitPathBox.SplitterPosition = splitterDisAll < 1 ? 1 : splitterDisAll;
                    splitButtons.SplitterPosition = _buttonWidth;
                    break;
                case EShowButtonOption.Opt:
                case EShowButtonOption.Select:
                case EShowButtonOption.ViewOpt:
                case EShowButtonOption.ViewSelect:
                    var splitterDisOpt = _buttonWidth + pathToButtonSplitWidth;
                    splitPathBox.SplitterPosition = splitterDisOpt < 1 ? 1 : splitterDisOpt;
                    break;
                default:
                    splitPathBox.SplitterPosition = Width;
                    break;
            }
        }
        /// <summary>
        /// 清理路径
        /// </summary>
        public void Clear() => cmbPath.Properties.Items.Clear();
        /// <summary>
        /// 向路径框中添加路径
        /// </summary>
        /// <param name="path"></param>
        public void Add(string path) => cmbPath.Properties.Items.Add(path);
        /// <summary>
        /// 向路径框中添加多个路径
        /// </summary>
        /// <param name="paths"></param>
        public void AddRange(string[] paths) => cmbPath.Properties.Items.AddRange(paths);
        /// <summary>
        /// 向路径框中添加路径，并触发AfeterSelectPath事件
        /// </summary>
        public void SelectPath(string path)
        {
            Text = path;
            AfeterSelectPath?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// 绑定控件事件处理
        /// </summary>
        private void BindingControlEvents()
        {
            this.cmbPath.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) AfeterSelectPath?.Invoke(this, new EventArgs()); };
            this.cmbPath.Click += (sender, e) => { if (!ReadOnly && Text == _defultTips) cmbPath.Text = string.Empty; };
            this.cmbPath.TextChanged += (sender, e) => { OnPathTextChanged?.Invoke(this, new EventArgs()); };
            this.cmbPath.MouseEnter += (sender, e) => { if ((ShowButtonOption & EShowButtonOption.View) == EShowButtonOption.View) btnView.Visible = true; };
            this.cmbPath.MouseLeave += (sender, e) => { if ((ShowButtonOption & EShowButtonOption.View) == EShowButtonOption.View) btnView.Visible = false; };
            this.cmbPath.GotFocus += (sender, e) =>
            {
                if (cmbPath.Text == _defultTips)
                    cmbPath.Text = string.Empty;
                cmbPath.ForeColor = SystemColors.WindowText;
            };
            this.cmbPath.GotFocus += (sender, e) =>
            {
                if (cmbPath.Text == string.Empty)
                {
                    cmbPath.ForeColor = Color.Gray;
                    cmbPath.Text = _defultTips;
                }
            };
        }


        private void btnSelect_Click(object sender, EventArgs e)
        {
            var path = cmbPath.Text.Trim();
            if (SelectPathType == ESelectPathType.Customize)
                CustomizeSelectPath?.Invoke(this, new EventArgs());
            else
            {
                var selPath = DialogOpt.ShowDialog(SelectPathType, path, SelectTips, FileFilter, path);
                if (!string.IsNullOrEmpty(selPath))
                {
                    cmbPath.Text = selPath;
                    AfeterSelectPath?.Invoke(this, new EventArgs());
                }
            }
            if (cmbPath.Text != string.Empty && cmbPath.Text != _defultTips)
                cmbPath.ForeColor = SystemColors.WindowText;
        }

        private void btnSave_Click(object sender, EventArgs e) => OperateButtonClick?.Invoke(sender, e);

        private void btnView_Click(object sender, EventArgs e)
        {
            var path = cmbPath.Text.Trim();
            if (Directory.Exists(path)) Process.Start("explorer.exe", path);
            else if (File.Exists(path)) Process.Start("explorer.exe", "/select," + path);
        }
    }
}
