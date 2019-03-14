/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/1/1
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using WLib.ArcGis.GeoDb.Fields;
using WLib.ArcGis.GeoDb.WorkSpace;
using WLib.ArcGis.Geomtry;
using static System.IO.Path;

namespace WLib.ArcGis.GeoDb.FeatClass
{
    /// <summary>
    /// 提供从指定路径生成要素类的方法
    /// </summary>
    public static class FeatClassToPath
    {
        /// <summary>
        /// 在指定路径(shp/mdb/gdb)中创建新的要素类，并返回该要素类；
        /// </summary>
        /// <param name="fullPath">
        /// 要素类的完整保存路径，包含以下情况：
        /// ①shp文件路径，创建shp文件；
        /// ②mdb文件路径[\DatasetName]\FeatureClassName，在mdb中或mdb的指定要素集中，创建指定名称的图层；mdb或要素集不存在则自动创建；
        /// ③gdb目录[\DatasetName]\FeatureClassName，在gdb中或gdb的指定要素集中，创建指定名称的图层；gdb或要素集不存在则自动创建；
        /// </param>
        /// <param name="fields">要创建的字段集（必须包含SHAPE字段）</param>
        /// <returns></returns>
        public static IFeatureClass CreateToPath(string fullPath, IFields fields)
        {
            var chars = GetInvalidPathChars();
            foreach (var c in chars)
            {
                if (fullPath.Contains(c))
                    throw new Exception("路径不符合规范，文件路径不能包含以下字符串：" + string.Concat(chars));
            }

            fullPath = fullPath.ToLower();
            if (fullPath.EndsWith(".shp"))
            {
                var dir = GetDirectoryName(fullPath);
                if (dir != null)
                {
                    Directory.CreateDirectory(dir);
                    return CreateToShpFile(fullPath, fields);
                }
            }
            else if (fullPath.Contains(".mdb"))
            {
                var dbPath = fullPath.Substring(0, fullPath.IndexOf(".mdb", StringComparison.OrdinalIgnoreCase));
                if (!File.Exists(dbPath))
                    CreateWorkspace.NewWorkspace(EWorkspaceType.Access, GetDirectoryName(dbPath), GetFileNameWithoutExtension(dbPath));

                var names = fullPath.Replace(dbPath, "").Split(new[] { DirectorySeparatorChar, AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
                if (names.Length == 1) return CreateToDb(dbPath, null, names[0], fields);
                if (names.Length == 2) return CreateToDb(dbPath, names[0], names[1], fields);
            }
            else if (fullPath.Contains(".gdb"))
            {
                var dbPath = fullPath.Substring(0, fullPath.IndexOf(".gdb", StringComparison.OrdinalIgnoreCase));
                if (!Directory.Exists(dbPath))
                {
                    var dirInfo = new DirectoryInfo(dbPath);
                    if (dirInfo.Parent == null) throw new Exception($"路径“{dbPath}”不是有效的文件地理数据库路径！");
                    CreateWorkspace.NewWorkspace(EWorkspaceType.FileGDB, dirInfo.Parent.FullName, dirInfo.Name);
                }
                var names = fullPath.Replace(dbPath, "").Split(new[] { DirectorySeparatorChar, AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
                if (names.Length == 1) return CreateToDb(dbPath, null, names[0], fields);
                if (names.Length == 2) return CreateToDb(dbPath, names[0], names[1], fields);
            }
            return null;
        }


        /// <summary>
        ///  新建一个Shp文件，并返回shp文件存储的要素类（注意目录中不能存在同名文件）
        /// </summary>
        /// <param name="shpPath">新建的Shp文件路径（注意目录中不能存在同名文件）</param>
        /// <param name="geoType">要素类的几何类型</param>
        /// <param name="spatialRef">空间参考（坐标系），创建方法参考<see cref="CoordinateSystem.CreateSpatialReference(int, ESrType)"/>及该方法的重载</param>
        /// <param name="otherFields">除了OID和SHAPE字段的其他字段</param>
        /// <returns></returns>
        public static IFeatureClass CreateToShpFile(string shpPath, esriGeometryType geoType, ISpatialReference spatialRef, IField[] otherFields = null)
        {
            return CreateToDb(GetDirectoryName(shpPath), GetFileNameWithoutExtension(shpPath), null, geoType, spatialRef, otherFields);
        }
        /// <summary>
        /// 新建一个Shp文件，并返回shp文件存储的要素类（注意目录中不能存在同名文件）
        /// </summary>
        /// <param name="shpPath">新建的Shp文件路径（注意目录中不能存在同名文件）</param>
        /// <param name="fields">字段集合（应包含OID和Shape字段）</param>
        /// <returns></returns>
        public static IFeatureClass CreateToShpFile(string shpPath, IFields fields)
        {
            return CreateToDb(GetDirectoryName(shpPath), GetFileNameWithoutExtension(shpPath), null, fields);
        }


        /// <summary>
        /// 在指定目录或地理数据库（mdb/gdb/sde/sql）中创建新的要素类，并返回该要素类
        /// </summary>
        /// <param name="geoDbPath">mdb文件路径，或shp所在文件夹路径，或gdb文件夹路径，或sde/sql数据库连接字符串</param>
        /// <param name="datasetName">要素数据集名称，若赋值为null则直接在数据库下创建要素类，否则在该要素数据集(不存在则创建)下创建要素类</param>
        /// <param name="className">需要创建的要素类名称</param>
        /// <param name="geoType">要素类的几何类型</param>
        /// <param name="spatialRef">空间参考（坐标系），创建方法参考<see cref="CoordinateSystem.CreateSpatialReference(int, ESrType)"/>及该方法的重载</param>
        /// <param name="otherFields">除了OID和SHAPE字段的其他字段</param>
        /// <returns></returns>
        public static IFeatureClass CreateToDb(string geoDbPath, string datasetName, string className, esriGeometryType geoType,
            ISpatialReference spatialRef, IField[] otherFields = null)
        {
            var fields = FieldOpt.CreateFields(geoType, spatialRef, otherFields);
            var workspace = GetWorkspace.GetWorkSpace(geoDbPath);
            IFeatureClass featureClass;
            if (!string.IsNullOrEmpty(datasetName))
            {
                var featureDataset = workspace.GetFeatureDataset(datasetName) ?? workspace.CreateFeatureDataset(datasetName, spatialRef);
                featureClass = featureDataset.CreateFeatureClass(className, fields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");
                Marshal.ReleaseComObject(featureDataset);
            }
            else
            {
                featureClass = CreateFeatClass.Create(workspace, className, spatialRef, geoType, fields);
            }
            Marshal.ReleaseComObject(workspace);
            return featureClass;
        }
        /// <summary>
        /// 在指定目录或地理数据库（mdb/gdb/sde/sql）中创建新的要素类，并返回该要素类
        /// </summary>
        /// <param name="geoDbPath">mdb文件路径，或shp所在文件夹路径，或gdb文件夹路径，或sde/sql数据库连接字符串</param>
        /// <param name="datasetName">要素数据集名称，若赋值为null则直接在数据库下创建要素类，否则在该要素数据集(不存在则创建)下创建要素类</param>
        /// <param name="className">需要创建的要素类名称</param>
        /// <param name="fields">字段集合（应包含OID和Shape字段）</param>
        /// <returns></returns>
        public static IFeatureClass CreateToDb(string geoDbPath, string datasetName, string className, IFields fields)
        {
            var workspace = GetWorkspace.GetWorkSpace(geoDbPath);
            var spatialRef = CoordinateSystem.GetSpatialReference(fields);
            IFeatureClass featureClass;
            if (!string.IsNullOrEmpty(datasetName))
            {
                var featureDataset = workspace.GetFeatureDataset(datasetName) ?? workspace.CreateFeatureDataset(datasetName, spatialRef);
                featureClass = featureDataset.CreateFeatureClass(className, fields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");
                Marshal.ReleaseComObject(featureDataset);
            }
            else
            {
                featureClass = (workspace as IFeatureWorkspace)?.CreateFeatureClass(className, fields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");
            }
            Marshal.ReleaseComObject(workspace);
            return featureClass;
        }
    }
}
