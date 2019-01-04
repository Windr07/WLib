namespace WLib.UserCtrls.PathControl
{
    partial class DataSelectorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataSelectorForm));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbAll = new System.Windows.Forms.CheckBox();
            this.listViewLayers = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.workspaceSelector1 = new WorkspaceSelector();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(515, 291);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 38);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(609, 291);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 38);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbAll
            // 
            this.cbAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAll.AutoSize = true;
            this.cbAll.Location = new System.Drawing.Point(13, 298);
            this.cbAll.Name = "cbAll";
            this.cbAll.Size = new System.Drawing.Size(66, 16);
            this.cbAll.TabIndex = 4;
            this.cbAll.Text = "全选(&A)";
            this.cbAll.UseVisualStyleBackColor = true;
            this.cbAll.CheckedChanged += new System.EventHandler(this.cbAll_cckedChanged);
            // 
            // listViewLayers
            // 
            this.listViewLayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewLayers.HideSelection = false;
            this.listViewLayers.LargeImageList = this.imageList1;
            this.listViewLayers.Location = new System.Drawing.Point(13, 50);
            this.listViewLayers.Name = "listViewLayers";
            this.listViewLayers.Size = new System.Drawing.Size(683, 235);
            this.listViewLayers.SmallImageList = this.imageList1;
            this.listViewLayers.TabIndex = 5;
            this.listViewLayers.UseCompatibleStateImageBehavior = false;
            this.listViewLayers.View = System.Windows.Forms.View.SmallIcon;
            this.listViewLayers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listViewLayers_ItemCheck);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "shape.png");
            this.imageList1.Images.SetKeyName(1, "table3.png");
            // 
            // workspaceSelector1
            // 
            this.workspaceSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.workspaceSelector1.Description = "工作空间：";
            this.workspaceSelector1.Location = new System.Drawing.Point(13, 13);
            this.workspaceSelector1.Name = "workspaceSelector1";
            this.workspaceSelector1.OptEnable = true;
            this.workspaceSelector1.PathOrConnStr = "";
            this.workspaceSelector1.Size = new System.Drawing.Size(684, 31);
            this.workspaceSelector1.TabIndex = 3;
            this.workspaceSelector1.WorkspaceIndex = 1;
            this.workspaceSelector1.WorkspaceTypeFilter = "shp|gdb|mdb|sde";
            this.workspaceSelector1.AfterSelectPath += new System.EventHandler(this.workspaceSelector1_AfterSelectPath);
            this.workspaceSelector1.WorkspaceTypeChanged += new System.EventHandler(this.workspaceSelector1_WorkspaceTypeChanged);
            // 
            // DataSelectorForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(708, 337);
            this.Controls.Add(this.listViewLayers);
            this.Controls.Add(this.cbAll);
            this.Controls.Add(this.workspaceSelector1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataSelectorForm";
            this.Text = "添加数据";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private WorkspaceSelector workspaceSelector1;
        private System.Windows.Forms.CheckBox cbAll;
        private System.Windows.Forms.ListView listViewLayers;
        private System.Windows.Forms.ImageList imageList1;

    }
}