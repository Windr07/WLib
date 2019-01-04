namespace WLib.UserCtrls.ExplorerControl
{
    partial class ExplorerTreeView
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
            this.treeWnd = new ExplorerTreeViewWnd();
            this.SuspendLayout();
            // 
            // treeWnd
            // 
            this.treeWnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeWnd.Location = new System.Drawing.Point(0, 0);
            this.treeWnd.Name = "treeWnd";
            this.treeWnd.Size = new System.Drawing.Size(289, 419);
            this.treeWnd.TabIndex = 0;
            // 
            // ExplorerTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeWnd);
            this.Name = "ExplorerTreeView";
            this.Size = new System.Drawing.Size(289, 419);
            this.ResumeLayout(false);

        }

        #endregion

        private ExplorerTreeViewWnd treeWnd;


    }
}
