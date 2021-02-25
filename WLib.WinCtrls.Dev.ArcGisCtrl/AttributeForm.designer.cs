namespace WLib.WinCtrls.Dev.ArcGisCtrl
{
    partial class AttributeForm
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
            this.attributeCtrl1 = new WLib.WinCtrls.Dev.ArcGisCtrl.AttributeCtrl();
            this.SuspendLayout();
            // 
            // attributeCtrl1
            // 
            this.attributeCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attributeCtrl1.Filter = "";
            this.attributeCtrl1.Location = new System.Drawing.Point(0, 0);
            this.attributeCtrl1.Name = "attributeCtrl1";
            this.attributeCtrl1.Size = new System.Drawing.Size(918, 539);
            this.attributeCtrl1.TabIndex = 0;
            // 
            // AttributeExForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 539);
            this.Controls.Add(this.attributeCtrl1);
            this.Name = "AttributeExForm";
            this.Text = "属性表";
            this.ResumeLayout(false);

        }

        #endregion

        private AttributeCtrl attributeCtrl1;
    }
}