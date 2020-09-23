using DevExpress.Utils;
using DevExpress.XtraBars.Ribbon;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Linq;
using System.Windows.Forms;
using WLib.ArcGis.Analysis.Gp;
using WLib.ArcGis.Carto.Element;
using WLib.ArcGis.Control;
using WLib.ArcGis.Control.MapAssociation;
using WLib.WinCtrls.Dev.ArcGisCtrl;
using WLib.WinCtrls.Dev.Extension;

namespace WLib.Samples.WinForm.Dev
{
    public partial class MainForm : RibbonForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)//加载地图
        {
            try
            {
                this.mapViewer1.MainMapControl.LoadMxFile(AppDomain.CurrentDomain.BaseDirectory + @"Data\SampleData.mxd");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void FileToolStripMenuItem_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)//文件菜单
        {
            var menuName = e.Item.Name;
            var docHelper = this.mapViewer1.Manger.DocHelper;

            if (menuName == this.btnNew.Name) docHelper.NewEmptyDoc();
            else if (menuName == this.btnOpen.Name) docHelper.OpenDoc();
            else if (menuName == this.btnSave.Name) docHelper.Save();
            else if (menuName == this.btnSaveAs.Name) docHelper.SaveAs();
            else if (menuName == this.btnAddData.Name) docHelper.AddData();
            else if (menuName == this.btnExit.Name) Application.Exit();
        }

        private void ViewToolStripMenuItem_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)//视图菜单
        {
            var menuName = e.Item.Name;
            var mapTools = this.mapViewer1.MapNavigationTools;
            this.mapViewer1.Manger.DrawElementHelper.DrawElementType = EDrawElementType.None;

            if (menuName == this.btnZoomIn.Name) mapTools.CurrentTool = EMapTools.ZoomIn;
            else if (menuName == this.btnZoomOut.Name) mapTools.CurrentTool = EMapTools.ZoomOut;
            else if (menuName == this.btnPan.Name) mapTools.CurrentTool = EMapTools.Pan;
            else if (menuName == this.btnFullExtent.Name) mapTools.CurrentTool = EMapTools.FullExtent;
            else if (menuName == this.btnEvelopeSelect.Name) mapTools.CurrentTool = EMapTools.Selection;
            else if (menuName == this.btnPreView.Name) mapTools.CurrentTool = EMapTools.PreView;
            else if (menuName == this.btnNextView.Name) mapTools.CurrentTool = EMapTools.NextView;
        }

        private void DrawToolStripMenuItem_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)//绘制菜单
        {
            var menuName = e.Item.Name;
            var drawHelper = this.mapViewer1.Manger.DrawElementHelper;
            this.mapViewer1.MapNavigationTools.CurrentTool = EMapTools.None;

            if (menuName == this.btnPoint.Name) drawHelper.DrawElementType = EDrawElementType.Point;
            else if (menuName == this.btnLine.Name) drawHelper.DrawElementType = EDrawElementType.Polyline;
            else if (menuName == this.btnPolygon.Name) drawHelper.DrawElementType = EDrawElementType.Polygon;
            else if (menuName == this.btnCircle.Name) drawHelper.DrawElementType = EDrawElementType.Circle;
            else if (menuName == this.btnRectangle.Name) drawHelper.DrawElementType = EDrawElementType.Rectangle;
            else if (menuName == this.btnText.Name) drawHelper.DrawElementType = EDrawElementType.Text;
        }

        private void 查询图形ToolStripMenuItem_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.mapViewer1.MainMapControl.LayerCount == 0) return;
            var form = new AttributeQueryForm();
            form.LoadQueryInfo(this.mapViewer1.MainMapControl.GetLayers().Select(v => v as ITable));
            form.Show(this);
        }

        private void 查询属性ToolStripMenuItem_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.mapViewer1.MainMapControl.LayerCount == 0) return;
            var form = new AttributeForm();
            form.AttributeCtrl.LoadAttribute(this.mapViewer1.MainMapControl.GetLayers().First() as ITable);
            form.Show(this);
        }

        private void 导出数据ToolStripMenuItem_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }


        private void ExportImages()
        {
            try
            {
                var imageCollection = new ImageCollection();
                imageCollection.ExportImageCollectionImages(Environment.CurrentDirectory + @"\Photo\");
            }
            catch (Exception ex) { MessageBox.Show(@"错误：", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void RunIntersect()
        {
            try
            {
                var dbPath = AppDomain.CurrentDomain.BaseDirectory + @"Data\SampleData.mdb";
                var path1 = dbPath + @"\XZQ;";
                var resultPath = dbPath + @"\XZQ_Intersect";
                new GpHelper(true).RunTool(GpHelper.Intersect(path1, resultPath), out _, out _);
            }
            catch (Exception ex) { MessageBox.Show(@"错误：", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
