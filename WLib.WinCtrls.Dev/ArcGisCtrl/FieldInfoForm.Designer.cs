namespace WLib.WinCtrls.Dev.ArcGisCtrl
{
    partial class FieldInfoForm
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
            this.workspaceSelector1 = new WLib.WinCtrls.Dev.ArcGisCtrl.WorkspaceSelectorControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.btnExportFieldInfo = new DevExpress.XtraEditors.SimpleButton();
            this.listBoxLayers = new DevExpress.XtraEditors.ListBoxControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxLayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // workspaceSelector1
            // 
            this.workspaceSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.workspaceSelector1.Description = "工作空间：";
            this.workspaceSelector1.Location = new System.Drawing.Point(14, 14);
            this.workspaceSelector1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.workspaceSelector1.MaximumSize = new System.Drawing.Size(8000, 30);
            this.workspaceSelector1.Name = "workspaceSelector1";
            this.workspaceSelector1.OptEnable = true;
            this.workspaceSelector1.PathOrConnStr = "";
            this.workspaceSelector1.Size = new System.Drawing.Size(1064, 30);
            this.workspaceSelector1.TabIndex = 0;
            this.workspaceSelector1.WorkspaceIndex = 0;
            this.workspaceSelector1.WorkspaceTypeFilter = "shp|gdb|mdb|sde";
            this.workspaceSelector1.AfterSelectPath += new System.EventHandler(this.WorkspaceSelector1_AfterSelectPath);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl1.Location = new System.Drawing.Point(14, 53);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.listBoxLayers);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1064, 538);
            this.splitContainerControl1.SplitterPosition = 277;
            this.splitContainerControl1.TabIndex = 1;
            // 
            // btnExportFieldInfo
            // 
            this.btnExportFieldInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportFieldInfo.Location = new System.Drawing.Point(837, 597);
            this.btnExportFieldInfo.Name = "btnExportFieldInfo";
            this.btnExportFieldInfo.Size = new System.Drawing.Size(241, 56);
            this.btnExportFieldInfo.TabIndex = 2;
            this.btnExportFieldInfo.Text = "导出字段信息";
            this.btnExportFieldInfo.Click += new System.EventHandler(this.btnGetFieldInfo_Click);
            // 
            // listBoxLayers
            // 
            this.listBoxLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxLayers.Location = new System.Drawing.Point(0, 0);
            this.listBoxLayers.Name = "listBoxLayers";
            this.listBoxLayers.Size = new System.Drawing.Size(277, 538);
            this.listBoxLayers.TabIndex = 3;
            this.listBoxLayers.SelectedIndexChanged += new System.EventHandler(this.ListBoxLayers_SelectedIndexChanged);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(781, 538);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // FieldInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 665);
            this.Controls.Add(this.btnExportFieldInfo);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.workspaceSelector1);
            this.Name = "FieldInfoForm";
            this.Text = "查看图层字段信息";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listBoxLayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WorkspaceSelectorControl workspaceSelector1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SimpleButton btnExportFieldInfo;
        private DevExpress.XtraEditors.ListBoxControl listBoxLayers;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}