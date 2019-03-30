namespace WLib.UserCtrls.ArcGisCtrl
{
    partial class AttributeQueryForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtSearchFields = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxFields = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.txtSearchValues = new System.Windows.Forms.TextBox();
            this.listBoxValues = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button14 = new System.Windows.Forms.Button();
            this.sBtnGetUniqueValue = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.sBtnClear = new System.Windows.Forms.Button();
            this.txtWhereClause = new System.Windows.Forms.TextBox();
            this.sBtnApply = new System.Windows.Forms.Button();
            this.sBtnClose = new System.Windows.Forms.Button();
            this.cMenuStripFields = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.获取唯一值CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.等于EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTables = new System.Windows.Forms.Label();
            this.cmbTables = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.cMenuStripFields.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 26);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 275F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 275F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 275F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 275F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 275F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(402, 275);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.txtSearchFields, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.listBoxFields, 0, 1);
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
            // txtSearchFields
            // 
            this.txtSearchFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchFields.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtSearchFields.Location = new System.Drawing.Point(3, 246);
            this.txtSearchFields.Name = "txtSearchFields";
            this.txtSearchFields.Size = new System.Drawing.Size(130, 21);
            this.txtSearchFields.TabIndex = 10;
            this.txtSearchFields.TextChanged += new System.EventHandler(this.txtSearchFields_EditValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "字段：";
            // 
            // listBoxFields
            // 
            this.listBoxFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxFields.FormattingEnabled = true;
            this.listBoxFields.ItemHeight = 12;
            this.listBoxFields.Location = new System.Drawing.Point(3, 25);
            this.listBoxFields.Name = "listBoxFields";
            this.listBoxFields.Size = new System.Drawing.Size(130, 215);
            this.listBoxFields.TabIndex = 1;
            this.listBoxFields.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxFields_MouseDoubleClick);
            this.listBoxFields.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxFields_MouseUp);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.txtSearchValues, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.listBoxValues, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
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
            // txtSearchValues
            // 
            this.txtSearchValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchValues.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtSearchValues.Location = new System.Drawing.Point(3, 246);
            this.txtSearchValues.Name = "txtSearchValues";
            this.txtSearchValues.Size = new System.Drawing.Size(130, 21);
            this.txtSearchValues.TabIndex = 11;
            this.txtSearchValues.TextChanged += new System.EventHandler(this.txtSearchValues_EditValueChanged);
            // 
            // listBoxValues
            // 
            this.listBoxValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxValues.FormattingEnabled = true;
            this.listBoxValues.ItemHeight = 12;
            this.listBoxValues.Location = new System.Drawing.Point(3, 24);
            this.listBoxValues.Name = "listBoxValues";
            this.listBoxValues.Size = new System.Drawing.Size(130, 216);
            this.listBoxValues.TabIndex = 2;
            this.listBoxValues.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxValues_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "唯一值：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button14);
            this.panel1.Controls.Add(this.sBtnGetUniqueValue);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button13);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button16);
            this.panel1.Controls.Add(this.button12);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.button11);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button15);
            this.panel1.Controls.Add(this.button10);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(145, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(112, 269);
            this.panel1.TabIndex = 3;
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(58, 146);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(53, 26);
            this.button14.TabIndex = 9;
            this.button14.Tag = "And";
            this.button14.Text = "And(&N)";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.symbolControl_Click);
            // 
            // sBtnGetUniqueValue
            // 
            this.sBtnGetUniqueValue.Location = new System.Drawing.Point(0, 236);
            this.sBtnGetUniqueValue.Name = "sBtnGetUniqueValue";
            this.sBtnGetUniqueValue.Size = new System.Drawing.Size(112, 31);
            this.sBtnGetUniqueValue.TabIndex = 11;
            this.sBtnGetUniqueValue.Tag = " ";
            this.sBtnGetUniqueValue.Text = "获取唯一值(&C)";
            this.sBtnGetUniqueValue.UseVisualStyleBackColor = true;
            this.sBtnGetUniqueValue.Click += new System.EventHandler(this.sBtnGetUniqueValue_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(58, 56);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(53, 26);
            this.button6.TabIndex = 9;
            this.button6.Tag = "<>";
            this.button6.Text = "<>";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.symbolControl_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(1, 146);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(53, 26);
            this.button13.TabIndex = 9;
            this.button13.Tag = "Like";
            this.button13.Text = "Like(&K)";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.symbolControl_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1, 56);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(53, 26);
            this.button5.TabIndex = 9;
            this.button5.Tag = "=";
            this.button5.Text = "=";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.symbolControl_Click);
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(58, 206);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(53, 26);
            this.button16.TabIndex = 9;
            this.button16.Tag = "= \'\'";
            this.button16.Text = "= \'\'";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.symbolControl_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(58, 176);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(53, 26);
            this.button12.TabIndex = 9;
            this.button12.Tag = "Not";
            this.button12.Text = "Not(&T)";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.symbolControl_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(58, 86);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(53, 26);
            this.button8.TabIndex = 9;
            this.button8.Tag = "<=";
            this.button8.Text = "<=";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.symbolControl_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(58, 116);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(53, 26);
            this.button11.TabIndex = 9;
            this.button11.Tag = "*";
            this.button11.Text = "*";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.symbolControl_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(58, 26);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(53, 26);
            this.button4.TabIndex = 9;
            this.button4.Tag = "<";
            this.button4.Text = "<";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.symbolControl_Click);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(1, 206);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(53, 26);
            this.button15.TabIndex = 9;
            this.button15.Tag = "Is";
            this.button15.Text = "Is(&I)";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.symbolControl_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(1, 176);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(53, 26);
            this.button10.TabIndex = 9;
            this.button10.Tag = "Or";
            this.button10.Text = "Or(&O)";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.symbolControl_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(1, 86);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(53, 26);
            this.button7.TabIndex = 9;
            this.button7.Tag = ">=";
            this.button7.Text = ">=";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.symbolControl_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(1, 116);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(53, 26);
            this.button9.TabIndex = 9;
            this.button9.Tag = "?";
            this.button9.Text = "?";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.symbolControl_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1, 26);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(53, 26);
            this.button2.TabIndex = 9;
            this.button2.Tag = ">";
            this.button2.Text = ">";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.symbolControl_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 308);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "select * from table where:";
            // 
            // sBtnClear
            // 
            this.sBtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnClear.Location = new System.Drawing.Point(322, 303);
            this.sBtnClear.Name = "sBtnClear";
            this.sBtnClear.Size = new System.Drawing.Size(75, 23);
            this.sBtnClear.TabIndex = 9;
            this.sBtnClear.Text = "清空(&D)";
            this.sBtnClear.UseVisualStyleBackColor = true;
            // 
            // txtWhereClause
            // 
            this.txtWhereClause.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWhereClause.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtWhereClause.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtWhereClause.Location = new System.Drawing.Point(7, 327);
            this.txtWhereClause.Multiline = true;
            this.txtWhereClause.Name = "txtWhereClause";
            this.txtWhereClause.Size = new System.Drawing.Size(389, 90);
            this.txtWhereClause.TabIndex = 10;
            // 
            // sBtnApply
            // 
            this.sBtnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnApply.Location = new System.Drawing.Point(222, 423);
            this.sBtnApply.Name = "sBtnApply";
            this.sBtnApply.Size = new System.Drawing.Size(84, 32);
            this.sBtnApply.TabIndex = 11;
            this.sBtnApply.Text = "应用(&A)";
            this.sBtnApply.UseVisualStyleBackColor = true;
            this.sBtnApply.Click += new System.EventHandler(this.sBtnApply_Click);
            // 
            // sBtnClose
            // 
            this.sBtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sBtnClose.Location = new System.Drawing.Point(312, 423);
            this.sBtnClose.Name = "sBtnClose";
            this.sBtnClose.Size = new System.Drawing.Size(84, 32);
            this.sBtnClose.TabIndex = 11;
            this.sBtnClose.Text = "关闭(&X)";
            this.sBtnClose.UseVisualStyleBackColor = true;
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
            this.等于EToolStripMenuItem.Tag = "=";
            this.等于EToolStripMenuItem.Text = "等于(&E)";
            this.等于EToolStripMenuItem.Click += new System.EventHandler(this.symbolControl_Click);
            // 
            // 清空DToolStripMenuItem
            // 
            this.清空DToolStripMenuItem.Name = "清空DToolStripMenuItem";
            this.清空DToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.清空DToolStripMenuItem.Text = "清空(&D)";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblTables);
            this.panel2.Controls.Add(this.cmbTables);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(402, 26);
            this.panel2.TabIndex = 12;
            // 
            // lblTables
            // 
            this.lblTables.AutoSize = true;
            this.lblTables.Location = new System.Drawing.Point(7, 8);
            this.lblTables.Name = "lblTables";
            this.lblTables.Size = new System.Drawing.Size(41, 12);
            this.lblTables.TabIndex = 1;
            this.lblTables.Text = "表格：";
            // 
            // cmbTables
            // 
            this.cmbTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTables.FormattingEnabled = true;
            this.cmbTables.Location = new System.Drawing.Point(54, 4);
            this.cmbTables.Name = "cmbTables";
            this.cmbTables.Size = new System.Drawing.Size(342, 20);
            this.cmbTables.TabIndex = 0;
            this.cmbTables.SelectedIndexChanged += new System.EventHandler(this.cmbTables_SelectedIndexChanged);
            // 
            // AttributeQueryForm
            // 
            this.AcceptButton = this.sBtnApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.sBtnClose;
            this.ClientSize = new System.Drawing.Size(402, 461);
            this.Controls.Add(this.sBtnClose);
            this.Controls.Add(this.sBtnApply);
            this.Controls.Add(this.txtWhereClause);
            this.Controls.Add(this.sBtnClear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AttributeQueryForm";
            this.Text = "按属性查询";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.cMenuStripFields.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button sBtnClear;
        private System.Windows.Forms.TextBox txtSearchFields;
        private System.Windows.Forms.ListBox listBoxFields;
        private System.Windows.Forms.TextBox txtSearchValues;
        private System.Windows.Forms.ListBox listBoxValues;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtWhereClause;
        private System.Windows.Forms.Button sBtnGetUniqueValue;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button sBtnApply;
        private System.Windows.Forms.Button sBtnClose;
        private System.Windows.Forms.ContextMenuStrip cMenuStripFields;
        private System.Windows.Forms.ToolStripMenuItem 获取唯一值CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 等于EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空DToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTables;
        private System.Windows.Forms.ComboBox cmbTables;
    }
}