namespace WLib.Samples.WinForm.SubForm
{
    partial class ExportMapForm
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
            this.btnExpt = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxPlus1 = new WLib.UserCtrls.ListCtrl.ListBoxPlus();
            this.pathBoxDb = new WLib.UserCtrls.PathCtrl.PathBoxSimple();
            this.pathBoxDir = new WLib.UserCtrls.PathCtrl.PathBoxSimple();
            this.pathBoxTemplate = new WLib.UserCtrls.PathCtrl.PathBoxSimple();
            this.numDpi = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbPicExtension = new System.Windows.Forms.ComboBox();
            this.txtRegionName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbExportMapType = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numDpi)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExpt
            // 
            this.btnExpt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExpt.Location = new System.Drawing.Point(678, 423);
            this.btnExpt.Name = "btnExpt";
            this.btnExpt.Size = new System.Drawing.Size(161, 44);
            this.btnExpt.TabIndex = 0;
            this.btnExpt.Text = "导出";
            this.btnExpt.UseVisualStyleBackColor = true;
            this.btnExpt.Click += new System.EventHandler(this.btnExprt_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage.Location = new System.Drawing.Point(262, 141);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessage.Size = new System.Drawing.Size(577, 276);
            this.txtMessage.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "导出目录";
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(511, 423);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(161, 44);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "数据库：";
            // 
            // listBoxPlus1
            // 
            this.listBoxPlus1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxPlus1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxPlus1.Description = "行政村列表：";
            this.listBoxPlus1.ListTitle = null;
            this.listBoxPlus1.Location = new System.Drawing.Point(12, 114);
            this.listBoxPlus1.Name = "listBoxPlus1";
            this.listBoxPlus1.ResponseCount = 30;
            this.listBoxPlus1.SearchDescription = "请输入要查找的项";
            this.listBoxPlus1.ShowRefreshButton = false;
            this.listBoxPlus1.ShowSelectRegion = true;
            this.listBoxPlus1.Size = new System.Drawing.Size(244, 352);
            this.listBoxPlus1.TabIndex = 4;
            this.listBoxPlus1.ViewOnly = false;
            // 
            // pathBoxDb
            // 
            this.pathBoxDb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathBoxDb.ButtonsSplitWidth = 2;
            this.pathBoxDb.ButtonWidth = 80;
            this.pathBoxDb.Cursor = System.Windows.Forms.Cursors.Default;
            this.pathBoxDb.DefaultTips = "粘贴路径于此并按下回车，或点击选择按钮以选择路径";
            this.pathBoxDb.FileFilter = "*.mdb|*.mdb";
            this.pathBoxDb.Location = new System.Drawing.Point(76, 46);
            this.pathBoxDb.MaximumSize = new System.Drawing.Size(9999, 100);
            this.pathBoxDb.MinimumSize = new System.Drawing.Size(0, 28);
            this.pathBoxDb.Name = "pathBoxDb";
            this.pathBoxDb.OperateButtonText = "操作";
            this.pathBoxDb.OptEnable = true;
            this.pathBoxDb.Path = "";
            this.pathBoxDb.PathToButtonSplitWidth = 2;
            this.pathBoxDb.ReadOnly = false;
            this.pathBoxDb.SelectButtonText = "选择";
            this.pathBoxDb.SelectEnable = true;
            this.pathBoxDb.SelectPathType = WLib.UserCtrls.PathCtrl.ESelectPathType.OpenFile;
            this.pathBoxDb.SelectTips = null;
            this.pathBoxDb.ShowButtonOption = WLib.UserCtrls.PathCtrl.EShowButtonOption.ViewSelect;
            this.pathBoxDb.Size = new System.Drawing.Size(763, 28);
            this.pathBoxDb.TabIndex = 1;
            this.pathBoxDb.AfeterSelectPath += new System.EventHandler(this.pathBoxDb_AfeterSelectPath);
            // 
            // pathBoxDir
            // 
            this.pathBoxDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathBoxDir.ButtonsSplitWidth = 2;
            this.pathBoxDir.ButtonWidth = 80;
            this.pathBoxDir.DefaultTips = "粘贴路径于此并按下回车，或点击选择按钮以选择路径";
            this.pathBoxDir.FileFilter = null;
            this.pathBoxDir.Location = new System.Drawing.Point(76, 80);
            this.pathBoxDir.MaximumSize = new System.Drawing.Size(9999, 100);
            this.pathBoxDir.MinimumSize = new System.Drawing.Size(0, 28);
            this.pathBoxDir.Name = "pathBoxDir";
            this.pathBoxDir.OperateButtonText = "操作";
            this.pathBoxDir.OptEnable = true;
            this.pathBoxDir.Path = "";
            this.pathBoxDir.PathToButtonSplitWidth = 2;
            this.pathBoxDir.ReadOnly = false;
            this.pathBoxDir.SelectButtonText = "选择";
            this.pathBoxDir.SelectEnable = true;
            this.pathBoxDir.SelectPathType = WLib.UserCtrls.PathCtrl.ESelectPathType.OpenFile;
            this.pathBoxDir.SelectTips = null;
            this.pathBoxDir.ShowButtonOption = WLib.UserCtrls.PathCtrl.EShowButtonOption.ViewSelect;
            this.pathBoxDir.Size = new System.Drawing.Size(763, 28);
            this.pathBoxDir.TabIndex = 1;
            // 
            // pathBoxTemplate
            // 
            this.pathBoxTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathBoxTemplate.ButtonsSplitWidth = 2;
            this.pathBoxTemplate.ButtonWidth = 80;
            this.pathBoxTemplate.DefaultTips = "粘贴路径于此并按下回车，或点击选择按钮以选择路径";
            this.pathBoxTemplate.FileFilter = null;
            this.pathBoxTemplate.Location = new System.Drawing.Point(166, 12);
            this.pathBoxTemplate.MaximumSize = new System.Drawing.Size(9999, 100);
            this.pathBoxTemplate.MinimumSize = new System.Drawing.Size(0, 28);
            this.pathBoxTemplate.Name = "pathBoxTemplate";
            this.pathBoxTemplate.OperateButtonText = "操作";
            this.pathBoxTemplate.OptEnable = true;
            this.pathBoxTemplate.Path = "";
            this.pathBoxTemplate.PathToButtonSplitWidth = 2;
            this.pathBoxTemplate.ReadOnly = false;
            this.pathBoxTemplate.SelectButtonText = "选择";
            this.pathBoxTemplate.SelectEnable = true;
            this.pathBoxTemplate.SelectPathType = WLib.UserCtrls.PathCtrl.ESelectPathType.OpenFile;
            this.pathBoxTemplate.SelectTips = null;
            this.pathBoxTemplate.ShowButtonOption = WLib.UserCtrls.PathCtrl.EShowButtonOption.ViewSelect;
            this.pathBoxTemplate.Size = new System.Drawing.Size(673, 28);
            this.pathBoxTemplate.TabIndex = 1;
            // 
            // numericUpDown1
            // 
            this.numDpi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numDpi.Location = new System.Drawing.Point(326, 3);
            this.numDpi.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numDpi.Name = "numericUpDown1";
            this.numDpi.Size = new System.Drawing.Size(82, 21);
            this.numDpi.TabIndex = 5;
            this.numDpi.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(217, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "图片分辨率(dpi)：";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(412, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "图片格式：";
            // 
            // cmbPicExtension
            // 
            this.cmbPicExtension.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPicExtension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPicExtension.FormattingEnabled = true;
            this.cmbPicExtension.Items.AddRange(new object[] {
            ".jpg",
            ".png",
            ".gif",
            ".tiff"});
            this.cmbPicExtension.Location = new System.Drawing.Point(481, 4);
            this.cmbPicExtension.Name = "cmbPicExtension";
            this.cmbPicExtension.Size = new System.Drawing.Size(95, 20);
            this.cmbPicExtension.TabIndex = 7;
            // 
            // txtRegionName
            // 
            this.txtRegionName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegionName.Location = new System.Drawing.Point(93, 4);
            this.txtRegionName.Name = "txtRegionName";
            this.txtRegionName.Size = new System.Drawing.Size(120, 21);
            this.txtRegionName.TabIndex = 8;
            this.txtRegionName.Text = "龙胜各族自治县";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "县名称：";
            // 
            // cmbExportMapType
            // 
            this.cmbExportMapType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExportMapType.FormattingEnabled = true;
            this.cmbExportMapType.Location = new System.Drawing.Point(14, 15);
            this.cmbExportMapType.Name = "cmbExportMapType";
            this.cmbExportMapType.Size = new System.Drawing.Size(146, 20);
            this.cmbExportMapType.TabIndex = 10;
            this.cmbExportMapType.SelectedIndexChanged += new System.EventHandler(this.cmbExportMapType_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.cmbPicExtension);
            this.panel1.Controls.Add(this.numDpi);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtRegionName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(262, 111);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(579, 28);
            this.panel1.TabIndex = 11;
            // 
            // ExportMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 478);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmbExportMapType);
            this.Controls.Add(this.listBoxPlus1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.pathBoxDb);
            this.Controls.Add(this.pathBoxDir);
            this.Controls.Add(this.pathBoxTemplate);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnExpt);
            this.Name = "ExportMapForm";
            this.Text = "导出地图";
            ((System.ComponentModel.ISupportInitialize)(this.numDpi)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExpt;
        private UserCtrls.PathCtrl.PathBoxSimple pathBoxTemplate;
        private UserCtrls.PathCtrl.PathBoxSimple pathBoxDir;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStop;
        private UserCtrls.ListCtrl.ListBoxPlus listBoxPlus1;
        private UserCtrls.PathCtrl.PathBoxSimple pathBoxDb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numDpi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbPicExtension;
        private System.Windows.Forms.TextBox txtRegionName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbExportMapType;
        private System.Windows.Forms.Panel panel1;
    }
}