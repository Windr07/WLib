using DevExpress.XtraEditors;

namespace WLib.WinCtrls.Dev.PathCtrl
{
    partial class LabelPathBoxControl
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
            this.TitleLabel = new DevExpress.XtraEditors.LabelControl();
            this.PathBox = new WLib.WinCtrls.Dev.PathCtrl.PathBoxControl();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TitleLabel.Location = new System.Drawing.Point(0, 0);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Padding = new System.Windows.Forms.Padding(3);
            this.TitleLabel.Size = new System.Drawing.Size(37, 20);
            this.TitleLabel.TabIndex = 2;
            this.TitleLabel.Text = "路径1";
            // 
            // PathBox
            // 
            this.PathBox.DefaultTips = "粘贴路径于此并按下回车，或点击选择按钮以选择路径";
            this.PathBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PathBox.FileFilter = "全部文件(*.*)|*.*";
            this.PathBox.Location = new System.Drawing.Point(0, 20);
            this.PathBox.MaximumSize = new System.Drawing.Size(7000, 23);
            this.PathBox.MultiSelect = true;
            this.PathBox.Name = "PathBox";
            this.PathBox.OptEnable = true;
            this.PathBox.Path = "粘贴路径于此并按下回车，或点击选择按钮以选择路径";
            this.PathBox.Paths = new string[0];
            this.PathBox.ReadOnly = false;
            this.PathBox.SelectPathType = WLib.WinCtrls.Extension.ESelectPathType.Folder;
            this.PathBox.SelectTips = null;
            this.PathBox.ShowButtonOption = WLib.WinCtrls.PathCtrl.EShowButtonOption.ViewSelect;
            this.PathBox.Size = new System.Drawing.Size(454, 23);
            this.PathBox.TabIndex = 0;
            // 
            // LabelPathBoxControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PathBox);
            this.Controls.Add(this.TitleLabel);
            this.MaximumSize = new System.Drawing.Size(3000, 47);
            this.MinimumSize = new System.Drawing.Size(10, 47);
            this.Name = "LabelPathBoxControl";
            this.Size = new System.Drawing.Size(454, 47);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public LabelControl TitleLabel;
        public PathBoxControl PathBox;
    }
}
