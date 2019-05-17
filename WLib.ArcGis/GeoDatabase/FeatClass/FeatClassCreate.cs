/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using WLib.ArcGis.GeoDatabase.Fields;
using WLib.ArcGis.GeoDatabase.WorkSpace;
using WLib.ArcGis.Geometry;

namespace WLib.ArcGis.GeoDatabase.FeatClass
{
    /// <summary>
    /// 提供创建要素类的方法
    /// </summary>
    public static class FeatClassCreate
    {
        /// <summary>
        /// 创建要素类
        /// </summary>
        /// <param name="obj">IWorkspace、IFeatureWorkspace或IFeatureDataset对象，在该对象中创建要素类</param>
        /// <param name="name">要素类名称（如果为shapefile,不能包含文件扩展名".shp"）</param>
        /// <param name="fields">要创建的字段集（必须包含SHAPE字段），可参考<see cref="FieldOpt.CreateBaseFields"/>等方法创建字段集</param>
        /// <returns></returns>
        public static IFeatureClass Create(object obj, string name, IFields fields)
        {
            var shapeField = fields.GetFirstFieldsByType(esriFieldType.esriFieldTypeGeometry);
            if (shapeField == null)
                throw new Exception($"在要创建的字段集（参数{nameof(fields)}）中找不到几何字段，创建要素类时应指定几何字段以确定几何类型和坐标系！");

            var geometryType = shapeField.GeometryDef.GeometryType;//几何类型
            var spatialRef = shapeField.GetSpatialRef();//坐标系
            return Create(obj, name, spatialRef, esriFeatureType.esriFTSimple, geometryType, fields, null, null, "");
        }
        /// <summary>
        /// 创建要素类
        /// </summary>
        /// <param name="obj">IWorkspace、IFeatureWorkspace或IFeatureDataset对象，在该对象中创建要素类</param>
        /// <param name="name">要素类名称（如果为shapefile,不能包含文件扩展名".shp"）</param>
        /// <param name="sptialRef">空间参考坐标系。若参数obj为IFeatureDataset则应赋值为null；否则不能为null，
        /// 可使用<see cref="SpatialRefOpt.CreateSpatialRef(esriSRProjCS4Type)"/>或其重载方法进行创建</param>
        /// <param name="geometryType">几何类型（点/线/面等）</param>
        /// <param name="fields">要创建的字段集（可以为null，该方法自动修改或加入OID和SHAPE字段以确保几何类型、坐标系与参数一致）</param>
        /// <returns></returns>
        public static IFeatureClass Create(object obj, string name, ISpatialReference sptialRef, esriGeometryType geometryType, IFields fields = null)
        {
            return Create(obj, name, sptialRef, esriFeatureType.esriFTSimple, geometryType, fields, null, null, "");
        }
        /// <summary>
        /// 创建要素类
        /// </summary>
        /// <param name="obj">IWorkspace、IFeatureWorkspace或IFeatureDataset对象，在该对象中创建要素类</param>
        /// <param name="name">要素类名称（如果为shapefile,不能包含文件扩展名.shp）</param>
        /// <param name="spatialRef">空间参考坐标系。若参数obj为IFeatureDataset则应赋值为null；否则不能为null，
        /// 可使用<see cref="SpatialRefOpt.CreateSpatialRef(esriSRProjCS4Type)"/>或其重载方法进行创建</param>
        /// <param name="featureType">要素类型</param>
        /// <param name="geometryType">几何类型</param>
        /// <param name="fields">要创建的字段集（可以为null，该方法自动修改或加入OID和SHAPE字段以确保几何类型、坐标系与参数一致）</param>
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

            #region uidClsExt字段为空时
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
            if (fields == null)
                fields = new FieldsClass();
            fields.AddBaseFields(geometryType, spatialRef);
            #endregion

            if (obj is IFeatureWorkspace featureWorkspace) //创建独立的FeatureClass
                return featureWorkspace.CreateFeatureClass(name, fields, null, uidClsExt, featureType, "SHAPE", configWord);
            else//在要素集中创建FeatureClass
                return ((IFeatureDataset)obj).CreateFeatureClass(name, fields, uidClsId, uidClsExt, featureType, "SHAPE", configWord);
        }


        /// <summary>
        /// 创建要素类，该要素类仅存储在内存中
        /// </summary>
        /// <param name="name">要素类名称</param>
        /// <param name="fields">要创建的字段集（必须包含SHAPE字段和OID字段），可参考<see cref="FieldOpt.CreateBaseFields"/>等方法创建字段集</param>
        /// <param name="strWorkspaceName">内存工作空间的名称</param>
        /// <returns></returns>
        public static IFeatureClass CreateInMemory(string name, IFields fields, string strWorkspaceName = "InMemoryWorkspace")
        {
            var workspace = WorkspaceCreate.NewInMemoryWorkspace(strWorkspaceName);
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
            return featureWorkspace.CreateFeatureClass(name, fields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");
        }
        /// <summary>
        /// 创建要素类，该要素类仅存储在内存中
        /// </summary>
        /// <param name="name">要素类名称</param>
        /// <param name="spatialRef">空间参考坐标系，可使用<see cref="SpatialRefOpt.CreateSpatialRef(esriSRProjCS4Type)"/>或其重载方法进行创建</param>
        /// <param name="geometryType">几何类型</param>
        /// <param name="fields">要创建的字段集（可以为null，该方法自动修改或加入OID和SHAPE字段以确保几何类型、坐标系与参数一致）</param>
        /// <param name="strWorkspaceName">内存工作空间的名称</param>
        /// <returns></returns>
        public static IFeatureClass CreateInMemory(string name, ISpatialReference spatialRef,
            esriGeometryType geometryType, IFields fields = null, string strWorkspaceName = "InMemoryWorkspace")
        {
            if (fields == null)
                fields = new FieldsClass();
            fields.AddBaseFields(geometryType, spatialRef);
            return CreateInMemory(name, fields, strWorkspaceName);
        }
    }
}
