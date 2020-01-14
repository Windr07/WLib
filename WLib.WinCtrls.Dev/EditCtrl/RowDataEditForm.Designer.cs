namespace WLib.WinCtrls.Dev.EditCtrl
{
    partial class RowDataEditForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelTips = new DevExpress.XtraEditors.PanelControl();
            this.lblTips = new System.Windows.Forms.Label();
            this.gridRowData = new System.Windows.Forms.DataGridView();
            this.colFieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldVaule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelTips)).BeginInit();
            this.panelTips.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRowData)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(313, 396);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 353);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(307, 40);
            this.panel1.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCancel.Location = new System.Drawing.Point(160, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 33);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSave.Location = new System.Drawing.Point(63, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(93, 33);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.dateTimePicker1);
            this.panelControl1.Controls.Add(this.panelTips);
            this.panelControl1.Controls.Add(this.gridRowData);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(3, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(307, 344);
            this.panelControl1.TabIndex = 2;
            // 
            // panelTips
            // 
            this.panelTips.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTips.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelTips.Appearance.Options.UseBackColor = true;
            this.panelTips.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelTips.Controls.Add(this.lblTips);
            this.panelTips.Location = new System.Drawing.Point(3, 315);
            this.panelTips.Name = "panelTips";
            this.panelTips.Size = new System.Drawing.Size(301, 26);
            this.panelTips.TabIndex = 3;
            this.panelTips.Visible = false;
            // 
            // lblTips
            // 
            this.lblTips.AutoSize = true;
            this.lblTips.Location = new System.Drawing.Point(3, 6);
            this.lblTips.Name = "lblTips";
            this.lblTips.Size = new System.Drawing.Size(67, 14);
            this.lblTips.TabIndex = 0;
            this.lblTips.Text = "保存成功！";
            // 
            // gridRowData
            // 
            this.gridRowData.AllowUserToAddRows = false;
            this.gridRowData.AllowUserToDeleteRows = false;
            this.gridRowData.AllowUserToResizeRows = false;
            this.gridRowData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRowData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFieldName,
            this.colFieldVaule});
            this.gridRowData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRowData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gridRowData.Location = new System.Drawing.Point(2, 2);
            this.gridRowData.MultiSelect = false;
            this.gridRowData.Name = "gridRowData";
            this.gridRowData.RowHeadersVisible = false;
            this.gridRowData.RowTemplate.Height = 23;
            this.gridRowData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridRowData.Size = new System.Drawing.Size(303, 340);
            this.gridRowData.TabIndex = 2;
            this.gridRowData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridRowData_CellClick);
            this.gridRowData.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridRowData_CellLeave);
            this.gridRowData.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.gridRowData_CellValidating);
            // 
            // colFieldName
            // 
            this.colFieldName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.colFieldName.DefaultCellStyle = dataGridViewCellStyle9;
            this.colFieldName.HeaderText = "属性名";
            this.colFieldName.Name = "colFieldName";
            this.colFieldName.ReadOnly = true;
            this.colFieldName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colFieldVaule
            // 
            this.colFieldVaule.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFieldVaule.HeaderText = "属性值";
            this.colFieldVaule.Name = "colFieldVaule";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(153, 25);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(151, 22);
            this.dateTimePicker1.TabIndex = 4;
            this.dateTimePicker1.Visible = false;
            this.dateTimePicker1.Leave += new System.EventHandler(this.dateTimePicker1_Leave);
            // 
            // RowDataEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 396);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RowDataEditForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "属性编辑";
            this.Load += new System.EventHandler(this.RowDataEditForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelTips)).EndInit();
            this.panelTips.ResumeLayout(false);
            this.panelTips.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRowData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.DataGridView gridRowData;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldVaule;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelTips;
        private System.Windows.Forms.Label lblTips;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}