namespace WLib.UserCtrls.Dev.PathCtrl
{
    partial class PathBoxControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PathBoxControl));
            this.cmbPath = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnView = new DevExpress.XtraEditors.PictureEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnView.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbPath
            // 
            this.cmbPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPath.Location = new System.Drawing.Point(1, 1);
            this.cmbPath.Name = "cmbPath";
            this.cmbPath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPath.Size = new System.Drawing.Size(196, 21);
            this.cmbPath.TabIndex = 0;
            this.cmbPath.Click += new System.EventHandler(this.cmbPath_Click);
            this.cmbPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPath_KeyDown);
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.EditValue = ((object)(resources.GetObject("btnView.EditValue")));
            this.btnView.Location = new System.Drawing.Point(159, 2);
            this.btnView.Name = "btnView";
            this.btnView.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btnView.Properties.Appearance.Options.UseBackColor = true;
            this.btnView.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnView.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.btnView.Size = new System.Drawing.Size(19, 19);
            this.btnView.TabIndex = 1;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            this.btnView.MouseEnter += new System.EventHandler(this.btnView_MouseEnter);
            this.btnView.MouseLeave += new System.EventHandler(this.btnView_MouseLeave);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(275, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Location = new System.Drawing.Point(199, 0);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "选择";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // PathBoxControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbPath);
            this.MaximumSize = new System.Drawing.Size(7000, 23);
            this.Name = "PathBoxControl";
            this.Size = new System.Drawing.Size(350, 23);
            ((System.ComponentModel.ISupportInitialize)(this.cmbPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnView.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit btnView;
        public DevExpress.XtraEditors.ComboBoxEdit cmbPath;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
    }
}
