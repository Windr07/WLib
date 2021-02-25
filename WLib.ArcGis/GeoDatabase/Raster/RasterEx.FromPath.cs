/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020/06/11
// desc： None
// mdfy:  None
// sorc:  https://gitee.com/windr07/WLib
//        https://github.com/Windr07/WLib
//----------------------------------------------------------------*/

using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        /// 从指定路径（或连接字符串）中获取栅格数据集
        /// <para>①bmp、tif、tiff、jpg、img文件路径：返回该文件（组）存储的栅格数据集；</para>
        /// <para>②mdb路径：返回该mdb数据库第一个栅格数据集；</para>
        /// <para>③gdb目录：返回gdb数据库第一个栅格数据集；</para>
        /// <para>④mdb文件路径\DatasetName：返回mdb数据库中指定名称的栅格数据集；</para>
        /// <para>⑤gdb目录\DatasetName：返回gdb数据库中指定名称的栅格数据集；</para>
        /// <para>⑥sde或oleDb或sql连接字符串：返回数据库中的第一个栅格数据集；</para>
        /// </summary>
        /// <param name="connStrOrPath">路径或连接字符串</param>
        /// <returns></returns>
        public static IRasterDataset FromPath(string connStrOrPath)
        {
            if (WorkspaceEx.IsConnectionString(connStrOrPath))
                return FirstFromConnString(connStrOrPath);

            if (Directory.Exists(connStrOrPath))
                return FirstFromDir(connStrOrPath);

            if (File.Exists(connStrOrPath))
                return FirstFromFile(connStrOrPath);

            return FirstFromFullPath(connStrOrPath);
        }
        /// <summary>
        /// 从指定路径（或连接字符串）中获取全部栅格数据集
        /// <para>①mdb路径：返回该mdb数据库的全部栅格数据集；</para>
        /// <para>②普通目录：返回目录下的栅格数据集；</para>
        /// <para>③gdb目录：返回gdb数据库的全部栅格数据集；</para>
        /// <para>④mdb文件路径\DatasetName：返回mdb数据库中的指定栅格数据集；</para>
        /// <para>⑤gdb目录\DatasetName：返回gdb数据库中的指定栅格数据集；</para>
        /// <para>⑥sde或oleDb或sql连接字符串：返回数据库的全部栅格数据集；</para>
        /// </summary>
        /// <param name="connStrOrPath">路径或连接字符串</param>
        /// <returns></returns>
        public static List<IRasterDataset> AllFromPath(string connStrOrPath)
        {
            if (WorkspaceEx.IsConnectionString(connStrOrPath))
                return FromConnString(connStrOrPath);

            if (Directory.Exists(connStrOrPath))
                return FromDir(connStrOrPath);

            if (File.Exists(connStrOrPath))
                return FromFile(connStrOrPath);

            return FromFullPath(connStrOrPath);
        }


        #region  私有方法 - 获取路径或数据库下的第一个栅格数据集
        /// <summary>
        /// 通过连接字符串连接数据库，获得数据库中的第一个栅格数据集
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <returns>返回栅格数据集，若工作空间没有栅格数据集则返回null</returns>
        private static IRasterDataset FirstFromConnString(string connString)
        {
            return connString.ToWorkspace(workspace => workspace.GetFirstDataset(esriDatasetType.esriDTRasterDataset) as IRasterDataset);
        }
        /// <summary>
        /// 从目录中打开找到的第一个栅格数据集，包括以下情况：
        /// <para>①从包含bmp/tif/jpg/img等文件的普通目录中，获取第一个栅格数据集</para>
        /// <para>②从gdb中获取第一个栅格数据集</para>
        /// </summary>
        /// <param name="rasterPath">gdb目录或普通目录</param>
        /// <returns>返回栅格数据集，路径有误或找不到时抛出异常</returns>
        private static IRasterDataset FirstFromDir(string dir)
        {
            var extension = Path.GetExtension(dir).ToLower();
            var workspace = extension == ".gdb"
                 ? WorkspaceEx.GetWorkSpace(dir, EWorkspaceType.FileGDB)
                 : WorkspaceEx.GetWorkSpace(dir, EWorkspaceType.Raster);

            var dataset = workspace.GetFirstDataset(esriDatasetType.esriDTRasterDataset);
            Marshal.ReleaseComObject(workspace);
            return dataset == null ? null : dataset as IRasterDataset;
        }
        /// <summary>
        /// 从文件中打开栅格数据集，包括以下情况：
        /// <para>①从bmp/tif/jpg/img文件中打开栅格数据集</para>
        /// <para>②从mdb数据库中获取第一个栅格数据集</para>
        /// </summary>
        /// <param name="rasterPath">bmp/tif/jpg/img等栅格数据文件的路径，或者mdb数据库路径</param>
        /// <returns>返回栅格数据集</returns>
        private static IRasterDataset FirstFromFile(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();
            if (extension == ".mdb")
                return filePath.ToWorkspace(workspace => workspace.GetFirstDataset(esriDatasetType.esriDTRasterDataset) as IRasterDataset);
            else
                return FromRasterFile(filePath);
        }
        /// <summary>
        /// 从bmp/tif/jpg/img等类型的栅格文件中打开栅格数据集
        /// </summary>
        /// <param name="rasterPath">bmp/tif/jpg/img等栅格数据文件的路径</param>
        /// <returns>返回栅格数据集，路径有误或找不到时抛出异常</returns>
        private static IRasterDataset FromRasterFile(string rasterPath)
        {
            var dir = Path.GetDirectoryName(rasterPath);  //获取文件路径
            var name = Path.GetFileName(rasterPath);      //获取栅格文件名

            var workspace = WorkspaceEx.GetWorkSpace(dir, EWorkspaceType.Raster);
            var rasterWorkspace = workspace as IRasterWorkspace;
            if (rasterWorkspace == null)
                throw new ArgumentException($"指定的工作空间“{dir}”不是栅格数据集工作空间！");

            var rasterDataset = rasterWorkspace.OpenRasterDataset(name);
            Marshal.ReleaseComObject(rasterWorkspace);
            return rasterDataset;
        }
        /// <summary>
        /// 获取指定全路径下的栅格数据集，全路径形式为“工作空间路径\栅格数据集名称”
        /// </summary>
        /// <param name="fullPath">格式为“工作空间路径\栅格数据集名称”的路径</param>
        /// <returns></returns>
        private static IRasterDataset FirstFromFullPath(string fullPath)
        {
            fullPath = fullPath.ToLower();
            string workspacePath = null;
            foreach (var extension in new[] { ".gdb", ".mdb" })
            {
                int index;
                if ((index = fullPath.LastIndexOf(extension, StringComparison.OrdinalIgnoreCase)) > -1)
                {
                    workspacePath = fullPath.Substring(0, index + 4);
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(workspacePath)) return null;

            return workspacePath.ToWorkspace(workspace =>
             {
                 var subPath = fullPath.Replace(workspacePath, "");
                 var names = subPath.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
                 if (names.Length == 1) return workspace.GetRasterDataset(names[0]);
                 else throw new Exception($"无法按照指定路径或连接字符串“{workspacePath}”获取栅格数据集，请验证路径正确性！");
             });
        }
        #endregion


        #region  私有方法 - 获取路径或数据库下的全部栅格数据集
        /// <summary>
        /// 获取指定连接字符串对应数据库存储的全部栅格数据集
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        private static List<IRasterDataset> FromConnString(string connString)
        {
            return connString.ToWorkspace(workspace => workspace.GetDatasets(esriDatasetType.esriDTRasterDataset).Cast<IRasterDataset>().ToList());
        }
        /// <summary>
        /// 获取指定目录下存储的全部栅格数据集
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private static List<IRasterDataset> FromDir(string dir)
        {
            var workspaceType = dir.EndsWith(".gdb") ? EWorkspaceType.FileGDB : EWorkspaceType.Raster;
            return dir.ToWorkspace(workspace =>
                workspace.GetDatasets(esriDatasetType.esriDTRasterDataset).Cast<IRasterDataset>().ToList(), workspaceType);
        }
        /// <summary>
        /// 获取指定文件中存储的全部栅格数据集
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        private static List<IRasterDataset> FromFile(string filePath)
        {
            var extension = Path.GetExtension(filePath.ToLower());
            if (extension == ".mdb")
                return filePath.ToWorkspace(workspace => workspace.GetDatasets(esriDatasetType.esriDTRasterDataset).Cast<IRasterDataset>().ToList());
            else
                return new[] { FromRasterFile(filePath) }.ToList();
        }
        /// <summary>
        /// 获取指定全路径下的栅格数据集，全路径形式为：“工作空间路径\要素集名称”
        /// </summary>
        /// <param name="fullPath">格式为“工作空间路径\要素集名称”的路径</param>
        /// <returns></returns>
        private static List<IRasterDataset> FromFullPath(string fullPath)
        {
            fullPath = fullPath.ToLower();
            string workspacePath = null;
            foreach (var extension in new[] { ".gdb", ".mdb" })
            {
                int index;
                if ((index = fullPath.LastIndexOf(extension, StringComparison.OrdinalIgnoreCase)) > -1)
                {
                    workspacePath = fullPath.Substring(0, index + 4);
                    break;
                }
            }

            var results = new List<IRasterDataset>();
            if (!string.IsNullOrWhiteSpace(workspacePath))
            {
                var rasterDataset = workspacePath.ToWorkspace(workspace =>
                {
                    var subPath = fullPath.Replace(workspacePath, "");
                    var names = subPath.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
                    if (names.Length == 1)
                    {
                        var dataset = workspace.GetDataset(esriDatasetType.esriDTRasterDataset, names[0]);
                        return dataset == null ? null : dataset as IRasterDataset;
                    }
                    else throw new Exception($"无法按照指定路径或连接字符串“{workspacePath}”获取栅格数据集，请验证路径正确性！");
                });
                if (rasterDataset != null)
                    results.Add(rasterDataset);
            }
            return results;

        }
        #endregion
    }
}
