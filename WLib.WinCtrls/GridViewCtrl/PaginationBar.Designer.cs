namespace WLib.WinCtrls.GridViewCtrl
{
    partial class PaginationBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaginationBar));
            this.ToolStripPagination = new System.Windows.Forms.ToolStrip();
            this.btnFirst = new System.Windows.Forms.ToolStripButton();
            this.btnPre = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblPages = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNext = new System.Windows.Forms.ToolStripButton();
            this.btnLast = new System.Windows.Forms.ToolStripButton();
            this.btnAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.lblSumRecord = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.cmbRecordCnt = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.txtGotoPageNum = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGoto = new System.Windows.Forms.ToolStripButton();
            this.ToolStripPagination.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolStripPagination
            // 
            this.ToolStripPagination.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripPagination.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStripPagination.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFirst,
            this.btnPre,
            this.toolStripSeparator1,
            this.lblPages,
            this.toolStripSeparator6,
            this.btnNext,
            this.btnLast,
            this.btnAll,
            this.toolStripSeparator5,
            this.lblSumRecord,
            this.toolStripSeparator3,
            this.toolStripLabel5,
            this.cmbRecordCnt,
            this.toolStripLabel4,
            this.toolStripSeparator2,
            this.toolStripLabel3,
            this.txtGotoPageNum,
            this.toolStripLabel1,
            this.toolStripSeparator4,
            this.btnGoto});
            this.ToolStripPagination.Location = new System.Drawing.Point(0, 0);
            this.ToolStripPagination.Name = "ToolStripPagination";
            this.ToolStripPagination.Size = new System.Drawing.Size(609, 29);
            this.ToolStripPagination.TabIndex = 8;
            this.ToolStripPagination.Text = "toolStrip1";
            // 
            // btnFirst
            // 
            this.btnFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.Image")));
            this.btnFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(23, 26);
            this.btnFirst.Text = "第一页";
            this.btnFirst.ToolTipText = "第一页";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPre
            // 
            this.btnPre.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPre.Image = ((System.Drawing.Image)(resources.GetObject("btnPre.Image")));
            this.btnPre.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(23, 26);
            this.btnPre.Text = "上一页";
            this.btnPre.ToolTipText = "上一页";
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 29);
            // 
            // lblPages
            // 
            this.lblPages.Name = "lblPages";
            this.lblPages.Size = new System.Drawing.Size(35, 26);
            this.lblPages.Text = "1 / 1";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 29);
            // 
            // btnNext
            // 
            this.btnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(23, 26);
            this.btnNext.Text = "下一页";
            this.btnNext.ToolTipText = "下一页";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLast.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.Image")));
            this.btnLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(23, 26);
            this.btnLast.Text = "最后一页";
            this.btnLast.ToolTipText = "最后一页";
            this.btnLast.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnAll
            // 
            this.btnAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAll.Image = ((System.Drawing.Image)(resources.GetObject("btnAll.Image")));
            this.btnAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(23, 26);
            this.btnAll.Text = "显示全部数据";
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 29);
            // 
            // lblSumRecord
            // 
            this.lblSumRecord.Name = "lblSumRecord";
            this.lblSumRecord.Size = new System.Drawing.Size(63, 26);
            this.lblSumRecord.Text = "共0条记录";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 29);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(32, 26);
            this.toolStripLabel5.Text = "每页";
            // 
            // cmbRecordCnt
            // 
            this.cmbRecordCnt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRecordCnt.Name = "cmbRecordCnt";
            this.cmbRecordCnt.Size = new System.Drawing.Size(75, 29);
            this.cmbRecordCnt.SelectedIndexChanged += new System.EventHandler(this.cmbRecordCount_SelectedIndexChanged);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(44, 26);
            this.toolStripLabel4.Text = "条记录";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 29);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(56, 26);
            this.toolStripLabel3.Text = "跳转到第";
            // 
            // txtGotoPageNum
            // 
            this.txtGotoPageNum.Name = "txtGotoPageNum";
            this.txtGotoPageNum.Size = new System.Drawing.Size(40, 29);
            this.txtGotoPageNum.Text = "1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(20, 26);
            this.toolStripLabel1.Text = "页";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 29);
            // 
            // btnGoto
            // 
            this.btnGoto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnGoto.Image = ((System.Drawing.Image)(resources.GetObject("btnGoto.Image")));
            this.btnGoto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGoto.Name = "btnGoto";
            this.btnGoto.Size = new System.Drawing.Size(52, 26);
            this.btnGoto.Text = "  确定  ";
            this.btnGoto.Click += new System.EventHandler(this.btnGoto_Click);
            // 
            // PaginationBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.ToolStripPagination);
            this.Name = "PaginationBar";
            this.Size = new System.Drawing.Size(609, 29);
            this.ToolStripPagination.ResumeLayout(false);
            this.ToolStripPagination.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripButton btnFirst;
        private System.Windows.Forms.ToolStripButton btnPre;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel lblPages;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnNext;
        private System.Windows.Forms.ToolStripButton btnLast;
        private System.Windows.Forms.ToolStripButton btnAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel lblSumRecord;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripComboBox cmbRecordCnt;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox txtGotoPageNum;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnGoto;
        public System.Windows.Forms.ToolStrip ToolStripPagination;
    }
}
