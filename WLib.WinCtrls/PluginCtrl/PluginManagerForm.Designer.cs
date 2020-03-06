namespace WLib.WinCtrls.PluginCtrl
{
    partial class PluginManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginManagerForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("节点16");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("节点15");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("节点23");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("节点25");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("菜单组", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("菜单页", new System.Windows.Forms.TreeNode[] {
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("节点21");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("主菜单栏", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("节点17");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("工具栏", new System.Windows.Forms.TreeNode[] {
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("窗口", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode10});
            this.splitContainerAll = new System.Windows.Forms.SplitContainer();
            this.splitContainerConfig = new System.Windows.Forms.SplitContainer();
            this.panel4 = new System.Windows.Forms.Panel();
            this.listBoxPlans = new WLib.WinCtrls.ListCtrl.ImageListBoxEx();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelPlanTop = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnDeletePlan = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panelSubSystem = new System.Windows.Forms.Panel();
            this.lblSubSystem = new System.Windows.Forms.Label();
            this.cmbSubSystem = new System.Windows.Forms.ComboBox();
            this.panelSystemName = new System.Windows.Forms.Panel();
            this.lblSystemName = new System.Windows.Forms.Label();
            this.groupBoxPlan = new System.Windows.Forms.GroupBox();
            this.splitContainerMenus = new System.Windows.Forms.SplitContainer();
            this.treeViewMenus = new System.Windows.Forms.TreeView();
            this.cMenuStripViewMenus = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.上移MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下移MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.添加子菜单AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插入分隔符MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移除分隔符MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移除MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.展开全部菜单EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.折叠全部菜单FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRemoveMenu = new System.Windows.Forms.Button();
            this.btnInsertSplit = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnAddMenu = new System.Windows.Forms.Button();
            this.splitContainerPlugin = new System.Windows.Forms.SplitContainer();
            this.treeViewCmds = new System.Windows.Forms.TreeView();
            this.cMenuStripPlugins = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.加入到菜单AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.重新加载插件RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.全部展开EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部折叠FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtPluginInfo = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCmdLib = new System.Windows.Forms.Label();
            this.cMenuStripPlans = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新建方案NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制方案CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除方案DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.选用此方案SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重命名方案NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTips = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerAll)).BeginInit();
            this.splitContainerAll.Panel1.SuspendLayout();
            this.splitContainerAll.Panel2.SuspendLayout();
            this.splitContainerAll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerConfig)).BeginInit();
            this.splitContainerConfig.Panel1.SuspendLayout();
            this.splitContainerConfig.Panel2.SuspendLayout();
            this.splitContainerConfig.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panelPlanTop.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelSubSystem.SuspendLayout();
            this.panelSystemName.SuspendLayout();
            this.groupBoxPlan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMenus)).BeginInit();
            this.splitContainerMenus.Panel1.SuspendLayout();
            this.splitContainerMenus.SuspendLayout();
            this.cMenuStripViewMenus.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPlugin)).BeginInit();
            this.splitContainerPlugin.Panel1.SuspendLayout();
            this.splitContainerPlugin.Panel2.SuspendLayout();
            this.splitContainerPlugin.SuspendLayout();
            this.cMenuStripPlugins.SuspendLayout();
            this.panel1.SuspendLayout();
            this.cMenuStripPlans.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerAll
            // 
            this.splitContainerAll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerAll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerAll.Location = new System.Drawing.Point(5, 5);
            this.splitContainerAll.Name = "splitContainerAll";
            // 
            // splitContainerAll.Panel1
            // 
            this.splitContainerAll.Panel1.Controls.Add(this.splitContainerConfig);
            // 
            // splitContainerAll.Panel2
            // 
            this.splitContainerAll.Panel2.Controls.Add(this.splitContainerPlugin);
            this.splitContainerAll.Panel2.Controls.Add(this.panel1);
            this.splitContainerAll.Size = new System.Drawing.Size(945, 546);
            this.splitContainerAll.SplitterDistance = 632;
            this.splitContainerAll.TabIndex = 1;
            // 
            // splitContainerConfig
            // 
            this.splitContainerConfig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerConfig.Location = new System.Drawing.Point(0, 0);
            this.splitContainerConfig.Name = "splitContainerConfig";
            // 
            // splitContainerConfig.Panel1
            // 
            this.splitContainerConfig.Panel1.Controls.Add(this.panel4);
            this.splitContainerConfig.Panel1.Controls.Add(this.panelSubSystem);
            this.splitContainerConfig.Panel1.Controls.Add(this.panelSystemName);
            this.splitContainerConfig.Panel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            // 
            // splitContainerConfig.Panel2
            // 
            this.splitContainerConfig.Panel2.Controls.Add(this.groupBoxPlan);
            this.splitContainerConfig.Size = new System.Drawing.Size(632, 546);
            this.splitContainerConfig.SplitterDistance = 269;
            this.splitContainerConfig.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.listBoxPlans);
            this.panel4.Controls.Add(this.panelPlanTop);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 48);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(267, 496);
            this.panel4.TabIndex = 9;
            // 
            // listBoxPlans
            // 
            this.listBoxPlans.DefaultImageIndex = -1;
            this.listBoxPlans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxPlans.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBoxPlans.FormattingEnabled = true;
            this.listBoxPlans.Images = this.imageList1;
            this.listBoxPlans.ImageSize = 16;
            this.listBoxPlans.Location = new System.Drawing.Point(0, 33);
            this.listBoxPlans.Name = "listBoxPlans";
            this.listBoxPlans.Size = new System.Drawing.Size(263, 459);
            this.listBoxPlans.TabIndex = 10;
            this.listBoxPlans.SelectedIndexChanged += new System.EventHandler(this.ListBoxPlans_SelectedIndexChanged);
            this.listBoxPlans.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListBoxPlans_MouseDoubleClick);
            this.listBoxPlans.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListBoxPlans_MouseDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "窗口.png");
            this.imageList1.Images.SetKeyName(1, "控件.png");
            this.imageList1.Images.SetKeyName(2, "菜单页.png");
            this.imageList1.Images.SetKeyName(3, "菜单组.png");
            this.imageList1.Images.SetKeyName(4, "菜单.png");
            this.imageList1.Images.SetKeyName(5, "工具.png");
            this.imageList1.Images.SetKeyName(6, "组件.png");
            this.imageList1.Images.SetKeyName(7, "插件.png");
            this.imageList1.Images.SetKeyName(8, "方案.png");
            this.imageList1.Images.SetKeyName(9, "exe.png");
            this.imageList1.Images.SetKeyName(10, "dll.png");
            this.imageList1.Images.SetKeyName(11, "分隔符.png");
            this.imageList1.Images.SetKeyName(12, "radiobutton.png");
            // 
            // panelPlanTop
            // 
            this.panelPlanTop.Controls.Add(this.tableLayoutPanel1);
            this.panelPlanTop.Controls.Add(this.label2);
            this.panelPlanTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPlanTop.Location = new System.Drawing.Point(0, 0);
            this.panelPlanTop.Name = "panelPlanTop";
            this.panelPlanTop.Size = new System.Drawing.Size(263, 33);
            this.panelPlanTop.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.btnAdd, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSelect, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDeletePlan, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(87, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(176, 31);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Location = new System.Drawing.Point(0, 0);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(58, 31);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "新建";
            this.toolTip1.SetToolTip(this.btnAdd, "新建");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.新建方案NToolStripMenuItem_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelect.Location = new System.Drawing.Point(116, 0);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(0);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(60, 31);
            this.btnSelect.TabIndex = 6;
            this.btnSelect.Text = "选用";
            this.toolTip1.SetToolTip(this.btnSelect, "选用");
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.选用此方案SToolStripMenuItem_Click);
            // 
            // btnDeletePlan
            // 
            this.btnDeletePlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeletePlan.Location = new System.Drawing.Point(58, 0);
            this.btnDeletePlan.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeletePlan.Name = "btnDeletePlan";
            this.btnDeletePlan.Size = new System.Drawing.Size(58, 31);
            this.btnDeletePlan.TabIndex = 6;
            this.btnDeletePlan.Text = "删除";
            this.toolTip1.SetToolTip(this.btnDeletePlan, "删除");
            this.btnDeletePlan.UseVisualStyleBackColor = true;
            this.btnDeletePlan.Click += new System.EventHandler(this.删除方案DToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "插件方案列表";
            // 
            // panelSubSystem
            // 
            this.panelSubSystem.Controls.Add(this.lblSubSystem);
            this.panelSubSystem.Controls.Add(this.cmbSubSystem);
            this.panelSubSystem.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSubSystem.Location = new System.Drawing.Point(0, 22);
            this.panelSubSystem.Name = "panelSubSystem";
            this.panelSubSystem.Size = new System.Drawing.Size(267, 26);
            this.panelSubSystem.TabIndex = 10;
            // 
            // lblSubSystem
            // 
            this.lblSubSystem.AutoSize = true;
            this.lblSubSystem.Location = new System.Drawing.Point(5, 6);
            this.lblSubSystem.Name = "lblSubSystem";
            this.lblSubSystem.Size = new System.Drawing.Size(41, 12);
            this.lblSubSystem.TabIndex = 1;
            this.lblSubSystem.Text = "子系统";
            // 
            // cmbSubSystem
            // 
            this.cmbSubSystem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSubSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubSystem.FormattingEnabled = true;
            this.cmbSubSystem.Location = new System.Drawing.Point(52, 3);
            this.cmbSubSystem.Name = "cmbSubSystem";
            this.cmbSubSystem.Size = new System.Drawing.Size(212, 20);
            this.cmbSubSystem.TabIndex = 0;
            this.cmbSubSystem.SelectedIndexChanged += new System.EventHandler(this.CmbSubSystem_SelectedIndexChanged);
            // 
            // panelSystemName
            // 
            this.panelSystemName.Controls.Add(this.lblSystemName);
            this.panelSystemName.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSystemName.Location = new System.Drawing.Point(0, 0);
            this.panelSystemName.Name = "panelSystemName";
            this.panelSystemName.Size = new System.Drawing.Size(267, 22);
            this.panelSystemName.TabIndex = 8;
            // 
            // lblSystemName
            // 
            this.lblSystemName.AutoSize = true;
            this.lblSystemName.Location = new System.Drawing.Point(5, 5);
            this.lblSystemName.Name = "lblSystemName";
            this.lblSystemName.Size = new System.Drawing.Size(77, 12);
            this.lblSystemName.TabIndex = 1;
            this.lblSystemName.Text = "当前系统名称";
            // 
            // groupBoxPlan
            // 
            this.groupBoxPlan.Controls.Add(this.splitContainerMenus);
            this.groupBoxPlan.Controls.Add(this.tableLayoutPanel2);
            this.groupBoxPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPlan.Location = new System.Drawing.Point(0, 0);
            this.groupBoxPlan.Name = "groupBoxPlan";
            this.groupBoxPlan.Size = new System.Drawing.Size(357, 544);
            this.groupBoxPlan.TabIndex = 2;
            this.groupBoxPlan.TabStop = false;
            this.groupBoxPlan.Text = "插件方案名称";
            // 
            // splitContainerMenus
            // 
            this.splitContainerMenus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerMenus.Location = new System.Drawing.Point(-1, 42);
            this.splitContainerMenus.Name = "splitContainerMenus";
            this.splitContainerMenus.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMenus.Panel1
            // 
            this.splitContainerMenus.Panel1.Controls.Add(this.treeViewMenus);
            // 
            // splitContainerMenus.Panel2
            // 
            this.splitContainerMenus.Panel2.AutoScroll = true;
            this.splitContainerMenus.Size = new System.Drawing.Size(359, 503);
            this.splitContainerMenus.SplitterDistance = 307;
            this.splitContainerMenus.TabIndex = 10;
            // 
            // treeViewMenus
            // 
            this.treeViewMenus.AllowDrop = true;
            this.treeViewMenus.ContextMenuStrip = this.cMenuStripViewMenus;
            this.treeViewMenus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewMenus.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeViewMenus.ImageIndex = 0;
            this.treeViewMenus.ImageList = this.imageList1;
            this.treeViewMenus.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.treeViewMenus.Location = new System.Drawing.Point(0, 0);
            this.treeViewMenus.Name = "treeViewMenus";
            treeNode1.ImageIndex = 4;
            treeNode1.Name = "节点16";
            treeNode1.Text = "节点16";
            treeNode2.ImageIndex = 11;
            treeNode2.Name = "节点15";
            treeNode2.Text = "节点15";
            treeNode3.ImageIndex = 4;
            treeNode3.Name = "节点23";
            treeNode3.Text = "节点23";
            treeNode4.ImageIndex = 4;
            treeNode4.Name = "节点25";
            treeNode4.Text = "节点25";
            treeNode5.ImageIndex = 3;
            treeNode5.Name = "菜单组";
            treeNode5.Text = "菜单组";
            treeNode6.ImageIndex = 2;
            treeNode6.Name = "节点20";
            treeNode6.Text = "菜单页";
            treeNode7.ImageIndex = 2;
            treeNode7.Name = "节点21";
            treeNode7.Text = "节点21";
            treeNode8.ImageIndex = 1;
            treeNode8.Name = "主菜单栏";
            treeNode8.Text = "主菜单栏";
            treeNode9.ImageIndex = 5;
            treeNode9.Name = "节点17";
            treeNode9.Text = "节点17";
            treeNode10.ImageIndex = 1;
            treeNode10.Name = "工具栏";
            treeNode10.Text = "工具栏";
            treeNode11.ImageIndex = 0;
            treeNode11.Name = "节点0";
            treeNode11.Text = "窗口";
            this.treeViewMenus.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode11});
            this.treeViewMenus.SelectedImageIndex = 12;
            this.treeViewMenus.Size = new System.Drawing.Size(359, 307);
            this.treeViewMenus.TabIndex = 8;
            this.treeViewMenus.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeViewCmds_ItemDrag);
            this.treeViewMenus.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewMenus_AfterSelect);
            this.treeViewMenus.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeViewMenus_DragDrop);
            this.treeViewMenus.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeViewMenus_DragEnter);
            this.treeViewMenus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TreeViewMenus_MouseUp);
            // 
            // cMenuStripViewMenus
            // 
            this.cMenuStripViewMenus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.上移MenuItem,
            this.下移MenuItem,
            this.toolStripSeparator3,
            this.添加子菜单AToolStripMenuItem,
            this.插入分隔符MenuItem,
            this.移除分隔符MenuItem,
            this.移除MenuItem,
            this.toolStripSeparator4,
            this.展开全部菜单EToolStripMenuItem,
            this.折叠全部菜单FToolStripMenuItem});
            this.cMenuStripViewMenus.Name = "cMenuStripViewMenus";
            this.cMenuStripViewMenus.Size = new System.Drawing.Size(186, 192);
            // 
            // 上移MenuItem
            // 
            this.上移MenuItem.Name = "上移MenuItem";
            this.上移MenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.上移MenuItem.Size = new System.Drawing.Size(185, 22);
            this.上移MenuItem.Text = "上移(&O)";
            this.上移MenuItem.Click += new System.EventHandler(this.上移OToolStripMenuItem_Click);
            // 
            // 下移MenuItem
            // 
            this.下移MenuItem.Name = "下移MenuItem";
            this.下移MenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.下移MenuItem.Size = new System.Drawing.Size(185, 22);
            this.下移MenuItem.Text = "下移(&P)";
            this.下移MenuItem.Click += new System.EventHandler(this.下移PToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(182, 6);
            // 
            // 添加子菜单AToolStripMenuItem
            // 
            this.添加子菜单AToolStripMenuItem.Name = "添加子菜单AToolStripMenuItem";
            this.添加子菜单AToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.添加子菜单AToolStripMenuItem.Text = "添加子菜单(&A)";
            this.添加子菜单AToolStripMenuItem.Click += new System.EventHandler(this.添加子菜单AToolStripMenuItem_Click);
            // 
            // 插入分隔符MenuItem
            // 
            this.插入分隔符MenuItem.Name = "插入分隔符MenuItem";
            this.插入分隔符MenuItem.Size = new System.Drawing.Size(185, 22);
            this.插入分隔符MenuItem.Text = "插入分隔符(&I)";
            this.插入分隔符MenuItem.Click += new System.EventHandler(this.插入分隔符IToolStripMenuItem_Click);
            // 
            // 移除分隔符MenuItem
            // 
            this.移除分隔符MenuItem.Name = "移除分隔符MenuItem";
            this.移除分隔符MenuItem.Size = new System.Drawing.Size(185, 22);
            this.移除分隔符MenuItem.Text = "移除分隔符(&O)";
            this.移除分隔符MenuItem.Click += new System.EventHandler(this.移除分隔符OToolStripMenuItem_Click);
            // 
            // 移除MenuItem
            // 
            this.移除MenuItem.Name = "移除MenuItem";
            this.移除MenuItem.Size = new System.Drawing.Size(185, 22);
            this.移除MenuItem.Text = "移除(&D)";
            this.移除MenuItem.Click += new System.EventHandler(this.移除RToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(182, 6);
            // 
            // 展开全部菜单EToolStripMenuItem
            // 
            this.展开全部菜单EToolStripMenuItem.Name = "展开全部菜单EToolStripMenuItem";
            this.展开全部菜单EToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.展开全部菜单EToolStripMenuItem.Text = "展开全部菜单(&E)";
            this.展开全部菜单EToolStripMenuItem.Click += new System.EventHandler(this.展开全部菜单EToolStripMenuItem_Click);
            // 
            // 折叠全部菜单FToolStripMenuItem
            // 
            this.折叠全部菜单FToolStripMenuItem.Name = "折叠全部菜单FToolStripMenuItem";
            this.折叠全部菜单FToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.折叠全部菜单FToolStripMenuItem.Text = "折叠全部菜单(&F)";
            this.折叠全部菜单FToolStripMenuItem.Click += new System.EventHandler(this.折叠全部菜单FToolStripMenuItem_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.04167F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.04167F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.625F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.625F));
            this.tableLayoutPanel2.Controls.Add(this.btnRemoveMenu, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnInsertSplit, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnDown, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnUp, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnAddMenu, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(-1, 15);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(359, 26);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // btnRemoveMenu
            // 
            this.btnRemoveMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRemoveMenu.Location = new System.Drawing.Point(0, 0);
            this.btnRemoveMenu.Margin = new System.Windows.Forms.Padding(0);
            this.btnRemoveMenu.Name = "btnRemoveMenu";
            this.btnRemoveMenu.Size = new System.Drawing.Size(59, 26);
            this.btnRemoveMenu.TabIndex = 7;
            this.btnRemoveMenu.Text = "移除";
            this.toolTip1.SetToolTip(this.btnRemoveMenu, "移除");
            this.btnRemoveMenu.UseVisualStyleBackColor = true;
            this.btnRemoveMenu.Click += new System.EventHandler(this.移除RToolStripMenuItem_Click);
            // 
            // btnInsertSplit
            // 
            this.btnInsertSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInsertSplit.Location = new System.Drawing.Point(152, 0);
            this.btnInsertSplit.Margin = new System.Windows.Forms.Padding(0);
            this.btnInsertSplit.Name = "btnInsertSplit";
            this.btnInsertSplit.Size = new System.Drawing.Size(93, 26);
            this.btnInsertSplit.TabIndex = 7;
            this.btnInsertSplit.Text = "插入分隔符";
            this.toolTip1.SetToolTip(this.btnInsertSplit, "插入分隔符");
            this.btnInsertSplit.UseVisualStyleBackColor = true;
            this.btnInsertSplit.Click += new System.EventHandler(this.插入分隔符IToolStripMenuItem_Click);
            // 
            // btnDown
            // 
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDown.Location = new System.Drawing.Point(301, 0);
            this.btnDown.Margin = new System.Windows.Forms.Padding(0);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(58, 26);
            this.btnDown.TabIndex = 7;
            this.btnDown.Text = "下移";
            this.toolTip1.SetToolTip(this.btnDown, "下移");
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.下移PToolStripMenuItem_Click);
            // 
            // btnUp
            // 
            this.btnUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUp.Location = new System.Drawing.Point(245, 0);
            this.btnUp.Margin = new System.Windows.Forms.Padding(0);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(56, 26);
            this.btnUp.TabIndex = 7;
            this.btnUp.Text = "上移";
            this.toolTip1.SetToolTip(this.btnUp, "上移");
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.上移OToolStripMenuItem_Click);
            // 
            // btnAddMenu
            // 
            this.btnAddMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddMenu.Location = new System.Drawing.Point(59, 0);
            this.btnAddMenu.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddMenu.Name = "btnAddMenu";
            this.btnAddMenu.Size = new System.Drawing.Size(93, 26);
            this.btnAddMenu.TabIndex = 8;
            this.btnAddMenu.Text = "添加子菜单";
            this.toolTip1.SetToolTip(this.btnAddMenu, "添加子菜单");
            this.btnAddMenu.UseVisualStyleBackColor = true;
            this.btnAddMenu.Click += new System.EventHandler(this.添加子菜单AToolStripMenuItem_Click);
            // 
            // splitContainerPlugin
            // 
            this.splitContainerPlugin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerPlugin.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerPlugin.Location = new System.Drawing.Point(0, 22);
            this.splitContainerPlugin.Name = "splitContainerPlugin";
            this.splitContainerPlugin.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerPlugin.Panel1
            // 
            this.splitContainerPlugin.Panel1.Controls.Add(this.treeViewCmds);
            // 
            // splitContainerPlugin.Panel2
            // 
            this.splitContainerPlugin.Panel2.Controls.Add(this.txtPluginInfo);
            this.splitContainerPlugin.Size = new System.Drawing.Size(307, 522);
            this.splitContainerPlugin.SplitterDistance = 366;
            this.splitContainerPlugin.TabIndex = 2;
            // 
            // treeViewCmds
            // 
            this.treeViewCmds.AllowDrop = true;
            this.treeViewCmds.ContextMenuStrip = this.cMenuStripPlugins;
            this.treeViewCmds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewCmds.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeViewCmds.ImageIndex = 0;
            this.treeViewCmds.ImageList = this.imageList1;
            this.treeViewCmds.Location = new System.Drawing.Point(0, 0);
            this.treeViewCmds.Name = "treeViewCmds";
            this.treeViewCmds.SelectedImageIndex = 12;
            this.treeViewCmds.Size = new System.Drawing.Size(307, 366);
            this.treeViewCmds.TabIndex = 0;
            this.treeViewCmds.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeViewCmds_ItemDrag);
            this.treeViewCmds.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeViewCmds_NodeMouseClick);
            // 
            // cMenuStripPlugins
            // 
            this.cMenuStripPlugins.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加入到菜单AToolStripMenuItem,
            this.toolStripSeparator2,
            this.重新加载插件RToolStripMenuItem,
            this.toolStripSeparator1,
            this.全部展开EToolStripMenuItem,
            this.全部折叠FToolStripMenuItem});
            this.cMenuStripPlugins.Name = "contextMenuStrip1";
            this.cMenuStripPlugins.Size = new System.Drawing.Size(165, 104);
            // 
            // 加入到菜单AToolStripMenuItem
            // 
            this.加入到菜单AToolStripMenuItem.Name = "加入到菜单AToolStripMenuItem";
            this.加入到菜单AToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.加入到菜单AToolStripMenuItem.Text = "加入到菜单(&A)";
            this.加入到菜单AToolStripMenuItem.Click += new System.EventHandler(this.加入到菜单AToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(161, 6);
            // 
            // 重新加载插件RToolStripMenuItem
            // 
            this.重新加载插件RToolStripMenuItem.Name = "重新加载插件RToolStripMenuItem";
            this.重新加载插件RToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.重新加载插件RToolStripMenuItem.Text = "重新加载插件(&R)";
            this.重新加载插件RToolStripMenuItem.Click += new System.EventHandler(this.重新加载插件RToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // 全部展开EToolStripMenuItem
            // 
            this.全部展开EToolStripMenuItem.Name = "全部展开EToolStripMenuItem";
            this.全部展开EToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.全部展开EToolStripMenuItem.Text = "全部展开(&E)";
            this.全部展开EToolStripMenuItem.Click += new System.EventHandler(this.全部展开EToolStripMenuItem_Click);
            // 
            // 全部折叠FToolStripMenuItem
            // 
            this.全部折叠FToolStripMenuItem.Name = "全部折叠FToolStripMenuItem";
            this.全部折叠FToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.全部折叠FToolStripMenuItem.Text = "全部折叠(&F)";
            this.全部折叠FToolStripMenuItem.Click += new System.EventHandler(this.全部折叠FToolStripMenuItem_Click);
            // 
            // txtPluginInfo
            // 
            this.txtPluginInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPluginInfo.Location = new System.Drawing.Point(0, 0);
            this.txtPluginInfo.Multiline = true;
            this.txtPluginInfo.Name = "txtPluginInfo";
            this.txtPluginInfo.ReadOnly = true;
            this.txtPluginInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPluginInfo.Size = new System.Drawing.Size(307, 152);
            this.txtPluginInfo.TabIndex = 0;
            this.txtPluginInfo.Text = "命令信息";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblCmdLib);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(307, 22);
            this.panel1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(102, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "将插件拖动到左侧作为软件菜单/工具";
            // 
            // lblCmdLib
            // 
            this.lblCmdLib.AutoSize = true;
            this.lblCmdLib.ContextMenuStrip = this.cMenuStripPlugins;
            this.lblCmdLib.Location = new System.Drawing.Point(6, 5);
            this.lblCmdLib.Name = "lblCmdLib";
            this.lblCmdLib.Size = new System.Drawing.Size(65, 12);
            this.lblCmdLib.TabIndex = 1;
            this.lblCmdLib.Text = "命令仓库▼";
            // 
            // cMenuStripPlans
            // 
            this.cMenuStripPlans.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建方案NToolStripMenuItem,
            this.复制方案CToolStripMenuItem,
            this.删除方案DToolStripMenuItem,
            this.toolStripSeparator5,
            this.选用此方案SToolStripMenuItem,
            this.重命名方案NToolStripMenuItem});
            this.cMenuStripPlans.Name = "cMenuStripPlans";
            this.cMenuStripPlans.Size = new System.Drawing.Size(155, 120);
            // 
            // 新建方案NToolStripMenuItem
            // 
            this.新建方案NToolStripMenuItem.Name = "新建方案NToolStripMenuItem";
            this.新建方案NToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.新建方案NToolStripMenuItem.Text = "新建方案(&N)";
            this.新建方案NToolStripMenuItem.Click += new System.EventHandler(this.新建方案NToolStripMenuItem_Click);
            // 
            // 复制方案CToolStripMenuItem
            // 
            this.复制方案CToolStripMenuItem.Name = "复制方案CToolStripMenuItem";
            this.复制方案CToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.复制方案CToolStripMenuItem.Text = "复制方案(&C)";
            this.复制方案CToolStripMenuItem.Click += new System.EventHandler(this.复制方案CToolStripMenuItem_Click);
            // 
            // 删除方案DToolStripMenuItem
            // 
            this.删除方案DToolStripMenuItem.Name = "删除方案DToolStripMenuItem";
            this.删除方案DToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.删除方案DToolStripMenuItem.Text = "删除方案(&D)";
            this.删除方案DToolStripMenuItem.Click += new System.EventHandler(this.删除方案DToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(151, 6);
            // 
            // 选用此方案SToolStripMenuItem
            // 
            this.选用此方案SToolStripMenuItem.Name = "选用此方案SToolStripMenuItem";
            this.选用此方案SToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.选用此方案SToolStripMenuItem.Text = "选用此方案(&S)";
            this.选用此方案SToolStripMenuItem.Click += new System.EventHandler(this.选用此方案SToolStripMenuItem_Click);
            // 
            // 重命名方案NToolStripMenuItem
            // 
            this.重命名方案NToolStripMenuItem.Name = "重命名方案NToolStripMenuItem";
            this.重命名方案NToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.重命名方案NToolStripMenuItem.Text = "重命名方案(&N)";
            this.重命名方案NToolStripMenuItem.Click += new System.EventHandler(this.重命名方案NToolStripMenuItem_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(720, 553);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(114, 33);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(836, 553);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(114, 33);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // lblTips
            // 
            this.lblTips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTips.AutoSize = true;
            this.lblTips.Location = new System.Drawing.Point(8, 562);
            this.lblTips.Name = "lblTips";
            this.lblTips.Size = new System.Drawing.Size(317, 12);
            this.lblTips.TabIndex = 4;
            this.lblTips.Text = "提示信息：可使用右键菜单或快捷键添加、修改、移除插件";
            // 
            // PluginManagerForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(954, 589);
            this.Controls.Add(this.lblTips);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.splitContainerAll);
            this.Name = "PluginManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "插件管理";
            this.splitContainerAll.Panel1.ResumeLayout(false);
            this.splitContainerAll.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerAll)).EndInit();
            this.splitContainerAll.ResumeLayout(false);
            this.splitContainerConfig.Panel1.ResumeLayout(false);
            this.splitContainerConfig.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerConfig)).EndInit();
            this.splitContainerConfig.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panelPlanTop.ResumeLayout(false);
            this.panelPlanTop.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelSubSystem.ResumeLayout(false);
            this.panelSubSystem.PerformLayout();
            this.panelSystemName.ResumeLayout(false);
            this.panelSystemName.PerformLayout();
            this.groupBoxPlan.ResumeLayout(false);
            this.splitContainerMenus.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMenus)).EndInit();
            this.splitContainerMenus.ResumeLayout(false);
            this.cMenuStripViewMenus.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.splitContainerPlugin.Panel1.ResumeLayout(false);
            this.splitContainerPlugin.Panel2.ResumeLayout(false);
            this.splitContainerPlugin.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPlugin)).EndInit();
            this.splitContainerPlugin.ResumeLayout(false);
            this.cMenuStripPlugins.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.cMenuStripPlans.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerAll;
        private System.Windows.Forms.SplitContainer splitContainerConfig;
        private System.Windows.Forms.SplitContainer splitContainerPlugin;
        private System.Windows.Forms.TreeView treeViewCmds;
        private System.Windows.Forms.TextBox txtPluginInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCmdLib;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panelPlanTop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnDeletePlan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelSystemName;
        private System.Windows.Forms.Label lblSystemName;
        private System.Windows.Forms.GroupBox groupBoxPlan;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnInsertSplit;
        private System.Windows.Forms.Button btnRemoveMenu;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TreeView treeViewMenus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip cMenuStripPlugins;
        private System.Windows.Forms.ToolStripMenuItem 全部展开EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全部折叠FToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 加入到菜单AToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 重新加载插件RToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cMenuStripViewMenus;
        private System.Windows.Forms.ToolStripMenuItem 上移MenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下移MenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 插入分隔符MenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移除MenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem 展开全部菜单EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 折叠全部菜单FToolStripMenuItem;
        private System.Windows.Forms.Label lblTips;
        private System.Windows.Forms.ContextMenuStrip cMenuStripPlans;
        private System.Windows.Forms.ToolStripMenuItem 新建方案NToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除方案DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选用此方案SToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem 重命名方案NToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制方案CToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private ListCtrl.ImageListBoxEx listBoxPlans;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.ToolStripMenuItem 添加子菜单AToolStripMenuItem;
        private System.Windows.Forms.Button btnAddMenu;
        private System.Windows.Forms.ToolStripMenuItem 移除分隔符MenuItem;
        private System.Windows.Forms.SplitContainer splitContainerMenus;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panelSubSystem;
        private System.Windows.Forms.Label lblSubSystem;
        private System.Windows.Forms.ComboBox cmbSubSystem;
    }
}