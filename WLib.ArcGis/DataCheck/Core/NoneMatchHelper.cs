using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using WLib.ArcGis.Geometry;
using WLib.Attributes.Description;

namespace WLib.ArcGis.DataCheck.Core
{
    /// <summary>
    /// 提供在数据对比过程中，数据查询或匹配失败的各类情况的处理
    /// </summary>
    public class NoneMatchHelper
    {
        /// <summary>
        /// 创建数据查询或匹配失败的各类情况和对应默认处理方式
        /// </summary>
        /// <returns></returns>
        public static Dictionary<ENoneMatchTypes, ENoneMatchHandlers> DefaultNoneMatchHanlders { get; } = new Dictionary<ENoneMatchTypes, ENoneMatchHandlers>()
        {
            { ENoneMatchTypes.NoDb, ENoneMatchHandlers.Error },
            { ENoneMatchTypes.NoDataset, ENoneMatchHandlers.Error },
            { ENoneMatchTypes.NoTable, ENoneMatchHandlers.Error },
            { ENoneMatchTypes.NoField, ENoneMatchHandlers.Error },

            { ENoneMatchTypes.EmptyDb, ENoneMatchHandlers.Warnning },
            { ENoneMatchTypes.EmptyDataset, ENoneMatchHandlers.Warnning },
            { ENoneMatchTypes.EmptyTable, ENoneMatchHandlers.Warnning },

            { ENoneMatchTypes.ErrorLayerShapeType, ENoneMatchHandlers.Error },
            { ENoneMatchTypes.ErrorGeometryType, ENoneMatchHandlers.Error },

            { ENoneMatchTypes.NoneMatchRecord, ENoneMatchHandlers.Warnning },
            { ENoneMatchTypes.NoneMatchGeometry, ENoneMatchHandlers.Error },

            { ENoneMatchTypes.NullValue, ENoneMatchHandlers.Tips },
            { ENoneMatchTypes.NullOrEmptyValue, ENoneMatchHandlers.Tips },
            { ENoneMatchTypes.NullorWhitespaceValue, ENoneMatchHandlers.Tips },
        };
        /// <summary>
        /// 数据查询或匹配失败的各类情况和对应处理方式
        /// </summary>
        public Dictionary<ENoneMatchTypes, ENoneMatchHandlers> NoneMatchHandlers { get; set; }
        /// <summary>
        /// 数据查询或匹配失败的各类情况下的信息输出委托
        /// </summary>
        public Action<ENoneMatchHandlers, string, string> MessageHandler { get; set; }
        /// <summary>
        /// 检查名或对比方案名称
        /// </summary>
        public string CheckerName { get; set; }
        /// <summary>
        /// 提供在数据对比过程中，数据查询或匹配失败的各类情况的处理
        /// </summary>
        public NoneMatchHelper() { }


        /// <summary>
        /// 判断和处理“找不到数据库”的情况
        /// </summary>
        /// <param name="dbPath"></param>
        public void NoDbHandler(string dbPath)
        {
            if (!File.Exists(dbPath) && !Directory.Exists(dbPath))
                MessageHandler?.Invoke(NoneMatchHandlers[ENoneMatchTypes.NoDb], CheckerName, $"找不到数据库“{dbPath}”");
        }
        /// <summary>
        /// 判断和处理“找不到数据集”的情况
        /// </summary>
        public void NoDatasetHandler()
        {

        }
        /// <summary>
        /// 判断和处理“找不到表格/图层”的情况
        /// </summary>
        /// <param name="tablePath"></param>
        /// <param name="tableName"></param> 
        public bool NoTableHandler(string tablePath, string tableName)
        {
            bool result = string.IsNullOrWhiteSpace(tableName);
            if (result)
                MessageHandler?.Invoke(NoneMatchHandlers[ENoneMatchTypes.NoTable], CheckerName, $"找不到表格或图层“{tableName}”:路径“{tablePath}”不存在或从路径获取图层失败！");
            return result;
        }
        /// <summary>
        /// 判断和处理“找不到表格/图层”的情况
        /// </summary>
        /// <param name="tablePath"></param>
        /// <param name="tableName"></param>
        public bool NoTableHandler(string tablePath, object table)
        {
            if (table == null)
                MessageHandler?.Invoke(NoneMatchHandlers[ENoneMatchTypes.NoTable], CheckerName, $"无法从路径“{tablePath}”获取到表格或图层，请确保路径指向具体表格或图层");
            return table == null;
        }
        /// <summary>
        /// 判断和处理“找不到字段”的情况
        /// </summary>
        public void NoFieldHandler(string tableName, params string[] unfindFields)
        {
            if (unfindFields.Length > 0)
                MessageHandler?.Invoke(NoneMatchHandlers[ENoneMatchTypes.NoField], CheckerName, $"表格或图层“{tableName}”中找不到以下字段“{string.Join(",", unfindFields)}”");
        }


        /// <summary>
        /// 判断和处理“数据库为空”的情况
        /// </summary>
        public void EmptyDbHandler()
        {

        }
        /// <summary>
        /// 判断和处理“数据集为空”的情况
        /// </summary>
        public void EmptyDatasetHandler()
        {

        }
        /// <summary>
        /// 判断和处理“表格/图层为空”的情况
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataTable"></param>
        public void EmptyTableHandler(string tableName, DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0)
                MessageHandler?.Invoke(NoneMatchHandlers[ENoneMatchTypes.EmptyTable], CheckerName, $"表格或图层“{tableName}”是空的（空记录）");
        }


