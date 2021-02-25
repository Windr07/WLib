/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using WLib.ArcGis.GeoDatabase.FeatClass;
using WLib.ArcGis.GeoDatabase.WorkSpace;
using WLib.ArcGis.Geometry;
using WLib.Attributes.Description;

namespace WLib.ArcGis.Analysis.OnClass
{
    /// <summary>
    /// 提供常见空间分析相关操作方法
    /// <para>融合、合并、裁剪、相交、联合、要素转点</para>
    /// </summary>
    public static class SpatialAnalysisOpt
    {
        /// <summary>
        /// 图层相交，返回相交部分的要素
        /// <para>注意输入要素类和叠加要素类不能有空几何等问题</para>
        /// </summary>
        /// <param name="inClass">输入的要素类</param>
        /// <param name="overlayClass">叠加的要素类</param>
        /// <param name="outPath">保存分析结果的工作空间路径</param>
        /// <param name="outName">保存分析结果的要素类名称</param>
        /// <param name="tolorance">XY容差</param>
        /// <returns></returns>
        public static IFeatureClass Intersect(this IFeatureClass inClass, IFeatureClass overlayClass, string outPath, string outName, double tolorance = 0.0)
        {
            var outClassName = CreateOutputName(inClass, outPath, outName);
            return new BasicGeoprocessorClass().Intersect((ITable)inClass, false, (ITable)overlayClass, false, tolorance, outClassName);
        }
        /// <summary>
        /// 图层裁剪，返回裁剪后剩余部分的要素
        /// <para>注意输入要素类和叠加要素类不能有空几何等问题</para>
        /// </summary>
        /// <param name="inClass">输入的要素类</param>
        /// <param name="clipClass">裁剪的要素类</param>
        /// <param name="outPath">保存分析结果的工作空间路径</param>
        /// <param name="outName">保存分析结果的要素类名称</param>
        /// <param name="tolorance">XY容差</param>
        /// <returns></returns>
        public static IFeatureClass Clip(this IFeatureClass inClass, IFeatureClass clipClass, string outPath, string outName, double tolorance = 0.0)
        {
            var outClassName = CreateOutputName(inClass, outPath, outName);
            return new BasicGeoprocessorClass().Clip((ITable)inClass, false, (ITable)clipClass, false, tolorance, outClassName);
        }
        /// <summary>
        /// 图层合并，返回合并后的图层（单纯把多个图层的图斑集合为一个图层）
        /// </summary>
        /// <param name="inClass">输入的要素类</param>
        /// <param name="mergeClass">参与合并的要素类</param>
        /// <param name="outPath">保存分析结果的工作空间路径</param>
        /// <param name="outName">保存分析结果的要素类名称</param>
        /// <returns></returns>
        public static IFeatureClass Merge(this IFeatureClass inClass, IFeatureClass mergeClass, string outPath, string outName)
        {
            if (mergeClass == null)
                throw new ArgumentException($"合并的图层（参数{nameof(mergeClass)}）不能为空！");
            return Merge(new[] { inClass, mergeClass }, outPath, outName);
        }
        /// <summary>
        /// 图层合并，返回合并后的图层（单纯把多个图层的图斑集合为一个图层）
        /// </summary>
        /// <param name="inClass">输入的要素类</param>
        /// <param name="mergeClasses">参与合并的要素类</param>
        /// <param name="outPath">保存分析结果的工作空间路径</param>
        /// <param name="outName">保存分析结果的要素类名称</param>
        /// <returns></returns>
        public static IFeatureClass Merge(this IFeatureClass inClass, IEnumerable<IFeatureClass> mergeClasses, string outPath, string outName)
        {
            if (mergeClasses == null)
                throw new ArgumentException($"合并的图层（参数{nameof(mergeClasses)}）不能为空！");

            var list = mergeClasses.ToList();
            list.Insert(0, inClass);
            return Merge(list.ToArray(), outPath, outName);
        }
        /// <summary>
        /// 图层合并，返回合并后的图层（单纯把多个图层的图斑集合为一个图层）
        /// </summary>
        /// <param name="featureClasses">参与合并的要素类</param>
        /// <param name="outPath">保存分析结果的工作空间路径</param>
        /// <param name="outName">保存分析结果的要素类名称</param>
        /// <returns></returns>
        public static IFeatureClass Merge(this IFeatureClass[] featureClasses, string outPath, string outName)
        {
            if (featureClasses == null || featureClasses.Length < 2)
                throw new ArgumentException($"图层数（参数{nameof(featureClasses)}）不足，进行数据合并至少需要输入两个图层！");

            IArray array = new ArrayClass();
            foreach (var cls in featureClasses)
                array.Add(cls);

            var fieldsTable = featureClasses[0] as ITable;
            var outClassName = CreateOutputName(featureClasses[0], outPath, outName);
            return new BasicGeoprocessorClass().Merge(array, fieldsTable, outClassName);
        }
        /// <summary>
        /// 图层融合，按照指定的融合字段将字段值相同的多个图斑融合成一个图斑，返回图斑融合后的图层
        /// </summary>
        /// <param name="inClass"></param>
        /// <param name="dissolveField">要聚合要素的字段（融合字段），根据此字段，将字段值相同的多个图斑融合成一个图斑</param>
        /// <param name="summaryFields">对属性进行汇总的字段，e.g. "Minimum.FieldName1,Average.FieldName2", 空值将被排除在所有统计计算之外。</param>
        /// <param name="outPath">保存分析结果的工作空间路径</param>
        /// <param name="outName">保存分析结果的要素类名称</param>
        /// <returns></returns>
        public static ITable Dissolve(this IFeatureClass inClass, string dissolveField, string summaryFields, string outPath, string outName)
        {
            var outClassName = CreateOutputName(outPath, outName);
            return new BasicGeoprocessorClass().Dissolve((ITable)inClass, false, dissolveField, summaryFields, outClassName);
        }
        /// <summary>
        /// 图层联合，将两个图层重叠部分进行属性组合后输出（等同于相交），其他不重叠的部分也输出到结果图层中
        /// </summary>
        /// <param name="inClass">输入的要素类</param>
        /// <param name="unionClass">联合的要素类</param>
        /// <param name="outPath">保存分析结果的工作空间路径</param>
        /// <param name="outName">保存分析结果的要素类名称</param>
        /// <param name="tolorance">XY容差</param>
        /// <returns></returns>
        public static IFeatureClass Union(this IFeatureClass inClass, IFeatureClass unionClass, string outPath, string outName, double tolorance = 0.0)
        {
            var outClassName = CreateOutputName(inClass, outPath, outName);
            return new BasicGeoprocessorClass().Union((ITable)inClass, false, (ITable)unionClass, false, tolorance, outClassName);
        }
        /// <summary>
        /// 构造输出要素类的信息
        /// </summary>
        /// <param name="inClass"></param>
        /// <param name="outPath"></param>
        /// <param name="outName"></param>
        /// <returns></returns>
        private static IFeatureClassName CreateOutputName(IFeatureClass inClass, string outPath, string outName)
        {
            var workspaceType = WorkspaceEx.GetDefaultWorkspaceType(outPath);
            if (workspaceType == EWorkspaceType.Default)
                throw new Exception($"工作空间路径(outPath)不存在！{outPath} 该路径必须是已存在的mdb文件路径，或shp所在文件夹路径，或gdb文件夹路径，或sde连接字符串");

            if (string.IsNullOrWhiteSpace(outName))
                throw new ArgumentNullException($"要素类名称({nameof(outName)})不能为空！");

            IFeatureClassName outClassName = new FeatureClassNameClass
            {
                ShapeType = inClass.ShapeType,
                ShapeFieldName = inClass.ShapeFieldName,
                FeatureType = esriFeatureType.esriFTSimple
            };
            IWorkspaceName workspaceName = new WorkspaceNameClass
            {
                WorkspaceFactoryProgID = workspaceType.GetDescriptionEx(1),
                PathName = outPath
            };
            IDatasetName datasetName = (IDatasetName)outClassName;
            datasetName.Name = outName;
            datasetName.WorkspaceName = workspaceName;

            return outClassName;
        }
        /// <summary>
        /// 构造输出要素类的信息
        /// </summary>
        /// <param name="outPath"></param>
        /// <param name="outName"></param>
        /// <returns></returns>
        private static IDatasetName CreateOutputName(string outPath, string outName)
        {
            var workspaceType = WorkspaceEx.GetDefaultWorkspaceType(outPath);
            if (workspaceType == EWorkspaceType.Default)
                throw new ArgumentException($"工作空间路径({nameof(outPath)}：{outPath})不存在！该路径必须是已存在的mdb文件路径，或shp所在文件夹路径，或gdb文件夹路径，或sde连接字符串");

            if (string.IsNullOrWhiteSpace(outName)) 
                throw new ArgumentNullException($"要素类名称({nameof(outName)})不能为空！");

            ITableName outTabaleName = new TableNameClass();

            IWorkspaceName workspaceName = new WorkspaceNameClass
            {
                WorkspaceFactoryProgID = workspaceType.GetDescriptionEx(1),
                PathName = outPath
            };
            IDatasetName datasetName = (IDatasetName)outTabaleName;
            datasetName.Name = outName;
            datasetName.WorkspaceName = workspaceName;

            return datasetName;
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

            Marshal.ReleaseComObject(outFeatureBuffer);
            Marshal.ReleaseComObject(outFeatureCursor);

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
            var sourceClass = FeatureClassEx.FromPath(sourceClassPath);
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
