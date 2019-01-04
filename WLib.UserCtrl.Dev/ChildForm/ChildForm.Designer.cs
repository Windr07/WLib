namespace WLib.UserCtrls.Dev.ChildForm
{
    partial class ChildForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChildForm));
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollections = new DevExpress.Utils.ImageCollection();
            this.topBaner = new System.Windows.Forms.Panel();
            this.btnExpant = new DevExpress.XtraEditors.SimpleButton();
            this.picIcon = new System.Windows.Forms.Panel();
            this.lbTitle = new System.Windows.Forms.Label();
            this.rBorder = new System.Windows.Forms.Panel();
            this.bBorder = new System.Windows.Forms.Panel();
            this.lBorder = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollections)).BeginInit();
            this.topBaner.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.ImageIndex = 0;
            this.btnClose.ImageList = this.imageCollections;
            this.btnClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnClose.Location = new System.Drawing.Point(526, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(27, 15);
            this.btnClose.TabIndex = 9;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // imageCollections
            // 
            this.imageCollections.ImageSize = new System.Drawing.Size(28, 16);
            this.imageCollections.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollections.ImageStream")));
            this.imageCollections.Images.SetKeyName(0, "关闭.png");
            this.imageCollections.Images.SetKeyName(1, "最大化.png");
            this.imageCollections.Images.SetKeyName(2, "最小化.png");
            // 
            // topBaner
            // 
            this.topBaner.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.topBaner.Controls.Add(this.btnExpant);
            this.topBaner.Controls.Add(this.picIcon);
            this.topBaner.Controls.Add(this.lbTitle);
            this.topBaner.Controls.Add(this.btnClose);
            this.topBaner.Dock = System.Windows.Forms.DockStyle.Top;
            this.topBaner.Location = new System.Drawing.Point(0, 0);
            this.topBaner.Name = "topBaner";
            this.topBaner.Size = new System.Drawing.Size(557, 26);
            this.topBaner.TabIndex = 10;
            this.topBaner.Paint += new System.Windows.Forms.PaintEventHandler(this.topBaner_Paint);
            this.topBaner.DoubleClick += new System.EventHandler(this.topBaner_DoubleClick);
            this.topBaner.MouseDown += new System.Windows.Forms.MouseEventHandler(this.topBaner_MouseDown);
            // 
            // btnExpant
            // 
            this.btnExpant.AllowFocus = false;
            this.btnExpant.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExpant.ImageIndex = 2;
            this.btnExpant.ImageList = this.imageCollections;
            this.btnExpant.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExpant.Location = new System.Drawing.Point(492, 5);
            this.btnExpant.Name = "btnExpant";
            this.btnExpant.Size = new System.Drawing.Size(27, 15);
            this.btnExpant.TabIndex = 12;
            this.btnExpant.Click += new System.EventHandler(this.btnExpant_Click);
            // 
            // picIcon
            // 
            this.picIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picIcon.Location = new System.Drawing.Point(6, 3);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(20, 20);
            this.picIcon.TabIndex = 11;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Location = new System.Drawing.Point(31, 5);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(31, 14);
            this.lbTitle.TabIndex = 11;
            this.lbTitle.Text = "标题";
            this.lbTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbTitle_MouseDown);
            // 
            // rBorder
            // 
            this.rBorder.Dock = System.Windows.Forms.DockStyle.Right;
            this.rBorder.Location = new System.Drawing.Point(554, 26);
            this.rBorder.Name = "rBorder";
            this.rBorder.Size = new System.Drawing.Size(3, 280);
            this.rBorder.TabIndex = 11;
            this.rBorder.Paint += new System.Windows.Forms.PaintEventHandler(this.rBorder_Paint);
            this.rBorder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rBorder_MouseDown);
            this.rBorder.MouseLeave += new System.EventHandler(this.rBorder_MouseLeave);
            this.rBorder.MouseMove += new System.Windows.Forms.MouseEventHandler(this.rBorder_MouseMove);
            this.rBorder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rBorder_MouseUp);
            // 
            // bBorder
            // 
            this.bBorder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bBorder.Location = new System.Drawing.Point(0, 303);
            this.bBorder.Name = "bBorder";
            this.bBorder.Size = new System.Drawing.Size(554, 3);
            this.bBorder.TabIndex = 12;
            this.bBorder.Paint += new System.Windows.Forms.PaintEventHandler(this.bBorder_Paint);
            this.bBorder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bBorder_MouseDown);
            this.bBorder.MouseLeave += new System.EventHandler(this.bBorder_MouseLeave);
            this.bBorder.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bBorder_MouseMove);
            this.bBorder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bBorder_MouseUp);
            // 
            // lBorder
            // 
            this.lBorder.Dock = System.Windows.Forms.DockStyle.Left;
            this.lBorder.Location = new System.Drawing.Point(0, 26);
            this.lBorder.Name = "lBorder";
            this.lBorder.Size = new System.Drawing.Size(3, 277);
            this.lBorder.TabIndex = 13;
            this.lBorder.Paint += new System.Windows.Forms.PaintEventHandler(this.lBorder_Paint);
            // 
            // ChildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(557, 306);
            this.Controls.Add(this.lBorder);
            this.Controls.Add(this.bBorder);
            this.Controls.Add(this.rBorder);
            this.Controls.Add(this.topBaner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ChildForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.ChildForm_Load);
            this.Resize += new System.EventHandler(this.ChildForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollections)).EndInit();
            this.topBaner.ResumeLayout(false);
            this.topBaner.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnClose;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Panel picIcon;
        private DevExpress.XtraEditors.SimpleButton btnExpant;
        private DevExpress.Utils.ImageCollection imageCollections;
        private System.Windows.Forms.Panel rBorder;
        private System.Windows.Forms.Panel bBorder;
        private System.Windows.Forms.Panel lBorder;
        protected System.Windows.Forms.Panel topBaner;
    }
}