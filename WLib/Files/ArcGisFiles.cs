/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;

namespace WLib.Files
{
    /// <summary>
    /// 提供ArcGIS相关文件(shp/mxd等)的增删改查操作
    /// </summary>
    public class ArcGisFiles
    {
        /// <summary>
        /// Shapefile相交的文件扩展名（包括.shp .shx .dbf .prj .sbn .sbx等等）
        /// </summary>
        public static string[] ShpExtensions = { ".shp", ".shx", ".dbf", ".prj", ".sbn", ".sbx", ".fbn", ".fbx", ".ain", ".aih", ".ixs", ".mxs", ".atx", ".cpg", ".shp.xml" };
        /// <summary>
        /// 地图文档相关文件扩展名（包括.mxd .pmf .mxt）
        /// </summary>
        public static string[] MapExtensions = { ".mxd", ".pmf", ".mxt" };


        /// <summary>
        /// 删除目录下的所有ArcGIS地图文件（shapeFile和mxd）
        /// </summary>
        /// <param name="dir"></param>
        public static void DeleteFromPath(string dir)
        {
            string[] files = System.IO.Directory.GetFiles(dir);
            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    if (System.IO.File.Exists(files[i]))
                    {
                        string type = System.IO.Path.GetExtension(files[i]);
                        if (ShpExtensions.Contains(type.ToLower()))//检查shp文件
                            System.IO.File.Delete(files[i]);
                        else if (MapExtensions.Contains(type.ToLower()))//检查mxd文件
                            System.IO.File.Delete(files[i]);
                    }
                }
                catch { }
            }
        }
        /// <summary>
        /// 删除单个Shapefile文件
        /// （同时删除关联文件".shp", ".shx", ".dbf", ".prj", ".sbn", ".sbx",".shp.xml"等）
        /// </summary>
        /// <param name="filePath">文件全路径</param>
        public static void DeleteFileFormPath(string filePath)
        {
            var fileDir = System.IO.Path.GetDirectoryName(filePath);
            var fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
            for (int i = 0; i < ShpExtensions.Length; i++)
            {
                System.IO.File.Delete(System.IO.Path.Combine(fileDir, fileName + ShpExtensions[i]));
            }
        }
        /// <summary>
        /// 获取指定shp文件关联的shp文件组（包括".shp", ".shx", ".dbf", ".prj", ".sbn", ".sbx",".shp.xml"等）
        /// </summary>
        /// <param name="filePath">shp文件全路径</param>
        /// <returns></returns>
        public static string[] GetShpFileFromPath(string filePath)
        {
            var filePaths = new List<string>();
            var fileDir = System.IO.Path.GetDirectoryName(filePath);
            var fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
            for (int i = 0; i < ShpExtensions.Length; i++)
            {
                var path = System.IO.Path.Combine(fileDir, fileName + ShpExtensions[i]);
                if (System.IO.File.Exists(path))
                    filePaths.Add(path);
            }
            return filePaths.ToArray();
        }
    }
}
