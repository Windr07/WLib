/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： Use Open Source Library: GDAL(Geospatial Data Abstraction Library) https://www.gdal.org/
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OSGeo.OGR;
using OSGeo.OSR;

/* *
 * GDAL是开源GIS库，官网及参考文档：https://www.gdal.org/
 * 使用当前代码库WLib.Gdal，需要引用GDAL的相关DLL，请从官网下载或者进行下列操作：
 * 1、将WLib\DLL\GDAL下4个DLL添加到项目引用： gdal_csharp.dll、gdalconst_csharp.dll、ogr_csharp.dll、osr_csharp.dll
 * 2、将WLib\DLL\GDAL文件夹下的全部dll复制到C#项目的生成目录下（Copy all dll to [YourProject\bin\Debug or Release]）
 */
namespace WLib.Gdal
{
    /// <summary>
    /// GDAL帮助类
    /// </summary>
    public static class GdalHelper
    {
        /// <summary>
        /// 初始化Gdal，注册所有驱动并支持中文
        /// </summary>
        public static void GdalInit()
        {
            Ogr.RegisterAll(); // 注册所有的驱动
            OSGeo.GDAL.Gdal.AllRegister();
            OSGeo.GDAL.Gdal.SetConfigOption("GDAL_FILENAME_IS_UTF8", "YES"); // 为了支持中文路径
            OSGeo.GDAL.Gdal.SetConfigOption("SHAPE_ENCODING", "CP936"); // 为了使属性表字段支持中文
            //OSGeo.GDAL.Gdal.SetConfigOption("GDAL_DATA", System.IO.Path.GetFullPath(GbHelper.DataDir));//设置GDAL Data文件夹位置
        }
        /// <summary>
        /// 打开数据源，若数据源为空则抛出异常
        /// </summary>
        /// <param name="pathOrConnStr">数据源文件路径或连接字符串</param>
        /// <returns></returns>
        public static DataSource OpenSource(string pathOrConnStr)
        {
            var dataSource = Ogr.Open(pathOrConnStr, 0);
            if (dataSource == null)
                throw new Exception("GDAL无法读取指定数据源内容，请确认数据源可正常读取，以及GDAL初始化正常");

            return dataSource;
        }
        /// <summary>
        /// 打开数据源，执行指定操作，关闭数据源并释放资源
        /// </summary>
        /// <param name="pathOrConnStr">数据源文件路径或连接字符串</param>
        /// <param name="action">打开数据源后进行的操作</param>
        /// <returns>返回异常信息，未发生异常则返回null</returns>
        public static void OpenSource(string pathOrConnStr, Action<DataSource> action)
        {
            DataSource dataSource = null;
            try
            {
                GdalInit();
                dataSource = Ogr.Open(pathOrConnStr, 0);
                if (dataSource == null)
                    throw new Exception("GDAL无法读取数据源内容");

                action(dataSource);
            }
            catch (Exception ex)
            {
                throw new Exception("打开数据源并执行操作失败，" + ex);
            }
            finally
            {
                if (dataSource != null)
                {
                    dataSource.FlushCache();
                    dataSource.Dispose();
                }
            }
        }
        /// <summary>
        /// 创建shp文件，将几何图斑保存到shp中
        /// </summary>
        /// <param name="shpPath">shp文件保存路径</param>
        /// <param name="spatialRef">坐标系</param>
        /// <param name="geometries">要保存的几何图斑</param>
        /// <returns></returns>
        public static DataSource SaveToShpFile(string shpPath, SpatialReference spatialRef, Geometry[] geometries)
        {
            //创建数据源
            var driver = Ogr.GetDriverByName("ESRI Shapefile");
            DataSource dataSource = driver.CreateDataSource(shpPath, null);

            //创建图层
            var name = System.IO.Path.GetFileNameWithoutExtension(shpPath);
            var geometryType = geometries[0].GetGeometryType();
            Layer layer = dataSource.CreateLayer(name, spatialRef, geometryType, null);

            //创建要素
            FeatureDefn featureDef = layer.GetLayerDefn();
            foreach (var geometry in geometries)
            {
                Feature feature = new Feature(featureDef);
                feature.SetGeometry(geometry);
                layer.CreateFeature(feature);
            }

            //同步到文件中
            layer.SyncToDisk();
            return dataSource;
        }


