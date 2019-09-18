using DevExpress.LookAndFeel;
using System.Collections.Generic;

namespace WLib.WinCtrls.Dev.SkinCtrl
{
    /// <summary>
    /// 界面皮肤操作
    /// </summary>
    public static class SkinOpt
    {
        public const string 浅褐色 = "浅褐色";
        public const string Office2007蓝色 = "Office 2007 蓝色";
        public const string Office2007绿色 = "Office 2007 绿色";
        public const string Office2010蓝色 = "Office 2010 蓝色";
        public const string Office2010银色 = "Office 2010 银色";


        /// <summary>
        /// 皮肤类型的中文名和英文名键值对
        /// </summary>
        public static Dictionary<string, string> SkinNameToValue;
        /// <summary>
        /// 界面皮肤操作
        /// </summary>
        static SkinOpt()
        {
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.Skins.SkinManager.EnableMdiFormSkins();
            SkinNameToValue = new Dictionary<string, string>()
            {
                { 浅褐色, "Caramel" },
                { Office2007蓝色, "Office 2007 Blue" },
                { Office2007绿色, "Office 2007 Green" },
                { Office2010蓝色, "Office 2010 Blue" },
                { Office2010银色, "Office 2010 Silver" },
            };
        }
        /// <summary>
        /// 初始化并设置界面皮肤
        /// </summary>
        /// <param name="defaultLookAndFeel"></param>
        /// <param name="skinName">皮肤类型的中文名，必须为<see cref="SkinOpt.SkinNameToValue"/>.Keys的值之一</param>
        public static void SetSkin(this DefaultLookAndFeel defaultLookAndFeel, string skinName)
        {
            defaultLookAndFeel.LookAndFeel.SkinName = SkinNameToValue[skinName];
        }
    }
}
