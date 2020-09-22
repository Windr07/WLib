/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using WLib.ArcGis.Analysis.OnClass;
using WLib.ArcGis.Analysis.OnShape;
using WLib.ArcGis.DataCheck.Compare.Item;
using WLib.ArcGis.DataCheck.Compare.Plan;
using WLib.ArcGis.DataCheck.Core;
using WLib.ArcGis.GeoDatabase.FeatClass;
using WLib.Data;
using WLib.Database;
using static WLib.ArcGis.DataCheck.Compare.FieldCompare;
using static WLib.ArcGis.DataCheck.Core.SpatialMatchTypesHelper;

namespace WLib.ArcGis.DataCheck.Compare
{
    /// <summary>
    /// 表格或图层数据对比操作
    /// </summary>
    public class DataCompareChecker : Checker
    {
        /// <summary>
        /// 对比方案
        /// </summary>
        public List<ComparePlan> ComparePlans { get; set; } = new List<ComparePlan>();
        /// <summary>
        /// 提供在数据对比过程中，数据查询或匹配失败的各类情况的处理
        /// </summary>
        public NoneMatchHelper MatchHelper { get; protected set; }
        /// <summary>
        /// 表格或图层数据对比操作
        /// </summary>
        /// <param name="name">操作名称</param>
        /// <param name="comparePlans">对比方案</param>
        public DataCompareChecker(string name, IEnumerable<ComparePlan> comparePlans = null) : base(name)
        {
            if (comparePlans != null)
                ComparePlans = comparePlans.ToList();

            MatchHelper = new NoneMatchHelper
            {
                NoneMatchHandlers = NoneMatchHelper.DefaultNoneMatchHanlders,
                MessageHandler = (eNoneMatchHandlers, checkerName, message) =>
                {
                    switch (eNoneMatchHandlers)
                    {
                        case ENoneMatchHandlers.Error: throw new Exception(message);
                        case ENoneMatchHandlers.Warnning: Warnning = "▲" + message; OnDataOutput(new CompareRecord(checkerName, message, EErrorLevel.警告)); break;
                        case ENoneMatchHandlers.Tips: Info = message; break;
                        case ENoneMatchHandlers.Ignore: break;
                    }
                }
            };
        }