        #region 图层、要素、值
        /// <summary>
        /// 重置图层的读取，重新设置图层的筛选条件
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="whereClause"></param>
        /// <param name="geometry"></param>
        private static void ResetLayerReading(this Layer layer, string whereClause = null, Geometry geometry = null)
        {
            layer.ResetReading();
            layer.SetAttributeFilter(whereClause);
            layer.SetSpatialFilter(geometry);
        }
        /// <summary>
        /// 按照名称查找图层
        /// </summary>
        /// <returns></returns>
        public static Layer GetLayerByName(this DataSource dataSource, string layerName, bool notFoundException = true)
        {
            layerName = layerName.Replace("dbo.", "");

            Layer resultLayer = null;
            for (int i = 0; i < dataSource.GetLayerCount(); i++)
            {
                var layer = dataSource.GetLayerByIndex(i);
                if (layer == null)
                    throw new Exception(string.Format("按照方法GetLayerByIndex在索引{0}处无法查找图层", i));
                if (layer.GetName() == layerName)
                {
                    resultLayer = layer;
                    break;
                }
            }

            if (resultLayer == null && notFoundException)
                throw new Exception(string.Format("从指定GDAL数据源中找不到名为{0}的图层，请确认以下几项：" +
                                                  "\r\n图层名是否有误/数据库是否完整/geometry_columns表是否包含相应数据", layerName));
            return resultLayer;
        }
        /// <summary>
        /// 获取数据源的全部图层
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public static List<Layer> GetLayers(this DataSource dataSource)
        {
            var layers = new List<Layer>();
            for (int i = 0; i < dataSource.GetLayerCount(); i++)
            {
                layers.Add(dataSource.GetLayerByIndex(i));
            }

            return layers;
        }
        /// <summary>
        /// （根据查询条件）获取图层中的要素
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="whereClause"></param>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public static List<Feature> GetFeatures(this Layer layer, string whereClause = null, Geometry geometry = null)
        {
            ResetLayerReading(layer, whereClause, geometry);

            var features = new List<Feature>();
            Feature oFeature = layer.GetNextFeature();
            while (oFeature != null)
            {
                features.Add(oFeature);
                oFeature = layer.GetNextFeature();
            }
            return features;
        }
        /// <summary>
        /// （根据查询条件）获取图层中的要素的OID
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="whereClause"></param>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public static List<long> GetFeatureFids(this Layer layer, string whereClause = null, Geometry geometry = null)
        {
            ResetLayerReading(layer, whereClause, geometry);

            var oids = new List<long>();
            Feature oFeature = null;
            while ((oFeature = layer.GetNextFeature()) != null)
            {
                oids.Add(oFeature.GetFID());
            }
            return oids;
        }
        /// <summary>
        /// （根据查询条件）获取从图层中找到的第一条要素
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="whereClause"></param>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public static Feature GetFirstFeature(this Layer layer, string whereClause = null, Geometry geometry = null)
        {
            ResetLayerReading(layer, whereClause, geometry);
            return layer.GetNextFeature();
        }
        /// <summary>
        /// （根据查询条件）获取从图层中找到的第一条要素的指定字段值
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="fieldName"></param>
        /// <param name="whereClause"></param>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public static string GetFirstStrValue(this Layer layer, string fieldName, string whereClause = null, Geometry geometry = null)
        {
            var feature = GetFirstFeature(layer, whereClause, geometry);
            if (feature == null)
                throw new Exception(string.Format("根据条件“{0}”在{1}图层中找不到记录/要素", whereClause, layer.GetName()));
            return feature.GetFieldAsString(fieldName);
        }
        /// <summary>
        /// （根据查询条件）获取图层中的图斑
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="whereClause"></param>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public static List<Geometry> GetGeometries(this Layer layer, string whereClause = null, Geometry geometry = null)
        {
            ResetLayerReading(layer, whereClause, geometry);

            var geometries = new List<Geometry>();
            Feature oFeature = null;
            while ((oFeature = layer.GetNextFeature()) != null)
            {
                geometries.Add(oFeature.GetGeometryRef());
            }
            return geometries;
        }
        /// <summary>
        /// （根据查询条件）获取图层中的要素，并指定更新的操作
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="action"></param>
        /// <param name="whereClause"></param>
        /// <param name="geometry"></param>
        public static void UpdateFeatures(this Layer layer, Action<Feature> action, string whereClause = null, Geometry geometry = null)
        {
            ResetLayerReading(layer, whereClause, geometry);

            Feature oFeature = null;
            while ((oFeature = layer.GetNextFeature()) != null)
            {
                action(oFeature);
            }
        }
        /// <summary>
        /// （根据查询条件）删除图层中的要素
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="whereClause"></param>
        /// <param name="geometry"></param>
        /// <returns>删除的要素的个数</returns>
        public static int DeleteFeatures(this Layer layer, string whereClause = null, Geometry geometry = null)
        {
            var oids = GetFeatureFids(layer, whereClause, geometry);
            oids.ForEach(oid => layer.DeleteFeature(oid));
            return oids.Count;
        }
        #endregion


