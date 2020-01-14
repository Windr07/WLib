/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using WLib.ArcGis.Carto.Map;
using WLib.ArcGis.GeoDatabase.Fields;

namespace WLib.ArcGis.Geometry
{
    //关于坐标系、坐标系代码(WKID/SRID/EPSG Code)：
    //   https://www.cnblogs.com/liweis/p/5951032.html
    //   http://spatialreference.org/ref/
    //   https://epsg.io/

    //常用坐标系代码：
    //  地理坐标系：
    //      4326  WGS84
    //      4214  Beijing54
    //      4610  Xian80
    //      4490  CGCS2000
    //  投影坐标系：
    //      2361: Xian 1980 / 3-degree Gauss-Kruger zone 37
    //      2362: Xian 1980 / 3-degree Gauss-Kruger zone 38
    //      2363: Xian 1980 / 3-degree Gauss-Kruger zone 39

    /// <summary>
    /// 坐标系操作
    /// </summary>
    public static class SpatialRefOpt
    {
        #region 获取坐标系
        /// <summary>
        /// 获取要素集坐标系
        /// </summary>
        /// <param name="featureDataset">要素集</param>
        /// <returns></returns>
        public static ISpatialReference GetSpatialRef(this IFeatureDataset featureDataset)
        {
            IGeoDataset geoDataset = (IGeoDataset)featureDataset;
            return geoDataset.SpatialReference;
        }
        /// <summary>
        /// 获取要素层坐标系
        /// </summary>
        /// <param name="featureLayer">要素层</param>
        /// <returns></returns>
        public static ISpatialReference GetSpatialRef(this IFeatureLayer featureLayer)
        {
            IGeoDataset geoDataset = (IGeoDataset)featureLayer.FeatureClass;
            return geoDataset.SpatialReference;
        }
        /// <summary>
        /// 获取要素类坐标系
        /// </summary>
        /// <param name="featureClass">要素类</param>
        /// <returns></returns>
        public static ISpatialReference GetSpatialRef(this IFeatureClass featureClass)
        {
            IGeoDataset geoDataset = (IGeoDataset)featureClass;
            return geoDataset.SpatialReference;
        }
        /// <summary>
        /// 从字段集中查找SHAPE字段，获取SHAPE字段包含的坐标系信息
        /// </summary>
        /// <param name="fields">字段集</param>
        /// <returns></returns>
        public static ISpatialReference GetSpatialRef(this IFields fields)
        {
            var shapeField = fields.GetFirstFieldsByType(esriFieldType.esriFieldTypeGeometry);
            if (shapeField == null) throw new Exception("字段集中不包含SHAPE字段！");

            return GetSpatialRef(shapeField);
        }
        /// <summary>
        /// 获取SHAPE字段包含的坐标系信息
        /// </summary>
        /// <param name="shapeField">SHAPE字段</param>
        /// <returns></returns>
        public static ISpatialReference GetSpatialRef(this IField shapeField)
        {
            return shapeField.GeometryDef.SpatialReference;
        }
        #endregion


