namespace WLib.WinCtrls.Dev.PathCtrl
{
    /// <summary>
    /// 在路径选择与显示的组合框<see cref="PathBoxControl"/>基础上，加上路径标题的控件
    /// </summary>
    public partial class LabelPathBoxControl : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 标题
        /// </summary>
        public override string Text { get => this.TitleLabel.Text; set => this.TitleLabel.Text = value; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get => this.PathBox.Path; set => this.PathBox.Path = value; }


        /// <summary>
        /// 在路径选择与显示的组合框<see cref="PathBoxControl"/>基础上，加上路径标题的控件
        /// </summary>
        public LabelPathBoxControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 在路径选择与显示的组合框<see cref="PathBoxControl"/>基础上，加上路径标题的控件
        /// </summary>
        /// <param name="text"></param>
        /// <param name="path"></param>
        public LabelPathBoxControl(string text, string path)
        {
            Text = text;
            Path = path;
        }

        public override string ToString() => $"{Name} - {Text}: {Path}";
    }
}
