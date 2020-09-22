/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/5 14:04:55
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace WLib.ArcGis.Carto.MapExport.Base
{
    /// <summary>
    /// 将地图导出为图片的配置集合
    /// </summary>
    [Serializable]
    public class ExportPicInfoCollection : List<ExportPicInfo>
    {
        /// <summary>
        /// 将地图导出为图片的配置集合
        /// </summary>
        public ExportPicInfoCollection() { }
        /// <summary>
        /// 将地图导出为图片的配置集合
        /// </summary>
        /// <param name="collection"></param>
        public ExportPicInfoCollection(IEnumerable<ExportPicInfo> collection) : base(collection) { }
        
        /// <summary>
        /// 获取指定图片格式的图片导出配置信息
        /// </summary>
        /// <param name="picExtension">图片格式</param>
        /// <returns></returns>
        public ExportPicInfo this[string picExtension] => this.First(v => v.PicExtension == picExtension);
        /// <summary>
        /// 添加一个或多个导出图片配置
        /// </summary>
        /// <param name="dpi">导出的图片的分辨率</param>
        /// <param name="overwrite">导出的图片是否覆盖同名文件</param>
        /// <param name="extensions">导出的图片一种或多种格式</param>
        public void Add(double dpi, bool overwrite, params string[] extensions)
        {
            foreach (var extension in extensions)
                Add(new ExportPicInfo(extension, dpi, overwrite));
        }
    }
}
