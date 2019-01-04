namespace WLib.UserCtrls.AddItemControl
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
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxSplitString = new System.Windows.Forms.GroupBox();
            this.listBoxSplitItems = new System.Windows.Forms.ListBox();
            this.sBtnAdd = new System.Windows.Forms.Button();
            this.cmbFilterString = new System.Windows.Forms.ComboBox();
            this.lblSplitString = new System.Windows.Forms.Label();
            this.radioSplitString = new System.Windows.Forms.RadioButton();
            this.txtRegex = new System.Windows.Forms.TextBox();
            this.sBtnOK = new System.Windows.Forms.Button();
            this.groupBoxRegex = new System.Windows.Forms.GroupBox();
            this.lblRegex = new System.Windows.Forms.Label();
            this.radioRegex = new System.Windows.Forms.RadioButton();
            this.sBtnCancel = new System.Windows.Forms.Button();
            this.cMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.移除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移除全部ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxSplitString.SuspendLayout();
            this.groupBoxRegex.SuspendLayout();
            this.cMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "筛选数据的方式：";
            // 
            // groupBoxSplitString
            // 
            this.groupBoxSplitString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSplitString.Controls.Add(this.listBoxSplitItems);
            this.groupBoxSplitString.Controls.Add(this.sBtnAdd);
            this.groupBoxSplitString.Controls.Add(this.cmbFilterString);
            this.groupBoxSplitString.Controls.Add(this.lblSplitString);
            this.groupBoxSplitString.Location = new System.Drawing.Point(13, 46);
            this.groupBoxSplitString.Name = "groupBoxSplitString";
            this.groupBoxSplitString.Size = new System.Drawing.Size(399, 132);
            this.groupBoxSplitString.TabIndex = 1;
            this.groupBoxSplitString.TabStop = false;
            // 
            // listBoxSplitItems
            // 
            this.listBoxSplitItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxSplitItems.FormattingEnabled = true;
            this.listBoxSplitItems.ItemHeight = 12;
            this.listBoxSplitItems.Location = new System.Drawing.Point(95, 50);
            this.listBoxSplitItems.Name = "listBoxSplitItems";
            this.listBoxSplitItems.Size = new System.Drawing.Size(294, 64);
            this.listBoxSplitItems.TabIndex = 5;
            // 
            // sBtnAdd
            // 
            this.sBtnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnAdd.Location = new System.Drawing.Point(320, 20);
            this.sBtnAdd.Name = "sBtnAdd";
            this.sBtnAdd.Size = new System.Drawing.Size(69, 25);
            this.sBtnAdd.TabIndex = 4;
            this.sBtnAdd.Text = "添加(&A)";
            this.sBtnAdd.UseVisualStyleBackColor = true;
            this.sBtnAdd.Click += new System.EventHandler(this.sBtnAdd_Click);
            // 
            // cmbFilterString
            // 
            this.cmbFilterString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFilterString.FormattingEnabled = true;
            this.cmbFilterString.Location = new System.Drawing.Point(95, 23);
            this.cmbFilterString.Name = "cmbFilterString";
            this.cmbFilterString.Size = new System.Drawing.Size(222, 20);
            this.cmbFilterString.TabIndex = 3;
            // 
            // lblSplitString
            // 
            this.lblSplitString.AutoSize = true;
            this.lblSplitString.Location = new System.Drawing.Point(11, 28);
            this.lblSplitString.Name = "lblSplitString";
            this.lblSplitString.Size = new System.Drawing.Size(77, 12);
            this.lblSplitString.TabIndex = 0;
            this.lblSplitString.Text = "输入分隔符：";
            // 
            // radioSplitString
            // 
            this.radioSplitString.AutoSize = true;
            this.radioSplitString.Checked = true;
            this.radioSplitString.Location = new System.Drawing.Point(19, 41);
            this.radioSplitString.Name = "radioSplitString";
            this.radioSplitString.Size = new System.Drawing.Size(95, 16);
            this.radioSplitString.TabIndex = 2;
            this.radioSplitString.TabStop = true;
            this.radioSplitString.Text = "按分隔符筛选";
            this.radioSplitString.UseVisualStyleBackColor = true;
            this.radioSplitString.CheckedChanged += new System.EventHandler(this.checkFilterString_CheckedChanged);
            // 
            // txtRegex
            // 
            this.txtRegex.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegex.Location = new System.Drawing.Point(95, 26);
            this.txtRegex.Multiline = true;
            this.txtRegex.Name = "txtRegex";
            this.txtRegex.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRegex.Size = new System.Drawing.Size(294, 132);
            this.txtRegex.TabIndex = 3;
            // 
            // sBtnOK
            // 
            this.sBtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnOK.Location = new System.Drawing.Point(208, 364);
            this.sBtnOK.Name = "sBtnOK";
            this.sBtnOK.Size = new System.Drawing.Size(94, 37);
            this.sBtnOK.TabIndex = 4;
            this.sBtnOK.Text = "确定(&O)";
            this.sBtnOK.UseVisualStyleBackColor = true;
            this.sBtnOK.Click += new System.EventHandler(this.sBtnOK_Click);
            // 
            // groupBoxRegex
            // 
            this.groupBoxRegex.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxRegex.Controls.Add(this.lblRegex);
            this.groupBoxRegex.Controls.Add(this.txtRegex);
            this.groupBoxRegex.Location = new System.Drawing.Point(13, 190);
            this.groupBoxRegex.Name = "groupBoxRegex";
            this.groupBoxRegex.Size = new System.Drawing.Size(399, 169);
            this.groupBoxRegex.TabIndex = 5;
            this.groupBoxRegex.TabStop = false;
            // 
            // lblRegex
            // 
            this.lblRegex.AutoSize = true;
            this.lblRegex.Location = new System.Drawing.Point(12, 29);
            this.lblRegex.Name = "lblRegex";
            this.lblRegex.Size = new System.Drawing.Size(77, 12);
            this.lblRegex.TabIndex = 4;
            this.lblRegex.Text = "正则表达式：";
            // 
            // radioRegex
            // 
            this.radioRegex.AutoSize = true;
            this.radioRegex.Location = new System.Drawing.Point(18, 186);
            this.radioRegex.Name = "radioRegex";
            this.radioRegex.Size = new System.Drawing.Size(119, 16);
            this.radioRegex.TabIndex = 2;
            this.radioRegex.Text = "按正则表达式筛选";
            this.radioRegex.UseVisualStyleBackColor = true;
            // 
            // sBtnCancel
            // 
            this.sBtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnCancel.Location = new System.Drawing.Point(308, 364);
            this.sBtnCancel.Name = "sBtnCancel";
            this.sBtnCancel.Size = new System.Drawing.Size(94, 37);
            this.sBtnCancel.TabIndex = 4;
            this.sBtnCancel.Text = "取消(&C)";
            this.sBtnCancel.UseVisualStyleBackColor = true;
            this.sBtnCancel.Click += new System.EventHandler(this.sBtnCancel_Click);
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
            // AddItemConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 413);
            this.Controls.Add(this.radioRegex);
            this.Controls.Add(this.radioSplitString);
            this.Controls.Add(this.groupBoxRegex);
            this.Controls.Add(this.sBtnCancel);
            this.Controls.Add(this.sBtnOK);
            this.Controls.Add(this.groupBoxSplitString);
            this.Controls.Add(this.label2);
            this.Name = "AddItemConfigForm";
            this.Text = "数据筛选设置";
            this.groupBoxSplitString.ResumeLayout(false);
            this.groupBoxSplitString.PerformLayout();
            this.groupBoxRegex.ResumeLayout(false);
            this.groupBoxRegex.PerformLayout();
            this.cMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxSplitString;
        private System.Windows.Forms.RadioButton radioSplitString;
        private System.Windows.Forms.TextBox txtRegex;
        private System.Windows.Forms.Button sBtnOK;
        private System.Windows.Forms.Label lblSplitString;
        private System.Windows.Forms.Button sBtnAdd;
        private System.Windows.Forms.ComboBox cmbFilterString;
        private System.Windows.Forms.ListBox listBoxSplitItems;
        private System.Windows.Forms.GroupBox groupBoxRegex;
        private System.Windows.Forms.RadioButton radioRegex;
        private System.Windows.Forms.Label lblRegex;
        private System.Windows.Forms.Button sBtnCancel;
        private System.Windows.Forms.ContextMenuStrip cMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 移除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移除全部ToolStripMenuItem;
    }
}