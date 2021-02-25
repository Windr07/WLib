namespace WLib.WinCtrls.ArcGisCtrl
{
    partial class OleDbQueryForm
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
            this.btnQuery = new System.Windows.Forms.Button();
            this.listBoxTables = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbLayerAsTable = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxFields = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.新建查询页ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除查询页ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtConnString = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCreatePage = new System.Windows.Forms.Button();
            this.btnSelectSql = new System.Windows.Forms.Button();
            this.cmbSql = new System.Windows.Forms.ComboBox();
            this.workspaceSelector1 = new WLib.WinCtrls.ArcGisCtrl.WorkspaceSelector();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(819, 576);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(164, 40);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "执行查询(&E)";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // listBoxTables
            // 
            this.listBoxTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxTables.FormattingEnabled = true;
            this.listBoxTables.ItemHeight = 12;
            this.listBoxTables.Location = new System.Drawing.Point(0, 20);
            this.listBoxTables.Margin = new System.Windows.Forms.Padding(0);
            this.listBoxTables.Name = "listBoxTables";
            this.listBoxTables.Size = new System.Drawing.Size(179, 164);
            this.listBoxTables.TabIndex = 3;
            this.listBoxTables.SelectedIndexChanged += new System.EventHandler(this.listBoxTables_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(8, 42);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(981, 533);
            this.splitContainer1.SplitterDistance = 179;
            this.splitContainer1.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listBoxTables, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.listBoxFields, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(179, 533);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbLayerAsTable);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(179, 20);
            this.panel1.TabIndex = 6;
            // 
            // cbLayerAsTable
            // 
            this.cbLayerAsTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLayerAsTable.AutoSize = true;
            this.cbLayerAsTable.Location = new System.Drawing.Point(79, 3);
            this.cbLayerAsTable.Name = "cbLayerAsTable";
            this.cbLayerAsTable.Size = new System.Drawing.Size(96, 16);
            this.cbLayerAsTable.TabIndex = 8;
            this.cbLayerAsTable.Text = "shp转dbf处理";
            this.cbLayerAsTable.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "图层或表";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 187);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "字段";
            // 
            // listBoxFields
            // 
            this.listBoxFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxFields.FormattingEnabled = true;
            this.listBoxFields.ItemHeight = 12;
            this.listBoxFields.Location = new System.Drawing.Point(0, 204);
            this.listBoxFields.Margin = new System.Windows.Forms.Padding(0);
            this.listBoxFields.Name = "listBoxFields";
            this.listBoxFields.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxFields.Size = new System.Drawing.Size(179, 329);
            this.listBoxFields.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(798, 533);
            this.tabControl1.TabIndex = 8;
            this.tabControl1.DoubleClick += new System.EventHandler(this.新建查询页ToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建查询页ToolStripMenuItem,
            this.删除查询页ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 48);
            // 
            // 新建查询页ToolStripMenuItem
            // 
            this.新建查询页ToolStripMenuItem.Name = "新建查询页ToolStripMenuItem";
            this.新建查询页ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.新建查询页ToolStripMenuItem.Text = "新建查询页";
            this.新建查询页ToolStripMenuItem.Click += new System.EventHandler(this.新建查询页ToolStripMenuItem_Click);
            // 
            // 删除查询页ToolStripMenuItem
            // 
            this.删除查询页ToolStripMenuItem.Name = "删除查询页ToolStripMenuItem";
            this.删除查询页ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.删除查询页ToolStripMenuItem.Text = "删除查询页";
            this.删除查询页ToolStripMenuItem.Click += new System.EventHandler(this.删除查询页ToolStripMenuItem_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(790, 507);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "查询1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.textBox1);
            this.splitContainer2.Panel1.Controls.Add(this.txtConnString);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer2.Size = new System.Drawing.Size(790, 507);
            this.splitContainer2.SplitterDistance = 143;
            this.splitContainer2.TabIndex = 7;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.textBox1.Location = new System.Drawing.Point(0, 21);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(790, 122);
            this.textBox1.TabIndex = 0;
            this.textBox1.Tag = "sql";
            // 
            // txtConnString
            // 
            this.txtConnString.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtConnString.Location = new System.Drawing.Point(0, 0);
            this.txtConnString.Name = "txtConnString";
            this.txtConnString.ReadOnly = true;
            this.txtConnString.Size = new System.Drawing.Size(790, 21);
            this.txtConnString.TabIndex = 1;
            this.txtConnString.Tag = "con";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(790, 360);
            this.dataGridView1.TabIndex = 5;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(649, 576);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(164, 40);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "清空语句(&C)";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCreatePage
            // 
            this.btnCreatePage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreatePage.Location = new System.Drawing.Point(479, 576);
            this.btnCreatePage.Name = "btnCreatePage";
            this.btnCreatePage.Size = new System.Drawing.Size(164, 40);
            this.btnCreatePage.TabIndex = 1;
            this.btnCreatePage.Text = "新建Tab查询页(&N)";
            this.btnCreatePage.UseVisualStyleBackColor = true;
            this.btnCreatePage.Click += new System.EventHandler(this.新建查询页ToolStripMenuItem_Click);
            // 
            // btnSelectSql
            // 
            this.btnSelectSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectSql.Location = new System.Drawing.Point(228, 584);
            this.btnSelectSql.Name = "btnSelectSql";
            this.btnSelectSql.Size = new System.Drawing.Size(60, 28);
            this.btnSelectSql.TabIndex = 8;
            this.btnSelectSql.Text = "确定";
            this.btnSelectSql.UseVisualStyleBackColor = true;
            this.btnSelectSql.Click += new System.EventHandler(this.cmbSql_SelectedIndexChanged);
            // 
            // cmbSql
            // 
            this.cmbSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbSql.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSql.FormattingEnabled = true;
            this.cmbSql.Items.AddRange(new object[] {
            "SELECT * FROM 表名称",
            "SELECT 列1, 列2 FROM 表名称 WHERE 列名称 = 某值",
            "INSERT INTO 表名称 VALUES (值1, 值2)",
            "INSERT INTO 表名称 (列1, 列2) VALUES (值1, 值2)",
            "UPDATE 表名称 SET 列1 = 新值",
            "UPDATE 表名称 SET 列1 = 新值 WHERE 列1 = 某值",
            "DELETE FROM 表名称 WHERE 列1 = 值"});
            this.cmbSql.Location = new System.Drawing.Point(9, 589);
            this.cmbSql.Name = "cmbSql";
            this.cmbSql.Size = new System.Drawing.Size(217, 20);
            this.cmbSql.TabIndex = 2;
            this.cmbSql.SelectedIndexChanged += new System.EventHandler(this.cmbSql_SelectedIndexChanged);
            // 
            // workspaceSelector1
            // 
            this.workspaceSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.workspaceSelector1.Description = "工作空间：";
            this.workspaceSelector1.Location = new System.Drawing.Point(8, 5);
            this.workspaceSelector1.Name = "workspaceSelector1";
            this.workspaceSelector1.OptEnable = true;
            this.workspaceSelector1.PathOrConnStr = "粘贴路径于此并按下回车，或点击选择按钮";
            this.workspaceSelector1.Size = new System.Drawing.Size(981, 31);
            this.workspaceSelector1.TabIndex = 9;
            this.workspaceSelector1.WorkspaceIndex = 0;
            this.workspaceSelector1.WorkspaceTypeFilter = "shp|gdb|mdb|sde|excel|sql";
            this.workspaceSelector1.AfterSelectPath += new System.EventHandler(this.workspaceSelector1_AfterSelectPath);
            // 
            // OleDbQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 622);
            this.Controls.Add(this.workspaceSelector1);
            this.Controls.Add(this.btnSelectSql);
            this.Controls.Add(this.cmbSql);
            this.Controls.Add(this.btnCreatePage);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.splitContainer1);
            this.Name = "OleDbQueryForm";
            this.Text = "通过OleDb连接和查询ArcGIS数据";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.ListBox listBoxTables;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ListBox listBoxFields;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnCreatePage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 新建查询页ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除查询页ToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSelectSql;
        private System.Windows.Forms.ComboBox cmbSql;
        private ArcGisCtrl.WorkspaceSelector workspaceSelector1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbLayerAsTable;
        private System.Windows.Forms.TextBox txtConnString;
    }
}