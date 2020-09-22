/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WLib.Files;
using WLib.WinCtrls.Extension;

namespace WLib.WinCtrls.PathCtrl
{
    /// <summary>
    /// 表示一个路径选择与显示的组合框
    /// </summary>
    [DefaultEvent(nameof(AfeterSelectPath))]
    [DefaultProperty(nameof(ShowButtonOption))]
    public partial class PathBoxSimple : UserControl
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
                if (string.IsNullOrEmpty(txtPath.Text.Trim()) || txtPath.Text == _defultTips)
                    txtPath.Text = value;
                _defultTips = value;
                if (ReadOnly)
                    txtPath.ForeColor = Color.Gray;
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
                splitPathBox.Panel2Collapsed = false;
                switch (value)
                {
                    case EShowButtonOption.All: splitButtons.Panel1Collapsed = splitButtons.Panel2Collapsed = false; break;
                    case EShowButtonOption.ViewSelect: splitButtons.Panel2Collapsed = true; break;
                    case EShowButtonOption.ViewOpt: splitButtons.Panel1Collapsed = true; break;
                    case EShowButtonOption.None: splitPathBox.Panel2Collapsed = true; break;
                    case EShowButtonOption.Select: splitButtons.Panel2Collapsed = true; break;
                    case EShowButtonOption.View: splitPathBox.Panel2Collapsed = true; break;
                    case EShowButtonOption.Opt: splitButtons.Panel1Collapsed = true; break;
                }
                ButtonWidthChanged();
                OnResize(new EventArgs());
            }
        }
        /// <summary>
        /// 文件筛选，等同于OpenFileDialog或SaveFileDialog控件的Filter属性
        /// </summary>
        public string FileFilter { get => _fileFilter; set => _fileFilter = value == null || value.Contains("|") ? value : value + "|" + value; }
        /// <summary>
        /// 选择文件或目录的标题或提示信息
        /// </summary>
        public string SelectTips { get; set; }
        /// <summary>
        /// 路径文本框只读
        /// </summary>
        public bool ReadOnly { get => txtPath.ReadOnly; set => txtPath.ReadOnly = value; }
        /// <summary>
        /// 显示的路径
        /// </summary>
        public string Path { get => txtPath.Text; set { txtPath.Text = value; txtPath.ForeColor = SystemColors.WindowText; } }
        /// <summary>
        /// 显示的路径
        /// </summary>
        public override string Text { get => Path; set => Path = value; }
        /// <summary>
        /// 标示除浏览按钮外，其余操作是否可用
        /// </summary>
        public bool OptEnable { get => txtPath.Enabled; set => btnOperate.Enabled = btnSelect.Enabled = txtPath.Enabled = value; }
        /// <summary>
        /// 选择按钮是否可用
        /// </summary>
        public bool SelectEnable { get => btnSelect.Enabled; set => btnSelect.Enabled = value; }
        /// <summary>
        /// 路径文本框
        /// </summary>
        public TextBox PathTextBox => txtPath;
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
                        return Directory.Exists(System.IO.Path.GetDirectoryName(Text)) && PathEx.FileNameIsValid(System.IO.Path.GetFileNameWithoutExtension(Text));
                    default: return false;
                }
            }
        }
        /// <summary>
        /// 选择和操作按钮的宽度
        /// </summary>
        public int ButtonWidth { get => _buttonWidth; set { _buttonWidth = value < 0 ? 0 : value; ButtonWidthChanged(); } }
        /// <summary>
        /// 选择和操作按钮之间的距离（像素）
        /// </summary>
        public int ButtonsSplitWidth { get => splitButtons.SplitterWidth; set { splitButtons.SplitterWidth = value < 0 ? 0 : value; ButtonWidthChanged(); } }
        /// <summary>
        /// 路径文本框和选择(或操作)按钮之间的距离（像素）
        /// </summary>
        public int PathToButtonSplitWidth { get => splitPathBox.SplitterWidth; set { splitPathBox.SplitterWidth = value < 0 ? 0 : value; ButtonWidthChanged(); } }
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
        public PathBoxSimple()
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
            switch (ShowButtonOption)
            {
                case EShowButtonOption.All:
                    var splitterDisAll = Width - _buttonWidth * 2 - PathToButtonSplitWidth - ButtonsSplitWidth;
                    splitPathBox.SplitterDistance = splitterDisAll < 1 ? 1 : splitterDisAll;
                    splitButtons.SplitterDistance = _buttonWidth;
                    break;
                case EShowButtonOption.Opt:
                case EShowButtonOption.Select:
                case EShowButtonOption.ViewOpt:
                case EShowButtonOption.ViewSelect:
                    var splitterDisOpt = Width - _buttonWidth - PathToButtonSplitWidth;
                    splitPathBox.SplitterDistance = splitterDisOpt < 1 ? 1 : splitterDisOpt;
                    break;
                default:
                    splitPathBox.SplitterDistance = Width;
                    break;
            }
        }
        /// <summary>
        /// 清理路径
        /// </summary>
        public void Clear() => txtPath.Clear();
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
            txtPath.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) AfeterSelectPath?.Invoke(this, new EventArgs()); };
            txtPath.Click += (sender, e) => { if (!ReadOnly && Text == _defultTips) txtPath.Text = string.Empty; };
            txtPath.TextChanged += (sender, e) => { OnPathTextChanged?.Invoke(this, new EventArgs()); };
            txtPath.MouseEnter += (sender, e) => { if ((ShowButtonOption & EShowButtonOption.View) == EShowButtonOption.View) btnView.Visible = true; };
            txtPath.MouseLeave += (sender, e) =>
            {
                if ((ShowButtonOption & EShowButtonOption.View) == EShowButtonOption.View)
                {
                    if (!txtPath.RectangleToScreen(txtPath.ClientRectangle).Contains(MousePosition))
                        btnView.Visible = false;
                }
            };
            txtPath.GotFocus += (sender, e) =>
            {
                if (txtPath.Text == _defultTips)
                    txtPath.Text = string.Empty;
                txtPath.ForeColor = SystemColors.WindowText;
            };
            txtPath.GotFocus += (sender, e) =>
            {
                if (txtPath.Text == string.Empty)
                {
                    txtPath.ForeColor = Color.Gray;
                    txtPath.Text = _defultTips;
                }
            };
        }


        private void btnSelect_Click(object sender, EventArgs e)
        {
            var path = txtPath.Text.Trim();
            if (SelectPathType == ESelectPathType.Customize)
                CustomizeSelectPath?.Invoke(this, new EventArgs());
            else
            {
                txtPath.Text = DialogOpt.ShowDialog(SelectPathType, path, SelectTips, FileFilter, path);
                AfeterSelectPath?.Invoke(this, new EventArgs());
            }
            if (txtPath.Text != string.Empty && txtPath.Text != _defultTips)
                txtPath.ForeColor = SystemColors.WindowText;
        }

        private void btnOperate_Click(object sender, EventArgs e) => OperateButtonClick?.Invoke(this, new EventArgs());

        private void btnView_Click(object sender, EventArgs e)
        {
            var path = txtPath.Text.Trim();
            if (Directory.Exists(path)) Process.Start("explorer.exe", path);
            else if (File.Exists(path)) Process.Start("explorer.exe", "/select," + path);
        }
    }
}
