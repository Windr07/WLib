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
using WLib.UserCtrls.ExplorerCtrl.ExplorerTreeCtrl;

namespace WLib.UserCtrls.ExplorerCtrl.PathFolderCtrl
{
    /// <summary>
    /// 与<see cref="FolderBrowserDialog"/>功能相近的文件夹选择控件，添加可以通过粘贴路径以定位和选择目录的功能
    /// </summary>
    public partial class PathFolderBrowser : UserControl
    {
        /// <summary>
        /// 是否允许重命名文件夹
        /// </summary>
        [DefaultValue(true)]
        public bool CanRename { get => this.explorerTreeView1.CanRename; set => this.explorerTreeView1.CanRename = value; }
        /// <summary>
        /// 对TreeView进行扩展的目录树视图控件
        /// </summary>
        public ExplorerTreeViewWnd TreeViewWnd => this.explorerTreeView1.TreeViewWnd;
        /// <summary>
        /// 路径文本框栏的停靠位置
        /// </summary>
        public DockStyle PathDockStyle { get => this.txtPath.Dock; set => this.txtPath.Dock = value; }
        /// <summary>
        /// 获取或设置所选的目录路径
        /// </summary>
        public string SelectedPath
        {
            get => explorerTreeView1.SelectedPath;
            set
            {
                if (Directory.Exists(value))
                {
                    this.txtPath.Text = explorerTreeView1.SelectedPath = value;
                    explorerTreeView1.TreeViewWnd.Focus();
                }
            }
        }
        /// <summary>
        /// 与<see cref="FolderBrowserDialog"/>功能相近的文件夹选择控件，添加可以通过粘贴路径以定位和选择目录的功能
        /// </summary>
        public PathFolderBrowser()
        {
            InitializeComponent();
            this.explorerTreeView1.TreeViewWnd.AfterSelect += (sender, e) => this.txtPath.Text = SelectedPath;
            this.explorerTreeView1.TreeViewWnd.AfterLabelEdit += (sender, e) => this.txtPath.Text = SelectedPath;
        }


        private void txtPath_TextChanged(object sender, System.EventArgs e)
        {
            var path = this.txtPath.Text.Trim();
            if (path != SelectedPath && Directory.Exists(path))
                SelectedPath = this.txtPath.Text.Trim();
        }
    }
}
