using WLib.UserCtrls.ArcGisCtrl;

namespace WLib.Samples.WinForm
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加数据1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.放大ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.缩小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.漫游ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.拉框选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上一视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下一视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.绘制ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.多边形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.矩形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.圆ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.设置颜色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置线宽ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置字体ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.专题ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.简单渲染ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分级渲染ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图表渲染ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.唯一值渲染ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询图形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询属性ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapViewer1 = new WLib.UserCtrls.ArcGisCtrl.MapViewer();
            this.添加数据2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.视图ToolStripMenuItem,
            this.绘制ToolStripMenuItem1,
            this.专题ToolStripMenuItem,
            this.查询ToolStripMenuItem,
            this.数据DToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(856, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem,
            this.打开ToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.另存为ToolStripMenuItem,
            this.添加数据1ToolStripMenuItem,
            this.添加数据2ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.文件ToolStripMenuItem.Text = "文件(&F)";
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.新建ToolStripMenuItem.Text = "新建(&N)";
            this.新建ToolStripMenuItem.Click += new System.EventHandler(this.FileToolStripMenuItem_Click);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.打开ToolStripMenuItem.Text = "打开(&O)";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.FileToolStripMenuItem_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.保存ToolStripMenuItem.Text = "保存(&S)";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.FileToolStripMenuItem_Click);
            // 
            // 另存为ToolStripMenuItem
            // 
            this.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
            this.另存为ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.另存为ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.另存为ToolStripMenuItem.Text = "另存为(&A)";
            this.另存为ToolStripMenuItem.Click += new System.EventHandler(this.FileToolStripMenuItem_Click);
            // 
            // 添加数据1ToolStripMenuItem
            // 
            this.添加数据1ToolStripMenuItem.Name = "添加数据1ToolStripMenuItem";
            this.添加数据1ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.添加数据1ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.添加数据1ToolStripMenuItem.Text = "添加数据1(&Q)...";
            this.添加数据1ToolStripMenuItem.Click += new System.EventHandler(this.FileToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.退出ToolStripMenuItem.Text = "退出(&X)";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.FileToolStripMenuItem_Click);
            // 
            // 视图ToolStripMenuItem
            // 
            this.视图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.放大ToolStripMenuItem,
            this.缩小ToolStripMenuItem,
            this.漫游ToolStripMenuItem,
            this.全图ToolStripMenuItem,
            this.拉框选择ToolStripMenuItem,
            this.上一视图ToolStripMenuItem,
            this.下一视图ToolStripMenuItem});
            this.视图ToolStripMenuItem.Name = "视图ToolStripMenuItem";
            this.视图ToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.视图ToolStripMenuItem.Text = "视图(&V)";
            // 
            // 放大ToolStripMenuItem
            // 
            this.放大ToolStripMenuItem.Name = "放大ToolStripMenuItem";
            this.放大ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.放大ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.放大ToolStripMenuItem.Text = "放大(&I)";
            this.放大ToolStripMenuItem.Click += new System.EventHandler(this.ViewToolStripMenuItem_Click);
            // 
            // 缩小ToolStripMenuItem
            // 
            this.缩小ToolStripMenuItem.Name = "缩小ToolStripMenuItem";
            this.缩小ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.缩小ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.缩小ToolStripMenuItem.Text = "缩小(&O)";
            this.缩小ToolStripMenuItem.Click += new System.EventHandler(this.ViewToolStripMenuItem_Click);
            // 
            // 漫游ToolStripMenuItem
            // 
            this.漫游ToolStripMenuItem.Name = "漫游ToolStripMenuItem";
            this.漫游ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.漫游ToolStripMenuItem.Text = "漫游(&P)";
            this.漫游ToolStripMenuItem.Click += new System.EventHandler(this.ViewToolStripMenuItem_Click);
            // 
            // 全图ToolStripMenuItem
            // 
            this.全图ToolStripMenuItem.Name = "全图ToolStripMenuItem";
            this.全图ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.全图ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.全图ToolStripMenuItem.Text = "全图(F)";
            this.全图ToolStripMenuItem.Click += new System.EventHandler(this.ViewToolStripMenuItem_Click);
            // 
            // 拉框选择ToolStripMenuItem
            // 
            this.拉框选择ToolStripMenuItem.Name = "拉框选择ToolStripMenuItem";
            this.拉框选择ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.拉框选择ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.拉框选择ToolStripMenuItem.Text = "拉框选择(&S)";
            this.拉框选择ToolStripMenuItem.Click += new System.EventHandler(this.ViewToolStripMenuItem_Click);
            // 
            // 上一视图ToolStripMenuItem
            // 
            this.上一视图ToolStripMenuItem.Name = "上一视图ToolStripMenuItem";
            this.上一视图ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.上一视图ToolStripMenuItem.Text = "上一视图(&R)";
            this.上一视图ToolStripMenuItem.Click += new System.EventHandler(this.ViewToolStripMenuItem_Click);
            // 
            // 下一视图ToolStripMenuItem
            // 
            this.下一视图ToolStripMenuItem.Name = "下一视图ToolStripMenuItem";
            this.下一视图ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.下一视图ToolStripMenuItem.Text = "下一视图(&N)";
            this.下一视图ToolStripMenuItem.Click += new System.EventHandler(this.ViewToolStripMenuItem_Click);
            // 
            // 绘制ToolStripMenuItem1
            // 
            this.绘制ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.点ToolStripMenuItem,
            this.线ToolStripMenuItem,
            this.多边形ToolStripMenuItem,
            this.矩形ToolStripMenuItem,
            this.圆ToolStripMenuItem,
            this.文本ToolStripMenuItem,
            this.toolStripSeparator1,
            this.设置颜色ToolStripMenuItem,
            this.设置线宽ToolStripMenuItem,
            this.设置字体ToolStripMenuItem});
            this.绘制ToolStripMenuItem1.Name = "绘制ToolStripMenuItem1";
            this.绘制ToolStripMenuItem1.Size = new System.Drawing.Size(61, 21);
            this.绘制ToolStripMenuItem1.Text = "绘制(&D)";
            // 
            // 点ToolStripMenuItem
            // 
            this.点ToolStripMenuItem.Name = "点ToolStripMenuItem";
            this.点ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.点ToolStripMenuItem.Text = "点(&P)";
            this.点ToolStripMenuItem.Click += new System.EventHandler(this.DrawToolStripMenuItem_Click);
            // 
            // 线ToolStripMenuItem
            // 
            this.线ToolStripMenuItem.Name = "线ToolStripMenuItem";
            this.线ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.线ToolStripMenuItem.Text = "线(&L)";
            this.线ToolStripMenuItem.Click += new System.EventHandler(this.DrawToolStripMenuItem_Click);
            // 
            // 多边形ToolStripMenuItem
            // 
            this.多边形ToolStripMenuItem.Name = "多边形ToolStripMenuItem";
            this.多边形ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.多边形ToolStripMenuItem.Text = "多边形(&G)";
            this.多边形ToolStripMenuItem.Click += new System.EventHandler(this.DrawToolStripMenuItem_Click);
            // 
            // 矩形ToolStripMenuItem
            // 
            this.矩形ToolStripMenuItem.Name = "矩形ToolStripMenuItem";
            this.矩形ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.矩形ToolStripMenuItem.Text = "矩形(&R)";
            this.矩形ToolStripMenuItem.Click += new System.EventHandler(this.DrawToolStripMenuItem_Click);
            // 
            // 圆ToolStripMenuItem
            // 
            this.圆ToolStripMenuItem.Name = "圆ToolStripMenuItem";
            this.圆ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.圆ToolStripMenuItem.Text = "圆(&C)";
            this.圆ToolStripMenuItem.Click += new System.EventHandler(this.DrawToolStripMenuItem_Click);
            // 
            // 文本ToolStripMenuItem
            // 
            this.文本ToolStripMenuItem.Name = "文本ToolStripMenuItem";
            this.文本ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.文本ToolStripMenuItem.Text = "文本(&T)";
            this.文本ToolStripMenuItem.Click += new System.EventHandler(this.DrawToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // 设置颜色ToolStripMenuItem
            // 
            this.设置颜色ToolStripMenuItem.Name = "设置颜色ToolStripMenuItem";
            this.设置颜色ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.设置颜色ToolStripMenuItem.Text = "设置颜色(&O)...";
            // 
            // 设置线宽ToolStripMenuItem
            // 
            this.设置线宽ToolStripMenuItem.Name = "设置线宽ToolStripMenuItem";
            this.设置线宽ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.设置线宽ToolStripMenuItem.Text = "设置线宽(&W)...";
            // 
            // 设置字体ToolStripMenuItem
            // 
            this.设置字体ToolStripMenuItem.Name = "设置字体ToolStripMenuItem";
            this.设置字体ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.设置字体ToolStripMenuItem.Text = "设置字体(&F)...";
            // 
            // 专题ToolStripMenuItem
            // 
            this.专题ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.简单渲染ToolStripMenuItem,
            this.分级渲染ToolStripMenuItem,
            this.图表渲染ToolStripMenuItem,
            this.唯一值渲染ToolStripMenuItem});
            this.专题ToolStripMenuItem.Name = "专题ToolStripMenuItem";
            this.专题ToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.专题ToolStripMenuItem.Text = "专题(&R)";
            // 
            // 简单渲染ToolStripMenuItem
            // 
            this.简单渲染ToolStripMenuItem.Name = "简单渲染ToolStripMenuItem";
            this.简单渲染ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.简单渲染ToolStripMenuItem.Text = "简单渲染(&S)...";
            // 
            // 分级渲染ToolStripMenuItem
            // 
            this.分级渲染ToolStripMenuItem.Name = "分级渲染ToolStripMenuItem";
            this.分级渲染ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.分级渲染ToolStripMenuItem.Text = "分级渲染(&B)...";
            // 
            // 图表渲染ToolStripMenuItem
            // 
            this.图表渲染ToolStripMenuItem.Name = "图表渲染ToolStripMenuItem";
            this.图表渲染ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.图表渲染ToolStripMenuItem.Text = "图表渲染(&C)...";
            // 
            // 唯一值渲染ToolStripMenuItem
            // 
            this.唯一值渲染ToolStripMenuItem.Name = "唯一值渲染ToolStripMenuItem";
            this.唯一值渲染ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.唯一值渲染ToolStripMenuItem.Text = "唯一值渲染(&U)...";
            // 
            // 查询ToolStripMenuItem
            // 
            this.查询ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查询图形ToolStripMenuItem,
            this.查询属性ToolStripMenuItem});
            this.查询ToolStripMenuItem.Name = "查询ToolStripMenuItem";
            this.查询ToolStripMenuItem.Size = new System.Drawing.Size(62, 21);
            this.查询ToolStripMenuItem.Text = "查询(&Q)";
            // 
            // 查询图形ToolStripMenuItem
            // 
            this.查询图形ToolStripMenuItem.Name = "查询图形ToolStripMenuItem";
            this.查询图形ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.查询图形ToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.查询图形ToolStripMenuItem.Text = "查询图形(&Q)...";
            // 
            // 查询属性ToolStripMenuItem
            // 
            this.查询属性ToolStripMenuItem.Name = "查询属性ToolStripMenuItem";
            this.查询属性ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.查询属性ToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.查询属性ToolStripMenuItem.Text = "查询属性(&A)";
            // 
            // 数据DToolStripMenuItem
            // 
            this.数据DToolStripMenuItem.Name = "数据DToolStripMenuItem";
            this.数据DToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.数据DToolStripMenuItem.Text = "数据(&D)";
            // 
            // mapViewer1
            // 
            this.mapViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapViewer1.Location = new System.Drawing.Point(0, 25);
            this.mapViewer1.Name = "mapViewer1";
            this.mapViewer1.Size = new System.Drawing.Size(856, 434);
            this.mapViewer1.TabIndex = 2;
            // 
            // 添加数据2ToolStripMenuItem
            // 
            this.添加数据2ToolStripMenuItem.Name = "添加数据2ToolStripMenuItem";
            this.添加数据2ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.添加数据2ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.添加数据2ToolStripMenuItem.Text = "添加数据2(&D)...";
            this.添加数据2ToolStripMenuItem.Click += new System.EventHandler(this.FileToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 459);
            this.Controls.Add(this.mapViewer1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainForm";
            this.Text = "Sample";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加数据1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 视图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 放大ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 缩小ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 漫游ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 拉框选择ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上一视图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下一视图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 绘制ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 多边形ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文本ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置颜色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置线宽ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置字体ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 专题ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 简单渲染ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分级渲染ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图表渲染ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 唯一值渲染ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询图形ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询属性ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 矩形ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 圆ToolStripMenuItem;
        private MapViewer mapViewer1;
        private System.Windows.Forms.ToolStripMenuItem 数据DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加数据2ToolStripMenuItem;
    }
}