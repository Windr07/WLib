////////////////////////////////////////////////////////////////
//根据某路径读取目录（文件）树的控件
//当前只识别的图标包括：mxd、jpg、bmp、png、doc、docx、xls、xlsx、pdf
//萧嘉明20130531
////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;

namespace WLib.UserCtrls.Dev
{
    /// <summary>
    /// Summary description for TreeExplorer.
    /// </summary>
    public partial class TreeExplorer : UserControl
    {

        /// <summary>
        /// 获取当前选中的文件，如果没有选中，则返回""
        /// </summary>
        public string CurrentFile
        {
            get
            {
                string res = "";
                try
                {
                    res = this.treeListFileInfo.FocusedNode[0].ToString();
                }
                catch { res = ""; }
                return res;
            }
        }

        public TreeExplorer()
        {
            InitializeComponent();
            InitData(@"");
            FocusChange = new EventHandler(UnboundMode_FocusChange);

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        void UnboundMode_FocusChange(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 选中项改变, 且选中的是文件项
        /// </summary>
        public event EventHandler FocusChange;

        /// <summary>
        /// 根据目录初始化控件
        /// </summary>
        /// <param name="Path"></param>
        public void InitData(string Path)
        {
            try
            {
                this.treeListFileInfo.ClearNodes();
                InitFolders(Path, null);
            }
            catch { throw new Exception("无法初始化目录:" + Path); }
        }

        void treeListFileInfo_GetPreviewText(object sender, DevExpress.XtraTreeList.GetPreviewTextEventArgs e)
        {
            e.PreviewText = "";
            TreeListNode node = e.Node;
            int columnId = 1;

            string ret = node.GetValue(columnId).ToString();
            while (node.ParentNode != null)
            {
                node = node.ParentNode;
                ret = node.GetValue(columnId).ToString() + "\\" + ret;
            }
            e.PreviewText = ret;
        }

        private void InitFolders(string path, TreeListNode pNode)
        {
            if (path.Equals("")) return;
            treeListFileInfo.BeginUnboundLoad();
            TreeListNode node;
            DirectoryInfo di;
            try
            {
                string[] root = Directory.GetDirectories(path);
                foreach (string s in root)
                {
                    try
                    {
                        if (s.EndsWith("_files"))
                        {
                            if (s.Length > 6)
                            {
                                string tempfilename = s.Substring(0, s.Length - 6) + ".html";
                                string tempfilename2 = s.Substring(0, s.Length - 6) + ".htm";
                                if (System.IO.File.Exists(tempfilename) || System.IO.File.Exists(tempfilename2))
                                    continue;
                            }
                        }

                        di = new DirectoryInfo(s);
                        node = treeListFileInfo.AppendNode(new object[] { s, di.Name, "Folder", null }, pNode);
                        node.StateImageIndex = 0;
                        node.HasChildren = HasFiles(s);
                        if (node.HasChildren)
                            node.Tag = true;
                    }
                    catch { }
                }
            }
            catch { }
            InitFiles(path, pNode);
            treeListFileInfo.EndUnboundLoad();
        }

        private void InitFiles(string path, TreeListNode pNode)
        {
            if (path.Equals("")) return;
            TreeListNode node;
            FileInfo fi;
            try
            {
                string[] root = Directory.GetFiles(path);
                List<int> potentialShpSubFileIndexList = new List<int>();
                List<string> shapeFileFileNameWithOutExtensionList = new List<string>();
                for (int i = 0; i < root.Length; i++)
                {
                    //System.IO.Path.GetExtension("C:\\asdfsa.BCd")=>".BCd"
                    string fiExt = System.IO.Path.GetExtension(root[i]).ToLower();
                    if (fiExt == ".shp")
                        shapeFileFileNameWithOutExtensionList.Add(System.IO.Path.GetFileNameWithoutExtension(root[i]));
                    else if (fiExt == ".shx" || fiExt == ".dbf" || fiExt == ".ain" || fiExt == ".aih" || fiExt == ".sbn" || fiExt == ".sbx" || fiExt == ".fbn" || fiExt == ".fbx" || fiExt == ".prj")
                    {
                        potentialShpSubFileIndexList.Add(i);
                    }
                    else if (fiExt == ".xml")
                    {
                        if (root[i].EndsWith(".shp.xml"))
                            potentialShpSubFileIndexList.Add(i);
                    }
                }
                for (int i = potentialShpSubFileIndexList.Count - 1; i >= 0; i--)
                {
                    string potentialFileName = System.IO.Path.GetFileName(root[potentialShpSubFileIndexList[i]]);
                    if (System.IO.Path.GetExtension(potentialFileName).ToLower() != ".xml")
                        potentialFileName = potentialFileName.Substring(0, potentialFileName.Length - 4).ToLower();
                    else
                        potentialFileName = potentialFileName.Substring(0, potentialFileName.Length - 8).ToLower();
                    if (!shapeFileFileNameWithOutExtensionList.Contains(potentialFileName))
                        potentialShpSubFileIndexList.RemoveAt(i);
                }

                int i_of_potential = 0;
                for (int i = 0; i < root.Length; i++)
                {
                    if (i_of_potential < potentialShpSubFileIndexList.Count)//如果需要跳过的shp附属文件没有检索完
                        if (i == potentialShpSubFileIndexList[i_of_potential])//如果当前索引等于需要跳过的shp附属文件的索引号
                        {
                            i_of_potential++;
                            continue;
                        }

                    string s = root[i];
                    fi = new FileInfo(s);
                    if (fi.Extension.ToLower() == ".lock")//跳过锁定文件
                        continue;

                    node = treeListFileInfo.AppendNode(new object[] { s, fi.Name, "File", fi.Length }, pNode);

                    ////下面这段代码是获取某文件对应的ico的位置
                    //RegistryKey rc = Registry.ClassesRoot.OpenSubKey(fi.Extension);
                    //RegistryKey rc2 = Registry.ClassesRoot.OpenSubKey(rc.GetValue("").ToString());
                    //RegistryKey rc3 = rc2.OpenSubKey("DefaultIcon");
                    //string icoPath = rc3.GetValue("").ToString();
                    if (fi.Extension.ToLower() == ".mxd")
                        node.StateImageIndex = 3;
                    else if (fi.Extension.ToLower() == ".jpg" || fi.Extension.ToLower() == ".bmp" || fi.Extension.ToLower() == ".png")
                        node.StateImageIndex = 4;
                    else if (fi.Extension.ToLower() == ".doc" || fi.Extension.ToLower() == ".docx")
                        node.StateImageIndex = 5;
                    else if (fi.Extension.ToLower() == ".xls" || fi.Extension.ToLower() == ".xlsx")
                        node.StateImageIndex = 6;
                    else if (fi.Extension.ToLower() == ".pdf")
                        node.StateImageIndex = 7;
                    else if (fi.Extension.ToLower() == ".html")
                        node.StateImageIndex = 8;
                    else if (fi.Extension.ToLower() == ".dbf")
                        node.StateImageIndex = 9;
                    else if (fi.Extension.ToLower() == ".lock")
                        node.StateImageIndex = 10;
                    else if (fi.Extension.ToLower() == ".shp")
                        node.StateImageIndex = 11;
                    else if (fi.Extension.ToLower() == ".bmp")
                        node.StateImageIndex = 12;
                    else if (fi.Extension.ToLower() == ".png")
                        node.StateImageIndex = 13;
                    else if (fi.Extension.ToLower() == ".gif")
                        node.StateImageIndex = 14;
                    else
                        node.StateImageIndex = 2;

                    node.HasChildren = false;
                }
            }
            catch { }
        }

        private void InitFiles_old(string path, TreeListNode pNode)
        {
            if (path.Equals("")) return;
            TreeListNode node;
            FileInfo fi;
            try
            {
                string[] root = Directory.GetFiles(path);

                for (int i = 0; i < root.Length; i++)
                {
                    string s = root[i];
                    fi = new FileInfo(s);
                    node = treeListFileInfo.AppendNode(new object[] { s, fi.Name, "File", fi.Length }, pNode);

                    ////下面这段代码是获取某文件对应的ico的位置
                    //RegistryKey rc = Registry.ClassesRoot.OpenSubKey(fi.Extension);
                    //RegistryKey rc2 = Registry.ClassesRoot.OpenSubKey(rc.GetValue("").ToString());
                    //RegistryKey rc3 = rc2.OpenSubKey("DefaultIcon");
                    //string icoPath = rc3.GetValue("").ToString();
                    if (fi.Extension.ToLower() == ".mxd")
                        node.StateImageIndex = 3;
                    else if (fi.Extension.ToLower() == ".jpg" || fi.Extension.ToLower() == ".bmp" || fi.Extension.ToLower() == ".png")
                        node.StateImageIndex = 4;
                    else if (fi.Extension.ToLower() == ".doc" || fi.Extension.ToLower() == ".docx")
                        node.StateImageIndex = 5;
                    else if (fi.Extension.ToLower() == ".xls" || fi.Extension.ToLower() == ".xlsx")
                        node.StateImageIndex = 6;
                    else if (fi.Extension.ToLower() == ".pdf")
                        node.StateImageIndex = 7;
                    else if (fi.Extension == ".shp")//先处理shp，再判断其他shp相关文件
                    {
                        node.StateImageIndex = 11;
                    }
                    node.HasChildren = false;
                }
            }
            catch { }
        }

        private bool HasFiles(string path)
        {
            string[] root = Directory.GetFiles(path);
            if (root.Length > 0) return true;
            root = Directory.GetDirectories(path);
            if (root.Length > 0) return true;
            return false;
        }

        private void treeListFileInfo_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                Cursor currentCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                InitFolders(e.Node.GetDisplayText("FullName"), e.Node);
                e.Node.Tag = null;
                Cursor.Current = currentCursor;
            }
        }

        private void treeListFileInfo_AfterExpand(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (e.Node.StateImageIndex < 2) e.Node.StateImageIndex = 1;
        }

        private void treeListFileInfo_AfterCollapse(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (e.Node.StateImageIndex < 2) e.Node.StateImageIndex = 0;
        }

        private void treeListFileInfo_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                if (e.Node == null) return;
                if (e.Node[2].ToString().Equals("File"))
                {
                    FocusChange(e.Node[0].ToString(), new EventArgs());
                    //UnboundMode_FocusChange(e.Node[0].ToString(), new EventArgs());
                }
            }
            catch (System.Exception ex)
            {

            }
        }

    }
}
