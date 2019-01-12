using System;
using System.Windows.Forms;

namespace WLib.UserCtrls.Dev.ProbarControl
{
    public partial class ProgressBarControl : DevExpress.XtraEditors.XtraUserControl
    {
        private string _tips;
        private int _tipsLength = 31;
        public DevExpress.XtraEditors.Controls.BorderStyles ProgressBarPanelStyle
        {
            get => this.panelControl1.BorderStyle;
            set => this.panelControl1.BorderStyle = value;
        }

        /// <summary>
        /// 当前进度提示
        /// </summary>
        public String Tips
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

        public ProgressBarControl()
        {
            InitializeComponent();
            this.marqueeBar.Properties.ProgressKind = DevExpress.XtraEditors.Controls.ProgressKind.Horizontal;
            this.marqueeBar.Properties.Stopped = false;
            this.marqueeBar.Properties.MarqueeAnimationSpeed = 120;
            Tips = this.labelControl1.Text;
        }

        private string _messageShow;
        public string MessageShow
        {
            get => _messageShow;
            set
            {
                if (_messageShow != value)
                {
                    _messageShow = value;
                    OnChageLabelValue(value);
                }
            }
        }

        private void OnChageLabelValue(string value)
        {
            this.Tips = value;
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
