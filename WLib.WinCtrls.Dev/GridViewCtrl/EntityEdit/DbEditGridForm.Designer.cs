namespace WLib.WinCtrls.Dev.GridViewCtrl.EntityEdit
{
    partial class DbEditGridForm
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
            this.dbEditGridView1 = new WLib.WinCtrls.Dev.GridViewCtrl.DbEditGridView();
            this.SuspendLayout();
            // 
            // dbEditGridView1
            // 
            this.dbEditGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbEditGridView1.Location = new System.Drawing.Point(0, 0);
            this.dbEditGridView1.Margin = new System.Windows.Forms.Padding(6);
            this.dbEditGridView1.Name = "dbEditGridView1";
            this.dbEditGridView1.Size = new System.Drawing.Size(1460, 971);
            this.dbEditGridView1.TabIndex = 0;
            // 
            // DbEditGridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1460, 971);
            this.Controls.Add(this.dbEditGridView1);
            this.Name = "DbEditGridForm";
            this.Text = "数据编辑";
            this.ResumeLayout(false);

        }

        #endregion

        private DbEditGridView dbEditGridView1;
    }
}