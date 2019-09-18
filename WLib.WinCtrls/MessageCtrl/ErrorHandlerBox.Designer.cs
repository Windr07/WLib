namespace WLib.WinCtrls.MessageCtrl
{
    partial class ErrorHandlerBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorHandlerBox));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelOptButtons = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblConcatTips = new System.Windows.Forms.Label();
            this.btnContact = new wyDay.Controls.SplitButton();
            this.btnOpenLog = new System.Windows.Forms.Button();
            this.panelSuggestion = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblExpand = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblSuggestion = new System.Windows.Forms.Label();
            this.panelErrorInfo = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtMessageDetail = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblCopyMsg = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelOptButtons.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelErrorInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelOptButtons, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.panelSuggestion, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelErrorInfo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtMessageDetail, 0, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 11);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(480, 285);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelOptButtons
            // 
            this.panelOptButtons.Controls.Add(this.btnOK);
            this.panelOptButtons.Controls.Add(this.lblConcatTips);
            this.panelOptButtons.Controls.Add(this.btnContact);
            this.panelOptButtons.Controls.Add(this.btnOpenLog);
            this.panelOptButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOptButtons.Location = new System.Drawing.Point(0, 248);
            this.panelOptButtons.Margin = new System.Windows.Forms.Padding(0);
            this.panelOptButtons.Name = "panelOptButtons";
            this.panelOptButtons.Size = new System.Drawing.Size(480, 37);
            this.panelOptButtons.TabIndex = 5;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(366, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(114, 37);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // lblConcatTips
            // 
            this.lblConcatTips.AutoSize = true;
            this.lblConcatTips.ForeColor = System.Drawing.Color.DarkRed;
            this.lblConcatTips.Location = new System.Drawing.Point(207, 12);
            this.lblConcatTips.Name = "lblConcatTips";
            this.lblConcatTips.Size = new System.Drawing.Size(137, 12);
            this.lblConcatTips.TabIndex = 4;
            this.lblConcatTips.Text = "联系方式已复制到剪贴板";
            this.lblConcatTips.Visible = false;
            // 
            // btnContact
            // 
            this.btnContact.AutoSize = true;
            this.btnContact.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnContact.Image = ((System.Drawing.Image)(resources.GetObject("btnContact.Image")));
            this.btnContact.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnContact.Location = new System.Drawing.Point(94, 0);
            this.btnContact.Name = "btnContact";
            this.btnContact.Size = new System.Drawing.Size(108, 37);
            this.btnContact.TabIndex = 3;
            this.btnContact.Text = "   联系管理员";
            this.btnContact.UseVisualStyleBackColor = true;
            // 
            // btnOpenLog
            // 
            this.btnOpenLog.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOpenLog.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenLog.Image")));
            this.btnOpenLog.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpenLog.Location = new System.Drawing.Point(0, 0);
            this.btnOpenLog.Name = "btnOpenLog";
            this.btnOpenLog.Size = new System.Drawing.Size(94, 37);
            this.btnOpenLog.TabIndex = 1;
            this.btnOpenLog.Text = "  查看日志";
            this.btnOpenLog.UseVisualStyleBackColor = true;
            this.btnOpenLog.Visible = false;
            this.btnOpenLog.Click += new System.EventHandler(this.BtnOpenLog_Click);
            // 
            // panelSuggestion
            // 
            this.panelSuggestion.AutoScroll = true;
            this.panelSuggestion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSuggestion.Location = new System.Drawing.Point(30, 123);
            this.panelSuggestion.Margin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.panelSuggestion.Name = "panelSuggestion";
            this.panelSuggestion.Size = new System.Drawing.Size(450, 100);
            this.panelSuggestion.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.lblExpand);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 223);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(480, 23);
            this.panel4.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.GhostWhite;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(82)))), ((int)(((byte)(175)))));
            this.label1.Image = global::WLib.WinCtrls.Properties.Resources.info16;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "     详细信息";
            // 
            // lblExpand
            // 
            this.lblExpand.BackColor = System.Drawing.Color.GhostWhite;
            this.lblExpand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblExpand.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblExpand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(82)))), ((int)(((byte)(175)))));
            this.lblExpand.Location = new System.Drawing.Point(0, 0);
            this.lblExpand.Margin = new System.Windows.Forms.Padding(3);
            this.lblExpand.Name = "lblExpand";
            this.lblExpand.Size = new System.Drawing.Size(480, 23);
            this.lblExpand.TabIndex = 4;
            this.lblExpand.Text = "展开";
            this.lblExpand.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblExpand.Click += new System.EventHandler(this.LblExpand_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.GhostWhite;
            this.panel3.Controls.Add(this.lblSuggestion);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 100);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(480, 23);
            this.panel3.TabIndex = 3;
            // 
            // lblSuggestion
            // 
            this.lblSuggestion.AutoSize = true;
            this.lblSuggestion.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSuggestion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(82)))), ((int)(((byte)(175)))));
            this.lblSuggestion.Image = global::WLib.WinCtrls.Properties.Resources.suggestion16;
            this.lblSuggestion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSuggestion.Location = new System.Drawing.Point(3, 3);
            this.lblSuggestion.Margin = new System.Windows.Forms.Padding(3);
            this.lblSuggestion.Name = "lblSuggestion";
            this.lblSuggestion.Size = new System.Drawing.Size(76, 17);
            this.lblSuggestion.TabIndex = 1;
            this.lblSuggestion.Text = "     处理建议";
            this.lblSuggestion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelErrorInfo
            // 
            this.panelErrorInfo.Controls.Add(this.lblCopyMsg);
            this.panelErrorInfo.Controls.Add(this.lblMessage);
            this.panelErrorInfo.Controls.Add(this.pictureBox1);
            this.panelErrorInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelErrorInfo.Location = new System.Drawing.Point(3, 3);
            this.panelErrorInfo.Name = "panelErrorInfo";
            this.panelErrorInfo.Size = new System.Drawing.Size(474, 94);
            this.panelErrorInfo.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::WLib.WinCtrls.Properties.Resources.错误1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(91, 94);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // txtMessageDetail
            // 
            this.txtMessageDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessageDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessageDetail.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMessageDetail.Location = new System.Drawing.Point(30, 252);
            this.txtMessageDetail.Margin = new System.Windows.Forms.Padding(30, 6, 3, 6);
            this.txtMessageDetail.Multiline = true;
            this.txtMessageDetail.Name = "txtMessageDetail";
            this.txtMessageDetail.ReadOnly = true;
            this.txtMessageDetail.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessageDetail.Size = new System.Drawing.Size(447, 1);
            this.txtMessageDetail.TabIndex = 4;
            this.txtMessageDetail.Text = "1、xxx\r\n2、xxx";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoEllipsis = true;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessage.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMessage.Location = new System.Drawing.Point(91, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(383, 94);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "System.Exception：未将对象引用设置到对象实例";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCopyMsg
            // 
            this.lblCopyMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopyMsg.AutoSize = true;
            this.lblCopyMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyMsg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCopyMsg.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCopyMsg.Location = new System.Drawing.Point(431, 82);
            this.lblCopyMsg.Name = "lblCopyMsg";
            this.lblCopyMsg.Size = new System.Drawing.Size(47, 12);
            this.lblCopyMsg.TabIndex = 2;
            this.lblCopyMsg.Text = "复制(&C)";
            this.lblCopyMsg.Click += new System.EventHandler(this.LblCopyMsg_Click);
            // 
            // ErrorHandlerBox
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(501, 307);
            this.Controls.Add(this.tableLayoutPanel1);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorHandlerBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "错误处理";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.ErrorHandlerBox_HelpButtonClicked);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelOptButtons.ResumeLayout(false);
            this.panelOptButtons.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panelErrorInfo.ResumeLayout(false);
            this.panelErrorInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelErrorInfo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtMessageDetail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSuggestion;
        private System.Windows.Forms.Panel panelOptButtons;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnOpenLog;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblExpand;
        private System.Windows.Forms.Panel panel3;
        private wyDay.Controls.SplitButton btnContact;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label lblConcatTips;
        private System.Windows.Forms.Panel panelSuggestion;
        private System.Windows.Forms.Label lblCopyMsg;
        private System.Windows.Forms.Label lblMessage;
    }
}