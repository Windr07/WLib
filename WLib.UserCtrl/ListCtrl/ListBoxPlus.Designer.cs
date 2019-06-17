namespace WLib.WinCtrls.ListCtrl
{
    partial class ListBoxPlus
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListBoxPlus));
            this.panel1 = new System.Windows.Forms.Panel();
            this.sbtRefreshRegionList = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panelSelectRegion = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.sBtnSelectOK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.numUpDwEnd = new System.Windows.Forms.NumericUpDown();
            this.numUpDwStart = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.labelSelectOption = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.sbtSelOppo = new System.Windows.Forms.Button();
            this.sbtSelectClear = new System.Windows.Forms.Button();
            this.sbtSelAll = new System.Windows.Forms.Button();
            this.picSearch = new System.Windows.Forms.PictureBox();
            this.labelSelectedState = new System.Windows.Forms.Label();
            this.labelShowOptions = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.ListMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.全选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.反选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查找ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选择域ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出列表EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panelSelectRegion.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwStart)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearch)).BeginInit();
            this.ListMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.sbtRefreshRegionList);
            this.panel1.Controls.Add(this.lblDescription);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(261, 27);
            this.panel1.TabIndex = 0;
            // 
            // sbtRefreshRegionList
            // 
            this.sbtRefreshRegionList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sbtRefreshRegionList.Location = new System.Drawing.Point(192, 2);
            this.sbtRefreshRegionList.Name = "sbtRefreshRegionList";
            this.sbtRefreshRegionList.Size = new System.Drawing.Size(65, 23);
            this.sbtRefreshRegionList.TabIndex = 1;
            this.sbtRefreshRegionList.Text = "刷新";
            this.sbtRefreshRegionList.UseVisualStyleBackColor = true;
            this.sbtRefreshRegionList.Click += new System.EventHandler(this.btRefreshRegionList_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(5, 8);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(41, 12);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "列表：";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(2, 348);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(259, 27);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(2, 348);
            this.panel3.TabIndex = 1;
            // 
            // panelSelectRegion
            // 
            this.panelSelectRegion.Controls.Add(this.tableLayoutPanel1);
            this.panelSelectRegion.Controls.Add(this.labelSelectOption);
            this.panelSelectRegion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSelectRegion.Location = new System.Drawing.Point(0, 375);
            this.panelSelectRegion.Name = "panelSelectRegion";
            this.panelSelectRegion.Size = new System.Drawing.Size(261, 31);
            this.panelSelectRegion.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.tableLayoutPanel1.Controls.Add(this.sBtnSelectOK, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.numUpDwEnd, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.numUpDwStart, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(42, 3);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(217, 23);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // sBtnSelectOK
            // 
            this.sBtnSelectOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sBtnSelectOK.Location = new System.Drawing.Point(157, 1);
            this.sBtnSelectOK.Margin = new System.Windows.Forms.Padding(1);
            this.sBtnSelectOK.Name = "sBtnSelectOK";
            this.sBtnSelectOK.Size = new System.Drawing.Size(59, 21);
            this.sBtnSelectOK.TabIndex = 1;
            this.sBtnSelectOK.Text = "确定";
            this.sBtnSelectOK.UseVisualStyleBackColor = true;
            this.sBtnSelectOK.Click += new System.EventHandler(this.sBtnSelectOK_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label4.Location = new System.Drawing.Point(0, 3);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "从";
            // 
            // numUpDwEnd
            // 
            this.numUpDwEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numUpDwEnd.Location = new System.Drawing.Point(94, 1);
            this.numUpDwEnd.Margin = new System.Windows.Forms.Padding(1);
            this.numUpDwEnd.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numUpDwEnd.Name = "numUpDwEnd";
            this.numUpDwEnd.Size = new System.Drawing.Size(61, 21);
            this.numUpDwEnd.TabIndex = 1;
            this.numUpDwEnd.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numUpDwStart
            // 
            this.numUpDwStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numUpDwStart.Location = new System.Drawing.Point(16, 1);
            this.numUpDwStart.Margin = new System.Windows.Forms.Padding(1);
            this.numUpDwStart.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numUpDwStart.Name = "numUpDwStart";
            this.numUpDwStart.Size = new System.Drawing.Size(61, 21);
            this.numUpDwStart.TabIndex = 1;
            this.numUpDwStart.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(78, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "到";
            // 
            // labelSelectOption
            // 
            this.labelSelectOption.AutoSize = true;
            this.labelSelectOption.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSelectOption.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelSelectOption.ForeColor = System.Drawing.Color.Blue;
            this.labelSelectOption.Location = new System.Drawing.Point(7, 7);
            this.labelSelectOption.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.labelSelectOption.Name = "labelSelectOption";
            this.labelSelectOption.Size = new System.Drawing.Size(43, 14);
            this.labelSelectOption.TabIndex = 0;
            this.labelSelectOption.Text = "选择：";
            this.labelSelectOption.Click += new System.EventHandler(this.labelSelectOption_Click);
            this.labelSelectOption.MouseEnter += new System.EventHandler(this.label_MouseEnter);
            this.labelSelectOption.MouseLeave += new System.EventHandler(this.label_MouseLeave);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.sbtSelOppo);
            this.panel5.Controls.Add(this.sbtSelectClear);
            this.panel5.Controls.Add(this.sbtSelAll);
            this.panel5.Controls.Add(this.picSearch);
            this.panel5.Controls.Add(this.labelSelectedState);
            this.panel5.Controls.Add(this.labelShowOptions);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 406);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(261, 31);
            this.panel5.TabIndex = 2;
            // 
            // sbtSelOppo
            // 
            this.sbtSelOppo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbtSelOppo.Location = new System.Drawing.Point(218, 4);
            this.sbtSelOppo.Name = "sbtSelOppo";
            this.sbtSelOppo.Size = new System.Drawing.Size(40, 23);
            this.sbtSelOppo.TabIndex = 1;
            this.sbtSelOppo.Text = "反选";
            this.sbtSelOppo.UseVisualStyleBackColor = true;
            this.sbtSelOppo.Click += new System.EventHandler(this.btSelOppo_Click);
            // 
            // sbtSelectClear
            // 
            this.sbtSelectClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbtSelectClear.Location = new System.Drawing.Point(177, 4);
            this.sbtSelectClear.Name = "sbtSelectClear";
            this.sbtSelectClear.Size = new System.Drawing.Size(40, 23);
            this.sbtSelectClear.TabIndex = 1;
            this.sbtSelectClear.Text = "清空";
            this.sbtSelectClear.UseVisualStyleBackColor = true;
            this.sbtSelectClear.Click += new System.EventHandler(this.btSelectClear_Click);
            // 
            // sbtSelAll
            // 
            this.sbtSelAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbtSelAll.Location = new System.Drawing.Point(136, 4);
            this.sbtSelAll.Name = "sbtSelAll";
            this.sbtSelAll.Size = new System.Drawing.Size(40, 23);
            this.sbtSelAll.TabIndex = 1;
            this.sbtSelAll.Text = "全选";
            this.sbtSelAll.UseVisualStyleBackColor = true;
            this.sbtSelAll.Click += new System.EventHandler(this.btSelAll_Click);
            // 
            // picSearch
            // 
            this.picSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.picSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picSearch.Image = ((System.Drawing.Image)(resources.GetObject("picSearch.Image")));
            this.picSearch.Location = new System.Drawing.Point(7, 7);
            this.picSearch.Name = "picSearch";
            this.picSearch.Size = new System.Drawing.Size(19, 19);
            this.picSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSearch.TabIndex = 45;
            this.picSearch.TabStop = false;
            this.picSearch.Click += new System.EventHandler(this.picSearch_Click);
            // 
            // labelSelectedState
            // 
            this.labelSelectedState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSelectedState.AutoSize = true;
            this.labelSelectedState.Location = new System.Drawing.Point(57, 11);
            this.labelSelectedState.Name = "labelSelectedState";
            this.labelSelectedState.Size = new System.Drawing.Size(11, 12);
            this.labelSelectedState.TabIndex = 0;
            this.labelSelectedState.Text = "0";
            // 
            // labelShowOptions
            // 
            this.labelShowOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelShowOptions.AutoSize = true;
            this.labelShowOptions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelShowOptions.ForeColor = System.Drawing.Color.Blue;
            this.labelShowOptions.Location = new System.Drawing.Point(28, 12);
            this.labelShowOptions.Name = "labelShowOptions";
            this.labelShowOptions.Size = new System.Drawing.Size(29, 12);
            this.labelShowOptions.TabIndex = 0;
            this.labelShowOptions.Text = "已选";
            this.labelShowOptions.Click += new System.EventHandler(this.labelShowOptions_Click);
            this.labelShowOptions.MouseEnter += new System.EventHandler(this.label_MouseEnter);
            this.labelShowOptions.MouseLeave += new System.EventHandler(this.label_MouseLeave);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.ContextMenuStrip = this.ListMenuStrip;
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(2, 27);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(257, 348);
            this.checkedListBox1.TabIndex = 3;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // ListMenuStrip
            // 
            this.ListMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全选ToolStripMenuItem,
            this.清空ToolStripMenuItem,
            this.反选ToolStripMenuItem,
            this.查找ToolStripMenuItem,
            this.复制CToolStripMenuItem,
            this.选择域ToolStripMenuItem,
            this.导出列表EToolStripMenuItem});
            this.ListMenuStrip.Name = "cMenuCheckList";
            this.ListMenuStrip.Size = new System.Drawing.Size(181, 180);
            // 
            // 全选ToolStripMenuItem
            // 
            this.全选ToolStripMenuItem.Name = "全选ToolStripMenuItem";
            this.全选ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.全选ToolStripMenuItem.Text = "全选(&A)";
            this.全选ToolStripMenuItem.Click += new System.EventHandler(this.btSelAll_Click);
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.清空ToolStripMenuItem.Text = "清空(&C)";
            this.清空ToolStripMenuItem.Click += new System.EventHandler(this.btSelectClear_Click);
            // 
            // 反选ToolStripMenuItem
            // 
            this.反选ToolStripMenuItem.Name = "反选ToolStripMenuItem";
            this.反选ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.反选ToolStripMenuItem.Text = "反选(&N)";
            this.反选ToolStripMenuItem.Click += new System.EventHandler(this.btSelOppo_Click);
            // 
            // 查找ToolStripMenuItem
            // 
            this.查找ToolStripMenuItem.Name = "查找ToolStripMenuItem";
            this.查找ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.查找ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.查找ToolStripMenuItem.Text = "查找(&F)";
            this.查找ToolStripMenuItem.Click += new System.EventHandler(this.picSearch_Click);
            // 
            // 复制CToolStripMenuItem
            // 
            this.复制CToolStripMenuItem.Name = "复制CToolStripMenuItem";
            this.复制CToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.复制CToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.复制CToolStripMenuItem.Text = "复制(&C)";
            this.复制CToolStripMenuItem.Click += new System.EventHandler(this.复制CToolStripMenuItem_Click);
            // 
            // 选择域ToolStripMenuItem
            // 
            this.选择域ToolStripMenuItem.Name = "选择域ToolStripMenuItem";
            this.选择域ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.选择域ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.选择域ToolStripMenuItem.Text = "选择域(&R)";
            this.选择域ToolStripMenuItem.Click += new System.EventHandler(this.labelShowOptions_Click);
            // 
            // 导出列表EToolStripMenuItem
            // 
            this.导出列表EToolStripMenuItem.Name = "导出列表EToolStripMenuItem";
            this.导出列表EToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.导出列表EToolStripMenuItem.Text = "导出列表(&E)...";
            this.导出列表EToolStripMenuItem.Click += new System.EventHandler(this.导出列表EToolStripMenuItem_Click);
            // 
            // ListBoxPlus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelSelectRegion);
            this.Controls.Add(this.panel5);
            this.Name = "ListBoxPlus";
            this.Size = new System.Drawing.Size(261, 437);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelSelectRegion.ResumeLayout(false);
            this.panelSelectRegion.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwStart)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearch)).EndInit();
            this.ListMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panelSelectRegion;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button sbtRefreshRegionList;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.NumericUpDown numUpDwStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelSelectOption;
        private System.Windows.Forms.NumericUpDown numUpDwEnd;
        private System.Windows.Forms.Button sBtnSelectOK;
        private System.Windows.Forms.PictureBox picSearch;
        private System.Windows.Forms.Label labelSelectedState;
        private System.Windows.Forms.Label labelShowOptions;
        private System.Windows.Forms.Button sbtSelAll;
        private System.Windows.Forms.Button sbtSelOppo;
        private System.Windows.Forms.Button sbtSelectClear;
        private System.Windows.Forms.ToolStripMenuItem 全选ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 反选ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查找ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选择域ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem 导出列表EToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.ContextMenuStrip ListMenuStrip;
    }
}
