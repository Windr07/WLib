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
        /// <param name="defaultLookAndFeel"></param>
        /// <param name="imageDir"></param>
        /// <param name="imageColorType"></param>
        public StyleSettingForm(DefaultLookAndFeel defaultLookAndFeel, string imageDir, EImageColorType imageColorType = EImageColorType.Default) : this()
        {
            this.thematicSettingControl.DefaultLookAndFeel = defaultLookAndFeel;
            this.iconColorfulControl1.ImageDir = imageDir;
            this.iconColorfulControl1.ImageColorType = imageColorType;
        }
    }
}