namespace WLib.UserCtrls.PathCtrl
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbAll = new System.Windows.Forms.CheckBox();
            this.listViewLayers = new System.Windows.Forms.ListView();
            this.workspaceSelector1 = new WLib.UserCtrls.PathCtrl.WorkspaceSelector();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cbTables = new System.Windows.Forms.CheckBox();
            this.cbLayers = new System.Windows.Forms.CheckBox();
            this.cbPoint = new System.Windows.Forms.CheckBox();
            this.cbLine = new System.Windows.Forms.CheckBox();
            this.cbPolygon = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Location = new System.Drawing.Point(448, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(94, 39);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(548, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 39);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbAll
            // 
            this.cbAll.AutoSize = true;
            this.cbAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbAll.Location = new System.Drawing.Point(3, 3);
            this.cbAll.Name = "cbAll";
            this.cbAll.Size = new System.Drawing.Size(67, 39);
            this.cbAll.TabIndex = 4;
            this.cbAll.Text = "全选(&A)";
            this.cbAll.UseVisualStyleBackColor = true;
            this.cbAll.CheckedChanged += new System.EventHandler(this.cbAll_cckedChanged);
            // 
            // listViewLayers
            // 
            this.listViewLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewLayers.HideSelection = false;
            this.listViewLayers.Location = new System.Drawing.Point(3, 41);
            this.listViewLayers.Name = "listViewLayers";
            this.listViewLayers.Size = new System.Drawing.Size(639, 237);
            this.listViewLayers.TabIndex = 5;
            this.listViewLayers.UseCompatibleStateImageBehavior = false;
            this.listViewLayers.View = System.Windows.Forms.View.SmallIcon;
            this.listViewLayers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listViewLayers_ItemCheck);
            // 
            // workspaceSelector1
            // 
            this.workspaceSelector1.Description = "工作空间：";
            this.workspaceSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workspaceSelector1.Location = new System.Drawing.Point(3, 3);
            this.workspaceSelector1.Name = "workspaceSelector1";
            this.workspaceSelector1.OptEnable = true;
            this.workspaceSelector1.PathOrConnStr = "";
            this.workspaceSelector1.Size = new System.Drawing.Size(639, 32);
            this.workspaceSelector1.TabIndex = 3;
            this.workspaceSelector1.WorkspaceIndex = 0;
            this.workspaceSelector1.WorkspaceTypeFilter = "shp|gdb|mdb|sde";
            this.workspaceSelector1.AfterSelectPath += new System.EventHandler(this.workspaceSelector1_AfterSelectPath);
            this.workspaceSelector1.WorkspaceTypeChanged += new System.EventHandler(this.workspaceSelector1_WorkspaceTypeChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.listViewLayers, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.workspaceSelector1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(645, 326);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 10;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.Controls.Add(this.cbAll, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnOK, 8, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnCancel, 9, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbTables, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbLayers, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbPoint, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbLine, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbPolygon, 6, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 281);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(645, 45);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // cbTables
            // 
            this.cbTables.AutoSize = true;
            this.cbTables.Checked = true;
            this.cbTables.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbTables.Location = new System.Drawing.Point(134, 3);
            this.cbTables.Name = "cbTables";
            this.cbTables.Size = new System.Drawing.Size(52, 39);
            this.cbTables.TabIndex = 5;
            this.cbTables.Text = "表格";
            this.cbTables.UseVisualStyleBackColor = true;
            this.cbTables.CheckedChanged += new System.EventHandler(this.cbShowItems_CheckedChanged);
            // 
            // cbLayers
            // 
            this.cbLayers.AutoSize = true;
            this.cbLayers.Checked = true;
            this.cbLayers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbLayers.Location = new System.Drawing.Point(192, 3);
            this.cbLayers.Name = "cbLayers";
            this.cbLayers.Size = new System.Drawing.Size(60, 39);
            this.cbLayers.TabIndex = 6;
            this.cbLayers.Text = "图层 (";
            this.cbLayers.UseVisualStyleBackColor = true;
            this.cbLayers.CheckedChanged += new System.EventHandler(this.cbShowItems_CheckedChanged);
            // 
            // cbPoint
            // 
            this.cbPoint.AutoSize = true;
            this.cbPoint.Checked = true;
            this.cbPoint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPoint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbPoint.Location = new System.Drawing.Point(258, 3);
            this.cbPoint.Name = "cbPoint";
            this.cbPoint.Size = new System.Drawing.Size(32, 39);
            this.cbPoint.TabIndex = 7;
            this.cbPoint.Text = "点";
            this.cbPoint.UseVisualStyleBackColor = true;
            this.cbPoint.CheckedChanged += new System.EventHandler(this.cbShowItems_CheckedChanged);
            // 
            // cbLine
            // 
            this.cbLine.AutoSize = true;
            this.cbLine.Checked = true;
            this.cbLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbLine.Location = new System.Drawing.Point(296, 3);
            this.cbLine.Name = "cbLine";
            this.cbLine.Size = new System.Drawing.Size(33, 39);
            this.cbLine.TabIndex = 8;
            this.cbLine.Text = "线";
            this.cbLine.UseVisualStyleBackColor = true;
            this.cbLine.CheckedChanged += new System.EventHandler(this.cbShowItems_CheckedChanged);
            // 
            // cbPolygon
            // 
            this.cbPolygon.AutoSize = true;
            this.cbPolygon.Checked = true;
            this.cbPolygon.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPolygon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbPolygon.Location = new System.Drawing.Point(335, 3);
            this.cbPolygon.Name = "cbPolygon";
            this.cbPolygon.Size = new System.Drawing.Size(49, 39);
            this.cbPolygon.TabIndex = 9;
            this.cbPolygon.Text = "面 )";
            this.cbPolygon.UseVisualStyleBackColor = true;
            this.cbPolygon.CheckedChanged += new System.EventHandler(this.cbShowItems_CheckedChanged);
            // 
            // DataSelectorForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(667, 345);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataSelectorForm";
            this.Text = "添加数据";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private WorkspaceSelector workspaceSelector1;
        private System.Windows.Forms.CheckBox cbAll;
        private System.Windows.Forms.ListView listViewLayers;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox cbTables;
        private System.Windows.Forms.CheckBox cbLayers;
        private System.Windows.Forms.CheckBox cbPoint;
        private System.Windows.Forms.CheckBox cbLine;
        private System.Windows.Forms.CheckBox cbPolygon;
    }
}