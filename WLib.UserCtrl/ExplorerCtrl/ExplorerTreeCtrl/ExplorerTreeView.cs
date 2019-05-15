/*---------------------------------------------------------------- 
// auth： Source by WilsonProgramming
// date： 2019/3
// desc： None
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System.Collections;
using System.IO;
using System.Windows.Forms;
using WLib.Files;

namespace WLib.UserCtrls.ExplorerCtrl.ExplorerTreeCtrl
{
    /// <summary>
    /// 目录树控件
    /// </summary>
    public partial class ExplorerTreeView : UserControl
    {
        /// <summary>
        /// 当前重命名的文件夹节点
        /// </summary>
        private TreeNode _currentNode;
        /// <summary>
        /// 
        /// </summary>
        internal ShellItem SelectShellItem => (ShellItem)TreeViewWnd.SelectedNode?.Tag;

        /// <summary>
        /// 返回控件中选中的目录的路径
        /// </summary>
        public string SelectedPath { get => ((ShellItem)TreeViewWnd.SelectedNode?.Tag)?.Path; set => TreeViewWnd.SelectedNode = ExpandPath(value); }
        /// <summary>
        /// 是否允许重命名文件夹
        /// </summary>
        public bool CanRename { get; set; }


        /// <summary>
        /// 目录树控件
        /// </summary>
        public ExplorerTreeView()
        {
            InitializeComponent();

            SystemImageList.SetTVImageList(TreeViewWnd.Handle);
            LoadRootNodes();
            TreeViewWnd.MouseDown -= TreeViewWnd_MouseDown;
            TreeViewWnd.MouseDown += TreeViewWnd_MouseDown;
            TreeViewWnd.DoubleClick -= TreeViewWnd_DoubleClick;
            TreeViewWnd.DoubleClick += TreeViewWnd_DoubleClick;
            TreeViewWnd.AfterLabelEdit -= TreeWnd_AfterLabelEdit;
            TreeViewWnd.AfterLabelEdit += TreeWnd_AfterLabelEdit;
        }
        /// <summary>
        /// 展开至指定目录
        /// </summary>
        /// <param name="path"></param>
        private TreeNode ExpandPath(string path)
        {
            string rootPath = Path.GetPathRoot(path);
            if (rootPath != null)
            {
                string disk = rootPath.Substring(0, 1);
                TreeNode desktopNode = TreeViewWnd.Nodes[0];
                TreeNode computerNode = desktopNode.Nodes[0];
                computerNode.Expand();

                foreach (TreeNode node in computerNode.Nodes)
                {
                    ShellItem shellItem = (ShellItem)node.Tag;
                    if (shellItem.DisplayName.Contains(disk))
                    {
                        node.Expand();
                        return GetNodeByName(node, path, rootPath);
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 递归展开至指定目录对应的结点(TreeNode)
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="path"></param>
        /// <param name="rootPath"></param>
        private TreeNode GetNodeByName(TreeNode treeNode, string path, string rootPath)
        {
            string subPath = path.Replace(rootPath, "");
            string folderName = subPath.Split(Path.DirectorySeparatorChar)[0];
            foreach (TreeNode node in treeNode.Nodes)
            {
                if (((ShellItem)node.Tag).DisplayName == folderName)
                {
                    node.Expand();
                    return GetNodeByName(node, subPath, folderName + Path.DirectorySeparatorChar);
                }
            }
            return treeNode;
        }
        /// <summary>
        /// 刷新控件，重新整个目录结构（加载根节点 Loads the root TreeView nodes）
        /// </summary>
        private void LoadRootNodes()
        {
            ShellItem desktopItem = new ShellItem();

            TreeNode tvwRoot = new TreeNode
            {
                Text = desktopItem.DisplayName,
                ImageIndex = desktopItem.IconIndex,
                SelectedImageIndex = desktopItem.IconIndex,
                Tag = desktopItem
            };
            ArrayList arrChildren = desktopItem.GetSubFolders();
            foreach (ShellItem shChild in arrChildren)
            {
                TreeNode tvwChild = new TreeNode
                {
                    Text = shChild.DisplayName,
                    ImageIndex = shChild.IconIndex,
                    SelectedImageIndex = shChild.IconIndex,
                    Tag = shChild
                };
                if (shChild.IsFolder && shChild.HasSubFolder)
                    tvwChild.Nodes.Add("PH");
                tvwRoot.Nodes.Add(tvwChild);
            }

            TreeViewWnd.Nodes.Clear();
            TreeViewWnd.Nodes.Add(tvwRoot);
            tvwRoot.Expand();
        }


        private void TreeViewWnd_MouseDown(object sender, MouseEventArgs e)
        {
            _currentNode = TreeViewWnd.GetNodeAt(e.X, e.Y);
        }

        private void TreeViewWnd_DoubleClick(object sender, System.EventArgs e)
        {
            if (_currentNode?.Parent != null && CanRename)
            {
                TreeViewWnd.SelectedNode = _currentNode;
                TreeViewWnd.LabelEdit = true;
                if (!_currentNode.IsEditing)
                    _currentNode.BeginEdit();
            }
        }

        private void TreeWnd_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)//重命名文件夹之后
        {
            if (e.Label != null && CanRename)
            {
                if (!string.IsNullOrWhiteSpace(e.Label))
                {
                    if (e.Label.IndexOfAny(Path.GetInvalidPathChars()) == -1)
                    {
                        e.Node.EndEdit(false);
                        var shellItem = (ShellItem)e.Node.Tag;
                        if (shellItem.IsFolder)
                        {
                            var dir = Path.GetDirectoryName(shellItem.Path);
                            FileOpt.ReNameFolder(shellItem.Path, e.Label);
                            shellItem.DisplayName = e.Label;
                            shellItem.Path = Path.Combine(dir, e.Label);
                            e.Node.Text = e.Label;
                            e.Node.Collapse();
                            SelectedPath = shellItem.Path;
                        }
                    }
                    else
                    {

                        e.CancelEdit = true;
                        MessageBox.Show(@"文件夹名称不允许包含以下字符串：" + new string(Path.GetInvalidPathChars()), @"重命名文件夹");
                        e.Node.BeginEdit();
                    }
                }
                else
                {
                    e.CancelEdit = true;
                    MessageBox.Show(@"文件夹名称不能为空或空格", @"重命名文件夹");
                    e.Node.BeginEdit();
                }
            }
        }
    }
}
