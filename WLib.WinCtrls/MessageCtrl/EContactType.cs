using WLib.Attributes.Description;

namespace WLib.WinCtrls.MessageCtrl
{
    /// <summary>
    /// 联系方式类型
    /// </summary>
    public enum EContactType
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        [Description("邮箱")]
        EMail,
        /// <summary>
        /// 电话
        /// </summary>
        [Description("电话")]
        Phone,
        /// <summary>
        /// QQ
        /// </summary>
        [Description("QQ")]
        QQ,
        /// <summary>
        /// QQ群
        /// </summary>
        [Description("QQ群")]
        QQGroup,
        /// <summary>
        /// 微信
        /// </summary>
        [Description("微信")]
        Wechat,
        /// <summary>
        /// 网站
        /// </summary>
        [Description("网站")]
        WebSite
    }
}
