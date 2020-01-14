using WLib.Attributes.Description;

namespace WLib.Drawing
{
    /// <summary>
    /// 图标色彩风格类型
    /// </summary>
    public enum EImageColorType
    {
        /// <summary>
        /// 默认
        /// </summary>
        [Description("默认")]
        Default = 0,
        /// <summary>
        /// 自定义
        /// </summary>
        [Description("自定义")]
        Custom,


        /// <summary>
        /// 酷炫黑
        /// </summary>
        [Description("酷炫黑")]
        LinearDark,
        /// <summary>
        /// 天空蓝
        /// </summary>
        [Description("天空蓝")]
        LinearBlue,
        /// <summary>
        /// 火焰红
        /// </summary>
        [Description("火焰红")]
        LinearRed,
        /// <summary>
        /// 草原绿
        /// </summary>
        [Description("草原绿")]
        LinearGreen,
        /// <summary>
        /// 土豪金
        /// </summary>
        [Description("土豪金")]
        LinearGold,
        /// <summary>
        /// 珍珠白
        /// </summary>
        [Description("珍珠白")]
        LinearWhite,


        /// <summary>
        /// 五彩斑斓的黑
        /// </summary>
        [Description("五彩斑斓的黑")]
        ColorfulDark,
        /// <summary>
        /// 五颜六色的白
        /// </summary>
        [Description("五颜六色的白")]
        ColorfulWhite,
        /// <summary>
        /// 彩虹色
        /// </summary>
        [Description("彩虹色")]
        RinbowColor,
        /// <summary>
        /// 随机色彩
        /// </summary>
        [Description("随机色彩")]
        RandomColor,
    }
}
