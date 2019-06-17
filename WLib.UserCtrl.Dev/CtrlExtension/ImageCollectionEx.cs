using System.Drawing.Imaging;
/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2/23
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.IO;
using DevExpress.Utils;

namespace WLib.WinCtrls.Dev.CtrlExtension
{
    /// <summary>
    /// <see cref="DevExpress.Utils.ImageCollection"/>控件的扩展功能
    /// </summary>
    public static class ImageCollectionEx
    {
        /// <summary>
        /// 将DevExpress.Utils.ImageCollection控件存储的图片导出到指定目录中
        /// </summary>
        /// <param name="imgColl">DevExpress.Utils.ImageCollection控件</param>
        /// <param name="saveDirectory">图片保存目录</param>
        public static void ExportImageCollectionImages(this ImageCollection imgColl, string saveDirectory)
        {
            Directory.CreateDirectory(saveDirectory);
            Images images = imgColl.Images;
            InnerImagesList innerImagesList = images.InnerImages;
            foreach (ImageInfo imageInfo in innerImagesList)
            {
                imageInfo.Image.Save(saveDirectory + imageInfo.Name, ImageFormat.Png);
            }
        }
    }
}
