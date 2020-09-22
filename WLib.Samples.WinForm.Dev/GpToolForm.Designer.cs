namespace WLib.Samples.WinForm.Dev
{
    partial class GpToolForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GpToolForm));
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection_Nav = new DevExpress.Utils.ImageCollection(this.components);
            this.btnRunIntersect = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection_Nav)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(81, 39);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(133, 41);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
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
            // btnRunIntersect
            // 
            this.btnRunIntersect.Location = new System.Drawing.Point(253, 39);
            this.btnRunIntersect.Name = "btnRunIntersect";
            this.btnRunIntersect.Size = new System.Drawing.Size(133, 41);
            this.btnRunIntersect.TabIndex = 0;
            this.btnRunIntersect.Text = "运行相交工具";
            this.btnRunIntersect.Click += new System.EventHandler(this.btnRunIntersect_Click);
            // 
            // GpToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 132);
            this.Controls.Add(this.btnRunIntersect);
            this.Controls.Add(this.btnExport);
            this.Name = "GpToolForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection_Nav)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.Utils.ImageCollection imageCollection_Nav;
        private DevExpress.XtraEditors.SimpleButton btnRunIntersect;
    }
}

