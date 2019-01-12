using WLib.UserCtrls.PathCtrl;

namespace WLib.UserCtrls.Dev.PathControl
{
    partial class WorkspaceSelectorControl
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cmbADBType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblWorkspaceDesc = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.SourcePathBox = new PathBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbADBType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.cmbADBType);
            this.panelControl1.Controls.Add(this.lblWorkspaceDesc);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(215, 23);
            this.panelControl1.TabIndex = 1;
            // 
            // cmbADBType
            // 
            this.cmbADBType.Location = new System.Drawing.Point(69, 1);
            this.cmbADBType.Name = "cmbADBType";
            this.cmbADBType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbADBType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbADBType.Size = new System.Drawing.Size(146, 21);
            this.cmbADBType.TabIndex = 1;
            this.cmbADBType.SelectedIndexChanged += new System.EventHandler(this.cmbWorkspaceType_SelectedIndexChanged);
            // 
            // lblWorkspaceDesc
            // 
            this.lblWorkspaceDesc.Location = new System.Drawing.Point(3, 4);
            this.lblWorkspaceDesc.Name = "lblWorkspaceDesc";
            this.lblWorkspaceDesc.Size = new System.Drawing.Size(60, 14);
            this.lblWorkspaceDesc.TabIndex = 0;
            this.lblWorkspaceDesc.Text = "工作空间：";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.SourcePathBox);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(215, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(468, 23);
            this.panelControl2.TabIndex = 1;
            // 
            // SourcePathBox
            // 
            this.SourcePathBox.CanTextEdit = true;
            this.SourcePathBox.DefaultTips = "粘贴路径于此并按下回车，或点击选择按钮";
            this.SourcePathBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SourcePathBox.FileFilter = null;
            this.SourcePathBox.Location = new System.Drawing.Point(0, 0);
            this.SourcePathBox.MaximumSize = new System.Drawing.Size(7000, 23);
            this.SourcePathBox.MultiSelect = true;
            this.SourcePathBox.Name = "SourcePathBox";
            this.SourcePathBox.OptEnable = true;
            this.SourcePathBox.Paths = new string[0];
            this.SourcePathBox.SelectPathType = ESelectPathType.Folder;
            this.SourcePathBox.SelectTips = null;
            this.SourcePathBox.ShowButtonOption = EShowButtonOption.ViewSelect;
            this.SourcePathBox.Size = new System.Drawing.Size(468, 23);
            this.SourcePathBox.TabIndex = 0;
            // 
            // WorkspaceSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.MaximumSize = new System.Drawing.Size(7000, 23);
            this.Name = "WorkspaceSelector";
            this.Size = new System.Drawing.Size(683, 23);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbADBType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbADBType;
        private DevExpress.XtraEditors.LabelControl lblWorkspaceDesc;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private PathBoxControl SourcePathBox;
    }
}
