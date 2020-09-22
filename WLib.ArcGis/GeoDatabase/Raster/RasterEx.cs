/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using System.Runtime.InteropServices;
using WLib.ArcGis.GeoDatabase.WorkSpace;

namespace WLib.ArcGis.GeoDatabase.Raster
{
    /// <summary>
    /// 提供操作栅格数据的方法
    /// </summary>
    public static partial class RasterEx
    {
        /// <summary>
        /// 打开栅格数据集，找不到则返回null
        /// </summary>
        /// <param name="dir">>栅格数据集所在目录</param>
        /// <param name="name">栅格数据集名称</param>
        /// <returns></returns>
        public static IRasterDataset GetRasterDataset(string dir, string name)
        {
            var workspace = WorkspaceEx.GetWorkSpace(dir, EWorkspaceType.Raster);
            var dataset = WorkspaceEx.GetDataset(workspace, esriDatasetType.esriDTRasterDataset, name);
            Marshal.ReleaseComObject(workspace);
            return dataset == null ? null : dataset as IRasterDataset;
        }
        /// <summary>
        /// 打开栅格数据集，找不到则返回null
        /// </summary>
        /// <param name="workspace"></param>
        /// <param name="name">栅格数据集名称</param>
        /// <returns></returns>
        public static IRasterDataset GetRasterDataset(this IWorkspace workspace, string name)
        {
            var dataset = WorkspaceEx.GetDataset(workspace, esriDatasetType.esriDTRasterDataset, name);
            return dataset == null ? null : dataset as IRasterDataset;
        }
        /// <summary>
        /// 获取栅格图层
        /// </summary>
        /// <param name="rasterPath">栅格数据文件的路径（bmp/tif/jpg/img）</param>
        /// <returns></returns>
        public static IRasterLayer GetRasterLayer(string rasterPath)
        {
            return FromPath(rasterPath).CreateToLayer();
        }
        /// <summary>
        /// 获取栅格图层
        /// </summary>
        /// <param name="workspace"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IRasterLayer GetRasterLayer(this IWorkspace workspace, string name)
        {
            return GetRasterDataset(workspace, name).CreateToLayer();
        }


        /// <summary>
        /// 打开栅格数据（bmp/tif/jpg/img）
        /// </summary>
        /// <param name="rasterPath">栅格数据文件的路径（bmp/tif/jpg/img）</param>
        /// <returns></returns>
        public static IRaster GetRaster(string rasterPath)
        {
            return FromPath(rasterPath).CreateDefaultRaster();
        }
        /// <summary>
        /// 创建影像金字塔
        /// </summary>
        /// <param name="rasterDataset"></param>
        public static void CreateRasterPyramid(this IRasterDataset rasterDataset)
        {
            if (rasterDataset is IRasterPyramid3 rasterPyrmid)
            {
                if (!rasterPyrmid.Present)
                    rasterPyrmid.Create();//创建金字塔
            }
        }
        /// <summary>
        /// 根据当前栅格数据集创建栅格图层
        /// </summary>
        /// <param name="rasterDataset"></param>
        /// <returns></returns>
        public static IRasterLayer CreateToLayer(this IRasterDataset rasterDataset)
        {
            IRasterLayer rasterLayer = new RasterLayerClass();
            rasterLayer.CreateFromDataset(rasterDataset);
            return rasterLayer;
        }
    }
}
