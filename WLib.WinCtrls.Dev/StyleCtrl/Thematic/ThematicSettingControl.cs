/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/12/12
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using DevExpress.LookAndFeel;
using System;
using System.Linq;

namespace WLib.WinCtrls.Dev.StyleCtrl.Thematic
{
    /// <summary>
    /// 界面主题风格设置控件
    /// </summary>
    public partial class ThematicSettingControl : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// DevExpress全局界面主题风格
        /// </summary>
        public DefaultLookAndFeel DefaultLookAndFeel { get; set; }
        /// <summary>
        /// DevExpress全局界面主题风格改变事件
        /// </summary>
        public event EventHandler<ThematicStyleChangedEventArgs> ThematicStyleChanged;


        /// <summary>
        /// 界面主题风格设置控件
        /// </summary>
        public ThematicSettingControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 界面主题风格设置控件
        /// </summary>
        /// <param name="defaultLookAndFeel"></param>
        public ThematicSettingControl(DefaultLookAndFeel defaultLookAndFeel) : this() => this.DefaultLookAndFeel = defaultLookAndFeel;


        private void SkinSettingControl_Load(object sender, EventArgs e)
        {
            if (DefaultLookAndFeel == null)
                DefaultLookAndFeel = new DefaultLookAndFeel();
            var skinNames = ThematicManager.SkinNameToValue.Keys.ToArray();

            this.listBoxControl1.Items.AddRange(skinNames);
            var curName = this.DefaultLookAndFeel.LookAndFeel.SkinName;
            if (ThematicManager.SkinNameToValue.ContainsValue(curName))
                this.listBoxControl1.SelectedItem = ThematicManager.SkinNameToValue.First(v => v.Value == curName).Key;
        }

        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cnName = this.listBoxControl1.SelectedItem.ToString();
            this.DefaultLookAndFeel.SetSkin(cnName);
            ThematicStyleChanged?.Invoke(this, new ThematicStyleChangedEventArgs(cnName, ThematicManager.SkinNameToValue[cnName]));
        }
    }
}
