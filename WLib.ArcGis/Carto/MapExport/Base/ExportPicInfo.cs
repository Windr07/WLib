/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/4 14:24:19
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.ArcGis.Carto.MapExport.Base
{
    /// <summary>
    /// 将地图导出为图片的配置
    /// </summary>
    [Serializable]
    public class ExportPicInfo
    {
        /// <summary>
        /// 导出的图片的分辨率
        /// </summary>
        public double Dpi { get; set; }
        /// <summary>
        /// 导出的图片是否覆盖同名文件
        /// </summary>
        public bool IsOverwrite { get; set; }
        /// <summary>
        /// 导出的图片格式
        /// （.jpg,.tiff,.bmp,.emf,.png,.gif,.pdf,.eps,.ai,.svg等）
        /// </summary>
        public string PicExtension { get; set; }


        /// <summary>
        /// 将地图导出为图片的配置
        /// </summary>
        public ExportPicInfo()
        {
            
        }
        /// <summary>
        /// 将地图导出为图片的配置
        /// </summary>
        /// <param name="picExtension">导出的图片格式</param>
        /// <param name="dpi">导出的图片的分辨率</param>
        /// <param name="isOverWrite">导出的图片是否覆盖同名文件</param>
        public ExportPicInfo(string picExtension, double dpi, bool isOverWrite)
        {
            PicExtension = picExtension;
            Dpi = dpi;
            IsOverwrite = isOverWrite;
        }
    }
}
