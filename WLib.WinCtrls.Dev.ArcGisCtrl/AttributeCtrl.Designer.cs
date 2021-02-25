namespace WLib.WinCtrls.Dev.ArcGisCtrl
{
    partial class AttributeCtrl
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SplitContainerCtrl = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupCtrlVisibleFields = new DevExpress.XtraEditors.GroupControl();
            this.txtSearchFields = new DevExpress.XtraEditors.TextEdit();
            this.ListBoxFields = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.全选AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.反选NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridCtrlData = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.paginationBar = new WLib.WinCtrls.GridViewCtrl.PaginationBar();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerCtrl)).BeginInit();
            this.SplitContainerCtrl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupCtrlVisibleFields)).BeginInit();
            this.groupCtrlVisibleFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchFields.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListBoxFields)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCtrlData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // SplitContainerCtrl
            // 
            this.SplitContainerCtrl.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.SplitContainerCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainerCtrl.Location = new System.Drawing.Point(0, 0);
            this.SplitContainerCtrl.Name = "SplitContainerCtrl";
            this.SplitContainerCtrl.Panel1.Controls.Add(this.groupCtrlVisibleFields);
            this.SplitContainerCtrl.Panel1.Text = "FieldPanel";
            this.SplitContainerCtrl.Panel2.Controls.Add(this.gridCtrlData);
            this.SplitContainerCtrl.Panel2.Controls.Add(this.paginationBar);
            this.SplitContainerCtrl.Panel2.Text = "DataPanel";
            this.SplitContainerCtrl.Size = new System.Drawing.Size(866, 516);
            this.SplitContainerCtrl.SplitterPosition = 173;
            this.SplitContainerCtrl.TabIndex = 2;
            this.SplitContainerCtrl.Text = "splitContainerControl1";
            // 
            // groupCtrlVisibleFields
            // 
            this.groupCtrlVisibleFields.Controls.Add(this.txtSearchFields);
            this.groupCtrlVisibleFields.Controls.Add(this.ListBoxFields);
            this.groupCtrlVisibleFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupCtrlVisibleFields.Location = new System.Drawing.Point(0, 0);
            this.groupCtrlVisibleFields.Name = "groupCtrlVisibleFields";
            this.groupCtrlVisibleFields.Size = new System.Drawing.Size(173, 516);
            this.groupCtrlVisibleFields.TabIndex = 2;
            this.groupCtrlVisibleFields.Text = "显示字段";
            // 
            // txtSearchFields
            // 
            this.txtSearchFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchFields.Location = new System.Drawing.Point(73, 1);
            this.txtSearchFields.Name = "txtSearchFields";
            this.txtSearchFields.Size = new System.Drawing.Size(97, 21);
            this.txtSearchFields.TabIndex = 1;
            this.txtSearchFields.EditValueChanged += new System.EventHandler(this.txtSearchFields_EditValueChanged);
            // 
            // ListBoxFields
            // 
            this.ListBoxFields.ContextMenuStrip = this.contextMenuStrip1;
            this.ListBoxFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListBoxFields.Location = new System.Drawing.Point(2, 23);
            this.ListBoxFields.Name = "ListBoxFields";
            this.ListBoxFields.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ListBoxFields.Size = new System.Drawing.Size(169, 491);
            this.ListBoxFields.TabIndex = 0;
            this.ListBoxFields.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.CListBoxFields_ItemCheck);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全选AToolStripMenuItem,
            this.清空EToolStripMenuItem,
            this.反选NToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 70);
            // 
            // 全选AToolStripMenuItem
            // 
            this.全选AToolStripMenuItem.Name = "全选AToolStripMenuItem";
            this.全选AToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.全选AToolStripMenuItem.Text = "全选(&A)";
            this.全选AToolStripMenuItem.Click += new System.EventHandler(this.全选ToolStripMenuItem_Click);
            // 
            // 清空EToolStripMenuItem
            // 
            this.清空EToolStripMenuItem.Name = "清空EToolStripMenuItem";
            this.清空EToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.清空EToolStripMenuItem.Text = "清空(&E)";
            this.清空EToolStripMenuItem.Click += new System.EventHandler(this.清空ToolStripMenuItem_Click);
            // 
            // 反选NToolStripMenuItem
            // 
            this.反选NToolStripMenuItem.Name = "反选NToolStripMenuItem";
            this.反选NToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.反选NToolStripMenuItem.Text = "反选(&N)";
            this.反选NToolStripMenuItem.Click += new System.EventHandler(this.反选ToolStripMenuItem_Click);
            // 
            // gridCtrlData
            // 
            this.gridCtrlData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCtrlData.Location = new System.Drawing.Point(0, 29);
            this.gridCtrlData.MainView = this.gridView1;
            this.gridCtrlData.Name = "gridCtrlData";
            this.gridCtrlData.Size = new System.Drawing.Size(687, 487);
            this.gridCtrlData.TabIndex = 0;
            this.gridCtrlData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridCtrlData;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
            // 
            // paginationBar
            // 
            this.paginationBar.BackColor = System.Drawing.Color.Transparent;
            this.paginationBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.paginationBar.Location = new System.Drawing.Point(0, 0);
            this.paginationBar.Name = "paginationBar";
            this.paginationBar.PageRecordCnt = 200;
            this.paginationBar.Size = new System.Drawing.Size(687, 29);
            this.paginationBar.TabIndex = 9;
            // 
            // AttributeCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SplitContainerCtrl);
            this.Name = "AttributeCtrl";
            this.Size = new System.Drawing.Size(866, 516);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerCtrl)).EndInit();
            this.SplitContainerCtrl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupCtrlVisibleFields)).EndInit();
            this.groupCtrlVisibleFields.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchFields.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListBoxFields)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCtrlData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl groupCtrlVisibleFields;
        private DevExpress.XtraEditors.TextEdit txtSearchFields;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 全选AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 反选NToolStripMenuItem;
        public DevExpress.XtraEditors.CheckedListBoxControl ListBoxFields;
        public DevExpress.XtraGrid.GridControl gridCtrlData;
        public DevExpress.XtraEditors.SplitContainerControl SplitContainerCtrl;
        public WLib.WinCtrls.GridViewCtrl.PaginationBar paginationBar;
    }
}
