/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/3/28 16:26:08
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Display;
using System.Collections.Generic;

namespace WLib.ArcGis.Display
{
    /// <summary>
    /// 提供获得颜色IColor的方法
    /// </summary>
    public static class ColorCreate
    {
        /// <summary>
        /// 获得颜色IColor
        /// </summary>
        /// <param name="red">红色颜色分量，取值范围为0-255</param>
        /// <param name="green">绿色颜色分量，取值范围为0-255</param>
        /// <param name="blue">蓝色颜色分量，取值范围为0-255</param>
        /// <param name="transparency">色彩透明度（0透明 - 255不透明）</param>
        /// <returns></returns>
        public static IColor GetIColor(int red, int green, int blue, byte transparency = 255)
        {
            IRgbColor rgbColor = new RgbColorClass();
            rgbColor.Red = red;
            rgbColor.Green = green;
            rgbColor.Blue = blue;
            rgbColor.Transparency = transparency;
            return rgbColor;
        }
        /// <summary>
        /// 获得颜色IColor
        /// </summary>
        /// <param name="rrggbbtt">6位颜色值RRGGBB，如"ff0000"为红色；
        /// 或8位颜色值RRGGBBTT，如"ff0000ff"为红色不透明(最后两位00表示透明，ff表示不透明)</param>
        /// <returns></returns>
        public static IColor GetIColor(string rrggbbtt)
        {
            IRgbColor rgbColor = new RgbColor();
            rgbColor.Red = int.Parse(rrggbbtt.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            rgbColor.Green = int.Parse(rrggbbtt.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            rgbColor.Blue = int.Parse(rrggbbtt.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            if (rrggbbtt.Length == 8)
                rgbColor.Transparency = byte.Parse(rrggbbtt.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);

            return rgbColor;
        }
        /// <summary>
        /// 获得多个颜色（IColor）
        /// </summary>
        /// <param name="rrggbbtt">16进制表示的颜色字符串数组，每个数组元素可以为： 6位颜色值RRGGBB，如"ff0000"为红色；
        /// 或8位颜色值RRGGBBTT，如"ff0000ff"为红色不透明(最后两位00表示透明，ff表示不透明)</param>
        /// <returns></returns>
        public static IEnumerable<IColor> GetIColors(params string[] rrggbbtt)
        {
            foreach (var item in rrggbbtt)
                yield return GetIColor(item);
        }
    }
}
