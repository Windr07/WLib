namespace WLib.WinCtrls.PluginCtrl
{
    partial class PluginInfoCtrl
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTips = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCmd = new System.Windows.Forms.TextBox();
            this.txtAssembly = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIcon = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmbShortcutKeys = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbInvokeType = new System.Windows.Forms.ComboBox();
            this.cbVisible = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名  称";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(51, 6);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(263, 21);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "快捷键";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "提  示";
            // 
            // txtTips
            // 
            this.txtTips.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTips.Location = new System.Drawing.Point(51, 53);
            this.txtTips.Multiline = true;
            this.txtTips.Name = "txtTips";
            this.txtTips.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTips.Size = new System.Drawing.Size(263, 33);
            this.txtTips.TabIndex = 2;
            this.txtTips.TextChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "命  令";
            // 
            // txtCmd
            // 
            this.txtCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCmd.Location = new System.Drawing.Point(51, 142);
            this.txtCmd.Name = "txtCmd";
            this.txtCmd.ReadOnly = true;
            this.txtCmd.Size = new System.Drawing.Size(349, 21);
            this.txtCmd.TabIndex = 1;
            this.txtCmd.TextChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // txtAssembly
            // 
            this.txtAssembly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAssembly.Location = new System.Drawing.Point(51, 169);
            this.txtAssembly.Name = "txtAssembly";
            this.txtAssembly.ReadOnly = true;
            this.txtAssembly.Size = new System.Drawing.Size(349, 21);
            this.txtAssembly.TabIndex = 1;
            this.txtAssembly.TextChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "程序集";
            // 
            // txtIcon
            // 
            this.txtIcon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIcon.Location = new System.Drawing.Point(51, 90);
            this.txtIcon.Name = "txtIcon";
            this.txtIcon.ReadOnly = true;
            this.txtIcon.Size = new System.Drawing.Size(349, 21);
            this.txtIcon.TabIndex = 1;
            this.txtIcon.TextChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "图  标";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Location = new System.Drawing.Point(320, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // cmbShortcutKeys
            // 
            this.cmbShortcutKeys.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbShortcutKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShortcutKeys.FormattingEnabled = true;
            this.cmbShortcutKeys.Location = new System.Drawing.Point(51, 30);
            this.cmbShortcutKeys.Name = "cmbShortcutKeys";
            this.cmbShortcutKeys.Size = new System.Drawing.Size(263, 20);
            this.cmbShortcutKeys.TabIndex = 1;
            this.cmbShortcutKeys.SelectedIndexChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "调  用";
            // 
            // cmbInvokeType
            // 
            this.cmbInvokeType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbInvokeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInvokeType.FormattingEnabled = true;
            this.cmbInvokeType.Location = new System.Drawing.Point(51, 116);
            this.cmbInvokeType.Name = "cmbInvokeType";
            this.cmbInvokeType.Size = new System.Drawing.Size(238, 20);
            this.cmbInvokeType.TabIndex = 4;
            this.cmbInvokeType.SelectedIndexChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // cbVisible
            // 
            this.cbVisible.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbVisible.AutoSize = true;
            this.cbVisible.Checked = true;
            this.cbVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbVisible.Location = new System.Drawing.Point(295, 119);
            this.cbVisible.Name = "cbVisible";
            this.cbVisible.Size = new System.Drawing.Size(96, 16);
            this.cbVisible.TabIndex = 5;
            this.cbVisible.Text = "是否显示插件";
            this.cbVisible.UseVisualStyleBackColor = true;
            // 
            // PluginInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbVisible);
            this.Controls.Add(this.cmbInvokeType);
            this.Controls.Add(this.cmbShortcutKeys);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTips);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAssembly);
            this.Controls.Add(this.txtIcon);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtCmd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(270, 230);
            this.Name = "PluginInfoForm";
            this.Size = new System.Drawing.Size(405, 230);
            this.TextChanged += new System.EventHandler(this.Control_ValueChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTips;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCmd;
        private System.Windows.Forms.TextBox txtAssembly;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtIcon;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbShortcutKeys;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbInvokeType;
        private System.Windows.Forms.CheckBox cbVisible;
    }
}