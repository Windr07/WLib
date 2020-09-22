/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

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
        [DescriptionEx("默认")]
        Default = 0,
        /// <summary>
        /// 自定义
        /// </summary>
        [DescriptionEx("自定义")]
        Custom,


        /// <summary>
        /// 酷炫黑
        /// </summary>
        [DescriptionEx("酷炫黑")]
        LinearDark,
        /// <summary>
        /// 天空蓝
        /// </summary>
        [DescriptionEx("天空蓝")]
        LinearBlue,
        /// <summary>
        /// 火焰红
        /// </summary>
        [DescriptionEx("火焰红")]
        LinearRed,
        /// <summary>
        /// 草原绿
        /// </summary>
        [DescriptionEx("草原绿")]
        LinearGreen,
        /// <summary>
        /// 土豪金
        /// </summary>
        [DescriptionEx("土豪金")]
        LinearGold,
        /// <summary>
        /// 珍珠白
        /// </summary>
        [DescriptionEx("珍珠白")]
        LinearWhite,


        /// <summary>
        /// 五彩斑斓的黑
        /// </summary>
        [DescriptionEx("五彩斑斓的黑")]
        ColorfulDark,
        /// <summary>
        /// 五颜六色的白
        /// </summary>
        [DescriptionEx("五颜六色的白")]
        ColorfulWhite,
        /// <summary>
        /// 彩虹色
        /// </summary>
        [DescriptionEx("彩虹色")]
        RinbowColor,
        /// <summary>
        /// 随机色彩
        /// </summary>
        [DescriptionEx("随机色彩")]
        RandomColor,
    }
}
