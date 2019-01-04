namespace WLib.UserCtrls.Dev.AddItemControl
{
    partial class AddItemConfigForm
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
            this.sBtnOK = new DevExpress.XtraEditors.SimpleButton();
            this.sBtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.radioSplitString = new DevExpress.XtraEditors.CheckEdit();
            this.radioRegex = new DevExpress.XtraEditors.CheckEdit();
            this.cmbFilterString = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtRegex = new DevExpress.XtraEditors.MemoEdit();
            this.lblSplitString = new DevExpress.XtraEditors.LabelControl();
            this.lblRegex = new DevExpress.XtraEditors.LabelControl();
            this.sBtnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.listBoxSplitItems = new DevExpress.XtraEditors.ListBoxControl();
            this.cMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.移除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移除全部ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxSplitString = new System.Windows.Forms.GroupBox();
            this.groupBoxRegex = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.radioSplitString.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioRegex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFilterString.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxSplitItems)).BeginInit();
            this.cMenuStrip1.SuspendLayout();
            this.groupBoxSplitString.SuspendLayout();
            this.groupBoxRegex.SuspendLayout();
            this.SuspendLayout();
            // 
            // sBtnOK
            // 
            this.sBtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnOK.Location = new System.Drawing.Point(318, 371);
            this.sBtnOK.Name = "sBtnOK";
            this.sBtnOK.Size = new System.Drawing.Size(94, 31);
            this.sBtnOK.TabIndex = 0;
            this.sBtnOK.Text = "确定(&O)";
            this.sBtnOK.Click += new System.EventHandler(this.sBtnOK_Click);
            // 
            // sBtnCancel
            // 
            this.sBtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sBtnCancel.Location = new System.Drawing.Point(218, 371);
            this.sBtnCancel.Name = "sBtnCancel";
            this.sBtnCancel.Size = new System.Drawing.Size(94, 31);
            this.sBtnCancel.TabIndex = 0;
            this.sBtnCancel.Text = "取消(&C)";
            this.sBtnCancel.Click += new System.EventHandler(this.sBtnCancel_Click);
            // 
            // radioSplitString
            // 
            this.radioSplitString.EditValue = true;
            this.radioSplitString.Location = new System.Drawing.Point(19, 41);
            this.radioSplitString.Name = "radioSplitString";
            this.radioSplitString.Properties.Caption = "按分隔符筛选";
            this.radioSplitString.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.radioSplitString.Properties.RadioGroupIndex = 1;
            this.radioSplitString.Size = new System.Drawing.Size(103, 19);
            this.radioSplitString.TabIndex = 0;
            this.radioSplitString.CheckedChanged += new System.EventHandler(this.checkFilterString_CheckedChanged);
            // 
            // radioRegex
            // 
            this.radioRegex.Location = new System.Drawing.Point(18, 186);
            this.radioRegex.Name = "radioRegex";
            this.radioRegex.Properties.Caption = "按正则表达式筛选";
            this.radioRegex.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.radioRegex.Properties.RadioGroupIndex = 1;
            this.radioRegex.Size = new System.Drawing.Size(139, 19);
            this.radioRegex.TabIndex = 1;
            this.radioRegex.TabStop = false;
            // 
            // cmbFilterString
            // 
            this.cmbFilterString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFilterString.Location = new System.Drawing.Point(91, 19);
            this.cmbFilterString.Name = "cmbFilterString";
            this.cmbFilterString.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFilterString.Size = new System.Drawing.Size(222, 21);
            this.cmbFilterString.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(96, 14);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "筛选数据的方式：";
            // 
            // txtRegex
            // 
            this.txtRegex.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegex.Location = new System.Drawing.Point(91, 26);
            this.txtRegex.Name = "txtRegex";
            this.txtRegex.Size = new System.Drawing.Size(297, 127);
            this.txtRegex.TabIndex = 4;
            // 
            // lblSplitString
            // 
            this.lblSplitString.Location = new System.Drawing.Point(13, 22);
            this.lblSplitString.Name = "lblSplitString";
            this.lblSplitString.Size = new System.Drawing.Size(72, 14);
            this.lblSplitString.TabIndex = 5;
            this.lblSplitString.Text = "输入分隔符：";
            // 
            // lblRegex
            // 
            this.lblRegex.Location = new System.Drawing.Point(13, 29);
            this.lblRegex.Name = "lblRegex";
            this.lblRegex.Size = new System.Drawing.Size(72, 14);
            this.lblRegex.TabIndex = 6;
            this.lblRegex.Text = "正则表达式：";
            // 
            // sBtnAdd
            // 
            this.sBtnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnAdd.Location = new System.Drawing.Point(319, 17);
            this.sBtnAdd.Name = "sBtnAdd";
            this.sBtnAdd.Size = new System.Drawing.Size(69, 23);
            this.sBtnAdd.TabIndex = 8;
            this.sBtnAdd.Text = "添加(&A)";
            this.sBtnAdd.Click += new System.EventHandler(this.sBtnAdd_Click);
            // 
            // listBoxSplitItems
            // 
            this.listBoxSplitItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxSplitItems.ContextMenuStrip = this.cMenuStrip1;
            this.listBoxSplitItems.Location = new System.Drawing.Point(91, 47);
            this.listBoxSplitItems.Name = "listBoxSplitItems";
            this.listBoxSplitItems.Size = new System.Drawing.Size(297, 73);
            this.listBoxSplitItems.TabIndex = 9;
            // 
            // cMenuStrip1
            // 
            this.cMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.移除ToolStripMenuItem,
            this.移除全部ToolStripMenuItem});
            this.cMenuStrip1.Name = "contextMenuStrip1";
            this.cMenuStrip1.Size = new System.Drawing.Size(141, 48);
            // 
            // 移除ToolStripMenuItem
            // 
            this.移除ToolStripMenuItem.Name = "移除ToolStripMenuItem";
            this.移除ToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.移除ToolStripMenuItem.Text = "移除(&D)";
            this.移除ToolStripMenuItem.Click += new System.EventHandler(this.移除ToolStripMenuItem_Click);
            // 
            // 移除全部ToolStripMenuItem
            // 
            this.移除全部ToolStripMenuItem.Name = "移除全部ToolStripMenuItem";
            this.移除全部ToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.移除全部ToolStripMenuItem.Text = "移除全部(&A)";
            this.移除全部ToolStripMenuItem.Click += new System.EventHandler(this.移除全部ToolStripMenuItem_Click);
            // 
            // groupBoxSplitString
            // 
            this.groupBoxSplitString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSplitString.Controls.Add(this.cmbFilterString);
            this.groupBoxSplitString.Controls.Add(this.sBtnAdd);
            this.groupBoxSplitString.Controls.Add(this.listBoxSplitItems);
            this.groupBoxSplitString.Controls.Add(this.lblSplitString);
            this.groupBoxSplitString.Location = new System.Drawing.Point(13, 46);
            this.groupBoxSplitString.Name = "groupBoxSplitString";
            this.groupBoxSplitString.Size = new System.Drawing.Size(399, 132);
            this.groupBoxSplitString.TabIndex = 10;
            this.groupBoxSplitString.TabStop = false;
            // 
            // groupBoxRegex
            // 
            this.groupBoxRegex.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxRegex.Controls.Add(this.lblRegex);
            this.groupBoxRegex.Controls.Add(this.txtRegex);
            this.groupBoxRegex.Enabled = false;
            this.groupBoxRegex.Location = new System.Drawing.Point(13, 190);
            this.groupBoxRegex.Name = "groupBoxRegex";
            this.groupBoxRegex.Size = new System.Drawing.Size(399, 169);
            this.groupBoxRegex.TabIndex = 11;
            this.groupBoxRegex.TabStop = false;
            // 
            // AddItemConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.sBtnCancel;
            this.ClientSize = new System.Drawing.Size(424, 413);
            this.Controls.Add(this.radioRegex);
            this.Controls.Add(this.radioSplitString);
            this.Controls.Add(this.groupBoxRegex);
            this.Controls.Add(this.groupBoxSplitString);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.sBtnCancel);
            this.Controls.Add(this.sBtnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddItemConfigForm";
            this.Text = "数据筛选设置";
            ((System.ComponentModel.ISupportInitialize)(this.radioSplitString.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioRegex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFilterString.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxSplitItems)).EndInit();
            this.cMenuStrip1.ResumeLayout(false);
            this.groupBoxSplitString.ResumeLayout(false);
            this.groupBoxSplitString.PerformLayout();
            this.groupBoxRegex.ResumeLayout(false);
            this.groupBoxRegex.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sBtnOK;
        private DevExpress.XtraEditors.SimpleButton sBtnCancel;
        private DevExpress.XtraEditors.CheckEdit radioSplitString;
        private DevExpress.XtraEditors.CheckEdit radioRegex;
        private DevExpress.XtraEditors.ComboBoxEdit cmbFilterString;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.MemoEdit txtRegex;
        private DevExpress.XtraEditors.LabelControl lblSplitString;
        private DevExpress.XtraEditors.LabelControl lblRegex;
        private DevExpress.XtraEditors.SimpleButton sBtnAdd;
        private DevExpress.XtraEditors.ListBoxControl listBoxSplitItems;
        private System.Windows.Forms.ContextMenuStrip cMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 移除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移除全部ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBoxSplitString;
        private System.Windows.Forms.GroupBox groupBoxRegex;
    }
}