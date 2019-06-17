namespace WLib.WinCtrls.Dev.ListCtrl
{
    partial class ListBoxPlusControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListBoxPlusControl));
            this.labelShowOptions = new System.Windows.Forms.Label();
            this.labelSelectedState = new System.Windows.Forms.Label();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.sbtRefreshRegionList = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelSelectOption = new DevExpress.XtraEditors.LabelControl();
            this.numUpDwEnd = new System.Windows.Forms.NumericUpDown();
            this.numUpDwStart = new System.Windows.Forms.NumericUpDown();
            this.sBtnSelectOK = new DevExpress.XtraEditors.SimpleButton();
            this.picSearch = new System.Windows.Forms.PictureBox();
            this.sbtSelOppo = new DevExpress.XtraEditors.SimpleButton();
            this.sbtSelectClear = new DevExpress.XtraEditors.SimpleButton();
            this.sbtSelAll = new DevExpress.XtraEditors.SimpleButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.checkedListBox1 = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.cMenuCheckList = new System.Windows.Forms.ContextMenuStrip();
            this.全选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.反选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查找ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选择域ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出列表EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelSelectRegion = new DevExpress.XtraEditors.PanelControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBox1)).BeginInit();
            this.cMenuCheckList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelSelectRegion)).BeginInit();
            this.panelSelectRegion.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelShowOptions
            // 
            this.labelShowOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelShowOptions.AutoSize = true;
            this.labelShowOptions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelShowOptions.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelShowOptions.ForeColor = System.Drawing.Color.Blue;
            this.labelShowOptions.Location = new System.Drawing.Point(37, 15);
            this.labelShowOptions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelShowOptions.Name = "labelShowOptions";
            this.labelShowOptions.Size = new System.Drawing.Size(37, 15);
            this.labelShowOptions.TabIndex = 34;
            this.labelShowOptions.Text = "已选";
            this.labelShowOptions.Click += new System.EventHandler(this.labelShowOptions_Click);
            this.labelShowOptions.MouseEnter += new System.EventHandler(this.label_MouseEnter);
            this.labelShowOptions.MouseLeave += new System.EventHandler(this.label_MouseLeave);
            // 
            // labelSelectedState
            // 
            this.labelSelectedState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSelectedState.AutoSize = true;
            this.labelSelectedState.Location = new System.Drawing.Point(76, 14);
            this.labelSelectedState.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSelectedState.Name = "labelSelectedState";
            this.labelSelectedState.Size = new System.Drawing.Size(15, 15);
            this.labelSelectedState.TabIndex = 33;
            this.labelSelectedState.Text = "0";
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(7, 8);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(4);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(45, 18);
            this.lblDescription.TabIndex = 38;
            this.lblDescription.Text = "列表：";
            // 
            // sbtRefreshRegionList
            // 
            this.sbtRefreshRegionList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sbtRefreshRegionList.Location = new System.Drawing.Point(259, 2);
            this.sbtRefreshRegionList.Margin = new System.Windows.Forms.Padding(4);
            this.sbtRefreshRegionList.Name = "sbtRefreshRegionList";
            this.sbtRefreshRegionList.Size = new System.Drawing.Size(87, 29);
            this.sbtRefreshRegionList.TabIndex = 0;
            this.sbtRefreshRegionList.Text = "刷新";
            this.sbtRefreshRegionList.Click += new System.EventHandler(this.btRefreshRegionList_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(108, 4);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(15, 18);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "到";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(4, 4);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(15, 18);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "从";
            // 
            // labelSelectOption
            // 
            this.labelSelectOption.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelSelectOption.Appearance.Options.UseForeColor = true;
            this.labelSelectOption.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSelectOption.Location = new System.Drawing.Point(9, 9);
            this.labelSelectOption.Margin = new System.Windows.Forms.Padding(4);
            this.labelSelectOption.Name = "labelSelectOption";
            this.labelSelectOption.Size = new System.Drawing.Size(45, 18);
            this.labelSelectOption.TabIndex = 2;
            this.labelSelectOption.Text = "选择：";
            this.labelSelectOption.Click += new System.EventHandler(this.labelSelectOption_Click);
            this.labelSelectOption.MouseEnter += new System.EventHandler(this.label_MouseEnter);
            this.labelSelectOption.MouseLeave += new System.EventHandler(this.label_MouseLeave);
            // 
            // numUpDwEnd
            // 
            this.numUpDwEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numUpDwEnd.Location = new System.Drawing.Point(125, 1);
            this.numUpDwEnd.Margin = new System.Windows.Forms.Padding(1);
            this.numUpDwEnd.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numUpDwEnd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDwEnd.Name = "numUpDwEnd";
            this.numUpDwEnd.Size = new System.Drawing.Size(82, 25);
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
            this.numUpDwStart.Location = new System.Drawing.Point(21, 1);
            this.numUpDwStart.Margin = new System.Windows.Forms.Padding(1);
            this.numUpDwStart.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numUpDwStart.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDwStart.Name = "numUpDwStart";
            this.numUpDwStart.Size = new System.Drawing.Size(82, 25);
            this.numUpDwStart.TabIndex = 1;
            this.numUpDwStart.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // sBtnSelectOK
            // 
            this.sBtnSelectOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sBtnSelectOK.Location = new System.Drawing.Point(209, 1);
            this.sBtnSelectOK.Margin = new System.Windows.Forms.Padding(1);
            this.sBtnSelectOK.Name = "sBtnSelectOK";
            this.sBtnSelectOK.Size = new System.Drawing.Size(79, 27);
            this.sBtnSelectOK.TabIndex = 0;
            this.sBtnSelectOK.Text = "确定";
            this.sBtnSelectOK.Click += new System.EventHandler(this.sBtnSelectOK_Click);
            // 
            // picSearch
            // 
            this.picSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.picSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picSearch.Image = ((System.Drawing.Image)(resources.GetObject("picSearch.Image")));
            this.picSearch.Location = new System.Drawing.Point(9, 9);
            this.picSearch.Margin = new System.Windows.Forms.Padding(4);
            this.picSearch.Name = "picSearch";
            this.picSearch.Size = new System.Drawing.Size(25, 24);
            this.picSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSearch.TabIndex = 44;
            this.picSearch.TabStop = false;
            this.picSearch.Click += new System.EventHandler(this.picSearch_Click);
            // 
            // sbtSelOppo
            // 
            this.sbtSelOppo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbtSelOppo.Location = new System.Drawing.Point(293, 5);
            this.sbtSelOppo.Margin = new System.Windows.Forms.Padding(4);
            this.sbtSelOppo.Name = "sbtSelOppo";
            this.sbtSelOppo.Size = new System.Drawing.Size(53, 29);
            this.sbtSelOppo.TabIndex = 0;
            this.sbtSelOppo.Text = "反选";
            this.sbtSelOppo.Click += new System.EventHandler(this.btSelOppo_Click);
            // 
            // sbtSelectClear
            // 
            this.sbtSelectClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbtSelectClear.Location = new System.Drawing.Point(239, 5);
            this.sbtSelectClear.Margin = new System.Windows.Forms.Padding(4);
            this.sbtSelectClear.Name = "sbtSelectClear";
            this.sbtSelectClear.Size = new System.Drawing.Size(53, 29);
            this.sbtSelectClear.TabIndex = 0;
            this.sbtSelectClear.Text = "清空";
            this.sbtSelectClear.Click += new System.EventHandler(this.btSelectClear_Click);
            // 
            // sbtSelAll
            // 
            this.sbtSelAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbtSelAll.Location = new System.Drawing.Point(184, 5);
            this.sbtSelAll.Margin = new System.Windows.Forms.Padding(4);
            this.sbtSelAll.Name = "sbtSelAll";
            this.sbtSelAll.Size = new System.Drawing.Size(53, 29);
            this.sbtSelAll.TabIndex = 0;
            this.sbtSelAll.Text = "全选";
            this.sbtSelAll.Click += new System.EventHandler(this.btSelAll_Click);
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(346, 2);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(3, 433);
            this.panel3.TabIndex = 42;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(2, 2);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(3, 433);
            this.panel4.TabIndex = 43;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.sbtRefreshRegionList);
            this.panelControl1.Controls.Add(this.lblDescription);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(351, 34);
            this.panelControl1.TabIndex = 45;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.checkedListBox1);
            this.panelControl2.Controls.Add(this.panel3);
            this.panelControl2.Controls.Add(this.panel4);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 34);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(351, 437);
            this.panelControl2.TabIndex = 44;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Appearance.BackColor = System.Drawing.Color.White;
            this.checkedListBox1.Appearance.Options.UseBackColor = true;
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.ContextMenuStrip = this.cMenuCheckList;
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox1.Location = new System.Drawing.Point(5, 2);
            this.checkedListBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(341, 433);
            this.checkedListBox1.TabIndex = 46;
            this.checkedListBox1.ItemChecking += new DevExpress.XtraEditors.Controls.ItemCheckingEventHandler(this.checkedListBox1_ItemChecking);
            this.checkedListBox1.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            this.checkedListBox1.DrawItem += new DevExpress.XtraEditors.ListBoxDrawItemEventHandler(this.checkedListBox1_DrawItem);
            // 
            // cMenuCheckList
            // 
            this.cMenuCheckList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全选ToolStripMenuItem,
            this.清空ToolStripMenuItem,
            this.反选ToolStripMenuItem,
            this.查找ToolStripMenuItem,
            this.复制CToolStripMenuItem,
            this.选择域ToolStripMenuItem,
            this.导出列表EToolStripMenuItem});
            this.cMenuCheckList.Name = "cMenuCheckList";
            this.cMenuCheckList.Size = new System.Drawing.Size(200, 172);
            // 
            // 全选ToolStripMenuItem
            // 
            this.全选ToolStripMenuItem.Name = "全选ToolStripMenuItem";
            this.全选ToolStripMenuItem.Size = new System.Drawing.Size(199, 24);
            this.全选ToolStripMenuItem.Text = "全选(&A)";
            this.全选ToolStripMenuItem.Click += new System.EventHandler(this.btSelAll_Click);
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(199, 24);
            this.清空ToolStripMenuItem.Text = "清空(&C)";
            this.清空ToolStripMenuItem.Click += new System.EventHandler(this.btSelectClear_Click);
            // 
            // 反选ToolStripMenuItem
            // 
            this.反选ToolStripMenuItem.Name = "反选ToolStripMenuItem";
            this.反选ToolStripMenuItem.Size = new System.Drawing.Size(199, 24);
            this.反选ToolStripMenuItem.Text = "反选(&N)";
            this.反选ToolStripMenuItem.Click += new System.EventHandler(this.btSelOppo_Click);
            // 
            // 查找ToolStripMenuItem
            // 
            this.查找ToolStripMenuItem.Name = "查找ToolStripMenuItem";
            this.查找ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.查找ToolStripMenuItem.Size = new System.Drawing.Size(199, 24);
            this.查找ToolStripMenuItem.Text = "查找(&F)";
            this.查找ToolStripMenuItem.Click += new System.EventHandler(this.picSearch_Click);
            // 
            // 复制CToolStripMenuItem
            // 
            this.复制CToolStripMenuItem.Name = "复制CToolStripMenuItem";
            this.复制CToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.复制CToolStripMenuItem.Size = new System.Drawing.Size(199, 24);
            this.复制CToolStripMenuItem.Text = "复制(&C)";
            this.复制CToolStripMenuItem.Click += new System.EventHandler(this.复制CToolStripMenuItem_Click);
            // 
            // 选择域ToolStripMenuItem
            // 
            this.选择域ToolStripMenuItem.Name = "选择域ToolStripMenuItem";
            this.选择域ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.选择域ToolStripMenuItem.Size = new System.Drawing.Size(199, 24);
            this.选择域ToolStripMenuItem.Text = "选择域(&R)";
            this.选择域ToolStripMenuItem.Click += new System.EventHandler(this.labelShowOptions_Click);
            // 
            // 导出列表EToolStripMenuItem
            // 
            this.导出列表EToolStripMenuItem.Name = "导出列表EToolStripMenuItem";
            this.导出列表EToolStripMenuItem.Size = new System.Drawing.Size(199, 24);
            this.导出列表EToolStripMenuItem.Text = "导出列表(&E)...";
            this.导出列表EToolStripMenuItem.Click += new System.EventHandler(this.导出列表EToolStripMenuItem_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.picSearch);
            this.panelControl3.Controls.Add(this.sbtSelectClear);
            this.panelControl3.Controls.Add(this.labelSelectedState);
            this.panelControl3.Controls.Add(this.labelShowOptions);
            this.panelControl3.Controls.Add(this.sbtSelOppo);
            this.panelControl3.Controls.Add(this.sbtSelAll);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 510);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(351, 39);
            this.panelControl3.TabIndex = 46;
            // 
            // panelSelectRegion
            // 
            this.panelSelectRegion.Controls.Add(this.tableLayoutPanel1);
            this.panelSelectRegion.Controls.Add(this.labelSelectOption);
            this.panelSelectRegion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSelectRegion.Location = new System.Drawing.Point(0, 471);
            this.panelSelectRegion.Margin = new System.Windows.Forms.Padding(4);
            this.panelSelectRegion.Name = "panelSelectRegion";
            this.panelSelectRegion.Size = new System.Drawing.Size(351, 39);
            this.panelSelectRegion.TabIndex = 47;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.Controls.Add(this.sBtnSelectOK, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.numUpDwStart, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelControl2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.numUpDwEnd, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelControl3, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(59, 5);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(289, 29);
            this.tableLayoutPanel1.TabIndex = 47;
            // 
            // ListBoxPlusControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelSelectRegion);
            this.Controls.Add(this.panelControl3);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ListBoxPlusControl";
            this.Size = new System.Drawing.Size(351, 549);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBox1)).EndInit();
            this.cMenuCheckList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelSelectRegion)).EndInit();
            this.panelSelectRegion.ResumeLayout(false);
            this.panelSelectRegion.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelShowOptions;
        private System.Windows.Forms.Label labelSelectedState;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox picSearch;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelSelectOption;
        private System.Windows.Forms.NumericUpDown numUpDwEnd;
        private System.Windows.Forms.NumericUpDown numUpDwStart;
        private DevExpress.XtraEditors.SimpleButton sBtnSelectOK;
        private DevExpress.XtraEditors.SimpleButton sbtSelAll;
        private DevExpress.XtraEditors.SimpleButton sbtSelOppo;
        private DevExpress.XtraEditors.SimpleButton sbtSelectClear;
        private DevExpress.XtraEditors.SimpleButton sbtRefreshRegionList;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl lblDescription;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBox1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelSelectRegion;
        private System.Windows.Forms.ToolStripMenuItem 全选ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 反选ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查找ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选择域ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制CToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem 导出列表EToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.ContextMenuStrip cMenuCheckList;
    }
}
