using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WLib.UserCtrls.PathControl
{
    /// <summary>
    /// 表示一个路径选择与显示的组合框
    /// </summary>
    [DefaultEvent("AfeterSelectPath")]
    [DefaultProperty("ShowButtonOption")]
    public partial class PathBoxSimple : UserControl
    {
        private string _fileFilter;
        private string _defultTips;
        private int _buttonWidth = 80;
        private EShowButtonOption _showButtonOption;

        #region 属性
        /// <summary>
        /// 在路径选择框中显示的默认提示
        /// </summary>
        public string DefaultTips
        {
            get => this._defultTips;
            set
            {
                if (string.IsNullOrEmpty(this.txtPath.Text.Trim()) || this.txtPath.Text == this._defultTips)
                    this.txtPath.Text = value;
                this._defultTips = value;
                if (!ReadOnly)
                    this.txtPath.ForeColor = Color.Gray;
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
            get => this._showButtonOption;
            set
            {
                this._showButtonOption = value;
                this.splitContainerPathBox.Panel2Collapsed = false;
                switch (value)
                {
                    case EShowButtonOption.ViewSelect:
                        this.splitContainerButtons.Panel2Collapsed = true;
                        break;
                    case EShowButtonOption.ViewSave:
                        this.splitContainerButtons.Panel1Collapsed = true;
                        break;
                    case EShowButtonOption.All:
                        this.splitContainerButtons.Panel1Collapsed = false;
                        this.splitContainerButtons.Panel2Collapsed = false;
                        break;
                    case EShowButtonOption.None:
                        this.splitContainerPathBox.Panel2Collapsed = true;
                        break;
                    case EShowButtonOption.Select:
                        this.splitContainerButtons.Panel2Collapsed = true;
                        break;
                    case EShowButtonOption.View:
                        this.splitContainerPathBox.Panel2Collapsed = true;
                        break;
                    case EShowButtonOption.Opt:
                        this.splitContainerButtons.Panel1Collapsed = true;
                        break;
                }
                ButtonWidthChanged();
                this.OnResize(new EventArgs());
            }
        }
        /// <summary>
        /// 文件名筛选器字符串
        /// </summary>
        public String FileFilter
        {
            get => this._fileFilter;
            set => this._fileFilter = (value != null && !value.Contains("|")) ? value + "|" + value : value;
        }
        /// <summary>
        /// 选择文件或目录的标题或提示信息
        /// </summary>
        public string SelectTips { get; set; }
        /// <summary>
        /// 路径文本框只读
        /// </summary>
        public bool ReadOnly
        {
            get => this.txtPath.ReadOnly;
            set => this.txtPath.ReadOnly = value;
        }
        /// <summary>
        /// 显示的路径
        /// </summary>
        public string Path
        {
            get => this.txtPath.Text;
            set { this.txtPath.Text = value; this.txtPath.ForeColor = SystemColors.WindowText; }
        }
        /// <summary>
        /// 显示的路径
        /// </summary>
        public override string Text
        {
            get => this.Path;
            set => this.Path = value;
        }
        /// <summary>
        /// 标示除浏览按钮外，其余操作是否可用
        /// </summary>
        public bool OptEnable
        {
            get => this.txtPath.Enabled;
            set
            {
                this.txtPath.Enabled = value;
                this.btnSelect.Enabled = value;
                this.btnOperate.Enabled = value;
            }
        }
        /// <summary>
        /// 选择按钮是否可用
        /// </summary>
        public bool SelectEnable { get => this.btnSelect.Enabled;
            set => this.btnSelect.Enabled = value;
        }
        /// <summary>
        /// 路径文本框
        /// </summary>
        public TextBox PathTextBox => this.txtPath;

        /// <summary>
        /// 验证路径是否存在
        /// </summary>
        public bool PathValidate
        {
            get
            {
                switch (SelectPathType)
                {
                    case ESelectPathType.Folder:
                        return Directory.Exists(this.Text);
                    case ESelectPathType.OpenFile:
                        return File.Exists(this.Text);
                    case ESelectPathType.SaveFile:
                        return Directory.Exists(System.IO.Path.GetDirectoryName(this.Text)) && ValidFileName(System.IO.Path.GetFileNameWithoutExtension(this.Text));
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// 选择和操作按钮的宽度
        /// </summary>
        public int ButtonWidth
        {
            get => this._buttonWidth;
            set { this._buttonWidth = value < 0 ? 0 : value; ButtonWidthChanged(); }
        }
        /// <summary>
        /// 选择和操作按钮之间的距离（像素）
        /// </summary>
        public int ButtonsSplitWidth
        {
            get => this.splitContainerButtons.SplitterWidth;
            set { this.splitContainerButtons.SplitterWidth = value < 0 ? 0 : value; ButtonWidthChanged(); }
        }
        /// <summary>
        /// 路径文本框和选择(或操作)按钮之间的距离（像素）
        /// </summary>
        public int PathToButtonSplitWidth
        {
            get => this.splitContainerPathBox.SplitterWidth;
            set { this.splitContainerPathBox.SplitterWidth = value < 0 ? 0 : value; ButtonWidthChanged(); }
        }
        /// <summary>
        /// 选择按钮上显示的文本
        /// </summary>
        public string SelectButtonText { get => this.btnSelect.Text;
            set => this.btnSelect.Text = value;
        }
        /// <summary>
        /// 操作按钮上显示的文本
        /// </summary>
        public string OperateButtonText { get => this.btnOperate.Text;
            set => this.btnOperate.Text = value;
        }
        #endregion


        #region 事件
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
        /// <summary>
        /// 触发选择或设置路径后的事件
        /// </summary>
        protected void OnAfeterSelectPath()
        {
            AfeterSelectPath?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// 触发点击操作按钮事件
        /// </summary>
        protected void OnOperateButtonClick()
        {
            OperateButtonClick?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// 触发自定义路径选择事件
        /// </summary>
        protected void OnCustomizeSelectPath()
        {
            CustomizeSelectPath?.Invoke(this, new EventArgs());
        }
        #endregion


        /// <summary>
        /// 表示一个路径选择与显示的组合框
        /// </summary>
        public PathBoxSimple()
        {
            InitializeComponent();

            this.txtPath.GotFocus += txtPath_GotFocus;
            this.txtPath.LostFocus += txtPath_LostFocus;
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
                    var splitterDisAll = this.Width - this._buttonWidth * 2 - this.PathToButtonSplitWidth - this.ButtonsSplitWidth;
                    this.splitContainerPathBox.SplitterDistance = splitterDisAll < 1 ? 1 : splitterDisAll;
                    this.splitContainerButtons.SplitterDistance = this._buttonWidth;
                    break;
                case EShowButtonOption.Opt:
                case EShowButtonOption.Select:
                case EShowButtonOption.ViewSave:
                case EShowButtonOption.ViewSelect:
                    var splitterDisOpt = this.Width - this._buttonWidth - this.PathToButtonSplitWidth;
                    this.splitContainerPathBox.SplitterDistance = splitterDisOpt < 1 ? 1 : splitterDisOpt;
                    break;
                default:
                    this.splitContainerPathBox.SplitterDistance = this.Width;
                    break;
            }
        }
        /// <summary>
        /// 清理路径
        /// </summary>
        public void Clear()
        {
            this.txtPath.Clear();
        }
        /// <summary>
        /// 向路径框中添加路径，并触发AfeterSelectPath事件
        /// </summary>
        public void SelectPath(string path)
        {
            this.Text = path;
            OnAfeterSelectPath();
        }
        /// <summary>
        /// 检查文件名是否合法：文字名中不能包含字符\/:*?"<>|
        /// </summary>
        /// <param name="fileName">文件名,不包含路径</param>
        /// <returns></returns>
        public static bool ValidFileName(string fileName)
        {
            bool isValid = true;
            string errChar = "\\/:*?\"<>|";
            if (string.IsNullOrEmpty(fileName))
            {
                isValid = false;
            }
            else
            {
                for (int i = 0; i < errChar.Length; i++)
                {
                    if (fileName.Contains(errChar[i].ToString()))
                    {
                        isValid = false;
                        break;
                    }
                }
            }
            return isValid;
        }


        private void btnSelect_Click(object sender, EventArgs e)
        {
            string path = this.txtPath.Text.Trim();
            if (SelectPathType == ESelectPathType.Folder)
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                dlg.Description = SelectTips;
                dlg.ShowNewFolderButton = true;

                if (System.IO.Directory.Exists(path))
                    dlg.SelectedPath = path;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.txtPath.Text = dlg.SelectedPath;
                    OnAfeterSelectPath();
                }
            }
            else if (SelectPathType == ESelectPathType.OpenFile)
            {
                OpenFileDialog dlg = new OpenFileDialog();
                try
                {
                    dlg.Filter = FileFilter;
                }
                catch { }
                dlg.Title = SelectTips;

                if (!System.IO.Path.IsPathRooted(path))
                    path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

                if (System.IO.Directory.Exists(path))
                    dlg.InitialDirectory = path;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.txtPath.Text = dlg.FileName;
                    OnAfeterSelectPath();
                }
            }
            else if (SelectPathType == ESelectPathType.SaveFile)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                try
                {
                    dlg.Filter = FileFilter;
                }
                catch { }
                dlg.Title = SelectTips;
                if (System.IO.Directory.Exists(path))
                    dlg.InitialDirectory = path;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.txtPath.Text = dlg.FileName;
                    OnAfeterSelectPath();
                }
            }
            else if (SelectPathType == ESelectPathType.Customize)
            {
                OnCustomizeSelectPath();
            }

            if (this.txtPath.Text != string.Empty && this.txtPath.Text != _defultTips)
                this.txtPath.ForeColor = SystemColors.WindowText;
        }

        private void btnOperate_Click(object sender, EventArgs e)
        {
            OnOperateButtonClick();
        }

        private void txtPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                OnAfeterSelectPath();
        }

        private void txtPath_Click(object sender, EventArgs e)
        {
            if (!this.ReadOnly && this.Text == _defultTips)
                this.txtPath.Text = string.Empty;
        }

        private void txtPath_GotFocus(object sender, EventArgs e)
        {
            if (this.txtPath.Text == _defultTips)
                this.txtPath.Text = string.Empty;

            this.txtPath.ForeColor = System.Drawing.SystemColors.WindowText;
        }

        private void txtPath_LostFocus(object sender, EventArgs e)
        {
            if (this.txtPath.Text == string.Empty)
            {
                this.txtPath.ForeColor = Color.Gray;
                this.txtPath.Text = _defultTips;
            }
        }

        private void picBoxViewFile_MouseEnter(object sender, EventArgs e)
        {
            this.picBoxViewFile.BackColor = SystemColors.ControlDark;
        }

        private void picBoxViewFile_MouseLeave(object sender, EventArgs e)
        {
            this.picBoxViewFile.BackColor = SystemColors.Control;
            txtPath_MouseLeave(null, null);
        }

        private void picBoxViewFile_Click(object sender, EventArgs e)
        {
            string path = this.txtPath.Text.Trim();
            if (System.IO.Directory.Exists(path))
            {
                System.Diagnostics.Process.Start("explorer.exe", path);
            }
            else if (System.IO.File.Exists(path))
            {
                System.Diagnostics.Process.Start("explorer.exe", "/select," + path);
            }
        }

        private void txtPath_MouseEnter(object sender, EventArgs e)
        {
            if (_showButtonOption == EShowButtonOption.All ||
                _showButtonOption == EShowButtonOption.View ||
                _showButtonOption == EShowButtonOption.ViewSave ||
                _showButtonOption == EShowButtonOption.ViewSelect)
                this.picBoxViewFile.Visible = true;
        }

        private void txtPath_MouseLeave(object sender, EventArgs e)
        {
            if (_showButtonOption == EShowButtonOption.All ||
                _showButtonOption == EShowButtonOption.View ||
                _showButtonOption == EShowButtonOption.ViewSave ||
                _showButtonOption == EShowButtonOption.ViewSelect)
            {
                Rectangle rectangle = this.txtPath.RectangleToScreen(this.txtPath.ClientRectangle);
                if (!rectangle.Contains(MousePosition))
                    this.picBoxViewFile.Visible = false;
            }

        }
    }
}