        #region 创建坐标系
        /// <summary>
        /// 创建与指定要素类坐标系相同的坐标系
        /// </summary>
        /// <param name="featureClass">要素类</param>
        /// <param name="type">创建的坐标系类别</param>
        /// <returns></returns>
        public static ISpatialReference CreateSpatialRef(this IFeatureClass featureClass, ESrType type = ESrType.Projected)
        {
            IField souceGeoField = featureClass.Fields.get_Field(featureClass.FindField(featureClass.ShapeFieldName));
            int factoryCode = souceGeoField.GeometryDef.SpatialReference.FactoryCode;
            //Console.WriteLine(factoryCode);
            //Console.WriteLine(featureClass.AliasName);
            //Console.WriteLine(YYGISLib.ArcGisHelper.Data.CoordinateSystem.GetCoordinateDetail(featureClass));
            //Console.WriteLine(YYGISLib.ArcGisHelper.FeatureHelper.FeatureClassOpt.GetSourcePath(featureClass));

            return CreateSpatialRef(factoryCode, type);
        }
        /// <summary>
        /// 创建与指定要素坐标系相同的坐标系
        /// </summary>
        /// <param name="feature">要素</param>
        /// <param name="type">创建的坐标系类别</param>
        /// <returns></returns>
        public static ISpatialReference CreateSpatialRef(this IFeature feature, ESrType type = ESrType.Projected)
        {
            int factoryCode = feature.Shape.SpatialReference.FactoryCode;
            return CreateSpatialRef(factoryCode, type);
        }
        /// <summary>
        ///  根据指定投影类型创建投影坐标系
        /// </summary>
        /// <param name="pcsType">投影类型枚举(esriSRProjCS4Type)</param>
        /// <returns></returns>
        public static ISpatialReference CreateSpatialRef(esriSRProjCS4Type pcsType)
        {
            return CreateSpatialRef((int)pcsType, ESrType.Projected);
        }
        /// <summary>
        ///  根据指定投影类型创建地理坐标系
        /// </summary>
        /// <param name="gcsType">地理坐标系枚举(esriSRGeoCS3Type)</param>
        /// <returns></returns>
        public static ISpatialReference CreateSpatialRef(esriSRGeoCS3Type gcsType)
        {
            return CreateSpatialRef((int)gcsType, ESrType.Geographic);
        }
        /// <summary>
        /// 根据指定坐标系代码创建坐标系
        /// </summary>
        /// <param name="factoryCode">坐标系代码</param>
        /// <param name="type">创建的坐标系类别</param>
        /// <returns></returns>
        public static ISpatialReference CreateSpatialRef(int factoryCode, ESrType type = ESrType.Projected)
        {
            if (type == ESrType.Geographic)
                return new SpatialReferenceEnvironmentClass().CreateGeographicCoordinateSystem(factoryCode);
            if (type == ESrType.Projected)
                return new SpatialReferenceEnvironmentClass().CreateProjectedCoordinateSystem(factoryCode);

            return null;
        }
        /// <summary>
        /// 创建空坐标系
        /// </summary>
        /// <returns></returns>
        public static ISpatialReference CreateUnKnownSpatialRef()
        {
            ISpatialReference spatialReference = new UnknownCoordinateSystemClass();
            spatialReference.SetDomain(0, 99999999, 0, 99999999);//设置空间范围
            return spatialReference;
        }
        #endregion


        #region 修改坐标系
        /// <summary>
        /// 修改要素集坐标系
        /// </summary>
        /// <param name="featureDataset">要素集</param>
        /// <param name="spatialReference">新坐标系</param>
        public static void AlterSpatialRef(this IFeatureDataset featureDataset, ISpatialReference spatialReference)
        {
            IGeoDataset geoDataset = (IGeoDataset)featureDataset;
            IGeoDatasetSchemaEdit geoDatasetSchemaEdit = (IGeoDatasetSchemaEdit)geoDataset;
            if (geoDatasetSchemaEdit.CanAlterSpatialReference)
                geoDatasetSchemaEdit.AlterSpatialReference(spatialReference);
        }
        /// <summary>
        /// 修改要素类坐标系
        /// </summary>
        /// <param name="featureClass">要素类</param>
        /// <param name="spatialReference">新坐标系</param>
        public static void AlterSpatialRef(this IFeatureClass featureClass, ISpatialReference spatialReference)
        {
            IGeoDataset geoDataset = (IGeoDataset)featureClass;
            IGeoDatasetSchemaEdit geoDatasetSchemaEdit = (IGeoDatasetSchemaEdit)geoDataset;
            if (geoDatasetSchemaEdit.CanAlterSpatialReference)
                geoDatasetSchemaEdit.AlterSpatialReference(spatialReference);
        }
        #endregion


