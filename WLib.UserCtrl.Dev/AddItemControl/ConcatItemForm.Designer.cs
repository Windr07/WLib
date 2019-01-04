namespace WLib.UserCtrls.Dev.AddItemControl
{
    partial class ConcatItemForm
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
            this.groupBoxSettings = new DevExpress.XtraEditors.GroupControl();
            this.tblPanelConcatStr = new System.Windows.Forms.TableLayoutPanel();
            this.tblPanelPreSubffix = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbConcatAll = new DevExpress.XtraEditors.CheckEdit();
            this.cbAddPrefixSuffix = new DevExpress.XtraEditors.CheckEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrefix = new DevExpress.XtraEditors.TextEdit();
            this.txtSuffix = new DevExpress.XtraEditors.TextEdit();
            this.txtConStr = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInput.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxCtrlItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBoxSettings)).BeginInit();
            this.groupBoxSettings.SuspendLayout();
            this.tblPanelConcatStr.SuspendLayout();
            this.tblPanelPreSubffix.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbConcatAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbAddPrefixSuffix.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrefix.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSuffix.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConStr.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            // 
            // sBtnOK
            // 
            this.sBtnOK.Location = new System.Drawing.Point(296, 482);
            // 
            // sBtnCancel
            // 
            this.sBtnCancel.Location = new System.Drawing.Point(174, 482);
            // 
            // listBoxCtrlItems
            // 
            this.listBoxCtrlItems.Size = new System.Drawing.Size(401, 106);
            // 
            // sBtnClear
            // 
            this.sBtnClear.Location = new System.Drawing.Point(309, 280);
            // 
            // sBtnRemoveRepeat
            // 
            this.sBtnRemoveRepeat.Location = new System.Drawing.Point(196, 280);
            // 
            // sBtnSettings
            // 
            this.sBtnSettings.Location = new System.Drawing.Point(11, 482);
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.tblPanelConcatStr);
            this.groupBoxSettings.Controls.Add(this.tblPanelPreSubffix);
            this.groupBoxSettings.Controls.Add(this.cbConcatAll);
            this.groupBoxSettings.Controls.Add(this.cbAddPrefixSuffix);
            this.groupBoxSettings.Location = new System.Drawing.Point(12, 322);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(400, 148);
            this.groupBoxSettings.TabIndex = 5;
            this.groupBoxSettings.Text = "拼接规则";
            // 
            // tblPanelConcatStr
            // 
            this.tblPanelConcatStr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblPanelConcatStr.ColumnCount = 2;
            this.tblPanelConcatStr.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tblPanelConcatStr.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblPanelConcatStr.Controls.Add(this.txtConStr, 0, 0);
            this.tblPanelConcatStr.Controls.Add(this.label3, 0, 0);
            this.tblPanelConcatStr.Location = new System.Drawing.Point(45, 102);
            this.tblPanelConcatStr.Name = "tblPanelConcatStr";
            this.tblPanelConcatStr.Padding = new System.Windows.Forms.Padding(3);
            this.tblPanelConcatStr.RowCount = 1;
            this.tblPanelConcatStr.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPanelConcatStr.Size = new System.Drawing.Size(347, 41);
            this.tblPanelConcatStr.TabIndex = 5;
            // 
            // tblPanelPreSubffix
            // 
            this.tblPanelPreSubffix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblPanelPreSubffix.ColumnCount = 4;
            this.tblPanelPreSubffix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tblPanelPreSubffix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblPanelPreSubffix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tblPanelPreSubffix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblPanelPreSubffix.Controls.Add(this.txtSuffix, 3, 0);
            this.tblPanelPreSubffix.Controls.Add(this.label1, 0, 0);
            this.tblPanelPreSubffix.Controls.Add(this.label2, 2, 0);
            this.tblPanelPreSubffix.Controls.Add(this.txtPrefix, 1, 0);
            this.tblPanelPreSubffix.Location = new System.Drawing.Point(45, 43);
            this.tblPanelPreSubffix.Name = "tblPanelPreSubffix";
            this.tblPanelPreSubffix.Padding = new System.Windows.Forms.Padding(3);
            this.tblPanelPreSubffix.RowCount = 1;
            this.tblPanelPreSubffix.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPanelPreSubffix.Size = new System.Drawing.Size(347, 38);
            this.tblPanelPreSubffix.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "前缀:";
            // 
            // cbConcatAll
            // 
            this.cbConcatAll.Location = new System.Drawing.Point(11, 83);
            this.cbConcatAll.Name = "cbConcatAll";
            this.cbConcatAll.Properties.Caption = "将全部项串联成一项";
            this.cbConcatAll.Size = new System.Drawing.Size(256, 19);
            this.cbConcatAll.TabIndex = 0;
            this.cbConcatAll.CheckedChanged += new System.EventHandler(this.cbConcatAll_CheckedChanged);
            // 
            // cbAddPrefixSuffix
            // 
            this.cbAddPrefixSuffix.EditValue = true;
            this.cbAddPrefixSuffix.Location = new System.Drawing.Point(11, 26);
            this.cbAddPrefixSuffix.Name = "cbAddPrefixSuffix";
            this.cbAddPrefixSuffix.Properties.Caption = "每一项都添加前缀或后缀";
            this.cbAddPrefixSuffix.Size = new System.Drawing.Size(256, 19);
            this.cbAddPrefixSuffix.TabIndex = 0;
            this.cbAddPrefixSuffix.CheckedChanged += new System.EventHandler(this.cbAddPrefixSuffix_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "后缀:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 14);
            this.label3.TabIndex = 1;
            this.label3.Text = "连接符:";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPrefix.Location = new System.Drawing.Point(42, 4);
            this.txtPrefix.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(128, 21);
            this.txtPrefix.TabIndex = 2;
            // 
            // txtSuffix
            // 
            this.txtSuffix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSuffix.Location = new System.Drawing.Point(212, 4);
            this.txtSuffix.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new System.Drawing.Size(129, 21);
            this.txtSuffix.TabIndex = 3;
            // 
            // txtConStr
            // 
            this.txtConStr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConStr.Location = new System.Drawing.Point(57, 4);
            this.txtConStr.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.txtConStr.Name = "txtConStr";
            this.txtConStr.Size = new System.Drawing.Size(284, 21);
            this.txtConStr.TabIndex = 3;
            // 
            // ConcatItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 525);
            this.Controls.Add(this.groupBoxSettings);
            this.Items = new string[0];
            this.Name = "ConcatItemForm";
            this.ShowSettingButton = true;
            this.Text = "拼接项";
            this.Controls.SetChildIndex(this.txtInput, 0);
            this.Controls.SetChildIndex(this.lblTips, 0);
            this.Controls.SetChildIndex(this.lblFilterItemTips, 0);
            this.Controls.SetChildIndex(this.listBoxCtrlItems, 0);
            this.Controls.SetChildIndex(this.sBtnOK, 0);
            this.Controls.SetChildIndex(this.sBtnSettings, 0);
            this.Controls.SetChildIndex(this.sBtnCancel, 0);
            this.Controls.SetChildIndex(this.sBtnAdd, 0);
            this.Controls.SetChildIndex(this.sBtnClear, 0);
            this.Controls.SetChildIndex(this.sBtnRemoveRepeat, 0);
            this.Controls.SetChildIndex(this.groupBoxSettings, 0);
            ((System.ComponentModel.ISupportInitialize)(this.txtInput.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxCtrlItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBoxSettings)).EndInit();
            this.groupBoxSettings.ResumeLayout(false);
            this.tblPanelConcatStr.ResumeLayout(false);
            this.tblPanelConcatStr.PerformLayout();
            this.tblPanelPreSubffix.ResumeLayout(false);
            this.tblPanelPreSubffix.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbConcatAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbAddPrefixSuffix.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrefix.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSuffix.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConStr.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupBoxSettings;
        private System.Windows.Forms.TableLayoutPanel tblPanelConcatStr;
        private System.Windows.Forms.TableLayoutPanel tblPanelPreSubffix;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.CheckEdit cbConcatAll;
        private DevExpress.XtraEditors.CheckEdit cbAddPrefixSuffix;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtPrefix;
        private DevExpress.XtraEditors.TextEdit txtConStr;
        private DevExpress.XtraEditors.TextEdit txtSuffix;
    }
}