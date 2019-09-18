namespace WLib.WinCtrls.Dev.ChildCtrl
{
    partial class ChildSizableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChildSizableForm));
            this.imageCollections = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollections)).BeginInit();
            this.SuspendLayout();
            // 
            // imageCollections
            // 
            this.imageCollections.ImageSize = new System.Drawing.Size(28, 16);
            this.imageCollections.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollections.ImageStream")));
            this.imageCollections.Images.SetKeyName(0, "关闭.png");
            this.imageCollections.Images.SetKeyName(1, "最大化.png");
            this.imageCollections.Images.SetKeyName(2, "最小化.png");
            // 
            // ChildSizableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(557, 306);
            this.MaximizeBox = false;
            this.Name = "ChildSizableForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Resize += new System.EventHandler(this.ChildForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollections)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ImageCollection imageCollections;
    }
}