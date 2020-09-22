namespace WLib.WinCtrls.ArcGisCtrl
{
    partial class MapNavigationTools
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapNavigationTools));
            this.btnFullExtent = new System.Windows.Forms.Button();
            this.imageCollection_Nav = new System.Windows.Forms.ImageList(this.components);
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnPan = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnAreaMeasure = new System.Windows.Forms.Button();
            this.btnLenMeasure = new System.Windows.Forms.Button();
            this.btnNextView = new System.Windows.Forms.Button();
            this.btnPreView = new System.Windows.Forms.Button();
            this.btnSelection = new System.Windows.Forms.Button();
            this.btnIdentify = new System.Windows.Forms.Button();
            this.btnSwipe = new System.Windows.Forms.Button();
            this.lblMeasureTips = new System.Windows.Forms.Label();
            this.lblMeasureInfo = new System.Windows.Forms.Label();
            this.lblSwipe = new System.Windows.Forms.Label();
            this.cmbLayers = new System.Windows.Forms.ComboBox();
            this.btnMeasureClose = new System.Windows.Forms.Button();
            this.grpMapNav = new System.Windows.Forms.Panel();
            this.grpMapNav.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.ImageIndex = 13;
            this.btnFullExtent.ImageList = this.imageCollection_Nav;
            this.btnFullExtent.Location = new System.Drawing.Point(-1, 0);
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.Size = new System.Drawing.Size(24, 24);
            this.btnFullExtent.TabIndex = 0;
            this.btnFullExtent.Tag = "全图";
            this.btnFullExtent.UseVisualStyleBackColor = true;
            this.btnFullExtent.Click += new System.EventHandler(this.navigationButton_Click);
            // 
            // imageCollection_Nav
            // 
            this.imageCollection_Nav.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageCollection_Nav.ImageStream")));
            this.imageCollection_Nav.TransparentColor = System.Drawing.Color.Transparent;
            this.imageCollection_Nav.Images.SetKeyName(0, "EffectsSwipe16.png");
            this.imageCollection_Nav.Images.SetKeyName(1, "EffectsSwipe32.png");
            this.imageCollection_Nav.Images.SetKeyName(2, "Nav_Area_16.png");
            this.imageCollection_Nav.Images.SetKeyName(3, "Nav_Close_16.png");
            this.imageCollection_Nav.Images.SetKeyName(4, "Nav_Identify_16.png");
            this.imageCollection_Nav.Images.SetKeyName(5, "Nav_Identify_32.png");
            this.imageCollection_Nav.Images.SetKeyName(6, "Nav_Left_16.png");
            this.imageCollection_Nav.Images.SetKeyName(7, "Nav_Left_32.png");
            this.imageCollection_Nav.Images.SetKeyName(8, "Nav_Length_16.png");
            this.imageCollection_Nav.Images.SetKeyName(9, "Nav_Pan_16.png");
            this.imageCollection_Nav.Images.SetKeyName(10, "Nav_Pan_32.png");
            this.imageCollection_Nav.Images.SetKeyName(11, "Nav_Right_16.png");
            this.imageCollection_Nav.Images.SetKeyName(12, "Nav_Right_32.png");
            this.imageCollection_Nav.Images.SetKeyName(13, "Nav_ZoomFull_16.png");
            this.imageCollection_Nav.Images.SetKeyName(14, "Nav_ZoomFull_32.png");
            this.imageCollection_Nav.Images.SetKeyName(15, "Nav_ZoomIn_16.png");
            this.imageCollection_Nav.Images.SetKeyName(16, "Nav_ZoomIn_32.png");
            this.imageCollection_Nav.Images.SetKeyName(17, "Nav_ZoomOut_16.png");
            this.imageCollection_Nav.Images.SetKeyName(18, "Nav_ZoomOut_32.png");
            this.imageCollection_Nav.Images.SetKeyName(19, "SelectionSelectPolygonTool16.png");
            this.imageCollection_Nav.Images.SetKeyName(20, "SelectionSelectTool16.png");
            this.imageCollection_Nav.Images.SetKeyName(21, "table2.png");
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.ImageIndex = 15;
            this.btnZoomIn.ImageList = this.imageCollection_Nav;
            this.btnZoomIn.Location = new System.Drawing.Point(22, 0);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(24, 24);
            this.btnZoomIn.TabIndex = 1;
            this.btnZoomIn.Tag = "放大";
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.navigationButton_Click);
            // 
            // btnPan
            // 
            this.btnPan.ImageIndex = 9;
            this.btnPan.ImageList = this.imageCollection_Nav;
            this.btnPan.Location = new System.Drawing.Point(68, 0);
            this.btnPan.Name = "btnPan";
            this.btnPan.Size = new System.Drawing.Size(24, 24);
            this.btnPan.TabIndex = 3;
            this.btnPan.Tag = "平移";
            this.btnPan.UseVisualStyleBackColor = true;
            this.btnPan.Click += new System.EventHandler(this.navigationButton_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.ImageIndex = 17;
            this.btnZoomOut.ImageList = this.imageCollection_Nav;
            this.btnZoomOut.Location = new System.Drawing.Point(45, 0);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(24, 24);
            this.btnZoomOut.TabIndex = 2;
            this.btnZoomOut.Tag = "缩小";
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.navigationButton_Click);
            // 
            // btnAreaMeasure
            // 
            this.btnAreaMeasure.ImageIndex = 2;
            this.btnAreaMeasure.ImageList = this.imageCollection_Nav;
            this.btnAreaMeasure.Location = new System.Drawing.Point(160, 0);
            this.btnAreaMeasure.Name = "btnAreaMeasure";
            this.btnAreaMeasure.Size = new System.Drawing.Size(24, 24);
            this.btnAreaMeasure.TabIndex = 7;
            this.btnAreaMeasure.Tag = "测量面积";
            this.btnAreaMeasure.UseVisualStyleBackColor = true;
            this.btnAreaMeasure.Click += new System.EventHandler(this.navigationButton_Click);
            // 
            // btnLenMeasure
            // 
            this.btnLenMeasure.ImageIndex = 8;
            this.btnLenMeasure.ImageList = this.imageCollection_Nav;
            this.btnLenMeasure.Location = new System.Drawing.Point(137, 0);
            this.btnLenMeasure.Name = "btnLenMeasure";
            this.btnLenMeasure.Size = new System.Drawing.Size(24, 24);
            this.btnLenMeasure.TabIndex = 6;
            this.btnLenMeasure.Tag = "测量距离";
            this.btnLenMeasure.UseVisualStyleBackColor = true;
            this.btnLenMeasure.Click += new System.EventHandler(this.navigationButton_Click);
            // 
            // btnNextView
            // 
            this.btnNextView.ImageIndex = 11;
            this.btnNextView.ImageList = this.imageCollection_Nav;
            this.btnNextView.Location = new System.Drawing.Point(114, 0);
            this.btnNextView.Name = "btnNextView";
            this.btnNextView.Size = new System.Drawing.Size(24, 24);
            this.btnNextView.TabIndex = 5;
            this.btnNextView.Tag = "后一视图";
            this.btnNextView.UseVisualStyleBackColor = true;
            this.btnNextView.Click += new System.EventHandler(this.navigationButton_Click);
            // 
            // btnPreView
            // 
            this.btnPreView.ImageIndex = 6;
            this.btnPreView.ImageList = this.imageCollection_Nav;
            this.btnPreView.Location = new System.Drawing.Point(91, 0);
            this.btnPreView.Name = "btnPreView";
            this.btnPreView.Size = new System.Drawing.Size(24, 24);
            this.btnPreView.TabIndex = 4;
            this.btnPreView.Tag = "前一视图";
            this.btnPreView.UseVisualStyleBackColor = true;
            this.btnPreView.Click += new System.EventHandler(this.navigationButton_Click);
            // 
            // btnSelection
            // 
            this.btnSelection.ImageIndex = 19;
            this.btnSelection.ImageList = this.imageCollection_Nav;
            this.btnSelection.Location = new System.Drawing.Point(229, 0);
            this.btnSelection.Name = "btnSelection";
            this.btnSelection.Size = new System.Drawing.Size(24, 24);
            this.btnSelection.TabIndex = 10;
            this.btnSelection.Tag = "选择图斑";
            this.btnSelection.UseVisualStyleBackColor = true;
            this.btnSelection.Click += new System.EventHandler(this.navigationButton_Click);
            // 
            // btnIdentify
            // 
            this.btnIdentify.ImageIndex = 4;
            this.btnIdentify.ImageList = this.imageCollection_Nav;
            this.btnIdentify.Location = new System.Drawing.Point(206, 0);
            this.btnIdentify.Name = "btnIdentify";
            this.btnIdentify.Size = new System.Drawing.Size(24, 24);
            this.btnIdentify.TabIndex = 9;
            this.btnIdentify.Tag = "识别";
            this.btnIdentify.UseVisualStyleBackColor = true;
            this.btnIdentify.Click += new System.EventHandler(this.navigationButton_Click);
            // 
            // btnSwipe
            // 
            this.btnSwipe.ImageIndex = 0;
            this.btnSwipe.ImageList = this.imageCollection_Nav;
            this.btnSwipe.Location = new System.Drawing.Point(183, 0);
            this.btnSwipe.Name = "btnSwipe";
            this.btnSwipe.Size = new System.Drawing.Size(24, 24);
            this.btnSwipe.TabIndex = 8;
            this.btnSwipe.Tag = "卷帘";
            this.btnSwipe.UseVisualStyleBackColor = true;
            this.btnSwipe.Click += new System.EventHandler(this.navigationButton_Click);
            // 
            // lblMeasureTips
            // 
            this.lblMeasureTips.AutoSize = true;
            this.lblMeasureTips.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblMeasureTips.Location = new System.Drawing.Point(3, 27);
            this.lblMeasureTips.Name = "lblMeasureTips";
            this.lblMeasureTips.Size = new System.Drawing.Size(221, 12);
            this.lblMeasureTips.TabIndex = 11;
            this.lblMeasureTips.Text = "提示：请点击地图，左键量算，右键结束";
            // 
            // lblMeasureInfo
            // 
            this.lblMeasureInfo.AutoSize = true;
            this.lblMeasureInfo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblMeasureInfo.ForeColor = System.Drawing.Color.Fuchsia;
            this.lblMeasureInfo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMeasureInfo.Location = new System.Drawing.Point(6, 42);
            this.lblMeasureInfo.Name = "lblMeasureInfo";
            this.lblMeasureInfo.Size = new System.Drawing.Size(65, 12);
            this.lblMeasureInfo.TabIndex = 12;
            this.lblMeasureInfo.Text = "量算结果：";
            this.lblMeasureInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSwipe
            // 
            this.lblSwipe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblSwipe.Location = new System.Drawing.Point(3, 25);
            this.lblSwipe.Name = "lblSwipe";
            this.lblSwipe.Size = new System.Drawing.Size(60, 14);
            this.lblSwipe.TabIndex = 13;
            this.lblSwipe.Text = "卷帘图层";
            this.lblSwipe.Visible = false;
            // 
            // cmbLayers
            // 
            this.cmbLayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLayers.FormattingEnabled = true;
            this.cmbLayers.Location = new System.Drawing.Point(69, 25);
            this.cmbLayers.Name = "cmbLayers";
            this.cmbLayers.Size = new System.Drawing.Size(182, 20);
            this.cmbLayers.TabIndex = 14;
            this.cmbLayers.SelectedIndexChanged += new System.EventHandler(this.cmbLayers_SelectedIndexChanged);
            // 
            // btnMeasureClose
            // 
            this.btnMeasureClose.ImageIndex = 3;
            this.btnMeasureClose.ImageList = this.imageCollection_Nav;
            this.btnMeasureClose.Location = new System.Drawing.Point(223, 25);
            this.btnMeasureClose.Name = "btnMeasureClose";
            this.btnMeasureClose.Size = new System.Drawing.Size(16, 16);
            this.btnMeasureClose.TabIndex = 15;
            this.btnMeasureClose.UseVisualStyleBackColor = true;
            this.btnMeasureClose.Visible = false;
            this.btnMeasureClose.Click += new System.EventHandler(this.btnMeasureClose_Click);
            // 
            // grpMapNav
            // 
            this.grpMapNav.Controls.Add(this.btnFullExtent);
            this.grpMapNav.Controls.Add(this.btnZoomIn);
            this.grpMapNav.Controls.Add(this.btnZoomOut);
            this.grpMapNav.Controls.Add(this.btnPan);
            this.grpMapNav.Controls.Add(this.btnSelection);
            this.grpMapNav.Controls.Add(this.btnPreView);
            this.grpMapNav.Controls.Add(this.btnIdentify);
            this.grpMapNav.Controls.Add(this.btnNextView);
            this.grpMapNav.Controls.Add(this.btnSwipe);
            this.grpMapNav.Controls.Add(this.btnLenMeasure);
            this.grpMapNav.Controls.Add(this.btnAreaMeasure);
            this.grpMapNav.Location = new System.Drawing.Point(0, -1);
            this.grpMapNav.Name = "grpMapNav";
            this.grpMapNav.Size = new System.Drawing.Size(253, 24);
            this.grpMapNav.TabIndex = 16;
            // 
            // MapNavigationTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.grpMapNav);
            this.Controls.Add(this.btnMeasureClose);
            this.Controls.Add(this.lblMeasureInfo);
            this.Controls.Add(this.lblMeasureTips);
            this.Controls.Add(this.lblSwipe);
            this.Controls.Add(this.cmbLayers);
            this.Name = "MapNavigationTools";
            this.Size = new System.Drawing.Size(252, 56);
            this.grpMapNav.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFullExtent;
        private System.Windows.Forms.ImageList imageCollection_Nav;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Button btnPan;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnAreaMeasure;
        private System.Windows.Forms.Button btnLenMeasure;
        private System.Windows.Forms.Button btnNextView;
        private System.Windows.Forms.Button btnPreView;
        private System.Windows.Forms.Button btnSelection;
        private System.Windows.Forms.Button btnIdentify;
        private System.Windows.Forms.Button btnSwipe;
        private System.Windows.Forms.Label lblMeasureTips;
        private System.Windows.Forms.Label lblMeasureInfo;
        private System.Windows.Forms.Label lblSwipe;
        private System.Windows.Forms.ComboBox cmbLayers;
        private System.Windows.Forms.Button btnMeasureClose;
        private System.Windows.Forms.Panel grpMapNav;
    }
}
