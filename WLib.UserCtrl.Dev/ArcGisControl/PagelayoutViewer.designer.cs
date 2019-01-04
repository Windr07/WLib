namespace WLib.UserCtrls.Dev.ArcGisControl
{
    partial class PagelayoutViewer
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PagelayoutViewer));
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            //this.identityPanel1 = new CommonLib.IdentityPanel();
            this.imageCollection_nav = new DevExpress.Utils.ImageCollection();
            this.barManagerForMap = new DevExpress.XtraBars.BarManager();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barBtnWholePageExtent = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnPagePan = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnZoomIn = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnZoomOut = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnPreView = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnNextView = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection_nav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerForMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Docking.DockPanel"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Float;
            this.dockPanel1.FloatLocation = new System.Drawing.Point(276, 165);
            this.dockPanel1.FloatSize = new System.Drawing.Size(388, 436);
            this.dockPanel1.ID = new System.Guid("352e4d60-8b1e-460d-bbd2-7904fe3f328c");
            this.dockPanel1.Location = new System.Drawing.Point(-32768, -32768);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel1.SavedIndex = 0;
            this.dockPanel1.Size = new System.Drawing.Size(388, 436);
            this.dockPanel1.Text = "信息查看";
            this.dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // dockPanel1_Container
            // 
            //this.dockPanel1_Container.Controls.Add(this.identityPanel1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(2, 24);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(384, 410);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // identityPanel1
            // 
            //this.identityPanel1.Active = true;
            //this.identityPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            //this.identityPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.identityPanel1.FlashWhenClick = false;
            //this.identityPanel1.Location = new System.Drawing.Point(0, 0);
            //this.identityPanel1.Name = "identityPanel1";
            //this.identityPanel1.Size = new System.Drawing.Size(384, 410);
            //this.identityPanel1.TabIndex = 0;
            // 
            // imageCollection_nav
            // 
            this.imageCollection_nav.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection_nav.ImageStream")));
            this.imageCollection_nav.Images.SetKeyName(0, "ElementSelectTool16.png");
            this.imageCollection_nav.Images.SetKeyName(1, "LayoutZoomWholePage16.png");
            this.imageCollection_nav.Images.SetKeyName(2, "LayoutPanTool16.png");
            this.imageCollection_nav.Images.SetKeyName(3, "LayoutZoomInTool16.png");
            this.imageCollection_nav.Images.SetKeyName(4, "LayoutZoomOutTool16.png");
            this.imageCollection_nav.Images.SetKeyName(5, "LayoutZoomPreviousExtent16.png");
            this.imageCollection_nav.Images.SetKeyName(6, "LayoutZoomNextExtent16.png");
            // 
            // barManagerForMap
            // 
            this.barManagerForMap.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManagerForMap.DockControls.Add(this.barDockControl1);
            this.barManagerForMap.DockControls.Add(this.barDockControl2);
            this.barManagerForMap.DockControls.Add(this.barDockControl3);
            this.barManagerForMap.DockControls.Add(this.barDockControl4);
            this.barManagerForMap.DockManager = this.dockManager1;
            this.barManagerForMap.Form = this;
            this.barManagerForMap.Images = this.imageCollection_nav;
            this.barManagerForMap.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barBtnWholePageExtent,
            this.barBtnZoomIn,
            this.barBtnZoomOut,
            this.barBtnPreView,
            this.barBtnPagePan,
            this.barBtnNextView});
            this.barManagerForMap.MaxItemId = 25;
            // 
            // bar2
            // 
            this.bar2.BarName = "Tools";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Left;
            this.bar2.FloatLocation = new System.Drawing.Point(29, 217);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnWholePageExtent),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnPagePan),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnZoomIn),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnZoomOut),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnPreView),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnNextView)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.Text = "Tools";
            // 
            // barBtnWholePageExtent
            // 
            this.barBtnWholePageExtent.Caption = "全局";
            this.barBtnWholePageExtent.Id = 1;
            this.barBtnWholePageExtent.ImageIndex = 1;
            this.barBtnWholePageExtent.Name = "barBtnWholePageExtent";
            this.barBtnWholePageExtent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSelectElement_ItemClick);
            // 
            // barBtnPagePan
            // 
            this.barBtnPagePan.Caption = "放大";
            this.barBtnPagePan.Id = 20;
            this.barBtnPagePan.ImageIndex = 2;
            this.barBtnPagePan.Name = "barBtnPagePan";
            this.barBtnPagePan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSelectElement_ItemClick);
            // 
            // barBtnZoomIn
            // 
            this.barBtnZoomIn.Caption = "缩小";
            this.barBtnZoomIn.Id = 3;
            this.barBtnZoomIn.ImageIndex = 3;
            this.barBtnZoomIn.Name = "barBtnZoomIn";
            this.barBtnZoomIn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSelectElement_ItemClick);
            // 
            // barBtnZoomOut
            // 
            this.barBtnZoomOut.Caption = "平移";
            this.barBtnZoomOut.Id = 4;
            this.barBtnZoomOut.ImageIndex = 4;
            this.barBtnZoomOut.Name = "barBtnZoomOut";
            this.barBtnZoomOut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSelectElement_ItemClick);
            // 
            // barBtnPreView
            // 
            this.barBtnPreView.Caption = "前一";
            this.barBtnPreView.Id = 5;
            this.barBtnPreView.ImageIndex = 5;
            this.barBtnPreView.Name = "barBtnPreView";
            this.barBtnPreView.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSelectElement_ItemClick);
            // 
            // barBtnNextView
            // 
            this.barBtnNextView.Caption = "后一";
            this.barBtnNextView.Id = 23;
            this.barBtnNextView.ImageIndex = 6;
            this.barBtnNextView.Name = "barBtnNextView";
            this.barBtnNextView.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSelectElement_ItemClick);
            // 
            // barDockControl1
            // 
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Size = new System.Drawing.Size(699, 0);
            // 
            // barDockControl2
            // 
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 488);
            this.barDockControl2.Size = new System.Drawing.Size(699, 0);
            // 
            // barDockControl3
            // 
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 0);
            this.barDockControl3.Size = new System.Drawing.Size(26, 488);
            // 
            // barDockControl4
            // 
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(699, 0);
            this.barDockControl4.Size = new System.Drawing.Size(0, 488);
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axPageLayoutControl1.Location = new System.Drawing.Point(26, 0);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(673, 488);
            this.axPageLayoutControl1.TabIndex = 13;
            this.axPageLayoutControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnMouseDownEventHandler(this.axPageLayoutControl1_OnMouseDown);
            this.axPageLayoutControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnMouseMoveEventHandler(this.axPageLayoutControl1_OnMouseMove);
            this.axPageLayoutControl1.OnDoubleClick += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnDoubleClickEventHandler(this.axPageLayoutControl1_OnDoubleClick);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(656, 4);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 18;
            // 
            // PagelayoutViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axPageLayoutControl1);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.Name = "PagelayoutViewer";
            this.Size = new System.Drawing.Size(699, 488);
            this.Load += new System.EventHandler(this.PagelayoutViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection_nav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerForMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ImageCollection imageCollection_nav;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        //private CommonLib.IdentityPanel identityPanel1;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarManager barManagerForMap;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barBtnWholePageExtent;
        private DevExpress.XtraBars.BarButtonItem barBtnZoomIn;
        private DevExpress.XtraBars.BarButtonItem barBtnZoomOut;
        private DevExpress.XtraBars.BarButtonItem barBtnPreView;
        private DevExpress.XtraBars.BarButtonItem barBtnPagePan;
        private DevExpress.XtraBars.BarButtonItem barBtnNextView;
        private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
    }
}
