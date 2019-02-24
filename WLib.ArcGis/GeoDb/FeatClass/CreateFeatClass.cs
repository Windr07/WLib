/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using WLib.ArcGis.GeoDb.Fields;
using WLib.ArcGis.Geomtry;

namespace WLib.ArcGis.GeoDb.FeatClass
{
    /// <summary>
    /// 提供创建要素类的方法
    /// </summary>
    public class CreateFeatClass
    {
        /// <summary>
        /// 创建要素类
        /// </summary>
        /// <param name="obj">IWorkspace、IFeatureWorkspace或IFeatureDataset对象</param>
        /// <param name="name">要素类名称（如果为shapefile,不能包含文件扩展名".shp"）</param>
        /// <param name="sptialRef">空间参考坐标系。若参数obj为IFeatureDataset则应赋值为null；否则不能为null，
        /// 可使用<see cref="CoordinateSystem.CreateSpatialReference(esriSRProjCS4Type)"/>或其重载方法进行创建</param>
        /// <param name="geometryType">几何类型（点/线/面等）</param>
        /// <param name="fields">要创建的字段集（若值为null或不包含OID和SHAPE字段，则该方法内会创建和加入OID和SHAPE字段）</param>
        /// <returns></returns>
        public static IFeatureClass Create(object obj, string name, ISpatialReference sptialRef, esriGeometryType geometryType, IFields fields = null)
        {
            return Create(obj, name, sptialRef, esriFeatureType.esriFTSimple, geometryType, fields, null, null, "");
        }
        /// <summary>
        /// 创建要素类
        /// </summary>
        /// <param name="obj">IWorkspace、IFeatureWorkspace或IFeatureDataset对象</param>
        /// <param name="name">要素类名称（如果为shapefile,不能包含文件扩展名.shp）</param>
        /// <param name="spatialRef">空间参考坐标系。若参数obj为IFeatureDataset则应赋值为null；否则不能为null，
        /// 可使用<see cref="CoordinateSystem.CreateSpatialReference(esriSRProjCS4Type)"/>或其重载方法进行创建</param>
        /// <param name="featureType">要素类型</param>
        /// <param name="geometryType">几何类型</param>
        /// <param name="fields">要创建的字段集（可设置为null，如果为null或不包含OID和SHAPE字段，则该方法内会创建和加入OID和SHAPE字段）</param>
        /// <param name="uidClsId">CLSID值（可以为Null）</param>
        /// <param name="uidClsExt">EXTCLSID值（可以为Null）</param>
        /// <param name="configWord">配置信息关键词（可以为""）</param>
        /// <returns>返回IFeatureClass</returns>
        public static IFeatureClass Create(object obj, string name, ISpatialReference spatialRef, esriFeatureType featureType,
            esriGeometryType geometryType, IFields fields, UID uidClsId, UID uidClsExt, string configWord)
        {
            #region 错误检测
            if (obj == null)
                throw new Exception("参数[obj]不能为空!");

            if (!(obj is IWorkspace || obj is IFeatureWorkspace || obj is IFeatureDataset))
                throw new Exception("参数[obj]必须为IFeatureWorkspace或者IFeatureDataset");

            if (name.Length == 0)
                throw new Exception("参数[name]不能为空!");

            if ((obj is IWorkspace || obj is IFeatureWorkspace) && spatialRef == null)
                throw new Exception("参数[spatialRef]不能为空(对于单独的要素类)");
            #endregion

            #region uidClsId字段为空时
            if (uidClsId == null)
            {
                uidClsId = new UIDClass();
                switch (featureType)
                {
                    case esriFeatureType.esriFTSimple:
                        if (geometryType == esriGeometryType.esriGeometryLine)
                            geometryType = esriGeometryType.esriGeometryPolyline;
                        uidClsId.Value = "{52353152-891A-11D0-BEC6-00805F7C4268}";
                        break;
                    case esriFeatureType.esriFTSimpleJunction:
                        geometryType = esriGeometryType.esriGeometryPoint;
                        uidClsId.Value = "{CEE8D6B8-55FE-11D1-AE55-0000F80372B4}";
                        break;
                    case esriFeatureType.esriFTComplexJunction:
                        uidClsId.Value = "{DF9D71F4-DA32-11D1-AEBA-0000F80372B4}";
                        break;
                    case esriFeatureType.esriFTSimpleEdge:
                        geometryType = esriGeometryType.esriGeometryPolyline;
                        uidClsId.Value = "{E7031C90-55FE-11D1-AE55-0000F80372B4}";
                        break;
                    case esriFeatureType.esriFTComplexEdge:
                        geometryType = esriGeometryType.esriGeometryPolyline;
                        uidClsId.Value = "{A30E8A2A-C50B-11D1-AEA9-0000F80372B4}";
                        break;
                    case esriFeatureType.esriFTAnnotation:
                        geometryType = esriGeometryType.esriGeometryPolygon;
                        uidClsId.Value = "{E3676993-C682-11D2-8A2A-006097AFF44E}";
                        break;
                    case esriFeatureType.esriFTDimension:
                        geometryType = esriGeometryType.esriGeometryPolygon;
                        uidClsId.Value = "{496764FC-E0C9-11D3-80CE-00C04F601565}";
                        break;
                }
            }
            #endregion

            #region pUidClsExt字段为空时
            if (uidClsExt == null)
            {
                switch (featureType)
                {
                    case esriFeatureType.esriFTAnnotation:
                        uidClsExt = new UIDClass();
                        uidClsExt.Value = "{24429589-D711-11D2-9F41-00C04F6BC6A5}";
                        break;
                    case esriFeatureType.esriFTDimension:
                        uidClsExt = new UIDClass();
                        uidClsExt.Value = "{48F935E2-DA66-11D3-80CE-00C04F601565}";
                        break;
                }
            }
            #endregion

            #region 添加OID和SHAPE字段
            if (fields == null) fields = new FieldsClass();

            IFieldsEdit fieldsEdit = (IFieldsEdit)fields;
            if (!fields.IsExsitOid())
                fieldsEdit.AddField(FieldOpt.CreateOidField());

            if (!fields.IsExsitShapeField())
                fieldsEdit.AddField(FieldOpt.CreateShapeField(geometryType, spatialRef));
            #endregion

            IFeatureClass featureClass = null;
            if (obj is IWorkspace || obj is IFeatureWorkspace)
            {
                //创建独立的FeatureClass
                IFeatureWorkspace featureWorkspace = obj as IFeatureWorkspace;
                featureClass = featureWorkspace.CreateFeatureClass(name, fields, null, uidClsExt, featureType, "SHAPE", configWord);
            }
            else
            {
                //在要素集中创建FeatureClass
                featureClass = ((IFeatureDataset)obj).CreateFeatureClass(name, fields, uidClsId, uidClsExt, featureType, "SHAPE", configWord);
            }

            return featureClass;
        }


