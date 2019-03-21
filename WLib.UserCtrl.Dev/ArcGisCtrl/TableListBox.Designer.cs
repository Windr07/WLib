namespace WLib.UserCtrls.Dev.ArcGisCtrl
{
    partial class TableListBox
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableListBox));
            this.tableBox = new DevExpress.XtraEditors.ImageListBoxControl();
            this.cMenuStripTableList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开表格属性表TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移除表格ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageCollection_Nav = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tableBox)).BeginInit();
            this.cMenuStripTableList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection_Nav)).BeginInit();
            this.SuspendLayout();
            // 
            // tableBox
            // 
            this.tableBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableBox.Location = new System.Drawing.Point(0, 0);
            this.tableBox.Name = "tableBox";
            this.tableBox.Size = new System.Drawing.Size(150, 150);
            this.tableBox.TabIndex = 5;
            this.tableBox.Visible = false;
            this.tableBox.SelectedIndexChanged += new System.EventHandler(this.imagelistBoxTables_SelectedIndexChanged);
            this.tableBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imagelistBoxTables_MouseDown);
            this.tableBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imagelistBoxTables_MouseUp);
            // 
            // cMenuStripTableList
            // 
            this.cMenuStripTableList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开表格属性表TToolStripMenuItem,
            this.移除表格ToolStripMenuItem});
            this.cMenuStripTableList.Name = "cMenuStripTableList";
            this.cMenuStripTableList.Size = new System.Drawing.Size(152, 48);
            // 
            // 打开表格属性表TToolStripMenuItem
            // 
            this.打开表格属性表TToolStripMenuItem.Name = "打开表格属性表TToolStripMenuItem";
            this.打开表格属性表TToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.打开表格属性表TToolStripMenuItem.Text = "打开属性表(&T)";
            this.打开表格属性表TToolStripMenuItem.Click += new System.EventHandler(this.打开表格属性表TToolStripMenuItem_Click);
            // 
            // 移除表格ToolStripMenuItem
            // 
            this.移除表格ToolStripMenuItem.Name = "移除表格ToolStripMenuItem";
            this.移除表格ToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.移除表格ToolStripMenuItem.Text = "移除(&D)";
            this.移除表格ToolStripMenuItem.Click += new System.EventHandler(this.移除表格ToolStripMenuItem_Click);
            // 
            // imageCollection_Nav
            // 
            this.imageCollection_Nav.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection_Nav.ImageStream")));
            this.imageCollection_Nav.Images.SetKeyName(0, "table2.png");
            // 
            // TableListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableBox);
            this.Name = "TableListBox";
            ((System.ComponentModel.ISupportInitialize)(this.tableBox)).EndInit();
            this.cMenuStripTableList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection_Nav)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ImageListBoxControl tableBox;
        private System.Windows.Forms.ContextMenuStrip cMenuStripTableList;
        private System.Windows.Forms.ToolStripMenuItem 打开表格属性表TToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem 移除表格ToolStripMenuItem;
        private DevExpress.Utils.ImageCollection imageCollection_Nav;
    }
}
