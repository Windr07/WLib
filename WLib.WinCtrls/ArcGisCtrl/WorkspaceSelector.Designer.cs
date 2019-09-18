using WLib.WinCtrls.PathCtrl;

namespace WLib.WinCtrls.ArcGisCtrl
{
    partial class WorkspaceSelector
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
            this.SourcePathBox = new PathBoxSimple();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblWorkspaceDesc = new System.Windows.Forms.Label();
            this.cmbADBType = new System.Windows.Forms.ComboBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SourcePathBox
            // 
            this.SourcePathBox.ButtonsSplitWidth = 2;
            this.SourcePathBox.ButtonWidth = 80;
            this.SourcePathBox.DefaultTips = "粘贴路径于此并按下回车，或点击选择按钮";
            this.SourcePathBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SourcePathBox.FileFilter = null;
            this.SourcePathBox.Location = new System.Drawing.Point(0, 0);
            this.SourcePathBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SourcePathBox.MaximumSize = new System.Drawing.Size(9999, 100);
            this.SourcePathBox.MinimumSize = new System.Drawing.Size(0, 28);
            this.SourcePathBox.Name = "SourcePathBox";
            this.SourcePathBox.OperateButtonText = "保存";
            this.SourcePathBox.OptEnable = true;
            this.SourcePathBox.Path = "粘贴路径于此并按下回车，或点击选择按钮";
            this.SourcePathBox.PathToButtonSplitWidth = 2;
            this.SourcePathBox.ReadOnly = false;
            this.SourcePathBox.SelectButtonText = "选择";
            this.SourcePathBox.SelectEnable = true;
            this.SourcePathBox.SelectPathType = ESelectPathType.Folder;
            this.SourcePathBox.SelectTips = null;
            this.SourcePathBox.ShowButtonOption = EShowButtonOption.ViewSelect;
            this.SourcePathBox.Size = new System.Drawing.Size(426, 31);
            this.SourcePathBox.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.SourcePathBox);
            this.splitContainer1.Size = new System.Drawing.Size(686, 31);
            this.splitContainer1.SplitterDistance = 258;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblWorkspaceDesc, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbADBType, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(258, 31);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // lblWorkspaceDesc
            // 
            this.lblWorkspaceDesc.AutoSize = true;
            this.lblWorkspaceDesc.Location = new System.Drawing.Point(3, 9);
            this.lblWorkspaceDesc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblWorkspaceDesc.Name = "lblWorkspaceDesc";
            this.lblWorkspaceDesc.Size = new System.Drawing.Size(65, 12);
            this.lblWorkspaceDesc.TabIndex = 0;
            this.lblWorkspaceDesc.Text = "工作空间：";
            // 
            // cmbADBType
            // 
            this.cmbADBType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbADBType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbADBType.FormattingEnabled = true;
            this.cmbADBType.Location = new System.Drawing.Point(73, 5);
            this.cmbADBType.Margin = new System.Windows.Forms.Padding(0);
            this.cmbADBType.Name = "cmbADBType";
            this.cmbADBType.Size = new System.Drawing.Size(185, 20);
            this.cmbADBType.TabIndex = 1;
            this.cmbADBType.SelectedIndexChanged += new System.EventHandler(this.cmbWorkspaceType_SelectedIndexChanged);
            // 
            // WorkspaceSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "WorkspaceSelector";
            this.Size = new System.Drawing.Size(686, 31);
            this.Load += new System.EventHandler(this.WorkspaceSelector_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public PathBoxSimple SourcePathBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox cmbADBType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblWorkspaceDesc;

    }
}
