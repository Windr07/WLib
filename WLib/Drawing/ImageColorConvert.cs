using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WLib.Drawing
{
    /// <summary>
    /// 提供图片色彩转换方法
    /// </summary>
    public static class ImageColorConvert
    {
        /// <summary>
        /// 将源图标转换成指定色彩风格的图标
        /// </summary>
        /// <param name="bitmap">源图标对象</param>
        /// <param name="imageColorType">要转换的色彩风格</param>
        /// <param name="colors">当色彩风格为自定义（即<paramref name="imageColorType"/>为<see cref="EImageColorType.Custom"/>）时，自定义的色彩</param>
        /// <returns></returns>
        public static Bitmap ToColorType(this Bitmap bitmap, EImageColorType imageColorType, Color[] colors = null)
        {
            switch (imageColorType)
            {
                case EImageColorType.LinearDark: return bitmap.ToMultiColor(false, Color.DimGray, Color.Black);
                case EImageColorType.LinearBlue: return bitmap.ToMultiColor(false, Color.SkyBlue, Color.LightBlue);
                case EImageColorType.LinearRed: return bitmap.ToMultiColor(false, Color.OrangeRed, Color.Red);
                case EImageColorType.LinearGreen: return bitmap.ToMultiColor(false, Color.LawnGreen, Color.ForestGreen);
                case EImageColorType.LinearGold: return bitmap.ToMultiColor(false, Color.Yellow, Color.Gold);
                case EImageColorType.LinearWhite: return bitmap.ToMultiColor(false, Color.WhiteSmoke, Color.White);
                case EImageColorType.RandomColor: return bitmap.ToRandomColor();
                case EImageColorType.ColorfulDark: return bitmap.ToDarkMultiColor(true);
                case EImageColorType.ColorfulWhite: return bitmap.ToLightMultiColor(true);
                case EImageColorType.RinbowColor: return bitmap.TorRinbowColor();
                case EImageColorType.Custom: return bitmap.ToMultiColor(false, colors);
                default: return null;
            }
        }

        /// <summary>
        /// 图片变成黑白图
        /// </summary>
        /// <param name="bitmap">原始图</param>
        /// <param name="mode">模式。0:加权平均  1:算数平均</param>
        /// <returns></returns>
        public static Bitmap ToGray(this Bitmap bitmap, int mode = 0)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);//获得坐标(x,y)颜色
                    int alpha = color.A;
                    int value;
                    if (mode == 0)
                        value = (int)((color.R * 0.114f + color.G * 0.587f + color.B * 0.299f) / 3.0f);//加权平均
                    else
                        value = (color.R + color.G + color.B) / 3;//算数平均
                    bitmap.SetPixel(x, y, Color.FromArgb(alpha, value, value, value)); //设置颜色
                }
            }
            return bitmap;
        }
        /// <summary>
        /// 图片变成指定颜色的单色图
        /// </summary>
        /// <param name="bitmap">原始图</param>
        /// <param name="newColor">修改后的图片颜色</param>
        /// <returns></returns>
        public static Bitmap ToColor(this Bitmap bitmap, Color newColor)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);//获得坐标(x,y)颜色
                    bitmap.SetPixel(x, y, Color.FromArgb(color.A, newColor.R, newColor.G, newColor.B)); //设置颜色
                }
            }
            return bitmap;
        }
        /// <summary>
        /// 图片变成随机颜色的单色图
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Bitmap ToRandomColor(this Bitmap bitmap)
        {
            var ramdom = new Random();
            return ToColor(bitmap, Color.FromArgb(ramdom.Next(0, 256), ramdom.Next(0, 256), ramdom.Next(0, 256)));
        }
        /// <summary>
        /// 图片按照指定多种颜色渐变
        /// </summary>
        /// <param name="bitmap">原始图</param>
        /// <param name="vertical">色彩的渐变方向，true为纵向，False为横向</param>
        /// <returns></returns>
        public static Bitmap ToMultiColor(this Bitmap bitmap, bool vertical, params Color[] colors)
        {
            if (colors == null || colors.Length == 0)
                throw new ArgumentException($"必须指定一种以上的色彩，即参数{colors}不能为null，元素个数不能为0");

            int width = bitmap.Width;
            int height = bitmap.Height;
            float time = colors.Length - 1;
            float timeDistance = vertical ? height / time : width / time;
            float step = timeDistance;
            for (int i = 0; i < time; i++)
            {
                Color startColor = colors[i];
                Color endColor = colors[i + 1];

                int startY = vertical ? (int)(timeDistance * i) : 0;
                int endY = vertical ? (int)(timeDistance * (i + 1)) : height;
                int startX = vertical ? 0 : (int)(timeDistance * i);
                int endX = vertical ? width : (int)(timeDistance * (i + 1));

                float diffR = (float)(endColor.R - startColor.R) / step;
                float diffG = (float)(endColor.G - startColor.G) / step;
                float diffB = (float)(endColor.B - startColor.B) / step;
                for (int x = startX; x < endX; x++)
                {
                    for (int y = startY; y < endY; y++)
                    {
                        double coordinate = vertical ? y - startY : x - startX;
                        Color color = bitmap.GetPixel(x, y);//获得坐标(x,y)颜色
                        Color tmpColor = Color.FromArgb(color.A,
                            startColor.R + (int)(diffR * coordinate),
                            startColor.G + (int)(diffG * coordinate),
                            startColor.B + (int)(diffB * coordinate));

                        bitmap.SetPixel(x, y, tmpColor); //设置颜色
                    }
                }
            }

            return bitmap;
        }
        /// <summary>
        /// 图片按照指定多种颜色渐变，指定的多种颜色按随机顺序渐变
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="colors"></param>
        /// <param name="vertical"></param>
        /// <returns></returns>
        public static Bitmap ToRandomMultiColor(this Bitmap bitmap, IEnumerable<Color> colors, bool vertical = false)
        {
            var resultColor = new List<Color>();
            var colorList = colors.ToList();
            var random = new Random();
            var cnt = colorList.Count;
            for (int i = cnt; i > 0; i--)
            {
                var index = random.Next(0, colorList.Count);
                resultColor.Add(colorList[index]);
                colorList.RemoveAt(index);
            }
            return ToMultiColor(bitmap, vertical, resultColor.ToArray());
        }
        /// <summary>
        /// 图片按照多种深色调颜色渐变
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="random"></param>
        /// <param name="vertical"></param>
        /// <returns></returns>
        public static Bitmap ToDarkMultiColor(this Bitmap bitmap, bool random, bool vertical = false)
        {
            var colors = new Color[] { Color.Black, Color.DarkRed, Color.DarkGreen, Color.DarkBlue };
            if (random)
                return ToRandomMultiColor(bitmap, colors, vertical);
            else
                return ToMultiColor(bitmap, vertical, colors);
        }
        /// <summary>
        /// 图片按照多种浅色调颜色渐变
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="random"></param>
        /// <param name="vertical"></param>
        /// <returns></returns>
        public static Bitmap ToLightMultiColor(this Bitmap bitmap, bool random, bool vertical = false)
        {
            var colors = new Color[] { Color.White, Color.SeaShell, Color.Honeydew, Color.LightCyan };
            if (random)
                return ToRandomMultiColor(bitmap, colors, vertical);
            else
                return ToMultiColor(bitmap, vertical, colors);
        }
        /// <summary>
        /// 图片按照彩虹色渐变
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="vertical"></param>
        /// <returns></returns>
        public static Bitmap TorRinbowColor(this Bitmap bitmap, bool vertical = false)
        {
            var colors = new Color[] { Color.Coral, Color.Orange, Color.Yellow, Color.LimeGreen, Color.LightBlue, Color.Purple };
            return ToMultiColor(bitmap, vertical, colors);
        }
    }
}
