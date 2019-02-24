namespace WLib.UserCtrls.Dev.ArcGisCtrl
{
    partial class PageNavigationTools
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageNavigationTools));
            this.grpPageNav = new DevExpress.XtraEditors.GroupControl();
            this.panPageLayout = new DevExpress.XtraEditors.SimpleButton();
            this.zoomOutPageLayout = new DevExpress.XtraEditors.SimpleButton();
            this.nextViewPageLayout = new DevExpress.XtraEditors.SimpleButton();
            this.preViewPageLayout = new DevExpress.XtraEditors.SimpleButton();
            this.zoomInPageLayout = new DevExpress.XtraEditors.SimpleButton();
            this.fullExtentPageLayout = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection_Nav = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grpPageNav)).BeginInit();
            this.grpPageNav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection_Nav)).BeginInit();
            this.SuspendLayout();
            // 
            // grpPageNav
            // 
            this.grpPageNav.Controls.Add(this.panPageLayout);
            this.grpPageNav.Controls.Add(this.zoomOutPageLayout);
            this.grpPageNav.Controls.Add(this.nextViewPageLayout);
            this.grpPageNav.Controls.Add(this.preViewPageLayout);
            this.grpPageNav.Controls.Add(this.zoomInPageLayout);
            this.grpPageNav.Controls.Add(this.fullExtentPageLayout);
            this.grpPageNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPageNav.Location = new System.Drawing.Point(0, 0);
            this.grpPageNav.Name = "grpPageNav";
            this.grpPageNav.Size = new System.Drawing.Size(147, 49);
            this.grpPageNav.TabIndex = 9;
            this.grpPageNav.Text = "地图导航条";
            // 
            // panPageLayout
            // 
            this.panPageLayout.ImageList = this.imageCollection_Nav;
            this.panPageLayout.ImageIndex = 6;
            this.panPageLayout.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.panPageLayout.Location = new System.Drawing.Point(74, 23);
            this.panPageLayout.Margin = new System.Windows.Forms.Padding(0);
            this.panPageLayout.Name = "panPageLayout";
            this.panPageLayout.Size = new System.Drawing.Size(24, 24);
            this.panPageLayout.TabIndex = 0;
            this.panPageLayout.ToolTip = "平移";
            this.panPageLayout.Click += new System.EventHandler(this.pageLayoutNavigationBtn_Click);
            // 
            // zoomOutPageLayout
            // 
            this.zoomOutPageLayout.ImageList = this.imageCollection_Nav;
            this.zoomOutPageLayout.ImageIndex = 14;
            this.zoomOutPageLayout.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.zoomOutPageLayout.Location = new System.Drawing.Point(50, 23);
            this.zoomOutPageLayout.Margin = new System.Windows.Forms.Padding(0);
            this.zoomOutPageLayout.Name = "zoomOutPageLayout";
            this.zoomOutPageLayout.Size = new System.Drawing.Size(24, 24);
            this.zoomOutPageLayout.TabIndex = 0;
            this.zoomOutPageLayout.ToolTip = "缩小";
            this.zoomOutPageLayout.Click += new System.EventHandler(this.pageLayoutNavigationBtn_Click);
            // 
            // nextViewPageLayout
            // 
            this.nextViewPageLayout.ImageList = this.imageCollection_Nav;
            this.nextViewPageLayout.ImageIndex = 8;
            this.nextViewPageLayout.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.nextViewPageLayout.Location = new System.Drawing.Point(122, 23);
            this.nextViewPageLayout.Margin = new System.Windows.Forms.Padding(0);
            this.nextViewPageLayout.Name = "nextViewPageLayout";
            this.nextViewPageLayout.Size = new System.Drawing.Size(24, 24);
            this.nextViewPageLayout.TabIndex = 0;
            this.nextViewPageLayout.ToolTip = "后一视图";
            this.nextViewPageLayout.Click += new System.EventHandler(this.pageLayoutNavigationBtn_Click);
            // 
            // preViewPageLayout
            // 
            this.preViewPageLayout.ImageList = this.imageCollection_Nav;
            this.preViewPageLayout.ImageIndex = 3;
            this.preViewPageLayout.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.preViewPageLayout.Location = new System.Drawing.Point(98, 23);
            this.preViewPageLayout.Margin = new System.Windows.Forms.Padding(0);
            this.preViewPageLayout.Name = "preViewPageLayout";
            this.preViewPageLayout.Size = new System.Drawing.Size(24, 24);
            this.preViewPageLayout.TabIndex = 0;
            this.preViewPageLayout.ToolTip = "前一视图";
            this.preViewPageLayout.Click += new System.EventHandler(this.pageLayoutNavigationBtn_Click);
            // 
            // zoomInPageLayout
            // 
            this.zoomInPageLayout.ImageList = this.imageCollection_Nav;
            this.zoomInPageLayout.ImageIndex = 12;
            this.zoomInPageLayout.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.zoomInPageLayout.Location = new System.Drawing.Point(26, 23);
            this.zoomInPageLayout.Margin = new System.Windows.Forms.Padding(0);
            this.zoomInPageLayout.Name = "zoomInPageLayout";
            this.zoomInPageLayout.Size = new System.Drawing.Size(24, 24);
            this.zoomInPageLayout.TabIndex = 0;
            this.zoomInPageLayout.ToolTip = "放大";
            this.zoomInPageLayout.Click += new System.EventHandler(this.pageLayoutNavigationBtn_Click);
            // 
            // fullExtentPageLayout
            // 
            this.fullExtentPageLayout.ImageList = this.imageCollection_Nav;
            this.fullExtentPageLayout.ImageIndex = 10;
            this.fullExtentPageLayout.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.fullExtentPageLayout.Location = new System.Drawing.Point(2, 23);
            this.fullExtentPageLayout.Margin = new System.Windows.Forms.Padding(0);
            this.fullExtentPageLayout.Name = "fullExtentPageLayout";
            this.fullExtentPageLayout.Size = new System.Drawing.Size(24, 24);
            this.fullExtentPageLayout.TabIndex = 0;
            this.fullExtentPageLayout.ToolTip = "全屏";
            this.fullExtentPageLayout.Click += new System.EventHandler(this.pageLayoutNavigationBtn_Click);
            // 
            // imageCollection_Nav
            // 
            this.imageCollection_Nav.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection_Nav.ImageStream")));
            this.imageCollection_Nav.Images.SetKeyName(0, "Nav_Area_16.png");
            this.imageCollection_Nav.Images.SetKeyName(1, "Nav_Identify_16.png");
            this.imageCollection_Nav.Images.SetKeyName(2, "Nav_Identify_32.png");
            this.imageCollection_Nav.Images.SetKeyName(3, "Nav_Left_16.png");
            this.imageCollection_Nav.Images.SetKeyName(4, "Nav_Left_32.png");
            this.imageCollection_Nav.Images.SetKeyName(5, "Nav_Length_16.png");
            this.imageCollection_Nav.Images.SetKeyName(6, "Nav_Pan_16.png");
            this.imageCollection_Nav.Images.SetKeyName(7, "Nav_Pan_32.png");
            this.imageCollection_Nav.Images.SetKeyName(8, "Nav_Right_16.png");
            this.imageCollection_Nav.Images.SetKeyName(9, "Nav_Right_32.png");
            this.imageCollection_Nav.Images.SetKeyName(10, "Nav_ZoomFull_16.png");
            this.imageCollection_Nav.Images.SetKeyName(11, "Nav_ZoomFull_32.png");
            this.imageCollection_Nav.Images.SetKeyName(12, "Nav_ZoomIn_16.png");
            this.imageCollection_Nav.Images.SetKeyName(13, "Nav_ZoomIn_32.png");
            this.imageCollection_Nav.Images.SetKeyName(14, "Nav_ZoomOut_16.png");
            this.imageCollection_Nav.Images.SetKeyName(15, "Nav_ZoomOut_32.png");
            this.imageCollection_Nav.Images.SetKeyName(16, "Nav_Close_16.png");
            this.imageCollection_Nav.Images.SetKeyName(17, "table2.png");
            this.imageCollection_Nav.Images.SetKeyName(18, "EffectsSwipe16.png");
            this.imageCollection_Nav.Images.SetKeyName(19, "EffectsSwipe32.png");
            this.imageCollection_Nav.Images.SetKeyName(20, "SelectionSelectPolygonTool16.png");
            this.imageCollection_Nav.Images.SetKeyName(21, "SelectionSelectTool16.png");
            // 
            // PageNavigationTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpPageNav);
            this.Name = "PageNavigationTools";
            this.Size = new System.Drawing.Size(147, 49);
            ((System.ComponentModel.ISupportInitialize)(this.grpPageNav)).EndInit();
            this.grpPageNav.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection_Nav)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpPageNav;
        private DevExpress.XtraEditors.SimpleButton panPageLayout;
        private DevExpress.XtraEditors.SimpleButton zoomOutPageLayout;
        private DevExpress.XtraEditors.SimpleButton nextViewPageLayout;
        private DevExpress.XtraEditors.SimpleButton preViewPageLayout;
        private DevExpress.XtraEditors.SimpleButton zoomInPageLayout;
        private DevExpress.XtraEditors.SimpleButton fullExtentPageLayout;
        private DevExpress.Utils.ImageCollection imageCollection_Nav;
    }
}
