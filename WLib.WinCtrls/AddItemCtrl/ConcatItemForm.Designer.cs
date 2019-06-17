namespace WLib.WinCtrls.AddItemCtrl
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
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.tblPanelConcatStr = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtConStr = new System.Windows.Forms.TextBox();
            this.tblPanelPreSubffix = new System.Windows.Forms.TableLayoutPanel();
            this.txtSuffix = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.cbAddPrefixSuffix = new System.Windows.Forms.CheckBox();
            this.cbConcatAll = new System.Windows.Forms.CheckBox();
            this.groupBoxSettings.SuspendLayout();
            this.tblPanelConcatStr.SuspendLayout();
            this.tblPanelPreSubffix.SuspendLayout();
            this.SuspendLayout();
            // 
            // sBtnCancel
            // 
            this.sBtnCancel.Location = new System.Drawing.Point(186, 480);
            // 
            // sBtnSettings
            // 
            this.sBtnSettings.Location = new System.Drawing.Point(14, 480);
            // 
            // sBtnOK
            // 
            this.sBtnOK.Location = new System.Drawing.Point(301, 480);
            // 
            // sBtnClear
            // 
            this.sBtnClear.Location = new System.Drawing.Point(314, 291);
            // 
            // sBtnRemoveRepeat
            // 
            this.sBtnRemoveRepeat.Location = new System.Drawing.Point(212, 291);
            // 
            // listBoxCtrlItems
            // 
            this.listBoxCtrlItems.Size = new System.Drawing.Size(396, 112);
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSettings.Controls.Add(this.tblPanelConcatStr);
            this.groupBoxSettings.Controls.Add(this.tblPanelPreSubffix);
            this.groupBoxSettings.Controls.Add(this.cbAddPrefixSuffix);
            this.groupBoxSettings.Controls.Add(this.cbConcatAll);
            this.groupBoxSettings.Location = new System.Drawing.Point(14, 326);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(396, 148);
            this.groupBoxSettings.TabIndex = 4;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "拼接规则：";
            // 
            // tblPanelConcatStr
            // 
            this.tblPanelConcatStr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblPanelConcatStr.ColumnCount = 2;
            this.tblPanelConcatStr.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tblPanelConcatStr.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblPanelConcatStr.Controls.Add(this.label4, 0, 0);
            this.tblPanelConcatStr.Controls.Add(this.txtConStr, 1, 0);
            this.tblPanelConcatStr.Location = new System.Drawing.Point(43, 101);
            this.tblPanelConcatStr.Name = "tblPanelConcatStr";
            this.tblPanelConcatStr.Padding = new System.Windows.Forms.Padding(3);
            this.tblPanelConcatStr.RowCount = 1;
            this.tblPanelConcatStr.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPanelConcatStr.Size = new System.Drawing.Size(347, 41);
            this.tblPanelConcatStr.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "连接符:";
            // 
            // txtConStr
            // 
            this.txtConStr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConStr.Location = new System.Drawing.Point(57, 6);
            this.txtConStr.Name = "txtConStr";
            this.txtConStr.Size = new System.Drawing.Size(284, 21);
            this.txtConStr.TabIndex = 1;
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
            this.tblPanelPreSubffix.Controls.Add(this.label2, 2, 0);
            this.tblPanelPreSubffix.Controls.Add(this.label1, 0, 0);
            this.tblPanelPreSubffix.Controls.Add(this.txtPrefix, 1, 0);
            this.tblPanelPreSubffix.Location = new System.Drawing.Point(43, 41);
            this.tblPanelPreSubffix.Name = "tblPanelPreSubffix";
            this.tblPanelPreSubffix.Padding = new System.Windows.Forms.Padding(3);
            this.tblPanelPreSubffix.RowCount = 1;
            this.tblPanelPreSubffix.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPanelPreSubffix.Size = new System.Drawing.Size(347, 38);
            this.tblPanelPreSubffix.TabIndex = 3;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "后缀:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "前缀:";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPrefix.Location = new System.Drawing.Point(42, 4);
            this.txtPrefix.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(128, 21);
            this.txtPrefix.TabIndex = 1;
            // 
            // cbAddPrefixSuffix
            // 
            this.cbAddPrefixSuffix.AutoSize = true;
            this.cbAddPrefixSuffix.Checked = true;
            this.cbAddPrefixSuffix.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAddPrefixSuffix.Location = new System.Drawing.Point(15, 22);
            this.cbAddPrefixSuffix.Name = "cbAddPrefixSuffix";
            this.cbAddPrefixSuffix.Size = new System.Drawing.Size(156, 16);
            this.cbAddPrefixSuffix.TabIndex = 0;
            this.cbAddPrefixSuffix.Text = "每一项都添加前缀或后缀";
            this.cbAddPrefixSuffix.UseVisualStyleBackColor = true;
            this.cbAddPrefixSuffix.CheckedChanged += new System.EventHandler(this.cbAddPrefixSuffix_CheckedChanged);
            // 
            // cbConcatAll
            // 
            this.cbConcatAll.AutoSize = true;
            this.cbConcatAll.Location = new System.Drawing.Point(15, 85);
            this.cbConcatAll.Name = "cbConcatAll";
            this.cbConcatAll.Size = new System.Drawing.Size(132, 16);
            this.cbConcatAll.TabIndex = 0;
            this.cbConcatAll.Text = "将全部项串联成一项";
            this.cbConcatAll.UseVisualStyleBackColor = true;
            this.cbConcatAll.CheckedChanged += new System.EventHandler(this.cbConcatAll_CheckedChanged);
            // 
            // ConcatItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 527);
            this.Controls.Add(this.groupBoxSettings);
            this.Items = new string[0];
            this.Name = "ConcatItemForm";
            this.ShowSettingButton = true;
            this.Text = "拼接项";
            this.Controls.SetChildIndex(this.lblTips, 0);
            this.Controls.SetChildIndex(this.lblFilterItemTips, 0);
            this.Controls.SetChildIndex(this.txtInput, 0);
            this.Controls.SetChildIndex(this.sBtnAdd, 0);
            this.Controls.SetChildIndex(this.listBoxCtrlItems, 0);
            this.Controls.SetChildIndex(this.sBtnCancel, 0);
            this.Controls.SetChildIndex(this.sBtnClear, 0);
            this.Controls.SetChildIndex(this.sBtnRemoveRepeat, 0);
            this.Controls.SetChildIndex(this.sBtnSettings, 0);
            this.Controls.SetChildIndex(this.sBtnOK, 0);
            this.Controls.SetChildIndex(this.groupBoxSettings, 0);
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.tblPanelConcatStr.ResumeLayout(false);
            this.tblPanelConcatStr.PerformLayout();
            this.tblPanelPreSubffix.ResumeLayout(false);
            this.tblPanelPreSubffix.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.CheckBox cbAddPrefixSuffix;
        private System.Windows.Forms.CheckBox cbConcatAll;
        private System.Windows.Forms.TableLayoutPanel tblPanelConcatStr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tblPanelPreSubffix;
        private System.Windows.Forms.TextBox txtSuffix;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.TextBox txtConStr;
    }
}