/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/1/1
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.GeoDb.WorkSpace;
using static System.IO.Path;

namespace WLib.ArcGis.GeoDb.FeatClass
{
    /// <summary>
    /// 提供从指定路径获取要素类的方法
    /// </summary>
    public static class FeatClassFromPath
    {
        /// <summary>
        /// 从指定路径（或连接字符串）中获取要素类。
        /// ①shp文件路径，返回该shp存储的要素类；
        /// ②mdb文件路径，返回该mdb数据库第一个要素类；
        /// ③shp目录，返回目录下第一个shp文件存储的要素类；
        /// ④gdb目录，返回gdb数据库第一个要素类；
        /// ⑤mdb文件路径[\DatasetName]\FeatureClassName，返回mdb数据库中指定名称或别名的要素类；
        /// ⑥gdb目录[\DatasetName]\FeatureClassName，返回gdb数据库中指定名称或别名的要素类；
        /// ⑦sde或oleDb或sql连接字符串，返回数据库中的第一个要素类；
        /// </summary>
        /// <param name="connStrOrPath">路径或连接字符串</param>
        /// <returns></returns>
        public static IFeatureClass FromPath(string connStrOrPath)
        {
            if (GetWorkspace.IsConnectionString(connStrOrPath))
                return FirstFromConnString(connStrOrPath);

            var path = connStrOrPath.ToLower();
            if (System.IO.Directory.Exists(path))
                return path.EndsWith(".gdb") ? FirstFromGdb(path) : FirstFromShpDir(path);

            if (System.IO.File.Exists(path))
            {
                var extension = GetExtension(path);
                if (extension == ".shp") return FromShpFile(path);
                if (extension == ".mdb") return FirstFromMdb(path);
            }
            else
            {
                string workspacePath = null;
                if (path.Contains(".gdb"))
                    workspacePath = path.Substring(0, path.LastIndexOf(".gdb", StringComparison.OrdinalIgnoreCase) + 4);
                else if (path.Contains(".mdb"))
                    workspacePath = path.Substring(0, path.LastIndexOf(".mdb", StringComparison.OrdinalIgnoreCase) + 4);

                if (!string.IsNullOrWhiteSpace(workspacePath))
                {
                    var workspace = GetWorkspace.GetWorkSpace(workspacePath);
                    if (workspace == null)
                        throw new Exception($"无法按照指定路径或连接字符串“{workspacePath}”打开工作空间！");

                    var subPath = path.Replace(workspacePath, "");
                    if (!subPath.Contains(DirectorySeparatorChar.ToString()) && !subPath.Contains(AltDirectorySeparatorChar.ToString()))
                        return workspace.GetFirstFeatureClass();

                    //按照"\"或者"/"分割子路径，获得要素集名称、要素类名称
                    var names = subPath.Split(new[] { DirectorySeparatorChar, AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
                    if (names.Length == 1)
                        return workspace.GetFeatureClassByName(names[0]);
                    if (names.Length == 2)
                        return workspace.GetFeatureDataset(names[0]).GetFeatureClassByName(names[1]);
                }
            }
            return null;
        }


        /// <summary>
        /// 获取指定shp文件存储的要素类
        /// </summary>
        /// <param name="shpPath"></param>
        /// <returns></returns>
        public static IFeatureClass FromShpFile(string shpPath)
        {
            var dir = GetDirectoryName(shpPath);
            var fileName = GetFileNameWithoutExtension(shpPath);
            return GetWorkspace.GetWorkSpace(dir, EWorkspaceType.ShapeFile).GetFeatureClassByName(fileName);
        }
        /// <summary>
        /// 获取指定shp目录存储的全部要素类
        /// </summary>
        /// <param name="shpDir"></param>
        /// <returns></returns>
        public static List<IFeatureClass> FromShpDir(string shpDir)
        {
            return GetWorkspace.GetWorkSpace(shpDir, EWorkspaceType.ShapeFile).GetFeatureClasses();
        }
        /// <summary>
        /// 获取指定mdb数据库存储的全部要素类
        /// </summary>
        /// <param name="mdbPath"></param>
        /// <returns></returns>
        public static List<IFeatureClass> FromMdb(string mdbPath)
        {
            return GetWorkspace.GetWorkSpace(mdbPath, EWorkspaceType.Access).GetFeatureClasses();
        }
        /// <summary>
        /// 获取指定gdb数据库存储的全部要素类
        /// </summary>
        /// <param name="gdbPath"></param>
        /// <returns></returns>
        public static List<IFeatureClass> FromGdb(string gdbPath)
        {
            return GetWorkspace.GetWorkSpace(gdbPath, EWorkspaceType.FileGDB).GetFeatureClasses();
        }
        /// <summary>
        /// 获取指定连接字符串对应数据库存储的全部要素类
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <returns></returns>
        public static List<IFeatureClass> FromConnString(string connString)
        {
            return GetWorkspace.GetWorkSpace(connString).GetFeatureClasses();
        }


        /// <summary>
        /// 获取指定shp目录存储的第一个要素类
        /// </summary>
        /// <param name="shpDir"></param>
        /// <returns></returns>
        public static IFeatureClass FirstFromShpDir(string shpDir)
        {
            return GetWorkspace.GetWorkSpace(shpDir, EWorkspaceType.ShapeFile).GetFirstFeatureClass();
        }
        /// <summary>
        /// 获取指定mdb数据库存储的第一个要素类
        /// </summary>
        /// <param name="mdbPath"></param>
        /// <returns></returns>
        public static IFeatureClass FirstFromMdb(string mdbPath)
        {
            return GetWorkspace.GetWorkSpace(mdbPath, EWorkspaceType.Access).GetFirstFeatureClass();
        }
        /// <summary>
        /// 获取指定gdb数据库存储的第一个要素类
        /// </summary>
        /// <param name="gdbPath"></param>
        /// <returns></returns>
        public static IFeatureClass FirstFromGdb(string gdbPath)
        {
            return GetWorkspace.GetWorkSpace(gdbPath, EWorkspaceType.FileGDB).GetFirstFeatureClass();
        }
        /// <summary>
        /// 获取指定连接字符串对应数据库存储的第一个要素类
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <returns></returns>
        public static IFeatureClass FirstFromConnString(string connString)
        {
            return GetWorkspace.GetWorkSpace(connString).GetFirstFeatureClass();
        }
    }
}
