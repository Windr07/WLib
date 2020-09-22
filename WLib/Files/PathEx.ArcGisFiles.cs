/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WLib.Files
{
    /// <summary>
    /// 提供ArcGIS相关文件(shp/mxd等)的增删改查操作
    /// </summary>
    public static partial class PathEx
    {
        /// <summary>
        /// Shapefile相关的文件扩展名（包括.shp .shx .dbf .prj .sbn .sbx等等）
        /// </summary>
        public static string[] ShpExtensions = { ".shp", ".shx", ".dbf", ".prj", ".sbn", ".sbx", ".fbn", ".fbx", ".ain", ".aih", ".ixs", ".mxs", ".atx", ".cpg", ".shp.xml" };
        /// <summary>
        /// 地图文档相关文件扩展名（包括.mxd .pmf .mxt）
        /// </summary>
        public static string[] MapExtensions = { ".mxd", ".pmf", ".mxt" };


        /// <summary>
        /// 删除目录下的所有shapeFile文件（包括shp相关文件.shp, .shx, .dbf, .prj, .sbx, .shp.xml等）
        /// </summary>
        /// <param name="dir"></param>
        public static void DeleteShpFromDir(string dir, bool deleteFailException = false)
        {
            var filePaths = Directory.GetFiles(dir);
            foreach (var filePath in filePaths)
            {
                try
                {
                    var extension = Path.GetExtension(filePath).ToLower();
                    if (ShpExtensions.Contains(extension))//检查shp文件
                        File.Delete(filePath);
                }
                catch (Exception ex) { if (deleteFailException) throw new Exception($"删除文件“{filePath}”失败：" + ex.Message); }
            }
        }
        /// <summary>
        /// 删除目录下的所有地图文档文件（包括.mxd, .pmf, .mxt ）
        /// </summary>
        /// <param name="dir"></param>
        public static void DeleteMxdFromDir(string dir, bool deleteFailException = false)
        {
            var filePaths = Directory.GetFiles(dir);
            foreach (var filePath in filePaths)
            {
                try
                {
                    var extension = Path.GetExtension(filePath).ToLower();
                    if (MapExtensions.Contains(extension))//检查mxd文件
                        File.Delete(filePath);
                }
                catch (Exception ex) { if (deleteFailException) throw new Exception($"删除文件“{filePath}”失败：" + ex.Message); }
            }
        }


        /// <summary>
        /// 删除单个Shapefile文件
        /// （同时删除关联文件".shp", ".shx", ".dbf", ".prj", ".sbn", ".sbx",".shp.xml"等）
        /// </summary>
        /// <param name="shpFilePath">shp文件全路径</param>
        public static void DeleteShpFiles(string shpFilePath)
        {
            var fileDir = Path.GetDirectoryName(shpFilePath);
            var fileName = Path.GetFileNameWithoutExtension(shpFilePath);
            foreach (var extension in ShpExtensions)
                File.Delete(Path.Combine(fileDir, fileName + extension));
        }
        /// <summary>
        /// 获取指定shp文件关联的shp文件组（包括".shp", ".shx", ".dbf", ".prj", ".sbn", ".sbx",".shp.xml"等）
        /// </summary>
        /// <param name="shpFilePath">shp文件全路径</param>
        /// <returns></returns>
        public static string[] GetShpFiles(string shpFilePath)
        {
            var filePaths = new List<string>();
            var fileDir = Path.GetDirectoryName(shpFilePath);
            var fileName = Path.GetFileNameWithoutExtension(shpFilePath);
            foreach (var extension in ShpExtensions)
            {
                var path = Path.Combine(fileDir, fileName + extension);
                if (File.Exists(path))
                    filePaths.Add(path);
            }
            return filePaths.ToArray();
        }
    }
}
