namespace WLib.Samples.WinForm.Dev
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNew = new DevExpress.XtraBars.BarButtonItem();
            this.btnOpen = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.btnAddData1 = new DevExpress.XtraBars.BarButtonItem();
            this.btnAddData2 = new DevExpress.XtraBars.BarButtonItem();
            this.btnExit = new DevExpress.XtraBars.BarButtonItem();
            this.btnZoomIn = new DevExpress.XtraBars.BarButtonItem();
            this.btnZoomOut = new DevExpress.XtraBars.BarButtonItem();
            this.btnFullExtent = new DevExpress.XtraBars.BarButtonItem();
            this.btnPan = new DevExpress.XtraBars.BarButtonItem();
            this.btnPreView = new DevExpress.XtraBars.BarButtonItem();
            this.btnNextView = new DevExpress.XtraBars.BarButtonItem();
            this.btnEvelopeSelect = new DevExpress.XtraBars.BarButtonItem();
            this.btnPoint = new DevExpress.XtraBars.BarButtonItem();
            this.btnLine = new DevExpress.XtraBars.BarButtonItem();
            this.btnPolygon = new DevExpress.XtraBars.BarButtonItem();
            this.btnRectangle = new DevExpress.XtraBars.BarButtonItem();
            this.btnCircle = new DevExpress.XtraBars.BarButtonItem();
            this.btnText = new DevExpress.XtraBars.BarButtonItem();
            this.btnSimpleRender = new DevExpress.XtraBars.BarButtonItem();
            this.btnClassifyRnder = new DevExpress.XtraBars.BarButtonItem();
            this.btnChartRender = new DevExpress.XtraBars.BarButtonItem();
            this.btnUniqueValueRender = new DevExpress.XtraBars.BarButtonItem();
            this.btnQueryGeometry = new DevExpress.XtraBars.BarButtonItem();
            this.btnQueryAttribute = new DevExpress.XtraBars.BarButtonItem();
            this.btnExport = new DevExpress.XtraBars.BarButtonItem();
            this.btnImport = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage3 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage4 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage5 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup5 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage6 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup6 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.mapViewer1 = new WLib.WinCtrls.Dev.ArcGisCtrl.MapViewer();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.btnNew,
            this.btnOpen,
            this.btnSave,
            this.btnSaveAs,
            this.btnAddData1,
            this.btnAddData2,
            this.btnExit,
            this.btnZoomIn,
            this.btnZoomOut,
            this.btnFullExtent,
            this.btnPan,
            this.btnPreView,
            this.btnNextView,
            this.btnEvelopeSelect,
            this.btnPoint,
            this.btnLine,
            this.btnPolygon,
            this.btnRectangle,
            this.btnCircle,
            this.btnText,
            this.btnSimpleRender,
            this.btnClassifyRnder,
            this.btnChartRender,
            this.btnUniqueValueRender,
            this.btnQueryGeometry,
            this.btnQueryAttribute,
            this.btnExport,
            this.btnImport});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 35;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1,
            this.ribbonPage2,
            this.ribbonPage3,
            this.ribbonPage4,
            this.ribbonPage5,
            this.ribbonPage6});
            this.ribbonControl1.Size = new System.Drawing.Size(879, 166);
            // 
            // btnNew
            // 
            this.btnNew.Caption = "新建";
            this.btnNew.Id = 2;
            this.btnNew.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.ImageOptions.Image")));
            this.btnNew.Name = "btnNew";
            this.btnNew.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.FileToolStripMenuItem_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Caption = "打开";
            this.btnOpen.Id = 3;
            this.btnOpen.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.ImageOptions.Image")));
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.FileToolStripMenuItem_Click);
            // 
            // btnSave
            // 
            this.btnSave.Caption = "保存";
            this.btnSave.Id = 4;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Name = "btnSave";
            this.btnSave.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.FileToolStripMenuItem_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Caption = "另存为";
            this.btnSaveAs.Id = 5;
            this.btnSaveAs.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAs.ImageOptions.Image")));
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnSaveAs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.FileToolStripMenuItem_Click);
            // 
            // btnAddData1
            // 
            this.btnAddData1.Caption = "添加数据1";
            this.btnAddData1.Id = 6;
            this.btnAddData1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAddData1.ImageOptions.Image")));
            this.btnAddData1.Name = "btnAddData1";
            this.btnAddData1.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnAddData1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.FileToolStripMenuItem_Click);
            // 
            // btnAddData2
            // 
            this.btnAddData2.Caption = "添加数据2";
            this.btnAddData2.Id = 8;
            this.btnAddData2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAddData2.ImageOptions.Image")));
            this.btnAddData2.Name = "btnAddData2";
            this.btnAddData2.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnAddData2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.FileToolStripMenuItem_Click);
            // 
            // btnExit
            // 
            this.btnExit.Caption = "退出";
            this.btnExit.Id = 9;
            this.btnExit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.ImageOptions.Image")));
            this.btnExit.Name = "btnExit";
            this.btnExit.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.FileToolStripMenuItem_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Caption = "放大";
            this.btnZoomIn.Id = 10;
            this.btnZoomIn.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomIn.ImageOptions.Image")));
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnZoomIn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ViewToolStripMenuItem_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Caption = "缩小";
            this.btnZoomOut.Id = 11;
            this.btnZoomOut.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomOut.ImageOptions.Image")));
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnZoomOut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ViewToolStripMenuItem_Click);
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.Caption = "全图";
            this.btnFullExtent.Id = 12;
            this.btnFullExtent.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnFullExtent.ImageOptions.Image")));
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnFullExtent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ViewToolStripMenuItem_Click);
            // 
            // btnPan
            // 
            this.btnPan.Caption = "漫游";
            this.btnPan.Id = 13;
            this.btnPan.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPan.ImageOptions.Image")));
            this.btnPan.Name = "btnPan";
            this.btnPan.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnPan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ViewToolStripMenuItem_Click);
            // 
            // btnPreView
            // 
            this.btnPreView.Caption = "上一视图";
            this.btnPreView.Id = 14;
            this.btnPreView.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPreView.ImageOptions.Image")));
            this.btnPreView.Name = "btnPreView";
            this.btnPreView.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnPreView.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ViewToolStripMenuItem_Click);
            // 
            // btnNextView
            // 
            this.btnNextView.Caption = "下一视图";
            this.btnNextView.Id = 15;
            this.btnNextView.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNextView.ImageOptions.Image")));
            this.btnNextView.Name = "btnNextView";
            this.btnNextView.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnNextView.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ViewToolStripMenuItem_Click);
            // 
            // btnEvelopeSelect
            // 
            this.btnEvelopeSelect.Caption = "拉框选择";
            this.btnEvelopeSelect.Id = 16;
            this.btnEvelopeSelect.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEvelopeSelect.ImageOptions.Image")));
            this.btnEvelopeSelect.Name = "btnEvelopeSelect";
            this.btnEvelopeSelect.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnEvelopeSelect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ViewToolStripMenuItem_Click);
            // 
            // btnPoint
            // 
            this.btnPoint.Caption = "点";
            this.btnPoint.Id = 17;
            this.btnPoint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem8.ImageOptions.Image")));
            this.btnPoint.Name = "btnPoint";
            this.btnPoint.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnPoint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.DrawToolStripMenuItem_Click);
            // 
            // btnLine
            // 
            this.btnLine.Caption = "线";
            this.btnLine.Id = 18;
            this.btnLine.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem9.ImageOptions.Image")));
            this.btnLine.Name = "btnLine";
            this.btnLine.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnLine.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.DrawToolStripMenuItem_Click);
            // 
            // btnPolygon
            // 
            this.btnPolygon.Caption = "多边形";
            this.btnPolygon.Id = 19;
            this.btnPolygon.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem10.ImageOptions.Image")));
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnPolygon.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.DrawToolStripMenuItem_Click);
            // 
            // btnRectangle
            // 
            this.btnRectangle.Caption = "矩形";
            this.btnRectangle.Id = 20;
            this.btnRectangle.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem11.ImageOptions.Image")));
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnRectangle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.DrawToolStripMenuItem_Click);
            // 
            // btnCircle
            // 
            this.btnCircle.Caption = "圆";
            this.btnCircle.Id = 21;
            this.btnCircle.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem12.ImageOptions.Image")));
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnCircle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.DrawToolStripMenuItem_Click);
            // 
            // btnText
            // 
            this.btnText.Caption = "文本";
            this.btnText.Id = 22;
            this.btnText.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem13.ImageOptions.Image")));
            this.btnText.Name = "btnText";
            this.btnText.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnText.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.DrawToolStripMenuItem_Click);
            // 
            // btnSimpleRender
            // 
            this.btnSimpleRender.Caption = "简单渲染";
            this.btnSimpleRender.Id = 25;
            this.btnSimpleRender.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.Image")));
            this.btnSimpleRender.Name = "btnSimpleRender";
            this.btnSimpleRender.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            // 
            // btnClassifyRnder
            // 
            this.btnClassifyRnder.Caption = "分级渲染";
            this.btnClassifyRnder.Id = 26;
            this.btnClassifyRnder.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.ImageOptions.Image")));
            this.btnClassifyRnder.Name = "btnClassifyRnder";
            this.btnClassifyRnder.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            // 
            // btnChartRender
            // 
            this.btnChartRender.Caption = "图表渲染";
            this.btnChartRender.Id = 27;
            this.btnChartRender.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.ImageOptions.Image")));
            this.btnChartRender.Name = "btnChartRender";
            this.btnChartRender.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            // 
            // btnUniqueValueRender
            // 
            this.btnUniqueValueRender.Caption = "唯一值渲染";
            this.btnUniqueValueRender.Id = 28;
            this.btnUniqueValueRender.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem4.ImageOptions.Image")));
            this.btnUniqueValueRender.Name = "btnUniqueValueRender";
            this.btnUniqueValueRender.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            // 
            // btnQueryGeometry
            // 
            this.btnQueryGeometry.Caption = "查询图形";
            this.btnQueryGeometry.Id = 29;
            this.btnQueryGeometry.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem5.ImageOptions.Image")));
            this.btnQueryGeometry.Name = "btnQueryGeometry";
            this.btnQueryGeometry.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnQueryGeometry.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.查询图形ToolStripMenuItem_Click);
            // 
            // btnQueryAttribute
            // 
            this.btnQueryAttribute.Caption = "查询属性";
            this.btnQueryAttribute.Id = 30;
            this.btnQueryAttribute.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem6.ImageOptions.Image")));
            this.btnQueryAttribute.Name = "btnQueryAttribute";
            this.btnQueryAttribute.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnQueryAttribute.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.查询属性ToolStripMenuItem_Click);
            // 
            // btnExport
            // 
            this.btnExport.Caption = "导出数据";
            this.btnExport.Id = 33;
            this.btnExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem15.ImageOptions.Image")));
            this.btnExport.Name = "btnExport";
            this.btnExport.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.导出数据ToolStripMenuItem_Click);
            // 
            // btnImport
            // 
            this.btnImport.Caption = "导入数据";
            this.btnImport.Id = 34;
            this.btnImport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem16.ImageOptions.Image")));
            this.btnImport.Name = "btnImport";
            this.btnImport.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.导出数据ToolStripMenuItem_Click);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "文件";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnNew);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnOpen);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnSaveAs);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnAddData1);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnAddData2);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnExit);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "文件";
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2});
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "视图";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.btnZoomIn);
            this.ribbonPageGroup2.ItemLinks.Add(this.btnZoomOut);
            this.ribbonPageGroup2.ItemLinks.Add(this.btnFullExtent);
            this.ribbonPageGroup2.ItemLinks.Add(this.btnPan);
            this.ribbonPageGroup2.ItemLinks.Add(this.btnEvelopeSelect);
            this.ribbonPageGroup2.ItemLinks.Add(this.btnPreView);
            this.ribbonPageGroup2.ItemLinks.Add(this.btnNextView);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "视图";
            // 
            // ribbonPage3
            // 
            this.ribbonPage3.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup3});
            this.ribbonPage3.Name = "ribbonPage3";
            this.ribbonPage3.Text = "绘制";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.btnPoint);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnLine);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnPolygon);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnRectangle);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnCircle);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnText);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "绘制";
            // 
            // ribbonPage4
            // 
            this.ribbonPage4.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup4});
            this.ribbonPage4.Name = "ribbonPage4";
            this.ribbonPage4.Text = "专题";
            // 
            // ribbonPageGroup4
            // 
            this.ribbonPageGroup4.ItemLinks.Add(this.btnSimpleRender);
            this.ribbonPageGroup4.ItemLinks.Add(this.btnClassifyRnder);
            this.ribbonPageGroup4.ItemLinks.Add(this.btnChartRender);
            this.ribbonPageGroup4.ItemLinks.Add(this.btnUniqueValueRender);
            this.ribbonPageGroup4.Name = "ribbonPageGroup4";
            this.ribbonPageGroup4.Text = "专题";
            // 
            // ribbonPage5
            // 
            this.ribbonPage5.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup5});
            this.ribbonPage5.Name = "ribbonPage5";
            this.ribbonPage5.Text = "查询";
            // 
            // ribbonPageGroup5
            // 
            this.ribbonPageGroup5.ItemLinks.Add(this.btnQueryGeometry);
            this.ribbonPageGroup5.ItemLinks.Add(this.btnQueryAttribute);
            this.ribbonPageGroup5.Name = "ribbonPageGroup5";
            this.ribbonPageGroup5.Text = "查询";
            // 
            // ribbonPage6
            // 
            this.ribbonPage6.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup6});
            this.ribbonPage6.Name = "ribbonPage6";
            this.ribbonPage6.Text = "数据";
            // 
            // ribbonPageGroup6
            // 
            this.ribbonPageGroup6.ItemLinks.Add(this.btnExport);
            this.ribbonPageGroup6.ItemLinks.Add(this.btnImport);
            this.ribbonPageGroup6.Name = "ribbonPageGroup6";
            this.ribbonPageGroup6.Text = "数据";
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "The Bezier";
            // 
            // mapViewer1
            // 
            this.mapViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapViewer1.Location = new System.Drawing.Point(0, 166);
            this.mapViewer1.Name = "mapViewer1";
            this.mapViewer1.Size = new System.Drawing.Size(879, 407);
            this.mapViewer1.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 573);
            this.Controls.Add(this.mapViewer1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "MainForm";
            this.Ribbon = this.ribbonControl1;
            this.Text = "Sample";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraBars.BarButtonItem btnNew;
        private DevExpress.XtraBars.BarButtonItem btnOpen;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem btnSaveAs;
        private DevExpress.XtraBars.BarButtonItem btnAddData1;
        private DevExpress.XtraBars.BarButtonItem btnAddData2;
        private DevExpress.XtraBars.BarButtonItem btnExit;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraBars.BarButtonItem btnZoomIn;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem btnZoomOut;
        private DevExpress.XtraBars.BarButtonItem btnFullExtent;
        private DevExpress.XtraBars.BarButtonItem btnPan;
        private DevExpress.XtraBars.BarButtonItem btnPreView;
        private DevExpress.XtraBars.BarButtonItem btnNextView;
        private DevExpress.XtraBars.BarButtonItem btnEvelopeSelect;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage3;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarButtonItem btnPoint;
        private DevExpress.XtraBars.BarButtonItem btnLine;
        private DevExpress.XtraBars.BarButtonItem btnPolygon;
        private DevExpress.XtraBars.BarButtonItem btnRectangle;
        private DevExpress.XtraBars.BarButtonItem btnCircle;
        private DevExpress.XtraBars.BarButtonItem btnText;
        private WinCtrls.Dev.ArcGisCtrl.MapViewer mapViewer1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage4;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup4;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage5;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup5;
        private DevExpress.XtraBars.BarButtonItem btnSimpleRender;
        private DevExpress.XtraBars.BarButtonItem btnClassifyRnder;
        private DevExpress.XtraBars.BarButtonItem btnChartRender;
        private DevExpress.XtraBars.BarButtonItem btnUniqueValueRender;
        private DevExpress.XtraBars.BarButtonItem btnQueryGeometry;
        private DevExpress.XtraBars.BarButtonItem btnQueryAttribute;
        private DevExpress.XtraBars.BarButtonItem btnExport;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage6;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup6;
        private DevExpress.XtraBars.BarButtonItem btnImport;
    }
}