        #region 修改图形坐标系
        /// <summary>
        /// 将图形转换成指定坐标系（要求图形已定义了坐标系）
        /// </summary>
        /// <param name="geometry">被转换坐标系的几何图形</param>
        /// <param name="spatialReference">转换后的坐标系</param>
        [Obsolete("该方法仅用作提示，实际中应直接调用IGeometry.Project(spatialReference)，不必使用此方法")]
        public static void AlterSpatialRef(this IGeometry geometry, ISpatialReference spatialReference)
        {
            geometry.Project(spatialReference);
        }
        /// <summary>
        /// 将图形转换成指定经纬度坐标系（要求图形已定义了坐标系）
        /// </summary>
        /// <param name="geometry">被转换坐标系的几何图形</param>
        /// <param name="gcsType">地理坐标系（经纬度坐标系）（eg:(int)esriSRGeoCS3Type.esriSRGeoCS_Xian1980)</param>
        /// <returns></returns>
        public static void ToGCS(this IGeometry geometry, int gcsType)
        {
            geometry.Project(new SpatialReferenceEnvironmentClass().CreateGeographicCoordinateSystem(gcsType));
        }
        /// <summary>
        /// 将图形集合全部转换成指定经纬度坐标系（要求图形已定义了坐标系）
        /// </summary>
        /// <param name="geometries"></param>
        /// <param name="gcsType"></param>
        public static void ToGCS(this IEnumerable<IGeometry> geometries, int gcsType)
        {
            var gcs = new SpatialReferenceEnvironmentClass().CreateGeographicCoordinateSystem(gcsType);
            foreach (var geometry in geometries)
            {
                geometry.Project(gcs);
            }
        }
        /// <summary>
        /// 将图形转换成指定投影坐标系（要求图形已定义了坐标系）
        /// </summary>
        /// <param name="geometry">被转换坐标系的几何图形</param>
        /// <param name="prjType">投影坐标系（eg:(int)esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_37）</param>
        /// <returns></returns>
        public static void ToPRJ(this IGeometry geometry, int prjType)
        {
            geometry.Project(new SpatialReferenceEnvironmentClass().CreateProjectedCoordinateSystem(prjType));
        }
        /// <summary>
        /// 将图形集合全部转换成指定投影坐标系（要求图形已定义了坐标系）
        /// </summary>
        /// <param name="geometries">被转换坐标系的几何图形</param>
        /// <param name="prjType">投影坐标系（eg:(int)esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_37）</param>
        /// <returns></returns>
        public static void ToPRJ(this IEnumerable<IGeometry> geometries, int prjType)
        {
            var pcs = new SpatialReferenceEnvironmentClass().CreateProjectedCoordinateSystem(prjType);
            foreach (var geometry in geometries)
            {
                geometry.Project(pcs);
            }
        }
        /// <summary>
        /// 将图形定义为指定经纬度坐标系，并转换成投影坐标系
        /// （需要预先知道源几何图形坐标系，可用于源图形坐标系未定义或定义错误的情况）
        /// </summary>
        /// <param name="geometry">被定义和转换坐标系的几何图形</param>
        /// <param name="gcsType">地理坐标系（经纬度坐标系），先将图形定义为此坐标系（eg:(int)esriSRGeoCS3Type.esriSRGeoCS_Xian1980)</param>
        /// <param name="prjType">投影坐标系，图形坐标系将转为此坐标系（eg:(int)esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_37）</param>
        public static void DefinitionGCStoPRJ(this IGeometry geometry, int gcsType, int prjType)
        {
            ISpatialReferenceFactory spatialRefFact = new SpatialReferenceEnvironmentClass();
            geometry.SpatialReference = spatialRefFact.CreateGeographicCoordinateSystem(gcsType);
            geometry.Project(spatialRefFact.CreateProjectedCoordinateSystem(prjType));
        }
        /// <summary>
        /// 将图形定义为指定投影坐标系，并转换成地理坐标系
        /// （需要预先知道源几何图形坐标系，可用于源图形坐标系未定义或定义错误的情况）
        /// </summary>
        /// <param name="geometry">被定义和转换坐标系的几何图形</param>
        /// <param name="prjType">投影坐标系，先将图形定义为此坐标系（eg:(int)esriSRGeoCS3Type.esriSRGeoCS_Xian1980)</param>
        /// <param name="gcsType">地理坐标系（经纬度坐标系），图形坐标系将转为此坐标系（eg:(int)esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_37）</param>
        /// <returns></returns>
        public static void DefinitionPRJtoGCS(this IGeometry geometry, int prjType, int gcsType)
        {
            ISpatialReferenceFactory spatialRefFact = new SpatialReferenceEnvironmentClass();
            geometry.SpatialReference = spatialRefFact.CreateProjectedCoordinateSystem(prjType);
            geometry.Project(spatialRefFact.CreateGeographicCoordinateSystem(gcsType));
        }
        #endregion


