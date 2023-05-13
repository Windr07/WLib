
using System.Drawing;
using System.Windows.Forms;

namespace WLib.WinCtrls.CodeGenerateCtrl
{
    partial class EntityCodeGenerator
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            cmbDbTypes = new ComboBox();
            txtConnectionStr = new TextBox();
            btnConnect = new Button();
            cListBoxTables = new CheckedListBox();
            groupBox1 = new GroupBox();
            lblTips = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            cMenuStripNamespaces = new ContextMenuStrip(components);
            menuItemNamespaceAdd = new ToolStripMenuItem();
            menuItemNamespaceDelete = new ToolStripMenuItem();
            cbAll = new CheckBox();
            progressBar1 = new ProgressBar();
            tabControl1 = new TabControl();
            tabPageCSharp = new TabPage();
            btnCreateCSharpCode = new Button();
            txtMsg = new TextBox();
            gBoxCSharpSettings = new GroupBox();
            listBoxCFieldAttr = new ListBox();
            cMenuStripAtField = new ContextMenuStrip(components);
            menuItemAtFieldAdd = new ToolStripMenuItem();
            menuItemAtFieldDelete = new ToolStripMenuItem();
            listBoxCClassAttr = new ListBox();
            cMenuStripAtClass = new ContextMenuStrip(components);
            menuItemAtClassAdd = new ToolStripMenuItem();
            menuItemAtClassDelete = new ToolStripMenuItem();
            listBoxCNamespaces = new ListBox();
            label4 = new Label();
            txtCSavePath = new TextBox();
            btnCOpen = new Button();
            btnCSelectDir = new Button();
            label10 = new Label();
            label6 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label5 = new Label();
            textBoxCExtends = new TextBox();
            textBoxCNamespace = new TextBox();
            tabPageJava = new TabPage();
            btnCreateJavaCode = new Button();
            txtJMsg = new TextBox();
            groupBox2 = new GroupBox();
            listBoxAtField = new ListBox();
            cMenuStripFieldAttr = new ContextMenuStrip(components);
            menuItemFieldAttrAdd = new ToolStripMenuItem();
            menuItemFieldAttrDelete = new ToolStripMenuItem();
            listBoxAtClass = new ListBox();
            cMenuStripClassAttr = new ContextMenuStrip(components);
            menuItemClassAttrAdd = new ToolStripMenuItem();
            menuItemClassAttrDelete = new ToolStripMenuItem();
            listBoxImports = new ListBox();
            cMenuStripImport = new ContextMenuStrip(components);
            menuItemImportAdd = new ToolStripMenuItem();
            menuItemImportDelete = new ToolStripMenuItem();
            label11 = new Label();
            txtJSavePath = new TextBox();
            btnJOpen = new Button();
            btnJSelect = new Button();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            label18 = new Label();
            label16 = new Label();
            label17 = new Label();
            txtImplement = new TextBox();
            txtExtendClass = new TextBox();
            txtPackage = new TextBox();
            btnSaveSettings = new Button();
            groupBox1.SuspendLayout();
            cMenuStripNamespaces.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPageCSharp.SuspendLayout();
            gBoxCSharpSettings.SuspendLayout();
            cMenuStripAtField.SuspendLayout();
            cMenuStripAtClass.SuspendLayout();
            tabPageJava.SuspendLayout();
            groupBox2.SuspendLayout();
            cMenuStripFieldAttr.SuspendLayout();
            cMenuStripClassAttr.SuspendLayout();
            cMenuStripImport.SuspendLayout();
            SuspendLayout();
            // 
            // cmbDbTypes
            // 
            cmbDbTypes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDbTypes.FormattingEnabled = true;
            cmbDbTypes.Location = new Point(83, 25);
            cmbDbTypes.Margin = new Padding(4);
            cmbDbTypes.Name = "cmbDbTypes";
            cmbDbTypes.Size = new Size(164, 25);
            cmbDbTypes.TabIndex = 1;
            cmbDbTypes.SelectedIndexChanged += cmbDbTypes_SelectedIndexChanged;
            // 
            // txtConnectionStr
            // 
            txtConnectionStr.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtConnectionStr.Location = new Point(83, 61);
            txtConnectionStr.Margin = new Padding(4);
            txtConnectionStr.Multiline = true;
            txtConnectionStr.Name = "txtConnectionStr";
            txtConnectionStr.Size = new Size(787, 54);
            txtConnectionStr.TabIndex = 2;
            // 
            // btnConnect
            // 
            btnConnect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnConnect.Location = new Point(727, 21);
            btnConnect.Margin = new Padding(4);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(143, 34);
            btnConnect.TabIndex = 3;
            btnConnect.Text = "连接";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // cListBoxTables
            // 
            cListBoxTables.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            cListBoxTables.FormattingEnabled = true;
            cListBoxTables.Location = new Point(7, 150);
            cListBoxTables.Margin = new Padding(4);
            cListBoxTables.Name = "cListBoxTables";
            cListBoxTables.Size = new Size(227, 436);
            cListBoxTables.TabIndex = 4;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(lblTips);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(btnConnect);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtConnectionStr);
            groupBox1.Controls.Add(cmbDbTypes);
            groupBox1.Location = new Point(7, 4);
            groupBox1.Margin = new Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4);
            groupBox1.Size = new Size(874, 140);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "数据库连接";
            // 
            // lblTips
            // 
            lblTips.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTips.BorderStyle = BorderStyle.None;
            lblTips.Location = new Point(107, 119);
            lblTips.Name = "lblTips";
            lblTips.ReadOnly = true;
            lblTips.Size = new Size(763, 16);
            lblTips.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 120);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(104, 17);
            label3.TabIndex = 3;
            label3.Text = "连接字符串示例：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 64);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(68, 17);
            label2.TabIndex = 3;
            label2.Text = "连接字符串";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 30);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(68, 17);
            label1.TabIndex = 3;
            label1.Text = "数据库类型";
            // 
            // cMenuStripNamespaces
            // 
            cMenuStripNamespaces.Items.AddRange(new ToolStripItem[] { menuItemNamespaceAdd, menuItemNamespaceDelete });
            cMenuStripNamespaces.Name = "contextMenuStrip1";
            cMenuStripNamespaces.Size = new Size(117, 48);
            // 
            // menuItemNamespaceAdd
            // 
            menuItemNamespaceAdd.Name = "menuItemNamespaceAdd";
            menuItemNamespaceAdd.Size = new Size(116, 22);
            menuItemNamespaceAdd.Text = "添加(&A)";
            // 
            // menuItemNamespaceDelete
            // 
            menuItemNamespaceDelete.Name = "menuItemNamespaceDelete";
            menuItemNamespaceDelete.Size = new Size(116, 22);
            menuItemNamespaceDelete.Text = "删除(&R)";
            // 
            // cbAll
            // 
            cbAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cbAll.AutoSize = true;
            cbAll.Checked = true;
            cbAll.CheckState = CheckState.Checked;
            cbAll.Location = new Point(8, 600);
            cbAll.Name = "cbAll";
            cbAll.Size = new Size(51, 21);
            cbAll.TabIndex = 9;
            cbAll.Text = "全选";
            cbAll.UseVisualStyleBackColor = true;
            cbAll.CheckedChanged += cbAll_CheckedChanged;
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.Location = new Point(7, 626);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(874, 6);
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 8;
            progressBar1.Visible = false;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPageCSharp);
            tabControl1.Controls.Add(tabPageJava);
            tabControl1.Location = new Point(237, 151);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(644, 470);
            tabControl1.TabIndex = 10;
            // 
            // tabPageCSharp
            // 
            tabPageCSharp.Controls.Add(btnCreateCSharpCode);
            tabPageCSharp.Controls.Add(txtMsg);
            tabPageCSharp.Controls.Add(gBoxCSharpSettings);
            tabPageCSharp.Location = new Point(4, 26);
            tabPageCSharp.Name = "tabPageCSharp";
            tabPageCSharp.Padding = new Padding(3);
            tabPageCSharp.Size = new Size(636, 440);
            tabPageCSharp.TabIndex = 0;
            tabPageCSharp.Text = "生成C#";
            tabPageCSharp.UseVisualStyleBackColor = true;
            // 
            // btnCreateCSharpCode
            // 
            btnCreateCSharpCode.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCreateCSharpCode.Location = new Point(513, 398);
            btnCreateCSharpCode.Name = "btnCreateCSharpCode";
            btnCreateCSharpCode.Size = new Size(117, 35);
            btnCreateCSharpCode.TabIndex = 2;
            btnCreateCSharpCode.Text = "生成代码(&C)";
            btnCreateCSharpCode.UseVisualStyleBackColor = true;
            btnCreateCSharpCode.Click += btnCreateCSharpCode_Click;
            // 
            // txtMsg
            // 
            txtMsg.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtMsg.Location = new Point(7, 216);
            txtMsg.Multiline = true;
            txtMsg.Name = "txtMsg";
            txtMsg.Size = new Size(623, 176);
            txtMsg.TabIndex = 1;
            // 
            // gBoxCSharpSettings
            // 
            gBoxCSharpSettings.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gBoxCSharpSettings.Controls.Add(listBoxCFieldAttr);
            gBoxCSharpSettings.Controls.Add(listBoxCClassAttr);
            gBoxCSharpSettings.Controls.Add(listBoxCNamespaces);
            gBoxCSharpSettings.Controls.Add(label4);
            gBoxCSharpSettings.Controls.Add(txtCSavePath);
            gBoxCSharpSettings.Controls.Add(btnCOpen);
            gBoxCSharpSettings.Controls.Add(btnCSelectDir);
            gBoxCSharpSettings.Controls.Add(label10);
            gBoxCSharpSettings.Controls.Add(label6);
            gBoxCSharpSettings.Controls.Add(label9);
            gBoxCSharpSettings.Controls.Add(label8);
            gBoxCSharpSettings.Controls.Add(label7);
            gBoxCSharpSettings.Controls.Add(label5);
            gBoxCSharpSettings.Controls.Add(textBoxCExtends);
            gBoxCSharpSettings.Controls.Add(textBoxCNamespace);
            gBoxCSharpSettings.Location = new Point(6, 4);
            gBoxCSharpSettings.Name = "gBoxCSharpSettings";
            gBoxCSharpSettings.Size = new Size(624, 206);
            gBoxCSharpSettings.TabIndex = 0;
            gBoxCSharpSettings.TabStop = false;
            gBoxCSharpSettings.Text = "设置";
            // 
            // listBoxCFieldAttr
            // 
            listBoxCFieldAttr.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            listBoxCFieldAttr.ContextMenuStrip = cMenuStripFieldAttr;
            listBoxCFieldAttr.FormattingEnabled = true;
            listBoxCFieldAttr.ItemHeight = 17;
            listBoxCFieldAttr.Location = new Point(462, 107);
            listBoxCFieldAttr.Name = "listBoxCFieldAttr";
            listBoxCFieldAttr.Size = new Size(156, 89);
            listBoxCFieldAttr.TabIndex = 9;
            // 
            // cMenuStripAtField
            // 
            cMenuStripAtField.Items.AddRange(new ToolStripItem[] { menuItemAtFieldAdd, menuItemAtFieldDelete });
            cMenuStripAtField.Name = "contextMenuStrip1";
            cMenuStripAtField.Size = new Size(117, 48);
            // 
            // menuItemAtFieldAdd
            // 
            menuItemAtFieldAdd.Name = "menuItemAtFieldAdd";
            menuItemAtFieldAdd.Size = new Size(116, 22);
            menuItemAtFieldAdd.Text = "添加(&A)";
            // 
            // menuItemAtFieldDelete
            // 
            menuItemAtFieldDelete.Name = "menuItemAtFieldDelete";
            menuItemAtFieldDelete.Size = new Size(116, 22);
            menuItemAtFieldDelete.Text = "删除(&R)";
            // 
            // listBoxCClassAttr
            // 
            listBoxCClassAttr.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            listBoxCClassAttr.ContextMenuStrip = cMenuStripClassAttr;
            listBoxCClassAttr.FormattingEnabled = true;
            listBoxCClassAttr.ItemHeight = 17;
            listBoxCClassAttr.Items.AddRange(new object[] { "Serializable" });
            listBoxCClassAttr.Location = new Point(302, 106);
            listBoxCClassAttr.Name = "listBoxCClassAttr";
            listBoxCClassAttr.Size = new Size(156, 89);
            listBoxCClassAttr.TabIndex = 9;
            // 
            // cMenuStripAtClass
            // 
            cMenuStripAtClass.Items.AddRange(new ToolStripItem[] { menuItemAtClassAdd, menuItemAtClassDelete });
            cMenuStripAtClass.Name = "contextMenuStrip1";
            cMenuStripAtClass.Size = new Size(117, 48);
            // 
            // menuItemAtClassAdd
            // 
            menuItemAtClassAdd.Name = "menuItemAtClassAdd";
            menuItemAtClassAdd.Size = new Size(116, 22);
            menuItemAtClassAdd.Text = "添加(&A)";
            // 
            // menuItemAtClassDelete
            // 
            menuItemAtClassDelete.Name = "menuItemAtClassDelete";
            menuItemAtClassDelete.Size = new Size(116, 22);
            menuItemAtClassDelete.Text = "删除(&R)";
            // 
            // listBoxCNamespaces
            // 
            listBoxCNamespaces.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            listBoxCNamespaces.ContextMenuStrip = cMenuStripNamespaces;
            listBoxCNamespaces.FormattingEnabled = true;
            listBoxCNamespaces.ItemHeight = 17;
            listBoxCNamespaces.Items.AddRange(new object[] { "System" });
            listBoxCNamespaces.Location = new Point(106, 88);
            listBoxCNamespaces.Name = "listBoxCNamespaces";
            listBoxCNamespaces.Size = new Size(185, 106);
            listBoxCNamespaces.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(18, 20);
            label4.Name = "label4";
            label4.Size = new Size(80, 17);
            label4.TabIndex = 8;
            label4.Text = "代码保存位置";
            // 
            // txtCSavePath
            // 
            txtCSavePath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCSavePath.Location = new Point(104, 17);
            txtCSavePath.Name = "txtCSavePath";
            txtCSavePath.Size = new Size(377, 23);
            txtCSavePath.TabIndex = 7;
            // 
            // btnCOpen
            // 
            btnCOpen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCOpen.Location = new Point(483, 12);
            btnCOpen.Name = "btnCOpen";
            btnCOpen.Size = new Size(49, 33);
            btnCOpen.TabIndex = 5;
            btnCOpen.Text = "打开";
            btnCOpen.UseVisualStyleBackColor = true;
            btnCOpen.Click += btnCOpen_Click;
            // 
            // btnCSelectDir
            // 
            btnCSelectDir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCSelectDir.Location = new Point(534, 12);
            btnCSelectDir.Name = "btnCSelectDir";
            btnCSelectDir.Size = new Size(87, 33);
            btnCSelectDir.TabIndex = 6;
            btnCSelectDir.Text = "选择";
            btnCSelectDir.UseVisualStyleBackColor = true;
            btnCSelectDir.Click += btnCSelectDir_Click;
            // 
            // label10
            // 
            label10.AutoEllipsis = true;
            label10.AutoSize = true;
            label10.ForeColor = SystemColors.HotTrack;
            label10.Location = new Point(6, 107);
            label10.Name = "label10";
            label10.Size = new Size(88, 17);
            label10.TabIndex = 4;
            label10.Text = "(右键菜单增删)";
            // 
            // label6
            // 
            label6.AutoEllipsis = true;
            label6.AutoSize = true;
            label6.Location = new Point(6, 86);
            label6.Name = "label6";
            label6.Size = new Size(92, 17);
            label6.TabIndex = 4;
            label6.Text = "引用的命名空间\r\n";
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Location = new Point(492, 86);
            label9.Name = "label9";
            label9.Size = new Size(122, 17);
            label9.TabIndex = 4;
            label9.Text = "在字段前加入的特性↓";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Location = new Point(301, 86);
            label8.Name = "label8";
            label8.Size = new Size(122, 17);
            label8.TabIndex = 4;
            label8.Text = "在类名前加入的特性↓";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new Point(308, 52);
            label7.Name = "label7";
            label7.Size = new Size(104, 17);
            label7.TabIndex = 4;
            label7.Text = "继承的父类或接口";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(30, 55);
            label5.Name = "label5";
            label5.Size = new Size(68, 17);
            label5.TabIndex = 4;
            label5.Text = "类命名空间";
            // 
            // textBoxCExtends
            // 
            textBoxCExtends.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBoxCExtends.Location = new Point(412, 49);
            textBoxCExtends.Name = "textBoxCExtends";
            textBoxCExtends.Size = new Size(209, 23);
            textBoxCExtends.TabIndex = 3;
            // 
            // textBoxCNamespace
            // 
            textBoxCNamespace.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxCNamespace.Location = new Point(104, 49);
            textBoxCNamespace.Name = "textBoxCNamespace";
            textBoxCNamespace.Size = new Size(187, 23);
            textBoxCNamespace.TabIndex = 3;
            textBoxCNamespace.Text = "MyLib.Model";
            // 
            // tabPageJava
            // 
            tabPageJava.Controls.Add(btnCreateJavaCode);
            tabPageJava.Controls.Add(txtJMsg);
            tabPageJava.Controls.Add(groupBox2);
            tabPageJava.Location = new Point(4, 26);
            tabPageJava.Name = "tabPageJava";
            tabPageJava.Padding = new Padding(3);
            tabPageJava.Size = new Size(636, 440);
            tabPageJava.TabIndex = 1;
            tabPageJava.Text = "生成Java";
            tabPageJava.UseVisualStyleBackColor = true;
            // 
            // btnCreateJavaCode
            // 
            btnCreateJavaCode.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCreateJavaCode.Location = new Point(512, 399);
            btnCreateJavaCode.Name = "btnCreateJavaCode";
            btnCreateJavaCode.Size = new Size(117, 35);
            btnCreateJavaCode.TabIndex = 4;
            btnCreateJavaCode.Text = "生成代码(&C)";
            btnCreateJavaCode.UseVisualStyleBackColor = true;
            btnCreateJavaCode.Click += btnCreateJavaCode_Click;
            // 
            // txtJMsg
            // 
            txtJMsg.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtJMsg.Location = new Point(6, 217);
            txtJMsg.Multiline = true;
            txtJMsg.Name = "txtJMsg";
            txtJMsg.Size = new Size(623, 176);
            txtJMsg.TabIndex = 3;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(listBoxAtField);
            groupBox2.Controls.Add(listBoxAtClass);
            groupBox2.Controls.Add(listBoxImports);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(txtJSavePath);
            groupBox2.Controls.Add(btnJOpen);
            groupBox2.Controls.Add(btnJSelect);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(label14);
            groupBox2.Controls.Add(label15);
            groupBox2.Controls.Add(label18);
            groupBox2.Controls.Add(label16);
            groupBox2.Controls.Add(label17);
            groupBox2.Controls.Add(txtImplement);
            groupBox2.Controls.Add(txtExtendClass);
            groupBox2.Controls.Add(txtPackage);
            groupBox2.Location = new Point(6, 6);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(624, 206);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "设置";
            // 
            // listBoxAtField
            // 
            listBoxAtField.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            listBoxAtField.ContextMenuStrip = cMenuStripAtField;
            listBoxAtField.FormattingEnabled = true;
            listBoxAtField.ItemHeight = 17;
            listBoxAtField.Items.AddRange(new object[] { "ApiModelProperty(value = \"Name\", name = \"Name\")" });
            listBoxAtField.Location = new Point(464, 111);
            listBoxAtField.Name = "listBoxAtField";
            listBoxAtField.Size = new Size(156, 89);
            listBoxAtField.TabIndex = 9;
            // 
            // cMenuStripFieldAttr
            // 
            cMenuStripFieldAttr.Items.AddRange(new ToolStripItem[] { menuItemFieldAttrAdd, menuItemFieldAttrDelete });
            cMenuStripFieldAttr.Name = "contextMenuStrip1";
            cMenuStripFieldAttr.Size = new Size(117, 48);
            // 
            // menuItemFieldAttrAdd
            // 
            menuItemFieldAttrAdd.Name = "menuItemFieldAttrAdd";
            menuItemFieldAttrAdd.Size = new Size(116, 22);
            menuItemFieldAttrAdd.Text = "添加(&A)";
            // 
            // menuItemFieldAttrDelete
            // 
            menuItemFieldAttrDelete.Name = "menuItemFieldAttrDelete";
            menuItemFieldAttrDelete.Size = new Size(116, 22);
            menuItemFieldAttrDelete.Text = "删除(&R)";
            // 
            // listBoxAtClass
            // 
            listBoxAtClass.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            listBoxAtClass.ContextMenuStrip = cMenuStripAtClass;
            listBoxAtClass.FormattingEnabled = true;
            listBoxAtClass.ItemHeight = 17;
            listBoxAtClass.Items.AddRange(new object[] { "Data", "ApiModel", "Api(tags=\"\")", "TableName(\"\")" });
            listBoxAtClass.Location = new Point(303, 111);
            listBoxAtClass.Name = "listBoxAtClass";
            listBoxAtClass.Size = new Size(155, 89);
            listBoxAtClass.TabIndex = 9;
            // 
            // cMenuStripClassAttr
            // 
            cMenuStripClassAttr.Items.AddRange(new ToolStripItem[] { menuItemClassAttrAdd, menuItemClassAttrDelete });
            cMenuStripClassAttr.Name = "contextMenuStrip1";
            cMenuStripClassAttr.Size = new Size(117, 48);
            // 
            // menuItemClassAttrAdd
            // 
            menuItemClassAttrAdd.Name = "menuItemClassAttrAdd";
            menuItemClassAttrAdd.Size = new Size(116, 22);
            menuItemClassAttrAdd.Text = "添加(&A)";
            // 
            // menuItemClassAttrDelete
            // 
            menuItemClassAttrDelete.Name = "menuItemClassAttrDelete";
            menuItemClassAttrDelete.Size = new Size(116, 22);
            menuItemClassAttrDelete.Text = "删除(&R)";
            // 
            // listBoxImports
            // 
            listBoxImports.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            listBoxImports.ContextMenuStrip = cMenuStripImport;
            listBoxImports.FormattingEnabled = true;
            listBoxImports.ItemHeight = 17;
            listBoxImports.Items.AddRange(new object[] { "java.io.Serializable", "java.util.Date", "lombok.Data", "com.baomidou.mybatisplus.annotation.IdType", "com.baomidou.mybatisplus.annotation.TableId", "com.baomidou.mybatisplus.annotation.TableName", "com.baomidou.mybatisplus.extension.activerecord.Model", "com.fasterxml.jackson.annotation.JsonFormat", "io.swagger.annotations.Api", "io.swagger.annotations.ApiModel", "io.swagger.annotations.ApiModelProperty" });
            listBoxImports.Location = new Point(92, 94);
            listBoxImports.Name = "listBoxImports";
            listBoxImports.Size = new Size(205, 106);
            listBoxImports.TabIndex = 9;
            // 
            // cMenuStripImport
            // 
            cMenuStripImport.Items.AddRange(new ToolStripItem[] { menuItemImportAdd, menuItemImportDelete });
            cMenuStripImport.Name = "contextMenuStrip1";
            cMenuStripImport.Size = new Size(117, 48);
            // 
            // menuItemImportAdd
            // 
            menuItemImportAdd.Name = "menuItemImportAdd";
            menuItemImportAdd.Size = new Size(116, 22);
            menuItemImportAdd.Text = "添加(&A)";
            // 
            // menuItemImportDelete
            // 
            menuItemImportDelete.Name = "menuItemImportDelete";
            menuItemImportDelete.Size = new Size(116, 22);
            menuItemImportDelete.Text = "删除(&R)";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 20);
            label11.Name = "label11";
            label11.Size = new Size(80, 17);
            label11.TabIndex = 8;
            label11.Text = "代码保存位置";
            // 
            // txtJSavePath
            // 
            txtJSavePath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtJSavePath.Location = new Point(92, 17);
            txtJSavePath.Name = "txtJSavePath";
            txtJSavePath.Size = new Size(387, 23);
            txtJSavePath.TabIndex = 7;
            // 
            // btnJOpen
            // 
            btnJOpen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnJOpen.Location = new Point(485, 10);
            btnJOpen.Name = "btnJOpen";
            btnJOpen.Size = new Size(49, 33);
            btnJOpen.TabIndex = 5;
            btnJOpen.Text = "打开";
            btnJOpen.UseVisualStyleBackColor = true;
            btnJOpen.Click += btnJOpen_Click;
            // 
            // btnJSelect
            // 
            btnJSelect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnJSelect.Location = new Point(536, 10);
            btnJSelect.Name = "btnJSelect";
            btnJSelect.Size = new Size(87, 33);
            btnJSelect.TabIndex = 6;
            btnJSelect.Text = "选择";
            btnJSelect.UseVisualStyleBackColor = true;
            btnJSelect.Click += btnJSelect_Click;
            // 
            // label12
            // 
            label12.AutoEllipsis = true;
            label12.AutoSize = true;
            label12.ForeColor = SystemColors.HotTrack;
            label12.Location = new Point(6, 107);
            label12.Name = "label12";
            label12.Size = new Size(88, 17);
            label12.TabIndex = 4;
            label12.Text = "(右键菜单增删)";
            // 
            // label13
            // 
            label13.AutoEllipsis = true;
            label13.AutoSize = true;
            label13.Location = new Point(6, 86);
            label13.Name = "label13";
            label13.Size = new Size(56, 17);
            label13.TabIndex = 4;
            label13.Text = "导入的包\r\n";
            // 
            // label14
            // 
            label14.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label14.AutoSize = true;
            label14.Location = new Point(500, 87);
            label14.Name = "label14";
            label14.Size = new Size(122, 17);
            label14.TabIndex = 4;
            label14.Text = "在字段前加入的注解↓";
            // 
            // label15
            // 
            label15.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label15.AutoSize = true;
            label15.Location = new Point(309, 87);
            label15.Name = "label15";
            label15.Size = new Size(122, 17);
            label15.TabIndex = 4;
            label15.Text = "在类名前加入的注解↓";
            // 
            // label18
            // 
            label18.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label18.AutoSize = true;
            label18.Location = new Point(412, 51);
            label18.Name = "label18";
            label18.Size = new Size(68, 17);
            label18.TabIndex = 4;
            label18.Text = "实现的接口";
            // 
            // label16
            // 
            label16.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label16.AutoSize = true;
            label16.Location = new Point(210, 52);
            label16.Name = "label16";
            label16.Size = new Size(56, 17);
            label16.TabIndex = 4;
            label16.Text = "继承的类";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(6, 55);
            label17.Name = "label17";
            label17.Size = new Size(32, 17);
            label17.TabIndex = 4;
            label17.Text = "包名";
            // 
            // txtImplement
            // 
            txtImplement.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtImplement.Location = new Point(485, 50);
            txtImplement.Name = "txtImplement";
            txtImplement.Size = new Size(133, 23);
            txtImplement.TabIndex = 3;
            // 
            // txtExtendClass
            // 
            txtExtendClass.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtExtendClass.Location = new Point(269, 50);
            txtExtendClass.Name = "txtExtendClass";
            txtExtendClass.Size = new Size(133, 23);
            txtExtendClass.TabIndex = 3;
            // 
            // txtPackage
            // 
            txtPackage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPackage.Location = new Point(44, 49);
            txtPackage.Name = "txtPackage";
            txtPackage.Size = new Size(161, 23);
            txtPackage.TabIndex = 3;
            txtPackage.Text = "MyLib.Model";
            // 
            // btnSaveSettings
            // 
            btnSaveSettings.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSaveSettings.Location = new Point(127, 592);
            btnSaveSettings.Name = "btnSaveSettings";
            btnSaveSettings.Size = new Size(108, 32);
            btnSaveSettings.TabIndex = 2;
            btnSaveSettings.Text = "保存设置(&C)";
            btnSaveSettings.UseVisualStyleBackColor = true;
            btnSaveSettings.Click += btnSaveSettings_Click;
            // 
            // EntityCodeGenerator
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnSaveSettings);
            Controls.Add(tabControl1);
            Controls.Add(cbAll);
            Controls.Add(progressBar1);
            Controls.Add(groupBox1);
            Controls.Add(cListBoxTables);
            Margin = new Padding(4);
            Name = "EntityCodeGenerator";
            Size = new Size(889, 633);
            Load += EntityCodeGenerator_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            cMenuStripNamespaces.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPageCSharp.ResumeLayout(false);
            tabPageCSharp.PerformLayout();
            gBoxCSharpSettings.ResumeLayout(false);
            gBoxCSharpSettings.PerformLayout();
            cMenuStripAtField.ResumeLayout(false);
            cMenuStripAtClass.ResumeLayout(false);
            tabPageJava.ResumeLayout(false);
            tabPageJava.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            cMenuStripFieldAttr.ResumeLayout(false);
            cMenuStripClassAttr.ResumeLayout(false);
            cMenuStripImport.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.ComboBox cmbDbTypes;
        private System.Windows.Forms.TextBox txtConnectionStr;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.CheckedListBox cListBoxTables;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip cMenuStripNamespaces;
        private Label label3;
        private CheckBox cbAll;
        private ProgressBar progressBar1;
        private TabControl tabControl1;
        private TabPage tabPageCSharp;
        private TabPage tabPageJava;
        private GroupBox gBoxCSharpSettings;
        private Button btnCreateCSharpCode;
        private TextBox txtMsg;
        private Label label5;
        private TextBox textBoxCNamespace;
        private Label label4;
        private TextBox txtCSavePath;
        private Button btnCOpen;
        private Button btnCSelectDir;
        private Label label6;
        private ListBox listBoxCNamespaces;
        private Label label7;
        private TextBox textBoxCExtends;
        private ListBox listBoxCFieldAttr;
        private ListBox listBoxCClassAttr;
        private Label label9;
        private Label label8;
        private Button btnSaveSettings;
        private ContextMenuStrip cMenuStripFieldAttr;
        private ContextMenuStrip cMenuStripClassAttr;
        private ToolStripMenuItem menuItemNamespaceAdd;
        private ToolStripMenuItem menuItemNamespaceDelete;
        private ToolStripMenuItem menuItemFieldAttrAdd;
        private ToolStripMenuItem menuItemFieldAttrDelete;
        private ToolStripMenuItem menuItemClassAttrAdd;
        private ToolStripMenuItem menuItemClassAttrDelete;
        private Label label10;
        private TextBox lblTips;
        private GroupBox groupBox2;
        private ListBox listBoxAtField;
        private ListBox listBoxAtClass;
        private ListBox listBoxImports;
        private Label label11;
        private TextBox txtJSavePath;
        private Button btnJOpen;
        private Button btnJSelect;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label18;
        private Label label16;
        private Label label17;
        private TextBox txtImplement;
        private TextBox txtExtendClass;
        private TextBox txtPackage;
        private Button btnCreateJavaCode;
        private TextBox txtJMsg;
        private ContextMenuStrip cMenuStripImport;
        private ToolStripMenuItem menuItemImportAdd;
        private ToolStripMenuItem menuItemImportDelete;
        private ContextMenuStrip cMenuStripAtClass;
        private ToolStripMenuItem menuItemAtClassAdd;
        private ToolStripMenuItem menuItemAtClassDelete;
        private ContextMenuStrip cMenuStripAtField;
        private ToolStripMenuItem menuItemAtFieldAdd;
        private ToolStripMenuItem menuItemAtFieldDelete;
    }
}
