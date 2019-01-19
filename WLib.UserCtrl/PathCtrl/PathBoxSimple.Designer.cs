namespace WLib.UserCtrls.PathCtrl
{
    partial class PathBoxSimple
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PathBoxSimple));
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnOperate = new System.Windows.Forms.Button();
            this.splitButtons = new System.Windows.Forms.SplitContainer();
            this.splitPathBox = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picBoxViewFile = new System.Windows.Forms.PictureBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.splitButtons.Panel1.SuspendLayout();
            this.splitButtons.Panel2.SuspendLayout();
            this.splitButtons.SuspendLayout();
            this.splitPathBox.Panel1.SuspendLayout();
            this.splitPathBox.Panel2.SuspendLayout();
            this.splitPathBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxViewFile)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelect
            // 
            this.btnSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelect.Location = new System.Drawing.Point(0, 0);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(72, 28);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "选择";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnOperate
            // 
            this.btnOperate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOperate.Location = new System.Drawing.Point(0, 0);
            this.btnOperate.Name = "btnOperate";
            this.btnOperate.Size = new System.Drawing.Size(72, 28);
            this.btnOperate.TabIndex = 2;
            this.btnOperate.Text = "操作";
            this.btnOperate.UseVisualStyleBackColor = true;
            this.btnOperate.Click += new System.EventHandler(this.btnOperate_Click);
            // 
            // splitContainerButtons
            // 
            this.splitButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitButtons.Location = new System.Drawing.Point(0, 0);
            this.splitButtons.Margin = new System.Windows.Forms.Padding(0);
            this.splitButtons.Name = "splitContainerButtons";
            // 
            // splitContainerButtons.Panel1
            // 
            this.splitButtons.Panel1.Controls.Add(this.btnSelect);
            // 
            // splitContainerButtons.Panel2
            // 
            this.splitButtons.Panel2.Controls.Add(this.btnOperate);
            this.splitButtons.Size = new System.Drawing.Size(146, 28);
            this.splitButtons.SplitterDistance = 72;
            this.splitButtons.SplitterWidth = 2;
            this.splitButtons.TabIndex = 3;
            // 
            // splitContainerPathBox
            // 
            this.splitPathBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitPathBox.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitPathBox.Location = new System.Drawing.Point(0, 0);
            this.splitPathBox.Margin = new System.Windows.Forms.Padding(0);
            this.splitPathBox.Name = "splitContainerPathBox";
            // 
            // splitContainerPathBox.Panel1
            // 
            this.splitPathBox.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainerPathBox.Panel2
            // 
            this.splitPathBox.Panel2.Controls.Add(this.splitButtons);
            this.splitPathBox.Size = new System.Drawing.Size(464, 28);
            this.splitPathBox.SplitterDistance = 316;
            this.splitPathBox.SplitterWidth = 2;
            this.splitPathBox.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(316, 28);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.picBoxViewFile);
            this.panel1.Controls.Add(this.txtPath);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(316, 21);
            this.panel1.TabIndex = 1;
            // 
            // picBoxViewFile
            // 
            this.picBoxViewFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picBoxViewFile.Image = ((System.Drawing.Image)(resources.GetObject("picBoxViewFile.Image")));
            this.picBoxViewFile.Location = new System.Drawing.Point(296, 1);
            this.picBoxViewFile.Name = "picBoxViewFile";
            this.picBoxViewFile.Size = new System.Drawing.Size(19, 19);
            this.picBoxViewFile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxViewFile.TabIndex = 1;
            this.picBoxViewFile.TabStop = false;
            this.picBoxViewFile.Click += new System.EventHandler(this.picBoxViewFile_Click);
            this.picBoxViewFile.MouseEnter += new System.EventHandler(this.picBoxViewFile_MouseEnter);
            this.picBoxViewFile.MouseLeave += new System.EventHandler(this.picBoxViewFile_MouseLeave);
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(0, 0);
            this.txtPath.Margin = new System.Windows.Forms.Padding(0);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(316, 21);
            this.txtPath.TabIndex = 0;
            this.txtPath.Click += new System.EventHandler(this.txtPath_Click);
            this.txtPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPath_KeyDown);
            this.txtPath.MouseEnter += new System.EventHandler(this.txtPath_MouseEnter);
            this.txtPath.MouseLeave += new System.EventHandler(this.txtPath_MouseLeave);
            // 
            // PathBoxSimple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitPathBox);
            this.MaximumSize = new System.Drawing.Size(9999, 100);
            this.MinimumSize = new System.Drawing.Size(0, 28);
            this.Name = "PathBoxSimple";
            this.Size = new System.Drawing.Size(464, 28);
            this.splitButtons.Panel1.ResumeLayout(false);
            this.splitButtons.Panel2.ResumeLayout(false);
            this.splitButtons.ResumeLayout(false);
            this.splitPathBox.Panel1.ResumeLayout(false);
            this.splitPathBox.Panel2.ResumeLayout(false);
            this.splitPathBox.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxViewFile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnOperate;
        private System.Windows.Forms.SplitContainer splitButtons;
        private System.Windows.Forms.SplitContainer splitPathBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picBoxViewFile;
    }
}