        #region 获取坐标系参数
        /// <summary>
        /// 获取坐标系详细参数信息
        /// </summary>
        /// <param name="featureClass"></param>
        /// <returns></returns>
        public static string GetSpatialRefDetail(this IFeatureClass featureClass)
        {
            if (featureClass == null)
                throw new Exception("要素类为空，无法获取坐标系信息！");
            return GetSpatialRefDetail(((IGeoDataset)featureClass).SpatialReference);
        }
        /// <summary>
        /// 获取坐标系详细参数信息
        /// </summary>
        /// <param name="spatialRef"></param>
        /// <returns></returns>
        public static string GetSpatialRefDetail(this ISpatialReference spatialRef)
        {
            if (spatialRef == null)
                throw new Exception("坐标系(ISpatialReference)对象为Null，无法获取坐标系信息！");

            string str = string.Empty;
            try
            {
                if (spatialRef is IPRJSpatialReferenceGEN gen1)
                {
                    gen1.ExportSpatialReferenceToPRJ(out str, out _);
                }
                else
                {
                    IESRISpatialReferenceGEN gen2 = spatialRef as IESRISpatialReferenceGEN;
                    gen2.ExportToESRISpatialReference(out str, out _);
                }
                str = Environment.NewLine + str;
            }
            catch { }//在部分环境中无法将ISpatialReference转成IPRJSpatialReferenceGEN接口
            return spatialRef.Name + str;
        }
        #endregion


        #region 对比坐标系是否一致
        /// <summary>
        /// 简单地根据名称和FactoryCode，判断两个坐标系是否一致
        /// </summary>
        /// <param name="spatialRef1"></param>
        /// <param name="spatialRef2"></param>
        /// <returns></returns>
        private static bool CheckSpatialRefSimple(this ISpatialReference spatialRef1, ISpatialReference spatialRef2)
        {
            return spatialRef1.FactoryCode == spatialRef2.FactoryCode && spatialRef1.Name == spatialRef2.Name;
        }
        /// <summary>
        /// 根据坐标系的详细参数，判断两个坐标系是否一致
        /// </summary>
        /// <param name="spatialRef1"></param>
        /// <param name="spatialRef2"></param>
        /// <returns></returns>
        private static bool CheckSpatialRefDetail(this ISpatialReference spatialRef1, ISpatialReference spatialRef2)
        {
            string str1 = GetSpatialRefDetail(spatialRef1);
            string str2 = GetSpatialRefDetail(spatialRef2);
            return str1.Trim() == str2.Trim();
        }

