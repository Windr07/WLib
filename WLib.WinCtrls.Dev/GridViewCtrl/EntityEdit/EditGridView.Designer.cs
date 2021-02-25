namespace WLib.WinCtrls.Dev.GridViewCtrl
{
    partial class EditGridView
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
            this.sBtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.sBtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.sBtnReset = new DevExpress.XtraEditors.SimpleButton();
            this.sBtnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // sBtnSave
            // 
            this.sBtnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sBtnSave.Location = new System.Drawing.Point(593, 3);
            this.sBtnSave.Name = "sBtnSave";
            this.sBtnSave.Size = new System.Drawing.Size(94, 35);
            this.sBtnSave.TabIndex = 0;
            this.sBtnSave.Text = "保存(&S)";
            this.sBtnSave.Click += new System.EventHandler(this.SBtnSave_Click);
            // 
            // sBtnDelete
            // 
            this.sBtnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sBtnDelete.Location = new System.Drawing.Point(493, 3);
            this.sBtnDelete.Name = "sBtnDelete";
            this.sBtnDelete.Size = new System.Drawing.Size(94, 35);
            this.sBtnDelete.TabIndex = 0;
            this.sBtnDelete.Text = "删除(&D)";
            this.sBtnDelete.Click += new System.EventHandler(this.SBtnDelete_Click);
            // 
            // sBtnReset
            // 
            this.sBtnReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sBtnReset.Location = new System.Drawing.Point(3, 3);
            this.sBtnReset.Name = "sBtnReset";
            this.sBtnReset.Size = new System.Drawing.Size(94, 35);
            this.sBtnReset.TabIndex = 0;
            this.sBtnReset.Text = "重置(&R)";
            this.sBtnReset.Click += new System.EventHandler(this.SBtnReset_Click);
            // 
            // sBtnAdd
            // 
            this.sBtnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sBtnAdd.Location = new System.Drawing.Point(393, 3);
            this.sBtnAdd.Name = "sBtnAdd";
            this.sBtnAdd.Size = new System.Drawing.Size(94, 35);
            this.sBtnAdd.TabIndex = 0;
            this.sBtnAdd.Text = "添加(&A)";
            this.sBtnAdd.Click += new System.EventHandler(this.SBtnAdd_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.sBtnSave, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.sBtnReset, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.sBtnDelete, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.sBtnAdd, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 409);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(690, 41);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(690, 409);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // ParamsTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 450);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ParamsTableForm";
            this.Text = "评价指数配置";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton sBtnSave;
        private DevExpress.XtraEditors.SimpleButton sBtnDelete;
        private DevExpress.XtraEditors.SimpleButton sBtnAdd;
        private DevExpress.XtraEditors.SimpleButton sBtnReset;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}