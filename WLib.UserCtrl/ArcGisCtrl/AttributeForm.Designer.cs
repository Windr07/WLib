namespace WLib.WinCtrls.ArcGisCtrl
{
    partial class AttributeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttributeForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.sBtnClose = new System.Windows.Forms.Button();
            this.lblTips = new System.Windows.Forms.Label();
            this.txtSql = new System.Windows.Forms.TextBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnBuildQuery = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(625, 199);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // sBtnClose
            // 
            this.sBtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnClose.Location = new System.Drawing.Point(540, 1);
            this.sBtnClose.Name = "sBtnClose";
            this.sBtnClose.Size = new System.Drawing.Size(82, 26);
            this.sBtnClose.TabIndex = 0;
            this.sBtnClose.Text = "关闭(&X)";
            this.sBtnClose.UseVisualStyleBackColor = true;
            // 
            // lblTips
            // 
            this.lblTips.AutoSize = true;
            this.lblTips.Location = new System.Drawing.Point(6, 8);
            this.lblTips.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.lblTips.Name = "lblTips";
            this.lblTips.Size = new System.Drawing.Size(71, 12);
            this.lblTips.TabIndex = 0;
            this.lblTips.Text = "共有0条记录";
            // 
            // txtSql
            // 
            this.txtSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSql.Location = new System.Drawing.Point(133, 6);
            this.txtSql.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.txtSql.Name = "txtSql";
            this.txtSql.Size = new System.Drawing.Size(329, 21);
            this.txtSql.TabIndex = 1;
            // 
            // btnQuery
            // 
            this.btnQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnQuery.Location = new System.Drawing.Point(466, 1);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(1);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(58, 29);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.Text = "查询(&A)";
            this.btnQuery.UseVisualStyleBackColor = true;
            // 
            // btnBuildQuery
            // 
            this.btnBuildQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBuildQuery.Location = new System.Drawing.Point(526, 1);
            this.btnBuildQuery.Margin = new System.Windows.Forms.Padding(1);
            this.btnBuildQuery.Name = "btnBuildQuery";
            this.btnBuildQuery.Size = new System.Drawing.Size(98, 29);
            this.btnBuildQuery.TabIndex = 2;
            this.btnBuildQuery.Text = "构建查询(&Q)";
            this.btnBuildQuery.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnBuildQuery, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTips, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnQuery, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtSql, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(625, 31);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(625, 232);
            this.splitContainer1.SplitterDistance = 31;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 3;
            // 
            // AttributeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 232);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.sBtnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AttributeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "属性表";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button sBtnClose;
        private System.Windows.Forms.Label lblTips;
        private System.Windows.Forms.TextBox txtSql;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnBuildQuery;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}