        #region 投影和投影变换
        /// <summary>
        /// 获取投影坐标系名称
        /// </summary>
        /// <param name="prjectionSpatialRef">投影坐标系</param>
        /// <seealso cref="http://spatialreference.org/ref/"/>
        /// <returns></returns>
        public static string GetProjectionSrName(this SpatialReference prjectionSpatialRef)
        {
            prjectionSpatialRef.AutoIdentifyEPSG();
            var projsc = prjectionSpatialRef.GetAttrValue("PROJCS", 0);
            return projsc;
        }
        /// <summary>
        /// 获取地理坐标系名称
        /// </summary>
        /// <param name="geographySpatialRef">地理坐标系</param>
        /// <seealso cref="http://spatialreference.org/ref/"/>
        /// <returns></returns>
        public static string GetGeographySrName(this SpatialReference geographySpatialRef)
        {
            geographySpatialRef.AutoIdentifyEPSG();
            var projsc = geographySpatialRef.GetAttrValue("GEOGCS", 0);
            return projsc;
        }
        /// <summary>
        /// 获取投影坐标系WKID
        /// </summary>
        /// <param name="prjectionSpatialRef">投影坐标系</param>
        /// <seealso cref="http://spatialreference.org/ref/"/>
        /// <returns></returns>
        public static int GetWkid(this SpatialReference prjectionSpatialRef)
        {
            prjectionSpatialRef.AutoIdentifyEPSG();

            var strProjcs = prjectionSpatialRef.GetAttrValue("AUTHORITY", 1);
            int prjcs;

            if (!Int32.TryParse(strProjcs, out prjcs))
                throw new Exception("无法通过GDAL SpatialReference.GetAttrValue('AUTHORITY',1)方法从坐标系中获取WKID，请确认坐标系是否符合规范");
            return prjcs;
        }
        /// <summary>
        /// 将投影坐标转成地理坐标
        /// </summary>
        /// <param name="xyGeometries">投影坐标系图斑</param>
        /// <param name="projectionSpatialRef">投影坐标系</param>
        /// <returns></returns>
        public static Geometry[] ProjectionToGeography(this Geometry[] xyGeometries, SpatialReference projectionSpatialRef)
        {
            //将投影坐标转成地理坐标
            var geographySpatialRef = projectionSpatialRef.CloneGeogCS(); //获取投影坐标对应的地理坐标系
            var coordinateTransformation =
                Osr.CreateCoordinateTransformation(projectionSpatialRef, geographySpatialRef);
            var llGeometries = new List<Geometry>(); //转换坐标系后的图斑
            for (int i = 0; i < xyGeometries.Length; i++)
            {
                var llGoemetry = xyGeometries[i].Clone();
                llGoemetry.Transform(coordinateTransformation); //转换坐标系
                llGeometries.Add(llGoemetry);
            }
            return llGeometries.ToArray();
        }
        /// <summary>
        /// 将几何图形的环方向修正为SqlServer要求的环方向
        /// </summary>
        /// <param name="llGeometries">地理坐标几何图斑</param>
        /// <param name="geographySpatialRef">地理坐标系</param>
        /// <returns></returns>
        public static Geometry[] ChangedRingDirection(this Geometry[] llGeometries, SpatialReference geographySpatialRef)
        {
            List<Geometry> resultGeometries = new List<Geometry>();
            foreach (var llGoemetry in llGeometries)
            {
                //Gdal导出的wkt表示的地理坐标多边形外环为顺时针方向，sqlServer Geography（地理坐标数据）要求外环为逆时针，因此需要做转换
                string wkt = null;
                llGoemetry.ExportToWkt(out wkt);
                wkt = GdalRingHelper.ModifyGeometryDirection(wkt);

                var newGeometry = Geometry.CreateFromWkt(wkt);
                newGeometry.AssignSpatialReference(geographySpatialRef);
                resultGeometries.Add(newGeometry);
            }

            return resultGeometries.ToArray();
        }
        /// <summary>
        /// 将地理坐标转成投影坐标
        /// </summary>
        /// <param name="llGeomerty"></param>
        /// <param name="geographySpatialRef"></param>
        /// <param name="projectionSpatialRef"></param>
        /// <returns></returns>
        public static Geometry GeographyToProjection(this Geometry llGeomerty, SpatialReference geographySpatialRef, SpatialReference projectionSpatialRef)
        {
            llGeomerty.AssignSpatialReference(geographySpatialRef);
            string wkt = null;
            llGeomerty.ExportToWkt(out wkt);

            var coordinateTransformation =
                Osr.CreateCoordinateTransformation(geographySpatialRef, projectionSpatialRef);
            var xyGoemetry = llGeomerty.Clone();
            xyGoemetry.Transform(coordinateTransformation);
            return xyGoemetry;
        }
        #endregion


