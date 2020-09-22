using System;

namespace WLib.WinCtrls.Dev.StyleCtrl.Thematic
{
    /// <summary>
    /// 为DevExpress全局界面主题风格改变事件<see cref="ThematicSettingControl.ThematicStyleChanged"/>提供参数
    /// </summary>
    public class ThematicStyleChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 主题皮肤中文名称
        /// </summary>
        public string CNName { get; set; }
        /// <summary>
        /// 主题皮肤英文名称
        /// </summary>
        public string ENName { get; set; }


        /// <summary>
        /// 为DevExpress全局界面主题风格改变事件<see cref="ThematicSettingControl.ThematicStyleChanged"/>提供参数
        /// </summary>
        public ThematicStyleChangedEventArgs()
        {
        }
        /// <summary>
        /// 为DevExpress全局界面主题风格改变事件<see cref="ThematicSettingControl.ThematicStyleChanged"/>提供参数
        /// </summary>
        /// <param name="cNName">主题皮肤中文名称</param>
        /// <param name="eNName">主题皮肤英文名称</param>
        public ThematicStyleChangedEventArgs(string cNName, string eNName)
        {
            CNName = cNName;
            ENName = eNName;
        }
    }
}
