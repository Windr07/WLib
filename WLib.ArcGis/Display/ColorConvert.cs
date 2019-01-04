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
    public class ColorConvert
    {
        /// <summary> 
        /// 将ArcGIS Engine中的IRgbColor接口转换至.NET中的Color结构
        /// </summary>
        /// <param name="rgbColor">IRgbColor</param>
        /// <returns>.NET中的System.Drawing.Color结构表示ARGB颜色</returns>
        public static Color ConvertIRgbColorToColor(IRgbColor rgbColor)
        {
            return ColorTranslator.FromOle(rgbColor.RGB);
        }
        /// <summary> 
        /// 将.NET中的Color结构转换至于ArcGIS Engine中的IColor接口
        /// </summary>
        /// <param name="color">.NET中的System.Drawing.Color结构表示ARGB颜色</param>
        /// <returns>IColor</returns>
        public static IColor ConvertColorToIColor(Color color)
        {
            IColor rgbColor = new RgbColorClass();
            rgbColor.RGB = color.B * 65536 + color.G * 256 + color.R;
            return rgbColor;
        }
    }
}