        /// <summary>
        /// 判断和处理“图层几何类型不匹配”的情况
        /// </summary>
        public void ErrorLayerShapeType(IFeatureClass featureClass1, IFeatureClass featureClass2)
        {
            if (featureClass1.ShapeType != featureClass2.ShapeType)
                MessageHandler?.Invoke(NoneMatchHandlers[ENoneMatchTypes.ErrorLayerShapeType], CheckerName,
                    $"图层“{featureClass1.AliasName}”（{featureClass1.ShapeType.GetGeometryTypeCnName()}图层），图层“{featureClass1.AliasName}”（{featureClass2.ShapeType.GetGeometryTypeCnName()}图层）几何类型不一致！");
        }
        /// <summary>
        /// 判断和处理“图层不是面图层”的情况
        /// </summary>
        public void NotPolygonLayerHandler(IFeatureClass featureClass)
        {
            if (featureClass == null || featureClass.ShapeType != esriGeometryType.esriGeometryPolygon)
                MessageHandler?.Invoke(NoneMatchHandlers[ENoneMatchTypes.ErrorLayerShapeType], CheckerName, $"图层“{featureClass.AliasName}”不是面图层");
        }


        /// <summary>
        /// 判断和处理“没有匹配的记录”的情况
        /// </summary>
        public void NoneMatchRecordHandler(string[] tableNames, DataRow dataRow, DataColumn expressionColumn, string idField)
        {
            if (dataRow[expressionColumn] == DBNull.Value)
            {
                var tableName1 = tableNames != null && tableNames.Length > 0 ? tableNames[0] : null;
                var tableName2 = tableNames != null && tableNames.Length > 1 ? tableNames[1] : tableName1;
                MessageHandler?.Invoke(NoneMatchHandlers[ENoneMatchTypes.NoneMatchRecord], CheckerName,
                    $"“{tableName1}”的记录“{idField}={dataRow[idField]}”在“{tableName2}”中找不到匹配的记录");
            }
        }
        /// <summary>
        /// 判断和处理“没有匹配的记录”的情况
        /// </summary>
        public string[] NoneMatchRecordHandler(string[] ids, string tableName1, string tableName2)
        {
            if (ids.Length > 0)
            {
                MessageHandler?.Invoke(NoneMatchHandlers[ENoneMatchTypes.NoneMatchRecord], CheckerName,
                    $"“{tableName1}”的以下记录/图斑在“{tableName2}”中找不到匹配的记录/图斑：{ids.Aggregate((a, b) => a + "," + b)}");
            }
            return ids;
        }
        /// <summary>
        /// 判断和处理“没有匹配的记录”的情况
        /// </summary>
        public void NoneMatchRecordHandler(IFeatureClass featureClass, IFeature feature, IFeature resultFeature, ESpatialMatchTypes spatialMatchType, string whereClause)
        {
            if (resultFeature == null)
                MessageHandler?.Invoke(NoneMatchHandlers[ENoneMatchTypes.NoneMatchRecord], CheckerName,
                    $"图层“{featureClass.AliasName}”中找不到与要素(OID={feature.OID})的图斑{spatialMatchType.GetDescription()}且符合过滤条件“{whereClause}”的要素");
        }
        /// <summary>
        /// 判断和处理“没有匹配的记录”的情况
        /// </summary>
        public void NoneMatchRecordHandler(IFeatureClass featureClass, IFeature feature, IEnumerable<IFeature> resultFeatures, ESpatialMatchTypes spatialMatchType, string whereClause)
        {
            if (resultFeatures == null || resultFeatures.Count() == 0)
            {
                var whereClauseMsg = string.IsNullOrWhiteSpace(whereClause) ? null : $"且符合过滤条件“{whereClause}”";
                MessageHandler?.Invoke(NoneMatchHandlers[ENoneMatchTypes.NoneMatchRecord], CheckerName,
                    $"图层“{featureClass.AliasName}”中找不到与要素(OID={feature.OID})的图斑{spatialMatchType.GetDescription()}{whereClauseMsg}的要素");
            }
        }
        /// <summary>
        /// 判断和处理“没有匹配的记录”的情况
        /// </summary>
        public void NoneMatchRecordHandler(IFeatureClass featureClass, IEnumerable<IFeature> noneCompareFeatures, ESpatialMatchTypes[] spatialMatchType, string whereClause)
        {
            if (noneCompareFeatures.Count() > 0)
            {
                var whereClauseMsg = string.IsNullOrWhiteSpace(whereClause) ? null : $"且符合过滤条件“{whereClause}”";
                var oids = noneCompareFeatures.Select(v => v.OID.ToString()).ToArray();
                var strMatchTypeMsg = spatialMatchType.Select(v => v.GetDescription()).Aggregate((a, b) => a + "、" + b);
                MessageHandler?.Invoke(NoneMatchHandlers[ENoneMatchTypes.NoneMatchRecord], CheckerName,
                    $"以下图斑无法在图层“{featureClass.AliasName}”中与之{strMatchTypeMsg}{whereClauseMsg}的要素：OID=" + oids.Aggregate((a, b) => a + ", " + b));
            }
        }

        /// <summary>
        /// 判断和处理“没有匹配的图斑”的情况
        /// </summary>
        public void NoneMatchGeometryHandler()
        {

        }


        /// <summary>
        /// 判断和处理“匹配的值为null”的情况
        /// </summary>
        public void NullValueHandler()
        {

        }
        /// <summary>
        /// 判断和处理“匹配的值为null或Empty”的情况
        /// </summary>
        public void NullOrEmptyValueHandler()
        {

        }
        /// <summary>
        /// 判断和处理“匹配的值为null、Empty或空白字符”的情况
        /// </summary>
        public void NullorWhitespaceValueHandler()
        {

        }
    }
}
