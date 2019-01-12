/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace WLib.ArcGis.GeoDb.Fields
{
    //  1、向已有要素类添加字段使用(featureClass as IClass).AddField(fieldEdit)，或使用本类的方法：AddField
    //  2、在创建要素类时设置字段使用IFieldsEdit.AddField(field);

    /// <summary>
    /// 字段操作
    /// </summary>
    public static class FieldOpt
    {
        /// <summary>
        /// 讲自定义类FieldItem转成ArcGIS的IField对象
        /// </summary>
        /// <param name="fieldItem"></param>
        /// <returns></returns>
        public static IField FieldItemToFiled(this FieldItem fieldItem)
        {
            return CreateField(fieldItem.Name, fieldItem.AliasName, fieldItem.FieldType);
        }
        /// <summary>
        /// 将ArcGIS表格的指定字段转为自定义类FieldItem集
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="fieldNames">从表格获取的字段集（名称或别名均可），值为null时获取全部字段</param>
        /// <returns></returns>
        public static List<FieldItem> GetFieldItems(this IFields fields, string[] fieldNames = null)
        {
            List<FieldItem> fieldItems = new List<FieldItem>();
            List<IField> fieldList = FieldsToList(fields);
            if (fieldNames == null)
            {
                fieldItems = fieldList.Select(v => new FieldItem(v.Name, v.AliasName, v.Type)).ToList();
            }
            else
            {
                foreach (var field in fieldList)
                {
                    if (fieldNames.Contains(field.Name) || fieldNames.Contains(field.AliasName))
                        fieldItems.Add(new FieldItem(field.Name, field.AliasName, field.Type));
                }
            }
            return fieldItems;
        }
        /// <summary>
        /// 将IFields转成IField列表
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static List<IField> FieldsToList(this IFields fields)
        {
            List<IField> fieldList = new List<IField>();
            for (int i = 0; i < fields.FieldCount; i++)
            {
                fieldList.Add(fields.get_Field(i));
            }
            return fieldList;
        }
        /// <summary>
        /// 将IFields转成字段数组
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static IField[] FieldsToArray(this IFields fields)
        {
            return FieldsToList(fields).ToArray();
        }


        #region 重设字段
        /// <summary>
        /// 重设字段（删除并重新创建指定字段）
        /// </summary>
        /// <param name="featureClass">要素类</param>
        /// <param name="fieldName">需要重设的字段</param>
        /// <param name="fieldAliasName">重设后的字段别名</param>
        /// <param name="fieldType">重设后的字段类型</param>
        /// <param name="length">重设后的字段长度</param>
        /// <returns>返回重设后的字段索引</returns>
        public static int ResetField(this IFeatureClass featureClass, string fieldName, string fieldAliasName, esriFieldType fieldType, int length = 0)
        {
            return ResetField(featureClass as ITable, fieldName, fieldAliasName, fieldType, length);
        }
        /// <summary>
        /// 重设字段（删除并重新创建指定字段）
        /// </summary>
        /// <param name="featureClass">要素类</param>
        /// <param name="table"></param>
        /// <param name="fieldName">需要重设的字段</param>
        /// <param name="fieldAliasName">重设后的字段别名</param>
        /// <param name="fieldType">重设后的字段类型</param>
        /// <param name="length">重设后的字段长度</param>
        /// <returns>返回重设后的字段索引</returns>
        public static int ResetField(this ITable table, string fieldName, string fieldAliasName, esriFieldType fieldType, int length = 0)
        {
            int index = table.Fields.FindField(fieldName);
            if (index > -1)
                table.DeleteField(table.Fields.get_Field(index));

            return AddField(table, fieldName, fieldAliasName, fieldType, length);
        }
        #endregion


        #region 复制字段
        /// <summary>
        /// 复制字段，包括几何字段、非几何字段，并设置新的几何字段的空间参考
        /// </summary>
        /// <param name="featureClass">源要素类</param>
        /// <param name="factoryCode">坐标系代码</param>
        /// <returns></returns>
        public static IFields CloneFeatureClassFields(this IFeatureClass featureClass, int factoryCode)
        {
            IFields outputFields;
            IFieldChecker fieldChecker = new FieldCheckerClass();
            IEnumFieldError obj = null;
            fieldChecker.Validate(featureClass.Fields, out obj, out outputFields);
            IField shapeField = outputFields.get_Field(outputFields.FindField(featureClass.ShapeFieldName));
            IGeometryDefEdit sfGeoDefEdit = (IGeometryDefEdit)shapeField.GeometryDef;

            ISpatialReferenceFactory spatialRefFac = new SpatialReferenceEnvironmentClass();
            IProjectedCoordinateSystem pcsSys = spatialRefFac.CreateProjectedCoordinateSystem(factoryCode);

            sfGeoDefEdit.SpatialReference_2 = pcsSys;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(fieldChecker);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(spatialRefFac);
            return outputFields;
        }
        /// <summary>
        /// 复制字段，包括几何字段、非几何字段和空间参考的设置
        /// </summary>
        /// <param name="featureClass">源要素类</param>
        /// <returns></returns>
        public static IFields CloneFeatureClassFields(this IFeatureClass featureClass)
        {
            IField souceGeoField = featureClass.Fields.get_Field(featureClass.FindField(featureClass.ShapeFieldName));
            int factoryCode = souceGeoField.GeometryDef.SpatialReference.FactoryCode;
            IFields result = CloneFeatureClassFields(featureClass, factoryCode);
            return result;
        }
        /// <summary>
        /// 复制字段，按照指定的字段名列表复制featureClass中的字段
        /// </summary>
        /// <param name="featureClass">源要素类</param>
        /// <param name="spatialRef">空间坐标系</param>
        /// <param name="fieldNames">指定的字段名列表，若指定的字段在源要素类中找不到，则跳过该字段</param>
        /// <returns></returns>
        public static IFields CloneFeatureClassFields(this IFeatureClass featureClass, ISpatialReference spatialRef, IEnumerable<string> fieldNames)
        {
            IFields fields = new FieldsClass();
            IFieldsEdit fieldsEdit = (IFieldsEdit)fields;

            //设置坐标系
            IGeometryDef geometryDef = new GeometryDefClass();
            IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
            geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
            geometryDefEdit.SpatialReference_2 = spatialRef;

            //添加Shape字段
            IField geometryField = new FieldClass();
            IFieldEdit geometryFieldEdit = (IFieldEdit)geometryField;
            geometryFieldEdit.Name_2 = featureClass.ShapeFieldName;
            geometryFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            geometryFieldEdit.GeometryDef_2 = geometryDef;
            fieldsEdit.AddField(geometryField);

            for (int i = 0; i < fieldNames.Count(); i++)
            {
                int index = featureClass.Fields.FindField(fieldNames.ElementAt(i));
                if (index == -1) continue;
                IField fieldSource = featureClass.Fields.get_Field(index);
                IField field = new FieldClass();
                IFieldEdit fieldEdit = (IFieldEdit)field;
                fieldEdit.Name_2 = fieldSource.Name;
                fieldEdit.AliasName_2 = fieldSource.AliasName;
                fieldEdit.DefaultValue_2 = fieldSource.DefaultValue;
                fieldEdit.Type_2 = fieldSource.Type;
                fieldEdit.Length_2 = fieldSource.Length;
                fieldsEdit.AddField(field);
            }
            return fields;
        }
        /// <summary>
        /// 复制字段，OID和Shape字段除外
        /// </summary>
        /// <param name="featureClass">源要素类</param>
        /// <returns></returns>
        public static IFields CloneFeatureClassFieldsSimple(this IFeatureClass featureClass)
        {
            IFields fields = new FieldsClass();
            IFieldsEdit fieldsEdit = (IFieldsEdit)fields;
            long fieldsCount = featureClass.Fields.FieldCount;
            long shapeFieldIndex = featureClass.Fields.FindField(featureClass.ShapeFieldName);
            long idFieldIndex = featureClass.Fields.FindField(featureClass.OIDFieldName);
            for (int i = 0; i < fieldsCount; i++)
            {
                if (i != shapeFieldIndex && i != idFieldIndex)
                    fieldsEdit.AddField(featureClass.Fields.get_Field(i));
            }
            return fields;
        }
        /// <summary>
        /// 复制字段
        /// </summary>
        /// <param name="table">源表格</param>
        /// <param name="containsOidField">是否复制OID字段</param>
        /// <returns></returns>
        public static IFields CloneTableFields(this ITable table, bool containsOidField = false)
        {
            IFields fields = new FieldsClass();
            IFieldsEdit fieldsEdit = (IFieldsEdit)fields;
            long fieldsCount = table.Fields.FieldCount;
            long idFieldIndex = table.Fields.FindField(table.OIDFieldName);
            for (int i = 0; i < fieldsCount; i++)
            {
                if (!containsOidField && i == idFieldIndex)
                    continue;

                fieldsEdit.AddField(table.Fields.get_Field(i));
            }
            return fields;
        }
        #endregion


        #region 创建字段
        /// <summary>
        /// 创建包含ObjectID和Shape字段的字段集（ObjectID会在不同数据源中自动转换为FID或OID）
        /// </summary>
        /// <param name="geometryType">几何类型</param>
        /// <param name="spatialRef">坐标系（若Shape字段所在要素类在工作空间中则此值不可为null，若位于要素数据集则应设置为null</param>
        /// <returns></returns>
        public static IFields CreateBaseFields(esriGeometryType geometryType, ISpatialReference spatialRef)
        {
            IFields fields = new FieldsClass();
            IFieldsEdit fieldsEdit = fields as IFieldsEdit;
            fieldsEdit.AddField(CreateOidField());
            fieldsEdit.AddField(CreateShapeField(geometryType, spatialRef));
            return fields;
        }
        /// <summary>
        /// 创建包含ObjectID和Shape字段的字段集（ObjectID会在不同数据源中自动转换为FID或OID），并且加入其他字段（可空）
        /// </summary>
        /// <param name="geometryType">几何类型</param>
        /// <param name="spatialRef">坐标系（若Shape字段所在要素类在工作空间中则此值不可为null，若位于要素数据集则应设置为null</param>
        /// <param name="otherFields">除了ObjectID和Shape字段以外的字段（可以为null）</param>
        /// <returns></returns>
        public static IFields CreateFields(esriGeometryType geometryType, ISpatialReference spatialRef, params IField[] otherFields)
        {
            var fields = CreateBaseFields(geometryType, spatialRef);
            if (otherFields != null)
                AddFields(fields, otherFields);
            return fields;
        }
        /// <summary>
        /// 创建ObjectID字段
        /// （ObjectID会在不同数据源中自动转换为FID或OID，关于OID、FID、OBJECTID参考 http://blog.csdn.net/yh0503/article/details/27862401）
        /// </summary>
        /// <returns></returns>
        public static IField CreateOidField()
        {
            //创建OBJECTID字段
            IField fieldOID = new FieldClass();
            IFieldEdit fieldEditOID = fieldOID as IFieldEdit;
            fieldEditOID.Name_2 = "OBJECTID";   //OBJECTID字段名在不同数据源会自动转换为FID或OID
            fieldEditOID.AliasName_2 = "OBJECTID";
            fieldEditOID.Type_2 = esriFieldType.esriFieldTypeOID;
            return fieldOID;
        }
        /// <summary>
        /// 创建Shape字段
        /// </summary>
        /// <param name="geometryType">几何类型</param>
        /// <param name="spatialRef">坐标系（若Shape字段所在要素类在工作空间中则此值不可为null，若位于要素数据集则应设置为null</param>
        /// <returns></returns>
        public static IField CreateShapeField(esriGeometryType geometryType, ISpatialReference spatialRef)
        {
            //创建几何对象字段定义字段
            IGeometryDef geometryDef = new GeometryDefClass();
            IGeometryDefEdit geometryDefEdit = geometryDef as IGeometryDefEdit;

            //指定几何对象字段属性值
            geometryDefEdit.GeometryType_2 = geometryType;
            geometryDefEdit.GridCount_2 = 1;
            geometryDefEdit.set_GridSize(0, 1000);
            if (spatialRef != null)
                geometryDefEdit.SpatialReference_2 = spatialRef;


            //创建几何字段
            IField fieldShape = new FieldClass();
            IFieldEdit fieldEditShape = fieldShape as IFieldEdit;
            fieldEditShape.Name_2 = "SHAPE";
            fieldEditShape.AliasName_2 = "SHAPE";
            fieldEditShape.Type_2 = esriFieldType.esriFieldTypeGeometry;
            fieldEditShape.GeometryDef_2 = geometryDef;

            return fieldShape;
        }
        /// <summary>
        /// 创建字段
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="aliasName">字段别名</param>
        /// <param name="fieldType">字段类型</param>
        /// <returns></returns>
        public static IField CreateField(string name, string aliasName, esriFieldType fieldType)
        {
            IField field = new FieldClass();
            IFieldEdit fieldEdit = field as IFieldEdit;
            fieldEdit.Name_2 = name;
            fieldEdit.AliasName_2 = aliasName;
            fieldEdit.Type_2 = fieldType;
            return field;
        }
        /// <summary>
        /// 创建字段
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="fieldType">字段类型</param>
        /// <returns></returns>
        public static IField CreateField(string name, esriFieldType fieldType)
        {
            return CreateField(name, name, fieldType);
        }
        #endregion


        #region 添加字段
        /// <summary>
        /// 向已有字段集添加多个字段
        /// </summary>
        /// <param name="iFields"></param>
        /// <param name="otherFields"></param>
        public static void AddFields(this IFields iFields, IEnumerable<IField> otherFields)
        {
            var fieldsEdit = iFields as IFieldsEdit;
            if (otherFields != null)
            {
                foreach (var field in otherFields)
                {
                    fieldsEdit.AddField(field);
                }
            }
        }
        /// <summary>
        /// 向已有要素类添加新字段，若字段存在则不添加，并返回字段索引
        /// （注意，向已有要素类添加字段使用IClass.AddFiled，而在创建要素类时设置字段使用IFieldsEdit.AddField）
        /// </summary>
        /// <param name="featureClass">操作的要素类</param>
        /// <param name="name">字段名</param>
        /// <param name="aliasName">字段别名</param>
        /// <param name="fieldType">字段类型</param>
        /// <param name="length">字段长度，若为0则不设置长度（使用默认长度）</param>
        /// <returns>添加的字段的索引</returns>
        public static int AddField(this IFeatureClass featureClass, string name, string aliasName, esriFieldType fieldType, int length = 0)
        {
            return AddField(featureClass as ITable, name, aliasName, fieldType, length);
        }
        /// <summary>
        /// 向已有表格添加新字段，若字段存在则不添加，并返回字段索引
        /// （注意，向已有表格添加字段使用IClass.AddFiled，而在创建表格时设置字段使用IFieldsEdit.AddField）
        /// </summary>
        /// <param name="table">操作的表格</param>
        /// <param name="name">字段名</param>
        /// <param name="aliasName">字段别名</param>
        /// <param name="fieldType">字段类型</param>
        /// <param name="length">字段长度，若为0则不设置长度（使用默认长度）</param>
        /// <returns>添加的字段的索引</returns>
        public static int AddField(this ITable table, string name, string aliasName, esriFieldType fieldType, int length = 0)
        {
            //若存在，则不需添加
            int index = -1;
            if ((index = table.Fields.FindField(name)) > -1)
                return index;

            IField field = new FieldClass();
            IFieldEdit fieldEdit = field as IFieldEdit;
            fieldEdit.Name_2 = name;
            fieldEdit.AliasName_2 = aliasName;
            fieldEdit.Type_2 = fieldType;
            if (length > 0)
                fieldEdit.Length_2 = length;
            IClass cls = table as IClass;
            cls.AddField(fieldEdit); //此处使用IClass.AddFiled方法，不使用IFieldsEdit.AddField方法

            return cls.FindField(name);
        }
        /// <summary>
        /// 向已有表格添加字符串类型的字段，若字段存在则不添加
        /// </summary>
        /// <param name="featureClass">操作的要素类</param>
        /// <param name="name">字段名</param>
        /// <param name="aliasName">字段别名</param>
        /// <param name="length">字段长度，即字符串长度，若字段已存在，此参数无效</param>
        /// <returns></returns>
        public static int AddStringField(this IFeatureClass featureClass, string name, string aliasName, int length)
        {
            return AddStringField(featureClass as ITable, name, aliasName, length);
        }
        /// <summary>
        /// 向已有表格添加字符串类型的字段，若字段存在则不添加
        /// </summary>
        /// <param name="table">操作的表格</param>
        /// <param name="name">字段名</param>
        /// <param name="aliasName">字段别名</param>
        /// <param name="length">字段长度，即字符串长度，若字段已存在，此参数无效</param>
        /// <returns></returns>
        public static int AddStringField(this ITable table, string name, string aliasName, int length)
        {
            //若存在，则不需添加
            int index = -1;
            if ((index = table.Fields.FindField(name)) > -1)
                return index;

            IField field = new FieldClass();
            IFieldEdit fieldEdit = field as IFieldEdit;
            fieldEdit.Name_2 = name;
            fieldEdit.AliasName_2 = aliasName;
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            fieldEdit.Length_2 = length;
            IClass cls = table as IClass;
            cls.AddField(fieldEdit); //此处使用IClass.AddFiled方法，不使用IFieldsEdit.AddField方法

            return cls.FindField(name);
        }
        #endregion


        #region 获取字段名称、别名、索引等信息
        /// <summary>
        /// 查找指定类型的字段
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public static List<IField> GetFieldsByType(this IFields fields, esriFieldType fieldType)
        {
            List<IField> fieldList = new List<IField>();
            for (int i = 0; i < fields.FieldCount; i++)
            {
                var field = fields.get_Field(i);
                if (field.Type == fieldType)
                    fieldList.Add(field);
            }
            return fieldList;
        }
        /// <summary>
        /// 查找指定类型的字段
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public static IField GetFirstFieldsByType(this IFields fields, esriFieldType fieldType)
        {
            for (int i = 0; i < fields.FieldCount; i++)
            {
                var field = fields.get_Field(i);
                if (field.Type == fieldType)
                    return field;
            }
            return null;
        }


        /// <summary>
        /// SHAPE_LENGTH
        /// </summary>
        public static string SHAPE_LENGTH = "SHAPE_LENGTH";
        /// <summary>
        /// SHAPE_AREA
        /// </summary>
        public static string SHAPE_AREA = "SHAPE_AREA";
        /// <summary>
        /// 获取系统字段
        /// （表格的系统字段为OID字段；要素类的系统字段为OID字段、SHAPE字段、"SHAPE_LENGTH"、"SHAPE_AREA"）
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string[] GetSystemFieldNames(this ITable table)
        {
            var featureClass = table as IFeatureClass;
            if (featureClass != null)
                return new[] { featureClass.OIDFieldName, featureClass.ShapeFieldName, SHAPE_LENGTH, SHAPE_AREA };
            else
                return new[] { table.OIDFieldName };
        }
        /// <summary>
        /// 获取系统字段（OID字段、SHAPE字段、"SHAPE_LENGTH"、"SHAPE_AREA"）
        /// </summary>
        /// <param name="featureClass"></param>
        /// <returns></returns>
        public static string[] GetSystemFieldNames(this IFeatureClass featureClass)
        {
            return new[] { featureClass.OIDFieldName, featureClass.ShapeFieldName, SHAPE_LENGTH, SHAPE_AREA };
        }


        /// <summary>
        /// 获取要素类的所有字段名
        /// </summary>
        /// <param name="featureClass">要素类</param>
        /// <param name="getSystemFields">是否获取系统字段(OID, SHAPE, SHAPE_LENGTH, SHAPE_AREA)</param>
        /// <returns></returns>
        public static List<string> GetFieldsNames(this IFeatureClass featureClass, bool getSystemFields = true)
        {
            return GetFieldsNames(featureClass as ITable, getSystemFields);
        }
        /// <summary>
        /// 获取表格的所有字段名
        /// </summary>
        /// <param name="table">表格</param>
        /// <param name="getSystemFields">是否获取系统字段(OID, SHAPE, SHAPE_LENGTH, SHAPE_AREA)</param>
        /// <returns></returns>
        public static List<string> GetFieldsNames(this ITable table, bool getSystemFields = true)
        {
            List<string> names = new List<string>();
            int cnt = table.Fields.FieldCount;
            for (int i = 0; i < cnt; i++)
            {
                names.Add(table.Fields.get_Field(i).Name);
            }

            if (!getSystemFields)
            {
                string[] sysFieldNames = GetSystemFieldNames(table);
                names.RemoveAll(v => sysFieldNames.Contains(v.ToUpper()));
            }
            return names;
        }
        /// <summary>
        /// 获取要素类的指定类型字段名
        /// </summary>
        /// <param name="featureClass"></param>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public static List<string> GetFieldsNames(this IFeatureClass featureClass, esriFieldType fieldType)
        {
            return GetFieldsNames(featureClass as ITable, fieldType);
        }


        /// <summary>
        /// 获取表格的指定类型字段名
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public static List<string> GetFieldsNames(this ITable table, esriFieldType fieldType)
        {
            List<string> names = new List<string>();
            int cnt = table.Fields.FieldCount;
            for (int i = 0; i < cnt; i++)
            {
                var field = table.Fields.get_Field(i);
                if (field.Type == fieldType)
                    names.Add(field.Name);
            }
            return names;
        }
        /// <summary>
        /// 获取要素类的所有字段别名
        /// </summary>
        /// <param name="featureClass">要素类</param>
        /// <returns></returns>
        public static List<string> GetFieldsAliasNames(this IFeatureClass featureClass)
        {
            return GetFieldsAliasNames(featureClass as ITable);
        }
        /// <summary>
        /// 获取表格的所有字段别名
        /// </summary>
        /// <param name="table">表格</param>
        /// <returns></returns>
        public static List<string> GetFieldsAliasNames(this ITable table)
        {
            List<string> names = new List<string>();
            int cnt = table.Fields.FieldCount;
            for (int i = 0; i < cnt; i++)
            {
                names.Add(table.Fields.get_Field(i).AliasName);
            }
            return names;
        }
        /// <summary>
        ///  获取表格的所有字段名称与别名键值对
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetFieldNameAndAliasName(this ITable table)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            int cnt = table.Fields.FieldCount;
            for (int i = 0; i < cnt; i++)
            {
                var field = table.Fields.get_Field(i);
                dict.Add(field.Name, field.AliasName);
            }
            return dict;
        }


        /// <summary>
        /// 查找指定字段在表格中的索引，字段不存在（索引为-1）则直接抛出异常
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static int GetFieldIndex(this ITable table, string fieldName)
        {
            int index = table.FindField(fieldName);
            if (index < 0)
                throw new Exception("在“{0}”表格中找不到字段“{1}”，请检验数据！");
            return index;
        }
        /// <summary>
        /// 查找指定字段在表格中的索引，字段不存在（索引为-1）则直接抛出异常
        /// </summary>
        /// <param name="featureClass"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static int GetFieldIndex(this IFeatureClass featureClass, string fieldName)
        {
            int index = featureClass.FindField(fieldName);
            if (index < 0)
                throw new Exception("在“{0}”图层中找不到字段“{1}”，请检验数据！");
            return index;
        }
        #endregion


        #region 是否存在指定名称的字段
        /// <summary>
        /// 判断要素类中是否存在指定字段，找不到则返回提示信息，全部都找到则返回null
        /// </summary>
        /// <param name="featureClass"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public static string CheckFieldExist(this IFeatureClass featureClass, params string[] fieldNames)
        {
            List<string> unfindFields = new List<string>();
            foreach (var fieldName in fieldNames)
            {
                if (featureClass.FindField(fieldName) < 0)
                    unfindFields.Add(fieldName);
            }
            string msg = null;
            if (unfindFields.Count > 0)
                msg = $"在“{featureClass.AliasName}”图层中找不到以下字段：{unfindFields.Aggregate((a, b) => a + "，" + b)}";
            return msg;
        }
        /// <summary>
        /// 判断表格中是否存在指定字段，找不到则返回提示信息，全部都找到则返回null
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public static string CheckFieldExist(this ITable table, params string[] fieldNames)
        {
            List<string> unfindFields = new List<string>();
            foreach (var fieldName in fieldNames)
            {
                if (table.FindField(fieldName) < 0)
                    unfindFields.Add(fieldName);
            }
            string msg = null;
            if (unfindFields.Count > 0)
                msg =
                    $"在“{(table as IObjectClass).AliasName}”表格中找不到以下字段：{unfindFields.Aggregate((a, b) => a + "," + b)}";
            return msg;
        }
        /// <summary>
        /// 判断要素类中是否存在指定字段，找不到则直接抛出异常，异常中包含找不到哪些字段的信息
        /// </summary>
        /// <param name="featureClass"></param>
        /// <param name="fieldNames"></param>
        public static void CheckFieldExistByException(this IFeatureClass featureClass, params string[] fieldNames)
        {
            var msg = CheckFieldExist(featureClass, fieldNames);
            if (msg != null)
                throw new Exception(msg);
        }
        /// <summary>
        /// 判断表格中是否存在指定字段，找不到则直接抛出异常，异常中包含找不到哪些字段的信息
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fieldNames"></param>
        public static void CheckFieldExistByException(this ITable table, params string[] fieldNames)
        {
            var msg = CheckFieldExist(table, fieldNames);
            if (msg != null)
                throw new Exception(msg);
        }
        #endregion


        #region 字段类型和中英文描述
        /// <summary>
        /// 字段类型(esriFieldType)和对应的中文文字描述的键值对
        /// </summary>
        private static Dictionary<esriFieldType, string> _fieldTypeAndCnDesriptions;
        /// <summary>
        /// 字段类型(esriFieldType)和对应的中文文字描述的键值对
        /// </summary>
        public static Dictionary<esriFieldType, string> FieldTypeAndCnDesriptions =>
            _fieldTypeAndCnDesriptions ?? (_fieldTypeAndCnDesriptions = new Dictionary<esriFieldType, string>
        {
            {esriFieldType.esriFieldTypeDouble, "双精度"},
            {esriFieldType.esriFieldTypeSingle, "浮点型"},
            {esriFieldType.esriFieldTypeInteger, "长整型"},
            {esriFieldType.esriFieldTypeSmallInteger, "短整型"},
            {esriFieldType.esriFieldTypeGeometry, "几何图形"},
            {esriFieldType.esriFieldTypeOID, "OID"},
            {esriFieldType.esriFieldTypeDate, "日期"},
            {esriFieldType.esriFieldTypeGUID, "GUID"},
            {esriFieldType.esriFieldTypeString, "文本"},
            {esriFieldType.esriFieldTypeXML, "XML"},
            {esriFieldType.esriFieldTypeRaster, "栅格"},
            {esriFieldType.esriFieldTypeBlob, "Blob"},
            {esriFieldType.esriFieldTypeGlobalID, "GlobalID"}
        });
        /// <summary>
        /// 获得字段类型的英文文字描述
        /// </summary>
        /// <returns></returns>
        public static string GetFieldTypeDesciption(this esriFieldType eFieldType)
        {
            return eFieldType.ToString().Replace("esriFieldType", "");
        }
        /// <summary>
        /// 获得字段类型的中文文字描述
        /// </summary>
        /// <returns></returns>
        public static string GetFieldTypeDesciptionCn(this esriFieldType eFieldType)
        {
            return FieldTypeAndCnDesriptions[eFieldType];
        }
        /// <summary>
        /// 根据字段类型的中文文字描述，获取字段类型
        /// </summary>
        /// <param name="fieldTypeDescriptionCn">字段类型的中文文字描述，来自<see cref="FieldTypeAndCnDesriptions"/></param>
        /// <returns></returns>
        public static esriFieldType GetFieldTypeByDesciptionCn(string fieldTypeDescriptionCn)
        {
            return FieldTypeAndCnDesriptions.FirstOrDefault(v =>
                v.Value.Contains(fieldTypeDescriptionCn) || fieldTypeDescriptionCn.Contains(v.Value)).Key;
        }
        #endregion


        #region 是否存在指定类型的字段
        /// <summary>
        /// 检查字段集合中是否存在OID字段
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static bool IsExsitOid(this IFields fields)
        {
            return IsExistFieldType(fields, esriFieldType.esriFieldTypeOID);
        }
        /// <summary>
        /// 检查字段集合中是否存在OID字段
        /// </summary>
        /// <param name="fieldsEdit"></param>
        /// <returns></returns>
        public static bool IsExsitOid(this IFieldsEdit fieldsEdit)
        {
            return IsExistFieldType(fieldsEdit, esriFieldType.esriFieldTypeOID);
        }

        /// <summary>
        /// 检查字段集合中是否存在Shape字段
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static bool IsExsitShapeField(this IFields fields)
        {
            return IsExistFieldType(fields, esriFieldType.esriFieldTypeGeometry);
        }
        /// <summary>
        /// 检查字段集合中是否存在Shape字段
        /// </summary>
        /// <param name="fieldsEdit"></param>
        /// <returns></returns>
        public static bool IsExsitShapeField(this IFieldsEdit fieldsEdit)
        {
            return IsExistFieldType(fieldsEdit, esriFieldType.esriFieldTypeGeometry);
        }

        /// <summary>
        /// 检查字段集合中是否存在指定类型的字段
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public static bool IsExistFieldType(this IFields fields, esriFieldType fieldType)
        {
            return IsExistFieldType(fields as IFieldsEdit, fieldType);
        }
        /// <summary>
        /// 检查字段集合中是否存在指定类型的字段
        /// </summary>
        /// <param name="fieldsEdit"></param>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public static bool IsExistFieldType(this IFieldsEdit fieldsEdit, esriFieldType fieldType)
        {
            for (int i = 0; i < fieldsEdit.FieldCount; i++)
            {
                if (fieldsEdit.get_Field(i).Type == fieldType)
                    return true;
            }
            return false;
        }
        #endregion
    }
}
