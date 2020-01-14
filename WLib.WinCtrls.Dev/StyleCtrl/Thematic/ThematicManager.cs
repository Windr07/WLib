/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using DevExpress.LookAndFeel;
using System.Collections.Generic;

namespace WLib.WinCtrls.Dev.StyleCtrl.Thematic
{
    /// <summary>
    /// 界面主题风格操作
    /// </summary>
    public static class ThematicManager
    {
        /// <summary>
        /// 主题风格类型的中文名和英文名键值对
        /// </summary>
        public static Dictionary<string, string> SkinNameToValue { get; } = new Dictionary<string, string>
        {
            { "Office 2007 蓝色" , "Office 2007 Blue" },
            { "Office 2007 黑色" , "Office 2007 Black" },
            { "DevExpress 浅色", "DevExpress Style" },
            { "DevExpress 黑色"  , "DevExpress Dark Style" },
            { "Visual Studio 2010", "VS2010" },
            { "Seven Classic" , "Seven Classic" },
            { "Office 2010 蓝色" , "Office 2010 Blue" },
            { "Office 2010 黑色" , "Office 2010 Black" },
            { "Office 2010 银色" , "Office 2010 Silver" },
            { "Office 2013" , "Office 2013" },
            { "Office 2013 深灰" , "Office 2013 Dark Gray" },
            { "Office 2010 浅灰" , "Office 2010 Light Gray" },
            { "Visual Studio 2013 蓝色" , "Visual Studio 2013 Blue" },
            { "Visual Studio 2013 浅色" , "Visual Studio 2013 Light" },
            { "Visual Studio 2013 黑色" , "Visual Studio 2013 Dark" },
            { "Office 2016 彩色" , "Office 2016 Colorful"},
            { "Office 2016 深色" ,"Office 2016 Dark" },
            { "Office 2016 黑色" , "Office 2016 Black" },
            { "The Bezier" , "The Bezier" },
            { "Office 2019 彩色" , "Office 2019 Colorful" },
        };
        /// <summary>
        /// 界面主题风格操作
        /// </summary>
        static ThematicManager()
        {
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.Skins.SkinManager.EnableMdiFormSkins();
        }
        /// <summary>
        /// 初始化并设置界面主题风格
        /// </summary>
        /// <param name="defaultLookAndFeel"></param>
        /// <param name="skinName">主题风格类型的中文名，必须为<see cref="ThematicManager.SkinNameToValue"/>.Keys的值之一</param>
        public static void SetSkin(this DefaultLookAndFeel defaultLookAndFeel, string skinName)
        {
            defaultLookAndFeel.LookAndFeel.SkinName = SkinNameToValue[skinName];
        }
    }
}
