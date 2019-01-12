namespace WLib.UserCtrls.Dev.ArcGisCtrl
{
    partial class MapViewerSimple
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapViewerSimple));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.sBtnExpend = new DevExpress.XtraEditors.SimpleButton();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.imageCollection_nav = new DevExpress.Utils.ImageCollection();
            this.barManagerForMap = new DevExpress.XtraBars.BarManager();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barBtnSelectElement = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnFullExtent = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnZoomIn = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnZoomOut = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnPan = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnPreView = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnNextView = new DevExpress.XtraBars.BarButtonItem();
            this.barSelectFeature = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnClearSelection = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnIdentify = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnZoomToTarget = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.axMapControlMainMap = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection_nav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerForMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControlMainMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.sBtnExpend);
            this.panelControl1.Controls.Add(this.axTOCControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl1.Location = new System.Drawing.Point(26, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(200, 488);
            this.panelControl1.TabIndex = 7;
            // 
            // sBtnExpend
            // 
            this.sBtnExpend.AllowFocus = false;
            this.sBtnExpend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnExpend.Location = new System.Drawing.Point(179, 1);
            this.sBtnExpend.Name = "sBtnExpend";
            this.sBtnExpend.Size = new System.Drawing.Size(20, 20);
            this.sBtnExpend.TabIndex = 4;
            this.sBtnExpend.Text = "<";
            this.sBtnExpend.Click += new System.EventHandler(this.sBtnExpend_Click);
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTOCControl1.Location = new System.Drawing.Point(2, 2);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(196, 484);
            this.axTOCControl1.TabIndex = 3;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(226, 0);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(6, 488);
            this.splitterControl1.TabIndex = 8;
            this.splitterControl1.TabStop = false;
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
            this.dockPanel1_Container.Location = new System.Drawing.Point(2, 24);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(384, 410);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // imageCollection_nav
            // 
            this.imageCollection_nav.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection_nav.ImageStream")));
            this.imageCollection_nav.Images.SetKeyName(0, "Nav_ZoomFull_16.png");
            this.imageCollection_nav.Images.SetKeyName(1, "Nav_Identify_16.png");
            this.imageCollection_nav.Images.SetKeyName(2, "Nav_Pan_16.png");
            this.imageCollection_nav.Images.SetKeyName(3, "Nav_Close_16.png");
            this.imageCollection_nav.Images.SetKeyName(4, "Nav_ZoomIn_16.png");
            this.imageCollection_nav.Images.SetKeyName(5, "Nav_ZoomOut_16.png");
            this.imageCollection_nav.Images.SetKeyName(6, "Nav_Right_16.png");
            this.imageCollection_nav.Images.SetKeyName(7, "Nav_Area_16.png");
            this.imageCollection_nav.Images.SetKeyName(8, "Nav_Left_16.png");
            this.imageCollection_nav.Images.SetKeyName(9, "Nav_Length_16.png");
            this.imageCollection_nav.Images.SetKeyName(10, "ElementSelectTool16.png");
            this.imageCollection_nav.Images.SetKeyName(11, "SelectionSelectTool16.png");
            this.imageCollection_nav.Images.SetKeyName(12, "SelectionClearSelected_small16.png");
            this.imageCollection_nav.Images.SetKeyName(13, "3DCenterOnTarget16.png");
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
            this.barBtnFullExtent,
            this.barBtnZoomOut,
            this.barBtnPan,
            this.barBtnPreView,
            this.barBtnClearSelection,
            this.barBtnIdentify,
            this.barBtnSelectElement,
            this.barBtnZoomIn,
            this.barBtnNextView,
            this.barSelectFeature,
            this.barBtnZoomToTarget});
            this.barManagerForMap.MaxItemId = 26;
            // 
            // bar2
            // 
            this.bar2.BarName = "Tools";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Left;
            this.bar2.FloatLocation = new System.Drawing.Point(29, 217);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnSelectElement),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnFullExtent),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnZoomIn),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnZoomOut),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnPan),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnPreView),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnNextView),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSelectFeature),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnClearSelection),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnIdentify),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnZoomToTarget)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.Text = "Tools";
            // 
            // barBtnSelectElement
            // 
            this.barBtnSelectElement.Caption = "指针";
            this.barBtnSelectElement.Id = 10;
            this.barBtnSelectElement.ImageIndex = 10;
            this.barBtnSelectElement.Name = "barBtnSelectElement";
            this.barBtnSelectElement.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barBtnSelectElement.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSelectElement_ItemClick);
            // 
            // barBtnFullExtent
            // 
            this.barBtnFullExtent.Caption = "全局";
            this.barBtnFullExtent.Id = 1;
            this.barBtnFullExtent.ImageIndex = 0;
            this.barBtnFullExtent.Name = "barBtnFullExtent";
            this.barBtnFullExtent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSelectElement_ItemClick);
            // 
            // barBtnZoomIn
            // 
            this.barBtnZoomIn.Caption = "放大";
            this.barBtnZoomIn.Id = 20;
            this.barBtnZoomIn.ImageIndex = 4;
            this.barBtnZoomIn.Name = "barBtnZoomIn";
            this.barBtnZoomIn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSelectElement_ItemClick);
            // 
            // barBtnZoomOut
            // 
            this.barBtnZoomOut.Caption = "缩小";
            this.barBtnZoomOut.Id = 3;
            this.barBtnZoomOut.ImageIndex = 5;
            this.barBtnZoomOut.Name = "barBtnZoomOut";
            this.barBtnZoomOut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSelectElement_ItemClick);
            // 
            // barBtnPan
            // 
            this.barBtnPan.Caption = "平移";
            this.barBtnPan.Id = 4;
            this.barBtnPan.ImageIndex = 2;
            this.barBtnPan.Name = "barBtnPan";
            this.barBtnPan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSelectElement_ItemClick);
            // 
            // barBtnPreView
            // 
            this.barBtnPreView.Caption = "前一";
            this.barBtnPreView.Id = 5;
            this.barBtnPreView.ImageIndex = 8;
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
            // barSelectFeature
            // 
            this.barSelectFeature.Caption = "选择";
            this.barSelectFeature.Id = 24;
            this.barSelectFeature.ImageIndex = 11;
            this.barSelectFeature.Name = "barSelectFeature";
            this.barSelectFeature.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSelectElement_ItemClick);
            // 
            // barBtnClearSelection
            // 
            this.barBtnClearSelection.Caption = "清除";
            this.barBtnClearSelection.Id = 8;
            this.barBtnClearSelection.ImageIndex = 12;
            this.barBtnClearSelection.Name = "barBtnClearSelection";
            this.barBtnClearSelection.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSelectElement_ItemClick);
            // 
            // barBtnIdentify
            // 
            this.barBtnIdentify.Caption = "查询";
            this.barBtnIdentify.Id = 9;
            this.barBtnIdentify.ImageIndex = 1;
            this.barBtnIdentify.Name = "barBtnIdentify";
            this.barBtnIdentify.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSelectElement_ItemClick);
            // 
            // barBtnZoomToTarget
            // 
            this.barBtnZoomToTarget.Caption = "坐标跳转";
            this.barBtnZoomToTarget.Id = 25;
            this.barBtnZoomToTarget.ImageIndex = 13;
            this.barBtnZoomToTarget.Name = "barBtnZoomToTarget";
            this.barBtnZoomToTarget.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSelectElement_ItemClick);
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
            // axMapControlMainMap
            // 
            this.axMapControlMainMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControlMainMap.Location = new System.Drawing.Point(26, 0);
            this.axMapControlMainMap.Name = "axMapControlMainMap";
            this.axMapControlMainMap.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControlMainMap.OcxState")));
            this.axMapControlMainMap.Size = new System.Drawing.Size(673, 488);
            this.axMapControlMainMap.TabIndex = 1;
            this.axMapControlMainMap.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControlMainMap_OnMouseDown);
            this.axMapControlMainMap.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControlMainMap_OnMouseMove);
            this.axMapControlMainMap.OnDoubleClick += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnDoubleClickEventHandler(this.axMapControlMainMap_OnDoubleClick);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(333, 228);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 13;
            // 
            // MapViewerSimple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axMapControlMainMap);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.Name = "MapViewerSimple";
            this.Size = new System.Drawing.Size(699, 488);
            this.Load += new System.EventHandler(this.MapViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection_nav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerForMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControlMainMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
         
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton sBtnExpend;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
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
        private DevExpress.XtraBars.BarButtonItem barBtnSelectElement;
        private DevExpress.XtraBars.BarButtonItem barBtnFullExtent;
        private DevExpress.XtraBars.BarButtonItem barBtnZoomOut;
        private DevExpress.XtraBars.BarButtonItem barBtnPan;
        private DevExpress.XtraBars.BarButtonItem barBtnPreView;
        private DevExpress.XtraBars.BarButtonItem barBtnClearSelection;
        private DevExpress.XtraBars.BarButtonItem barBtnIdentify;
        private DevExpress.XtraBars.BarButtonItem barBtnZoomIn;
        private DevExpress.XtraBars.BarButtonItem barBtnNextView;
        private DevExpress.XtraBars.BarButtonItem barSelectFeature;
        private DevExpress.XtraBars.BarButtonItem barBtnZoomToTarget;
    }
}
