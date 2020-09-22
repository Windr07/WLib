using ESRI.ArcGIS.Geodatabase;
using System;
using System.Linq;
using System.Windows.Forms;
using WLib.ArcGis.Carto.Element;
using WLib.ArcGis.Control;
using WLib.ArcGis.Control.MapAssociation;
using WLib.ArcGis.Data;
using WLib.ArcGis.GeoDatabase.FeatClass;
using WLib.ArcGis.GeoDatabase.WorkSpace;
using WLib.Database;
using WLib.WinCtrls.ArcGisCtrl;
using FolderBrowserDialog = WLib.WinCtrls.ExplorerCtrl.FileFolderCtrl.FolderBrowserDialog;

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
            var docHelper = this.mapViewer1.Manger.DocHelper;

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
            this.mapViewer1.Manger.DrawElementHelper.DrawElementType = EDrawElementType.None;

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
            var drawHelper = this.mapViewer1.Manger.DrawElementHelper;
            this.mapViewer1.MapNavigationTools.CurrentTool = EMapTools.None;

            if (menuName == this.点ToolStripMenuItem.Name) drawHelper.DrawElementType = EDrawElementType.Point;
            else if (menuName == this.线ToolStripMenuItem.Name) drawHelper.DrawElementType = EDrawElementType.Polyline;
            else if (menuName == this.多边形ToolStripMenuItem.Name) drawHelper.DrawElementType = EDrawElementType.Polygon;
            else if (menuName == this.圆ToolStripMenuItem.Name) drawHelper.DrawElementType = EDrawElementType.Circle;
            else if (menuName == this.矩形ToolStripMenuItem.Name) drawHelper.DrawElementType = EDrawElementType.Rectangle;
            else if (menuName == this.文本ToolStripMenuItem.Name) drawHelper.DrawElementType = EDrawElementType.Text;
        }

        private void 查询图形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.mapViewer1.MainMapControl.LayerCount == 0) return;
            var form = new AttributeQueryForm();
            form.LoadQueryInfo(this.mapViewer1.MainMapControl.GetLayers().Select(v => v as ITable));
            form.Show(this);
        }

        private void 查询属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.mapViewer1.MainMapControl.LayerCount == 0) return;
            var form = new AttributeForm();
            form.AttributeCtrl.LoadAttribute(this.mapViewer1.MainMapControl.GetLayers().First() as ITable);
            form.Show(this);
        }

        private void 导出数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 导入数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Test()
        {
            double sumRiverLength = 0.0;
            FeatureClassEx.FromPath(@"c:\River.shp").QueryFeatures(@"XZQDM = '440000'",
                feature => sumRiverLength += feature.ToDouble("RiverLength"));

            var workspace = WorkspaceEx.GetWorkSpace(@"c:\World.mdb");
            FeatureClassEx.FromPath(@"c:\World.mdb\river").CopyStruct(workspace, "NewRiver", "河流");

            var connString = DbHelper.Dbf_OleDb4(@"c:\River.dbf");

            DbHelper.GetOleDbHelper(connString).ExcNonQuery(@"update River set Name = 'Pearl River' where RiverCode ='003'");
            var connString2 = DbHelper.Access_OleDb4(@"c:\World.mdb");
        }

        private void 测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.ShowDialog(this);
        }
    }
}