        /// <summary>
        /// 表格或图层数据对比操作
        /// </summary>
        protected override void MainOperation()
        {
            foreach (var comparePlan in ComparePlans)
            {
                try
                {
                    Info = $"------------{comparePlan.GetAllInfo()}-------------";
                    MatchHelper.CheckerName = comparePlan.Name;
                    switch (comparePlan)
                    {
                        case SelfDefaultComparePlan selfDefaultComparePlan: SelfDefaultCompare(selfDefaultComparePlan); break;
                        case SelfFieldComparePlan selfFieldComparePlan: SelfFieldCompare(selfFieldComparePlan); break;
                        case SelfSpatialComparePlan selfSpatialComparePlan: SelfSpatialCompare(selfSpatialComparePlan); break;
                        case EachFieldComparePlan eachFieldComparePlan: EachFieldCompare(eachFieldComparePlan); break;
                        case EachSpatialComparePlan eachSpatialComparePlan: EachSpatialCompare(eachSpatialComparePlan); break;
                    }
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    Info = Error = "✘" + message;
                    OnDataOutput(new CompareRecord(MatchHelper.CheckerName, message, EErrorLevel.异常));
                }
            }
        }
        /// <summary>
        /// 单个表格或图层自身信息的对比
        /// </summary>
        /// <param name="plan"></param>
        protected virtual void SelfDefaultCompare(SelfDefaultComparePlan plan)
        {
            Info = "正在筛选需要对比的字段..";
            plan.GetCompareFields(out var fieldNames, out var _);
            Info = $"对比字段：{string.Join(",", fieldNames)}";

            var dataTable = LoadDataTable(plan.TablePath, plan.WhereClause, fieldNames);
            AddCaculateColumn(plan.CompareItems, plan.TablePath, dataTable, plan.WhereClause, plan.IdField);
            CompareByFields(plan, dataTable);
        }
        /// <summary>
        /// 按照字段匹配的方式，进行单表/图层对比
        /// </summary>
        /// <param name="plan"></param>
        protected virtual void SelfFieldCompare(SelfFieldComparePlan plan)
        {
            Info = "正在筛选需要对比的字段..";
            plan.GetCompareFields(out var leftFields, out var rightFields);
            Info = $"对比字段：左表：{string.Join(",", leftFields)}  右表：{string.Join(",", rightFields)}";

            //获取图层或表数据，与自身进行左连接，再执行数据对比操作
            var leftTable = LoadDataTable(plan.TablePath, plan.WhereClause, leftFields);
            var rightTable = LoadDataTable(plan.TablePath, plan.WhereClause2, rightFields, "$" + leftTable.TableName);

            //创建和计算特殊字段
            AddCaculateColumn(plan.CompareItems, plan.TablePath, leftTable, plan.WhereClause, plan.IdField);
            AddCaculateColumn(plan.CompareItems, plan.TablePath, rightTable, plan.WhereClause2, plan.IdField);

            //判断匹配字段是否为表达式，若为表达式则创建新字段存储表达式
            string matchField1 = plan.MatchField1, matchField2 = plan.MatchField2;
            if (plan.MatchField1_IsExpression) leftTable.Columns.Add(new DataColumn { ColumnName = matchField1 = "@matchField1", Expression = plan.MatchField1 });
            if (plan.MatchField2_IsExpression) leftTable.Columns.Add(new DataColumn { ColumnName = matchField2 = "@matchField2", Expression = plan.MatchField2 });

            Info = $"正在对[{leftTable.TableName}]与自身进行连接..";
            var dataTable = leftTable.Join(rightTable, matchField1, matchField2, "LeftJoin", null, null, "$");
            RemoveNoneMatchRecord(dataTable, $"[${matchField2}]", plan.IdField);
            CompareByFields(plan, dataTable);
        }
        /// <summary>
        /// 按照空间关系匹配的方式，进行单图层对比
        /// </summary>
        /// <param name="plan"></param>
        protected virtual void SelfSpatialCompare(SelfSpatialComparePlan plan)
        {
            var eachSpatialComparePlan = new EachSpatialComparePlan()
            {
                TablePath = plan.TablePath,
                TablePath2 = plan.TablePath,
                WhereClause = plan.WhereClause,
                WhereClause2 = plan.WhereClause2,
                IdField = plan.IdField,
                IdField2 = plan.IdField,
                CompareItems = plan.CompareItems,
                NoneMatchHandlers = plan.NoneMatchHandlers,
                SpatialMatchTypes = plan.SpatialMatchTypes,
            };
            EachSpatialCompare(eachSpatialComparePlan, true);
        }
        /// <summary>
        /// 按照字段匹配的方式，进行两个表/图层之间的对比
        /// </summary>
        /// <param name="plan">两个表格或图层之间，通过字段进行匹配的对比的方案</param>
        protected virtual void EachFieldCompare(EachFieldComparePlan plan)
        {
            Info = "正在筛选需要对比的字段..";
            plan.GetCompareFields(out var leftFields, out var rightFields);
            Info = $"对比字段：左表：{string.Join(",", leftFields)}  右表：{string.Join(",", rightFields)}";

            //获取要对比的左表和右表的数据
            var leftTable = LoadDataTable(plan.TablePath, plan.WhereClause, leftFields);
            var rightTable = string.IsNullOrEmpty(plan.TablePath2) ?
                plan.Table2 :
                LoadDataTable(plan.TablePath2, plan.WhereClause2, rightFields, "$" + leftTable.TableName);

            //创建和计算特殊字段
            AddCaculateColumn(plan.CompareItems, plan.TablePath, leftTable, plan.WhereClause, plan.IdField);
            AddCaculateColumn(plan.CompareItems, plan.TablePath2, rightTable, plan.WhereClause2, plan.IdField2);

            //判断匹配字段是否为表达式，若为表达式则创建新字段存储表达式
            string matchField1 = plan.MatchField1, matchField2 = plan.MatchField2;
            if (plan.MatchField1_IsExpression) leftTable.Columns.Add(new DataColumn { ColumnName = matchField1 = "@matchField1", Expression = plan.MatchField1 });
            if (plan.MatchField2_IsExpression) leftTable.Columns.Add(new DataColumn { ColumnName = matchField2 = "@matchField2", Expression = plan.MatchField2 });

            Info = $"正在连接[{leftTable.TableName}]和[{leftTable.TableName}]..";
            var dataTable = leftTable.Join(rightTable, matchField1, matchField2, "LeftJoin", null, null, "$");
            RemoveNoneMatchRecord(dataTable, $"[${matchField2}]", plan.IdField);
            CompareByFields(plan, dataTable);
        }
        /// <summary>
        /// 按照空间关系匹配的方式，两个图层之间的对比
        /// </summary>
        /// <param name="plan">两个图层之间，通过空间关系进行匹配的对比的方案</param>
        /// <param name="isSelftCompare">在<see cref="SelfSpatialCompare"/>方法中调用本方法时，该值应当传参为true，代表实际是单图层对比</param>
        protected virtual void EachSpatialCompare(EachSpatialComparePlan plan, bool isSelftCompare = false)
        {
            SplitPath(plan.TablePath, out var leftWorkspacePath, out var leftClassName);
            SplitPath(plan.TablePath2, out var rightWorkspacePath, out var rightClassName);

            Info = "正在筛选需要对比的字段..";
            plan.GetCompareFields(out var leftFields, out var rightFields);
            Info = $"对比字段：左表：{string.Join(",", leftFields)}  右表：{string.Join(",", rightFields)}";

            Info = "正在根据路径获取图层..";
            var leftClass = FeatureClassEx.FromPath(plan.TablePath);
            var rightClass = plan.TablePath == plan.TablePath2 ? leftClass : FeatureClassEx.FromPath(plan.TablePath2);
            MatchHelper.NoTableHandler(plan.TablePath, leftClass);
            MatchHelper.NoTableHandler(plan.TablePath2, rightClass);
            MatchHelper.ErrorLayerShapeType(leftClass, rightClass);

            Info = "正在校验图层的几何类型..";
            ValidateGeometryType(plan.SpatialMatchTypes, leftClass, rightClass);

            Info = $"正在获取[{leftClassName}]和[{rightClassName}]的表结构..";
            var whereClause = string.IsNullOrWhiteSpace(plan.WhereClause) ? null : "where " + plan.WhereClause;
            var leftTable = GetDataTable(leftWorkspacePath, leftFields, leftClassName, whereClause, plan.IdField);
            var rightTable = GetDataTable(rightWorkspacePath, rightFields, rightClassName, $"where {plan.IdField2} is null", plan.IdField2);
            rightTable.Columns.Add("@leftTable_id", leftTable.Columns[plan.IdField].DataType);

            Info = "正在执行数据的空间匹配...";
            var idIndex = leftClass.FindField(plan.IdField);
            var rightFieldIndexs = GetFieldIndexs(rightClass, rightFields);//右表字段索引
            var noneCompareFeatures = new List<IFeature>();
            leftClass.QueryFeatures(plan.WhereClause, leftFeature =>
            {
                var id = leftFeature.get_Value(idIndex);
                var rightFeatures = FilterFeature(rightClass, leftFeature, plan.SpatialMatchTypes, plan.WhereClause2, isSelftCompare);
                foreach (var rightFeature in rightFeatures)
                {
                    var rightValues = rightFieldIndexs.Select(index => rightFeature.get_Value(index)).ToList();
                    rightValues.Add(id);
                    var dataRow = rightTable.NewRow();
                    dataRow.ItemArray = rightValues.ToArray();
                    rightTable.Rows.Add(dataRow);
                }
                if (rightFeatures.Count == 0) noneCompareFeatures.Add(leftFeature);
            });

            AddCaculateColumn(plan.CompareItems, leftClass, leftTable, plan.WhereClause, plan.IdField);
            AddCaculateColumn(plan.CompareItems, rightClass, rightTable, plan.WhereClause2, plan.IdField2);

            Info = $"正在连接[{leftTable.TableName}]和[{leftTable.TableName}]..";
            var dataTable = leftTable.Join(rightTable, plan.IdField, "@leftTable_id", "LeftJoin", null, null, "$");
            RemoveNoneMatchRecord(dataTable, "[$@leftTable_id]", plan.IdField);
            CompareByFields(plan, dataTable);
        }