        /// <summary>
        /// 判断两个坐标系是否一致
        /// </summary>
        /// <param name="spatialRef1"></param>
        /// <param name="spatialRef2"></param>
        /// <param name="message">若两要素类坐标系一致，此值为坐标系信息，否则提示坐标不一致并列出各坐标系信息</param>
        /// <returns></returns>
        public static bool CheckSpatialRef(this ISpatialReference spatialRef1, ISpatialReference spatialRef2, out string message)
        {
            bool result = CheckSpatialRefSimple(spatialRef1, spatialRef2);
            if (result)
            {
                var str1 = GetSpatialRefDetail(spatialRef1);
                var str2 = GetSpatialRefDetail(spatialRef2);
                result = str1.Trim() == str2.Trim();
                message = result ? str1 : $"✘坐标系具体参数有所差异，请先调整坐标系！\r\n\t坐标系1：\r\n{str1}\r\n\t坐标系2：\r\n{str2}\r\n";
            }
            else
            {
                message = $"✘坐标系不一致！\r\n\t坐标系1：{spatialRef1.Name}\r\n\t坐标系2：{spatialRef2.Name}";
            }
            return result;
        }
        /// <summary>
        /// 判断两个图层的坐标系是否一致
        /// </summary>
        /// <param name="featureClass1"></param>
        /// <param name="featureClass2"></param>
        /// <param name="message">若两要素类坐标系一致，此值为坐标系信息，否则提示坐标不一致并列出各坐标系信息</param>
        /// <returns></returns>
        public static bool CheckSpatialRef(this IFeatureClass featureClass1, IFeatureClass featureClass2, out string message)
        {
            var spatialRef1 = GetSpatialRef(featureClass1);
            var spatialRef2 = GetSpatialRef(featureClass2);
            bool result = CheckSpatialRefSimple(spatialRef1, spatialRef2);
            if (result)
            {
                var str1 = GetSpatialRefDetail(spatialRef1);
                var str2 = GetSpatialRefDetail(spatialRef2);
                result = str1.Trim() == str2.Trim();
                message = result ? str1 : $"✘坐标系具体参数有所差异，请先调整坐标系！\r\n\t{featureClass1.AliasName}-坐标系：\r\n{str1}\r\n\t{featureClass2.AliasName}-坐标系：\r\n{str2}\r\n";
            }
            else
            {
                message =
                    $"✘坐标系不一致！\r\n\t{featureClass1.AliasName}-坐标系：{spatialRef1.Name}\r\n\t{featureClass2.AliasName}-坐标系：{spatialRef2.Name}";
            }
            return result;
        }
        /// <summary>
        /// 检查多个要素类的坐标系是否完全一致
        /// </summary>
        /// <param name="featureClasses"></param>
        /// <param name="message">判断结果信息，若要素类坐标系一致，此值为坐标系详细参数，否则提示坐标不一致并列出各坐标系详细参数</param>
        /// <returns></returns>
        public static bool CheckSpatialRef(this IEnumerable<IFeatureClass> featureClasses, out string message)
        {
            bool result = true;
            StringBuilder sb = new StringBuilder();
            string str1 = GetSpatialRefDetail(featureClasses.ElementAt(0));
            sb.AppendFormat("{0}-坐标系:\r\n{1}\r\n", featureClasses.ElementAt(0).AliasName, str1);

            for (int i = 1; i < featureClasses.Count(); i++)
            {
                string str2 = GetSpatialRefDetail(featureClasses.ElementAt(i));
                if (str1.Trim() != str2.Trim())
                    result = false;
                sb.AppendFormat("\r\n{0}-坐标系:\r\n{1}\r\n", featureClasses.ElementAt(i).AliasName, str2);
            }

            if (result == false)
            {
                sb.Insert(0, "各图层坐标系不是完全一致的，请先调整坐标系！\r\n");
                message = sb.ToString();
            }
            else
            {
                message = null;
            }
            return result;
        }
        /// <summary>
        /// 检查多个要素类的坐标系是否完全一致
        /// </summary>
        /// <param name="spatialRefs"></param>
        /// <param name="message">判断结果信息，若要素类坐标系一致，此值为坐标系详细参数，否则提示坐标不一致并列出各坐标系详细参数</param>
        /// <returns></returns>
        public static bool CheckSpatialRef(this IEnumerable<ISpatialReference> spatialRefs, out string message)
        {
            bool result = true;
            StringBuilder sb = new StringBuilder();
            string str1 = GetSpatialRefDetail(spatialRefs.ElementAt(0));
            sb.AppendFormat("坐标系1:\r\n{0}\r\n", str1);

            for (int i = 1; i < spatialRefs.Count(); i++)
            {
                string str2 = GetSpatialRefDetail(spatialRefs.ElementAt(i));
                if (str1.Trim() != str2.Trim())
                    result = false;
                sb.AppendFormat("\r\n坐标系{0}:\r\n{1}\r\n", i + 1, str2);
            }

            if (result == false)
            {
                sb.Insert(0, "各坐标系不是完全一致的，请先调整坐标系！\r\n");
                message = sb.ToString();
            }
            else
            {
                message = null;
            }
            return result;
        }
        /// <summary>
        /// 检查地图坐标系与其中的所有图层的坐标系是否一致
        /// </summary>
        /// <param name="map"></param>
        /// <param name="message">判断结果信息，若地图与图层坐标系一致，此值为null，否则提示坐标不一致并列出各坐标系详细参数</param>
        /// <returns></returns>
        public static bool CheckSpatialRef(this IMap map, out string message)
        {
            var featureLayers = map.GetFeatureLayers();
            var featureClasses = featureLayers.Select(v => v.FeatureClass).ToArray();
            bool result = CheckSpatialRef(featureClasses, out message);
            if (result)
            {
                string str1 = GetSpatialRefDetail(map.SpatialReference);
                string str2 = GetSpatialRefDetail(featureClasses[0]);
                if (str1 != str2)
                {
                    message = $"地图(数据框)坐标系与各图层坐标系不一致！\r\n地图坐标系为：{str1}\r\n各图层坐标系为：{str2}";
                    result = false;
                }
            }
            return result;
        }
        #endregion


