using WLib.WinCtrls.Extension;
using WLib.WinCtrls.PathCtrl;

namespace WLib.WinCtrls.AddItemCtrl
{
    partial class ConcatPathForm
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
            this.gbOption = new System.Windows.Forms.GroupBox();
            this.radioFilter = new System.Windows.Forms.RadioButton();
            this.radioConcat = new System.Windows.Forms.RadioButton();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxResult = new System.Windows.Forms.ListBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnConcat = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.panelDir = new System.Windows.Forms.Panel();
            this.pathBoxDir = new PathBoxSimple();
            this.lblInputTips = new System.Windows.Forms.Label();
            this.cListBoxSplitDef = new System.Windows.Forms.CheckedListBox();
            this.cbSettings = new System.Windows.Forms.CheckBox();
            this.splitContrInput = new System.Windows.Forms.SplitContainer();
            this.cbContainsSuffix = new System.Windows.Forms.CheckBox();
            this.txtSuffix = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbOption.SuspendLayout();
            this.panelDir.SuspendLayout();
            this.splitContrInput.Panel1.SuspendLayout();
            this.splitContrInput.Panel2.SuspendLayout();
            this.splitContrInput.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOption
            // 
            this.gbOption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOption.Controls.Add(this.radioFilter);
            this.gbOption.Controls.Add(this.radioConcat);
            this.gbOption.Location = new System.Drawing.Point(12, 9);
            this.gbOption.Name = "gbOption";
            this.gbOption.Size = new System.Drawing.Size(475, 50);
            this.gbOption.TabIndex = 0;
            this.gbOption.TabStop = false;
            this.gbOption.Text = "选项";
            // 
            // radioFilter
            // 
            this.radioFilter.AutoSize = true;
            this.radioFilter.Location = new System.Drawing.Point(281, 20);
            this.radioFilter.Name = "radioFilter";
            this.radioFilter.Size = new System.Drawing.Size(191, 16);
            this.radioFilter.TabIndex = 0;
            this.radioFilter.Text = "从路径中选出文件名(文件夹名)\r\n";
            this.radioFilter.UseVisualStyleBackColor = true;
            this.radioFilter.CheckedChanged += new System.EventHandler(this.radioFilter_CheckedChanged);
            // 
            // radioConcat
            // 
            this.radioConcat.AutoSize = true;
            this.radioConcat.Checked = true;
            this.radioConcat.Location = new System.Drawing.Point(9, 21);
            this.radioConcat.Name = "radioConcat";
            this.radioConcat.Size = new System.Drawing.Size(251, 16);
            this.radioConcat.TabIndex = 0;
            this.radioConcat.TabStop = true;
            this.radioConcat.Text = "将目录和文件名(文件夹名)拼接为完整路径";
            this.radioConcat.UseVisualStyleBackColor = true;
            // 
            // txtInput
            // 
            this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput.Location = new System.Drawing.Point(0, 0);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInput.Size = new System.Drawing.Size(335, 125);
            this.txtInput.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "目录路径：";
            // 
            // listBoxResult
            // 
            this.listBoxResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxResult.FormattingEnabled = true;
            this.listBoxResult.ItemHeight = 12;
            this.listBoxResult.Location = new System.Drawing.Point(12, 304);
            this.listBoxResult.Name = "listBoxResult";
            this.listBoxResult.Size = new System.Drawing.Size(475, 124);
            this.listBoxResult.TabIndex = 3;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(12, 285);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(65, 12);
            this.lblResult.TabIndex = 2;
            this.lblResult.Text = "拼接结果：";
            // 
            // btnConcat
            // 
            this.btnConcat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConcat.Location = new System.Drawing.Point(394, 266);
            this.btnConcat.Name = "btnConcat";
            this.btnConcat.Size = new System.Drawing.Size(93, 32);
            this.btnConcat.TabIndex = 4;
            this.btnConcat.Text = "拼接(&F)";
            this.btnConcat.UseVisualStyleBackColor = true;
            this.btnConcat.Click += new System.EventHandler(this.btnConcat_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(394, 436);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 32);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(295, 436);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(93, 32);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panelDir
            // 
            this.panelDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDir.Controls.Add(this.pathBoxDir);
            this.panelDir.Controls.Add(this.label1);
            this.panelDir.Location = new System.Drawing.Point(5, 64);
            this.panelDir.Name = "panelDir";
            this.panelDir.Size = new System.Drawing.Size(491, 35);
            this.panelDir.TabIndex = 5;
            // 
            // pathBoxDir
            // 
            this.pathBoxDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathBoxDir.ButtonsSplitWidth = 2;
            this.pathBoxDir.ButtonWidth = 80;
            this.pathBoxDir.DefaultTips = "粘贴路径于此，或点击选择按钮以选择路径";
            this.pathBoxDir.FileFilter = null;
            this.pathBoxDir.Location = new System.Drawing.Point(79, 4);
            this.pathBoxDir.MaximumSize = new System.Drawing.Size(9999, 100);
            this.pathBoxDir.MinimumSize = new System.Drawing.Size(0, 28);
            this.pathBoxDir.Name = "pathBoxDir";
            this.pathBoxDir.OperateButtonText = "操作";
            this.pathBoxDir.OptEnable = true;
            this.pathBoxDir.Path = "粘贴路径于此，或点击选择按钮以选择路径";
            this.pathBoxDir.PathToButtonSplitWidth = 2;
            this.pathBoxDir.ReadOnly = false;
            this.pathBoxDir.SelectButtonText = "选择";
            this.pathBoxDir.SelectEnable = true;
            this.pathBoxDir.SelectPathType = ESelectPathType.Folder;
            this.pathBoxDir.SelectTips = null;
            this.pathBoxDir.ShowButtonOption = EShowButtonOption.ViewSelect;
            this.pathBoxDir.Size = new System.Drawing.Size(403, 28);
            this.pathBoxDir.TabIndex = 3;
            // 
            // lblInputTips
            // 
            this.lblInputTips.AutoSize = true;
            this.lblInputTips.Location = new System.Drawing.Point(12, 115);
            this.lblInputTips.Name = "lblInputTips";
            this.lblInputTips.Size = new System.Drawing.Size(131, 12);
            this.lblInputTips.TabIndex = 2;
            this.lblInputTips.Text = "文件夹名/文件名列表：";
            // 
            // cListBoxSplitDef
            // 
            this.cListBoxSplitDef.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cListBoxSplitDef.FormattingEnabled = true;
            this.cListBoxSplitDef.Location = new System.Drawing.Point(0, 0);
            this.cListBoxSplitDef.Name = "cListBoxSplitDef";
            this.cListBoxSplitDef.Size = new System.Drawing.Size(139, 125);
            this.cListBoxSplitDef.TabIndex = 7;
            // 
            // cbSettings
            // 
            this.cbSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSettings.AutoSize = true;
            this.cbSettings.Location = new System.Drawing.Point(172, 5);
            this.cbSettings.Name = "cbSettings";
            this.cbSettings.Size = new System.Drawing.Size(138, 16);
            this.cbSettings.TabIndex = 8;
            this.cbSettings.Text = "设置提取列表的方式:";
            this.cbSettings.UseVisualStyleBackColor = true;
            this.cbSettings.CheckedChanged += new System.EventHandler(this.cbSettings_CheckedChanged);
            // 
            // splitContrInput
            // 
            this.splitContrInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContrInput.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContrInput.Location = new System.Drawing.Point(12, 135);
            this.splitContrInput.Name = "splitContrInput";
            // 
            // splitContrInput.Panel1
            // 
            this.splitContrInput.Panel1.Controls.Add(this.txtInput);
            // 
            // splitContrInput.Panel2
            // 
            this.splitContrInput.Panel2.Controls.Add(this.cListBoxSplitDef);
            this.splitContrInput.Size = new System.Drawing.Size(475, 125);
            this.splitContrInput.SplitterDistance = 335;
            this.splitContrInput.SplitterWidth = 1;
            this.splitContrInput.TabIndex = 9;
            // 
            // cbContainsSuffix
            // 
            this.cbContainsSuffix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbContainsSuffix.AutoSize = true;
            this.cbContainsSuffix.Location = new System.Drawing.Point(7, 5);
            this.cbContainsSuffix.Name = "cbContainsSuffix";
            this.cbContainsSuffix.Size = new System.Drawing.Size(72, 16);
            this.cbContainsSuffix.TabIndex = 8;
            this.cbContainsSuffix.Text = "加入后缀";
            this.cbContainsSuffix.UseVisualStyleBackColor = true;
            this.cbContainsSuffix.CheckedChanged += new System.EventHandler(this.cbContainsSuffix_CheckedChanged);
            // 
            // txtSuffix
            // 
            this.txtSuffix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSuffix.Location = new System.Drawing.Point(81, 2);
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new System.Drawing.Size(65, 21);
            this.txtSuffix.TabIndex = 10;
            this.txtSuffix.Text = ".txt";
            this.txtSuffix.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbContainsSuffix);
            this.panel1.Controls.Add(this.txtSuffix);
            this.panel1.Controls.Add(this.cbSettings);
            this.panel1.Location = new System.Drawing.Point(179, 108);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(313, 25);
            this.panel1.TabIndex = 11;
            // 
            // ConcatPathForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(499, 480);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitContrInput);
            this.Controls.Add(this.lblInputTips);
            this.Controls.Add(this.panelDir);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConcat);
            this.Controls.Add(this.listBoxResult);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.gbOption);
            this.Name = "ConcatPathForm";
            this.Text = "路径筛选或拼接";
            this.gbOption.ResumeLayout(false);
            this.gbOption.PerformLayout();
            this.panelDir.ResumeLayout(false);
            this.panelDir.PerformLayout();
            this.splitContrInput.Panel1.ResumeLayout(false);
            this.splitContrInput.Panel1.PerformLayout();
            this.splitContrInput.Panel2.ResumeLayout(false);
            this.splitContrInput.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOption;
        private System.Windows.Forms.RadioButton radioConcat;
        private System.Windows.Forms.RadioButton radioFilter;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxResult;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnConcat;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel panelDir;
        private System.Windows.Forms.Label lblInputTips;
        private PathBoxSimple pathBoxDir;
        private System.Windows.Forms.CheckedListBox cListBoxSplitDef;
        private System.Windows.Forms.CheckBox cbSettings;
        private System.Windows.Forms.SplitContainer splitContrInput;
        private System.Windows.Forms.CheckBox cbContainsSuffix;
        private System.Windows.Forms.TextBox txtSuffix;
        private System.Windows.Forms.Panel panel1;
    }
}