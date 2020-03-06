/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2/23
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using DevExpress.Utils;
using System.Drawing.Imaging;
using System.IO;

namespace WLib.WinCtrls.Dev.Extension
{
    /// <summary>
    /// <see cref="ImageCollection"/>控件的扩展功能
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
                imageInfo.Image.Save(Path.Combine(saveDirectory, imageInfo.Name + "." + ImageFormat.Png));
        }
    }
}
