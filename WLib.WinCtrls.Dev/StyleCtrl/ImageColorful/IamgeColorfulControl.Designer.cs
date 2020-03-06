namespace WLib.WinCtrls.Dev.StyleCtrl.ImageColorful
{
    partial class IamgeColorfulControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IamgeColorfulControl));
            this.listBoxColors = new DevExpress.XtraEditors.ListBoxControl();
            this.groupCtrlColors = new DevExpress.XtraEditors.GroupControl();
            this.groupCtrlOption = new DevExpress.XtraEditors.GroupControl();
            this.panelCustom = new DevExpress.XtraEditors.PanelControl();
            this.cmbIconLib = new DevExpress.XtraEditors.ComboBoxEdit();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.colortEnd = new DevExpress.XtraEditors.ColorPickEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.colorStart = new DevExpress.XtraEditors.ColorPickEdit();
            this.panelOptions = new DevExpress.XtraEditors.PanelControl();
            this.btnReset = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenImageDir = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.listBoxColors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupCtrlColors)).BeginInit();
            this.groupCtrlColors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupCtrlOption)).BeginInit();
            this.groupCtrlOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelCustom)).BeginInit();
            this.panelCustom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIconLib.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colortEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelOptions)).BeginInit();
            this.panelOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxColors
            // 
            this.listBoxColors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxColors.Location = new System.Drawing.Point(2, 27);
            this.listBoxColors.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listBoxColors.Name = "listBoxColors";
            this.listBoxColors.Size = new System.Drawing.Size(502, 290);
            this.listBoxColors.TabIndex = 1;
            this.listBoxColors.SelectedIndexChanged += new System.EventHandler(this.listBoxColors_SelectedIndexChanged);
            // 
            // groupCtrlColors
            // 
            this.groupCtrlColors.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupCtrlColors.CaptionImageOptions.Image")));
            this.groupCtrlColors.Controls.Add(this.listBoxColors);
            this.groupCtrlColors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupCtrlColors.Location = new System.Drawing.Point(0, 0);
            this.groupCtrlColors.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupCtrlColors.Name = "groupCtrlColors";
            this.groupCtrlColors.Size = new System.Drawing.Size(506, 319);
            this.groupCtrlColors.TabIndex = 2;
            this.groupCtrlColors.Text = "请选择图标色彩风格：";
            // 
            // groupCtrlOption
            // 
            this.groupCtrlOption.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupCtrlOption.CaptionImageOptions.Image")));
            this.groupCtrlOption.Controls.Add(this.panelCustom);
            this.groupCtrlOption.Controls.Add(this.panelOptions);
            this.groupCtrlOption.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupCtrlOption.Location = new System.Drawing.Point(0, 319);
            this.groupCtrlOption.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupCtrlOption.Name = "groupCtrlOption";
            this.groupCtrlOption.Size = new System.Drawing.Size(506, 132);
            this.groupCtrlOption.TabIndex = 3;
            this.groupCtrlOption.Text = "自定义选项";
            // 
            // panelCustom
            // 
            this.panelCustom.Controls.Add(this.cmbIconLib);
            this.panelCustom.Controls.Add(this.tableLayoutPanel1);
            this.panelCustom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCustom.Location = new System.Drawing.Point(2, 27);
            this.panelCustom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelCustom.Name = "panelCustom";
            this.panelCustom.Size = new System.Drawing.Size(347, 103);
            this.panelCustom.TabIndex = 2;
            // 
            // cmbIconLib
            // 
            this.cmbIconLib.Location = new System.Drawing.Point(20, 61);
            this.cmbIconLib.Name = "cmbIconLib";
            this.cmbIconLib.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbIconLib.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbIconLib.Size = new System.Drawing.Size(316, 24);
            this.cmbIconLib.TabIndex = 3;
            this.cmbIconLib.SelectedIndexChanged += new System.EventHandler(this.cmbIconLib_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.labelControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.colortEnd, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelControl2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.colorStart, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 9);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(319, 35);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(3, 4);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(15, 18);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "从";
            // 
            // colortEnd
            // 
            this.colortEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colortEnd.EditValue = System.Drawing.Color.Empty;
            this.colortEnd.Location = new System.Drawing.Point(185, 4);
            this.colortEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.colortEnd.Name = "colortEnd";
            this.colortEnd.Properties.AutomaticColor = System.Drawing.Color.Black;
            this.colortEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colortEnd.Size = new System.Drawing.Size(131, 24);
            this.colortEnd.TabIndex = 1;
            this.colortEnd.EditValueChanged += new System.EventHandler(this.colorPickEdit_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(162, 4);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(15, 18);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "至";
            // 
            // colorStart
            // 
            this.colorStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorStart.EditValue = System.Drawing.Color.Empty;
            this.colorStart.Location = new System.Drawing.Point(26, 4);
            this.colorStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.colorStart.Name = "colorStart";
            this.colorStart.Properties.AutomaticColor = System.Drawing.Color.Black;
            this.colorStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorStart.Size = new System.Drawing.Size(130, 24);
            this.colorStart.TabIndex = 1;
            this.colorStart.EditValueChanged += new System.EventHandler(this.colorPickEdit_EditValueChanged);
            // 
            // panelOptions
            // 
            this.panelOptions.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelOptions.Controls.Add(this.btnReset);
            this.panelOptions.Controls.Add(this.btnOpenImageDir);
            this.panelOptions.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelOptions.Location = new System.Drawing.Point(349, 27);
            this.panelOptions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelOptions.Name = "panelOptions";
            this.panelOptions.Size = new System.Drawing.Size(155, 103);
            this.panelOptions.TabIndex = 2;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(6, 4);
            this.btnReset.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(145, 42);
            this.btnReset.TabIndex = 0;
            this.btnReset.Text = "重置图标";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnOpenImageDir
            // 
            this.btnOpenImageDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenImageDir.Location = new System.Drawing.Point(6, 52);
            this.btnOpenImageDir.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOpenImageDir.Name = "btnOpenImageDir";
            this.btnOpenImageDir.Size = new System.Drawing.Size(145, 42);
            this.btnOpenImageDir.TabIndex = 0;
            this.btnOpenImageDir.Text = "打开图标目录";
            this.btnOpenImageDir.Click += new System.EventHandler(this.btnOpenImageDir_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "daishu.png");
            this.imageCollection1.Images.SetKeyName(1, "daxiang.png");
            this.imageCollection1.Images.SetKeyName(2, "e.png");
            this.imageCollection1.Images.SetKeyName(3, "gongji.png");
            this.imageCollection1.Images.SetKeyName(4, "gonglu.png");
            this.imageCollection1.Images.SetKeyName(5, "gou.png");
            this.imageCollection1.Images.SetKeyName(6, "hou.png");
            this.imageCollection1.Images.SetKeyName(7, "huaban.png");
            this.imageCollection1.Images.SetKeyName(8, "huli.png");
            this.imageCollection1.Images.SetKeyName(9, "lang.png");
            this.imageCollection1.Images.SetKeyName(10, "lu.png");
            this.imageCollection1.Images.SetKeyName(11, "luotuo.png");
            this.imageCollection1.Images.SetKeyName(12, "ma.png");
            this.imageCollection1.Images.SetKeyName(13, "maqueniao.png");
            this.imageCollection1.Images.SetKeyName(14, "mianyang.png");
            this.imageCollection1.Images.SetKeyName(15, "niao.png");
            this.imageCollection1.Images.SetKeyName(16, "tuoniao.png");
            this.imageCollection1.Images.SetKeyName(17, "tuzi.png");
            this.imageCollection1.Images.SetKeyName(18, "yang.png");
            this.imageCollection1.Images.SetKeyName(19, "zhu.png");
            this.imageCollection1.Images.SetKeyName(20, "beijingshi.png");
            this.imageCollection1.Images.SetKeyName(21, "fujiansheng.png");
            this.imageCollection1.Images.SetKeyName(22, "gansusheng.png");
            this.imageCollection1.Images.SetKeyName(23, "guangdongsheng.png");
            this.imageCollection1.Images.SetKeyName(24, "guangxizizhiqu.png");
            this.imageCollection1.Images.SetKeyName(25, "guizhousheng.png");
            this.imageCollection1.Images.SetKeyName(26, "hainansheng.png");
            this.imageCollection1.Images.SetKeyName(27, "hebeisheng.png");
            this.imageCollection1.Images.SetKeyName(28, "heilongjiangsheng.png");
            this.imageCollection1.Images.SetKeyName(29, "henansheng.png");
            this.imageCollection1.Images.SetKeyName(30, "hubeisheng.png");
            this.imageCollection1.Images.SetKeyName(31, "hunansheng.png");
            this.imageCollection1.Images.SetKeyName(32, "jiangsusheng.png");
            this.imageCollection1.Images.SetKeyName(33, "jiangxisheng.png");
            this.imageCollection1.Images.SetKeyName(34, "jilinsheng.png");
            this.imageCollection1.Images.SetKeyName(35, "liaoningsheng.png");
            this.imageCollection1.Images.SetKeyName(36, "neimengguzizhiqu.png");
            this.imageCollection1.Images.SetKeyName(37, "ningxiazizhiqu.png");
            this.imageCollection1.Images.SetKeyName(38, "qinghaisheng.png");
            this.imageCollection1.Images.SetKeyName(39, "shandongsheng.png");
            this.imageCollection1.Images.SetKeyName(40, "shandongsheng_1.png");
            this.imageCollection1.Images.SetKeyName(41, "shanxisheng.png");
            this.imageCollection1.Images.SetKeyName(42, "sichuansheng.png");
            this.imageCollection1.Images.SetKeyName(43, "taiwansheng.png");
            this.imageCollection1.Images.SetKeyName(44, "tianjinshi.png");
            this.imageCollection1.Images.SetKeyName(45, "xicangzizhiqu.png");
            this.imageCollection1.Images.SetKeyName(46, "xinjiangzizhiqu.png");
            this.imageCollection1.Images.SetKeyName(47, "yunnansheng.png");
            this.imageCollection1.Images.SetKeyName(48, "zhejiangsheng.png");
            this.imageCollection1.Images.SetKeyName(49, "zhongqingshi.png");
            // 
            // IamgeColorfulControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupCtrlColors);
            this.Controls.Add(this.groupCtrlOption);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "IamgeColorfulControl";
            this.Size = new System.Drawing.Size(506, 451);
            ((System.ComponentModel.ISupportInitialize)(this.listBoxColors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupCtrlColors)).EndInit();
            this.groupCtrlColors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupCtrlOption)).EndInit();
            this.groupCtrlOption.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelCustom)).EndInit();
            this.panelCustom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbIconLib.Properties)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colortEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelOptions)).EndInit();
            this.panelOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.ListBoxControl listBoxColors;
        private DevExpress.XtraEditors.GroupControl groupCtrlColors;
        private DevExpress.XtraEditors.GroupControl groupCtrlOption;
        private DevExpress.XtraEditors.SimpleButton btnReset;
        private DevExpress.XtraEditors.SimpleButton btnOpenImageDir;
        private DevExpress.XtraEditors.ColorPickEdit colorStart;
        private DevExpress.XtraEditors.ColorPickEdit colortEnd;
        private DevExpress.XtraEditors.PanelControl panelCustom;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl panelOptions;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbIconLib;
    }
}
