namespace WLib.WinCtrls.PluginCtrl
{
    partial class PluginForm<TViewEventType, TCmdInputData>
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBarStart = new System.Windows.Forms.ToolStripProgressBar();
            this.lblData = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.HandlerProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblNotification = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblLog = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.progressBarStart,
            this.lblData,
            this.HandlerProgressBar,
            this.lblVersion,
            this.lblNotification,
            this.lblLog});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Image = global::WLib.WinCtrls.Properties.Resources.info_32;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(48, 17);
            this.lblStatus.Text = "就绪";
            // 
            // progressBarStart
            // 
            this.progressBarStart.Name = "progressBarStart";
            this.progressBarStart.Size = new System.Drawing.Size(100, 16);
            this.progressBarStart.Visible = false;
            // 
            // lblData
            // 
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(588, 17);
            this.lblData.Spring = true;
            this.lblData.Text = "未加载数据";
            this.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVersion
            // 
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(53, 17);
            this.lblVersion.Text = "V1.0.0.0";
            // 
            // HandlerProgressBar
            // 
            this.HandlerProgressBar.Name = "HandlerProgressBar";
            this.HandlerProgressBar.Size = new System.Drawing.Size(260, 16);
            this.HandlerProgressBar.Visible = false;
            // 
            // lblNotification
            // 
            this.lblNotification.Image = global::WLib.WinCtrls.Properties.Resources.info16;
            this.lblNotification.Name = "lblNotification";
            this.lblNotification.Size = new System.Drawing.Size(48, 17);
            this.lblNotification.Text = "通知";
            // 
            // lblLog
            // 
            this.lblLog.Image = global::WLib.WinCtrls.Properties.Resources.log;
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(48, 17);
            this.lblLog.Text = "日志";
            // 
            // PluginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PluginForm";
            this.Text = "插件窗体 - PluginView";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar progressBarStart;
        private System.Windows.Forms.ToolStripStatusLabel lblData;
        private System.Windows.Forms.ToolStripStatusLabel lblVersion;
        private System.Windows.Forms.ToolStripProgressBar HandlerProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel lblNotification;
        private System.Windows.Forms.ToolStripStatusLabel lblLog;
    }
}