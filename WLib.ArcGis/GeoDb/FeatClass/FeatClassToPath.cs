/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/1/1
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
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
        /// 
        /// </summary>
        /// <param name="strConnOrPath"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static IFeatureClass CreateToPath(string strConnOrPath, IFields fields)
        {
            throw new NotImplementedException();
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
        /// 在指定的地理数据库（mdb/gdb/sde）中创建新的要素类，并返回该要素类
        /// </summary>
        /// <param name="geoDbPath">mdb文件路径，或gdb数据库路径，或sde数据库连接字符串</param>
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
        /// 在指定的地理数据库（mdb/gdb/sde）中创建新的要素类，并返回该要素类
        /// </summary>
        /// <param name="geoDbPath">mdb文件路径，或gdb数据库路径，或sde数据库连接字符串</param>
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
