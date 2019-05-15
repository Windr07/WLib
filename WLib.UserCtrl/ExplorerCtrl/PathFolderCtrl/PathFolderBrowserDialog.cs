/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/18
// desc： .NET WinForm本身的FolderBrowserDialog选择目录比较麻烦，
          此控件目的是在原控件功能中加上路径输入文本框以快速定位文件夹
// mdfy:  None
//----------------------------------------------------------------*/

using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using WLib.Files;
using WLib.UserCtrls.ExplorerCtrl.ExplorerTreeCtrl;

namespace WLib.UserCtrls.ExplorerCtrl.PathFolderCtrl
{
    /// <summary>
    /// 与<see cref="FolderBrowserDialog"/>功能相近的文件夹选择对话框，添加可以通过粘贴路径以定位和选择目录的功能
    /// </summary>
    [DefaultProperty("SelectedPath")]
    public partial class PathFolderBrowserDialog : Form
    {
        /// <summary>
        /// 是否显示“创建新文件夹”按钮
        /// </summary>
        [DefaultValue(true)]
        public bool ShowNewFolderButton
        {
            get => this.btnNewFolder.Visible;
            set
            {
                this.btnNewFolder.Visible = value;
                this.folderBrowser1.CanRename = value;
            }
        }

        /// <summary>
        /// 获取或设置在对话框中树视图控件的上方显示的说明性文本
        /// </summary>
        public string Description { get => this.lblDescription.Text; set => this.lblDescription.Text = value; }
        /// <summary>
        /// 获取或设置用户选定的路径
        /// </summary>
        public string SelectedPath { get => this.folderBrowser1.SelectedPath; set => this.folderBrowser1.SelectedPath = value; }


        /// <summary>
        /// 与<see cref="FolderBrowserDialog"/>功能相近的文件夹选择控件，添加可以通过粘贴路径以定位和选择目录的功能
        /// </summary>
        public PathFolderBrowserDialog()
        {
            InitializeComponent();
            this.folderBrowser1.TreeViewWnd.AfterSelect += (sender, e) =>
                this.btnNewFolder.Enabled = this.folderBrowser1.SelectedPath != null;
        }
        /// <summary>
        /// 与<see cref="FolderBrowserDialog"/>功能相近的文件夹选择控件，添加可以通过粘贴路径以定位和选择目录的功能
        /// </summary>
        public PathFolderBrowserDialog(string selectPath, string description = null, bool showNewFolderButton = true) : this()
        {
            this.SelectedPath = selectPath;
            this.Description = description;
            this.ShowNewFolderButton = showNewFolderButton;
        }


        private void btnNewFolder_Click(object sender, System.EventArgs e)
        {
            var selectedNode = this.folderBrowser1.TreeViewWnd.SelectedNode;
            if (selectedNode == null)
                return;

            var path = ((ShellItem)selectedNode.Tag).GetPath();
            if (string.IsNullOrWhiteSpace(path))
                return;

            var newPath = FileOpt.CombineReNameFolder(path, "新建文件夹", false);
            Directory.CreateDirectory(newPath);
            selectedNode.Collapse();
            this.folderBrowser1.SelectedPath = newPath;
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SelectedPath))
            {
                var name = this.folderBrowser1.explorerTreeView1.SelectShellItem.DisplayName;
                MessageBox.Show($@"“{name}”不是文件目录，请选择具体文件目录！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
