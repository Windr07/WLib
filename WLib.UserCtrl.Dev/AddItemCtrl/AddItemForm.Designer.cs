namespace WLib.UserCtrls.Dev.AddItemCtrl
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
            this.lblTips = new DevExpress.XtraEditors.LabelControl();
            this.txtInput = new DevExpress.XtraEditors.MemoEdit();
            this.lblFilterItemTips = new DevExpress.XtraEditors.LabelControl();
            this.sBtnOK = new DevExpress.XtraEditors.SimpleButton();
            this.sBtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.listBoxCtrlItems = new DevExpress.XtraEditors.ListBoxControl();
            this.cMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.移除选中项DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移除全部CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.移除重复项RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.复制选中项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制全部ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sBtnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.sBtnClear = new DevExpress.XtraEditors.SimpleButton();
            this.sBtnRemoveRepeat = new DevExpress.XtraEditors.SimpleButton();
            this.sBtnSettings = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtInput.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxCtrlItems)).BeginInit();
            this.cMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTips
            // 
            this.lblTips.Location = new System.Drawing.Point(10, 8);
            this.lblTips.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblTips.Name = "lblTips";
            this.lblTips.Size = new System.Drawing.Size(108, 14);
            this.lblTips.TabIndex = 0;
            this.lblTips.Text = "从以下文字中筛选：";
            // 
            // txtInput
            // 
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput.Location = new System.Drawing.Point(10, 29);
            this.txtInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(402, 95);
            this.txtInput.TabIndex = 1;
            // 
            // lblFilterItemTips
            // 
            this.lblFilterItemTips.Location = new System.Drawing.Point(10, 150);
            this.lblFilterItemTips.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblFilterItemTips.Name = "lblFilterItemTips";
            this.lblFilterItemTips.Size = new System.Drawing.Size(60, 14);
            this.lblFilterItemTips.TabIndex = 0;
            this.lblFilterItemTips.Text = "获取的项：";
            // 
            // sBtnOK
            // 
            this.sBtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnOK.Enabled = false;
            this.sBtnOK.Location = new System.Drawing.Point(295, 395);
            this.sBtnOK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sBtnOK.Name = "sBtnOK";
            this.sBtnOK.Size = new System.Drawing.Size(117, 34);
            this.sBtnOK.TabIndex = 3;
            this.sBtnOK.Text = "确定(&O)";
            this.sBtnOK.Click += new System.EventHandler(this.sBtnOK_Click);
            // 
            // sBtnCancel
            // 
            this.sBtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sBtnCancel.Location = new System.Drawing.Point(173, 395);
            this.sBtnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sBtnCancel.Name = "sBtnCancel";
            this.sBtnCancel.Size = new System.Drawing.Size(117, 34);
            this.sBtnCancel.TabIndex = 3;
            this.sBtnCancel.Text = "取消(&C)";
            this.sBtnCancel.Click += new System.EventHandler(this.sBtnCancel_Click);
            // 
            // listBoxCtrlItems
            // 
            this.listBoxCtrlItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxCtrlItems.ContextMenuStrip = this.cMenuStrip1;
            this.listBoxCtrlItems.Location = new System.Drawing.Point(10, 170);
            this.listBoxCtrlItems.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxCtrlItems.Name = "listBoxCtrlItems";
            this.listBoxCtrlItems.Size = new System.Drawing.Size(401, 175);
            this.listBoxCtrlItems.TabIndex = 2;
            this.listBoxCtrlItems.SelectedIndexChanged += new System.EventHandler(this.listBoxCtrlItems_SelectedIndexChanged);
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
            // sBtnAdd
            // 
            this.sBtnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnAdd.Location = new System.Drawing.Point(295, 128);
            this.sBtnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sBtnAdd.Name = "sBtnAdd";
            this.sBtnAdd.Size = new System.Drawing.Size(117, 34);
            this.sBtnAdd.TabIndex = 3;
            this.sBtnAdd.Text = "添加(&A)";
            this.sBtnAdd.Click += new System.EventHandler(this.sBtnAdd_Click);
            // 
            // sBtnClear
            // 
            this.sBtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnClear.Enabled = false;
            this.sBtnClear.Location = new System.Drawing.Point(310, 350);
            this.sBtnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sBtnClear.Name = "sBtnClear";
            this.sBtnClear.Size = new System.Drawing.Size(102, 29);
            this.sBtnClear.TabIndex = 3;
            this.sBtnClear.Text = "移除全部(&C)";
            this.sBtnClear.Click += new System.EventHandler(this.sBtnClear_Click);
            // 
            // sBtnRemoveRepeat
            // 
            this.sBtnRemoveRepeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnRemoveRepeat.Enabled = false;
            this.sBtnRemoveRepeat.Location = new System.Drawing.Point(197, 350);
            this.sBtnRemoveRepeat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sBtnRemoveRepeat.Name = "sBtnRemoveRepeat";
            this.sBtnRemoveRepeat.Size = new System.Drawing.Size(107, 29);
            this.sBtnRemoveRepeat.TabIndex = 3;
            this.sBtnRemoveRepeat.Text = "移除重复项(&R)";
            this.sBtnRemoveRepeat.Click += new System.EventHandler(this.sBtnRemoveRepeat_Click);
            // 
            // sBtnSettings
            // 
            this.sBtnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sBtnSettings.Location = new System.Drawing.Point(10, 395);
            this.sBtnSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sBtnSettings.Name = "sBtnSettings";
            this.sBtnSettings.Size = new System.Drawing.Size(117, 34);
            this.sBtnSettings.TabIndex = 3;
            this.sBtnSettings.Text = "设置(&S)";
            this.sBtnSettings.Click += new System.EventHandler(this.sBtnSettings_Click);
            // 
            // AddItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.sBtnCancel;
            this.ClientSize = new System.Drawing.Size(422, 441);
            this.Controls.Add(this.sBtnRemoveRepeat);
            this.Controls.Add(this.sBtnClear);
            this.Controls.Add(this.sBtnAdd);
            this.Controls.Add(this.sBtnCancel);
            this.Controls.Add(this.sBtnSettings);
            this.Controls.Add(this.sBtnOK);
            this.Controls.Add(this.listBoxCtrlItems);
            this.Controls.Add(this.lblFilterItemTips);
            this.Controls.Add(this.lblTips);
            this.Controls.Add(this.txtInput);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddItemForm";
            this.Text = "添加项";
            ((System.ComponentModel.ISupportInitialize)(this.txtInput.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxCtrlItems)).EndInit();
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
        protected internal DevExpress.XtraEditors.LabelControl lblTips;
        protected internal DevExpress.XtraEditors.MemoEdit txtInput;
        protected internal DevExpress.XtraEditors.LabelControl lblFilterItemTips;
        protected internal DevExpress.XtraEditors.SimpleButton sBtnOK;
        protected internal DevExpress.XtraEditors.SimpleButton sBtnCancel;
        protected internal DevExpress.XtraEditors.ListBoxControl listBoxCtrlItems;
        protected internal DevExpress.XtraEditors.SimpleButton sBtnAdd;
        protected internal DevExpress.XtraEditors.SimpleButton sBtnClear;
        protected internal DevExpress.XtraEditors.SimpleButton sBtnRemoveRepeat;
        protected internal DevExpress.XtraEditors.SimpleButton sBtnSettings;
    }
}