using System.Collections;
using System.IO;
using System.Windows.Forms;

//来源(Source by): WilsonProgramming
//Modify: Windragon
namespace WLib.UserCtrls.ExplorerCtrl  
{
    /// <summary>
    /// 目录树控件
    /// </summary>
    public partial class ExplorerTreeView : UserControl
    {
        /// <summary>
        /// 对TreeView进行扩展的目录树视图控件
        /// </summary>
        public ExplorerTreeViewWnd Tree => this.treeWnd;
        /// <summary>
        /// 
        /// </summary>
        public ShellItem SelectShellItem => (ShellItem)this.treeWnd.SelectedNode?.Tag;
        /// <summary>
        /// 返回控件中选中的目录的路径
        /// </summary>
        public string SelectPath => ((ShellItem)this.treeWnd.SelectedNode?.Tag)?.Path;


        /// <summary>
        /// 刷新控件，重新加载目录结构
        /// </summary>
        public void RefreshTree()
        {
            LoadRootNodes();
        }
        /// <summary>
        /// 展开至指定目录
        /// </summary>
        /// <param name="path"></param>
        public void ExpandPath(string path)
        {
            string rootPath = System.IO.Path.GetPathRoot(path);
            if (rootPath != null)
            {
                string disk = rootPath.Substring(0, 1);
                TreeNode desktopNode = this.treeWnd.Nodes[0];
                TreeNode computerNode = desktopNode.Nodes[0];
                computerNode.Expand();

                foreach (TreeNode node in computerNode.Nodes)
                {
                    ShellItem shellItem = (ShellItem)node.Tag;
                    if (shellItem.DisplayName.Contains(disk))
                    {
                        node.Expand();
                        GetNodeByName(node, path, rootPath);
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 递归展开至指定目录对应的结点(TreeNode)
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="path"></param>
        /// <param name="rootPath"></param>
        private void GetNodeByName(TreeNode treeNode, string path, string rootPath)
        {
            string subPath = path.Replace(rootPath, "");
            string folderName = subPath.Split(Path.DirectorySeparatorChar)[0];
            foreach (TreeNode node in treeNode.Nodes)
            {
                if (((ShellItem)node.Tag).DisplayName == folderName)
                {
                    node.Expand();
                    GetNodeByName(node, subPath, folderName + Path.DirectorySeparatorChar);
                    break;
                }
            }
        }
        /// <summary>
        /// 目录树控件
        /// </summary>
        public ExplorerTreeView()
        {
            InitializeComponent();

            SystemImageList.SetTVImageList(treeWnd.Handle);
            LoadRootNodes();
        }
        /// <summary>
        /// Loads the root TreeView nodes.
        /// </summary>
        private void LoadRootNodes()
        {
            ShellItem desktopItem = new ShellItem();

            TreeNode tvwRoot = new TreeNode();
            tvwRoot.Text = desktopItem.DisplayName;
            tvwRoot.ImageIndex = desktopItem.IconIndex;
            tvwRoot.SelectedImageIndex = desktopItem.IconIndex;
            tvwRoot.Tag = desktopItem;

            ArrayList arrChildren = desktopItem.GetSubFolders();
            foreach (ShellItem shChild in arrChildren)
            {
                TreeNode tvwChild = new TreeNode();
                tvwChild.Text = shChild.DisplayName;
                tvwChild.ImageIndex = shChild.IconIndex;
                tvwChild.SelectedImageIndex = shChild.IconIndex;
                tvwChild.Tag = shChild;

                if (shChild.IsFolder && shChild.HasSubFolder)
                    tvwChild.Nodes.Add("PH");
                tvwRoot.Nodes.Add(tvwChild);
            }

            treeWnd.Nodes.Clear();
            treeWnd.Nodes.Add(tvwRoot);
            tvwRoot.Expand();
        }
    }
}