        #region 西安80坐标系
        /// <summary>
        /// 获取“西安1980高斯克吕格3度分带含带号”的指定带号的投影坐标系的枚举
        /// </summary>
        /// <param name="belt">带号（范围在[25,45]之间）</param>
        /// <returns></returns>
        public static esriSRProjCS4Type GetPcsType_Gauss3_Xian1980(int belt)
        {
            esriSRProjCS4Type jcsType;
            switch (belt)
            {
                //3_Dgree表示3度分带，没有则是6度分带；GK代表高斯克吕格；CM(centralMeridian)表示中央经线，Zone表示分带号；N是不显示带号
                case 25: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_25; break;
                case 26: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_26; break;
                case 27: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_27; break;
                case 28: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_28; break;
                case 29: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_29; break;
                case 30: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_30; break;
                case 31: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_31; break;
                case 32: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_32; break;
                case 33: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_33; break;
                case 34: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_34; break;
                case 35: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_35; break;
                case 36: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_36; break;
                case 37: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_37; break;
                case 38: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_38; break;
                case 39: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_39; break;
                case 40: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_40; break;
                case 41: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_41; break;
                case 42: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_42; break;
                case 43: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_43; break;
                case 44: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_44; break;
                case 45: jcsType = esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_45; break;
                default: throw new Exception("带号超出范围，西安1980 3度分带的带号范围为25至45");
            }
            return jcsType;
        }
        /// <summary>
        /// 创建西安1980地理坐标系（经纬度坐标系）
        /// </summary>
        /// <returns></returns>
        public static ISpatialReference CreateGeoRef_Xian1980()
        {
            return new SpatialReferenceEnvironmentClass().CreateGeographicCoordinateSystem((int)esriSRGeoCS3Type.esriSRGeoCS_Xian1980);
        }
        /// <summary>
        /// 创建西安1980高斯克吕格3度分带含带号”的指定带号投影坐标系
        /// </summary>
        /// <param name="belt">带号（范围在[25,45]之间）</param>
        /// <returns></returns>
        public static ISpatialReference CreateGauss3_Xian1980(int belt)
        {
            esriSRProjCS4Type jcsType = GetPcsType_Gauss3_Xian1980(belt);
            return new SpatialReferenceEnvironmentClass().CreateProjectedCoordinateSystem((int)jcsType);
        }
        /// <summary>
        ///  获取“西安80高斯克吕格3度分带含带号”中指定WKID的坐标系对应的带号
        /// </summary>
        /// <param name="wkid">Well Known Id，等同于SRID或EPSG（37/38/39度带分别为2361,2362,2363，即：带号 = wkid - 2324）</param>
        /// <returns></returns>
        public static int GetBelt_Gauss3_Xian1980(int wkid)
        {
            int minWkid = 2324, maxWkid = 2369;
            if (wkid < minWkid || wkid > maxWkid)
                throw new Exception($"WKID超出范围，西安1980 3度分带的WKID范围为{minWkid}至{maxWkid}");
            return wkid - minWkid;
        }
        /// <summary>
        ///  获取“西安80高斯克吕格3度分带含带号”中指定带号的坐标系对应的WKID
        /// </summary>
        /// <param name="belt">带号</param>
        /// <returns></returns>
        public static int GetWkid_Gauss3_Xian1980(int belt)
        {
            int minBelt = 25, maxBelt = 45, minWkid = 2324;
            if (belt < minBelt || belt > maxBelt)
                throw new Exception($"带号超出范围，西安1980 3度分带的带号范围为{minBelt}至{maxBelt}");
            return belt + minWkid;
        }
        #endregion


