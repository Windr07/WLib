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
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.GeoDatabase.Fields;

namespace WLib.ArcGis.Data
{
    /// <summary>
    /// ArcGIS数据与.Net数据的转换（包括ArcGIS数据与DataTable互转、ArcGIS字段类型与.Net字段类型互转等）
    /// </summary>
    public static class DataTableConvert
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
        public static DataTable CreateDataTableScheme(this ITable iTable, string tableName, IEnumerable<string> fieldNames = null, bool ignoredUnExistField = true)
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
                var dataColumn = new DataColumn(field.Name, TypeConvert.ParseFieldType(field.Type))
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
        /// <param name="nameToAliasNameDict">字段名(作为DataTable列的columnName)及别名(作为DataTable列的Caption)的键值对，用于指定将ITable哪些字段填充到DataTable中</param>
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
                var dataColumn = new DataColumn(field.Name, TypeConvert.ParseFieldType(field.Type))
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
        /// <param name="iTable">ArcGIS ITable对象</param>
        /// <param name="fieldNames">指定DataTable包含的字段，如果为null则获取全部字段</param>
        /// <param name="tableName">DataTable表名称，如果为null则获取iTable表名称</param>
        /// <param name="whereClause">查询条件，根据此条件从ITable筛选数据到DataTable</param>
        /// <param name="startRow">分页查询的起始记录行，即从表中的第startRow条记录开始到第endRow记录作为当前分页的记录</param>
        /// <param name="endRow">分页查询的结束记录行</param>
        /// <returns></returns>
        public static DataTable CreateDataTable(this ITable iTable,
            IEnumerable<string> fieldNames = null, string tableName = null, string whereClause = null, int startRow = 1, int endRow = int.MaxValue)
        {
            var dict = iTable.GetFieldNameAndAliasName(fieldNames);
            return CreateDataTableEx(iTable, dict, tableName, whereClause, startRow, endRow);
        }
        /// <summary>
        /// 将ITable数据转成DataTable
        /// </summary>
        /// <param name="iTable">ArcGIS ITable对象</param>
        /// <param name="nameToAliasNamesDict">字段名(作为DataTable列的columnName)及别名(作为DataTable列的Caption)的键值对，用于指定将ITable哪些字段填充到DataTable中</param>
        /// <param name="tableName"></param>
        /// <param name="whereClause">查询条件，根据此条件从ITable筛选数据到DataTable</param>
        /// <param name="startRow">分页查询的起始记录行，即从表中的第startRow条记录开始到第endRow记录作为当前分页的记录</param>
        /// <param name="endRow">分页查询的结束记录行</param>
        /// <returns></returns>
        public static DataTable CreateDataTableEx(this ITable iTable,
            Dictionary<string, string> nameToAliasNamesDict, string tableName = null, string whereClause = null, int startRow = 1, int endRow = int.MaxValue)
        {
            tableName = tableName ?? (iTable as IDataset)?.Name;
            var dataTable = CreateDataTableScheme(iTable, tableName, nameToAliasNamesDict);
            var cursor = iTable.Search(new QueryFilterClass { WhereClause = whereClause }, true);

            int curRow = 0;
            IRow row;
            while ((row = cursor.NextRow()) != null)
            {
                curRow++;
                if (curRow < startRow) continue;
                if (curRow > endRow) break;
                AddDataToTable(dataTable, row);
            }
            Marshal.ReleaseComObject(cursor);
            return dataTable;
        }
        #endregion


