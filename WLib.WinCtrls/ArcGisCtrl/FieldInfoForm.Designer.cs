namespace WLib.WinCtrls.ArcGisCtrl
{
    partial class FieldInfoForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnExportFieldInfo = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.workspaceSelector1 = new WLib.WinCtrls.ArcGisCtrl.WorkspaceSelector();
            this.listBoxLayers = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExportFieldInfo
            // 
            this.btnExportFieldInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportFieldInfo.Location = new System.Drawing.Point(688, 485);
            this.btnExportFieldInfo.Name = "btnExportFieldInfo";
            this.btnExportFieldInfo.Size = new System.Drawing.Size(181, 45);
            this.btnExportFieldInfo.TabIndex = 0;
            this.btnExportFieldInfo.Text = "导出字段信息";
            this.btnExportFieldInfo.UseVisualStyleBackColor = true;
            this.btnExportFieldInfo.Click += new System.EventHandler(this.btnGetFieldInfo_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(250, 45);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(619, 435);
            this.dataGridView1.TabIndex = 1;
            // 
            // workspaceSelector1
            // 
            this.workspaceSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.workspaceSelector1.Description = "数据源：";
            this.workspaceSelector1.Location = new System.Drawing.Point(12, 9);
            this.workspaceSelector1.Name = "workspaceSelector1";
            this.workspaceSelector1.OptEnable = true;
            this.workspaceSelector1.PathOrConnStr = "粘贴路径于此并按下回车，或点击选择按钮";
            this.workspaceSelector1.Size = new System.Drawing.Size(857, 31);
            this.workspaceSelector1.TabIndex = 2;
            this.workspaceSelector1.WorkspaceIndex = 0;
            this.workspaceSelector1.WorkspaceTypeFilter = "shp|gdb|mdb|sde|sql";
            this.workspaceSelector1.AfterSelectPath += new System.EventHandler(this.WorkspaceSelector1_AfterSelectPath);
            // 
            // listBoxLayers
            // 
            this.listBoxLayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxLayers.FormattingEnabled = true;
            this.listBoxLayers.ItemHeight = 12;
            this.listBoxLayers.Location = new System.Drawing.Point(12, 45);
            this.listBoxLayers.Name = "listBoxLayers";
            this.listBoxLayers.Size = new System.Drawing.Size(232, 436);
            this.listBoxLayers.TabIndex = 3;
            this.listBoxLayers.SelectedIndexChanged += new System.EventHandler(this.ListBoxLayers_SelectedIndexChanged);
            // 
            // FieldInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 542);
            this.Controls.Add(this.listBoxLayers);
            this.Controls.Add(this.workspaceSelector1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnExportFieldInfo);
            this.Name = "FieldInfoForm";
            this.Text = "查看图层字段信息";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExportFieldInfo;
        private System.Windows.Forms.DataGridView dataGridView1;
        private WLib.WinCtrls.ArcGisCtrl.WorkspaceSelector workspaceSelector1;
        private System.Windows.Forms.ListBox listBoxLayers;
    }
}