        #region 西安80坐标系广东省范围
        /// <summary>
        ///  获取“西安1980高斯克吕格3度分带含带号”的指定带号的投影坐标系的枚举，
        ///  带号不在广东省范围（37,38,39）内则抛出异常
        /// </summary>
        /// <param name="belt">带号（37,38,39）</param>
        /// <returns></returns>
        public static esriSRProjCS4Type GetPcsType_Gauss3_Xian1980_GuangDong(int belt)
        {
            if (belt < 37 || belt > 39)
                throw new Exception("带号超出范围，广东省的“西安1980 3度分带”的带号范围为37至39");

            return GetPcsType_Gauss3_Xian1980(belt);
        }
        /// <summary>
        ///  获取“西安1980高斯克吕格3度分带含带号”广东省范围的坐标系枚举
        /// </summary>
        /// <returns></returns>
        public static esriSRProjCS4Type[] GetPcsType_Gauss3_Xian1980_GuangDong()
        {
            return new[]
            {
                GetPcsType_Gauss3_Xian1980(37),
                GetPcsType_Gauss3_Xian1980(38),
                GetPcsType_Gauss3_Xian1980(39)
            };
        }
        /// <summary>
        /// 创建西安1980高斯克吕格3度分带含带号”的广东省范围的坐标系
        /// </summary>
        /// <returns></returns>
        public static ISpatialReference[] CreateGauss3_Xian1980_GuangDong()
        {
            ISpatialReferenceFactory spatialRefFac = new SpatialReferenceEnvironmentClass();
            return new ISpatialReference[]
            {
                spatialRefFac.CreateProjectedCoordinateSystem((int)GetPcsType_Gauss3_Xian1980(37)),
                spatialRefFac.CreateProjectedCoordinateSystem((int)GetPcsType_Gauss3_Xian1980(38)),
                spatialRefFac.CreateProjectedCoordinateSystem((int)GetPcsType_Gauss3_Xian1980(39)),
            };
        }
        #endregion


