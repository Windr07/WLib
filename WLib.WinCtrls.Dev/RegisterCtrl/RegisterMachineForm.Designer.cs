namespace WLib.WinCtrls.Dev.RegisterCtrl
{
    partial class RegisterMachineForm
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbSoftware = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtComment = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtMachineCode = new DevExpress.XtraEditors.MemoEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblEndTime = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.lblStartTime = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.numMinute = new DevExpress.XtraEditors.SpinEdit();
            this.numHour = new DevExpress.XtraEditors.SpinEdit();
            this.numDay = new DevExpress.XtraEditors.SpinEdit();
            this.numMonth = new DevExpress.XtraEditors.SpinEdit();
            this.numYear = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtRegisterCode = new DevExpress.XtraEditors.MemoEdit();
            this.btnGetRegisterCode = new DevExpress.XtraEditors.SimpleButton();
            this.hyperlinkLabelControl1 = new DevExpress.XtraEditors.HyperlinkLabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSoftware.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMachineCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinute.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHour.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegisterCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(12, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(359, 57);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "1、确保本机时间是标准北京时间，授权开始时间自动提前两分钟；\r\n2、提醒用户确保授权的机器的系统时间是正确的时间；\r\n3、提醒用户在发送机器码并注册成功前不要关闭" +
    "软件注册窗口，\r\n   否则下次打开注册窗口需要重新发送机器码。\r\n";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 64);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "授权对象";
            // 
            // cmbSoftware
            // 
            this.cmbSoftware.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSoftware.Location = new System.Drawing.Point(73, 62);
            this.cmbSoftware.Name = "cmbSoftware";
            this.cmbSoftware.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSoftware.Size = new System.Drawing.Size(404, 20);
            this.cmbSoftware.TabIndex = 2;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 90);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "授权备注";
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.EditValue = "授权给XX单位/个人";
            this.txtComment.Location = new System.Drawing.Point(73, 87);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(404, 20);
            this.txtComment.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 114);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 14);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = "机器码";
            // 
            // txtMachineCode
            // 
            this.txtMachineCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMachineCode.EditValue = "";
            this.txtMachineCode.Location = new System.Drawing.Point(12, 130);
            this.txtMachineCode.Name = "txtMachineCode";
            this.txtMachineCode.Size = new System.Drawing.Size(465, 56);
            this.txtMachineCode.TabIndex = 3;
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.lblEndTime);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.labelControl14);
            this.groupControl1.Controls.Add(this.labelControl13);
            this.groupControl1.Controls.Add(this.labelControl12);
            this.groupControl1.Controls.Add(this.labelControl11);
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.lblStartTime);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.numMinute);
            this.groupControl1.Controls.Add(this.numHour);
            this.groupControl1.Controls.Add(this.numDay);
            this.groupControl1.Controls.Add(this.numMonth);
            this.groupControl1.Controls.Add(this.numYear);
            this.groupControl1.Location = new System.Drawing.Point(12, 193);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(465, 78);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "授权时长";
            // 
            // lblEndTime
            // 
            this.lblEndTime.Location = new System.Drawing.Point(281, 59);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(4, 14);
            this.lblEndTime.TabIndex = 1;
            this.lblEndTime.Text = "-";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(210, 59);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(60, 14);
            this.labelControl7.TabIndex = 1;
            this.labelControl7.Text = "结束时间：";
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(407, 32);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(12, 14);
            this.labelControl14.TabIndex = 1;
            this.labelControl14.Text = "分";
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(325, 32);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(12, 14);
            this.labelControl13.TabIndex = 1;
            this.labelControl13.Text = "时";
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(247, 32);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(12, 14);
            this.labelControl12.TabIndex = 1;
            this.labelControl12.Text = "日";
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(169, 30);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(12, 14);
            this.labelControl11.TabIndex = 1;
            this.labelControl11.Text = "月";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(97, 32);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(12, 14);
            this.labelControl10.TabIndex = 1;
            this.labelControl10.Text = "年";
            // 
            // lblStartTime
            // 
            this.lblStartTime.Location = new System.Drawing.Point(78, 58);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(4, 14);
            this.lblStartTime.TabIndex = 1;
            this.lblStartTime.Text = "-";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(7, 59);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(60, 14);
            this.labelControl6.TabIndex = 1;
            this.labelControl6.Text = "开始时间：";
            // 
            // numMinute
            // 
            this.numMinute.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numMinute.Location = new System.Drawing.Point(347, 29);
            this.numMinute.Name = "numMinute";
            this.numMinute.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numMinute.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numMinute.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.numMinute.Size = new System.Drawing.Size(49, 20);
            this.numMinute.TabIndex = 3;
            // 
            // numHour
            // 
            this.numHour.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numHour.Location = new System.Drawing.Point(265, 29);
            this.numHour.Name = "numHour";
            this.numHour.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numHour.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numHour.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.numHour.Size = new System.Drawing.Size(49, 20);
            this.numHour.TabIndex = 3;
            // 
            // numDay
            // 
            this.numDay.EditValue = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numDay.Location = new System.Drawing.Point(189, 29);
            this.numDay.Name = "numDay";
            this.numDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numDay.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numDay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.numDay.Size = new System.Drawing.Size(49, 20);
            this.numDay.TabIndex = 3;
            // 
            // numMonth
            // 
            this.numMonth.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numMonth.Location = new System.Drawing.Point(115, 29);
            this.numMonth.Name = "numMonth";
            this.numMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numMonth.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numMonth.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.numMonth.Size = new System.Drawing.Size(49, 20);
            this.numMonth.TabIndex = 3;
            // 
            // numYear
            // 
            this.numYear.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numYear.Location = new System.Drawing.Point(6, 29);
            this.numYear.Name = "numYear";
            this.numYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numYear.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numYear.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.numYear.Size = new System.Drawing.Size(79, 20);
            this.numYear.TabIndex = 3;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 311);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(36, 14);
            this.labelControl5.TabIndex = 1;
            this.labelControl5.Text = "注册码";
            // 
            // txtRegisterCode
            // 
            this.txtRegisterCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegisterCode.EditValue = "";
            this.txtRegisterCode.Location = new System.Drawing.Point(12, 331);
            this.txtRegisterCode.Name = "txtRegisterCode";
            this.txtRegisterCode.Size = new System.Drawing.Size(465, 153);
            this.txtRegisterCode.TabIndex = 3;
            // 
            // btnGetRegisterCode
            // 
            this.btnGetRegisterCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetRegisterCode.Location = new System.Drawing.Point(320, 290);
            this.btnGetRegisterCode.Name = "btnGetRegisterCode";
            this.btnGetRegisterCode.Size = new System.Drawing.Size(157, 34);
            this.btnGetRegisterCode.TabIndex = 5;
            this.btnGetRegisterCode.Text = "获取注册码";
            this.btnGetRegisterCode.Click += new System.EventHandler(this.btnGetRegisterCode_Click);
            // 
            // hyperlinkLabelControl1
            // 
            this.hyperlinkLabelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.hyperlinkLabelControl1.Location = new System.Drawing.Point(393, 490);
            this.hyperlinkLabelControl1.Name = "hyperlinkLabelControl1";
            this.hyperlinkLabelControl1.Size = new System.Drawing.Size(84, 14);
            this.hyperlinkLabelControl1.TabIndex = 6;
            this.hyperlinkLabelControl1.Text = "查看已授权情况";
            this.hyperlinkLabelControl1.Click += new System.EventHandler(this.hyperlinkLabelControl1_Click);
            // 
            // RegisterMachineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 514);
            this.Controls.Add(this.hyperlinkLabelControl1);
            this.Controls.Add(this.btnGetRegisterCode);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.cmbSoftware);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtRegisterCode);
            this.Controls.Add(this.txtMachineCode);
            this.Name = "RegisterMachineForm";
            this.Text = "注册机";
            ((System.ComponentModel.ISupportInitialize)(this.cmbSoftware.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMachineCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinute.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHour.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegisterCode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSoftware;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtComment;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.MemoEdit txtMachineCode;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl lblEndTime;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl lblStartTime;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.MemoEdit txtRegisterCode;
        private DevExpress.XtraEditors.SpinEdit numYear;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.SpinEdit numMonth;
        private DevExpress.XtraEditors.SpinEdit numHour;
        private DevExpress.XtraEditors.SpinEdit numDay;
        private DevExpress.XtraEditors.SpinEdit numMinute;
        private DevExpress.XtraEditors.SimpleButton btnGetRegisterCode;
        private DevExpress.XtraEditors.HyperlinkLabelControl hyperlinkLabelControl1;
    }
}