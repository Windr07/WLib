using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using WLib.UserCtrls.PathCtrl;

namespace WLib.UserCtrls.Dev.PathControl
{
    /// <summary>
    /// 表示一个路径选择与显示的组合框
    /// </summary>
    public partial class PathBoxControl : DevExpress.XtraEditors.XtraUserControl
    {
        private bool _canTextEdit = true;
        private EShowButtonOption _showButtonOption;
        private string _fileFilter;
        private bool _optEnable = true;
        private bool _multiSelect = true;
        private string DEFAUL_TTIPS = "粘贴路径于此并按下回车，或点击选择按钮";
        private string _defultTips;

        /// <summary>
        /// 在路径选择框中显示的默认提示
        /// </summary>
        public string DefaultTips
        {
            get => this._defultTips;
            set
            {
                if (string.IsNullOrEmpty(this.cmbPath.Text.Trim()) || this.cmbPath.Text == this._defultTips)
                    this.cmbPath.Text = value;
                this._defultTips = value;
                if (!_canTextEdit)
                    this.cmbPath.ForeColor = Color.Gray;
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
                switch (value)
                {
                    case EShowButtonOption.ViewSelect:
                    case EShowButtonOption.Select:
                        this.cmbPath.Width = this.Width - 77;
                        this.btnSelect.Visible = true;
                        this.btnSelect.Location = new Point(this.Width - 75, 0);
                        this.btnView.Location = new Point(this.Width - 113, 2);
                        this.btnSave.Visible = false;
                        break;
                    case EShowButtonOption.ViewSave:
                    case EShowButtonOption.Opt:
                        this.cmbPath.Width = this.Width - 77;
                        this.btnSave.Visible = true;
                        this.btnSave.Location = new Point(this.Width - 75, 0);
                        this.btnView.Location = new Point(this.Width - 113, 2);
                        this.btnSelect.Visible = false;
                        break;
                    case EShowButtonOption.All:
                        this.cmbPath.Width = this.Width - 153;
                        this.btnSelect.Visible = this.btnSave.Visible = true;
                        this.btnSelect.Location = new Point(this.Width - 151, 0);
                        this.btnView.Location = new Point(this.Width - 189, 2);
                        this.btnSave.Location = new Point(this.Width - 75, 0);
                        break;
                    case EShowButtonOption.None:
                    case EShowButtonOption.View:
                        this.cmbPath.Width = this.Width - 1;
                        this.btnSelect.Visible = this.btnSave.Visible = false;
                        this.btnView.Location = new Point(this.Width - 38, 2);
                        break;
                }

                if (value == EShowButtonOption.View || value == EShowButtonOption.ViewSelect ||
                    value == EShowButtonOption.ViewSave || value == EShowButtonOption.All)
                    this.btnView.Visible = true;
                else
                    this.btnView.Visible = false;

                this.OnResize(new EventArgs());
            }

        }
        /// <summary>
        /// 是否允许提供多个文件路径
        /// </summary>
        public bool MultiSelect
        {
            get => this._multiSelect;
            set
            {
                if (!value && this.cmbPath.Properties.Items.Count > 1)
                {
                    var item = (string)this.cmbPath.Properties.Items[0];
                    this.cmbPath.Properties.Items.Clear();
                    this.Text = item;
                }
                this._multiSelect = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public String FileFilter
        {
            get => this._fileFilter;
            set
            {
                if (value != null && !value.Contains("|"))
                    this._fileFilter = value + "|" + value;
                else
                    this._fileFilter = value;
            }
        }
        /// <summary>
        /// 选择文件或目录的标题或提示信息
        /// </summary>
        public string SelectTips { get; set; }
        /// <summary>
        /// 是否允许编辑路径
        /// </summary>
        public bool CanTextEdit
        {
            get => this._canTextEdit;
            set
            {
                _canTextEdit = value;
                if (_canTextEdit)
                {
                    this.cmbPath.Properties.TextEditStyle = TextEditStyles.Standard;
                }
                else
                {
                    this.cmbPath.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                }
                //this.btnView.Parent = this.PathComboBoxEdit;
            }
        }
        /// <summary>
        /// 显示的路径
        /// </summary>
        public string Path
        {
            get => this.cmbPath.EditValue?.ToString();
            set => this.cmbPath.EditValue = value;
        }
        /// <summary>
        /// 显示的路径
        /// </summary>
        public override string Text
        {
            get => this.cmbPath.EditValue?.ToString();
            set => this.cmbPath.EditValue = value;
        }
        /// <summary>
        /// 获取或设置路径框中的所有路径
        /// </summary>
        public string[] Paths
        {
            get => this.cmbPath.Properties.Items.Cast<string>().ToArray();
            set
            {
                this.cmbPath.Properties.Items.Clear();

                this.cmbPath.Properties.Items.AddRange(value);
                if (this.cmbPath.Properties.Items.Count > 0)
                    this.cmbPath.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 标示除浏览按钮外，其余操作是否可用
        /// </summary>
        public bool OptEnable
        {
            get => this._optEnable;
            set
            {
                this._optEnable = value;
                this.cmbPath.Enabled = value;
                this.btnSelect.Enabled = value;
                this.btnSave.Enabled = value;
            }
        }
        /// <summary>
        /// 路径下拉列表
        /// </summary>
        public ComboBoxEdit PathComboBoxEdit => this.cmbPath;

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
                        return System.IO.Directory.Exists(this.Text);
                    case ESelectPathType.OpenFile:
                        return System.IO.File.Exists(this.Text);
                    case ESelectPathType.SaveFile:
                        return System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(this.Text)) && ValidFileName(System.IO.Path.GetFileNameWithoutExtension(this.Text));
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// 点击保存按钮事件
        /// </summary>
        public event EventHandler SaveButtonClick;
        /// <summary>
        /// 选择路径后的事件
        /// </summary>
        public event EventHandler AfeterSelectPath;

        /// <summary>
        /// 表示一个路径选择与显示的组合框
        /// </summary>
        public PathBoxControl()
        {
            InitializeComponent();

            SaveButtonClick = new EventHandler((object sender, EventArgs e) => { });
            AfeterSelectPath = new EventHandler((object sender, EventArgs e) => { });
            //this.btnView.Parent = this.PathComboBoxEdit;
            this.cmbPath.GotFocus += cmbPath_GotFocus;
            this.cmbPath.LostFocus += cmbPath_LostFocus;
            DefaultTips = DEFAUL_TTIPS;
        }




        /// <summary>
        /// 清理路径
        /// </summary>
        public void Clear()
        {
            this.cmbPath.Properties.Items.Clear();
        }
        /// <summary>
        /// 向路径框中添加路径
        /// </summary>
        /// <param name="path"></param>
        public void Add(string path)
        {
            this.cmbPath.Properties.Items.Add(path);
        }
        /// <summary>
        /// 向路径框中添加多个路径
        /// </summary>
        /// <param name="paths"></param>
        public void AddRange(string[] paths)
        {
            this.cmbPath.Properties.Items.AddRange(paths);
        }
        /// <summary>
        /// 向路径框中添加路径，并触发AfeterSelectPath事件
        /// </summary>
        public void SelectPath(string path)
        {
            this.Text = path;
            AfeterSelectPath(this, new EventArgs());
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


        private void btnView_Click(object sender, EventArgs e)
        {
            string path = this.cmbPath.Text.Trim();
            if (System.IO.Directory.Exists(path))
            {
                System.Diagnostics.Process.Start("explorer.exe", path);
            }
            else if (System.IO.File.Exists(path))
            {
                System.Diagnostics.Process.Start("explorer.exe", "/select," + path);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            string path = this.cmbPath.Text.Trim();
            if (SelectPathType == ESelectPathType.Folder)
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                dlg.Description = SelectTips;
                dlg.ShowNewFolderButton = true;

                if (System.IO.Directory.Exists(path))
                    dlg.SelectedPath = path;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (!MultiSelect)
                        this.cmbPath.Properties.Items.Clear();
                    this.Text = dlg.SelectedPath;
                    AfeterSelectPath(this, new EventArgs());
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
                dlg.Multiselect = MultiSelect;

                if (!System.IO.Path.IsPathRooted(path))
                    path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

                if (System.IO.Directory.Exists(path))
                    dlg.InitialDirectory = path;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (MultiSelect == true)
                    {
                        this.cmbPath.Properties.Items.AddRange(dlg.FileNames);
                        this.cmbPath.SelectedItem = dlg.FileNames[0];
                    }
                    else
                    {
                        this.cmbPath.Properties.Items.Clear();
                        this.cmbPath.Properties.Items.Add(dlg.FileName);
                        this.cmbPath.SelectedIndex = 0;
                    }
                    AfeterSelectPath(this, new EventArgs());
                }

                if (this.cmbPath.Text != string.Empty && this.cmbPath.Text != _defultTips)
                    this.cmbPath.ForeColor = SystemColors.WindowText;
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
                    if (!MultiSelect)
                        this.cmbPath.Properties.Items.Clear();
                    this.cmbPath.Properties.Items.Add(dlg.FileName);
                    AfeterSelectPath(this, new EventArgs());
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveButtonClick(sender, e);
        }

        private void cmbPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                AfeterSelectPath(this, new EventArgs());
        }

        private void cmbPath_Click(object sender, EventArgs e)
        {
            if (!this._canTextEdit && this.Text == _defultTips)
                this.cmbPath.Text = string.Empty;
        }

        private void cmbPath_GotFocus(object sender, EventArgs e)
        {
            if (this.cmbPath.Text == _defultTips)
                this.cmbPath.Text = string.Empty;

            this.cmbPath.ForeColor = System.Drawing.SystemColors.WindowText;
        }

        private void cmbPath_LostFocus(object sender, EventArgs e)
        {
            if (this.cmbPath.Text == string.Empty)
            {
                this.cmbPath.ForeColor = Color.Gray;
                this.cmbPath.Text = _defultTips;
            }
        }

        private void btnView_MouseEnter(object sender, EventArgs e)
        {
            this.btnView.BackColor = SystemColors.ControlDark;
        }

        private void btnView_MouseLeave(object sender, EventArgs e)
        {
            this.btnView.BackColor = Color.Transparent;
        }
    }
}
