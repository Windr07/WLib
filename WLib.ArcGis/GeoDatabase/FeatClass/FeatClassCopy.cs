/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/14 14:36:42
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using WLib.ArcGis.GeoDatabase.Fields;
using WLib.ArcGis.Geometry;

namespace WLib.ArcGis.GeoDatabase.FeatClass
{
    /// <summary>
    /// 提供复制要素类的方法
    /// </summary>
    public static class FeatClassCopy
    {
        #region 复制表结构，生成新要素类
        /// <summary>
        /// 复制源要素类的表结构，创建一个空的要素类
        /// </summary>
        /// <param name="sourceClass">源要素类</param>
        /// <param name="targetObject">IWorkspace、IFeatureWorkspace或IFeatureDataset对象，在该对象中创建新要素类</param>
        /// <param name="name">新要素类名称</param>
        /// <param name="aliasName">新要素类别名，值为null则别名与名字相同</param>
        /// <returns></returns>
        public static IFeatureClass CopyStruct(this IFeatureClass sourceClass, object targetObject, string name, string aliasName = null)
        {
            return CopyStruct(sourceClass, targetObject, name, sourceClass.ShapeType, aliasName);
        }
        /// <summary>
        /// 复制源要素类的表结构，创建一个空的要素类
        /// </summary>
        /// <param name="sourceClass">源要素类</param>
        /// <param name="targetObject">IWorkspace、IFeatureWorkspace或IFeatureDataset对象，在该对象中创建新要素类</param>
        /// <param name="name">新要素类名称</param>
        /// <param name="geoType">要素类的几何类型</param>
        /// <param name="aliasName">新要素类别名，值为null则别名与名字相同</param>
        /// <returns></returns>
        public static IFeatureClass CopyStruct(this IFeatureClass sourceClass, object targetObject, string name, esriGeometryType geoType, string aliasName = null)
        {
            var spatialRef = sourceClass.GetSpatialRef();
            var feilds = sourceClass.CloneFeatureClassFieldsSimple();

            var featureClass = FeatClassCreate.Create(targetObject, name, spatialRef, geoType, feilds);

            if (!String.IsNullOrEmpty(aliasName))
                featureClass.RenameFeatureClassAliasName(aliasName);
            return featureClass;
        }
        /// <summary>
        /// 复制源要素类的表结构，创建一个空的要素类
        /// </summary>
        /// <param name="sourceClass">源要素类</param>
        /// <param name="targetFullPath">新要素类的保存路径</param>
        /// <param name="geoType">要素类的几何类型</param>
        /// <param name="aliasName">新要素类别名，值为null则别名与名字相同</param>
        /// <returns></returns>
        public static IFeatureClass CopyStruct(this IFeatureClass sourceClass, string targetFullPath, esriGeometryType geoType, string aliasName = null)
        {
            var spatialRef = sourceClass.GetSpatialRef();
            var feilds = sourceClass.CloneFeatureClassFieldsSimple();

            var shapeField = FieldOpt.CreateShapeField(geoType, spatialRef);
            ((IFieldsEdit)feilds).AddField(shapeField);
            var featureClass = FeatClassToPath.CreateToPath(targetFullPath, feilds);

            if (!String.IsNullOrEmpty(aliasName))
                featureClass.RenameFeatureClassAliasName(aliasName);
            return featureClass;
        }
        #endregion


