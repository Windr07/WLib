namespace WLib.WinCtrls.Dev.GridViewCtrl
{
    partial class DbEditGridView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbEditGridView));
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.navBarCtrl = new DevExpress.XtraNavBar.NavBarControl();
            this.editGridView1 = new WLib.WinCtrls.Dev.GridViewCtrl.EditGridView();
            this.panelCtrlTips = new DevExpress.XtraEditors.PanelControl();
            this.labelControlTips = new DevExpress.XtraEditors.LabelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.imgListBoxPlanColl = new DevExpress.XtraEditors.ImageListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarCtrl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelCtrlTips)).BeginInit();
            this.panelCtrlTips.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgListBoxPlanColl)).BeginInit();
            this.SuspendLayout();
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "item.png");
            this.imageCollection1.Images.SetKeyName(1, "selecteditem.png");
            this.imageCollection1.Images.SetKeyName(2, "entity.png");
            this.imageCollection1.Images.SetKeyName(3, "info.png");
            // 
            // navBarCtrl
            // 
            this.navBarCtrl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.navBarCtrl.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBarCtrl.LinkSelectionMode = DevExpress.XtraNavBar.LinkSelectionModeType.OneInControl;
            this.navBarCtrl.Location = new System.Drawing.Point(0, 0);
            this.navBarCtrl.Margin = new System.Windows.Forms.Padding(6);
            this.navBarCtrl.Name = "navBarCtrl";
            this.navBarCtrl.OptionsNavPane.ExpandedWidth = 292;
            this.navBarCtrl.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBarCtrl.Size = new System.Drawing.Size(292, 781);
            this.navBarCtrl.SmallImages = this.imageCollection1;
            this.navBarCtrl.TabIndex = 15;
            this.navBarCtrl.Text = "navBarControl1";
            this.navBarCtrl.View = new DevExpress.XtraNavBar.ViewInfo.SkinNavigationPaneViewInfoRegistrator();
            this.navBarCtrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NavBarCtrl_MouseDown);
            // 
            // editGridView1
            // 
            this.editGridView1.Connection = null;
            this.editGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editGridView1.Location = new System.Drawing.Point(292, 94);
            this.editGridView1.Margin = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.editGridView1.Name = "editGridView1";
            this.editGridView1.Size = new System.Drawing.Size(1089, 687);
            this.editGridView1.TabIndex = 0;
            // 
            // panelCtrlTips
            // 
            this.panelCtrlTips.Controls.Add(this.labelControlTips);
            this.panelCtrlTips.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCtrlTips.Location = new System.Drawing.Point(292, 0);
            this.panelCtrlTips.Margin = new System.Windows.Forms.Padding(6);
            this.panelCtrlTips.Name = "panelCtrlTips";
            this.panelCtrlTips.Size = new System.Drawing.Size(1089, 94);
            this.panelCtrlTips.TabIndex = 0;
            // 
            // labelControlTips
            // 
            this.labelControlTips.Appearance.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.labelControlTips.Appearance.ImageIndex = 3;
            this.labelControlTips.Appearance.ImageList = this.imageCollection1;
            this.labelControlTips.Appearance.Options.UseImageAlign = true;
            this.labelControlTips.Appearance.Options.UseImageIndex = true;
            this.labelControlTips.Appearance.Options.UseImageList = true;
            this.labelControlTips.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlTips.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControlTips.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.labelControlTips.Location = new System.Drawing.Point(3, 3);
            this.labelControlTips.Margin = new System.Windows.Forms.Padding(6);
            this.labelControlTips.Name = "labelControlTips";
            this.labelControlTips.Size = new System.Drawing.Size(1083, 41);
            this.labelControlTips.TabIndex = 1;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Collapsed = true;
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.imgListBoxPlanColl);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.editGridView1);
            this.splitContainerControl1.Panel2.Controls.Add(this.panelCtrlTips);
            this.splitContainerControl1.Panel2.Controls.Add(this.navBarCtrl);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1391, 781);
            this.splitContainerControl1.SplitterPosition = 192;
            this.splitContainerControl1.TabIndex = 16;
            // 
            // imgListBoxPlanColl
            // 
            this.imgListBoxPlanColl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgListBoxPlanColl.ImageList = this.imageCollection1;
            this.imgListBoxPlanColl.Location = new System.Drawing.Point(0, 0);
            this.imgListBoxPlanColl.Margin = new System.Windows.Forms.Padding(6);
            this.imgListBoxPlanColl.Name = "imgListBoxPlanColl";
            this.imgListBoxPlanColl.Size = new System.Drawing.Size(0, 0);
            this.imgListBoxPlanColl.TabIndex = 1;
            this.imgListBoxPlanColl.SelectedIndexChanged += new System.EventHandler(this.imgListBoxPlanColl_SelectedIndexChanged);
            // 
            // DbEditGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "DbEditGridView";
            this.Size = new System.Drawing.Size(1391, 781);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarCtrl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelCtrlTips)).EndInit();
            this.panelCtrlTips.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgListBoxPlanColl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraNavBar.NavBarControl navBarCtrl;
        private DevExpress.XtraEditors.PanelControl panelCtrlTips;
        private DevExpress.XtraEditors.LabelControl labelControlTips;
        private WLib.WinCtrls.Dev.GridViewCtrl.EditGridView editGridView1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.ImageListBoxControl imgListBoxPlanColl;
    }
}