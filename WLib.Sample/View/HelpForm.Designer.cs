namespace WLib.Samples.WinForm.View
{
    partial class frmHelp
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("打开Mxd");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("添加图层");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("打开", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("lyr");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("shp");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("cad矢量");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("cad栅格");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("选取点线面(null)");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("mdb(null)", new System.Windows.Forms.TreeNode[] {
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("添加图层", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("指南针");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("页面预览", new System.Windows.Forms.TreeNode[] {
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("删除图层");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("已有文档保存");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("新文档保存");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("保存", new System.Windows.Forms.TreeNode[] {
            treeNode14,
            treeNode15});
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("文件", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode12,
            treeNode13,
            treeNode16});
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("放大缩小");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("移动");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("全图");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("多边形选取");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("拉框选取");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("刷新");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("基本操作", new System.Windows.Forms.TreeNode[] {
            treeNode18,
            treeNode19,
            treeNode20,
            treeNode21,
            treeNode22,
            treeNode23});
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("自定义工具");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("操作", new System.Windows.Forms.TreeNode[] {
            treeNode24,
            treeNode25});
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("点");
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("线");
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("面");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("绘制图形", new System.Windows.Forms.TreeNode[] {
            treeNode27,
            treeNode28,
            treeNode29});
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("绘制文本");
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("设置属性");
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("绘制", new System.Windows.Forms.TreeNode[] {
            treeNode30,
            treeNode31,
            treeNode32});
            System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("设置图层与字段");
            System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("专题图", new System.Windows.Forms.TreeNode[] {
            treeNode34});
            System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("专题", new System.Windows.Forms.TreeNode[] {
            treeNode35});
            System.Windows.Forms.TreeNode treeNode37 = new System.Windows.Forms.TreeNode("查询属性");
            System.Windows.Forms.TreeNode treeNode38 = new System.Windows.Forms.TreeNode("查询图形");
            System.Windows.Forms.TreeNode treeNode39 = new System.Windows.Forms.TreeNode("查询", new System.Windows.Forms.TreeNode[] {
            treeNode37,
            treeNode38});
            System.Windows.Forms.TreeNode treeNode40 = new System.Windows.Forms.TreeNode("菜单", new System.Windows.Forms.TreeNode[] {
            treeNode17,
            treeNode26,
            treeNode33,
            treeNode36,
            treeNode39});
            System.Windows.Forms.TreeNode treeNode41 = new System.Windows.Forms.TreeNode("鹰眼");
            System.Windows.Forms.TreeNode treeNode42 = new System.Windows.Forms.TreeNode("自定义");
            System.Windows.Forms.TreeNode treeNode43 = new System.Windows.Forms.TreeNode("清除当前工具");
            System.Windows.Forms.TreeNode treeNode44 = new System.Windows.Forms.TreeNode("工具栏", new System.Windows.Forms.TreeNode[] {
            treeNode42,
            treeNode43});
            System.Windows.Forms.TreeNode treeNode45 = new System.Windows.Forms.TreeNode("符号选择器");
            System.Windows.Forms.TreeNode treeNode46 = new System.Windows.Forms.TreeNode("符号选择", new System.Windows.Forms.TreeNode[] {
            treeNode45});
            System.Windows.Forms.TreeNode treeNode47 = new System.Windows.Forms.TreeNode("地图");
            System.Windows.Forms.TreeNode treeNode48 = new System.Windows.Forms.TreeNode("图层树");
            System.Windows.Forms.TreeNode treeNode49 = new System.Windows.Forms.TreeNode("右键菜单", new System.Windows.Forms.TreeNode[] {
            treeNode47,
            treeNode48});
            System.Windows.Forms.TreeNode treeNode50 = new System.Windows.Forms.TreeNode("菜单快捷键");
            System.Windows.Forms.TreeNode treeNode51 = new System.Windows.Forms.TreeNode("右键菜单");
            System.Windows.Forms.TreeNode treeNode52 = new System.Windows.Forms.TreeNode("快捷键", new System.Windows.Forms.TreeNode[] {
            treeNode50,
            treeNode51});
            System.Windows.Forms.TreeNode treeNode53 = new System.Windows.Forms.TreeNode("其他");
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点0";
            treeNode1.Text = "打开Mxd";
            treeNode2.Name = "节点1";
            treeNode2.Text = "添加图层";
            treeNode3.Name = "节点0";
            treeNode3.Text = "打开";
            treeNode4.Name = "节点25";
            treeNode4.Text = "lyr";
            treeNode5.Name = "节点27";
            treeNode5.Text = "shp";
            treeNode6.Name = "节点0";
            treeNode6.Text = "cad矢量";
            treeNode7.Name = "节点1";
            treeNode7.Text = "cad栅格";
            treeNode8.Name = "节点6";
            treeNode8.Text = "选取点线面(null)";
            treeNode9.Name = "节点2";
            treeNode9.Text = "mdb(null)";
            treeNode10.Name = "节点12";
            treeNode10.Text = "添加图层";
            treeNode11.Name = "节点3";
            treeNode11.Text = "指南针";
            treeNode12.Name = "节点13";
            treeNode12.Text = "页面预览";
            treeNode13.Name = "节点14";
            treeNode13.Text = "删除图层";
            treeNode14.Name = "节点8";
            treeNode14.Text = "已有文档保存";
            treeNode15.Name = "节点9";
            treeNode15.Text = "新文档保存";
            treeNode16.Name = "节点7";
            treeNode16.Text = "保存";
            treeNode17.Name = "节点2";
            treeNode17.Text = "文件";
            treeNode18.Name = "节点11";
            treeNode18.Text = "放大缩小";
            treeNode19.Name = "节点12";
            treeNode19.Text = "移动";
            treeNode20.Name = "节点13";
            treeNode20.Text = "全图";
            treeNode21.Name = "节点14";
            treeNode21.Text = "多边形选取";
            treeNode22.Name = "节点15";
            treeNode22.Text = "拉框选取";
            treeNode23.Name = "节点16";
            treeNode23.Text = "刷新";
            treeNode24.Name = "节点15";
            treeNode24.Text = "基本操作";
            treeNode25.Name = "节点16";
            treeNode25.Text = "自定义工具";
            treeNode26.Name = "节点3";
            treeNode26.Text = "操作";
            treeNode27.Name = "节点17";
            treeNode27.Text = "点";
            treeNode28.Name = "节点18";
            treeNode28.Text = "线";
            treeNode29.Name = "节点19";
            treeNode29.Text = "面";
            treeNode30.Name = "节点17";
            treeNode30.Text = "绘制图形";
            treeNode31.Name = "节点18";
            treeNode31.Text = "绘制文本";
            treeNode32.Name = "节点24";
            treeNode32.Text = "设置属性";
            treeNode33.Name = "节点4";
            treeNode33.Text = "绘制";
            treeNode34.Name = "节点10";
            treeNode34.Text = "设置图层与字段";
            treeNode35.Name = "节点19";
            treeNode35.Text = "专题图";
            treeNode36.Name = "节点7";
            treeNode36.Text = "专题";
            treeNode37.Name = "节点21";
            treeNode37.Text = "查询属性";
            treeNode38.Name = "节点22";
            treeNode38.Text = "查询图形";
            treeNode39.Name = "节点5";
            treeNode39.Text = "查询";
            treeNode40.Name = "节点1";
            treeNode40.Text = "菜单";
            treeNode41.Name = "节点20";
            treeNode41.Text = "鹰眼";
            treeNode42.Name = "节点5";
            treeNode42.Text = "自定义";
            treeNode43.Name = "节点4";
            treeNode43.Text = "清除当前工具";
            treeNode44.Name = "节点2";
            treeNode44.Text = "工具栏";
            treeNode45.Name = "节点1";
            treeNode45.Text = "符号选择器";
            treeNode46.Name = "节点0";
            treeNode46.Text = "符号选择";
            treeNode47.Name = "节点7";
            treeNode47.Text = "地图";
            treeNode48.Name = "节点8";
            treeNode48.Text = "图层树";
            treeNode49.Name = "节点3";
            treeNode49.Text = "右键菜单";
            treeNode50.Name = "节点9";
            treeNode50.Text = "菜单快捷键";
            treeNode51.Name = "节点10";
            treeNode51.Text = "右键菜单";
            treeNode52.Name = "节点6";
            treeNode52.Text = "快捷键";
            treeNode53.Name = "节点6";
            treeNode53.Text = "其他";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode40,
            treeNode41,
            treeNode44,
            treeNode46,
            treeNode49,
            treeNode52,
            treeNode53});
            this.treeView1.Size = new System.Drawing.Size(156, 301);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(174, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(254, 330);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 319);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "查看";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 319);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 351);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.treeView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmHelp";
            this.Text = "帮助";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}