/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/09/02
// desc： 显示目录/文件结构的TreeView
// mdfy:  None
//----------------------------------------------------------------*/

using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WLib.WinCtrls.ExplorerCtrl
{
    /// <summary>
    /// 显示目录/文件结构的<see cref="TreeView"/>
    /// </summary>
    public partial class FileTreeView : UserControl
    {
        /// <summary>
        /// TreeView中显示的目录路径
        /// </summary>
        private string _path;
        /// <summary>
        /// TreeView中显示的目录路径
        /// </summary>
        public string Path { get => _path; set => LoadPath(treeView1, _path = value); }
        /// <summary>
        /// 显示目录/文件结构的<see cref="TreeView"/>
        /// </summary>
        public TreeView DirTreeView => treeView1;

        /// <summary>
        /// 是否显示目录中的文件
        /// <para>默认True</para>
        /// </summary>
        [DefaultValue(true)]
        public bool ShowFiles { get; set; } = true;

        /// <summary>
        /// 在目录显示文件时的文件过滤条件
        /// <para>默认"*.*"，即所有文件</para>
        /// </summary>
        [DefaultValue("*.*")]
        public string FileFilter { get; set; } = "*.*";


        /// <summary>
        /// 显示目录/文件结构的<see cref="TreeView"/>
        /// </summary>
        public FileTreeView() => InitializeComponent();
        /// <summary>
        /// 显示目录/文件结构的<see cref="TreeView"/>
        /// </summary>
        /// <param name="path">TreeView中显示的目录路径</param>
        public FileTreeView(string path) : this() => Path = path;
        /// <summary>
        /// 显示目录/文件结构的<see cref="TreeView"/>
        /// </summary>
        /// <param name="path">TreeView中显示的目录路径</param>
        /// <param name="showFiles">是否显示目录中的文件</param>
        public FileTreeView(string path, bool showFiles) : this(path) => ShowFiles = showFiles;
        /// <summary>
        /// 显示目录/文件结构的<see cref="TreeView"/>
        /// </summary>
        /// <param name="path">TreeView中显示的目录路径</param>
        /// <param name="fileFilter">在目录显示文件时的文件过滤条件</param>
        public FileTreeView(string path, string fileFilter) : this(path) => FileFilter = fileFilter;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="path"></param>
        private void LoadPath(TreeView treeView, string path)
        {
            for (int i = 3; i < imageList1.Images.Count; i++)
                imageList1.Images.RemoveAt(i);

            treeView.Nodes.Clear();
            if (!Directory.Exists(path))
                return;

            var currentDir = new DirectoryInfo(path);
            var node = new TreeNode { Text = currentDir.Name, Tag = currentDir.FullName, ImageIndex = 0, SelectedImageIndex = 1 };
            treeView.Nodes.Add(node);
            LoadPath(node, currentDir);
            treeView.Nodes[0].Expand();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="currentDir"></param>
        private void LoadPath(TreeNode node, DirectoryInfo currentDir)
        {
            var dirs = currentDir.GetDirectories();//获取目录
            foreach (var dir in dirs)
                node.Nodes.Add(new TreeNode { Text = dir.Name, Tag = dir.FullName, ImageIndex = 0, SelectedImageIndex = 1 });

            if (!ShowFiles)
                return;
            var files = currentDir.GetFiles(FileFilter);//获取文件
            foreach (var file in files)
            {
                Icon fileIcon = GetSystemIcon.GetIconByFileName(file.FullName, out var hicon);
                string imageKey = file.Name;
                if (imageList1.Images.ContainsKey(hicon.ToString()))
                    imageKey = hicon.ToString();
                else
                    imageList1.Images.Add(file.Name, fileIcon);
                node.Nodes.Add(new TreeNode { Text = file.Name, Tag = file.FullName, ImageKey = imageKey, SelectedImageKey = file.Name });
            }
        }


        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            foreach (TreeNode node in e.Node.Nodes) //更新所有子结点
            {
                if (node.Nodes.Count > 0)
                    continue;
                var path = node.Tag.ToString();
                if (Directory.Exists(path))
                    LoadPath(node, new DirectoryInfo(path));
            }
        }

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) => e.Node.Expand();
    }
}