        #region 国家2000坐标系
        /// <summary>
        /// 获取“国家2000高斯克吕格3度分带含带号”的指定带号的投影坐标系的枚举
        /// </summary>
        /// <param name="belt">带号（范围在[25,45]之间）</param>
        /// <returns></returns>
        public static int GetPcsType_Gauss3_GCGS2000(int belt)
        {
            if (belt < 25 && belt > 45)
                throw new Exception("带号超出范围，GCGS2000 3度分带的带号范围为25至45");

            return 4513 - 25 + belt;
        }
        /// <summary>
        /// 创建国家2000地理坐标系（经纬度坐标系）
        /// </summary>
        /// <returns></returns>
        public static ISpatialReference CreateGeoRef_GCGS2000()
        {
            return new SpatialReferenceEnvironmentClass().CreateGeographicCoordinateSystem(4490);
        }
        /// <summary>
        /// 创建国家2000高斯克吕格3度分带含带号”的指定带号投影坐标系
        /// </summary>
        /// <param name="belt">带号（范围在[25,45]之间）</param>
        /// <returns></returns>
        public static ISpatialReference CreateGauss3_GCGS2000(int belt)
        {
            int jcsType = GetPcsType_Gauss3_GCGS2000(belt);
            return new SpatialReferenceEnvironmentClass().CreateProjectedCoordinateSystem(jcsType);
        }
        /// <summary>
        ///  获取“国家2000高斯克吕格3度分带含带号”中指定WKID的坐标系对应的带号
        /// </summary>
        /// <param name="wkid">Well Known Id，等同于SRID或EPSG（37/38/39度带分别为2361,2362,2363，即：带号 = wkid - 2324）</param>
        /// <returns></returns>
        public static int GetBelt_Gauss3_GCGS2000(int wkid)
        {
            int minWkid = 4513, maxWkid = 4533;
            if (wkid < minWkid || wkid > maxWkid)
                throw new Exception($"WKID超出范围，GCGS2000 3度分带的WKID范围为{minWkid}至{maxWkid}");
            return wkid - minWkid;
        }
        /// <summary>
        ///  获取“国家2000高斯克吕格3度分带含带号”中指定带号的坐标系对应的WKID
        /// </summary>
        /// <param name="belt">带号</param>
        /// <returns></returns>
        public static int GetWkid_Gauss3_GCGS2000(int belt)
        {
            int minBelt = 25, maxBelt = 45, minWkid = 4513;
            if (belt < minBelt || belt > maxBelt)
                throw new Exception($"带号超出范围，GCGS2000 3度分带的带号范围为{minBelt}至{maxBelt}");
            return belt + minWkid - 25;
        }
        /// <summary>
        ///  获取“国家2000高斯克吕格3度分带含带号”的全部坐标系的WKID
        ///  <para>范围[4513,4533]</para>
        /// </summary>
        /// <returns></returns>
        public static int[] GetWkid_Gauss3_GCGS2000()
        {
            return Enumerable.Range(4513, 21).ToArray();//4513至4533
        }
        #endregion


        /// <summary>
        /// （仅作参考）坐标点的坐标系转换：WGS84转西安80
        /// </summary>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <param name="belt">Xian80 3度分带投影的带号（范围在[25,45]之间）</param>
        /// <returns></returns>
        [Obsolete("未测试，仅作参考")]
        public static IPoint WGS84ToXian80(double longitude, double latitude, int belt)
        {
            IPoint point = new PointClass();
            point.PutCoords(longitude, latitude);

            ISpatialReferenceFactory2 spatialRefFact = new SpatialReferenceEnvironmentClass();
            IGeographicCoordinateSystem geoWGS84 = spatialRefFact.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_WGS1984);

            ISpatialReference projectXian80 = CreateGauss3_Xian1980(belt);
            IGeometry geometry = (IGeometry)point;
            geometry.SpatialReference = geoWGS84;
            geometry.Project(projectXian80);
            return point;
        }
    }
}
