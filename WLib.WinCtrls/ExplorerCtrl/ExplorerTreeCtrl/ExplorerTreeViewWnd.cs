/*---------------------------------------------------------------- 
// auth： Source by WilsonProgramming
// date： None
// desc： 扩展TreeView,用于目录树控件，用于方便加入窗体中显示，而不必使用FolderBrowserDialog弹框显示
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System.Collections;
using System.Windows.Forms;

namespace WLib.WinCtrls.ExplorerCtrl.ExplorerTreeCtrl
{
    /// <summary>
    /// 对TreeView进行扩展的目录树视图控件
    /// </summary>
    public class ExplorerTreeViewWnd : TreeView
    {
        /// <summary>
        /// 对TreeView进行扩展的目录树视图控件
        /// </summary>
        public ExplorerTreeViewWnd() { }

        /// <summary>
        /// 展开某个文件夹节点前，获取子文件夹信息作为子节点
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            // Remove the placeholder node.
            // 删除占位符节点
            e.Node.Nodes.Clear();

            // We stored the ShellItem object in the node's Tag property - hah!
            // ShellItem对象存储在节点的Tag属性中
            ShellItem shellItem = (ShellItem)e.Node.Tag;
            ArrayList subShellItems = shellItem.GetSubFolders();
            foreach (ShellItem subShellItem in subShellItems)
            {
                var treeNode = new TreeNode
                {
                    Text = subShellItem.DisplayName,
                    ImageIndex = subShellItem.IconIndex,
                    SelectedImageIndex = subShellItem.IconIndex,
                    Tag = subShellItem
                };

                // If this is a folder item and has children then add a place holder node.
                // 若这是一个文件夹项并且有子项，则添加一个占位符节点
                if (subShellItem.IsFolder && subShellItem.HasSubFolder)
                    treeNode.Nodes.Add("PH");
                e.Node.Nodes.Add(treeNode);
            }

            base.OnBeforeExpand(e);
        }
    }
}