        /// <summary>
        /// 复制源要素类的表结构，创建一个空的要素类
        /// </summary>
        /// <param name="sourceClass">源要素类</param>
        /// <param name="resultObject">IWorkspace、IFeatureWorkspace或IFeatureDataset对象，在该对象中创建新要素类</param>
        /// <param name="name">新要素类名称</param>
        /// <param name="aliasName">新要素类别名，值为null则别名与名字相同</param>
        /// <returns></returns>
        public static IFeatureClass Create(IFeatureClass sourceClass, object resultObject, string name, string aliasName = null)
        {
            return Create(sourceClass, resultObject, name, sourceClass.ShapeType, aliasName);
        }
        /// <summary>
        /// 复制源要素类的表结构，创建一个空的要素类
        /// </summary>
        /// <param name="sourceClass">源要素类</param>
        /// <param name="resultObject">IWorkspace、IFeatureWorkspace或IFeatureDataset对象，在该对象中创建新要素类</param>
        /// <param name="name">新要素类名称</param>
        /// <param name="geoType">要素类的几何类型</param>
        /// <param name="aliasName">新要素类别名，值为null则别名与名字相同</param>
        /// <returns></returns>
        public static IFeatureClass Create(IFeatureClass sourceClass, object resultObject, string name, esriGeometryType geoType, string aliasName = null)
        {
            var spatialRef = CoordinateSystem.GetSpatialReference(sourceClass);
            var feilds = sourceClass.CloneFeatureClassFieldsSimple();

            var featureClass = Create(resultObject, name, spatialRef, geoType, feilds);

            if (!string.IsNullOrEmpty(aliasName))
                featureClass.RenameFeatureClassAliasName(aliasName);
            return featureClass;
        }
    }
}
