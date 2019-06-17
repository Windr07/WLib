namespace WLib.WinCtrls.ArcGisCtrl
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
            this.cMenuStripTableList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开表格属性表TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移除表格RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenuStripTableList.SuspendLayout();
            this.SuspendLayout();
            // 
            // cMenuStripTableList
            // 
            this.cMenuStripTableList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开表格属性表TToolStripMenuItem,
            this.移除表格RToolStripMenuItem});
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
            // 移除表格RToolStripMenuItem
            // 
            this.移除表格RToolStripMenuItem.Name = "移除表格RToolStripMenuItem";
            this.移除表格RToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.移除表格RToolStripMenuItem.Text = "移除表格(&R)";
            this.移除表格RToolStripMenuItem.Click += new System.EventHandler(this.移除表格ToolStripMenuItem_Click);
            // 
            // TableListBox
            // 
            this.Name = "TableListBox";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tableListBox_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tableListBox_MouseUp);
            this.cMenuStripTableList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip cMenuStripTableList;
        private System.Windows.Forms.ToolStripMenuItem 打开表格属性表TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移除表格RToolStripMenuItem;
    }
}
