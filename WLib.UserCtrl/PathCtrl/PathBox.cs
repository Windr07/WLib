/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WLib.UserCtrls.PathCtrl
{
    /// <summary>
    /// 表示一个路径选择与显示的组合框
    /// </summary>
    public partial class PathBox : UserControl
    {
        private bool _readOnly;
        private bool _optEnable = true;
        private bool _multiSelect = true;
        private string _fileFilter;
        private string _defultTips;
        private string DEFAUL_TTIPS = "粘贴路径于此并按下回车，或点击选择按钮";
        private EShowButtonOption _showButtonOption;

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
                if (!_readOnly)
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
                switch (value)
                {
                    case EShowButtonOption.ViewSelect:
                        panelPath.Width = Width - 89;
                        btnSelect.Visible = btnView.Visible = true;
                        btnOperate.Visible = false;
                        break;
                    case EShowButtonOption.ViewSave:
                        panelPath.Width = Width - 89;
                        btnOperate.Visible = btnView.Visible = true;
                        btnSelect.Visible = false;
                        break;
                    case EShowButtonOption.All:
                        panelPath.Width = Width - 151;
                        btnSelect.Visible = btnView.Visible = btnOperate.Visible = true;
                        break;
                    case EShowButtonOption.None:
                        panelPath.Width = Width - 1;
                        btnSelect.Visible = btnView.Visible = btnOperate.Visible = false;
                        break;
                    case EShowButtonOption.Select:
                        panelPath.Width = Width - 62;
                        btnSelect.Visible = true;
                        btnOperate.Visible = btnView.Visible = false;
                        break;
                    case EShowButtonOption.View:
                        panelPath.Width = Width - 29;
                        btnView.Visible = true;
                        btnOperate.Visible = btnSelect.Visible = false;
                        break;
                    case EShowButtonOption.Opt:
                        panelPath.Width = Width - 62;
                        btnOperate.Visible = true;
                        btnView.Visible = btnSelect.Visible = false;
                        break;
                }
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
                if (value && cmbPath.Items.Count > 1)
                {
                    var item = (string)cmbPath.Items[0];
                    cmbPath.Items.Clear();
                    Text = item;
                }
                _multiSelect = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public String FileFilter
        {
            get => _fileFilter;
            set
            {
                if (value != null && !value.Contains("|"))
                    _fileFilter = value + "|" + value;
                else
                    _fileFilter = value;
            }
        }
        /// <summary>
        /// 选择文件或目录的标题或提示信息
        /// </summary>
        public string SelectTips { get; set; }
        /// <summary>
        /// 所选的路径在列表中的索引
        /// </summary>
        public int SelectIndex { get => cmbPath.SelectedIndex; set => cmbPath.SelectedIndex = value; }
        /// <summary>
        /// 路径文本框只读
        /// </summary>
        public bool ReadOnly
        {
            get => _readOnly;
            set
            {
                _readOnly = value;
                cmbPath.DropDownStyle = _readOnly ? ComboBoxStyle.DropDownList : ComboBoxStyle.DropDown;
            }
        }
        /// <summary>
        /// 显示的路径
        /// </summary>
        public override string Text
        {
            get
            {
                switch (cmbPath.DropDownStyle)
                {
                    case ComboBoxStyle.DropDown: return cmbPath.Text;
                    case ComboBoxStyle.DropDownList: return (string)cmbPath.SelectedItem;
                    default: return cmbPath.Text;
                }
            }
            set
            {
                if (!_multiSelect)
                    cmbPath.Items.Clear();
                if (!_readOnly)
                    cmbPath.ForeColor = SystemColors.WindowText;
                cmbPath.Items.Add(value);
                cmbPath.SelectedItem = value;
            }
        }
        /// <summary>
        /// 获取或设置路径框中的所有路径
        /// </summary>
        public string[] Paths
        {
            get => cmbPath.Items.Cast<string>().ToArray();
            set
            {
                cmbPath.Items.Clear();
                if (!_readOnly)
                    cmbPath.ForeColor = SystemColors.WindowText;
                cmbPath.Items.AddRange(value);
                if (cmbPath.Items.Count > 0)
                    cmbPath.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 标示除浏览按钮外，其余操作是否可用
        /// </summary>
        public bool OptEnable
        {
            get => _optEnable;
            set
            {
                _optEnable = value;
                cmbPath.Enabled = value;
                btnSelect.Enabled = value;
                btnOperate.Enabled = value;
            }
        }
        /// <summary>
        /// 选择按钮是否可用
        /// </summary>
        public bool SelectEnable
        {
            get => btnSelect.Enabled;
            set => btnSelect.Enabled = value;
        }
        /// <summary>
        /// 路径下拉列表
        /// </summary>
        public ComboBox PathComboBox => cmbPath;


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
                        return Directory.Exists(Text);
                    case ESelectPathType.OpenFile:
                        return File.Exists(Text);
                    case ESelectPathType.SaveFile:
                        return Directory.Exists(Path.GetDirectoryName(Text)) && ValidFileName(Path.GetFileNameWithoutExtension(Text));
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// 选择按钮上显示的文本
        /// </summary>
        public string SelectButtonText { get => btnSelect.Text; set => btnSelect.Text = value; }
        /// <summary>
        /// 操作按钮上显示的文本
        /// </summary>
        public string OperateButtonText { get => btnOperate.Text; set => btnOperate.Text = value; }


        /// <summary>
        /// 点击保存按钮事件
        /// </summary>
        public event EventHandler OperateButtonClick;
        /// <summary>
        /// 选择路径后的事件
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


        /// <summary>
        /// 表示一个路径选择与显示的组合框
        /// </summary>
        public PathBox()
        {
            InitializeComponent();
            cmbPath.GotFocus += cmbPath_GotFocus;
            DefaultTips = DEFAUL_TTIPS;
        }
        /// <summary>
        /// 清理路径
        /// </summary>
        public void Clear()
        {
            cmbPath.Items.Clear();
        }
        /// <summary>
        /// 向路径框中添加路径，并触发AfeterSelectPath事件
        /// </summary>
        public void SelectPath(string path)
        {
            Text = path;
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
        /// <summary>
        /// 向路径框中添加路径
        /// </summary>
        /// <param name="path"></param>
        public void Add(string path)
        {
            cmbPath.Items.Add(path);
        }
        /// <summary>
        /// 向路径框中添加多个路径
        /// </summary>
        /// <param name="paths"></param>
        public void AddRange(string[] paths)
        {
            cmbPath.Items.AddRange(paths);
        }


        private void btnView_Click(object sender, EventArgs e)
        {
            var path = cmbPath.Text.Trim();
            if (Directory.Exists(path))
                Process.Start("explorer.exe", path);
            else if (File.Exists(path))
                Process.Start("explorer.exe", "/select," + path);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            string path = cmbPath.Text.Trim();
            if (SelectPathType == ESelectPathType.Folder)
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                dlg.Description = SelectTips;
                dlg.ShowNewFolderButton = true;

                if (Directory.Exists(path))
                    dlg.SelectedPath = path;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (!MultiSelect)
                        cmbPath.Items.Clear();
                    Text = dlg.SelectedPath;
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
                dlg.Multiselect = MultiSelect;

                if (!Path.IsPathRooted(path))
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

                if (Directory.Exists(path))
                    dlg.InitialDirectory = path;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (MultiSelect == true)
                    {
                        cmbPath.Items.AddRange(dlg.FileNames);
                        cmbPath.SelectedItem = dlg.FileNames[0];
                    }
                    else
                    {
                        cmbPath.Items.Clear();
                        cmbPath.Items.Add(dlg.FileName);
                        cmbPath.SelectedIndex = 0;
                    }
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
                if (Directory.Exists(path))
                    dlg.InitialDirectory = path;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (!MultiSelect)
                        cmbPath.Items.Clear();
                    cmbPath.Items.Add(dlg.FileName);
                    OnAfeterSelectPath();
                }
            }
            else if (SelectPathType == ESelectPathType.Customize)
            {
                OnCustomizeSelectPath();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OnOperateButtonClick();
        }

        private void cmbPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                OnAfeterSelectPath();
        }

        private void cmbPath_Click(object sender, EventArgs e)
        {
            if (!ReadOnly && Text == _defultTips)
                Text = "";
        }

        private void cmbPath_GotFocus(object sender, EventArgs e)
        {
            if (cmbPath.Text == _defultTips)
            {
                cmbPath.Text = "";
                cmbPath.ForeColor = SystemColors.WindowText;
            }
        }

        private void PathBox_Resize(object sender, EventArgs e)
        {
            if (cmbPath.Text == _defultTips && !_readOnly)
                cmbPath.ForeColor = Color.Gray;
        }
    }
}
