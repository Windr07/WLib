namespace WLib.Samples.WinForm.View
{
    partial class frmQueryFeat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQueryFeat));
            this.featCls_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.field_listBox = new System.Windows.Forms.ListBox();
            this.sql_textBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.apply_button = new System.Windows.Forms.Button();
            this.close_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.clear_button = new System.Windows.Forms.Button();
            this.uniqueValue_listBox = new System.Windows.Forms.ListBox();
            this.UniqueValue_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // featCls_comboBox
            // 
            this.featCls_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.featCls_comboBox.FormattingEnabled = true;
            this.featCls_comboBox.Location = new System.Drawing.Point(12, 30);
            this.featCls_comboBox.Name = "featCls_comboBox";
            this.featCls_comboBox.Size = new System.Drawing.Size(326, 20);
            this.featCls_comboBox.TabIndex = 0;
            this.featCls_comboBox.SelectedIndexChanged += new System.EventHandler(this.featCls_comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择图层";
            // 
            // field_listBox
            // 
            this.field_listBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.field_listBox.FormattingEnabled = true;
            this.field_listBox.ItemHeight = 12;
            this.field_listBox.Location = new System.Drawing.Point(15, 73);
            this.field_listBox.Name = "field_listBox";
            this.field_listBox.Size = new System.Drawing.Size(210, 148);
            this.field_listBox.TabIndex = 2;
            this.field_listBox.DoubleClick += new System.EventHandler(this.field_DoubleClick);
            // 
            // sql_textBox
            // 
            this.sql_textBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.sql_textBox.Location = new System.Drawing.Point(12, 360);
            this.sql_textBox.Multiline = true;
            this.sql_textBox.Name = "sql_textBox";
            this.sql_textBox.Size = new System.Drawing.Size(326, 62);
            this.sql_textBox.TabIndex = 3;
            this.sql_textBox.Text = "NAME = \'广州市\' OR NAME = \'佛山市\'";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(231, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 26);
            this.button1.TabIndex = 4;
            this.button1.Text = "=";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(287, 58);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 26);
            this.button2.TabIndex = 4;
            this.button2.Text = "< >";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(231, 90);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(50, 26);
            this.button3.TabIndex = 4;
            this.button3.Text = ">";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(231, 122);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(50, 26);
            this.button5.TabIndex = 4;
            this.button5.Text = "<";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button4_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(287, 90);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(50, 26);
            this.button4.TabIndex = 4;
            this.button4.Text = "> =";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(287, 122);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(50, 26);
            this.button6.TabIndex = 4;
            this.button6.Text = "< =";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // apply_button
            // 
            this.apply_button.Location = new System.Drawing.Point(196, 428);
            this.apply_button.Name = "apply_button";
            this.apply_button.Size = new System.Drawing.Size(68, 26);
            this.apply_button.TabIndex = 4;
            this.apply_button.Text = "应用";
            this.apply_button.UseVisualStyleBackColor = true;
            this.apply_button.Click += new System.EventHandler(this.apply_button_Click);
            // 
            // close_button
            // 
            this.close_button.Location = new System.Drawing.Point(270, 428);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(68, 26);
            this.close_button.TabIndex = 4;
            this.close_button.Text = "关闭";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 339);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "select * from layer where:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "选择属性";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(231, 160);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(50, 26);
            this.button7.TabIndex = 4;
            this.button7.Text = "and";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(231, 192);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(50, 26);
            this.button8.TabIndex = 4;
            this.button8.Text = "not";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(287, 160);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(50, 26);
            this.button9.TabIndex = 4;
            this.button9.Text = "or";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(287, 192);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(50, 26);
            this.button10.TabIndex = 4;
            this.button10.Text = "like";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // clear_button
            // 
            this.clear_button.Location = new System.Drawing.Point(261, 339);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(77, 22);
            this.clear_button.TabIndex = 4;
            this.clear_button.Text = "清除";
            this.clear_button.UseVisualStyleBackColor = true;
            this.clear_button.Click += new System.EventHandler(this.clear_button_Click);
            // 
            // uniqueValue_listBox
            // 
            this.uniqueValue_listBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.uniqueValue_listBox.FormattingEnabled = true;
            this.uniqueValue_listBox.ItemHeight = 12;
            this.uniqueValue_listBox.Location = new System.Drawing.Point(15, 228);
            this.uniqueValue_listBox.Name = "uniqueValue_listBox";
            this.uniqueValue_listBox.Size = new System.Drawing.Size(222, 100);
            this.uniqueValue_listBox.TabIndex = 5;
            this.uniqueValue_listBox.DoubleClick += new System.EventHandler(this.uniqueValue_DoubleClick);
            // 
            // UniqueValue_button
            // 
            this.UniqueValue_button.Location = new System.Drawing.Point(243, 228);
            this.UniqueValue_button.Name = "UniqueValue_button";
            this.UniqueValue_button.Size = new System.Drawing.Size(94, 23);
            this.UniqueValue_button.TabIndex = 4;
            this.UniqueValue_button.Text = "获取唯一值";
            this.UniqueValue_button.UseVisualStyleBackColor = true;
            this.UniqueValue_button.Click += new System.EventHandler(this.UniqueValue_button_Click);
            // 
            // frmQueryFeat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 465);
            this.Controls.Add(this.uniqueValue_listBox);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.close_button);
            this.Controls.Add(this.apply_button);
            this.Controls.Add(this.clear_button);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.UniqueValue_button);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.sql_textBox);
            this.Controls.Add(this.field_listBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.featCls_comboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQueryFeat";
            this.Text = "按属性查询";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox featCls_comboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox field_listBox;
        private System.Windows.Forms.TextBox sql_textBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button apply_button;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button clear_button;
        private System.Windows.Forms.ListBox uniqueValue_listBox;
        private System.Windows.Forms.Button UniqueValue_button;
    }
}