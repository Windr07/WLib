namespace WLib.UserCtrls.Dev.ArcGisControl
{
    partial class QueryDevForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryDevForm));
            this.listBoxCtrlFields = new DevExpress.XtraEditors.ListBoxControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.listBoxCtrlValues = new DevExpress.XtraEditors.ListBoxControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtWhereClause = new DevExpress.XtraEditors.MemoEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton6 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton7 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton8 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton9 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton10 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton11 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton12 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton13 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton14 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtSearchFields = new DevExpress.XtraEditors.TextEdit();
            this.txtSearchValues = new DevExpress.XtraEditors.TextEdit();
            this.sBtnGetUniqueValue = new DevExpress.XtraEditors.SimpleButton();
            this.sBtnClose = new DevExpress.XtraEditors.SimpleButton();
            this.sBtnApply = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.sBtnClear = new DevExpress.XtraEditors.SimpleButton();
            this.cMenuStripFields = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.获取唯一值CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.等于EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxCtrlFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxCtrlValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWhereClause.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchFields.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchValues.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.cMenuStripFields.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxCtrlFields
            // 
            this.listBoxCtrlFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxCtrlFields.Location = new System.Drawing.Point(3, 25);
            this.listBoxCtrlFields.Name = "listBoxCtrlFields";
            this.listBoxCtrlFields.Size = new System.Drawing.Size(130, 215);
            this.listBoxCtrlFields.TabIndex = 0;
            this.listBoxCtrlFields.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxCtrlFields_MouseDoubleClick);
            this.listBoxCtrlFields.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxCtrlFields_MouseUp);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(3, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "字段：";
            // 
            // listBoxCtrlValues
            // 
            this.listBoxCtrlValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxCtrlValues.Location = new System.Drawing.Point(3, 24);
            this.listBoxCtrlValues.Name = "listBoxCtrlValues";
            this.listBoxCtrlValues.Size = new System.Drawing.Size(130, 216);
            this.listBoxCtrlValues.TabIndex = 0;
            this.listBoxCtrlValues.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxCtrlValues_MouseDoubleClick);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(3, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "唯一值：";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(1, 26);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(53, 26);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Tag = ">";
            this.simpleButton1.Text = ">";
            this.simpleButton1.Click += new System.EventHandler(this.symbolButton_Click);
            // 
            // txtWhereClause
            // 
            this.txtWhereClause.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWhereClause.Location = new System.Drawing.Point(7, 300);
            this.txtWhereClause.Name = "txtWhereClause";
            this.txtWhereClause.Size = new System.Drawing.Size(389, 89);
            this.txtWhereClause.TabIndex = 3;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.Location = new System.Drawing.Point(58, 26);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(53, 26);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Tag = "<";
            this.simpleButton2.Text = "<";
            this.simpleButton2.Click += new System.EventHandler(this.symbolButton_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(1, 56);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(53, 26);
            this.simpleButton3.TabIndex = 2;
            this.simpleButton3.Tag = "=";
            this.simpleButton3.Text = "=";
            this.simpleButton3.Click += new System.EventHandler(this.symbolButton_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton4.Location = new System.Drawing.Point(58, 56);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(53, 26);
            this.simpleButton4.TabIndex = 2;
            this.simpleButton4.Tag = "<>";
            this.simpleButton4.Text = "<>";
            this.simpleButton4.Click += new System.EventHandler(this.symbolButton_Click);
            // 
            // simpleButton5
            // 
            this.simpleButton5.Location = new System.Drawing.Point(1, 86);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(53, 26);
            this.simpleButton5.TabIndex = 2;
            this.simpleButton5.Tag = ">=";
            this.simpleButton5.Text = ">=";
            this.simpleButton5.Click += new System.EventHandler(this.symbolButton_Click);
            // 
            // simpleButton6
            // 
            this.simpleButton6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton6.Location = new System.Drawing.Point(58, 86);
            this.simpleButton6.Name = "simpleButton6";
            this.simpleButton6.Size = new System.Drawing.Size(53, 26);
            this.simpleButton6.TabIndex = 2;
            this.simpleButton6.Tag = "<=";
            this.simpleButton6.Text = "<=";
            this.simpleButton6.Click += new System.EventHandler(this.symbolButton_Click);
            // 
            // simpleButton7
            // 
            this.simpleButton7.Location = new System.Drawing.Point(1, 116);
            this.simpleButton7.Name = "simpleButton7";
            this.simpleButton7.Size = new System.Drawing.Size(53, 26);
            this.simpleButton7.TabIndex = 2;
            this.simpleButton7.Tag = "?";
            this.simpleButton7.Text = "?";
            this.simpleButton7.Click += new System.EventHandler(this.symbolButton_Click);
            // 
            // simpleButton8
            // 
            this.simpleButton8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton8.Location = new System.Drawing.Point(58, 116);
            this.simpleButton8.Name = "simpleButton8";
            this.simpleButton8.Size = new System.Drawing.Size(53, 26);
            this.simpleButton8.TabIndex = 2;
            this.simpleButton8.Tag = "*";
            this.simpleButton8.Text = "*";
            this.simpleButton8.Click += new System.EventHandler(this.symbolButton_Click);
            // 
            // simpleButton9
            // 
            this.simpleButton9.Location = new System.Drawing.Point(1, 146);
            this.simpleButton9.Name = "simpleButton9";
            this.simpleButton9.Size = new System.Drawing.Size(53, 26);
            this.simpleButton9.TabIndex = 2;
            this.simpleButton9.Tag = "Like";
            this.simpleButton9.Text = "Like(&K)";
            this.simpleButton9.Click += new System.EventHandler(this.symbolButton_Click);
            // 
            // simpleButton10
            // 
            this.simpleButton10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton10.Location = new System.Drawing.Point(58, 146);
            this.simpleButton10.Name = "simpleButton10";
            this.simpleButton10.Size = new System.Drawing.Size(53, 26);
            this.simpleButton10.TabIndex = 2;
            this.simpleButton10.Tag = "And";
            this.simpleButton10.Text = "And(&N)";
            this.simpleButton10.Click += new System.EventHandler(this.symbolButton_Click);
            // 
            // simpleButton11
            // 
            this.simpleButton11.Location = new System.Drawing.Point(1, 176);
            this.simpleButton11.Name = "simpleButton11";
            this.simpleButton11.Size = new System.Drawing.Size(53, 26);
            this.simpleButton11.TabIndex = 2;
            this.simpleButton11.Tag = "Or";
            this.simpleButton11.Text = "Or(&O)";
            this.simpleButton11.Click += new System.EventHandler(this.symbolButton_Click);
            // 
            // simpleButton12
            // 
            this.simpleButton12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton12.Location = new System.Drawing.Point(58, 176);
            this.simpleButton12.Name = "simpleButton12";
            this.simpleButton12.Size = new System.Drawing.Size(53, 26);
            this.simpleButton12.TabIndex = 2;
            this.simpleButton12.Tag = "Not";
            this.simpleButton12.Text = "Not(&T)";
            this.simpleButton12.Click += new System.EventHandler(this.symbolButton_Click);
            // 
            // simpleButton13
            // 
            this.simpleButton13.Location = new System.Drawing.Point(1, 206);
            this.simpleButton13.Name = "simpleButton13";
            this.simpleButton13.Size = new System.Drawing.Size(53, 26);
            this.simpleButton13.TabIndex = 2;
            this.simpleButton13.Tag = "Is";
            this.simpleButton13.Text = "Is(&I)";
            this.simpleButton13.Click += new System.EventHandler(this.symbolButton_Click);
            // 
            // simpleButton14
            // 
            this.simpleButton14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton14.Location = new System.Drawing.Point(58, 206);
            this.simpleButton14.Name = "simpleButton14";
            this.simpleButton14.Size = new System.Drawing.Size(53, 26);
            this.simpleButton14.TabIndex = 2;
            this.simpleButton14.Tag = "= \'\'";
            this.simpleButton14.Text = "= \'\'";
            this.simpleButton14.Click += new System.EventHandler(this.symbolButton_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(7, 281);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(146, 14);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "select * from table where:";
            // 
            // txtSearchFields
            // 
            this.txtSearchFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchFields.Location = new System.Drawing.Point(3, 246);
            this.txtSearchFields.Name = "txtSearchFields";
            this.txtSearchFields.Size = new System.Drawing.Size(130, 21);
            this.txtSearchFields.TabIndex = 4;
            this.txtSearchFields.EditValueChanged += new System.EventHandler(this.txtSearchFields_EditValueChanged);
            // 
            // txtSearchValues
            // 
            this.txtSearchValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchValues.Location = new System.Drawing.Point(3, 246);
            this.txtSearchValues.Name = "txtSearchValues";
            this.txtSearchValues.Size = new System.Drawing.Size(130, 21);
            this.txtSearchValues.TabIndex = 4;
            this.txtSearchValues.EditValueChanged += new System.EventHandler(this.txtSearchValues_EditValueChanged);
            // 
            // sBtnGetUniqueValue
            // 
            this.sBtnGetUniqueValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnGetUniqueValue.Location = new System.Drawing.Point(0, 236);
            this.sBtnGetUniqueValue.Name = "sBtnGetUniqueValue";
            this.sBtnGetUniqueValue.Size = new System.Drawing.Size(112, 31);
            this.sBtnGetUniqueValue.TabIndex = 2;
            this.sBtnGetUniqueValue.Text = "获取唯一值(&C)";
            this.sBtnGetUniqueValue.Click += new System.EventHandler(this.sBtnGetUniqueValue_Click);
            // 
            // sBtnClose
            // 
            this.sBtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sBtnClose.Location = new System.Drawing.Point(312, 395);
            this.sBtnClose.Name = "sBtnClose";
            this.sBtnClose.Size = new System.Drawing.Size(84, 32);
            this.sBtnClose.TabIndex = 5;
            this.sBtnClose.Text = "关闭(&X)";
            this.sBtnClose.Click += new System.EventHandler(this.sBtnClose_Click);
            // 
            // sBtnApply
            // 
            this.sBtnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnApply.Location = new System.Drawing.Point(222, 395);
            this.sBtnApply.Name = "sBtnApply";
            this.sBtnApply.Size = new System.Drawing.Size(84, 32);
            this.sBtnApply.TabIndex = 5;
            this.sBtnApply.Text = "应用(&A)";
            this.sBtnApply.Click += new System.EventHandler(this.sBtnApply_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelControl1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 275F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 275F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 275F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 275F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 275F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(402, 275);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.listBoxCtrlFields, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelControl1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtSearchFields, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 221F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(136, 269);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.simpleButton2);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.simpleButton3);
            this.panelControl1.Controls.Add(this.simpleButton4);
            this.panelControl1.Controls.Add(this.simpleButton14);
            this.panelControl1.Controls.Add(this.simpleButton5);
            this.panelControl1.Controls.Add(this.sBtnGetUniqueValue);
            this.panelControl1.Controls.Add(this.simpleButton6);
            this.panelControl1.Controls.Add(this.simpleButton13);
            this.panelControl1.Controls.Add(this.simpleButton7);
            this.panelControl1.Controls.Add(this.simpleButton12);
            this.panelControl1.Controls.Add(this.simpleButton8);
            this.panelControl1.Controls.Add(this.simpleButton11);
            this.panelControl1.Controls.Add(this.simpleButton9);
            this.panelControl1.Controls.Add(this.simpleButton10);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(145, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(112, 269);
            this.panelControl1.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.labelControl2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.listBoxCtrlValues, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtSearchValues, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(263, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 222F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(136, 269);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // sBtnClear
            // 
            this.sBtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnClear.Location = new System.Drawing.Point(312, 278);
            this.sBtnClear.Name = "sBtnClear";
            this.sBtnClear.Size = new System.Drawing.Size(84, 21);
            this.sBtnClear.TabIndex = 2;
            this.sBtnClear.Text = "清空(&D)";
            this.sBtnClear.Click += new System.EventHandler(this.sBtnClear_Click);
            // 
            // cMenuStripFields
            // 
            this.cMenuStripFields.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.获取唯一值CToolStripMenuItem,
            this.等于EToolStripMenuItem,
            this.清空DToolStripMenuItem});
            this.cMenuStripFields.Name = "cMenuStripFields";
            this.cMenuStripFields.Size = new System.Drawing.Size(153, 70);
            // 
            // 获取唯一值CToolStripMenuItem
            // 
            this.获取唯一值CToolStripMenuItem.Name = "获取唯一值CToolStripMenuItem";
            this.获取唯一值CToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.获取唯一值CToolStripMenuItem.Text = "获取唯一值(&C)";
            this.获取唯一值CToolStripMenuItem.Click += new System.EventHandler(this.sBtnGetUniqueValue_Click);
            // 
            // 等于EToolStripMenuItem
            // 
            this.等于EToolStripMenuItem.Name = "等于EToolStripMenuItem";
            this.等于EToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.等于EToolStripMenuItem.Text = "等于(&E)";
            this.等于EToolStripMenuItem.Click += new System.EventHandler(this.等于EToolStripMenuItem_Click);
            // 
            // 清空DToolStripMenuItem
            // 
            this.清空DToolStripMenuItem.Name = "清空DToolStripMenuItem";
            this.清空DToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.清空DToolStripMenuItem.Text = "清空(&D)";
            this.清空DToolStripMenuItem.Click += new System.EventHandler(this.sBtnClear_Click);
            // 
            // QueryDevForm
            // 
            this.AcceptButton = this.sBtnApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.sBtnClose;
            this.ClientSize = new System.Drawing.Size(402, 433);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.sBtnApply);
            this.Controls.Add(this.sBtnClose);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtWhereClause);
            this.Controls.Add(this.sBtnClear);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QueryDevForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "按属性查询";
            this.Load += new System.EventHandler(this.QueryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listBoxCtrlFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxCtrlValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWhereClause.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchFields.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchValues.Properties)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.cMenuStripFields.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl listBoxCtrlFields;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ListBoxControl listBoxCtrlValues;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.MemoEdit txtWhereClause;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private DevExpress.XtraEditors.SimpleButton simpleButton6;
        private DevExpress.XtraEditors.SimpleButton simpleButton7;
        private DevExpress.XtraEditors.SimpleButton simpleButton8;
        private DevExpress.XtraEditors.SimpleButton simpleButton9;
        private DevExpress.XtraEditors.SimpleButton simpleButton10;
        private DevExpress.XtraEditors.SimpleButton simpleButton11;
        private DevExpress.XtraEditors.SimpleButton simpleButton12;
        private DevExpress.XtraEditors.SimpleButton simpleButton13;
        private DevExpress.XtraEditors.SimpleButton simpleButton14;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtSearchFields;
        private DevExpress.XtraEditors.TextEdit txtSearchValues;
        private DevExpress.XtraEditors.SimpleButton sBtnGetUniqueValue;
        private DevExpress.XtraEditors.SimpleButton sBtnClose;
        private DevExpress.XtraEditors.SimpleButton sBtnApply;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private DevExpress.XtraEditors.SimpleButton sBtnClear;
        private System.Windows.Forms.ContextMenuStrip cMenuStripFields;
        private System.Windows.Forms.ToolStripMenuItem 获取唯一值CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 等于EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空DToolStripMenuItem;

    }
}