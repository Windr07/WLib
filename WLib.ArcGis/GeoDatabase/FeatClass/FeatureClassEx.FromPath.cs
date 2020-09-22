/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/1/1
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.GeoDatabase.FeatDataset;
using WLib.ArcGis.GeoDatabase.Table;
using WLib.ArcGis.GeoDatabase.WorkSpace;

namespace WLib.ArcGis.GeoDatabase.FeatClass
{
    /// <summary>
    /// 提供对要素类数据的获取、输出、复制、创建、增、删、改、查、筛选、检查、重命名等方法
    /// </summary>
    public static partial class FeatureClassEx
    {
        /// <summary>
        /// 从指定路径（或连接字符串）中获取要素类
        /// <para><paramref name="connStrOrPath"/>支持以下情况：</para>
        /// <para>①shp路径：返回该shp存储的要素类；</para>
        /// <para>②mdb路径：返回该mdb数据库第一个要素类；</para>
        /// <para>③dwg路径：返回该dwg数据集第一个要素类；</para>
        /// <para>④普通目录：返回目录下第一个shp（优先）或dwg文件存储的要素类；</para>
        /// <para>⑤gdb目录：返回gdb数据库第一个要素类；</para>
        /// <para>⑥mdb文件路径[\DatasetName]\FeatureClassName：返回mdb数据库中指定名称或别名的要素类；</para>
        /// <para>⑦gdb目录[\DatasetName]\FeatureClassName：返回gdb数据库中指定名称或别名的要素类；</para>
        /// <para>⑧sde或oleDb或sql连接字符串：返回数据库中的第一个要素类；</para>
        /// </summary>
        /// <param name="connStrOrPath">路径或连接字符串</param>
        /// <param name="autoAddExtension">是否自动在路径末尾增加.shp/.mdb/.dwg后缀，以再次查找要素类</param>
        /// <returns></returns>
        public static IFeatureClass FromPath(string connStrOrPath, bool autoAddExtension = false)
        {
            if (WorkspaceEx.IsConnectionString(connStrOrPath))
                return FirstFromConnString(connStrOrPath);

            if (Directory.Exists(connStrOrPath))
                return FirstFormDir(connStrOrPath);

            if (File.Exists(connStrOrPath))
                return FirstFormFile(connStrOrPath);

            var featureClass = FirstFromFullPath(connStrOrPath);
            if (autoAddExtension && featureClass == null)
            {
                foreach (var extension in new[] { ".shp", ".dwg", ".mdb" })
                    if (File.Exists(connStrOrPath + extension))
                        return FirstFormFile(connStrOrPath + extension);
            }
            return featureClass;
        }
        /// <summary>
        /// 从指定路径（或连接字符串）中获取全部要素类
        /// <para><paramref name="connStrOrPath"/>支持以下情况：</para>
        /// <para>①shp路径：返回该shp存储的要素类；</para>
        /// <para>②mdb路径：返回该mdb数据库的全部要素类；</para>
        /// <para>③dwg路径：返回该dwg数据集的全部要素类；</para>
        /// <para>④普通目录：返回目录下的全部shp和dwg要素类；</para>
        /// <para>⑤gdb目录：返回gdb数据库的全部要素类；</para>
        /// <para>⑥mdb文件路径[\DatasetName][\FeatureClassName]：返回mdb数据库中指定要素类，或指定数据集下的全部要素类；</para>
        /// <para>⑦gdb目录[\DatasetName][\FeatureClassName]：返回gdb数据库中指定要素类，或指定数据集下的全部要素类；</para>
        /// <para>⑧sde或oleDb或sql连接字符串：返回数据库的全部要素类；</para>
        /// </summary>
        /// <param name="connStrOrPath">路径或连接字符串</param>
        /// <returns></returns>
        public static List<IFeatureClass> AllFromPath(string connStrOrPath)
        {
            if (WorkspaceEx.IsConnectionString(connStrOrPath))
                return FromConnString(connStrOrPath);

            if (Directory.Exists(connStrOrPath))
                return FromDir(connStrOrPath);

            if (File.Exists(connStrOrPath))
                return FromFile(connStrOrPath);

            return FromFullPath(connStrOrPath);
        }
        /// <summary>
        /// 从完整的要素类或表格路径中，获取工作空间路径、数据集名称、要素类或表格名称
        /// <para>注意：路径应为要素类或表格或工作空间路径，指向数据集的路径(dwg除外)是无法识别的</para>
        /// <para>支持的数据类型包括gdb、mdb、shp、dbf、dwg等</para>
        /// </summary>
        /// <param name="fullPath">格式为“工作空间路径[\要素集名称]\要素类或表格名称”的路径</param>
        /// <param name="workspacePath">工作空间路径</param>
        /// <param name="datasetName">数据集名称（可能为空）</param>
        /// <param name="objectName">要素类或表格名称，如果数据为shp则包含.shp后缀</param>
        /// <returns></returns>
        public static void SplitPath(string fullPath, out string workspacePath, out string datasetName, out string objectName)
        {
            TableEx.SplitPath(fullPath, out workspacePath, out datasetName, out objectName);
        }


