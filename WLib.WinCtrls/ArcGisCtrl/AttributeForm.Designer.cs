namespace WLib.WinCtrls.ArcGisCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttributeForm));
            this.sBtnClose = new System.Windows.Forms.Button();
            this.attributeCtrl1 = new WLib.WinCtrls.ArcGisCtrl.AttributeCtrl();
            this.SuspendLayout();
            // 
            // sBtnClose
            // 
            this.sBtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnClose.Location = new System.Drawing.Point(819, 1);
            this.sBtnClose.Name = "sBtnClose";
            this.sBtnClose.Size = new System.Drawing.Size(82, 26);
            this.sBtnClose.TabIndex = 0;
            this.sBtnClose.Text = "关闭(&X)";
            this.sBtnClose.UseVisualStyleBackColor = true;
            this.sBtnClose.Click += new System.EventHandler(this.SBtnClose_Click);
            // 
            // attributeCtrl1
            // 
            this.attributeCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attributeCtrl1.Filter = null;
            this.attributeCtrl1.Location = new System.Drawing.Point(0, 0);
            this.attributeCtrl1.Name = "attributeCtrl1";
            this.attributeCtrl1.Size = new System.Drawing.Size(904, 479);
            this.attributeCtrl1.TabIndex = 1;
            // 
            // AttributeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 479);
            this.Controls.Add(this.attributeCtrl1);
            this.Controls.Add(this.sBtnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AttributeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "属性表";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button sBtnClose;
        private AttributeCtrl attributeCtrl1;
    }
}