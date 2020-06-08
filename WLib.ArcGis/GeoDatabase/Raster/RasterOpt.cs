/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.GeoDatabase.WorkSpace;

namespace WLib.ArcGis.GeoDatabase.Raster
{
    /// <summary>
    /// 提供操作栅格数据集的方法
    /// </summary>
    public static class RasterOpt
    {
        /// <summary>
        /// 打开栅格数据集（bmp/tif/jpg/img）
        /// </summary>
        /// <param name="rasterPath">栅格数据文件的路径（bmp/tif/jpg/img）</param>
        /// <returns>返回栅格数据集，路径有误或找不到时抛出异常</returns>
        /// <exception cref="System.Runtime.InteropServices.COMException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IRasterDataset GetRasterDataset(string rasterPath)
        {
            var dir = System.IO.Path.GetDirectoryName(rasterPath);  //获取文件路径
            var name = System.IO.Path.GetFileName(rasterPath);      //获取栅格文件名

            var workspace = WorkspaceEx.GetWorkSpace(dir, EWorkspaceType.Raster);
            var rasterWorkspace = workspace as IRasterWorkspace;
            if (rasterWorkspace == null)
                throw new ArgumentException($"指定的工作空间“{dir}”不是栅格数据集工作空间！");

            return rasterWorkspace.OpenRasterDataset(name);
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
            var rasterDataset = GetRasterDataset(rasterPath);
            IRasterLayer rasterLayer = new RasterLayerClass();
            rasterLayer.CreateFromDataset(rasterDataset);
            return rasterLayer;
        }
        /// <summary>
        /// 获取栅格图层
        /// </summary>
        /// <param name="workspace"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IRasterLayer GetRasterLayer(this IWorkspace workspace, string name)
        {
            var rasterDataset = GetRasterDataset(workspace, name);
            IRasterLayer rasterLayer = new RasterLayerClass();
            rasterLayer.CreateFromDataset(rasterDataset);
            return rasterLayer;
        }

        /// <summary>
        /// 打开栅格数据（bmp/tif/jpg/img）
        /// </summary>
        /// <param name="rasterPath">栅格数据文件的路径（bmp/tif/jpg/img）</param>
        /// <returns></returns>
        public static IRaster GetRaster(string rasterPath)
        {
            var rasterDataset = GetRasterDataset(rasterPath);
            return rasterDataset.CreateDefaultRaster();
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
    }
}
