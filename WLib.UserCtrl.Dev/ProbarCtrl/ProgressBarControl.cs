using System;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

namespace WLib.UserCtrls.Dev.ProbarCtrl
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProgressBarControl : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 当前进度提示
        /// </summary>
        private string _tips;
        /// <summary>
        /// 当前进度提示所能显示的最大字符串长度
        /// </summary>
        private int _tipsLength = 31;
        /// <summary>
        /// 当前进度提示
        /// </summary>
        public string Tips
        {
            get => _tips;
            set
            {
                _tips = value;
                if (value != null && value.Length > _tipsLength)
                    value = value.Substring(0, _tipsLength) + "...";
                this.labelControl1.Text = value;
            }
        }
        public BorderStyles ProgressBarPanelStyle { get => this.panelControl1.BorderStyle; set => this.panelControl1.BorderStyle = value; }
    
        /// <summary>
        /// 
        /// </summary>
        public ProgressBarControl()
        {
            InitializeComponent();
            this.marqueeBar.Properties.ProgressKind = DevExpress.XtraEditors.Controls.ProgressKind.Horizontal;
            this.marqueeBar.Properties.Stopped = false;
            this.marqueeBar.Properties.MarqueeAnimationSpeed = 120;
            Tips = this.labelControl1.Text;
        }


        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
