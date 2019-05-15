/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using WLib.ArcGis.GeoDatabase.FeatClass;
using WLib.ArcGis.GeoDatabase.WorkSpace;
using WLib.ArcGis.Geometry;
using WLib.Attributes;

namespace WLib.ArcGis.Analysis.OnClass
{
    /// <summary>
    /// 提供空间分析相关操作方法
    /// </summary>
    public static class SpatialAnalysisOpt
    {
        /// <summary>
        /// 相交分析，返回相交部分的要素（注意输入要素类和叠加要素类不能有空几何等问题）
        /// </summary>
        /// <param name="inClass">输入的要素类</param>
        /// <param name="overlayClass">叠加的要素类</param>
        /// <param name="outPath">保存分析结果的工作空间路径</param>
        /// <param name="outName">保存分析结果的要素类名称</param>
        /// <returns></returns>
        public static IFeatureClass Intersect(this IFeatureClass inClass, IFeatureClass overlayClass, string outPath, string outName)
        {
            var workspaceType = GetWorkspace.GetDefaultWorkspaceType(outPath);
            if (workspaceType == EWorkspaceType.Default)
                throw new Exception($"工作空间路径(outPath)不存在！{outPath} 该路径必须是已存在的mdb文件路径，或shp所在文件夹路径，或gdb文件夹路径");

            IFeatureClassName outClassName = new FeatureClassNameClass
            {
                ShapeType = inClass.ShapeType,
                ShapeFieldName = inClass.ShapeFieldName,
                FeatureType = esriFeatureType.esriFTSimple
            };
            IWorkspaceName workspaceName = new WorkspaceNameClass
            {
                WorkspaceFactoryProgID = workspaceType.GetDescription(1),
                PathName = outPath
            };
            IDatasetName datasetName = (IDatasetName)outClassName;
            datasetName.Name = outName;
            datasetName.WorkspaceName = workspaceName;

            return new BasicGeoprocessorClass().Intersect((ITable)inClass, false, (ITable)overlayClass, false, 0.01, outClassName);
        }

        /// <summary>
        /// 多边形转点，将多边形要素类每个图斑的中心（重心）点存入新的点要素类中
        /// </summary>
        /// <param name="sourceClass"></param>
        /// <param name="targeClassPath"></param>
        /// <returns></returns>
        public static IFeatureClass PolygonClassToPoint(this IFeatureClass sourceClass, string targeClassPath)
        {
            if (sourceClass.ShapeType != esriGeometryType.esriGeometryPolygon)
                throw new Exception($"{sourceClass}不是面图层！");

            var outClass = sourceClass.CopyStruct(targeClassPath, esriGeometryType.esriGeometryPoint);
            var outFeatureCursor = outClass.Insert(true);
            var outFeatureBuffer = outClass.CreateFeatureBuffer();
            sourceClass.QueryFeatures(null, feature =>
            {
                var shape = feature.Shape;
                if (shape == null || shape.IsEmpty)
                    throw new Exception($"{sourceClass}图层中存在几何为空的记录，请先进行修复几何！");

                var dict = GetFieldIndexFromTwoClass(sourceClass, outClass, true);//其他字段值都从原图层复制过来
                foreach (var pair in dict)
                {
                    outFeatureBuffer.set_Value(pair.Value, feature.get_Value(pair.Key));
                }
                outFeatureBuffer.Shape = (shape as IPolygon).GetPolygonCenter();//获取多边形的中心点
                outFeatureCursor.InsertFeature(outFeatureBuffer);
            }, true);

            outFeatureCursor.Flush();

            Marshal.ReleaseComObject(sourceClass);
            Marshal.ReleaseComObject(outFeatureBuffer);
            Marshal.ReleaseComObject(outFeatureCursor);
            Marshal.ReleaseComObject(outClass);

            return outClass;
        }
        /// <summary>
        /// 多边形转点，将多边形要素类每个图斑的中心（重心）点存入新的点要素类中
        /// </summary>
        /// <param name="sourceClassPath"></param>
        /// <param name="targeClassPath"></param>
        /// <returns></returns>
        public static IFeatureClass PolygonClassToPoint(string sourceClassPath, string targeClassPath)
        {
            var sourceClass = FeatClassFromPath.FromPath(sourceClassPath);
            return sourceClass.PolygonClassToPoint(targeClassPath);
        }
        /// <summary>
        /// 获取两个要素类同名字段的索引的对应关系，存入键值对中
        /// </summary>
        /// <param name="classOne">第一个要素类</param>
        /// <param name="classTwo">第二个要素类</param>
        /// <param name="withOutOidShape">去除OID和Shape字段的获取，默认true</param>
        /// <returns></returns>
        private static Dictionary<int, int> GetFieldIndexFromTwoClass(IFeatureClass classOne, IFeatureClass classTwo, bool withOutOidShape = true)
        {
            var dict = new Dictionary<int, int>();//key：字段在源要素类的索引；value：在目标要素类中的索引
            var sourceFields = classOne.Fields;
            var tarFields = classTwo.Fields;

            var shapeFieldName = classOne.ShapeFieldName;
            var oidFieldName = classOne.OIDFieldName;
            for (var i = 0; i < sourceFields.FieldCount; i++)
            {
                var fieldName = sourceFields.get_Field(i).Name;
                if (withOutOidShape && (fieldName == shapeFieldName || fieldName == oidFieldName))
                    continue;

                var index = tarFields.FindField(fieldName);
                if (index > -1 && tarFields.get_Field(index).Editable)
                    dict.Add(i, index);
            }
            return dict;
        }
    }
}
