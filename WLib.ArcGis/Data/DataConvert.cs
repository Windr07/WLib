/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using WLib.ArcGis.GeoDb.Fields;

namespace WLib.ArcGis.Data
{
    /// <summary>
    /// ArcGIS数据与.Net数据的转换（包括ArcGIS数据与DataTable互转、ArcGIS字段类型与.Net字段类型互转等）
    /// </summary>
    public static class DataConvert
    {
        #region 创建空DataTable
        /// <summary>
        /// 根据ITable字段创建一个只含指定字段的空DataTable
        /// </summary>
        /// <param name="iTable">ArcGIS ITable对象</param>
        /// <param name="tableName">DataTable表名</param>
        /// <param name="fieldNames">指定将ITable哪些字段填充到DataTable中，值为null时获取ITable的全部字段</param>
        /// <param name="ignoredUnExistField">指定在ITable找不到的字段是否跳过，找不到时，True为跳过，False将抛出异常</param>
        /// <returns></returns>
        public static DataTable CreateDataTableScheme(this ITable iTable, string tableName, List<string> fieldNames = null, bool ignoredUnExistField = true)
        {
            var dataTable = new DataTable(tableName);
            if (fieldNames == null)
                fieldNames = iTable.GetFieldsNames();

            foreach (var fieldName in fieldNames)
            {
                int index = iTable.Fields.FindField(fieldName);
                if (index == -1)
                {
                    if (!ignoredUnExistField)
                        continue;

                    throw new Exception($"“{(iTable as IDataset)?.Name}”中找不到字段“{fieldName}”！");
                }
                var field = iTable.Fields.get_Field(index);
                var dataColumn = new DataColumn(field.Name, ParseFieldType(field.Type))
                {
                    Caption = field.AliasName, //字段别名
                    DefaultValue = field.DefaultValue//字段默认值
                };
                dataTable.Columns.Add(dataColumn);
            }
            return dataTable;
        }
        /// <summary>
        /// 根据ITable字段创建一个只含指定字段的空DataTable
        /// </summary>
        /// <param name="iTable">ArcGIS ITable对象</param>
        /// <param name="tableName">DataTable表名</param>
        /// <param name="nameToAliasNameDict">字段名及别名（作为DataTable列标题）的键值对，用于指定将ITable哪些字段填充到DataTable中</param>
        /// <param name="ignoredUnExistField">指定在ITable找不到的字段是否跳过，找不到时，True为跳过，False将抛出异常</param>
        /// <returns></returns>
        public static DataTable CreateDataTableScheme(this ITable iTable, string tableName, Dictionary<string, string> nameToAliasNameDict, bool ignoredUnExistField = true)
        {
            DataTable dataTable = new DataTable(tableName);
            foreach (var pair in nameToAliasNameDict)
            {
                int index = iTable.Fields.FindField(pair.Key);
                if (index == -1)
                {
                    if (ignoredUnExistField)
                        continue;

                    throw new Exception($"“{(iTable as IDataset)?.Name}”中找不到字段“{pair.Value}”！");
                }
                var field = iTable.Fields.get_Field(index);
                var dataColumn = new DataColumn(field.Name, ParseFieldType(field.Type))
                {
                    Caption = pair.Value, //字段别名
                    DefaultValue = field.DefaultValue //字段默认值
                };
                dataTable.Columns.Add(dataColumn);
            }
            return dataTable;
        }
        #endregion


