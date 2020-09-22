using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using WLib.ArcGis.Analysis.OnClass;
using WLib.ArcGis.Analysis.OnShape;
using WLib.ArcGis.DataCheck.Compare.Enum;
using WLib.ArcGis.DataCheck.Compare.Item;
using WLib.ArcGis.DataCheck.Compare.Plan;
using WLib.ArcGis.GeoDatabase.FeatClass;
using WLib.Data;
using WLib.Database;
using WLib.ExtProgress;

namespace WLib.ArcGis.DataCheck.Compare
{
    /// <summary>
    /// 表格或图层数据对比操作
    /// </summary>
    public class DataCompareOpt : ProLogOperation<IEnumerable<ComparePlan>, CompareResult>
    {
        public static ESpatialMatchTypes[] AllSpatialMatchTypes = System.Enum.GetValues(typeof(ESpatialMatchTypes)).Cast<ESpatialMatchTypes>().ToArray();
        /// <summary>
        /// “空间关系匹配方式”与对应的“ArcGIS空间关系枚举”键值对
        /// </summary>
        public static Dictionary<ESpatialMatchTypes, esriSpatialRelEnum> SpatialRelDict { get; } = new Dictionary<ESpatialMatchTypes, esriSpatialRelEnum>()
        {
            { ESpatialMatchTypes.Intersects, esriSpatialRelEnum.esriSpatialRelIntersects },
            { ESpatialMatchTypes.EnvelopeIntersects, esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects },
            { ESpatialMatchTypes.IndexIntersects, esriSpatialRelEnum.esriSpatialRelIndexIntersects },
            { ESpatialMatchTypes.Touches, esriSpatialRelEnum.esriSpatialRelTouches },
            { ESpatialMatchTypes.Overlaps, esriSpatialRelEnum.esriSpatialRelOverlaps },
            { ESpatialMatchTypes.Crosses, esriSpatialRelEnum.esriSpatialRelCrosses },
            { ESpatialMatchTypes.Within, esriSpatialRelEnum.esriSpatialRelWithin },
            { ESpatialMatchTypes.Contains, esriSpatialRelEnum.esriSpatialRelContains },
            { ESpatialMatchTypes.Relation, esriSpatialRelEnum.esriSpatialRelRelation },
            { ESpatialMatchTypes.InteresctMaxArea, esriSpatialRelEnum.esriSpatialRelIntersects },
            { ESpatialMatchTypes.InteresctMaxLength, esriSpatialRelEnum.esriSpatialRelIntersects },
        };
        /// <summary>
        /// 提供在数据对比过程中，数据查询或匹配失败的各类情况的处理
        /// </summary>
        public NoneMatchHelper MatchHelper { get; }
        /// <summary>
        /// 表格或图层数据对比操作
        /// </summary>
        /// <param name="name">操作名称</param>
        /// <param name="comparePlans">对比方案</param>
        public DataCompareOpt(string name, IEnumerable<ComparePlan> comparePlans) : base(name, comparePlans)
        {
            MatchHelper = new NoneMatchHelper
            {
                NoneMatchHandlers = NoneMatchHelper.DefaultNoneMatchHanlders,
                MessageHandler = (eNoneMatchHandlers, message) =>
                {
                    switch (eNoneMatchHandlers)
                    {
                        case ENoneMatchHandlers.Error: Error = message; OnDataOutput(new CompareResult(this.Name, message)); throw new Exception(message);
                        case ENoneMatchHandlers.Warnning: Warnning = message; OnDataOutput(new CompareResult(this.Name, message)); break;
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
            foreach (var comparePlan in InputData)
            {
                Info = $"------{comparePlan}------";
                switch (comparePlan)
                {
                    //单表/图层对比
                    case SelfDefaultComparePlan selfDefaultComparePlan: SelfDefaultCompare(selfDefaultComparePlan); break;
                    case SelfFieldComparePlan selfFieldComparePlan: SelfFieldCompare(selfFieldComparePlan); break;
                    case SelfSpatialComparePlan selfSpatialComparePlan: SelfSpatialCompare(selfSpatialComparePlan); break;

                    //两表/图层对比
                    case EachFieldComparePlan eachFieldComparePlan: EachFieldCompare(eachFieldComparePlan); break;
                    case EachSpatialComparePlan eachSpatialComparePlan: EachSpatialCompare(eachSpatialComparePlan); break;
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

            var dataTable = LoadDataTable(plan.TablePath, plan.WhereClause, fieldNames);
            AddSpecialColumn(plan.CompareItems, plan.TablePath, dataTable, plan.WhereClause, plan.IdField);
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

            //获取图层或表数据，与自身进行左连接，再执行数据对比操作
            var leftTable = LoadDataTable(plan.TablePath, plan.WhereClause, leftFields);
            var rightTable = LoadDataTable(plan.TablePath, plan.WhereClause2, rightFields, "$" + leftTable.TableName);
            AddSpecialColumn(plan.CompareItems, plan.TablePath, leftTable, plan.WhereClause, plan.IdField);
            AddSpecialColumn(plan.CompareItems, plan.TablePath, rightTable, plan.WhereClause2, plan.IdField, true);

            var dataTable = leftTable.Join(rightTable, plan.MatchField1, plan.MatchField2, "LeftJoin", null, null, "$");
            CompareByFields(plan, dataTable);
        }
        /// <summary>
        /// 按照空间关系匹配的方式，进行单表/图层对比
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
            EachSpatialCompare(eachSpatialComparePlan);
        }
        /// <summary>
        /// 按照字段匹配的方式，进行两个表/图层之间的对比
        /// </summary>
        /// <param name="plan"></param>
        protected virtual void EachFieldCompare(EachFieldComparePlan plan)
        {
            Info = "正在筛选需要对比的字段..";
            plan.GetCompareFields(out var leftFields, out var rightFields);

            //分别获取表格1和表格2的数据，对两个表进行左连接，再执行数据对比操作
            var leftTable = LoadDataTable(plan.TablePath, plan.WhereClause, leftFields);
            var rightTable = LoadDataTable(plan.TablePath2, plan.WhereClause2, rightFields, "$" + leftTable.TableName);
            AddSpecialColumn(plan.CompareItems, plan.TablePath, leftTable, plan.WhereClause, plan.IdField);
            AddSpecialColumn(plan.CompareItems, plan.TablePath2, rightTable, plan.WhereClause2, plan.IdField, true);

            var dataTable = leftTable.Join(rightTable, plan.MatchField1, plan.MatchField2, "LeftJoin", null, null, "$");
            CompareByFields(plan, dataTable);
        }
        /// <summary>
        /// 按照空间关系匹配的方式，两个表/图层之间的对比
        /// </summary>
        /// <param name="plan"></param>
        protected virtual void EachSpatialCompare(EachSpatialComparePlan plan)
        {
            SplitPath(plan.TablePath, out var leftWorkspacePath, out var leftClassName);
            SplitPath(plan.TablePath2, out var rightWorkspacePath, out var rightClassName);

            Info = "正在筛选需要对比的字段..";
            plan.GetCompareFields(out var leftFields, out var rightFields);
            var leftClass = FeatureClassEx.FromPath(plan.TablePath);
            var rightClass = plan.TablePath == plan.TablePath2 ? leftClass : FeatureClassEx.FromPath(plan.TablePath2);
            var leftFieldIndexs = GetFieldIndexs(leftClass, leftFields);//左表字段索引
            var rightFieldIndexs = GetFieldIndexs(rightClass, rightFields);//右表字段索引

            Info = "正在校验图层的几何类型..";
            ValidateGeometryType(plan.SpatialMatchTypes, leftClass, rightClass);

            Info = "正在获取表结构..";
            var leftDbHelper = DbHelper.GetShpMdbGdbHelper(leftWorkspacePath);
            var rightDbHelper = leftWorkspacePath == rightWorkspacePath ? leftDbHelper : DbHelper.GetShpMdbGdbHelper(rightWorkspacePath);
            var leftTable = leftDbHelper.GetDataTable($"select {string.Join(",", leftFields)} from {leftClassName} where {plan.WhereClause}", leftClass.AliasName);
            var rightTable = rightDbHelper.GetDataTable($"select {string.Join(",", rightFields)} from {rightClassName} where 1=0", "$" + rightClass.AliasName);
            rightTable.Columns.Add("@leftTable_id", leftTable.Columns[plan.IdField].DataType);
            leftDbHelper.Close();
            rightDbHelper.Close();

            Info = "正在执行数据的空间匹配...";
            var idIndex = leftClass.FindField(plan.IdField);
            leftClass.QueryFeatures(plan.WhereClause, leftFeature =>
            {
                var id = leftFeature.get_Value(idIndex);
                var rightFeatures = FilterFeature(rightClass, leftFeature, plan.SpatialMatchTypes, plan.WhereClause2);
                foreach (var rightFeature in rightFeatures)
                {
                    var rightValues = rightFieldIndexs.Select(index => rightFeature.get_Value(index)).ToList();
                    rightValues.Add(id);
                    var dataRow = rightTable.NewRow();
                    dataRow.ItemArray = rightValues.ToArray();
                }
            });

            AddSpecialColumn(plan.CompareItems, leftClass, leftTable, plan.WhereClause, plan.IdField);
            AddSpecialColumn(plan.CompareItems, rightClass, rightTable, plan.WhereClause2, plan.IdField2, true);

            var dataTable = leftTable.Join(rightTable, plan.IdField, "@leftTable_id", "LeftJoin", null, null, "$");
            CompareByFields(plan, dataTable);
        }


        #region 具体公共方法
        /// <summary>
        /// 获取指定字段的索引
        /// </summary>
        /// <param name="fieldNames"></param>
        /// <param name="featureClass"></param>
        /// <returns></returns>
        private int[] GetFieldIndexs(IFeatureClass featureClass, List<string> fieldNames)
        {
            return fieldNames.Select(fieldName =>
            {
                var index = featureClass.FindField(fieldName);
                if (index < 0) MatchHelper.NoFieldHandler();
                return index;
            }).ToArray();//需要对比的字段的索引
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tablePath"></param>
        /// <param name="workspacePath"></param>
        /// <param name="className"></param>
        private void SplitPath(string tablePath, out string workspacePath, out string className)
        {
            Info = "正在检索路径：" + tablePath;
            FeatureClassEx.SplitPath(tablePath, out workspacePath, out _, out className);
            MatchHelper.NoDbHandler(workspacePath);
            MatchHelper.NoTableHandler(tablePath, className);
        }
        /// <summary>
        /// 筛选要素类中，与指定要素图斑满足对比方案的指定空间匹配关系的要素
        /// </summary>
        /// <param name="featureClass">需要筛选要素的要素类</param>
        /// <param name="feature">进行控件匹配的图斑要素</param>
        /// <param name="spatialMatchTypes"></param>
        /// <param name="whereClause"></param>
        /// <returns>key:满足条件的要素的OID，value:满足条件的要素</returns>
        private List<IFeature> FilterFeature(IFeatureClass featureClass, IFeature feature, ESpatialMatchTypes[] spatialMatchTypes, string whereClause)
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
        private IEnumerable<IFeature> FilterFeature(IFeatureClass featureClass, IFeature feature, ESpatialMatchTypes spatialMatchType, string whereClause)
        {
            IFeature resultFeature = null;
            var geometry = feature.Shape;
            var resultFeatures = featureClass.FilterFeatures(geometry, SpatialRelDict[spatialMatchType], whereClause);
            if (spatialMatchType == ESpatialMatchTypes.InteresctMaxArea)
                resultFeature = resultFeatures.GetMaxAreaIntersectFeature(geometry, out _);
            else if (spatialMatchType == ESpatialMatchTypes.InteresctMaxLength)
                resultFeature = resultFeatures.GetMaxLengthIntersectFeature(geometry, out _);
            else
            {
                MatchHelper.NoneMatchRecordHandler(featureClass, feature, resultFeatures, spatialMatchType, whereClause);
                return resultFeatures;
            }

            MatchHelper.NoneMatchRecordHandler(featureClass, feature, resultFeature, spatialMatchType, whereClause);
            return resultFeature == null ? new IFeature[] { } : new IFeature[] { resultFeature };
        }
        /// <summary>
        /// 判断图层的几何类型是否符合空间匹配的要求
        /// </summary>
        /// <param name="featureClass"></param>
        /// <param name="eSpatialMatchTypes"></param>
        private void ValidateGeometryType(ESpatialMatchTypes[] eSpatialMatchTypes, params IFeatureClass[] featureClasses)
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
        private DataTable LoadDataTable(string tablePath, string whereClause, List<string> fieldNames, string tableName = null)
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
        private void AddSpecialColumn(CompareItemCollection compareItems, IFeatureClass featureClass, DataTable dataTable, string whereClause, string idField, bool isRightTable = false)
        {
            if (compareItems.ContainSpecialFields(out var containArea, out var containLength))
            {
                MatchHelper.NotPolygonLayerHandler(featureClass);

                var areaColumnName = isRightTable ? "$@Area" : "@Area";
                if (containArea && !dataTable.Columns.Contains(areaColumnName))
                    CaculateArea(areaColumnName, featureClass, dataTable, idField, whereClause);

                var lengthColumnName = isRightTable ? "$@Length" : "@Length";
                if (containLength && !dataTable.Columns.Contains(lengthColumnName))
                    throw new Exception("暂未实现图斑的长度计算");
            }
        }
        /// <summary>
        /// 根据对比方案是否指定面积、长度等字段，向dataTable中添加面积、长度字段并且获取字段值
        /// </summary>
        /// <param name="plan">对比方案</param>
        /// <param name="tablePath">图层或表格完整路径</param>
        /// <param name="dataTable"></param>
        private void AddSpecialColumn(CompareItemCollection compareItems, string tablePath, DataTable dataTable, string whereClause, string idField, bool isRightTable = false)
        {
            if (compareItems.ContainSpecialFields(out _, out _))
            {
                var featureClass = FeatureClassEx.FromPath(tablePath);
                AddSpecialColumn(compareItems, featureClass, dataTable, whereClause, idField, isRightTable);
                Marshal.ReleaseComObject(featureClass);
            }
        }
        /// <summary>
        /// 按照字段匹配的方式，进行单表/图层对比，对字段进行对比
        /// </summary>
        /// <param name="fieldsCompare"></param>
        /// <param name="dataTable"></param>
        /// <param name="className"></param>
        private void CompareByFields(ComparePlan plan, DataTable dataTable)
        {
            Info = $"正在比对【{dataTable.TableName}】字段值..";
            var fieldCompares = plan.CompareItems.OfType<FieldCompare>().ToArray();
            foreach (var fieldCompare in fieldCompares)
            {
                var column = new DataColumn("对比结果", typeof(bool), fieldCompare.FieldExpression);
                dataTable.Columns.Add(column);
                var dataRows = dataTable.Rows;
                var className = dataTable.TableName;
                var columnNames = dataTable.Columns.Cast<DataColumn>().Select(v => v.Caption);
                foreach (DataRow row in dataRows)
                {
                    var tableNames = dataTable.TableName.Split(',');
                    MatchHelper.NoneMatchRecordHandler(tableNames, row, column, plan.IdField);
                    if (row[column].ToString() == "False")//检查不通过，输出检查结果信息
                    {
                        var leftColumn = fieldCompare.LeftColumnName;
                        var rightColumn = fieldCompare.RightColumnName;
                        if (leftColumn == null) throw new Exception($"系统错误：没有指定要对比的字段，即{nameof(fieldCompare.LeftColumnName)}为空！");
                        if (!columnNames.Contains(leftColumn)) throw new Exception($"系统错误：在表格或图层“{className}”找不到要对比的字段“{leftColumn}”");

                        if (rightColumn == null)
                            OnDataOutput(new CompareResult(Name, $"{className}:{leftColumn}", row[leftColumn].ToString(), "", "", fieldCompare.Description));
                        else
                        {
                            if (!columnNames.Contains(rightColumn))
                                throw new Exception($"系统错误：在表格或图层“{className}”找不到要对比的字段“{leftColumn}”");

                            OnDataOutput(new CompareResult(Name, $"{className}:{leftColumn}", row[leftColumn].ToString(), $"{className}:{rightColumn}", row[rightColumn].ToString(), fieldCompare.Description));
                        }
                    }
                }
                dataTable.Columns.Remove(column);
            }
        }
        /// <summary>
        /// 按照字段匹配的方式，进行单图层对比，对面积进行对比
        /// </summary>
        /// <param name="areaCompare">面积对比信息</param>
        /// <param name="featureClass">需要对比字段或面积的要素类</param>
        /// <param name="dataTable">需要对比字段或面积的要素类属性表</param>
        /// <param name="idField">对记录进行标识的字段，一般为FID、ObjectID、单元编号、标识码等字段</param>
        private void CaculateArea(string areaColumn, IFeatureClass featureClass, DataTable dataTable, string idField, string whereClause)
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
        #endregion
    }
}