        #region 私有方法 - 获取路径或数据库下的第一个要素类
        /// <summary>
        /// 获取指定目录下的第一个要素类，目录为gdb文件夹，或shp（优先）、dwg所在文件夹位置
        /// </summary>
        /// <param name="dir">gdb文件夹，或shp、dwg所在文件夹路径</param>
        /// <returns></returns>
        private static IFeatureClass FirstFormDir(string dir)
        {
            if (dir.ToLower().EndsWith(".gdb"))
                return FirstFromGdb(dir);

            return FirstFromShpDir(dir) ?? FirstFromDwgDir(dir);
        }
        /// <summary>
        /// 获取指定文件存储的第一个要素类
        /// </summary>
        /// <param name="filePath">shp/mdb/dwg文件路径</param>
        /// <returns></returns>
        private static IFeatureClass FirstFormFile(string filePath)
        {
            var extension = Path.GetExtension(filePath.ToLower());
            if (extension == ".shp") return FromShpFile(filePath);
            if (extension == ".mdb") return FirstFromMdb(filePath);
            if (extension == ".dwg") return FirstFromDwgFile(filePath);
            return null;
        }
        /// <summary>
        /// 获取指定全路径下的要素类，全路径形式为：“工作空间路径[\要素集名称]\要素类名称”
        /// </summary>
        /// <param name="fullPath">格式为“工作空间路径[\要素集名称]\要素类名称”的路径</param>
        /// <returns></returns>
        private static IFeatureClass FirstFromFullPath(string fullPath)
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

            IFeatureClass featureClass = null;
            if (!string.IsNullOrWhiteSpace(workspacePath))//按照"\"或者"/"分割子路径，获得要素集名称、要素类名称
            {
                workspacePath.ToWorkspace(workspace =>
                {
                    var subPath = fullPath.Replace(workspacePath, "");
                    var names = subPath.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
                    if (names.Length == 1) featureClass = workspace.GetFeatureClassByName(names[0]);
                    if (names.Length == 2) featureClass = workspace.GetFeatureDataset(names[0])?.GetFeatureClassByName(names[1]);
                });
            }

            if (featureClass == null && fullPath.Contains(".dwg"))
                featureClass = FromDwgFullPath(fullPath);

