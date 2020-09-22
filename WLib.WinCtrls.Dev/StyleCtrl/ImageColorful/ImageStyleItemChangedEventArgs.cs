using System;
using WLib.Drawing;

namespace WLib.WinCtrls.Dev.StyleCtrl.ImageColorful
{
    /// <summary>
    /// 为单个图片色彩样式改变事件<see cref="IamgeColorfulControl.ImageColorStyleItemChanged"/>提供参数
    /// </summary>
    public class ImageStyleItemChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 源图片路径
        /// </summary>
        public string SourcePath { get; set; }
        /// <summary>
        /// 图标转换风格后的保存路径
        /// </summary>
        public string TargetPath { get; set; }
        /// <summary>
        /// 图片色彩风格类型
        /// </summary>
        public EImageColorType ColorType { get; set; }


        /// <summary>
        /// 为单个图片色彩样式改变事件<see cref="IamgeColorfulControl.ImageColorStyleItemChanged"/>提供参数
        /// </summary>
        public ImageStyleItemChangedEventArgs()
        {
        }
        /// <summary>
        /// 为单个图片色彩样式改变事件<see cref="IamgeColorfulControl.ImageColorStyleItemChanged"/>提供参数
        /// </summary>
        /// <param name="sourcePath">源图片路径</param>
        /// <param name="targetPath">修改后的图片保存路径</param>
        /// <param name="iconColorType">图片色彩风格类型</param>
        public ImageStyleItemChangedEventArgs(string sourcePath, string targetPath, EImageColorType iconColorType)
        {
            SourcePath = sourcePath;
            TargetPath = targetPath;
            ColorType = iconColorType;
        }
    }
}
