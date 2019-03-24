using System;
using System.Windows.Forms;
using WLib.ArcGis.Control.MapAssociation;

namespace WLib.Samples.WinForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)//加载地图
        {

        }

        private void FileToolStripMenuItem_Click(object sender, EventArgs e)//文件菜单
        {
            var menuName = (sender as ToolStripMenuItem)?.Name;
            var docHelper = this.mapViewer1.DocHelper;

            if (menuName == this.新建ToolStripMenuItem.Name) docHelper.NewEmptyDoc();
            else if (menuName == this.打开ToolStripMenuItem.Name) docHelper.OpenDoc();
            else if (menuName == this.保存ToolStripMenuItem.Name) docHelper.Save();
            else if (menuName == this.另存为ToolStripMenuItem.Name) docHelper.Save();
            else if (menuName == this.添加图层ToolStripMenuItem.Name) docHelper.Save();
            else if (menuName == this.退出ToolStripMenuItem.Name) this.Close();
        }

        private void ViewToolStripMenuItem_Click(object sender, EventArgs e)//视图菜单
        {
            var menuName = (sender as ToolStripMenuItem)?.Name;
            var mapTools = this.mapViewer1.MapNavigationTools;

            if (menuName == this.放大ToolStripMenuItem.Name) mapTools.ToolOnClick(EMapTools.ZoomIn);
            else if (menuName == this.缩小ToolStripMenuItem.Name) mapTools.ToolOnClick(EMapTools.ZoomOut);
            else if (menuName == this.漫游ToolStripMenuItem.Name) mapTools.ToolOnClick(EMapTools.Pan);
            else if (menuName == this.全图ToolStripMenuItem.Name) mapTools.ToolOnClick(EMapTools.FullExtent);
            else if (menuName == this.拉框选择ToolStripMenuItem.Name) mapTools.ToolOnClick(EMapTools.Selection);
            else if (menuName == this.上一视图ToolStripMenuItem.Name) mapTools.ToolOnClick(EMapTools.PreView);
            else if (menuName == this.上一视图ToolStripMenuItem.Name) mapTools.ToolOnClick(EMapTools.NextView);
        }
    }
}
