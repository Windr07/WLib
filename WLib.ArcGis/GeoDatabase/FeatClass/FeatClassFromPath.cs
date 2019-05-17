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
using WLib.ArcGis.GeoDatabase.WorkSpace;

namespace WLib.ArcGis.GeoDatabase.FeatClass
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
        /// ③dwg文件路径，返回该dwg数据集第一个要素类；
        /// ④shp目录，返回目录下第一个shp文件存储的要素类；
        /// ⑤gdb目录，返回gdb数据库第一个要素类；
        /// ⑥mdb文件路径[\DatasetName]\FeatureClassName，返回mdb数据库中指定名称或别名的要素类；
        /// ⑦gdb目录[\DatasetName]\FeatureClassName，返回gdb数据库中指定名称或别名的要素类；
        /// ⑧sde或oleDb或sql连接字符串，返回数据库中的第一个要素类；
        /// </summary>
        /// <param name="connStrOrPath">路径或连接字符串</param>
        /// <returns></returns>
        public static IFeatureClass FromPath(string connStrOrPath)
        {
            if (GetWorkspace.IsConnectionString(connStrOrPath))
                return FirstFromConnString(connStrOrPath);

            if (Directory.Exists(connStrOrPath))
                return FirstFormDir(connStrOrPath);

            if (File.Exists(connStrOrPath))
                return FirstFormFile(connStrOrPath);

            return FirstFromFullPath(connStrOrPath);
        }
        /// <summary>
        /// 从指定路径（或连接字符串）中获取全部要素类
        /// </summary>
        /// <param name="connStrOrPath"></param>
        /// <returns></returns>
        public static List<IFeatureClass> AllFromPath(string connStrOrPath)
        {
            if (GetWorkspace.IsConnectionString(connStrOrPath))
                return FromConnString(connStrOrPath);

            if (Directory.Exists(connStrOrPath))
                return FromDir(connStrOrPath);

            if (File.Exists(connStrOrPath))
                return FromFile(connStrOrPath);

            return FromFullPath(connStrOrPath);
        }

        /// <summary>
        /// 从完整的要素类路径中，获取工作空间路径、数据集名称、要素类名称
        /// </summary>
        /// <param name="fullPath">格式为“工作空间路径[\要素集名称]\要素类名称”的路径</param>
        /// <param name="workspacePath">工作空间路径</param>
        /// <param name="datasetName">数据集名称</param>
        /// <param name="featureClassName">要素类名称</param>
        /// <returns></returns>
        public static void SplitPath(string fullPath, out string workspacePath, out string datasetName, out string featureClassName)
        {
            workspacePath = null; datasetName = null; featureClassName = null;
            fullPath = fullPath.ToLower();
            foreach (var extension in new[] { ".gdb", ".mdb" })
            {
                int index;
                if ((index = fullPath.LastIndexOf(extension, StringComparison.OrdinalIgnoreCase)) > -1)
                {
                    workspacePath = fullPath.Substring(0, index + 4);
                    break;
                }
            }
            if (Path.GetExtension(fullPath) == ".shp")
                workspacePath = Path.GetDirectoryName(fullPath);
            if (!string.IsNullOrWhiteSpace(workspacePath))//按照"\"或者"/"分割子路径，获得要素集名称、要素类名称
            {
                var subPath = fullPath.Replace(workspacePath, "");
                var names = subPath.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
                if (names.Length == 1) { datasetName = null; featureClassName = names[0]; }
                if (names.Length == 2) { datasetName = names[0]; featureClassName = names[1]; }
            }
        }

        #region 私有方法 - 获取路径下的第一个要素类
        /// <summary>
        /// 获取指定目录下的第一个要素类，目录为gdb文件夹，或shp、dwg所在文件夹位置
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
            if (!string.IsNullOrWhiteSpace(workspacePath))//按照"\"或者"/"分割子路径，获得要素集名称、要素类名称
            {
                var workspace = GetWorkspace.GetWorkSpace(workspacePath);
                if (workspace == null) throw new Exception($"无法按照指定路径或连接字符串“{workspacePath}”打开工作空间！");

                var subPath = fullPath.Replace(workspacePath, "");
                var names = subPath.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
                if (names.Length == 1) return workspace.GetFeatureClassByName(names[0]);
                if (names.Length == 2) return workspace.GetFeatureDataset(names[0])?.GetFeatureClassByName(names[1]);
            }

            if (fullPath.Contains(".dwg"))
                return FromDwgFullPath(fullPath);

            return null;
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
            return GetWorkspace.GetWorkSpace(dir, EWorkspaceType.ShapeFile).GetFeatureClassByName(fileName);
        }
        /// <summary>
        /// 获取指定shp目录存储的第一个要素类
        /// </summary>
        /// <param name="shpDir">存储shp文件的目录</param>
        /// <returns></returns>
        private static IFeatureClass FirstFromShpDir(string shpDir)
        {
            return GetWorkspace.GetWorkSpace(shpDir, EWorkspaceType.ShapeFile).GetFirstFeatureClass();
        }
        /// <summary>
        /// 获取指定mdb数据库存储的第一个要素类
        /// </summary>
        /// <param name="mdbPath">mdb文件路径</param>
        /// <returns></returns>
        private static IFeatureClass FirstFromMdb(string mdbPath)
        {
            return GetWorkspace.GetWorkSpace(mdbPath, EWorkspaceType.Access).GetFirstFeatureClass();
        }
        /// <summary>
        /// 获取指定gdb数据库存储的第一个要素类
        /// </summary>
        /// <param name="gdbPath">gdb文件夹路径</param>
        /// <returns></returns>
        private static IFeatureClass FirstFromGdb(string gdbPath)
        {
            return GetWorkspace.GetWorkSpace(gdbPath, EWorkspaceType.FileGDB).GetFirstFeatureClass();
        }
        /// <summary>
        /// 获取指定CAD的dwg工作空间存储的第一个要素类
        /// </summary>
        /// <param name="dwgDir">CAD的dwg数据集路径</param>
        /// <returns></returns>
        private static IFeatureClass FirstFromDwgDir(string dwgDir)
        {
            return GetWorkspace.GetWorkSpace(dwgDir, EWorkspaceType.CAD).GetFirstFeatureClass();
        }
        /// <summary>
        /// 获取指定CAD的dwg数据集存储的第一个要素类
        /// </summary>
        /// <param name="dwgPath">CAD的dwg数据集路径</param>
        /// <returns></returns>
        private static IFeatureClass FirstFromDwgFile(string dwgPath)
        {
            var dwgDir = Path.GetDirectoryName(dwgPath);
            var dataSetName = Path.GetFileNameWithoutExtension(dwgPath);
            return GetWorkspace.GetWorkSpace(dwgDir, EWorkspaceType.CAD).GetFeatureDataset(dataSetName).GetFirstFeatureClass();
        }
        /// <summary>
        /// 获取指定CAD的dwg数据集存储的第一个要素类
        /// </summary>
        /// <param name="dwgLayerPath">指向dwg图层的完整路径：dwg文件路径\图层名</param>
        /// <returns></returns>
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
            return GetWorkspace.GetWorkSpace(dwgDir, EWorkspaceType.CAD).GetFeatureDataset(dataSetName).GetFeatureClassByName(layerName);
        }
        /// <summary>
        /// 获取指定连接字符串对应数据库存储的第一个要素类
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <returns></returns>
        private static IFeatureClass FirstFromConnString(string connString)
        {
            return GetWorkspace.GetWorkSpace(connString).GetFirstFeatureClass();
        }
        #endregion


        #region 私有方法 - 获取路径下的全部要素类
        /// <summary>
        /// 获取指定目录下存储的全部要素类
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
                var workspace = GetWorkspace.GetWorkSpace(workspacePath);
                if (workspace == null) throw new Exception($"无法按照指定路径或连接字符串“{workspacePath}”打开工作空间！");

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
            return GetWorkspace.GetWorkSpace(shpDir, EWorkspaceType.ShapeFile).GetFeatureClasses();
        }
        /// <summary>
        /// 获取指定mdb数据库存储的全部要素类
        /// </summary>
        /// <param name="mdbPath"></param>
        /// <returns></returns>
        private static List<IFeatureClass> FromMdb(string mdbPath)
        {
            return GetWorkspace.GetWorkSpace(mdbPath, EWorkspaceType.Access).GetFeatureClasses();
        }
        /// <summary>
        /// 获取指定gdb数据库存储的全部要素类
        /// </summary>
        /// <param name="gdbPath"></param>
        /// <returns></returns>
        private static List<IFeatureClass> FromGdb(string gdbPath)
        {
            return GetWorkspace.GetWorkSpace(gdbPath, EWorkspaceType.FileGDB).GetFeatureClasses();
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
            return GetWorkspace.GetWorkSpace(dir, EWorkspaceType.CAD).GetFeatureDataset(dataSetName).GetFeatureClasses();
        }
        /// <summary>
        /// 获取指定目录下全部CAD的dwg数据集存储的要素类
        /// </summary>
        /// <param name="dwgDir">CAD的dwg数据集所在目录</param>
        /// <returns></returns>
        private static List<IFeatureClass> FromDwgDir(string dwgDir)
        {
            return GetWorkspace.GetWorkSpace(dwgDir, EWorkspaceType.CAD).GetFeatureClasses();
        }
        /// <summary>
        /// 获取指定连接字符串对应数据库存储的全部要素类
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <returns></returns>
        private static List<IFeatureClass> FromConnString(string connString)
        {
            return GetWorkspace.GetWorkSpace(connString).GetFeatureClasses();
        }
        #endregion
    }
}
