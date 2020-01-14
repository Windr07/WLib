using System.Drawing;

namespace WLib.Drawing
{
    /// <summary>
    /// 提供获取位图的操作
    /// </summary>
    public static class GetBitmap
    {
        /// <summary>
        /// 从指定路径中获取位图（<see cref="Bitmap"/>），自动释放对图片文件占用
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Bitmap FromFile(string path)
        {
            var image = Image.FromFile(path);
            var bitmap = new Bitmap(image);
            image.Dispose();
            return bitmap;
        }
    }
}