        #region 具体公共方法
        /// <summary>
        /// 获取指定字段的索引
        /// </summary>
        /// <param name="fieldNames"></param>
        /// <param name="featureClass"></param>
        /// <returns></returns>
        protected int[] GetFieldIndexs(IFeatureClass featureClass, List<string> fieldNames)
        {
            return fieldNames.Select(fieldName =>
            {
                var index = featureClass.FindField(fieldName);
                if (index < 0) MatchHelper.NoFieldHandler(featureClass.AliasName, fieldName);
                return index;
            }).ToArray();//需要对比的字段的索引
        }
        /// <summary>
        /// 从完整的表格或图层路径中，获取工作空间路径、表或图层名，判断工作空间和表是否存在
        /// </summary>
        /// <param name="tablePath"></param>
        /// <param name="workspacePath"></param>
        /// <param name="className"></param>
        protected void SplitPath(string tablePath, out string workspacePath, out string className)
        {
            Info = "正在检索路径：" + tablePath;
            if (string.IsNullOrWhiteSpace(tablePath))
                MatchHelper.NoDbHandler(tablePath);
            FeatureClassEx.SplitPath(tablePath, out workspacePath, out _, out className);
            MatchHelper.NoDbHandler(workspacePath);
            MatchHelper.NoTableHandler(tablePath, className);
        }
        /// <summary>
        /// 查询获取工作空间指定表格、指定字段、指定筛选条件的数据，存入DataTable
        /// </summary>
        /// <param name="workspacePath"></param>
        /// <param name="fields"></param>
        /// <param name="tableName"></param>
        /// <param name="whereClause"></param>
        /// <param name="idField"></param>
        /// <returns></returns>
        protected DataTable GetDataTable(string workspacePath, List<string> fields, string tableName, string whereClause, string idField)
        {
            //创建DbHelper
            var dbHelper = DbHelper.GetShpMdbGdbHelper(workspacePath);

            //检查字段是否存在
            var emptyTable = dbHelper.GetDataTable($"select * from {tableName} where {idField} is null");//通过“where idField is null”条件获取一张空表
            var allFields = emptyTable.Columns.Cast<DataColumn>().Select(v => v.ColumnName).ToArray();
            var unfindFields = fields.Except(allFields).ToArray();
            MatchHelper.NoFieldHandler(tableName, unfindFields);

            //执行查询
            var sql = $"select {string.Join(",", fields)} from {tableName} {whereClause}";
            var dataTable = dbHelper.GetDataTable(sql, tableName);
            dbHelper.Close();
            return dataTable;
        }
        /// <summary>
        /// 筛选要素类中，与指定要素图斑满足对比方案的指定空间匹配关系的要素
        /// </summary>
        /// <param name="featureClass">需要筛选要素的要素类</param>
        /// <param name="feature">进行控件匹配的图斑要素</param>
        /// <param name="spatialMatchTypes"></param>
        /// <param name="whereClause"></param>
        /// <param name="isSelftCompare"></param>
        /// <returns>key:满足条件的要素的OID，value:满足条件的要素</returns>
        protected List<IFeature> FilterFeature(IFeatureClass featureClass, IFeature feature, ESpatialMatchTypes[] spatialMatchTypes, string whereClause, bool isSelftCompare = false)
        {
            var dict = new Dictionary<int, IFeature>();
            foreach (var spatialMatchType in spatialMatchTypes)
            {
                var filterFeatures = new List<IFeature>();
                foreach (var tmpSpatialMatchType in AllSpatialMatchTypes)
                {
                    if ((spatialMatchType & tmpSpatialMatchType) == tmpSpatialMatchType)
                        filterFeatures.AddRange(FilterFeature(featureClass, feature, tmpSpatialMatchType, whereClause));
                }
                foreach (var tmpFeature in filterFeatures)
                {
                    if (!dict.Keys.Contains(tmpFeature.OID))
                        dict.Add(tmpFeature.OID, tmpFeature);
                }
            }
            if (isSelftCompare)
                dict.Remove(feature.OID);
            return dict.Values.ToList();
        }
        /// <summary>
        /// 筛选要素类中，与指定图形满足指定空间匹配关系的要素
        /// </summary>
        /// <param name="featureClass"></param>
        /// <param name="geometry"></param>
        /// <param name="spatialMatchType"></param>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        protected IEnumerable<IFeature> FilterFeature(IFeatureClass featureClass, IFeature feature, ESpatialMatchTypes spatialMatchType, string whereClause)
        {
            IFeature resultFeature;
            var geometry = feature.Shape;
            var resultFeatures = featureClass.FilterFeatures(geometry, SpatialRelDict[spatialMatchType], whereClause);
            if (spatialMatchType == ESpatialMatchTypes.InteresctMaxArea)
                resultFeature = resultFeatures.GetMaxAreaIntersectFeature(geometry, out _);
            else if (spatialMatchType == ESpatialMatchTypes.InteresctMaxLength)
                resultFeature = resultFeatures.GetMaxLengthIntersectFeature(geometry, out _);
            else
            {
                return resultFeatures;
            }

            return resultFeature == null ? new IFeature[] { } : new IFeature[] { resultFeature };
        }
        /// <summary>
        /// 判断图层的几何类型是否符合空间匹配的要求
        /// </summary>
        /// <param name="featureClass"></param>
        /// <param name="eSpatialMatchTypes"></param>
        protected void ValidateGeometryType(ESpatialMatchTypes[] eSpatialMatchTypes, params IFeatureClass[] featureClasses)
        {
            foreach (var featureClass in featureClasses)
            {
                foreach (var spatialMatchType in eSpatialMatchTypes)
                {
                    if ((spatialMatchType & ESpatialMatchTypes.InteresctMaxArea) == ESpatialMatchTypes.InteresctMaxArea)
                    {
                        if (featureClass.ShapeType != esriGeometryType.esriGeometryPolygon)
                            throw new Exception($"图层“{featureClass.AliasName}”不是面图层，不能获取相交面积最大的图斑！");
                    }
                    if ((spatialMatchType & ESpatialMatchTypes.InteresctMaxLength) == ESpatialMatchTypes.InteresctMaxLength)
                    {
                        if (featureClass.ShapeType != esriGeometryType.esriGeometryPolygon && featureClass.ShapeType != esriGeometryType.esriGeometryPolyline)
                            throw new Exception($"图层“{featureClass.AliasName}”不是面图层，不能获取相交长度最大的图斑！");
                    }
                }
            }
        }
        /// <summary>
        /// 加载指定的表格中指定字段、指定过滤条件的数据
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        protected DataTable LoadDataTable(string tablePath, string whereClause, List<string> fieldNames, string tableName = null)
        {
            SplitPath(tablePath, out var workspacePath, out var tmpTableName);

            Info = $"正在加载图层或表格数据：{tablePath}..";
            var dbHelper = DbHelper.GetShpMdbGdbHelper(workspacePath);
            var strWhereClause = string.IsNullOrWhiteSpace(whereClause) ? null : $"where {whereClause}";
            var dataTable = dbHelper.GetDataTable($"select {string.Join(",", fieldNames)} from {tmpTableName} {strWhereClause}", tableName ?? tmpTableName);
            MatchHelper.EmptyTableHandler(tmpTableName, dataTable);
            dbHelper.Close();

            return dataTable;
        }
        /// <summary>
        /// 根据对比方案是否指定面积、长度等字段，向dataTable中添加面积、长度字段并且获取字段值
        /// </summary>
        /// <param name="plan">对比方案</param>
        /// <param name="tablePath">图层或表格完整路径</param>
        /// <param name="dataTable"></param> 
        protected void AddCaculateColumn(CompareItemCollection compareItems, IFeatureClass featureClass, DataTable dataTable, string whereClause, string idField)
        {
            Info = $"判断“{dataTable.TableName}”是否需要额外计算字段..";
            if (compareItems.ContainCaculateFields(out var containArea, out var containLength, out var containCount))
            {
                var areaColumnName = _AREA;
                if (containArea && !dataTable.Columns.Contains(areaColumnName))
                {
                    MatchHelper.NotPolygonLayerHandler(featureClass);
                    CaculateArea(areaColumnName, featureClass, dataTable, idField, whereClause);
                }

                var lengthColumnName = _LENGTH;
                if (containLength && !dataTable.Columns.Contains(lengthColumnName))
                {
                    MatchHelper.NotPolygonLayerHandler(featureClass);
                    throw new Exception("暂未实现图斑的长度计算");
                }

                var countColumnName = _COUNT;
                if (containCount && !dataTable.Columns.Contains(countColumnName))
                    CaculateCount(countColumnName, dataTable);
            }
        }
        /// <summary>
        /// 根据对比方案是否指定面积、长度、记录数等特殊字段，向dataTable中添加面积、长度、记录数字段并且计算字段值
        /// </summary>
        /// <param name="compareItems"></param>
        /// <param name="tablePath"></param>
        /// <param name="dataTable"></param>
        /// <param name="whereClause"></param>
        /// <param name="idField"></param>
        /// <param name="isRightTable"></param>
        protected void AddCaculateColumn(CompareItemCollection compareItems, string tablePath, DataTable dataTable, string whereClause, string idField)
        {
            if (compareItems.ContainCaculateFields(out _, out _, out _))
            {
                var featureClass = FeatureClassEx.FromPath(tablePath);
                AddCaculateColumn(compareItems, featureClass, dataTable, whereClause, idField);
                Marshal.ReleaseComObject(featureClass);
            }
        }
        /// <summary>
        /// 按照字段匹配的方式，进行单表/图层对比，对字段进行对比
        /// </summary>
        /// <param name="fieldsCompare"></param>
        /// <param name="dataTable"></param>
        /// <param name="className"></param>
        protected void CompareByFields(ComparePlan plan, DataTable dataTable)
        {
            Info = $"正在比对【{dataTable.TableName}】字段值..";
            var fieldCompares = plan.CompareItems.OfType<FieldCompare>().ToArray();
            var dataRows = dataTable.Rows;
            var tableNames = dataTable.TableName.Split(',');
            var leftTableName = tableNames[0];
            var rightTableName = tableNames.Length > 1 ? tableNames[1] : tableNames[0];
            var columnNames = dataTable.Columns.Cast<DataColumn>().Select(v => v.Caption.ToLower());
            OnProgressChanged(0, fieldCompares.Length);
            foreach (var fieldCompare in fieldCompares)
            {
                Info = "对比" + fieldCompare.FieldExpression;
                var column = new DataColumn("对比结果", typeof(bool), fieldCompare.FieldExpression);
                dataTable.Columns.Add(column);
                var tmpDataRows = dataTable.Select("DYBH = '4453020032100000013'").ToArray();
                foreach (DataRow row in tmpDataRows)
                {
                    var expValue = row[column];
                    if (expValue.ToString() == "False")//检查不通过，输出检查结果信息
                    {
                        var leftColumn = fieldCompare.LeftColumnName;
                        var rightColumn = fieldCompare.RightColumnName;
                        if (leftColumn == null) throw new Exception($"系统错误：没有指定要对比的字段，即{nameof(fieldCompare.LeftColumnName)}为空！");
                        if (!columnNames.Contains(leftColumn.ToLower())) throw new Exception($"系统错误：在表格或图层“{leftTableName}”找不到要对比的字段“{leftColumn}”");

                        if (rightColumn == null)
                            OnDataOutput(new CompareRecord(plan.Name, $"{leftTableName}:{leftColumn}", row[leftColumn].ToString(), "", "", fieldCompare.Description));
                        else
                        {
                            if (!columnNames.Contains(rightColumn.ToLower()))
                                throw new Exception($"系统错误：在表格或图层“{rightTableName}”找不到要对比的字段“{rightColumn}”");

                            OnDataOutput(new CompareRecord(plan.Name, $"{leftTableName}:{leftColumn}", row[leftColumn].ToString(), $"{rightTableName}:{rightColumn}", row[rightColumn].ToString(), fieldCompare.Description));
                        }
                    }
                    else if (expValue == DBNull.Value)
                    {
                        var id = row[plan.IdField].ToString();
                    }
                }
                OnProgressAdd();
                dataTable.Columns.Remove(column);
            }
        }
        /// <summary>
        /// 添加面积字段，计算图斑面积
        /// </summary>
        /// <param name="areaColumn">面积字段名</param>
        /// <param name="featureClass">需要对比字段或面积的要素类</param>
        /// <param name="dataTable">需要对比字段或面积的要素类属性表</param>
        /// <param name="idField">对记录进行标识的字段，一般为FID、ObjectID、单元编号、标识码等字段</param>
        protected void CaculateArea(string areaColumn, IFeatureClass featureClass, DataTable dataTable, string idField, string whereClause)
        {
            Info = $"正在添加【{dataTable.TableName}】的面积列..";
            dataTable.Columns.Add(new DataColumn(areaColumn, typeof(double)));
            if (string.IsNullOrWhiteSpace(idField))
                throw new Exception($"ID字段（参数{nameof(idField)}）不能为空！请对{nameof(ComparePlan)}.{nameof(ComparePlan.IdField)}进行赋值！");

            Info = $"正在计算【{dataTable.TableName}】的面积..";
            var dict = featureClass.QueryFeatures(whereClause,
                feature => feature.get_Value(feature.Fields.FindField(idField)),//key: FID/ObjectID/单元编号/标识码等
                feature =>//Value: 面积
                {
                    var geometry = feature.Shape;
                    if (geometry == null || geometry.IsEmpty) return 0.0;
                    else return (feature.Shape as IArea).Area;
                }
            );
            foreach (DataRow row in dataTable.Rows)
                row[areaColumn] = dict[row[idField]];
        }
        /// <summary>
        /// 添加记录数字段，赋值记录数
        /// </summary>
        /// <param name="countColumn"></param>
        /// <param name="dataTable"></param>
        protected void CaculateCount(string countColumn, DataTable dataTable)
        {
            Info = $"正在添加【{dataTable.TableName}】的计数列..";
            dataTable.Columns.Add(new DataColumn(countColumn, typeof(int)));
            int count = dataTable.Rows.Count;
            foreach (DataRow row in dataTable.Rows)
                row[countColumn] = count;
        }
        /// <summary>
        /// 根据右表匹配字段是否为空，判断左表记录在右表中找到匹配，处理“没有匹配的记录”的情况同时移除这些记录
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="matchField"></param>
        protected void RemoveNoneMatchRecord(DataTable dataTable, string matchField, string idField)
        {
            var tableNames = dataTable.TableName.Split(',');
            var leftTableName = tableNames[0];
            var rightTableName = tableNames.Length > 1 ? tableNames[1] : tableNames[0];

            var dataRows = dataTable.Select($"{matchField} is null");
            var ids = dataRows.Select(row => row[idField].ToString()).ToArray();
            MatchHelper.NoneMatchRecordHandler(ids, leftTableName, rightTableName);
            foreach (DataRow dataRow in dataRows)
                dataTable.Rows.Remove(dataRow);
        }
        #endregion
    }
}
