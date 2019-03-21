namespace GISsys
{
    partial class frmRender
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRender));
            this.lyr_label = new System.Windows.Forms.Label();
            this.featCls_comboBox = new System.Windows.Forms.ComboBox();
            this.lyr2_label = new System.Windows.Forms.Label();
            this.featCls2_comboBox = new System.Windows.Forms.ComboBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.lineColor_button = new System.Windows.Forms.Button();
            this.field_comboBox = new System.Windows.Forms.ComboBox();
            this.field2_comboBox = new System.Windows.Forms.ComboBox();
            this.lyrFiled_label = new System.Windows.Forms.Label();
            this.lyr2Field_label = new System.Windows.Forms.Label();
            this.OK_button = new System.Windows.Forms.Button();
            this.Cancel_button = new System.Windows.Forms.Button();
            this.fillColor_button = new System.Windows.Forms.Button();
            this.classUpDown = new System.Windows.Forms.NumericUpDown();
            this.class_label = new System.Windows.Forms.Label();
            this.lineColor_label = new System.Windows.Forms.Label();
            this.fillColor_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.classUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // lyr_label
            // 
            this.lyr_label.AutoSize = true;
            this.lyr_label.Location = new System.Drawing.Point(13, 9);
            this.lyr_label.Name = "lyr_label";
            this.lyr_label.Size = new System.Drawing.Size(53, 12);
            this.lyr_label.TabIndex = 3;
            this.lyr_label.Text = "选择图层";
            // 
            // featCls_comboBox
            // 
            this.featCls_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.featCls_comboBox.FormattingEnabled = true;
            this.featCls_comboBox.Location = new System.Drawing.Point(12, 24);
            this.featCls_comboBox.Name = "featCls_comboBox";
            this.featCls_comboBox.Size = new System.Drawing.Size(223, 20);
            this.featCls_comboBox.TabIndex = 2;
            // 
            // lyr2_label
            // 
            this.lyr2_label.AutoSize = true;
            this.lyr2_label.Location = new System.Drawing.Point(10, 118);
            this.lyr2_label.Name = "lyr2_label";
            this.lyr2_label.Size = new System.Drawing.Size(59, 12);
            this.lyr2_label.TabIndex = 3;
            this.lyr2_label.Text = "选择图层2";
            // 
            // featCls2_comboBox
            // 
            this.featCls2_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.featCls2_comboBox.Enabled = false;
            this.featCls2_comboBox.FormattingEnabled = true;
            this.featCls2_comboBox.Location = new System.Drawing.Point(12, 133);
            this.featCls2_comboBox.Name = "featCls2_comboBox";
            this.featCls2_comboBox.Size = new System.Drawing.Size(223, 20);
            this.featCls2_comboBox.TabIndex = 2;
            // 
            // lineColor_button
            // 
            this.lineColor_button.Location = new System.Drawing.Point(71, 223);
            this.lineColor_button.Name = "lineColor_button";
            this.lineColor_button.Size = new System.Drawing.Size(55, 23);
            this.lineColor_button.TabIndex = 5;
            this.lineColor_button.UseVisualStyleBackColor = true;
            // 
            // field_comboBox
            // 
            this.field_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.field_comboBox.FormattingEnabled = true;
            this.field_comboBox.Location = new System.Drawing.Point(12, 74);
            this.field_comboBox.Name = "field_comboBox";
            this.field_comboBox.Size = new System.Drawing.Size(223, 20);
            this.field_comboBox.TabIndex = 2;
            // 
            // field2_comboBox
            // 
            this.field2_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.field2_comboBox.Enabled = false;
            this.field2_comboBox.FormattingEnabled = true;
            this.field2_comboBox.Location = new System.Drawing.Point(12, 182);
            this.field2_comboBox.Name = "field2_comboBox";
            this.field2_comboBox.Size = new System.Drawing.Size(223, 20);
            this.field2_comboBox.TabIndex = 2;
            // 
            // lyrFiled_label
            // 
            this.lyrFiled_label.AutoSize = true;
            this.lyrFiled_label.Location = new System.Drawing.Point(13, 59);
            this.lyrFiled_label.Name = "lyrFiled_label";
            this.lyrFiled_label.Size = new System.Drawing.Size(77, 12);
            this.lyrFiled_label.TabIndex = 3;
            this.lyrFiled_label.Text = "选择图层字段";
            // 
            // lyr2Field_label
            // 
            this.lyr2Field_label.AutoSize = true;
            this.lyr2Field_label.Location = new System.Drawing.Point(13, 167);
            this.lyr2Field_label.Name = "lyr2Field_label";
            this.lyr2Field_label.Size = new System.Drawing.Size(83, 12);
            this.lyr2Field_label.TabIndex = 3;
            this.lyr2Field_label.Text = "选择图层2字段";
            // 
            // OK_button
            // 
            this.OK_button.Location = new System.Drawing.Point(79, 298);
            this.OK_button.Name = "OK_button";
            this.OK_button.Size = new System.Drawing.Size(75, 23);
            this.OK_button.TabIndex = 7;
            this.OK_button.Text = "确定";
            this.OK_button.UseVisualStyleBackColor = true;
            // 
            // Cancel_button
            // 
            this.Cancel_button.Location = new System.Drawing.Point(160, 298);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_button.TabIndex = 7;
            this.Cancel_button.Text = "取消";
            this.Cancel_button.UseVisualStyleBackColor = true;
            // 
            // fillColor_button
            // 
            this.fillColor_button.Location = new System.Drawing.Point(71, 253);
            this.fillColor_button.Name = "fillColor_button";
            this.fillColor_button.Size = new System.Drawing.Size(55, 23);
            this.fillColor_button.TabIndex = 5;
            this.fillColor_button.UseVisualStyleBackColor = true;
            // 
            // classUpDown
            // 
            this.classUpDown.Location = new System.Drawing.Point(185, 249);
            this.classUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.classUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.classUpDown.Name = "classUpDown";
            this.classUpDown.Size = new System.Drawing.Size(50, 21);
            this.classUpDown.TabIndex = 8;
            this.classUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.classUpDown.Visible = false;
            // 
            // class_label
            // 
            this.class_label.AutoSize = true;
            this.class_label.Location = new System.Drawing.Point(183, 230);
            this.class_label.Name = "class_label";
            this.class_label.Size = new System.Drawing.Size(41, 12);
            this.class_label.TabIndex = 3;
            this.class_label.Text = "分级：";
            this.class_label.Visible = false;
            // 
            // lineColor_label
            // 
            this.lineColor_label.AutoSize = true;
            this.lineColor_label.Location = new System.Drawing.Point(12, 228);
            this.lineColor_label.Name = "lineColor_label";
            this.lineColor_label.Size = new System.Drawing.Size(53, 12);
            this.lineColor_label.TabIndex = 9;
            this.lineColor_label.Text = "线条颜色";
            // 
            // fillColor_label
            // 
            this.fillColor_label.AutoSize = true;
            this.fillColor_label.Location = new System.Drawing.Point(12, 258);
            this.fillColor_label.Name = "fillColor_label";
            this.fillColor_label.Size = new System.Drawing.Size(53, 12);
            this.fillColor_label.TabIndex = 9;
            this.fillColor_label.Text = "填充颜色";
            // 
            // frmRender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 333);
            this.Controls.Add(this.fillColor_label);
            this.Controls.Add(this.lineColor_label);
            this.Controls.Add(this.classUpDown);
            this.Controls.Add(this.Cancel_button);
            this.Controls.Add(this.OK_button);
            this.Controls.Add(this.fillColor_button);
            this.Controls.Add(this.lineColor_button);
            this.Controls.Add(this.class_label);
            this.Controls.Add(this.lyr2Field_label);
            this.Controls.Add(this.lyr2_label);
            this.Controls.Add(this.lyrFiled_label);
            this.Controls.Add(this.lyr_label);
            this.Controls.Add(this.featCls2_comboBox);
            this.Controls.Add(this.field2_comboBox);
            this.Controls.Add(this.field_comboBox);
            this.Controls.Add(this.featCls_comboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRender";
            this.Text = "`";
            ((System.ComponentModel.ISupportInitialize)(this.classUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lyr_label;
        private System.Windows.Forms.ComboBox featCls_comboBox;
        private System.Windows.Forms.Label lyr2_label;
        private System.Windows.Forms.ComboBox featCls2_comboBox;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button lineColor_button;
        private System.Windows.Forms.ComboBox field_comboBox;
        private System.Windows.Forms.ComboBox field2_comboBox;
        private System.Windows.Forms.Label lyrFiled_label;
        private System.Windows.Forms.Label lyr2Field_label;
        private System.Windows.Forms.Button OK_button;
        private System.Windows.Forms.Button Cancel_button;
        private System.Windows.Forms.Button fillColor_button;
        private System.Windows.Forms.NumericUpDown classUpDown;
        private System.Windows.Forms.Label class_label;
        private System.Windows.Forms.Label lineColor_label;
        private System.Windows.Forms.Label fillColor_label;
    }
}