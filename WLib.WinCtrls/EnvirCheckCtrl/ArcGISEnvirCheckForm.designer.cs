namespace WLib.WinCtrls.EnvirCheckCtrl
{
    partial class ArcGISEnvirCheckForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArcGISEnvirCheckForm));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblTips = new System.Windows.Forms.Label();
            this.lblStartupLicenseManager = new System.Windows.Forms.Label();
            this.btnCheckEnviroment = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbArcGISVersion = new System.Windows.Forms.ComboBox();
            this.btnInstallDotNet35 = new System.Windows.Forms.Button();
            this.btnInstallRuntime = new System.Windows.Forms.Button();
            this.btnInstallSoftware = new System.Windows.Forms.Button();
            this.lblOpenLicenseEcp = new System.Windows.Forms.Label();
            this.lblStartupAdministrator = new System.Windows.Forms.Label();
            this.btnInstallLisence = new System.Windows.Forms.Button();
            this.btnInstallDotNet4 = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(8, 411);
            this.progressBar1.MarqueeAnimationSpeed = 30;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(800, 10);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 2;
            // 
            // lblTips
            // 
            this.lblTips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTips.AutoSize = true;
            this.lblTips.Location = new System.Drawing.Point(8, 395);
            this.lblTips.Name = "lblTips";
            this.lblTips.Size = new System.Drawing.Size(143, 12);
            this.lblTips.TabIndex = 3;
            this.lblTips.Text = "正在检测系统安装环境...";
            // 
            // lblStartupLicenseManager
            // 
            this.lblStartupLicenseManager.AutoSize = true;
            this.lblStartupLicenseManager.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblStartupLicenseManager.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStartupLicenseManager.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblStartupLicenseManager.Location = new System.Drawing.Point(11, 289);
            this.lblStartupLicenseManager.MaximumSize = new System.Drawing.Size(330, 80);
            this.lblStartupLicenseManager.Name = "lblStartupLicenseManager";
            this.lblStartupLicenseManager.Size = new System.Drawing.Size(161, 12);
            this.lblStartupLicenseManager.TabIndex = 1;
            this.lblStartupLicenseManager.Text = "启动ArcGIS License Manager";
            this.lblStartupLicenseManager.Click += new System.EventHandler(this.lblStartupLicenseManager_Click);
            // 
            // btnCheckEnviroment
            // 
            this.btnCheckEnviroment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckEnviroment.Location = new System.Drawing.Point(590, 367);
            this.btnCheckEnviroment.Name = "btnCheckEnviroment";
            this.btnCheckEnviroment.Size = new System.Drawing.Size(218, 38);
            this.btnCheckEnviroment.TabIndex = 4;
            this.btnCheckEnviroment.Text = "检测系统安装环境";
            this.btnCheckEnviroment.UseVisualStyleBackColor = true;
            this.btnCheckEnviroment.Visible = false;
            this.btnCheckEnviroment.Click += new System.EventHandler(this.btnCheckEnviroment_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(824, 453);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.lblTips);
            this.tabPage1.Controls.Add(this.txtMessage);
            this.tabPage1.Controls.Add(this.btnCheckEnviroment);
            this.tabPage1.Controls.Add(this.progressBar1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(816, 427);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "安装进程";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.cmbArcGISVersion);
            this.groupBox1.Controls.Add(this.btnInstallDotNet35);
            this.groupBox1.Controls.Add(this.btnInstallRuntime);
            this.groupBox1.Controls.Add(this.btnInstallSoftware);
            this.groupBox1.Controls.Add(this.lblOpenLicenseEcp);
            this.groupBox1.Controls.Add(this.lblStartupAdministrator);
            this.groupBox1.Controls.Add(this.lblStartupLicenseManager);
            this.groupBox1.Controls.Add(this.btnInstallLisence);
            this.groupBox1.Controls.Add(this.btnInstallDotNet4);
            this.groupBox1.Location = new System.Drawing.Point(8, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 355);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "安装";
            // 
            // cmbArcGISVersion
            // 
            this.cmbArcGISVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbArcGISVersion.FormattingEnabled = true;
            this.cmbArcGISVersion.Items.AddRange(new object[] {
            "ArcGIS 10.0"});
            this.cmbArcGISVersion.Location = new System.Drawing.Point(13, 25);
            this.cmbArcGISVersion.Margin = new System.Windows.Forms.Padding(2);
            this.cmbArcGISVersion.Name = "cmbArcGISVersion";
            this.cmbArcGISVersion.Size = new System.Drawing.Size(260, 20);
            this.cmbArcGISVersion.TabIndex = 2;
            // 
            // btnInstallDotNet35
            // 
            this.btnInstallDotNet35.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInstallDotNet35.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInstallDotNet35.Location = new System.Drawing.Point(13, 51);
            this.btnInstallDotNet35.Name = "btnInstallDotNet35";
            this.btnInstallDotNet35.Size = new System.Drawing.Size(259, 31);
            this.btnInstallDotNet35.TabIndex = 0;
            this.btnInstallDotNet35.Text = "      1、安装.net Framework 3.5(sp1)";
            this.btnInstallDotNet35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInstallDotNet35.UseVisualStyleBackColor = true;
            this.btnInstallDotNet35.Click += new System.EventHandler(this.btnInstallDotNet35_Click);
            // 
            // btnInstallRuntime
            // 
            this.btnInstallRuntime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInstallRuntime.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInstallRuntime.Location = new System.Drawing.Point(13, 125);
            this.btnInstallRuntime.Name = "btnInstallRuntime";
            this.btnInstallRuntime.Size = new System.Drawing.Size(259, 31);
            this.btnInstallRuntime.TabIndex = 0;
            this.btnInstallRuntime.Text = "      3、安装ArcGIS Runtime / ArcMap";
            this.btnInstallRuntime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInstallRuntime.UseVisualStyleBackColor = true;
            this.btnInstallRuntime.Click += new System.EventHandler(this.btnInstallRuntime_Click);
            // 
            // btnInstallSoftware
            // 
            this.btnInstallSoftware.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInstallSoftware.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInstallSoftware.Location = new System.Drawing.Point(13, 197);
            this.btnInstallSoftware.Name = "btnInstallSoftware";
            this.btnInstallSoftware.Size = new System.Drawing.Size(259, 31);
            this.btnInstallSoftware.TabIndex = 0;
            this.btnInstallSoftware.Text = "      5、安装软件";
            this.btnInstallSoftware.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInstallSoftware.UseVisualStyleBackColor = true;
            this.btnInstallSoftware.Click += new System.EventHandler(this.btnInstallSoftware_Click);
            // 
            // lblOpenLicenseEcp
            // 
            this.lblOpenLicenseEcp.AutoSize = true;
            this.lblOpenLicenseEcp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblOpenLicenseEcp.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOpenLicenseEcp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblOpenLicenseEcp.Location = new System.Drawing.Point(11, 263);
            this.lblOpenLicenseEcp.MaximumSize = new System.Drawing.Size(330, 80);
            this.lblOpenLicenseEcp.Name = "lblOpenLicenseEcp";
            this.lblOpenLicenseEcp.Size = new System.Drawing.Size(137, 12);
            this.lblOpenLicenseEcp.TabIndex = 1;
            this.lblOpenLicenseEcp.Text = "打开ArcGIS License授权";
            this.lblOpenLicenseEcp.Click += new System.EventHandler(this.lblOpenLicenseEcp_Click);
            // 
            // lblStartupAdministrator
            // 
            this.lblStartupAdministrator.AutoSize = true;
            this.lblStartupAdministrator.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblStartupAdministrator.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStartupAdministrator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblStartupAdministrator.Location = new System.Drawing.Point(11, 313);
            this.lblStartupAdministrator.MaximumSize = new System.Drawing.Size(330, 80);
            this.lblStartupAdministrator.Name = "lblStartupAdministrator";
            this.lblStartupAdministrator.Size = new System.Drawing.Size(149, 12);
            this.lblStartupAdministrator.TabIndex = 1;
            this.lblStartupAdministrator.Text = "启动ArcGIS Administrator";
            this.lblStartupAdministrator.Click += new System.EventHandler(this.lblStartupAdministrator_Click);
            // 
            // btnInstallLisence
            // 
            this.btnInstallLisence.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInstallLisence.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInstallLisence.Location = new System.Drawing.Point(13, 161);
            this.btnInstallLisence.Name = "btnInstallLisence";
            this.btnInstallLisence.Size = new System.Drawing.Size(259, 31);
            this.btnInstallLisence.TabIndex = 0;
            this.btnInstallLisence.Text = "      4、安装ArcGIS License";
            this.btnInstallLisence.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInstallLisence.UseVisualStyleBackColor = true;
            this.btnInstallLisence.Click += new System.EventHandler(this.btnInstallLisence_Click);
            // 
            // btnInstallDotNet4
            // 
            this.btnInstallDotNet4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInstallDotNet4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInstallDotNet4.Location = new System.Drawing.Point(13, 87);
            this.btnInstallDotNet4.Name = "btnInstallDotNet4";
            this.btnInstallDotNet4.Size = new System.Drawing.Size(259, 31);
            this.btnInstallDotNet4.TabIndex = 0;
            this.btnInstallDotNet4.Text = "      2、安装.net Framework 4.0";
            this.btnInstallDotNet4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInstallDotNet4.UseVisualStyleBackColor = true;
            this.btnInstallDotNet4.Click += new System.EventHandler(this.btnInstallDotNet4_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage.Location = new System.Drawing.Point(303, 17);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessage.Size = new System.Drawing.Size(505, 347);
            this.txtMessage.TabIndex = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.richTextBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(816, 427);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "安装说明";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(810, 421);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // ArcGISEnvirCheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 453);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ArcGISEnvirCheckForm";
            this.Text = "软件环境检测搭建和系统安装";
            this.Shown += new System.EventHandler(this.btnCheckEnviroment_Click);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInstallDotNet35;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblTips;
        private System.Windows.Forms.Button btnInstallLisence;
        private System.Windows.Forms.Button btnInstallSoftware;
        private System.Windows.Forms.Button btnInstallRuntime;
        private System.Windows.Forms.Label lblStartupLicenseManager;
        private System.Windows.Forms.Button btnCheckEnviroment;
        private System.Windows.Forms.Button btnInstallDotNet4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblStartupAdministrator;
        private System.Windows.Forms.Label lblOpenLicenseEcp;
        private System.Windows.Forms.ComboBox cmbArcGISVersion;
    }
}

