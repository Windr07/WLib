/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Drawing;
using ESRI.ArcGIS.Display;

namespace WLib.ArcGis.Display
{
    public static class ColorConvert
    {
        /// <summary> 
        /// 将ArcGIS Engine中的IColor接口转换至.NET中的Color结构
        /// </summary>
        /// <param name="iColor">IColor</param>
        /// <returns>.NET中的System.Drawing.Color结构表示ARGB颜色</returns>
        public static Color ToColor(this IColor iColor)
        {
            return ColorTranslator.FromOle(((IRgbColor)iColor).RGB);
        }
        /// <summary> 
        /// 将ArcGIS Engine中的IRgbColor接口转换至.NET中的Color结构
        /// </summary>
        /// <param name="rgbColor">IRgbColor</param>
        /// <returns>.NET中的System.Drawing.Color结构表示ARGB颜色</returns>
        public static Color ToColor(this IRgbColor rgbColor)
        {
            return ColorTranslator.FromOle(rgbColor.RGB);
        }
        /// <summary> 
        /// 将.NET中的Color结构转换至于ArcGIS Engine中的IColor接口
        /// </summary>
        /// <param name="color">.NET中的System.Drawing.Color结构表示ARGB颜色</param>
        /// <returns>IColor</returns>
        public static IColor ToIColor(this Color color)
        {
            return new RgbColorClass { RGB = color.B * 65536 + color.G * 256 + color.R };
        }
    }
}