            return featureClass;
        }
        /// <summary>
        /// 获取指定shp文件存储的要素类
        /// </summary>
        /// <param name="shpPath"></param>
        /// <returns></returns>
        private static IFeatureClass FromShpFile(string shpPath)
        {
            var dir = Path.GetDirectoryName(shpPath);
            var fileName = Path.GetFileNameWithoutExtension(shpPath);
            return dir.ToWorkspace(workspace => workspace.GetFeatureClassByName(fileName));
        }
        /// <summary>
        /// 获取指定shp目录存储的第一个要素类
        /// </summary>
        /// <param name="shpDir">存储shp文件的目录</param>
        /// <returns></returns>
        private static IFeatureClass FirstFromShpDir(string shpDir)
        {
            return shpDir.ToWorkspace(workspace => workspace.GetFirstFeatureClass());
        }
        /// <summary>
        /// 获取指定mdb数据库存储的第一个要素类
        /// </summary>
        /// <param name="mdbPath">mdb文件路径</param>
        /// <returns></returns>
        private static IFeatureClass FirstFromMdb(string mdbPath)
        {
            return mdbPath.ToWorkspace(workspace => workspace.GetFirstFeatureClass());
        }
        /// <summary>
        /// 获取指定gdb数据库存储的第一个要素类
        /// </summary>
        /// <param name="gdbPath">gdb文件夹路径</param>
        /// <returns></returns>
        private static IFeatureClass FirstFromGdb(string gdbPath)
        {
            return gdbPath.ToWorkspace(workspace => workspace.GetFirstFeatureClass());
        }
        /// <summary>
        /// 获取指定CAD的dwg工作空间存储的第一个要素类
        /// </summary>
        /// <param name="dwgDir">CAD的dwg数据集路径</param>
        /// <returns></returns>
        private static IFeatureClass FirstFromDwgDir(string dwgDir)
        {
            return dwgDir.ToWorkspace(workspace => workspace.GetFirstFeatureClass(), EWorkspaceType.CAD);
        }
        /// <summary>
        /// 获取指定CAD的dwg数据集存储的第一个要素类
        /// </summary>
        /// <param name="dwgPath">CAD的dwg数据集路径</param>
        /// <returns>返回要素类，路径不存在则抛出异常，要素类不存在则返回null</returns>
        private static IFeatureClass FirstFromDwgFile(string dwgPath)
        {
            if (!File.Exists(dwgPath))
                throw new Exception("获取要素类失败，找不到指定路径：" + dwgPath);

            var dwgDir = Path.GetDirectoryName(dwgPath);
            var dataSetName = Path.GetFileNameWithoutExtension(dwgPath);
            return dwgDir.ToWorkspace(workspace => workspace.GetFeatureDataset(dataSetName).GetFirstFeatureClass(), EWorkspaceType.CAD);
        }
        /// <summary>
        /// 获取指定CAD的dwg数据集存储的第一个要素类
        /// </summary>
        /// <param name="dwgLayerPath">指向dwg图层的完整路径：dwg文件路径\图层名</param>
        /// <returns>返回要素类，路径不存在则抛出异常，要素类不存在则返回null</returns>
        private static IFeatureClass FromDwgFullPath(string dwgLayerPath)
        {
            var index = dwgLayerPath.LastIndexOf(dwgLayerPath, StringComparison.OrdinalIgnoreCase);
            if (index < 0)
                throw new Exception($"获取CAD要素类失败，“{dwgLayerPath}”不是有效的dwg数据路径！");

            var dwgPath = dwgLayerPath.Substring(0, index + 4);
            if (!File.Exists(dwgPath))
                throw new Exception("获取要素类失败，找不到指定路径：" + dwgPath);

            var dwgDir = Path.GetDirectoryName(dwgPath);
            var dataSetName = Path.GetFileNameWithoutExtension(dwgPath);
            var layerName = dwgLayerPath.Replace(dwgPath, "");
            return dwgDir.ToWorkspace(workspace => workspace.GetFeatureDataset(dataSetName).GetFeatureClassByName(layerName), EWorkspaceType.CAD);
        }
        /// <summary>
        /// 获取指定连接字符串对应数据库存储的第一个要素类
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <returns>返回要素类，若工作空间没有要素类则返回null</returns>
        private static IFeatureClass FirstFromConnString(string connString)
        {
            return connString.ToWorkspace(workspace => workspace.GetFirstFeatureClass());
        }
        #endregion


        #region 私有方法 - 获取路径或数据库下的全部要素类
        /// <summary>
        /// 获取指定目录下存储的全部shp和dwg要素类
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private static List<IFeatureClass> FromDir(string dir)
        {
            var classes = new List<IFeatureClass>();
            if (dir.EndsWith(".gdb"))
                classes.AddRange(FromGdb(dir));

            if (classes.Count == 0)
            {
                var shpPaths = Directory.GetFiles(dir, "*.shp", SearchOption.TopDirectoryOnly);
                if (shpPaths.Length > 0) classes.AddRange(FromShpDir(dir));

                var dwgPaths = Directory.GetFiles(dir, "*.dwg", SearchOption.TopDirectoryOnly);
                if (dwgPaths.Length > 0) classes.AddRange(FromDwgDir(dir));
            }

            return classes;
        }
        /// <summary>
        /// 获取指定文件中存储的全部要素类
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        private static List<IFeatureClass> FromFile(string filePath)
        {
            var classes = new List<IFeatureClass>();

            var extension = Path.GetExtension(filePath.ToLower());
            if (extension == ".shp") classes.Add(FromShpFile(filePath));
            else if (extension == ".mdb") classes.AddRange(FromMdb(filePath));
            else if (extension == ".dwg") classes.AddRange(FromDwgFile(filePath));

            return classes;
        }
        /// <summary>
        /// 获取指定全路径下的要素类，全路径形式为：“工作空间路径[\要素集名称][\要素类名称]”
        /// </summary>
        /// <param name="fullPath">格式为“工作空间路径[\要素集名称][\要素类名称]”的路径</param>
        /// <returns></returns>
        private static List<IFeatureClass> FromFullPath(string fullPath)
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

