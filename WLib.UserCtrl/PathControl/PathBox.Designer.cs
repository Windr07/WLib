namespace WLib.UserCtrls.PathControl
{
    partial class PathBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PathBox));
            this.cmbPath = new System.Windows.Forms.ComboBox();
            this.btnView = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnOperate = new System.Windows.Forms.Button();
            this.panelPath = new System.Windows.Forms.Panel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelBotton = new System.Windows.Forms.Panel();
            this.panelPath.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbPath
            // 
            this.cmbPath.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cmbPath.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.cmbPath.FormattingEnabled = true;
            this.cmbPath.Location = new System.Drawing.Point(0, 4);
            this.cmbPath.Name = "cmbPath";
            this.cmbPath.Size = new System.Drawing.Size(343, 20);
            this.cmbPath.TabIndex = 0;
            this.cmbPath.Click += new System.EventHandler(this.cmbPath_Click);
            this.cmbPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPath_KeyDown);
            // 
            // btnView
            // 
            this.btnView.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnView.Image = ((System.Drawing.Image)(resources.GetObject("btnView.Image")));
            this.btnView.Location = new System.Drawing.Point(343, 0);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(28, 28);
            this.btnView.TabIndex = 1;
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelect.Location = new System.Drawing.Point(371, 0);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(61, 28);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "选择";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnOperate
            // 
            this.btnOperate.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOperate.Location = new System.Drawing.Point(432, 0);
            this.btnOperate.Name = "btnOperate";
            this.btnOperate.Size = new System.Drawing.Size(61, 28);
            this.btnOperate.TabIndex = 2;
            this.btnOperate.Text = "保存";
            this.btnOperate.UseVisualStyleBackColor = true;
            this.btnOperate.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelPath
            // 
            this.panelPath.Controls.Add(this.panelTop);
            this.panelPath.Controls.Add(this.cmbPath);
            this.panelPath.Controls.Add(this.panelBotton);
            this.panelPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPath.Location = new System.Drawing.Point(0, 0);
            this.panelPath.Name = "panelPath";
            this.panelPath.Size = new System.Drawing.Size(343, 28);
            this.panelPath.TabIndex = 5;
            // 
            // panelTop
            // 
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(343, 4);
            this.panelTop.TabIndex = 1;
            // 
            // panelBotton
            // 
            this.panelBotton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotton.Location = new System.Drawing.Point(0, 24);
            this.panelBotton.Name = "panelBotton";
            this.panelBotton.Size = new System.Drawing.Size(343, 4);
            this.panelBotton.TabIndex = 2;
            // 
            // PathBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelPath);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnOperate);
            this.MaximumSize = new System.Drawing.Size(9999, 28);
            this.MinimumSize = new System.Drawing.Size(0, 28);
            this.Name = "PathBox";
            this.Size = new System.Drawing.Size(493, 28);
            this.Resize += new System.EventHandler(this.PathBox_Resize);
            this.panelPath.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnOperate;
        public System.Windows.Forms.ComboBox cmbPath;
        private System.Windows.Forms.Panel panelPath;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelBotton;
    }
}