        #region 复制数据，生成新要素类
        /// <summary>
        /// 复制要素，生成为新的要素类（注意目标位置不能存在同名要素类）
        /// </summary>
        /// <param name="features">需要复制的要素集合，不能有null</param>
        /// <param name="targetFullPath">新建的要素类的完整保存路径（注意目标位置不能存在同名要素类）</param>
        /// <returns>新的要素类(shp)</returns>
        public static IFeatureClass CopyDataToNewPath(this IEnumerable<IFeature> features, string targetFullPath)
        {
            var feature = features.FirstOrDefault();
            if (feature == null)
                throw new Exception("复制要素至新的shp文件时，至少要有一个要素！");

            var geoType = feature.Shape.GeometryType;
            var spatialReference = feature.Shape.SpatialReference;
            var fields = (feature.Class as IFeatureClass).CloneFeatureClassFieldsSimple();
            var feildArray = fields.FieldsToArray();
            var faetureClass = FeatClassToPath.CreateToPath(targetFullPath, geoType, spatialReference, feildArray);

            CopyDataTo(features, faetureClass);
            return faetureClass;
        }
        /// <summary>
        /// 在内存中创建新要素类，根据查询条件复制要素到新要素类，返回新要素类
        /// </summary> 
        /// <param name="sourceClass">要素类</param>
        /// <param name="whereClause">筛选条件，值为null或Empty时将复制全部要素</param>
        /// <param name="memoryClassName">内存要素类的名称</param>
        /// <returns></returns>
        public static IFeatureClass CopyDataToNewMemory(this IFeatureClass sourceClass, string whereClause, string memoryClassName = "tempFeatureClass")
        {
            IFields fields = sourceClass.CloneFeatureClassFields();
            IFeatureClass memoryClass = FeatClassCreate.CreateInMemory(memoryClassName, fields);
            IQueryFilter filter = new QueryFilterClass();
            filter.WhereClause = whereClause;
            IFeatureCursor cursor = sourceClass.Search(filter, false);
            IFeature feature;
            while ((feature = cursor.NextFeature()) != null)
            {
                IFeature tmpFeature = memoryClass.CreateFeature();
                tmpFeature.Shape = feature.ShapeCopy;
                tmpFeature.Store();
            }
            return memoryClass;
        }
        #endregion


