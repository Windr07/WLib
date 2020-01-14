namespace WLib.WinCtrls.Dev.PluginCtrl
{
    partial class PluginRibbonForm
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
            this.ribbonCtrl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonCtrl)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonCtrl.ApplicationButtonText = null;
            // 
            // 
            // 
            this.ribbonCtrl.ExpandCollapseItem.Id = 0;
            this.ribbonCtrl.ExpandCollapseItem.Name = "";
            this.ribbonCtrl.ExpandCollapseItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            this.ribbonCtrl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonCtrl.ExpandCollapseItem});
            this.ribbonCtrl.Location = new System.Drawing.Point(0, 0);
            this.ribbonCtrl.MaxItemId = 1;
            this.ribbonCtrl.Name = "ribbonControl1";
            this.ribbonCtrl.Size = new System.Drawing.Size(713, 49);
            // 
            // PluginRibbonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 389);
            this.Controls.Add(this.ribbonCtrl);
            this.Name = "PluginRibbonForm";
            this.Ribbon = this.ribbonCtrl;
            this.Text = "插件窗体 - PluginView";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonCtrl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public DevExpress.XtraBars.Ribbon.RibbonControl ribbonCtrl;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
    }
}