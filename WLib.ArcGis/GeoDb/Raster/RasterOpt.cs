/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.GeoDb.WorkSpace;

namespace WLib.ArcGis.GeoDb.Raster
{
    public static class RasterOpt
    {
        /// <summary>
        /// 打开栅格数据集（bmp/tif/jpg/img）
        /// </summary>
        /// <param name="rasterPath">栅格数据文件的路径（bmp/tif/jpg/img）</param>
        /// <returns></returns>
        public static IRasterDataset GetRasterDataset(string rasterPath)
        {
            var dir = System.IO.Path.GetDirectoryName(rasterPath);  //获取文件路径
            var name = System.IO.Path.GetFileName(rasterPath);      //获取栅格文件名

            var workspace = GetWorkspace.GetWorkSpace(dir, EWorkspaceType.Raster);
            var rasterWorkspace = workspace as IRasterWorkspace;
            if (rasterWorkspace == null)
                throw new Exception($"指定的工作空间“{dir}”不是栅格数据集工作空间！");

            return rasterWorkspace.OpenRasterDataset(name);
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
