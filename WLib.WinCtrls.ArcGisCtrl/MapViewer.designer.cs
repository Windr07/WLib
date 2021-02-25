namespace WLib.WinCtrls.ArcGisCtrl
{
    partial class MapViewer
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapViewer));
            this.splitContainerFull = new System.Windows.Forms.SplitContainer();
            this.splitContainerLeft = new System.Windows.Forms.SplitContainer();
            this.TocControl = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.tocGroupControl = new System.Windows.Forms.Panel();
            this.lblTocTips = new System.Windows.Forms.Label();
            this.btnExpand = new System.Windows.Forms.Button();
            this.btnCollapsed = new System.Windows.Forms.Button();
            this.btnSwitchContent = new System.Windows.Forms.Button();
            this.btnAddData = new System.Windows.Forms.Button();
            this.TableListBox = new WLib.WinCtrls.ArcGisCtrl.TableListBox();
            this.EagleMapControl = new ESRI.ArcGIS.Controls.AxMapControl();
            this.ViewerTabControl = new System.Windows.Forms.TabControl();
            this.xtpMapView = new System.Windows.Forms.TabPage();
            this.MapNavigationTools = new WLib.WinCtrls.ArcGisCtrl.MapNavigationTools();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.MainMapControl = new ESRI.ArcGIS.Controls.AxMapControl();
            this.xtpPageLayout = new System.Windows.Forms.TabPage();
            this.PageLayoutControl = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFull)).BeginInit();
            this.splitContainerFull.Panel1.SuspendLayout();
            this.splitContainerFull.Panel2.SuspendLayout();
            this.splitContainerFull.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeft)).BeginInit();
            this.splitContainerLeft.Panel1.SuspendLayout();
            this.splitContainerLeft.Panel2.SuspendLayout();
            this.splitContainerLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TocControl)).BeginInit();
            this.tocGroupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EagleMapControl)).BeginInit();
            this.ViewerTabControl.SuspendLayout();
            this.xtpMapView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainMapControl)).BeginInit();
            this.xtpPageLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PageLayoutControl)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerFull
            // 
            this.splitContainerFull.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerFull.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerFull.Location = new System.Drawing.Point(0, 0);
            this.splitContainerFull.Name = "splitContainerFull";
            // 
            // splitContainerFull.Panel1
            // 
            this.splitContainerFull.Panel1.Controls.Add(this.splitContainerLeft);
            // 
            // splitContainerFull.Panel2
            // 
            this.splitContainerFull.Panel2.Controls.Add(this.ViewerTabControl);
            this.splitContainerFull.Size = new System.Drawing.Size(927, 537);
            this.splitContainerFull.SplitterDistance = 255;
            this.splitContainerFull.TabIndex = 0;
            // 
            // splitContainerLeft
            // 
            this.splitContainerLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerLeft.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerLeft.Location = new System.Drawing.Point(0, 0);
            this.splitContainerLeft.Name = "splitContainerLeft";
            this.splitContainerLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerLeft.Panel1
            // 
            this.splitContainerLeft.Panel1.Controls.Add(this.TocControl);
            this.splitContainerLeft.Panel1.Controls.Add(this.tocGroupControl);
            this.splitContainerLeft.Panel1.Controls.Add(this.TableListBox);
            // 
            // splitContainerLeft.Panel2
            // 
            this.splitContainerLeft.Panel2.Controls.Add(this.EagleMapControl);
            this.splitContainerLeft.Size = new System.Drawing.Size(255, 537);
            this.splitContainerLeft.SplitterDistance = 335;
            this.splitContainerLeft.TabIndex = 0;
            // 
            // TocControl
            // 
            this.TocControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TocControl.Location = new System.Drawing.Point(0, 24);
            this.TocControl.Name = "TocControl";
            this.TocControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("TocControl.OcxState")));
            this.TocControl.Size = new System.Drawing.Size(255, 311);
            this.TocControl.TabIndex = 0;
            // 
            // tocGroupControl
            // 
            this.tocGroupControl.Controls.Add(this.lblTocTips);
            this.tocGroupControl.Controls.Add(this.btnExpand);
            this.tocGroupControl.Controls.Add(this.btnCollapsed);
            this.tocGroupControl.Controls.Add(this.btnSwitchContent);
            this.tocGroupControl.Controls.Add(this.btnAddData);
            this.tocGroupControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tocGroupControl.Location = new System.Drawing.Point(0, 0);
            this.tocGroupControl.Name = "tocGroupControl";
            this.tocGroupControl.Size = new System.Drawing.Size(255, 24);
            this.tocGroupControl.TabIndex = 1;
            // 
            // lblTocTips
            // 
            this.lblTocTips.AutoSize = true;
            this.lblTocTips.Location = new System.Drawing.Point(4, 7);
            this.lblTocTips.Name = "lblTocTips";
            this.lblTocTips.Size = new System.Drawing.Size(53, 12);
            this.lblTocTips.TabIndex = 1;
            this.lblTocTips.Text = "图层列表";
            // 
            // btnExpand
            // 
            this.btnExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExpand.Image = ((System.Drawing.Image)(resources.GetObject("btnExpand.Image")));
            this.btnExpand.Location = new System.Drawing.Point(231, 1);
            this.btnExpand.Name = "btnExpand";
            this.btnExpand.Size = new System.Drawing.Size(22, 22);
            this.btnExpand.TabIndex = 0;
            this.btnExpand.UseVisualStyleBackColor = true;
            this.btnExpand.Click += new System.EventHandler(this.layerControlButtons_Click);
            // 
            // btnCollapsed
            // 
            this.btnCollapsed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCollapsed.Image = ((System.Drawing.Image)(resources.GetObject("btnCollapsed.Image")));
            this.btnCollapsed.Location = new System.Drawing.Point(208, 1);
            this.btnCollapsed.Name = "btnCollapsed";
            this.btnCollapsed.Size = new System.Drawing.Size(22, 22);
            this.btnCollapsed.TabIndex = 0;
            this.btnCollapsed.UseVisualStyleBackColor = true;
            this.btnCollapsed.Click += new System.EventHandler(this.layerControlButtons_Click);
            // 
            // btnSwitchContent
            // 
            this.btnSwitchContent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSwitchContent.Image = ((System.Drawing.Image)(resources.GetObject("btnSwitchContent.Image")));
            this.btnSwitchContent.Location = new System.Drawing.Point(185, 1);
            this.btnSwitchContent.Name = "btnSwitchContent";
            this.btnSwitchContent.Size = new System.Drawing.Size(22, 22);
            this.btnSwitchContent.TabIndex = 0;
            this.btnSwitchContent.UseVisualStyleBackColor = true;
            this.btnSwitchContent.Click += new System.EventHandler(this.layerControlButtons_Click);
            // 
            // btnAddData
            // 
            this.btnAddData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddData.Image = ((System.Drawing.Image)(resources.GetObject("btnAddData.Image")));
            this.btnAddData.Location = new System.Drawing.Point(162, 1);
            this.btnAddData.Name = "btnAddData";
            this.btnAddData.Size = new System.Drawing.Size(22, 22);
            this.btnAddData.TabIndex = 0;
            this.btnAddData.UseVisualStyleBackColor = true;
            this.btnAddData.Click += new System.EventHandler(this.layerControlButtons_Click);
            // 
            // TableListBox
            // 
            this.TableListBox.AttributeForm = null;
            this.TableListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableListBox.Location = new System.Drawing.Point(0, 0);
            this.TableListBox.Name = "TableListBox";
            this.TableListBox.Size = new System.Drawing.Size(255, 335);
            this.TableListBox.TabIndex = 2;
            this.TableListBox.Visible = false;
            // 
            // EagleMapControl
            // 
            this.EagleMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EagleMapControl.Location = new System.Drawing.Point(0, 0);
            this.EagleMapControl.Name = "EagleMapControl";
            this.EagleMapControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("EagleMapControl.OcxState")));
            this.EagleMapControl.Size = new System.Drawing.Size(255, 198);
            this.EagleMapControl.TabIndex = 0;
            // 
            // ViewerTabControl
            // 
            this.ViewerTabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.ViewerTabControl.Controls.Add(this.xtpMapView);
            this.ViewerTabControl.Controls.Add(this.xtpPageLayout);
            this.ViewerTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewerTabControl.Location = new System.Drawing.Point(0, 0);
            this.ViewerTabControl.Name = "ViewerTabControl";
            this.ViewerTabControl.SelectedIndex = 0;
            this.ViewerTabControl.Size = new System.Drawing.Size(668, 537);
            this.ViewerTabControl.TabIndex = 0;
            // 
            // xtpMapView
            // 
            this.xtpMapView.BackColor = System.Drawing.Color.Transparent;
            this.xtpMapView.Controls.Add(this.MapNavigationTools);
            this.xtpMapView.Controls.Add(this.axLicenseControl1);
            this.xtpMapView.Controls.Add(this.MainMapControl);
            this.xtpMapView.Location = new System.Drawing.Point(4, 4);
            this.xtpMapView.Name = "xtpMapView";
            this.xtpMapView.Padding = new System.Windows.Forms.Padding(3);
            this.xtpMapView.Size = new System.Drawing.Size(660, 511);
            this.xtpMapView.TabIndex = 0;
            this.xtpMapView.Text = "地图";
            // 
            // MapNavigationTools
            // 
            this.MapNavigationTools.BackColor = System.Drawing.Color.Transparent;
            this.MapNavigationTools.CurrentTool = WLib.ArcGis.Control.MapAssociation.EMapTools.None;
            this.MapNavigationTools.Location = new System.Drawing.Point(6, 6);
            this.MapNavigationTools.MapControl = null;
            this.MapNavigationTools.Name = "MapNavigationTools";
            this.MapNavigationTools.Size = new System.Drawing.Size(252, 24);
            this.MapNavigationTools.TabIndex = 3;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(167, 207);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 2;
            // 
            // MainMapControl
            // 
            this.MainMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainMapControl.Location = new System.Drawing.Point(3, 3);
            this.MainMapControl.Name = "MainMapControl";
            this.MainMapControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MainMapControl.OcxState")));
            this.MainMapControl.Size = new System.Drawing.Size(654, 505);
            this.MainMapControl.TabIndex = 0;
            // 
            // xtpPageLayout
            // 
            this.xtpPageLayout.Controls.Add(this.PageLayoutControl);
            this.xtpPageLayout.Location = new System.Drawing.Point(4, 4);
            this.xtpPageLayout.Name = "xtpPageLayout";
            this.xtpPageLayout.Padding = new System.Windows.Forms.Padding(3);
            this.xtpPageLayout.Size = new System.Drawing.Size(660, 511);
            this.xtpPageLayout.TabIndex = 1;
            this.xtpPageLayout.Text = "页面";
            this.xtpPageLayout.UseVisualStyleBackColor = true;
            // 
            // PageLayoutControl
            // 
            this.PageLayoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PageLayoutControl.Location = new System.Drawing.Point(3, 3);
            this.PageLayoutControl.Name = "PageLayoutControl";
            this.PageLayoutControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("PageLayoutControl.OcxState")));
            this.PageLayoutControl.Size = new System.Drawing.Size(654, 505);
            this.PageLayoutControl.TabIndex = 0;
            // 
            // MapViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerFull);
            this.Name = "MapViewer";
            this.Size = new System.Drawing.Size(927, 537);
            this.splitContainerFull.Panel1.ResumeLayout(false);
            this.splitContainerFull.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFull)).EndInit();
            this.splitContainerFull.ResumeLayout(false);
            this.splitContainerLeft.Panel1.ResumeLayout(false);
            this.splitContainerLeft.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeft)).EndInit();
            this.splitContainerLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TocControl)).EndInit();
            this.tocGroupControl.ResumeLayout(false);
            this.tocGroupControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EagleMapControl)).EndInit();
            this.ViewerTabControl.ResumeLayout(false);
            this.xtpMapView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainMapControl)).EndInit();
            this.xtpPageLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PageLayoutControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerFull;
        private System.Windows.Forms.SplitContainer splitContainerLeft;
        private System.Windows.Forms.Panel tocGroupControl;
        private System.Windows.Forms.TabControl ViewerTabControl;
        private System.Windows.Forms.TabPage xtpMapView;
        private System.Windows.Forms.TabPage xtpPageLayout;
        private ESRI.ArcGIS.Controls.AxPageLayoutControl PageLayoutControl;
        private System.Windows.Forms.Button btnAddData;
        private System.Windows.Forms.Button btnSwitchContent;
        private System.Windows.Forms.Button btnExpand;
        private System.Windows.Forms.Button btnCollapsed;
        public ESRI.ArcGIS.Controls.AxTOCControl TocControl;
        public ESRI.ArcGIS.Controls.AxMapControl EagleMapControl;
        public ESRI.ArcGIS.Controls.AxMapControl MainMapControl;
        public TableListBox TableListBox;
        private System.Windows.Forms.Label lblTocTips;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        public MapNavigationTools MapNavigationTools;
    }
}
