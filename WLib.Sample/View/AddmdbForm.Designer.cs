namespace GISsys
{
    partial class frmAddmdb
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
            this.polygon_checkBox = new System.Windows.Forms.CheckBox();
            this.polyline_checkBox = new System.Windows.Forms.CheckBox();
            this.point_checkBox = new System.Windows.Forms.CheckBox();
            this.feature_treeView = new System.Windows.Forms.TreeView();
            this.OpenMDB_button = new System.Windows.Forms.Button();
            this.Cancel_button = new System.Windows.Forms.Button();
            this.OK_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // polygon_checkBox
            // 
            this.polygon_checkBox.AutoSize = true;
            this.polygon_checkBox.Location = new System.Drawing.Point(160, 21);
            this.polygon_checkBox.Name = "polygon_checkBox";
            this.polygon_checkBox.Size = new System.Drawing.Size(36, 16);
            this.polygon_checkBox.TabIndex = 15;
            this.polygon_checkBox.Text = "面";
            this.polygon_checkBox.UseVisualStyleBackColor = true;
            // 
            // polyline_checkBox
            // 
            this.polyline_checkBox.AutoSize = true;
            this.polyline_checkBox.Location = new System.Drawing.Point(85, 21);
            this.polyline_checkBox.Name = "polyline_checkBox";
            this.polyline_checkBox.Size = new System.Drawing.Size(36, 16);
            this.polyline_checkBox.TabIndex = 16;
            this.polyline_checkBox.Text = "线";
            this.polyline_checkBox.UseVisualStyleBackColor = true;
            // 
            // point_checkBox
            // 
            this.point_checkBox.AutoSize = true;
            this.point_checkBox.Location = new System.Drawing.Point(17, 21);
            this.point_checkBox.Name = "point_checkBox";
            this.point_checkBox.Size = new System.Drawing.Size(36, 16);
            this.point_checkBox.TabIndex = 17;
            this.point_checkBox.Text = "点";
            this.point_checkBox.UseVisualStyleBackColor = true;
            // 
            // feature_treeView
            // 
            this.feature_treeView.BackColor = System.Drawing.Color.Bisque;
            this.feature_treeView.CheckBoxes = true;
            this.feature_treeView.Location = new System.Drawing.Point(12, 48);
            this.feature_treeView.Name = "feature_treeView";
            this.feature_treeView.Size = new System.Drawing.Size(292, 332);
            this.feature_treeView.TabIndex = 13;
            // 
            // OpenMDB_button
            // 
            this.OpenMDB_button.Location = new System.Drawing.Point(215, 15);
            this.OpenMDB_button.Name = "OpenMDB_button";
            this.OpenMDB_button.Size = new System.Drawing.Size(89, 27);
            this.OpenMDB_button.TabIndex = 12;
            this.OpenMDB_button.Text = "打开";
            this.OpenMDB_button.UseVisualStyleBackColor = true;
            this.OpenMDB_button.Click += new System.EventHandler(this.OpenMDB_button_Click);
            // 
            // Cancel_button
            // 
            this.Cancel_button.Location = new System.Drawing.Point(223, 386);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(81, 29);
            this.Cancel_button.TabIndex = 19;
            this.Cancel_button.Text = "取消";
            this.Cancel_button.UseVisualStyleBackColor = true;
            this.Cancel_button.Click += new System.EventHandler(this.Cancel_button_Click);
            // 
            // OK_button
            // 
            this.OK_button.Location = new System.Drawing.Point(136, 386);
            this.OK_button.Name = "OK_button";
            this.OK_button.Size = new System.Drawing.Size(81, 29);
            this.OK_button.TabIndex = 18;
            this.OK_button.Text = "确定";
            this.OK_button.UseVisualStyleBackColor = true;
            // 
            // frmAddmdb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 428);
            this.Controls.Add(this.Cancel_button);
            this.Controls.Add(this.OK_button);
            this.Controls.Add(this.polygon_checkBox);
            this.Controls.Add(this.polyline_checkBox);
            this.Controls.Add(this.point_checkBox);
            this.Controls.Add(this.feature_treeView);
            this.Controls.Add(this.OpenMDB_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddmdb";
            this.Text = "加载个人数据库要素类";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox polygon_checkBox;
        private System.Windows.Forms.CheckBox polyline_checkBox;
        private System.Windows.Forms.CheckBox point_checkBox;
        private System.Windows.Forms.TreeView feature_treeView;
        private System.Windows.Forms.Button OpenMDB_button;
        private System.Windows.Forms.Button Cancel_button;
        private System.Windows.Forms.Button OK_button;
    }
}