        #region IFeatureClass转DataTable
        /// <summary>
        /// 将IFeatureClass指定字段数据转成DataTable
        /// </summary>
        /// <param name="featureClass"></param>
        /// <param name="fieldNames">指定字段列表，若值为null则查询全部字段</param>
        /// <param name="whereClause">查询条件，根据此条件从IFeatureClass筛选数据到DataTable</param>
        /// <param name="startRow">分页查询的起始记录行，即从IFeatureClass中的第startRow条记录开始到第endRow记录作为当前分页的记录</param>
        /// <param name="endRow">分页查询的结束记录行</param>
        /// <returns></returns>
        public static DataTable CreateDataTable(this IFeatureClass featureClass,
            IEnumerable<string> fieldNames, string whereClause = null, string tableName = null, int startRow = 1, int endRow = int.MaxValue)
        {
            var dict = (featureClass as ITable).GetFieldNameAndAliasName(fieldNames);
            return CreateDataTableEx(featureClass, dict, whereClause, tableName, startRow, endRow);
        }
        /// <summary>
        /// 将IFeatureClass指定字段数据转成DataTable
        /// </summary>
        /// <param name="featureClass"></param>
        /// <param name="nameToAliasNamesDict">字段名及别名（作为DataTable列标题）的键值对，用于指定将ITable哪些字段填充到DataTable中</param>
        /// <param name="whereClause">查询条件，根据此条件从IFeatureClass筛选数据到DataTable</param>
        /// <param name="startRow">分页查询的起始记录行，即从IFeatureClass中的第startRow条记录开始到第endRow记录作为当前分页的记录</param>
        /// <param name="endRow">分页查询的结束记录行</param>
        /// <returns></returns>
        public static DataTable CreateDataTableEx(this IFeatureClass featureClass,
            Dictionary<string, string> nameToAliasNamesDict, string whereClause = null, string tableName = null, int startRow = 1, int endRow = int.MaxValue)
        {
            var iTable = featureClass as ITable;
            tableName = tableName ?? featureClass.AliasName;
            var dataTable = CreateDataTableScheme(iTable, tableName, nameToAliasNamesDict).ChangedFieldToString(featureClass.ShapeFieldName);
            var fieldNames = nameToAliasNamesDict.Keys;
            var geoTypeString = TypeConvert.GetGeoTypeStr(featureClass.ShapeType);

            int curRow = 0;
            IFeature feature;
            var featureCursor = featureClass.Search(new QueryFilterClass { WhereClause = whereClause }, false);
            while ((feature = featureCursor.NextFeature()) != null)
            {
                curRow++;
                if (curRow < startRow) continue;
                if (curRow > endRow) break;
                var values = GetFeatureValues(feature, fieldNames, geoTypeString).ToArray();
                dataTable.Rows.Add(values);
            }
            Marshal.ReleaseComObject(featureCursor);
            return dataTable;
        }
        /// <summary>
        /// 获取要素的指定字段的值，特殊字段值将转为字符串（Blob和Geometry转为字符串）
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="fieldNames"></param>
        /// <param name="geoTypeString"></param>
        /// <returns></returns>
        private static IEnumerable<object> GetFeatureValues(this IFeature feature, IEnumerable<string> fieldNames, string geoTypeString)
        {
            foreach (var fieldName in fieldNames)
            {
                int index = feature.Fields.FindField(fieldName);
                if (index == -1) continue;
                var fieldType = feature.Fields.get_Field(index).Type;
                switch (fieldType)
                {
                    //当图层类型为Anotation时，要素类中会有esriFieldTypeBlob类型的数据，其存储的是标注内容，如此情况需将对应的字段值设置为Element
                    case esriFieldType.esriFieldTypeBlob: yield return "Element"; break;
                    case esriFieldType.esriFieldTypeGeometry: yield return geoTypeString; break;
                    default: yield return feature.get_Value(index); break;
                }
            }
        }
        /// <summary>
        /// 将指定列转为字符串类型
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldName"></param>
        private static DataTable ChangedFieldToString(this DataTable dataTable, params string[] fieldNames)
        {
            foreach (DataColumn column in dataTable.Columns)
            {
                if (fieldNames.Contains(column.ColumnName) || fieldNames.Contains(column.Caption))
                    column.DataType = typeof(string);
            }
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
                var row = rows.ElementAt(j);
                object[] values = new object[row.Fields.FieldCount];
                for (int i = 0; i < row.Fields.FieldCount; i++)
                    values[i] = row.get_Value(i).ToString();

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
                values[i] = row.get_Value(i);

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
            var exsitFields = new List<string>();//共有字段的名称
            var exsitFieldsIndex = new List<int>();//共有字段的索引
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
            Marshal.ReleaseComObject(cursor);
        }
    }
}
