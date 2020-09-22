using DevExpress.LookAndFeel;
using System;
using WLib.Drawing;
using WLib.WinCtrls.Dev.StyleCtrl.ImageColorful;
using WLib.WinCtrls.Dev.StyleCtrl.Thematic;

namespace WLib.WinCtrls.Dev.StyleCtrl
{
    /// <summary>
    /// 界面风格设置控件
    /// </summary>
    public partial class StyleSettingForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 图标色彩风格类型改变事件
        /// </summary>
        public event EventHandler<ImageStyleChangedEventArgs> ImageColorStyleChanged;
        /// <summary>
        /// 全局界面主题风格改变事件
        /// </summary>
        public event EventHandler<ThematicStyleChangedEventArgs> ThematicStyleChanged;


        /// <summary>
        /// 界面风格设置控件
        /// </summary>
        public StyleSettingForm()
        {
            InitializeComponent();
            this.iconColorfulControl1.ImageColorStyleChanged += (sender, e) => ImageColorStyleChanged?.Invoke(sender, e);
            this.thematicSettingControl.ThematicStyleChanged += (sender, e) => ThematicStyleChanged?.Invoke(sender, e);
        }
        /// <summary>
        /// 界面风格设置控件
        /// </summary>
        /// <param name="showImageStyle">表示优先显示图标色彩风格设置(true)，还是界面主题风格(false)</param>
        public StyleSettingForm(bool showImageStyle) : this()
        {
            this.xtraTabControl1.SelectedTabPage = showImageStyle ? this.xtraTabPageIcon : this.xtraTabPageThematic;
        }
        /// <summary>
        /// 界面风格设置控件
        /// </summary>
        /// <param name="defaultLookAndFeel"></param>
        /// <param name="imageDir">修改图标色彩风格时，图标保存的位置</param>
        /// <param name="imageColorType">当前图标色彩风格类型</param>
        /// <param name="showImageStyle">表示优先显示图标色彩风格设置(true)，还是界面主题风格(false)</param>
        public StyleSettingForm(DefaultLookAndFeel defaultLookAndFeel, string imageDir, EImageColorType imageColorType = EImageColorType.Default, bool showImageStyle = false) : this(showImageStyle)
        {
            this.thematicSettingControl.DefaultLookAndFeel = defaultLookAndFeel;
            this.iconColorfulControl1.ImageDir = imageDir;
            this.iconColorfulControl1.ImageColorType = imageColorType;
        }
    }
}