namespace WLib.WinCtrls
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
            this.panelTables = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panelSql = new System.Windows.Forms.Panel();
            this.btnSelectSql = new System.Windows.Forms.Button();
            this.cmbSql = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.listBoxFields = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.workspaceSelector1 = new WLib.WinCtrls.PathCtrl.WorkspaceSelector();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelTables.SuspendLayout();
            this.panelSql.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(534, 240);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(212, 40);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "执行查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // listBoxTables
            // 
            this.listBoxTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxTables.FormattingEnabled = true;
            this.listBoxTables.ItemHeight = 12;
            this.listBoxTables.Location = new System.Drawing.Point(0, 26);
            this.listBoxTables.Name = "listBoxTables";
            this.listBoxTables.Size = new System.Drawing.Size(227, 160);
            this.listBoxTables.TabIndex = 3;
            this.listBoxTables.SelectedIndexChanged += new System.EventHandler(this.listBoxTables_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 48);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBoxTables);
            this.splitContainer1.Panel1.Controls.Add(this.panelTables);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainer1.Panel2.Controls.Add(this.panelSql);
            this.splitContainer1.Size = new System.Drawing.Size(734, 186);
            this.splitContainer1.SplitterDistance = 227;
            this.splitContainer1.TabIndex = 4;
            // 
            // panelTables
            // 
            this.panelTables.Controls.Add(this.label1);
            this.panelTables.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTables.Location = new System.Drawing.Point(0, 0);
            this.panelTables.Name = "panelTables";
            this.panelTables.Size = new System.Drawing.Size(227, 26);
            this.panelTables.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图层或表：";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 26);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(503, 160);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // panelSql
            // 
            this.panelSql.Controls.Add(this.btnSelectSql);
            this.panelSql.Controls.Add(this.cmbSql);
            this.panelSql.Controls.Add(this.label3);
            this.panelSql.Controls.Add(this.label2);
            this.panelSql.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSql.Location = new System.Drawing.Point(0, 0);
            this.panelSql.Name = "panelSql";
            this.panelSql.Size = new System.Drawing.Size(503, 26);
            this.panelSql.TabIndex = 6;
            // 
            // btnSelectSql
            // 
            this.btnSelectSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectSql.Location = new System.Drawing.Point(450, 2);
            this.btnSelectSql.Name = "btnSelectSql";
            this.btnSelectSql.Size = new System.Drawing.Size(50, 22);
            this.btnSelectSql.TabIndex = 8;
            this.btnSelectSql.Text = "确定";
            this.btnSelectSql.UseVisualStyleBackColor = true;
            this.btnSelectSql.Click += new System.EventHandler(this.cmbSql_SelectedIndexChanged);
            // 
            // cmbSql
            // 
            this.cmbSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.cmbSql.Location = new System.Drawing.Point(232, 3);
            this.cmbSql.Name = "cmbSql";
            this.cmbSql.Size = new System.Drawing.Size(217, 20);
            this.cmbSql.TabIndex = 2;
            this.cmbSql.SelectedIndexChanged += new System.EventHandler(this.cmbSql_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(175, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "备选语句";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "请输入SQL语句：";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(247, 286);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(499, 170);
            this.dataGridView1.TabIndex = 5;
            // 
            // listBoxFields
            // 
            this.listBoxFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxFields.FormattingEnabled = true;
            this.listBoxFields.ItemHeight = 12;
            this.listBoxFields.Location = new System.Drawing.Point(12, 286);
            this.listBoxFields.Name = "listBoxFields";
            this.listBoxFields.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxFields.Size = new System.Drawing.Size(231, 172);
            this.listBoxFields.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 268);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "字段：";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(418, 240);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(110, 40);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "清空语句";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // workspaceSelector1
            // 
            this.workspaceSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.workspaceSelector1.Description = "工作空间：";
            this.workspaceSelector1.Location = new System.Drawing.Point(7, 7);
            this.workspaceSelector1.Name = "workspaceSelector1";
            this.workspaceSelector1.OptEnable = true;
            this.workspaceSelector1.PathOrConnStr = "粘贴路径于此并按下回车，或点击选择按钮";
            this.workspaceSelector1.Size = new System.Drawing.Size(739, 35);
            this.workspaceSelector1.TabIndex = 2;
            this.workspaceSelector1.WorkspaceIndex = 0;
            this.workspaceSelector1.WorkspaceTypeFilter = "shp|gdb|mdb|sde|excel|sql";
            this.workspaceSelector1.AfterSelectPath += new System.EventHandler(this.workspaceSelector1_AfterSelectPath);
            // 
            // OleDbQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 468);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listBoxFields);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.workspaceSelector1);
            this.Name = "OleDbQueryForm";
            this.Text = "通过OleDb连接和查询ArcGIS数据";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelTables.ResumeLayout(false);
            this.panelTables.PerformLayout();
            this.panelSql.ResumeLayout(false);
            this.panelSql.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnQuery;
        private WinCtrls.PathCtrl.WorkspaceSelector workspaceSelector1;
        private System.Windows.Forms.ListBox listBoxTables;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelTables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelSql;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmbSql;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxFields;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSelectSql;
    }
}