using WLib.WinCtrls.Dev.StyleCtrl.ImageColorful;
using WLib.WinCtrls.Dev.StyleCtrl.Thematic;

namespace WLib.WinCtrls.Dev.StyleCtrl
{
    partial class StyleSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StyleSettingForm));
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.xtraTabPageThematic = new DevExpress.XtraTab.XtraTabPage();
            this.thematicSettingControl = new WLib.WinCtrls.Dev.StyleCtrl.Thematic.ThematicSettingControl();
            this.xtraTabPageIcon = new DevExpress.XtraTab.XtraTabPage();
            this.iconColorfulControl1 = new WLib.WinCtrls.Dev.StyleCtrl.ImageColorful.IamgeColorfulControl();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.xtraTabPageThematic.SuspendLayout();
            this.xtraTabPageIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Images = this.imageCollection1;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPageThematic;
            this.xtraTabControl1.Size = new System.Drawing.Size(445, 381);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageThematic,
            this.xtraTabPageIcon});
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "主题1.png");
            this.imageCollection1.Images.SetKeyName(1, "主题2.png");
            // 
            // xtraTabPageThematic
            // 
            this.xtraTabPageThematic.Controls.Add(this.thematicSettingControl);
            this.xtraTabPageThematic.ImageOptions.ImageIndex = 0;
            this.xtraTabPageThematic.Name = "xtraTabPageThematic";
            this.xtraTabPageThematic.Size = new System.Drawing.Size(439, 350);
            this.xtraTabPageThematic.Text = "主题风格";
            // 
            // skinSettingControl1
            // 
            this.thematicSettingControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.thematicSettingControl.Location = new System.Drawing.Point(0, 0);
            this.thematicSettingControl.Name = "skinSettingControl1";
            this.thematicSettingControl.Size = new System.Drawing.Size(439, 350);
            this.thematicSettingControl.TabIndex = 0;
            // 
            // xtraTabPageIcon
            // 
            this.xtraTabPageIcon.Controls.Add(this.iconColorfulControl1);
            this.xtraTabPageIcon.ImageOptions.ImageIndex = 1;
            this.xtraTabPageIcon.Name = "xtraTabPageIcon";
            this.xtraTabPageIcon.Size = new System.Drawing.Size(441, 300);
            this.xtraTabPageIcon.Text = "图标风格";
            // 
            // iconColorfulControl1
            // 
            this.iconColorfulControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iconColorfulControl1.Location = new System.Drawing.Point(0, 0);
            this.iconColorfulControl1.Name = "iconColorfulControl1";
            this.iconColorfulControl1.Size = new System.Drawing.Size(441, 300);
            this.iconColorfulControl1.TabIndex = 0;
            // 
            // StyleSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 381);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "StyleSettingForm";
            this.Text = "界面风格设置";
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.xtraTabPageThematic.ResumeLayout(false);
            this.xtraTabPageIcon.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageThematic;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageIcon;
        private ThematicSettingControl thematicSettingControl;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private IamgeColorfulControl iconColorfulControl1;
    }
}