        #region 复制数据，存入已有要素类
        /// <summary>
        /// 从源要素类中获取数据添加到目标要素类中（复制同名字段的值）
        /// </summary>
        /// <param name="sourceClass">源要素类</param>
        /// <param name="targetClass">目标要素类</param> 
        /// <param name="whereClause">筛选条件，从源要素类中筛选指定的要素复制到目标要素，为null或Empty时将复制全部要素</param>
        /// <param name="aferEachInsert">每复制一条要素之后执行的操作</param>
        public static void CopyDataTo(this IFeatureClass sourceClass, IFeatureClass targetClass, string whereClause = null, Action<IFeatureBuffer> aferEachInsert = null)
        {
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.WhereClause = whereClause;

            var featureCursor = sourceClass.Search(queryFilter, true);
            var tarFeatureCursor = targetClass.Insert(true);
            var tarFeatureBuffer = targetClass.CreateFeatureBuffer();
            var sourceFields = sourceClass.Fields;

            //获取源要素类与目标要素类相同的字段的索引
            var dict = new Dictionary<int, int>();//key：字段在源要素类的索引；value：在目标要素类中的索引
            for (var i = 0; i < sourceFields.FieldCount; i++)
            {
                var index1 = tarFeatureBuffer.Fields.FindField(sourceFields.get_Field(i).Name);
                if (index1 > -1 && tarFeatureBuffer.Fields.get_Field(index1).Editable)
                    dict.Add(i, index1);
            }

            //复制源要素类数据到目标要素类
            var feature = featureCursor.NextFeature();
            while (feature != null)
            {
                foreach (var pair in dict)
                {
                    tarFeatureBuffer.set_Value(pair.Value, feature.get_Value(pair.Key));
                }
                tarFeatureCursor.InsertFeature(tarFeatureBuffer);
                aferEachInsert?.Invoke(tarFeatureBuffer);
                feature = featureCursor.NextFeature();
            }
            tarFeatureCursor.Flush();

            Marshal.ReleaseComObject(featureCursor);
            Marshal.ReleaseComObject(tarFeatureCursor);
        }
        /// <summary>
        /// 从指定要素中获取数据添加到目标要素类中（复制同名字段的值）
        /// </summary>
        /// <param name="features"></param>
        /// <param name="targetClass"></param>
        public static void CopyDataTo(this IEnumerable<IFeature> features, IFeatureClass targetClass)
        {
            //获取源要素类与目标要素类相同的字段的索引
            var dict = new Dictionary<int, int>();//key：字段在源要素类的索引；value：在目标要素类中的索引

            var tarFeatureCursor = targetClass.Insert(true);
            var tarFeatureBuffer = targetClass.CreateFeatureBuffer();
            var sourceFields = features.First().Fields;
            for (var i = 0; i < sourceFields.FieldCount; i++)
            {
                var index1 = tarFeatureBuffer.Fields.FindField(sourceFields.get_Field(i).Name);
                if (index1 > -1 && tarFeatureBuffer.Fields.get_Field(index1).Editable)
                    dict.Add(i, index1);
            }
            foreach (var feature in features)
            {
                foreach (var pair in dict)
                {
                    tarFeatureBuffer.set_Value(pair.Value, feature.get_Value(pair.Key));
                }
                tarFeatureCursor.InsertFeature(tarFeatureBuffer);
            }
            tarFeatureCursor.Flush();
            Marshal.ReleaseComObject(tarFeatureCursor);
        }
        /// <summary>
        /// 从指定要素中获取数据插入到目标要素类中（具体赋值操作应在action委托中指定）
        /// </summary>
        /// <param name="features"></param>
        /// <param name="targetClass"></param>
        /// <param name="action">对每一个插入的要素执行的操作（一般是赋值操作），IFeature是源要素，IFeatureBuffer是新要素</param>
        public static void CopyDataTo(this IEnumerable<IFeature> features, IFeatureClass targetClass, Action<IFeature, IFeatureBuffer> action)
        {
            //获取源要素类与目标要素类相同的字段的索引
            var tarFeatureCursor = targetClass.Insert(true);
            var tarFeatureBuffer = targetClass.CreateFeatureBuffer();
            var sourceFields = features.First().Fields;
            foreach (var feature in features)
            {
                action(feature, tarFeatureBuffer);
                tarFeatureCursor.InsertFeature(tarFeatureBuffer);
            }
            tarFeatureCursor.Flush();

            Marshal.ReleaseComObject(tarFeatureCursor);
        }
        /// <summary>
        /// 从指定要素中获取数据插入到目标要素类中，并将图形坐标系转为与目标要素类一致
        /// </summary>
        /// <param name="features"></param>
        /// <param name="targetClass"></param>
        public static void CopyDataToProjectShape(this IEnumerable<IFeature> features, IFeatureClass targetClass)
        {
            //获取源要素类与目标要素类相同的字段的索引，不含Shape字段
            var dict = new Dictionary<int, int>();//key：字段在源要素类的索引；value：在目标要素类中的索引
            var sourceFields = features.First().Fields;
            var tarShapeFieldIndex = sourceFields.FindField(targetClass.ShapeFieldName);//目标要素类的Shape字段索引

            for (var i = 0; i < sourceFields.FieldCount; i++)
            {
                var index1 = targetClass.FindField(sourceFields.get_Field(i).Name);
                if (index1 > -1 &&
                    index1 != tarShapeFieldIndex &&
                    targetClass.Fields.get_Field(index1).Editable)//获取可编辑、非Shape字段
                    dict.Add(i, index1);
            }
            CopyDataTo(features, targetClass, (sourceFeature, tarFeatureBuffer) =>
            {
                //赋值非Shape字段
                foreach (var pair in dict)
                    tarFeatureBuffer.set_Value(pair.Value, sourceFeature.get_Value(pair.Key));

                //投影变换，赋值Shape字段
                var shape = sourceFeature.ShapeCopy;
                var spatialReference = targetClass.GetSpatialRef();
                shape.Project(spatialReference);
                tarFeatureBuffer.Shape = shape;
            });
        }
        #endregion
    }
}
