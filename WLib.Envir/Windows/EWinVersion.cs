/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.ComponentModel;

namespace WLib.Envir.Windows
{
    /// <summary>
    /// Windows 操作系统版本
    /// </summary>
    public enum EWinVersion
    {
        [Description("未知")]
        UnKnown,

        [Description("Windows XP")]
        XP,

        [Description("Windows 98")]
        Win98,

        [Description("Windows 95")]
        Win95,

        [Description("Windows 7")]
        Win7,

        [Description("Windows 8")]
        Win8,

        [Description("Windows 8.1")]
        Win81,

        [Description("Windows 10")]
        Win10,

        [Description("Windows或以上版本")]
        Win8或以上,

        [Description("Windows 2000")]
        Win2000,

        [Description("Windows Me")]
        WinMe,

        [Description("Windows Vista")]
        Vista,
    }
}