        #region 字段操作
        /// <summary>
        /// 获取图层中的全部字段
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static List<FieldDefn> GetFields(this Layer layer)
        {
            var fields = new List<FieldDefn>();
            FeatureDefn featureDefn = layer.GetLayerDefn();
            for (int i = 0; i < featureDefn.GetFieldCount(); i++)
            {
                fields.Add(featureDefn.GetFieldDefn(i));
            }

            return fields;
        }
        /// <summary>
        /// 获取图层中的全部字段名称
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static List<string> GetFieldNames(this Layer layer)
        {
            return GetFields(layer).Select(v => v.GetName()).ToList();
        }
        /// <summary>
        /// 将GDAL字段类型转成.NET类型
        /// </summary>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public static Type ConvertFieldType(this FieldType fieldType)
        {
            Type type = null;
            switch (fieldType)
            {
                case FieldType.OFTInteger:
                    type = typeof(int);
                    break;
                case FieldType.OFTIntegerList:
                    type = typeof(int[]);
                    break;
                case FieldType.OFTReal:
                    type = typeof(double);
                    break;
                case FieldType.OFTRealList:
                    type = typeof(double[]);
                    break;
                case FieldType.OFTString:
                    type = typeof(string);
                    break;
                case FieldType.OFTStringList:
                    type = typeof(string[]);
                    break;
                case FieldType.OFTDate:
                    type = typeof(DateTime);
                    break;
                case FieldType.OFTTime:
                    type = typeof(DateTime);
                    break;
                case FieldType.OFTDateTime:
                    type = typeof(DateTime);
                    break;
                case FieldType.OFTInteger64:
                    type = typeof(long);
                    break;
                case FieldType.OFTInteger64List:
                    type = typeof(long[]);
                    break;
                    //case FieldType.OFTWideString:
                    //    type = typeof(int);
                    //    break;
                    //case FieldType.OFTWideStringList:
                    //    type = typeof(int);
                    //    break;
                    //case FieldType.OFTBinary:
                    //    type = typeof(int);
                    //    break;
            }

            return type;
        }
        #endregion


        #region 几何图斑操作
        /// <summary>
        /// 将多个几何图斑合并成一个
        /// </summary>
        /// <param name="geometries"></param>
        /// <returns></returns>
        public static Geometry UnionGeometries(this IEnumerable<Geometry> geometries)
        {
            Geometry unionGeometry = null;
            foreach (Geometry geometry in geometries)
            {
                if (unionGeometry == null)
                    unionGeometry = geometry;
                else
                    unionGeometry = unionGeometry.Union(geometry);
            }
            return unionGeometry;
        }
        /// <summary>
        /// 将几何图形转成GeoJson
        /// </summary>
        /// <param name="geometries">几何图形</param>
        /// <param name="properties">属性值，可以为null</param>
        /// <returns></returns>
        public static string GeometriesToJson(this IEnumerable<Geometry> geometries, string[] properties = null)
        {
            StringBuilder json = new StringBuilder("{\"type\":\"FeatureCollection\",\"features\":[");
            int index = 0;
            foreach (var geometry in geometries)
            {
                string geometryJson = geometry.ExportToJson(null);
                json.Append((index == 0 ? "" : ",") +
                            "{\"type\":\"Feature\",\"geometry\":" +
                            geometryJson +
                            (properties == null ? "" : "," + properties[index]) +
                            "}");
                index++;
            }

            json.Append("]}");
            return json.ToString();
        }
        #endregion


        #region 图层转DataTable
        /// <summary>
        /// 根据图层字段信息创建一个空的DataTable
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static DataTable LayerToEmptyDataTable(this Layer layer)
        {
            var fields = GetFields(layer);
            var features = GetFeatures(layer);

            var dataTable = new DataTable(layer.GetName());
            foreach (var field in fields)
            {
                Type type = ConvertFieldType(field.GetFieldType());
                dataTable.Columns.Add(field.GetName(), type);
            }

            return dataTable;
        }
        /// <summary>
        /// 创建DataTable并将图层数据全部转为字符串写入DataTable中
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static DataTable LayerToStrDataTable(this Layer layer)
        {
            var fields = GetFields(layer);
            var features = GetFeatures(layer);

            var dataTable = new DataTable(layer.GetName());
            fields.ForEach(field => dataTable.Columns.Add(field.GetName()));
            features.ForEach(feature =>
            {
                var values = new string[fields.Count];
                for (int i = 0; i < fields.Count; i++)
                {
                    values[i] = feature.GetFieldAsString(i);
                }

                dataTable.Rows.Add(values);
            });

            return dataTable;
        }
        #endregion
    }
}