        #region ITable转DataTable
        /// <summary>
        /// 将ITable数据转成DataTable
        /// </summary>
        /// <param name="iTable"></param>
        /// <param name="whereClause">查询条件，根据此条件从ITable筛选数据到DataTable</param>
        /// <returns></returns>
        public static DataTable CreateDataTable(this ITable iTable, string whereClause = null)
        {
            var dataTable = CreateDataTableScheme(iTable, (iTable as IDataset)?.Name);
            var cursor = iTable.Search(new QueryFilterClass { WhereClause = whereClause }, false);
            IRow row;
            while ((row = cursor.NextRow()) != null)
            {
                AddDataToTable(dataTable, row);
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
            return dataTable;
        }
        /// <summary>
        /// 将ITable数据转成DataTable
        /// </summary>
        /// <param name="iTable">ArcGIS ITable对象</param>
        /// <param name="fieldNames">指定DataTable包含的字段</param>
        /// <param name="whereClause">查询条件，根据此条件从ITable筛选数据到DataTable</param>
        /// <returns></returns>
        public static DataTable CreateDataTable(this ITable iTable, List<string> fieldNames, string whereClause = null)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            fieldNames.ForEach(v => dict.Add(v, v));
            return CreateDataTable(iTable, dict, whereClause);
        }
        /// <summary>
        /// 将ITable数据转成DataTable
        /// </summary>
        /// <param name="iTable">ArcGIS ITable对象</param>
        /// <param name="nameToAliasNamesDict">指定DataTable包含的字段（名称和别名键值对）</param>
        /// <param name="whereClause">查询条件，根据此条件从ITable筛选数据到DataTable</param>
        /// <returns></returns>
        public static DataTable CreateDataTable(this ITable iTable, Dictionary<string, string> nameToAliasNamesDict, string whereClause = null)
        {
            var dataTable = CreateDataTableScheme(iTable, (iTable as IDataset)?.Name, nameToAliasNamesDict);
            var cursor = iTable.Search(new QueryFilterClass { WhereClause = whereClause }, false);
            IRow row;
            while ((row = cursor.NextRow()) != null)
            {
                AddDataToTable(dataTable, row);
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
            return dataTable;
        }
        #endregion


        #region IFeatureClass转DataTable

        /// <summary>
        /// 将要素IFeatureClass指定字段数据转成DataTable
        /// </summary>
        /// <param name="featureClass"></param>
        /// <param name="fieldNames">指定字段列表</param>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public static DataTable CreateDataTable(this IFeatureClass featureClass, List<string> fieldNames, string whereClause = null)
        {
            ITable iTable = (ITable)featureClass;
            string geoTypeString = GetGeoTypeStr(featureClass.ShapeType);
            DataTable dataTable = CreateDataTableScheme(iTable, featureClass.AliasName, fieldNames);

            IFeatureCursor featureCursor = featureClass.Search(new QueryFilterClass { WhereClause = whereClause }, false);
            IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                object[] values = new object[fieldNames.Count];
                for (int i = 0; i < fieldNames.Count; i++)
                {
                    int idex = feature.Fields.FindField(fieldNames[i]);
                    if (idex == -1)
                        continue;
                    IField field = feature.Fields.get_Field(idex);
                    string value;
                    //当图层类型为Anotation时，要素类中会有esriFieldTypeBlob类型的数据，
                    //其存储的是标注内容，如此情况需将对应的字段值设置为Element
                    if (field.Type == esriFieldType.esriFieldTypeBlob)
                        value = "Element";
                    else if (field.Type == esriFieldType.esriFieldTypeGeometry)
                        value = geoTypeString;
                    else
                        value = feature.get_Value(idex).ToString().Trim();
                    values[i] = value;
                }
                dataTable.Rows.Add(values);
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(featureCursor);
            return dataTable;
        }

        /// <summary>
        /// 将要素IFeatureClass指定字段数据转成DataTable
        /// </summary>
        /// <param name="featureClass"></param>
        /// <param name="nameToAliasNamesDict">字段名及别名（作为DataTable列标题）的键值对，用于指定将ITable哪些字段填充到DataTable中</param>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public static DataTable CreateDataTable(this IFeatureClass featureClass, Dictionary<string, string> nameToAliasNamesDict, string whereClause = null)
        {
            ITable iTable = featureClass as ITable;
            string geoTypeString = GetGeoTypeStr(featureClass.ShapeType);
            DataTable result = CreateDataTableScheme(iTable, featureClass.AliasName, nameToAliasNamesDict);

            IFeatureCursor featureCursor = featureClass.Search(new QueryFilterClass { WhereClause = whereClause }, false);
            IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                object[] values = new object[nameToAliasNamesDict.Count];
                int i = 0;
                foreach (var pair in nameToAliasNamesDict)
                {
                    int idex = feature.Fields.FindField(pair.Key);
                    if (idex == -1)
                        continue;
                    IField field = feature.Fields.get_Field(idex);
                    string value = "";
                    //当图层类型为Anotation时，要素类中会有esriFieldTypeBlob类型的数据，
                    //其存储的是标注内容，如此情况需将对应的字段值设置为Element
                    if (field.Type == esriFieldType.esriFieldTypeBlob)
                        value = "Element";
                    else if (field.Type == esriFieldType.esriFieldTypeGeometry)
                        value = geoTypeString;
                    else
                        value = feature.get_Value(idex).ToString().Trim();
                    values[i++] = value;
                }
                result.Rows.Add(values);
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(featureCursor);
            return result;
        }
        /// <summary>
        /// 将要素IFeatureClass指定条件、指定行数的数据转成DataTable
        /// </summary>
        /// <param name="featureClass"></param>
        /// <param name="tableName"></param>
        /// <param name="whereClause"></param>
        /// <param name="maxRow">数据数量</param>
        /// <returns></returns>
        public static DataTable CreateDataTable(this IFeatureClass featureClass, string tableName = "", string whereClause = null, int maxRow = int.MaxValue)
        {
            ITable table = (ITable)featureClass;
            string geoTypeString = GetGeoTypeStr(featureClass.ShapeType);
            DataTable result = CreateDataTableScheme(table, tableName);
            ICursor featureCursor = table.Search(new QueryFilterClass { WhereClause = whereClause }, false);
            int rownum = 0;
            IRow iRow = featureCursor.NextRow();
            while (iRow != null)
            {
                object[] values = new object[iRow.Fields.FieldCount];
                for (int i = 0; i < iRow.Fields.FieldCount; i++)
                {
                    object value;
                    //当图层类型为Anotation时，要素类中会有esriFieldTypeBlob类型的数据，
                    //其存储的是标注内容，如此情况需将对应的字段值设置为Element
                    if (iRow.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeBlob)
                        value = "Element";
                    else if (iRow.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                        value = geoTypeString;
                    else
                        value = iRow.get_Value(i);
                    values[i] = value;
                }
                result.Rows.Add(values);
                rownum++;
                if (rownum > maxRow) break;
                iRow = featureCursor.NextRow();
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(featureCursor);
            return result;
        }
        /// <summary>
        /// 将要素IFeatureClass数据转成DataTable，在最后增加面积列并列出面积
        /// </summary>
        /// <param name="featureClass">要素集</param>
        /// <param name="areaColumnName">在DataTable加上面积列并计算面积，此为面积列的列名</param>
        /// <returns></returns>
        public static DataTable CreateDataTableCalArea(this IFeatureClass featureClass, string areaColumnName)
        {
            ITable table = featureClass as ITable;
            DataTable dataTable = CreateDataTableScheme(table, "");
            //图斑面积
            DataColumn areaDc = new DataColumn(areaColumnName, typeof(double));//, string areaColumnName
            dataTable.Columns.Add(areaDc);
            IFeatureCursor featureCursor = featureClass.Search(null, false);
            IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                DataRow tempRow = dataTable.NewRow();
                for (int i = 0; i < table.Fields.FieldCount; i++)
                {
                    tempRow[i] = feature.get_Value(i).ToString();
                }
                tempRow[areaColumnName] = ((feature.Shape as IPolygon) as IArea).Area.ToString();//图斑面积 
                dataTable.Rows.Add(tempRow);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(feature);
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(featureCursor);
            return dataTable;
        }
        #endregion


        #region IRow数据插入DataTable
        /// <summary>
        /// 添加数据到DataTable中，IRow字段必须与dataTable相同
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="rows"></param>
        public static void AddDataToTable(DataTable dataTable, IEnumerable<IRow> rows)
        {
            for (int j = 0; j < rows.Count(); j++)
            {
                IRow row = rows.ElementAt(j);
                object[] values = new object[row.Fields.FieldCount];
                for (int i = 0; i < row.Fields.FieldCount; i++)
                {
                    values[i] = row.get_Value(i).ToString();
                }
                dataTable.Rows.Add(values);
            }
        }
        /// <summary>
        /// 添加数据到DataTable中，IRow字段必须与dataTable相同
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="row"></param>
        public static void AddDataToTable(DataTable dataTable, IRow row)
        {
            if (row == null) return;
            object[] values = new object[row.Fields.FieldCount];
            for (int i = 0; i < row.Fields.FieldCount; i++)
            {
                values[i] = row.get_Value(i);
            }
            dataTable.Rows.Add(values);
        }
        /// <summary>
        /// 添加数据到DataTable中，选取IRow与dataTable共有的字段填写到dataTable中
        /// </summary>
        /// <param name="row"></param>
        /// <param name="dataTable"></param>
        public static void AddDataToTable2(DataTable dataTable, IRow row)
        {
            if (row == null) return;
            object[] values = new object[dataTable.Columns.Count];
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                int index = row.Fields.FindField(dataTable.Columns[i].ColumnName);
                values[i] = index == -1 ? null : row.get_Value(index);
            }
            dataTable.Rows.Add(values);
        }
        #endregion


        /// <summary>
        /// 将DataTable的数据写入ITable中
        /// </summary>
        /// <param name="dataTable">System.Data.DataTable对象，其字段名与ITable对象的字段名或字段别名一致的数据将写入ITable对象</param>
        /// <param name="table">ESRI.ArcGIS.Geodatabase.ITable对象</param>
        public static void DataTable2ITable(DataTable dataTable, ITable table)
        {
            //获取在ITable和System.Data.DataTable共有的字段
            List<string> exsitFields = new List<string>();//共有字段的名称
            List<int> exsitFieldsIndex = new List<int>();//共有字段的索引
            for (int i = 0; i < table.Fields.FieldCount; i++)
            {
                var field = table.Fields.get_Field(i);
                if (dataTable.Columns.Contains(field.Name))
                {
                    exsitFields.Add(field.Name);
                    exsitFieldsIndex.Add(i);
                }
                else if (dataTable.Columns.Contains(field.AliasName))
                {
                    exsitFields.Add(field.AliasName);
                    exsitFieldsIndex.Add(i);
                }
            }

            //将System.Data.DataTable的数据写入ESRI的ITable中
            ICursor cursor = table.Insert(true);
            IRowBuffer rowBuffer;
            int rowIndex = 0;
            try
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    rowBuffer = table.CreateRowBuffer();
                    rowIndex = i + 1;
                    var dataRow = dataTable.Rows[i];
                    for (int j = 0; j < exsitFields.Count; j++)
                    {
                        var name = exsitFields[j];
                        var value = dataRow[name];
                        if (value == null || value.ToString() == "")
                            continue;
                        rowBuffer.set_Value(exsitFieldsIndex[j], value);
                    }
                    cursor.InsertRow(rowBuffer);
                }
                cursor.Flush();
            }
            catch (Exception ex)
            {
                throw new Exception($"将DataTable的第{rowIndex}行写入“{(table as IDataset)?.Name}”表中时出现错误：{ex.Message}");
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
        }
        /// <summary>
        /// 获取几何类型字符（eg:"Line","Point","Polygon","Polyline","Null"等等）
        /// </summary>
        /// <param name="geoType"></param>
        /// <returns></returns>
        public static string GetGeoTypeStr(esriGeometryType geoType)
        {
            return geoType.ToString().Replace("esriGeometry", "");
        }
        /// <summary>
        /// 将ArcGIS字段类型转换成.Net相应的数据类型
        /// </summary>
        /// <param name="fieldType">字段类型</param>
        /// <returns></returns>
        public static Type ParseFieldType(esriFieldType fieldType)
        {
            switch (fieldType)
            {
                case esriFieldType.esriFieldTypeSmallInteger:
                    return typeof(int);
                case esriFieldType.esriFieldTypeInteger:
                    return typeof(int);
                case esriFieldType.esriFieldTypeDouble:
                    return typeof(double);
                case esriFieldType.esriFieldTypeSingle:
                    return typeof(Single);
                default:
                    return typeof(string);
            }

        }
    }
}
