namespace WLib.WinCtrls.PluginCtrl
{
    partial class PluginContainerInfoCtrl
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
            this.lblShortCutKeys = new System.Windows.Forms.Label();
            this.txtCaption = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.txtContainerType = new System.Windows.Forms.TextBox();
            this.cmbShortcutKeys = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.cbVisible = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblShortCutKeys
            // 
            this.lblShortCutKeys.AutoSize = true;
            this.lblShortCutKeys.Location = new System.Drawing.Point(3, 85);
            this.lblShortCutKeys.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblShortCutKeys.Name = "lblShortCutKeys";
            this.lblShortCutKeys.Size = new System.Drawing.Size(53, 12);
            this.lblShortCutKeys.TabIndex = 6;
            this.lblShortCutKeys.Text = "快捷键：";
            // 
            // txtCaption
            // 
            this.txtCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCaption.Location = new System.Drawing.Point(62, 29);
            this.txtCaption.Margin = new System.Windows.Forms.Padding(2);
            this.txtCaption.Name = "txtCaption";
            this.txtCaption.Size = new System.Drawing.Size(320, 21);
            this.txtCaption.TabIndex = 0;
            this.txtCaption.TextChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 31);
            this.lblName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(53, 12);
            this.lblName.TabIndex = 7;
            this.lblName.Text = "标  题：";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(3, 58);
            this.lblType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(53, 12);
            this.lblType.TabIndex = 6;
            this.lblType.Text = "类  别：";
            // 
            // txtContainerType
            // 
            this.txtContainerType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContainerType.Location = new System.Drawing.Point(62, 56);
            this.txtContainerType.Margin = new System.Windows.Forms.Padding(2);
            this.txtContainerType.Name = "txtContainerType";
            this.txtContainerType.ReadOnly = true;
            this.txtContainerType.Size = new System.Drawing.Size(320, 21);
            this.txtContainerType.TabIndex = 4;
            this.txtContainerType.TextChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // cmbShortcutKeys
            // 
            this.cmbShortcutKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbShortcutKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShortcutKeys.FormattingEnabled = true;
            this.cmbShortcutKeys.Location = new System.Drawing.Point(62, 83);
            this.cmbShortcutKeys.Margin = new System.Windows.Forms.Padding(2);
            this.cmbShortcutKeys.Name = "cmbShortcutKeys";
            this.cmbShortcutKeys.Size = new System.Drawing.Size(320, 20);
            this.cmbShortcutKeys.TabIndex = 2;
            this.cmbShortcutKeys.SelectedIndexChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "名  称：";
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Location = new System.Drawing.Point(62, 2);
            this.txtName.Margin = new System.Windows.Forms.Padding(2);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(320, 21);
            this.txtName.TabIndex = 3;
            this.txtName.TextChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbShortcutKeys, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtCaption, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblShortCutKeys, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtContainerType, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblType, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbVisible, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.99813F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(384, 131);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 112);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "快捷键：";
            // 
            // cbVisible
            // 
            this.cbVisible.AutoSize = true;
            this.cbVisible.Location = new System.Drawing.Point(63, 111);
            this.cbVisible.Name = "cbVisible";
            this.cbVisible.Size = new System.Drawing.Size(84, 16);
            this.cbVisible.TabIndex = 9;
            this.cbVisible.Text = "显示该菜单";
            this.cbVisible.UseVisualStyleBackColor = true;
            this.cbVisible.CheckedChanged += new System.EventHandler(this.CbVisible_CheckedChanged);
            // 
            // PluginContainerInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PluginContainerInfoForm";
            this.Size = new System.Drawing.Size(384, 131);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblShortCutKeys;
        private System.Windows.Forms.TextBox txtCaption;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.TextBox txtContainerType;
        private System.Windows.Forms.ComboBox cmbShortcutKeys;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbVisible;
    }
}