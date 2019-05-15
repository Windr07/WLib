/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using WLib.Attributes;

namespace WLib.ExtProgram.Office
{
    /// <summary>
    /// Microsoft Office版本
    /// </summary>
    public enum EOfficeVersion
    {
        /// <summary>
        /// 1997年：office 8.0(office 97)
        /// </summary>
        [Description("Office 97")]
        Office97,
        /// <summary>
        /// 1999年：office 9.0(office 2000)　
        /// </summary>
        [Description("Office 2000")]
        Office2000,
        /// <summary>
        /// 2001年：office 10(office XP/2002)——Office XP/2002
        /// </summary>
        [Description("Office XP")]
        OfficeXP,
        /// <summary>
        /// 2003年：office 2003(office XP/2003)——  11.0
        /// </summary>
        [Description("Office 2003")]
        Office2003,
        /// <summary>
        /// 2007年：office 2007(office XP/2007) —— 12.0
        /// </summary>
        [Description("Office 2007")]
        Office2007,
        /// <summary>
        /// 2010年：office 2010 —— 14.0
        /// </summary>
        [Description("Office 2010")]
        Office2010,
        /// <summary>
        /// 2012年:office 2013 ——15.0
        /// </summary>
        [Description("Office 2013")]
        Office2013,
        /// <summary>
        /// 201x年:office 2016 ——16.0
        /// </summary>
        [Description("Office 2016")]
        Office2016
    }
}
