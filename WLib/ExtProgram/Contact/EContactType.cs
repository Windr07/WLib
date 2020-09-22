/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using WLib.Attributes.Description;

namespace WLib.ExtProgram.Contact
{
    /// <summary>
    /// 联系方式类型
    /// </summary>
    public enum EContactType
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        [DescriptionEx("邮箱")]
        EMail,
        /// <summary>
        /// 电话
        /// </summary>
        [DescriptionEx("电话")]
        Phone,
        /// <summary>
        /// QQ
        /// </summary>
        [DescriptionEx("QQ")]
        QQ,
        /// <summary>
        /// QQ群
        /// </summary>
        [DescriptionEx("QQ群")]
        QQGroup,
        /// <summary>
        /// 微信
        /// </summary>
        [DescriptionEx("微信")]
        Wechat,
        /// <summary>
        /// 网站
        /// </summary>
        [DescriptionEx("网站")]
        WebSite
    }
}
