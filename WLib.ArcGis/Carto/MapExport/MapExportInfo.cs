/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/4 14:19:24
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using WLib.ArcGis.Carto.MapExport.Base;

namespace WLib.ArcGis.Carto.MapExport
{
    /// <summary>
    /// 对地图进行各项配置和出图的信息
    /// </summary>
    [Serializable]
    public class MapExportInfo
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        public string CfgName { get; set; }
        /// <summary>
        /// 出图文档模板路径
        /// </summary>
        public string TemplateMxdPath { get; set; }
        /// <summary>
        /// 导出目录
        /// </summary>
        public string ExportDirectory { get; set; }
        /// <summary>
        /// 导出文件名（不含扩展名）
        /// </summary>
        public string ExportFileName { get; set; }
        /// <summary>
        /// 是否导出mxd地图文档，默认True
        /// </summary>
        public bool IsExportMxd { get; set; } = true;
        /// <summary>
        /// 导出mxd地图文档时，是否覆盖已有地图文档，默认True
        /// </summary>
        public bool Overwrite { get; set; } = true;
       

        /// <summary>
        /// 将地图导出为哪些类型图片的配置
        /// </summary>
        public ExportPicInfoCollection ExportPictures { get; set; }
        /// <summary>
        /// 出图文档需要修改的元素
        /// </summary>
        public ElementInfoCollection Elements { get; set; }
        /// <summary>
        /// 出图文档中包含的地图框（地图）
        /// </summary>
        public MapFrameInfoCollection MapFrames { get; set; }
        /// <summary>
        /// 出图文档中包含的表格
        /// </summary>
        public TableInfoCollection Tables { get; set; }


        /// <summary>
        /// 对地图进行各项配置和出图的信息
        /// </summary>
        public MapExportInfo()
        {
            ExportPictures = new ExportPicInfoCollection();
            Elements = new ElementInfoCollection();
            MapFrames = new MapFrameInfoCollection();
        }
    }
}
