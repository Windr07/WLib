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

namespace WLib.ArcGis.GeoDatabase.Fields
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
        /// 将ArcGIS表格的字段转为自定义类FieldItem集
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="fieldNames">从表格获取的字段集（名称或别名均可），值为null时获取全部字段</param>
        /// <returns></returns>
        public static IEnumerable<FieldItem> ToFieldItems(this IFields fields, string[] fieldNames = null)
        {
            var eFields = ToEnumerable(fields);
            if (fieldNames == null || fieldNames.Length == 0)
                return eFields.Select(v => new FieldItem(v.Name, v.AliasName, v.Type));

            var filterFields = eFields.Where(v => fieldNames.Contains(v.Name) || fieldNames.Contains(v.AliasName));
            return filterFields.Select(v => new FieldItem(v.Name, v.AliasName, v.Type));
        }
        /// <summary>
        /// 将ArcGIS表格的字段转为自定义类FieldItemEx集
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="fieldNames">从表格获取的字段集（名称或别名均可），值为null时获取全部字段</param>
        /// <returns></returns>
        public static IEnumerable<FieldItemEx> ToFieldItemExs(this IFields fields, string[] fieldNames = null)
        {
            var eFields = ToEnumerable(fields);
            if (fieldNames == null || fieldNames.Length == 0)
                return eFields.Select(v => new FieldItemEx(
                    v.Name, v.AliasName, v.Type, v.Length, v.Precision, v.Scale, v.IsNullable, v.Required, v.Editable));

            var filterFields = eFields.Where(v => fieldNames.Contains(v.Name) || fieldNames.Contains(v.AliasName));
            return filterFields.Select(v => new FieldItemEx(
                v.Name, v.AliasName, v.Type, v.Length, v.Precision, v.Scale, v.IsNullable, v.Required, v.Editable));
        }


        /// <summary>
        /// 将IFields转成可枚举类型IEnumerable
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static IEnumerable<IField> ToEnumerable(this IFields fields)
        {
            for (int i = 0; i < fields.FieldCount; i++)
                yield return fields.get_Field(i);
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
        /// 创建包含ObjectID和Shape字段的字段集（ObjectID会在不同数据源中自动转换为OID/FID/OJBECTID）
        /// </summary>
        /// <param name="geometryType">几何类型</param>
        /// <param name="spatialRef">坐标系（若Shape字段所在要素类位于工作空间中，则此值不可为null，若位于要素数据集则应设置为null</param>
        /// <returns></returns>
        public static IFields CreateBaseFields(esriGeometryType geometryType, ISpatialReference spatialRef)
        {
            IFields fields = new FieldsClass();
            IFieldsEdit fieldsEdit = (IFieldsEdit)fields;
            fieldsEdit.AddField(CreateOidField());
            fieldsEdit.AddField(CreateShapeField(geometryType, spatialRef));
            return fields;
        }
        /// <summary>
        /// 创建包含ObjectID和Shape字段的字段集（ObjectID会在不同数据源中自动转换为OID/FID/OJBECTID），并且加入其他字段（可空）
        /// </summary>
        /// <param name="geometryType">几何类型</param>
        /// <param name="spatialRef">坐标系（若Shape字段所在要素类在工作空间中则此值不可为null，若位于要素数据集则应设置为null</param>
        /// <param name="otherFields">除了ObjectID和Shape字段以外的字段（可以为null）</param>
        /// <returns></returns>
        public static IFields CreateFields(esriGeometryType geometryType, ISpatialReference spatialRef, IEnumerable<IField> otherFields)
        {
            var fields = CreateBaseFields(geometryType, spatialRef);
            if (otherFields != null)
                AddFields(fields, otherFields);
            return fields;
        }
        /// <summary>
        /// 创建ObjectID字段
        /// （ObjectID会在不同数据源中自动转换为OID/FID/OJBECTID，关于OID、FID、OBJECTID参考
        ///  http://support.esrichina.com.cn/2009/1229/595.html 或
        ///  http://blog.csdn.net/yh0503/article/details/27862401）
        /// </summary>
        /// <returns></returns>
        public static IField CreateOidField()
        {
            //创建OBJECTID字段
            IField fieldOid = new FieldClass();
            IFieldEdit fieldEditOID = (IFieldEdit)fieldOid;
            fieldEditOID.Name_2 = "OBJECTID";   //OBJECTID字段名在不同数据源会自动转换为FID或OID
            fieldEditOID.AliasName_2 = "OBJECTID";
            fieldEditOID.Type_2 = esriFieldType.esriFieldTypeOID;
            return fieldOid;
        }
        /// <summary>
        /// 创建Shape字段
        /// </summary>
        /// <param name="geometryType">几何类型</param>
        /// <param name="spatialRef">坐标系（若Shape字段所在要素类在工作空间中则此值不可为null，若位于要素数据集则应设置为null</param>
        /// <returns></returns>
        public static IField CreateShapeField(esriGeometryType geometryType, ISpatialReference spatialRef)
        {
            //创建几何字段
            IField shapeField = new FieldClass();
            IFieldEdit shapeFieldEdit = (IFieldEdit)shapeField;
            shapeFieldEdit.Name_2 = "SHAPE";
            shapeFieldEdit.AliasName_2 = "SHAPE";
            shapeFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            shapeFieldEdit.GeometryDef_2 = CreateGeometryDef(geometryType, spatialRef);
            return shapeField;
        }
        /// <summary>
        /// 创建几何对象字段定义字段
        /// </summary>
        /// <param name="geometryType"></param>
        /// <param name="spatialRef"></param>
        /// <returns></returns>
        private static IGeometryDef CreateGeometryDef(esriGeometryType geometryType, ISpatialReference spatialRef)
        {
            //创建几何对象字段定义字段
            IGeometryDef geometryDef = new GeometryDefClass();
            IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;

            //指定几何对象字段属性值
            geometryDefEdit.GeometryType_2 = geometryType;
            geometryDefEdit.GridCount_2 = 1;
            geometryDefEdit.set_GridSize(0, 1000);
            if (spatialRef != null)
                geometryDefEdit.SpatialReference_2 = spatialRef;

            return geometryDef;
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
        /// 向字段集添加多个字段
        /// </summary>
        /// <param name="iFields"></param>
        /// <param name="otherFields"></param>
        public static void AddFields(this IFields iFields, IEnumerable<IField> otherFields)
        {
            var fieldsEdit = (IFieldsEdit)iFields;
            if (otherFields != null)
                foreach (var field in otherFields)
                    fieldsEdit.AddField(field);
        }
        /// <summary>
        /// 向字段集添加或修改ObjectID和拥有指定几何类型与坐标系Shape字段，若字段存在且符合要求则不添加或修改
        /// （ObjectID会在不同数据源中自动转换为OID/FID/OJBECTID）
        /// </summary>
        /// <param name="fields">字段集</param>
        /// <param name="geometryType">需要添加的Shape字段存储的几何类型</param>
        /// <param name="spatialRef">需要添加的Shape字段存储的坐标系</param>
        /// <returns></returns>
        public static IFields AddBaseFields(this IFields fields, esriGeometryType geometryType, ISpatialReference spatialRef)
        {
            IFieldsEdit fieldsEdit = (IFieldsEdit)fields;
            var oidField = fields.GetFirstFieldsByType(esriFieldType.esriFieldTypeOID);
            if (oidField == null)
                fieldsEdit.AddField(CreateOidField());

            var shapeField = fields.GetFirstFieldsByType(esriFieldType.esriFieldTypeGeometry);
            if (shapeField == null)
                fieldsEdit.AddField(CreateShapeField(geometryType, spatialRef));
            else if (shapeField.GeometryDef.GeometryType != geometryType ||
                     shapeField.GeometryDef.SpatialReference != spatialRef)
            {
                IFieldEdit shapeFieldEdit = (IFieldEdit)shapeField;
                shapeFieldEdit.GeometryDef_2 = CreateGeometryDef(geometryType, spatialRef);
            }

            return fields;
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
        public static int AddField(this IFeatureClass featureClass, string name, string aliasName, esriFieldType fieldType, int length = 0, int precision = 0, int scale = 0)
        {
            return AddField(featureClass as ITable, name, aliasName, fieldType, length, precision, scale);
        }
        /// <summary>
        /// 向已有表格添加新字段，若字段存在则不添加，并返回字段索引
        /// <para>注意：若数据源为shp，字段名长度不能超过10，否则报错</para>
        /// <para>注意：向已有表格添加字段使用IClass.AddFiled，而在创建表格时设置字段使用IFieldsEdit.AddField</para>
        /// </summary>
        /// <param name="table">操作的表格</param>
        /// <param name="name">字段名</param>
        /// <param name="aliasName">字段别名</param>
        /// <param name="fieldType">字段类型</param>
        /// <param name="length">字段长度，若为0则不设置长度（使用默认长度）</param>
        /// <param name="precision">字段精度，表示数值类型字段除小数点外的总长度，即整数位数加上小数位数</param>
        /// <param name="scale">小数位数</param>
        /// <returns>添加的字段的索引</returns>
        public static int AddField(this ITable table, string name, string aliasName, esriFieldType fieldType, int length = 0, int precision = 0, int scale = 0)
        {
            //若存在，则不需添加
            int index;
            if ((index = table.Fields.FindField(name)) > -1)
                return index;

            IField field = new FieldClass();
            try
            {
                IFieldEdit fieldEdit = field as IFieldEdit;
                fieldEdit.Name_2 = name;
                fieldEdit.AliasName_2 = aliasName;
                fieldEdit.Type_2 = fieldType;
                if (length > 0)
                    fieldEdit.Length_2 = length;
                if (precision > 0)
                    fieldEdit.Precision_2 = precision;
                if (scale > 0)
                    fieldEdit.Scale_2 = scale;

                IClass cls = table as IClass;
                cls.AddField(fieldEdit); //此处使用IClass.AddFiled方法，不使用IFieldsEdit.AddField方法
                return cls.FindField(name);
            }
            catch (Exception ex) { throw new Exception($"添加字段{name}({aliasName})失败：{ex.Message}", ex); }
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


        #region 获取字段、字段名称、别名、索引等信息
        /// <summary>
        /// 获取字段
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static IField GetField(this IFields fields, string fieldName) => fields.get_Field(fields.FindField(fieldName));
        /// <summary>
        /// 获取字段
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="fieldIndex"></param>
        /// <returns></returns>
        public static IField GetField(this IFields fields, int fieldIndex) => fields.get_Field(fieldIndex);
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
        /// 查找第一个符合指定类型的字段
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
            if (table is IFeatureClass featureClass)
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
        /// 获取表格的字段名称与别名键值对
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fieldNames">要获取的字段，值为null时获取全部字段</param>
        /// <param name="getSystemFields">是否获取系统字段(OID, SHAPE, SHAPE_LENGTH, SHAPE_AREA)</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetFieldNameAndAliasName(this ITable table, IEnumerable<string> fieldNames = null, bool getSystemFields = true)
        {
            var dict = new Dictionary<string, string>();
            var cnt = table.Fields.FieldCount;
            for (int i = 0; i < cnt; i++)
            {
                var field = table.Fields.get_Field(i);
                if (fieldNames != null && !fieldNames.Contains(field.Name))
                    continue;
                dict.Add(field.Name, field.AliasName);
            }

            if (!getSystemFields)
            {
                foreach (var fieldName in GetSystemFieldNames(table))
                    dict.Remove(fieldName);
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
        /// <summary>
        /// 查找指定字段在表格中的索引
        /// </summary>
        /// <param name="featureClass"></param>
        /// <param name="fieldNames"></param>
        /// <param name="unfindException">表示当找不到字段时是否抛出异常</param>
        /// <returns></returns>
        public static IEnumerable<int> GetFieldIndexs(this IFeatureClass featureClass, IEnumerable<string> fieldNames, bool unfindException = false)
        {
            foreach (var fieldName in fieldNames)
            {
                int index = featureClass.FindField(fieldName);
                if (unfindException && index < 0)
                    throw new Exception($"在“{featureClass.AliasName}”图层中找不到字段“{fieldName}”，请检验数据！");
                yield return index;
            }
        }
        /// <summary>
        /// 查找指定字段在表格中的索引
        /// <para>只返回能找到的字段的索引，找不到的字段将存入<paramref name="unfindFields"/>参数中</para>
        /// </summary>
        /// <param name="featureClass"></param>
        /// <param name="fieldNames"></param>
        /// <param name="unfindFields">表示找不到的字段</param>
        /// <returns></returns>
        public static List<int> GetFieldIndexs(this IFeatureClass featureClass, IEnumerable<string> fieldNames, out List<string> unfindFields)
        {
            unfindFields = new List<string>();
            var resultIndexs = new List<int>();
            foreach (var fieldName in fieldNames)
            {
                int index = featureClass.FindField(fieldName);
                if (index < 0)
                    unfindFields.Add(fieldName);
                else
                    resultIndexs.Add(index);
            }
            return resultIndexs;
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


        #region 字段类型、中英文描述、.NET类型

        /// <summary>
        /// 字段类型<see cref="esriFieldType"/>和对应的中文文字描述的键值对
        /// </summary>
        private static Dictionary<esriFieldType, string> _fieldTypeAndCnDesriptions;
        /// <summary>
        /// 字段类型<see cref="esriFieldType"/>和对应的中文文字描述的键值对
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
        /// 字段类型<see cref="esriFieldType"/>和对应的英文文字描述的键值对
        /// </summary>
        private static Dictionary<esriFieldType, string> _fieldTypeAndDesriptions;
        /// <summary>
        /// 字段类型<see cref="esriFieldType"/>和对应的英文文字描述的键值对
        /// </summary>
        public static Dictionary<esriFieldType, string> FieldTypeAndDesriptions
        {
            get
            {
                if (_fieldTypeAndDesriptions == null)
                {
                    _fieldTypeAndDesriptions = new Dictionary<esriFieldType, string>();
                    var eTypes = GetAllFieldTypes();
                    foreach (var eType in eTypes)
                        _fieldTypeAndDesriptions.Add(eType, eType.ToString().Replace("esriFieldType", ""));
                }
                return _fieldTypeAndDesriptions;
            }
        }
        /// <summary>
        /// 获取全部字段类型<see cref="esriFieldType"/>
        /// </summary>
        /// <returns></returns>
        public static esriFieldType[] GetAllFieldTypes()
        {
            return new[]
            {
                esriFieldType.esriFieldTypeDouble,
                esriFieldType.esriFieldTypeSingle,
                esriFieldType.esriFieldTypeInteger,
                esriFieldType.esriFieldTypeSmallInteger,
                esriFieldType.esriFieldTypeGeometry,
                esriFieldType.esriFieldTypeOID,
                esriFieldType.esriFieldTypeDate,
                esriFieldType.esriFieldTypeGUID,
                esriFieldType.esriFieldTypeString,
                esriFieldType.esriFieldTypeXML,
                esriFieldType.esriFieldTypeRaster,
                esriFieldType.esriFieldTypeBlob,
                esriFieldType.esriFieldTypeGlobalID,
            };
        }

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
        /// 将esriFieldType类型转成.NET的基本类型（数值类型、字符串、日期）
        /// </summary>
        /// <returns></returns>
        public static Type FieldTypeToDotNetType(this esriFieldType eFieldType)
        {
            switch (eFieldType)
            {
                case esriFieldType.esriFieldTypeDouble: return typeof(double);
                case esriFieldType.esriFieldTypeSingle: return typeof(float);
                case esriFieldType.esriFieldTypeInteger: return typeof(int);
                case esriFieldType.esriFieldTypeSmallInteger: return typeof(short);
                case esriFieldType.esriFieldTypeOID: return typeof(int);
                case esriFieldType.esriFieldTypeDate: return typeof(DateTime);
                case esriFieldType.esriFieldTypeString: return typeof(string);
                default: throw new Exception($"无法将类型{eFieldType.GetType()}转换为.NET基本类型（数值类型、字符串、日期），请单独处理类型{eFieldType.GetType()}！");
            }
        }
        /// <summary>
        /// 根据字段类型的中文文字描述，获取字段类型
        /// </summary>
        /// <param name="fieldTypeDescriptionCn">字段类型的中文文字描述，来自<see cref="FieldTypeAndCnDesriptions"/></param>
        /// <returns></returns>
        public static esriFieldType GetFieldTypeByDesciptionCn(string fieldTypeDescriptionCn)
        {
            return FieldTypeAndCnDesriptions.First(v =>
                v.Value.Contains(fieldTypeDescriptionCn) || fieldTypeDescriptionCn.Contains(v.Value)).Key;
        }
        /// <summary>
        /// 根据字段类型的文字描述，获取字段类型。文字描述支持以下类型：
        /// <para>中文描述，参考<see cref="FieldTypeAndCnDesriptions"/>，例如："双精度"</para>
        /// <para>英文描述，不区分字母大小写，例如："Double"或"double"</para>
        /// <para>枚举描述，不区分字母大小写，例如："esriFieldTypeDouble"或"esrifieldtypedouble"</para>
        /// </summary>
        /// <returns></returns>
        public static esriFieldType GetFieldTypeByDesciption(string description)
        {
            //英文描述或枚举描述
            description = description.ToLower();
            foreach (var eType in GetAllFieldTypes())
            {
                var enDescription = eType.ToString().ToLower();
                if (enDescription == description || enDescription.Replace("esrifieldtype", "") == description)
                    return eType;
            }

            //英文描述特例
            if (description == "int")
                return esriFieldType.esriFieldTypeInteger;
            else if (description == "short")
                return esriFieldType.esriFieldTypeSmallInteger;
            else if (description == "float")
                return esriFieldType.esriFieldTypeSingle;
            else if (description == "datetime" || description == "time")
                return esriFieldType.esriFieldTypeDate;

            //中文描述
            foreach (var pair in FieldTypeAndCnDesriptions)
            {
                if (pair.Value.ToLower() == description)
                    return pair.Key;
            }
            throw new Exception($"无法根据描述“{description}”找到对应的{nameof(esriFieldType)}枚举类型");
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
