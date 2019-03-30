using System;
using System.Windows.Forms;
using WLib.ArcGis.Carto.Element;
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
            try
            {
                this.mapViewer1.MainMapControl.LoadMxFile(AppDomain.CurrentDomain.BaseDirectory + @"Data\\SampleData.mxd");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void FileToolStripMenuItem_Click(object sender, EventArgs e)//文件菜单
        {
            var menuName = (sender as ToolStripMenuItem)?.Name;
            var docHelper = this.mapViewer1.DocHelper;

            if (menuName == this.新建ToolStripMenuItem.Name) docHelper.NewEmptyDoc();
            else if (menuName == this.打开ToolStripMenuItem.Name) docHelper.OpenDoc();
            else if (menuName == this.保存ToolStripMenuItem.Name) docHelper.Save();
            else if (menuName == this.另存为ToolStripMenuItem.Name) docHelper.SaveAs();
            else if (menuName == this.添加数据1ToolStripMenuItem.Name) docHelper.AddData();
            else if (menuName == this.添加数据2ToolStripMenuItem.Name) docHelper.AddData();
            else if (menuName == this.退出ToolStripMenuItem.Name) Application.Exit();
        }

        private void ViewToolStripMenuItem_Click(object sender, EventArgs e)//视图菜单
        {
            var menuName = (sender as ToolStripMenuItem)?.Name;
            var mapTools = this.mapViewer1.MapNavigationTools;
            this.mapViewer1.DrawElementHelper.DrawElementType = EDrawElementType.None;

            if (menuName == this.放大ToolStripMenuItem.Name) mapTools.CurrentTool = EMapTools.ZoomIn;
            else if (menuName == this.缩小ToolStripMenuItem.Name) mapTools.CurrentTool = EMapTools.ZoomOut;
            else if (menuName == this.漫游ToolStripMenuItem.Name) mapTools.CurrentTool = EMapTools.Pan;
            else if (menuName == this.全图ToolStripMenuItem.Name) mapTools.CurrentTool = EMapTools.FullExtent;
            else if (menuName == this.拉框选择ToolStripMenuItem.Name) mapTools.CurrentTool = EMapTools.Selection;
            else if (menuName == this.上一视图ToolStripMenuItem.Name) mapTools.CurrentTool = EMapTools.PreView;
            else if (menuName == this.上一视图ToolStripMenuItem.Name) mapTools.CurrentTool = EMapTools.NextView;
        }

        private void DrawToolStripMenuItem_Click(object sender, EventArgs e)//绘制菜单
        {
            var menuName = (sender as ToolStripMenuItem)?.Name;
            var drawHelper = this.mapViewer1.DrawElementHelper;
            this.mapViewer1.MapNavigationTools.CurrentTool = EMapTools.None;

            if (menuName == this.点ToolStripMenuItem.Name) drawHelper.DrawElementType = EDrawElementType.Point;
            else if (menuName == this.线ToolStripMenuItem.Name) drawHelper.DrawElementType = EDrawElementType.Polyline;
            else if (menuName == this.多边形ToolStripMenuItem.Name) drawHelper.DrawElementType = EDrawElementType.Polygon;
            else if (menuName == this.圆ToolStripMenuItem.Name) drawHelper.DrawElementType = EDrawElementType.Circle;
            else if (menuName == this.矩形ToolStripMenuItem.Name) drawHelper.DrawElementType = EDrawElementType.Rectangle;
            else if (menuName == this.文本ToolStripMenuItem.Name) drawHelper.DrawElementType = EDrawElementType.Text;
        }
    }
}
