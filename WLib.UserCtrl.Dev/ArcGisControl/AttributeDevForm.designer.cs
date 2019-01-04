namespace WLib.UserCtrls.Dev.ArcGisControl
{
    partial class AttributeDevForm
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttributeDevForm));
            this.dataGridView1 = new DevExpress.XtraGrid.GridControl();
            this.cMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.缩放至图斑GToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.按属性查询QToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制值CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制整行RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.sBtnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.cMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ContextMenuStrip = this.cMenuStrip;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.dataGridView1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MainView = this.gridView1;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(818, 271);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // cMenuStrip
            // 
            this.cMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.缩放至图斑GToolStripMenuItem,
            this.按属性查询QToolStripMenuItem,
            this.复制值CToolStripMenuItem,
            this.复制整行RToolStripMenuItem});
            this.cMenuStrip.Name = "cMenuStrip";
            this.cMenuStrip.Size = new System.Drawing.Size(210, 92);
            // 
            // 缩放至图斑GToolStripMenuItem
            // 
            this.缩放至图斑GToolStripMenuItem.Name = "缩放至图斑GToolStripMenuItem";
            this.缩放至图斑GToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.缩放至图斑GToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.缩放至图斑GToolStripMenuItem.Text = "缩放至图斑(&G)";
            this.缩放至图斑GToolStripMenuItem.Click += new System.EventHandler(this.缩放至图斑GToolStripMenuItem_Click);
            // 
            // 按属性查询QToolStripMenuItem
            // 
            this.按属性查询QToolStripMenuItem.Name = "按属性查询QToolStripMenuItem";
            this.按属性查询QToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.按属性查询QToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.按属性查询QToolStripMenuItem.Text = "按属性查询(&Q)";
            this.按属性查询QToolStripMenuItem.Click += new System.EventHandler(this.按属性查询QToolStripMenuItem_Click);
            // 
            // 复制值CToolStripMenuItem
            // 
            this.复制值CToolStripMenuItem.Name = "复制值CToolStripMenuItem";
            this.复制值CToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.复制值CToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.复制值CToolStripMenuItem.Text = "复制值(&C)";
            this.复制值CToolStripMenuItem.Click += new System.EventHandler(this.复制值CToolStripMenuItem_Click);
            // 
            // 复制整行RToolStripMenuItem
            // 
            this.复制整行RToolStripMenuItem.Name = "复制整行RToolStripMenuItem";
            this.复制整行RToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.复制整行RToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.复制整行RToolStripMenuItem.Text = "复制整行数据(&R)";
            this.复制整行RToolStripMenuItem.Click += new System.EventHandler(this.复制整行RToolStripMenuItem_Click);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dataGridView1;
            this.gridView1.Name = "gridView1";
            // 
            // sBtnClose
            // 
            this.sBtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sBtnClose.Location = new System.Drawing.Point(731, 6);
            this.sBtnClose.Name = "sBtnClose";
            this.sBtnClose.Size = new System.Drawing.Size(82, 26);
            this.sBtnClose.TabIndex = 3;
            this.sBtnClose.Text = "关闭(&X)";
            this.sBtnClose.Click += new System.EventHandler(this.sBtnClose_Click);
            // 
            // AttributeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.sBtnClose;
            this.ClientSize = new System.Drawing.Size(818, 271);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.sBtnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AttributeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "属性表";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.cMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dataGridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ContextMenuStrip cMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 按属性查询QToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制值CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制整行RToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 缩放至图斑GToolStripMenuItem;
        private DevExpress.XtraEditors.SimpleButton sBtnClose;
    }
}