            var classes = new List<IFeatureClass>();
            IFeatureClass featureClass = null;
            if (!string.IsNullOrWhiteSpace(workspacePath))//按照"\"或者"/"分割子路径，获得要素集名称、要素类名称
            {
                workspacePath.ToWorkspace(workspace =>
                {
                    var subPath = fullPath.Replace(workspacePath, "");
                    var names = subPath.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
                    if (names.Length == 1)
                    {
                        var dataset = workspace.GetFeatureDataset(names[0]);
                        if (dataset != null)
                            classes.AddRange(dataset.GetFeatureClasses());
                        else if ((featureClass = workspace.GetFeatureClassByName(names[0])) != null)
                            classes.Add(featureClass);
                    }
                    if (names.Length == 2)
                    {
                        featureClass = workspace.GetFeatureDataset(names[0])?.GetFeatureClassByName(names[1]);
                        classes.Add(featureClass);
                    }
                });
            }

            if (fullPath.Contains(".dwg") &&
                (featureClass = FromDwgFullPath(fullPath)) != null)
                classes.Add(featureClass);

            return classes;
        }
        /// <summary>
        /// 获取指定shp目录存储的全部要素类
        /// </summary>
        /// <param name="shpDir"></param>
        /// <returns></returns>
        private static List<IFeatureClass> FromShpDir(string shpDir)
        {
            return shpDir.ToWorkspace(workspace => workspace.GetFeatureClasses());
        }
        /// <summary>
        /// 获取指定mdb数据库存储的全部要素类
        /// </summary>
        /// <param name="mdbPath"></param>
        /// <returns></returns>
        private static List<IFeatureClass> FromMdb(string mdbPath)
        {
            return mdbPath.ToWorkspace(workspace => workspace.GetFeatureClasses());
        }
        /// <summary>
        /// 获取指定gdb数据库存储的全部要素类
        /// </summary>
        /// <param name="gdbPath"></param>
        /// <returns></returns>
        private static List<IFeatureClass> FromGdb(string gdbPath)
        {
            return gdbPath.ToWorkspace(workspace => workspace.GetFeatureClasses());
        }
        /// <summary>
        /// 获取指定CAD的dwg数据集存储的要素类
        /// </summary>
        /// <param name="dwgPath">CAD的dwg数据集路径</param>
        /// <returns></returns>
        private static List<IFeatureClass> FromDwgFile(string dwgPath)
        {
            var dir = Path.GetDirectoryName(dwgPath);
            var dataSetName = Path.GetFileNameWithoutExtension(dwgPath);
            return dir.ToWorkspace(workspace => workspace.GetFeatureDataset(dataSetName).GetFeatureClasses(), EWorkspaceType.CAD);
        }
        /// <summary>
        /// 获取指定目录下全部CAD的dwg数据集存储的要素类
        /// </summary>
        /// <param name="dwgDir">CAD的dwg数据集所在目录</param>
        /// <returns></returns>
        private static List<IFeatureClass> FromDwgDir(string dwgDir)
        {
            return dwgDir.ToWorkspace(workspace => workspace.GetFeatureClasses(), EWorkspaceType.CAD);
        }
        /// <summary>
        /// 获取指定连接字符串对应数据库存储的全部要素类
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <returns></returns>
        private static List<IFeatureClass> FromConnString(string connString)
        {
            return connString.ToWorkspace(workspace => workspace.GetFeatureClasses());
        }
        #endregion
    }
}
