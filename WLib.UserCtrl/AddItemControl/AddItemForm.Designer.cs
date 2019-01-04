namespace WLib.UserCtrls.AddItemControl
{
    partial class AddItemForm
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
            this.lblTips = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.sBtnAdd = new System.Windows.Forms.Button();
            this.listBoxCtrlItems = new System.Windows.Forms.ListBox();
            this.lblFilterItemTips = new System.Windows.Forms.Label();
            this.sBtnCancel = new System.Windows.Forms.Button();
            this.sBtnSettings = new System.Windows.Forms.Button();
            this.sBtnOK = new System.Windows.Forms.Button();
            this.sBtnClear = new System.Windows.Forms.Button();
            this.sBtnRemoveRepeat = new System.Windows.Forms.Button();
            this.cMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.移除选中项DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移除全部CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.移除重复项RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.复制选中项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制全部ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTips
            // 
            this.lblTips.AutoSize = true;
            this.lblTips.Location = new System.Drawing.Point(12, 9);
            this.lblTips.Name = "lblTips";
            this.lblTips.Size = new System.Drawing.Size(113, 12);
            this.lblTips.TabIndex = 0;
            this.lblTips.Text = "从以下文字中筛选：";
            // 
            // txtInput
            // 
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput.Location = new System.Drawing.Point(14, 27);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInput.Size = new System.Drawing.Size(396, 98);
            this.txtInput.TabIndex = 1;
            // 
            // sBtnAdd
            // 
            this.sBtnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnAdd.Location = new System.Drawing.Point(301, 129);
            this.sBtnAdd.Name = "sBtnAdd";
            this.sBtnAdd.Size = new System.Drawing.Size(109, 35);
            this.sBtnAdd.TabIndex = 2;
            this.sBtnAdd.Text = "添加(&A)";
            this.sBtnAdd.UseVisualStyleBackColor = true;
            this.sBtnAdd.Click += new System.EventHandler(this.sBtnAdd_Click);
            // 
            // listBoxCtrlItems
            // 
            this.listBoxCtrlItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxCtrlItems.FormattingEnabled = true;
            this.listBoxCtrlItems.ItemHeight = 12;
            this.listBoxCtrlItems.Location = new System.Drawing.Point(14, 172);
            this.listBoxCtrlItems.Name = "listBoxCtrlItems";
            this.listBoxCtrlItems.Size = new System.Drawing.Size(396, 160);
            this.listBoxCtrlItems.TabIndex = 3;
            this.listBoxCtrlItems.SelectedIndexChanged += new System.EventHandler(this.listBoxCtrlItems_SelectedIndexChanged);
            // 
            // lblFilterItemTips
            // 
            this.lblFilterItemTips.AutoSize = true;
            this.lblFilterItemTips.Location = new System.Drawing.Point(12, 154);
            this.lblFilterItemTips.Name = "lblFilterItemTips";
            this.lblFilterItemTips.Size = new System.Drawing.Size(65, 12);
            this.lblFilterItemTips.TabIndex = 0;
            this.lblFilterItemTips.Text = "获取的项：";
            // 
            // sBtnCancel
            // 
            this.sBtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sBtnCancel.Location = new System.Drawing.Point(301, 396);
            this.sBtnCancel.Name = "sBtnCancel";
            this.sBtnCancel.Size = new System.Drawing.Size(109, 35);
            this.sBtnCancel.TabIndex = 2;
            this.sBtnCancel.Text = "取消(&C)";
            this.sBtnCancel.UseVisualStyleBackColor = true;
            this.sBtnCancel.Click += new System.EventHandler(this.sBtnCancel_Click);
            // 
            // sBtnSettings
            // 
            this.sBtnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sBtnSettings.Location = new System.Drawing.Point(14, 396);
            this.sBtnSettings.Name = "sBtnSettings";
            this.sBtnSettings.Size = new System.Drawing.Size(109, 35);
            this.sBtnSettings.TabIndex = 2;
            this.sBtnSettings.Text = "筛选规则设置(&S)";
            this.sBtnSettings.UseVisualStyleBackColor = true;
            this.sBtnSettings.Click += new System.EventHandler(this.sBtnSettings_Click);
            // 
            // sBtnOK
            // 
            this.sBtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnOK.Enabled = false;
            this.sBtnOK.Location = new System.Drawing.Point(186, 396);
            this.sBtnOK.Name = "sBtnOK";
            this.sBtnOK.Size = new System.Drawing.Size(109, 35);
            this.sBtnOK.TabIndex = 2;
            this.sBtnOK.Text = "确定(&O)";
            this.sBtnOK.UseVisualStyleBackColor = true;
            this.sBtnOK.Click += new System.EventHandler(this.sBtnOK_Click);
            // 
            // sBtnClear
            // 
            this.sBtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnClear.Enabled = false;
            this.sBtnClear.Location = new System.Drawing.Point(314, 348);
            this.sBtnClear.Name = "sBtnClear";
            this.sBtnClear.Size = new System.Drawing.Size(96, 29);
            this.sBtnClear.TabIndex = 2;
            this.sBtnClear.Text = "移除全部(&C)";
            this.sBtnClear.UseVisualStyleBackColor = true;
            this.sBtnClear.Click += new System.EventHandler(this.sBtnClear_Click);
            // 
            // sBtnRemoveRepeat
            // 
            this.sBtnRemoveRepeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnRemoveRepeat.Enabled = false;
            this.sBtnRemoveRepeat.Location = new System.Drawing.Point(212, 348);
            this.sBtnRemoveRepeat.Name = "sBtnRemoveRepeat";
            this.sBtnRemoveRepeat.Size = new System.Drawing.Size(96, 29);
            this.sBtnRemoveRepeat.TabIndex = 2;
            this.sBtnRemoveRepeat.Text = "移除重复项(&R)";
            this.sBtnRemoveRepeat.UseVisualStyleBackColor = true;
            this.sBtnRemoveRepeat.Click += new System.EventHandler(this.sBtnRemoveRepeat_Click);
            // 
            // cMenuStrip1
            // 
            this.cMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.移除选中项DToolStripMenuItem,
            this.移除全部CToolStripMenuItem,
            this.toolStripSeparator1,
            this.移除重复项RToolStripMenuItem,
            this.toolStripSeparator2,
            this.复制选中项ToolStripMenuItem,
            this.复制全部ToolStripMenuItem});
            this.cMenuStrip1.Name = "cMenuStrip1";
            this.cMenuStrip1.Size = new System.Drawing.Size(154, 126);
            // 
            // 移除选中项DToolStripMenuItem
            // 
            this.移除选中项DToolStripMenuItem.Enabled = false;
            this.移除选中项DToolStripMenuItem.Name = "移除选中项DToolStripMenuItem";
            this.移除选中项DToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.移除选中项DToolStripMenuItem.Text = "移除选中项(&D)";
            this.移除选中项DToolStripMenuItem.Click += new System.EventHandler(this.移除选中项DToolStripMenuItem_Click);
            // 
            // 移除全部CToolStripMenuItem
            // 
            this.移除全部CToolStripMenuItem.Enabled = false;
            this.移除全部CToolStripMenuItem.Name = "移除全部CToolStripMenuItem";
            this.移除全部CToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.移除全部CToolStripMenuItem.Text = "移除全部(&C)";
            this.移除全部CToolStripMenuItem.Click += new System.EventHandler(this.sBtnClear_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
            // 
            // 移除重复项RToolStripMenuItem
            // 
            this.移除重复项RToolStripMenuItem.Enabled = false;
            this.移除重复项RToolStripMenuItem.Name = "移除重复项RToolStripMenuItem";
            this.移除重复项RToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.移除重复项RToolStripMenuItem.Text = "移除重复项(&R)";
            this.移除重复项RToolStripMenuItem.Click += new System.EventHandler(this.sBtnRemoveRepeat_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(150, 6);
            // 
            // 复制选中项ToolStripMenuItem
            // 
            this.复制选中项ToolStripMenuItem.Enabled = false;
            this.复制选中项ToolStripMenuItem.Name = "复制选中项ToolStripMenuItem";
            this.复制选中项ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.复制选中项ToolStripMenuItem.Text = "复制选中项";
            this.复制选中项ToolStripMenuItem.Click += new System.EventHandler(this.复制选中项ToolStripMenuItem_Click);
            // 
            // 复制全部ToolStripMenuItem
            // 
            this.复制全部ToolStripMenuItem.Enabled = false;
            this.复制全部ToolStripMenuItem.Name = "复制全部ToolStripMenuItem";
            this.复制全部ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.复制全部ToolStripMenuItem.Text = "复制全部";
            this.复制全部ToolStripMenuItem.Click += new System.EventHandler(this.复制全部ToolStripMenuItem_Click);
            // 
            // AddItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.sBtnCancel;
            this.ClientSize = new System.Drawing.Size(422, 441);
            this.Controls.Add(this.listBoxCtrlItems);
            this.Controls.Add(this.sBtnOK);
            this.Controls.Add(this.sBtnSettings);
            this.Controls.Add(this.sBtnRemoveRepeat);
            this.Controls.Add(this.sBtnClear);
            this.Controls.Add(this.sBtnCancel);
            this.Controls.Add(this.sBtnAdd);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.lblFilterItemTips);
            this.Controls.Add(this.lblTips);
            this.Name = "AddItemForm";
            this.Text = "添加项";
            this.cMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip cMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 移除选中项DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移除全部CToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 移除重复项RToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 复制选中项ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制全部ToolStripMenuItem;
        protected internal System.Windows.Forms.Button sBtnCancel;
        protected internal System.Windows.Forms.Button sBtnSettings;
        protected internal System.Windows.Forms.Button sBtnOK;
        protected internal System.Windows.Forms.Button sBtnClear;
        protected internal System.Windows.Forms.Button sBtnRemoveRepeat;
        protected internal System.Windows.Forms.Label lblTips;
        protected internal System.Windows.Forms.TextBox txtInput;
        protected internal System.Windows.Forms.Button sBtnAdd;
        protected internal System.Windows.Forms.ListBox listBoxCtrlItems;
        protected internal System.Windows.Forms.Label lblFilterItemTips;
    }
}