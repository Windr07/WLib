namespace WLib.WinCtrls.Dev.PathCtrl
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
            this.btnOperate = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.splitButtons = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitPathBox = new DevExpress.XtraEditors.SplitContainerControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnView.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitButtons)).BeginInit();
            this.splitButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPathBox)).BeginInit();
            this.splitPathBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbPath
            // 
            this.cmbPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPath.Location = new System.Drawing.Point(0, 0);
            this.cmbPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbPath.Name = "cmbPath";
            this.cmbPath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPath.Size = new System.Drawing.Size(552, 24);
            this.cmbPath.TabIndex = 0;
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.EditValue = ((object)(resources.GetObject("btnView.EditValue")));
            this.btnView.Location = new System.Drawing.Point(513, 2);
            this.btnView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnView.Name = "btnView";
            this.btnView.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btnView.Properties.Appearance.Options.UseBackColor = true;
            this.btnView.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnView.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.btnView.Size = new System.Drawing.Size(19, 22);
            this.btnView.TabIndex = 1;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnOperate
            // 
            this.btnOperate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOperate.Location = new System.Drawing.Point(0, 0);
            this.btnOperate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOperate.Name = "btnOperate";
            this.btnOperate.Size = new System.Drawing.Size(81, 35);
            this.btnOperate.TabIndex = 2;
            this.btnOperate.Text = "操作";
            this.btnOperate.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelect.Location = new System.Drawing.Point(0, 0);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(67, 35);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "选择";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // splitButtons
            // 
            this.splitButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitButtons.Location = new System.Drawing.Point(0, 0);
            this.splitButtons.Name = "splitButtons";
            this.splitButtons.Panel1.Controls.Add(this.btnSelect);
            this.splitButtons.Panel1.Text = "Panel1";
            this.splitButtons.Panel2.Controls.Add(this.btnOperate);
            this.splitButtons.Panel2.Text = "Panel2";
            this.splitButtons.Size = new System.Drawing.Size(154, 35);
            this.splitButtons.SplitterPosition = 67;
            this.splitButtons.TabIndex = 3;
            // 
            // splitPathBox
            // 
            this.splitPathBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitPathBox.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitPathBox.IsSplitterFixed = true;
            this.splitPathBox.Location = new System.Drawing.Point(0, 0);
            this.splitPathBox.Name = "splitPathBox";
            this.splitPathBox.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitPathBox.Panel1.Text = "Panel1";
            this.splitPathBox.Panel2.Controls.Add(this.splitButtons);
            this.splitPathBox.Panel2.Text = "Panel2";
            this.splitPathBox.Size = new System.Drawing.Size(712, 35);
            this.splitPathBox.SplitterPosition = 154;
            this.splitPathBox.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(552, 35);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnView);
            this.panelControl1.Controls.Add(this.cmbPath);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 4);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(552, 26);
            this.panelControl1.TabIndex = 4;
            // 
            // PathBoxControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitPathBox);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(13332, 125);
            this.Name = "PathBoxControl";
            this.Size = new System.Drawing.Size(712, 35);
            ((System.ComponentModel.ISupportInitialize)(this.cmbPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnView.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitButtons)).EndInit();
            this.splitButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPathBox)).EndInit();
            this.splitPathBox.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit btnView;
        public DevExpress.XtraEditors.ComboBoxEdit cmbPath;
        private DevExpress.XtraEditors.SimpleButton btnOperate;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraEditors.SplitContainerControl splitButtons;
        private DevExpress.XtraEditors.SplitContainerControl splitPathBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}
