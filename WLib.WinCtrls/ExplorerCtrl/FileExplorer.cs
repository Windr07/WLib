/*---------------------------------------------------------------- 
// auth： https://download.csdn.net/download/ZhengZhiRen/1013102
// date： 2019/09/02
// desc： 简化版的Windows资源管理器
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WLib.Files;

namespace WLib.WinCtrls.ExplorerCtrl
{
    /// <summary>
    /// 简化版的Windows资源管理器
    /// <para>左侧为目录树，右侧查看目录中的文件夹和文件</para>
    /// </summary>
    public partial class FileExplorer : UserControl
    {
        /// <summary>
        /// 复制文件的源路径
        /// </summary>
        private List<string> _sourcePaths = new List<string>();
        /// <summary>
        /// 是否移动
        /// </summary>
        private bool _isMove = false;


        /// <summary>
        /// 当前路径
        /// </summary>
        public string CurrentPath { get => this.cmbPath.Text.Trim(); set => this.cmbPath.Text = value; }
        /// <summary>
        /// 是否显示顶部的路径栏
        /// </summary>
        public bool ShowAddress { get => this.tableLayoutPanel1.Visible; set => this.tableLayoutPanel1.Visible = value; }
        /// <summary>
        /// 是否显示左侧的目录树
        /// </summary>
        public bool ShowDirTree { get => !this.splitContainer1.Panel1Collapsed; set => this.splitContainer1.Panel1Collapsed = !value; }
        /// <summary>
        /// 是否显示右侧的文件内容视图
        /// </summary>
        public bool ShowFileView { get => !this.splitContainer1.Panel2Collapsed; set => this.splitContainer1.Panel2Collapsed = !value; }


        /// <summary>
        /// 简化版的Windows资源管理器
        /// <para>左侧为目录树，右侧查看目录中的文件夹和文件</para>
        /// </summary>
        public FileExplorer()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 列出磁盘
        /// </summary>
        private void ListDrivers()
        {
            treeViewDir.Nodes.Clear();
            listViewContent.Items.Clear();

            var drivers = DriveInfo.GetDrives();
            foreach (var driver in drivers)
            {
                var node = treeViewDir.Nodes.Add(driver.Name);
                var item = listViewContent.Items.Add(driver.Name);
                item.Name = driver.Name;

                //判断驱动器类型，用不同图标显示
                var imgIndex = driver.DriveType == DriveType.CDRom ? 1 : 0;
                item.ImageIndex = node.ImageIndex = node.SelectedImageIndex = imgIndex;
            }
            foreach (TreeNode node in treeViewDir.Nodes)
                NodeUpdate(node);
        }
        /// <summary>
        /// 更新结点(列出当前目录下的子目录)
        /// </summary>
        /// <param name="node"></param>
        private void NodeUpdate(TreeNode node)
        {
            try
            {
                node.Nodes.Clear();
                var dirInfos = new DirectoryInfo(node.FullPath).GetDirectories();
                foreach (var tmpDirInfo in dirInfos)
                    node.Nodes.Add(tmpDirInfo.Name);
            }
            catch { }
        }
        /// <summary>
        /// 更新列表(列出当前目录下的目录和文件)
        /// </summary>
        /// <param name="newPath"></param>
        private void ListUpdate(string newPath)
        {
            if (string.IsNullOrEmpty(newPath))
            {
                ListDrivers();
                return;
            }
            try
            {
                CurrentPath = newPath;
                ClearListViewExeImages();//删除ImageList中的exe程序图标
                listViewContent.Items.Clear();

                var currentDir = new DirectoryInfo(newPath);
                ListFolders(currentDir);
                ListFiles(currentDir);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, $"{nameof(FileExplorer)}控件错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        /// <summary>
        /// 显示目录下的子文件夹
        /// </summary>
        /// <param name="dirInfo"></param>
        private void ListFolders(DirectoryInfo dirInfo)
        {
            var dirs = dirInfo.GetDirectories();//获取目录
            foreach (DirectoryInfo dir in dirs)//列出文件夹
            {
                var listViewItem = listViewContent.Items.Add(dir.Name, 2);
                listViewItem.Name = dir.FullName;
                listViewItem.SubItems.Add("");
                listViewItem.SubItems.Add("文件夹");
                listViewItem.SubItems.Add(dir.LastWriteTimeUtc.ToString());
            }
        }
        /// <summary>
        /// 显示目录下的文件
        /// </summary>
        /// <param name="dirInfo"></param>
        private void ListFiles(DirectoryInfo dirInfo)
        {
            var files = dirInfo.GetFiles();//获取文件
            foreach (FileInfo file in files)//列出文件
            {
                ListViewItem fileItem = listViewContent.Items.Add(file.Name);
                if (file.Extension == ".exe" || file.Extension == "")   //程序文件或无扩展名
                {
                    Icon fileIcon = GetSystemIcon.GetIconByFileName(file.FullName);
                    SmallImageList.Images.Add(file.Name, fileIcon);
                    LargeImageList.Images.Add(file.Name, fileIcon);
                    fileItem.ImageKey = file.Name;
                }
                else    //其它文件
                {
                    if (!SmallImageList.Images.ContainsKey(file.Extension))  //ImageList中不存在此类图标
                    {
                        Icon fileIcon = GetSystemIcon.GetIconByFileName(file.FullName);
                        SmallImageList.Images.Add(file.Extension, fileIcon);
                        LargeImageList.Images.Add(file.Extension, fileIcon);
                    }
                    fileItem.ImageKey = file.Extension;
                }
                fileItem.Name = file.FullName;
                fileItem.SubItems.Add(file.Length.ToString() + "字节");
                fileItem.SubItems.Add(file.Extension);
                fileItem.SubItems.Add(file.LastWriteTimeUtc.ToString());
            }
        }
        /// <summary>
        /// 删除ImageList中的exe程序图标
        /// </summary>
        private void ClearListViewExeImages()
        {
            foreach (ListViewItem item in listViewContent.Items) //删除ImageList中的exe程序图标
            {
                if (item.Text.EndsWith(".exe"))  //是程序
                {
                    SmallImageList.Images.RemoveByKey(item.Text);
                    LargeImageList.Images.RemoveByKey(item.Text);
                }
            }
        }


        #region 文件、文件夹操作
        /// <summary>
        /// 打开文件夹或文件
        /// </summary>
        private void Open()
        {
            var newPath = listViewContent.SelectedItems[0].Name;
            if (Directory.Exists(newPath))
                ListUpdate(newPath);
            else
                Process.Start(newPath); //打开文件
        }
        /// <summary>
        /// 清除菜单选中
        /// </summary>
        private void ClearCheck(ToolStripMenuItem checkItem, View view)
        {
            foreach (ToolStripMenuItem item in 查看ToolStripMenuItem.DropDownItems)
                item.Checked = false;

            checkItem.Checked = true;
            listViewContent.View = view;
        }
        /// <summary>
        /// 剪切
        /// </summary>
        private void Cut()
        {
            Copy();
            _isMove = true;
        }
        /// <summary>
        /// 复制
        /// </summary>
        private void Copy()
        {
            _sourcePaths.Clear();
            foreach (ListViewItem item in listViewContent.SelectedItems)
                _sourcePaths.Add(item.Name);
            _isMove = false;
        }
        /// <summary>
        /// 复制或移动文件
        /// </summary>
        /// <param name="sourcePath"></param>
        private void CopyFile(string sourcePath)
        {
            var file = new FileInfo(sourcePath);
            var targetPath = Path.Combine(CurrentPath, file.Name);
            if (targetPath == sourcePath)
                return;
            if (_isMove)
                file.MoveTo(targetPath);
            else
                file.CopyTo(targetPath);
        }
        /// <summary>
        /// 复制或移动目录
        /// </summary>
        /// <param name="sourcePath"></param>
        private void CopyDirectory(string sourcePath)
        {
            try
            {
                var dir = new DirectoryInfo(sourcePath);
                var targetPath = Path.Combine(CurrentPath, dir.Name);
                if (targetPath == sourcePath)
                    return;
                if (_isMove)
                    dir.MoveTo(targetPath);
                else
                    DirectoryOpt.CopyDir(dir, new DirectoryInfo(targetPath), true);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, $"{nameof(FileExplorer)}控件错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        /// <summary>
        /// 粘贴
        /// </summary>
        private void Paste()
        {
            if (_sourcePaths.Count == 0 || !Directory.Exists(CurrentPath)) //无源文件则返回、当前路径无效则返回
                return;

            foreach (var path in _sourcePaths)
            {
                if (File.Exists(path)) CopyFile(path);
                else if (Directory.Exists(path)) CopyDirectory(path);
            }
            ListUpdate(CurrentPath);
            _sourcePaths.Clear();
        }
        /// <summary>
        /// 删除
        /// </summary>
        private void Delete()
        {
            if (MessageBox.Show("确定要删除选中文件夹或文件吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            try
            {
                foreach (ListViewItem item in listViewContent.SelectedItems)
                {
                    string path = item.Name;
                    if (File.Exists(path)) File.Delete(path);
                    else if (Directory.Exists(path)) Directory.Delete(path, true);
                    listViewContent.Items.Remove(item);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, $"{nameof(FileExplorer)}控件错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        /// <summary>
        /// 新建文件夹
        /// </summary>
        private void CreateFolder()
        {
            try
            {
                string path = Path.Combine(CurrentPath, "重命名");
                int i = 1;
                string newPath = path;
                while (Directory.Exists(newPath))
                {
                    newPath = path + i;
                    i++;
                }
                Directory.CreateDirectory(newPath);
                listViewContent.Items.Add(newPath, "重命名" + (i - 1 == 0 ? "" : (i - 1).ToString()), 2);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, $"{nameof(FileExplorer)}控件错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        #endregion


        private void FileExplorer_Load(object sender, EventArgs e) => ListUpdate(CurrentPath);

        private void BtnUp_Click(object sender, EventArgs e)//向上
        {
            if (string.IsNullOrEmpty(CurrentPath))
                return;
            var dirInfo = new DirectoryInfo(CurrentPath);
            if (dirInfo.Parent != null)
                ListUpdate(dirInfo.Parent.FullName);
            else
                ListDrivers();
        }

        private void treeViewDir_AfterSelect(object sender, TreeViewEventArgs e)
        {
            e.Node.Expand();
            ListUpdate(e.Node.FullPath);
        }

        private void treeViewDir_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            NodeUpdate(e.Node); //更新当前结点
            foreach (TreeNode node in e.Node.Nodes) //更新所有子结点
                NodeUpdate(node);
        }

        private void listViewContent_ItemActivate(object sender, EventArgs e) => Open();

        private void toolStripCmbPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                var newPath = this.cmbPath.Text;
                if (Directory.Exists(newPath) || File.Exists(newPath))
                    ListUpdate(newPath);
            }
        }


        #region 上下文菜单代码
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            var point = listViewContent.PointToClient(Cursor.Position);
            var item = listViewContent.GetItemAt(point.X, point.Y);
            if (item != null && (Directory.Exists(item.Name) || File.Exists(item.Name)))
            {
                查看ToolStripMenuItem.Visible = false;
                刷新ToolStripMenuItem.Visible = false;
                粘贴ToolStripMenuItem.Visible = false;
                新建文件夹ToolStripMenuItem.Visible = false;
                打开ToolStripMenuItem.Visible = true;
                剪切ToolStripMenuItem.Visible = true;
                复制ToolStripMenuItem.Visible = true;
                删除ToolStripMenuItem.Visible = true;
                return;
            }
            打开ToolStripMenuItem.Visible = false;
            剪切ToolStripMenuItem.Visible = false;
            复制ToolStripMenuItem.Visible = false;
            删除ToolStripMenuItem.Visible = false;
            查看ToolStripMenuItem.Visible = true;
            刷新ToolStripMenuItem.Visible = true;
            粘贴ToolStripMenuItem.Visible = true;
            新建文件夹ToolStripMenuItem.Visible = true;
        }
        private void 打开ToolStripMenuItem1_Click(object sender, EventArgs e) => Open();
        private void 大图标ToolStripMenuItem1_Click(object sender, EventArgs e) => ClearCheck(大图标ToolStripMenuItem, View.LargeIcon);
        private void 小图标ToolStripMenuItem1_Click(object sender, EventArgs e) => ClearCheck(小图标ToolStripMenuItem, View.SmallIcon);
        private void 列表ToolStripMenuItem1_Click(object sender, EventArgs e) => ClearCheck(列表ToolStripMenuItem, View.List);
        private void 详细信息ToolStripMenuItem1_Click(object sender, EventArgs e) => ClearCheck(详细信息ToolStripMenuItem, View.Details);
        private void 剪切ToolStripMenuItem2_Click(object sender, EventArgs e) => Cut();
        private void 复制ToolStripMenuItem2_Click(object sender, EventArgs e) => Copy();
        private void 删除ToolStripMenuItem2_Click(object sender, EventArgs e) => Delete();
        private void 粘贴ToolStripMenuItem1_Click(object sender, EventArgs e) => Paste();
        private void 新建文件夹ToolStripMenuItem_Click(object sender, EventArgs e) => CreateFolder();
        private void 刷新ToolStripMenuItem1_Click(object sender, EventArgs e) => ListUpdate(CurrentPath);
        #endregion
    }
}
