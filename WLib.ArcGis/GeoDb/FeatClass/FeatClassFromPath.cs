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
        /// <param name="strConnOrPath">路径或连接字符串</param>
        /// <returns></returns>
        public static IFeatureClass FromPath(string strConnOrPath)
        {
            if (System.IO.Directory.Exists(strConnOrPath))
            {
                return strConnOrPath.EndsWith(".gdb") ? FirstFromGdb(strConnOrPath) : FirstFromShpDir(strConnOrPath);
            }
            else if (System.IO.File.Exists(strConnOrPath))
            {
                var extension = System.IO.Path.GetExtension(strConnOrPath);
                if (extension == ".shp")
                    return FromShpFile(strConnOrPath);
                else if (extension == ".mdb")
                    return FirstFromMdb(strConnOrPath);
            }
            else
            {
                string workspacePath = null;
                if (strConnOrPath.Contains(".gdb"))
                    workspacePath = strConnOrPath.Substring(0, strConnOrPath.LastIndexOf(".gdb", StringComparison.Ordinal));
                else if (strConnOrPath.Contains(".mdb"))
                    workspacePath = strConnOrPath.Substring(0, strConnOrPath.LastIndexOf(".mdb", StringComparison.Ordinal));
                else if (GetWorkspace.IsConnectionString(strConnOrPath))
                    workspacePath = strConnOrPath;

                if (!string.IsNullOrWhiteSpace(workspacePath))
                {
                    var workspace = GetWorkspace.GetWorkSpace(workspacePath);
                    if (workspace == null)
                        throw new Exception($"无法按照指定路径或连接字符串“{workspacePath}”打开工作空间！");

                    var subPath = strConnOrPath.Replace(workspacePath, "");
                    if (!subPath.Contains("\\"))
                        workspace.GetFirstFeatureClass();
                    var names = subPath.Split('\\');
                    if (names.Length == 1)
                        workspace.GetFeatureClassByName(names[0]);
                    else if (names.Length == 2)
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
            var dir = System.IO.Path.GetDirectoryName(shpPath);
            var fileName = System.IO.Path.GetFileNameWithoutExtension(shpPath);
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
    }
}
