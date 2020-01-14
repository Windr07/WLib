using System;
using WLib.Drawing;

namespace WLib.WinCtrls.Dev.StyleCtrl.ImageColorful
{
    /// <summary>
    /// 为图片色彩样式改变事件<see cref="IamgeColorfulControl.ImageColorStyleItemChanged"/>提供参数
    /// </summary>
    public class ImageStyleChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 源图片目录
        /// </summary>
        public string SourceIamgeDir { get; set; }
        /// <summary>
        /// 图标转换风格后的保存目录
        /// </summary>
        public string TargetIamgeDir { get; set; }
        /// <summary>
        /// 图片色彩风格类型
        /// </summary>
        public EImageColorType ColorType { get; set; }


        /// <summary>
        /// 为图片色彩样式改变事件<see cref="IamgeColorfulControl.ImageColorStyleItemChanged"/>提供参数
        /// </summary>
        public ImageStyleChangedEventArgs()
        {
        }
        /// <summary>
        /// 为图片色彩样式改变事件<see cref="IamgeColorfulControl.ImageColorStyleItemChanged"/>提供参数
        /// </summary>
        /// <param name="sourceIamgeDir">源图片目录</param>
        /// <param name="targetIamgeDir">图标转换风格后的保存目录</param>
        /// <param name="colorType">图片色彩风格类型</param>
        public ImageStyleChangedEventArgs(string sourceIamgeDir, string targetIamgeDir, EImageColorType colorType)
        {
            SourceIamgeDir = sourceIamgeDir;
            TargetIamgeDir = targetIamgeDir;
            ColorType = colorType;
        }
    }
}
