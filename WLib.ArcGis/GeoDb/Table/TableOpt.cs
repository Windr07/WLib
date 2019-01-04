/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geodatabase;

namespace WLib.ArcGis.GeoDb.Table
{
    /// <summary>
    /// 提供对表格(ITable)数据的增、删、改、查、复制、检查、重命名等方法
    /// </summary>
    public static class TableOpt
    {
        #region 新增记录
        /// <summary>
        /// 在表中创建多条新记录，新记录是空的需要对其内容执行赋值操作
        /// </summary>
        /// <param name="table">操作的表</param>
        /// <param name="insertCount">创建新记录的数量</param>
        /// <param name="doActionByRows">在保存记录前，对记录执行的操作，整型参数是新增记录的索引</param>
        public static void InsertRows(this ITable table, int insertCount, Action<IRowBuffer, int> doActionByRows)
        {
            ICursor tarCursor = table.Insert(true);
            IRowBuffer tarRowBuffer = table.CreateRowBuffer();
            for (int i = 0; i < insertCount; i++)
            {
                doActionByRows(tarRowBuffer, i);
                tarCursor.InsertRow(tarRowBuffer);
            }
            tarCursor.Flush();
            Marshal.ReleaseComObject(tarRowBuffer);
            Marshal.ReleaseComObject(tarCursor);
        }
        /// <summary>
        ///  在表类中创建一条新记录，新记录是空的需要对其内容执行赋值操作
        /// </summary>
        /// <param name="table">操作的表</param>
        /// <param name="doActionByRow">在保存记录前，对记录执行的操作</param>
        public static void InsertOneRow(this ITable table, Action<IRowBuffer> doActionByRow)
        {
            ICursor tarCursor = table.Insert(true);
            IRowBuffer tarRowBuffer = table.CreateRowBuffer();
            doActionByRow(tarRowBuffer);

            tarCursor.InsertRow(tarRowBuffer);
            tarCursor.Flush();
            Marshal.ReleaseComObject(tarRowBuffer);
        }
        #endregion


        #region 删除记录
        /// <summary>
        /// 删除所有符合查询条件的记录（使用Update游标方式删除）
        /// </summary>
        /// <param name="table">操作的表</param>
        /// <param name="whereClause">查询条件，注意如果值为空则删除所有记录</param>
        public static void DeleteRows(this ITable table, string whereClause)
        {
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.WhereClause = whereClause;
            ICursor cursor = table.Update(queryFilter, false);
            IRow row = cursor.NextRow();
            while (row != null)
            {
                cursor.DeleteRow();
                row = cursor.NextRow();
            }
            Marshal.ReleaseComObject(queryFilter);
            Marshal.ReleaseComObject(cursor);
        }
        /// <summary>
        /// 根据查询条件查询要素，按判断条件执行删除操作（使用Update游标方式删除，此方法执行速度较Search方法快）
        /// </summary>
        /// <param name="table">操作的表</param>
        /// <param name="whereClause">查询条件，注意如果值为空则删除所有要素</param>
        /// <param name="isDeleteFunc">判断函数，根据返回值确定是否删除要素（返回值为True则删除）</param>
        public static void DeleteRows(this ITable table, string whereClause, Func<IRow, bool> isDeleteFunc)
        {
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.WhereClause = whereClause;
            ICursor cursor = table.Update(queryFilter, false);
            IRow row = cursor.NextRow();
            while (row != null)
            {
                if (isDeleteFunc(row))
                    cursor.DeleteRow();
                row = cursor.NextRow();
            }
            Marshal.ReleaseComObject(queryFilter);
            Marshal.ReleaseComObject(cursor);
        }
        ///<summary>  
        ///删除所有符合查询条件的记录（使用ITable.DeleteSearchedRows）
        ///</summary>  
        ///<param name="table">操作的表</param>  
        ///<param name="whereClause">查询条件，注意如果值为空则删除所有记录</param>  
        public static void DeleteRows2(this ITable table, string whereClause)
        {
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.WhereClause = whereClause;
            table.DeleteSearchedRows(queryFilter);
        }
        /// <summary>
        /// 删除所有符合查询条件的记录（执行sql）
        /// </summary>
        /// <param name="table">操作的表</param>
        /// <param name="whereClause">查询条件，注意如果值为空则删除所有记录</param>
        public static void DeleteRows3(this ITable table, string whereClause)
        {
            IDataset dataset = table as IDataset;
            whereClause = string.IsNullOrEmpty(whereClause) ? "1=1" : whereClause;
            string sql = $"delete from {dataset.Name} where {whereClause}";
            dataset.Workspace.ExecuteSQL(sql);
        }
        #endregion


        #region 更新记录
        /// <summary>
        /// 根据查询条件查询记录，对查询获取的记录执行更新操作
        /// </summary>
        /// <param name="table">查询的表</param>
        /// <param name="whereClause">查询条件，此值为null时查询所有记录</param>
        /// <param name="doActionByRows">针对记录执行的操作</param>
        /// <param name="nullRecordException">在查询不到记录时是否抛出异常，默认false</param>
        public static void UpdateRows(this ITable table, string whereClause, Action<IRow> doActionByRows, bool nullRecordException = false)
        {
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.WhereClause = whereClause;

            ICursor cursor = table.Update(queryFilter, false);
            IRow row = null;

            if (nullRecordException && table.RowCount(queryFilter) == 0)
                CheckNullToThrowException(table, null, whereClause);

            try
            {
                while ((row = cursor.NextRow()) != null)
                {
                    doActionByRows(row);
                    cursor.UpdateRow(row);
                }
            }
            catch (Exception ex)//抛出更具体的异常信息
            {
                string msgOID = row == null ? null : $"“OID = {row.OID}”的";
                string msgWhereClause = string.IsNullOrEmpty(whereClause) ? null : $"根据条件“{whereClause}”";

                throw new Exception($"在{(table as IDataset)?.Name}表格中，{msgWhereClause}更新{msgOID}记录时出错：{ex.Message}");
            }
            finally
            {
                Marshal.ReleaseComObject(cursor);
            }
        }
        #endregion


        #region 查询记录
        /// <summary>
        /// 查询符合条件的行数
        /// </summary>
        /// <param name="table">查询的表</param>
        /// <param name="whereClause">查询条件</param>
        /// <returns></returns>
        public static int QueryCount(this ITable table, string whereClause = null)
        {
            return table.RowCount(new QueryFilterClass { WhereClause = whereClause });
        }
        /// <summary>
        /// 查询获取表格行
        /// </summary>
        /// <param name="table">查询的表</param>
        /// <param name="whereClause">查询条件</param>
        /// <param name="nullRecordException">在查询不到记录时是否抛出异常，默认false</param>
        /// <returns></returns>
        public static List<IRow> QueryRows(this ITable table, string whereClause = null, bool nullRecordException = false)
        {
            List<IRow> rows = new List<IRow>();
            int isfffef = table.Fields.FindField("FBFBM");
            for (int j = 0; j < table.Fields.FieldCount; j++)
            {
                Console.WriteLine(table.Fields.Field[j].Name);
            }
            ICursor cursor = table.Search(new QueryFilterClass { WhereClause = whereClause }, false);
            IRow row = null;
            while ((row = cursor.NextRow()) != null)
            {
                rows.Add(row);
            }
            Marshal.ReleaseComObject(cursor);

            if (nullRecordException && rows.Count == 0)
                CheckNullToThrowException(table, null, whereClause);

            return rows;
        }
        /// <summary>
        /// 查找符合条件的第一条记录
        /// </summary>
        /// <param name="table">查询表</param>
        /// <param name="whereClause">查询条件</param>
        /// <param name="nullRecordException">在查询不到记录时是否抛出异常，默认false</param>
        /// <returns></returns>
        public static IRow QueryFirstRow(this ITable table, string whereClause = null, bool nullRecordException = false)
        {
            if (table == null) return null;
            ICursor cursor = table.Search(new QueryFilterClass { WhereClause = whereClause }, false);
            IRow row = cursor.NextRow();
            Marshal.ReleaseComObject(cursor);

            if (nullRecordException)
                CheckNullToThrowException(table, row, whereClause);

            return row;
        }
        /// <summary>
        ///  根据查询条件查询记录，对查询获取的记录行执行指定操作
        /// </summary>
        /// <param name="table">查询的表</param>
        /// <param name="whereClause">查询条件，此值为null时查询所有记录</param>
        /// <param name="doActionByRows">针对记录执行的操作</param>
        /// <param name="nullRecordException">在查询不到记录时是否抛出异常，默认false</param>
        public static void QueryRows(this ITable table, string whereClause, Action<IRow> doActionByRows, bool nullRecordException = false)
        {
            IQueryFilter queryFilter = new QueryFilterClass { WhereClause = whereClause };
            if (nullRecordException && table.RowCount(queryFilter) == 0)
                CheckNullToThrowException(table, null, whereClause);

            ICursor cursor = table.Search(queryFilter, false);
            IRow row;
            while ((row = cursor.NextRow()) != null)
            {
                doActionByRows(row);
            }
            Marshal.ReleaseComObject(cursor);
        }
        #endregion


        #region 复制记录
        /// <summary>
        /// 从源表格中获取数据添加到目标表格中
        /// </summary>
        /// <param name="sourceTable">源表格</param>
        /// <param name="targetTable">目标表格</param>
        /// <param name="aferInsertEach">每复制一条要素之后执行的操作</param>
        public static void CopyDataToTable(this ITable sourceTable, ITable targetTable, Action<IRowBuffer> aferInsertEach = null)
        {
            ICursor cursor = sourceTable.Search(null, true);
            IRow row = cursor.NextRow();
            ICursor tarRowCursor = targetTable.Insert(true);
            IRowBuffer tarRowBuffer;
            while (row != null)
            {
                tarRowBuffer = targetTable.CreateRowBuffer();
                IField field = new FieldClass();
                IFields fields = row.Fields;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    field = fields.get_Field(i);
                    int index = tarRowBuffer.Fields.FindField(field.Name);
                    if (index != -1 && tarRowBuffer.Fields.get_Field(index).Editable)
                    {
                        tarRowBuffer.set_Value(index, row.get_Value(i));
                    }
                }
                tarRowCursor.InsertRow(tarRowBuffer);
                aferInsertEach?.Invoke(tarRowBuffer);
                row = cursor.NextRow();
            }
            tarRowCursor.Flush();
            Marshal.ReleaseComObject(cursor);
            Marshal.ReleaseComObject(tarRowCursor);
        }
        #endregion


        #region 查询值
        /// <summary>
        ///  查找符合条件的第一条记录，并返回记录中指定字段的值
        /// </summary>
        /// <param name="table">查询的表</param>
        /// <param name="queryFiledName">查询的字段（该字段必须存在否则抛出异常）</param>
        /// <param name="whereClause">查询条件</param>
        /// <returns>查询结果的第一条记录的指定字段值，找不到则返回null</returns>
        public static object QueryFirstValue(this ITable table, string queryFiledName, string whereClause = null)
        {
            int fieldIndex = table.FindField(queryFiledName);
            if (fieldIndex < 0)
                throw new Exception("找不到字段：" + queryFiledName);

            object value = null;
            ICursor cursor = table.Search(new QueryFilterClass { WhereClause = whereClause }, true);
            IRow row = cursor.NextRow();
            if (row != null)
                value = row.get_Value(fieldIndex);

            Marshal.ReleaseComObject(cursor);
            return value;
        }
        /// <summary>
        /// 查找符合条件的第一条记录，并返回记录中指定字段的值，此值为不包含前后空白的字符串或null
        /// </summary>
        /// <param name="table">查询的表</param>
        /// <param name="queryFiledName">查询的字段（该字段必须存在否则抛出异常）</param>
        /// <param name="whereClause">查询条件</param>
        /// <returns>查询结果的第一条记录的指定字段的转成字符串的值，找不到则返回null</returns>
        public static string QueryFirstStringValue(this ITable table, string queryFiledName, string whereClause = null)
        {
            object value = QueryFirstValue(table, queryFiledName, whereClause);
            return (value == null || value == DBNull.Value) ? null : value.ToString().Trim();
        }
        /// <summary>
        /// 查找符合条件的第一条记录，并返回记录中指定若干个字段的值
        /// </summary>
        /// <param name="table">查询的表</param>
        /// <param name="queryFiledNames">查询的字段集合（当此数组为null时返回记录的全部字段值，否则字段必须存在，不存在则会出现异常）</param>
        /// <param name="whereClause">查询条件</param>
        /// <returns></returns>
        public static List<object> QueryFirstRowValues(this ITable table, string[] queryFiledNames = null, string whereClause = null)
        {
            List<object> values = new List<object>();
            var row = QueryFirstRow(table, whereClause);

            if (queryFiledNames == null)
            {
                for (int i = 0; i < row.Fields.FieldCount; i++)
                {
                    values.Add(row.get_Value(i));
                }
            }
            else
            {
                foreach (var fieldName in queryFiledNames)
                {
                    values.Add(row.get_Value(row.Fields.FindField(fieldName)));
                }
            }
            Marshal.ReleaseComObject(row);
            return values;
        }
        /// <summary>
        /// 查询符合条件的指定字段值
        /// </summary>
        /// <param name="table">查询的表</param>
        /// <param name="queryFiledName">查询的字段（该字段必须存在否则抛出异常）</param>
        /// <param name="whereClause">查询条件</param>
        /// <returns></returns>
        public static List<object> QueryValues(this ITable table, string queryFiledName, string whereClause = null)
        {
            int fieldIndex = table.FindField(queryFiledName);
            if (fieldIndex < 0)
                throw new Exception("找不到字段：" + queryFiledName);

            List<object> values = new List<object>();
            ICursor cursor = table.Search(new QueryFilterClass { WhereClause = whereClause }, true);//Search方法Recycling参数此处可以为true
            IRow row;
            while ((row = cursor.NextRow()) != null)
            {
                values.Add(row.get_Value(fieldIndex));
            }
            Marshal.ReleaseComObject(cursor);
            return values;
        }
        /// <summary>
        /// 查询符合条件的字段值组，组合成键值对（注意key字段必须符合唯一值规范）
        /// </summary>
        /// <param name="table">查询的表</param>
        /// <param name="keyFiledName">查询的字段，此作为键值对key值，查询前必须确定此字段值符合唯一值规范</param>
        /// <param name="valueFiledName">查询的字段，此作为键值对value值</param>
        /// <param name="whereClause">查询条件</param>
        /// <returns></returns>
        public static Dictionary<object, object> QueryValueDict(this ITable table, string keyFiledName, string valueFiledName, string whereClause = null)
        {
            int keyFieldIndex = table.FindField(keyFiledName);
            if (keyFieldIndex < 0) throw new Exception((table as IDataset)?.Name + "表找不到字段：" + keyFiledName);
            int valueFieldIndex = table.FindField(valueFiledName);
            if (valueFieldIndex < 0) throw new Exception((table as IDataset)?.Name + "表找不到字段：" + valueFiledName);

            Dictionary<object, object> values = new Dictionary<object, object>();
            ICursor cursor = table.Search(new QueryFilterClass { WhereClause = whereClause }, true);//Search方法Recycling参数此处可以为true
            IRow row;
            while ((row = cursor.NextRow()) != null)
            {
                object key = row.get_Value(keyFieldIndex);
                if (values.ContainsKey(key))
                {
                    if (key.ToString().Trim() == string.Empty)
                        throw new Exception($"{(table as IDataset)?.Name}表中，字段“{keyFiledName}”存在空值或空格，该字段的值要求不能重复，也不应为空！");
                    else
                        throw new Exception($"{(table as IDataset)?.Name}表中，要求值不能重复的字段“{keyFiledName}”，出现了重复的值“{key}”！");
                }
                values.Add(key, row.get_Value(valueFieldIndex));
            }
            Marshal.ReleaseComObject(cursor);
            return values;
        }
        /// <summary>
        /// 查询符合条件的字段值组，组合成键值对（注意key字段必须符合唯一值规范）
        /// </summary>
        /// <param name="table">查询的表</param>
        /// <param name="keyFiledName">查询的字段，此作为键值对key值，查询前必须确定此字段值符合唯一值规范</param>
        /// <param name="valueFiledName">查询的字段，此作为键值对value值</param>
        /// <param name="whereClause">查询条件</param>
        /// <returns></returns>
        public static Dictionary<string, string> QueryValueStringDict(this ITable table, string keyFiledName, string valueFiledName, string whereClause = null)
        {
            int keyFieldIndex = table.FindField(keyFiledName);
            if (keyFieldIndex < 0) throw new Exception("找不到字段：" + keyFiledName);
            int valueFieldIndex = table.FindField(valueFiledName);
            if (valueFieldIndex < 0) throw new Exception("找不到字段：" + valueFiledName);

            Dictionary<string, string> values = new Dictionary<string, string>();
            ICursor cursor = table.Search(new QueryFilterClass { WhereClause = whereClause }, true);//Search方法Recycling参数此处可以为true
            IRow row;
            while ((row = cursor.NextRow()) != null)
            {
                string key = row.get_Value(keyFieldIndex).ToString();
                if (values.ContainsKey(key))
                {
                    if (key.Trim() == string.Empty)
                        throw new Exception($"{(table as IDataset).Name}表中，字段“{keyFiledName}”存在空值或空格，该字段的值要求不能重复，也不应为空！");
                    else
                        throw new Exception($"{(table as IDataset).Name}表中，要求值不能重复的字段“{keyFiledName}”，出现了重复的值“{key}”！");
                }
                values.Add(key, row.get_Value(valueFieldIndex).ToString());
            }
            Marshal.ReleaseComObject(cursor);
            return values;
        }
        /// <summary>
        /// 查询符合条件的字段值组，组合成键值对（注意key字段必须符合唯一值规范）允许字段位空的且获取唯一值，
        /// </summary>
        /// <param name="table">查询的表</param>
        /// <param name="keyFiledName">查询的字段，此作为键值对key值，查询前必须确定此字段值符合唯一值规范</param>
        /// <param name="valueFiledName">查询的字段，此作为键值对value值</param>
        /// <param name="whereClause">查询条件</param>
        /// <returns></returns>
        public static Dictionary<string, string> QueryValueStringDict2(this ITable table, string keyFiledName, string valueFiledName, string whereClause = null)
        {
            int keyFieldIndex = table.FindField(keyFiledName);
            if (keyFieldIndex < 0) throw new Exception("找不到字段：" + keyFiledName);
            int valueFieldIndex = table.FindField(valueFiledName);
            if (valueFieldIndex < 0) throw new Exception("找不到字段：" + valueFiledName);

            Dictionary<string, string> values = new Dictionary<string, string>();
            ICursor cursor = table.Search(new QueryFilterClass { WhereClause = whereClause }, true);//Search方法Recycling参数此处可以为true
            IRow row;
            while ((row = cursor.NextRow()) != null)
            {
                string key = row.get_Value(keyFieldIndex).ToString().Trim();
                if (values.ContainsKey(key) || key == string.Empty)
                    continue;
                values.Add(key, row.get_Value(valueFieldIndex).ToString());
            }
            Marshal.ReleaseComObject(cursor);
            return values;
        }
        #endregion


        #region 数据源
        //当表格是从地图文档中获取（(map as ITableCollection).Table[0]）
        //且表格没有正确关联数据源时，下列三个方法会报错，暂未找到合适的接口处理此类情况，
        //表格没有正确关联数据源时应直接移除这些表格(ITableCollection.RemoveAllTables或RemoveTable)，然后再添加

        /// <summary>
        /// 获取表格所属的工作空间（IWorkspace）
        /// </summary>
        /// <param name="table">表格，此处不能把要素类(IFeatureClass)等当成表格处理</param>
        /// <returns></returns>
        public static IWorkspace GetWorkspace(this ITable table)
        {
            return (table as IDataset)?.Workspace;
        }
        /// <summary>
        /// 获取表格所属的工作空间的路径（IWorkspace）
        /// </summary>
        /// <param name="table">表格，此处不能把要素类(IFeatureClass)等当成表格处理</param>
        /// <returns></returns>
        public static string GetWorkspacePathName(this ITable table)
        {
            return (table as IDataset)?.Workspace.PathName;
        }
        /// <summary>
        /// 获取表格的完整路径（eg: C:\xxx.mdb\xx表）
        /// </summary>
        /// <param name="table">表格，此处不能把要素类(IFeatureClass)等当成表格处理</param>
        /// <returns></returns>
        public static string GetSourcePath(this ITable table)
        {
            IDataset dataset = (IDataset)table;
            return dataset.Workspace.PathName + "\\" + dataset.Name;
        }
        #endregion


        #region 修改表格名称、别名
        /// <summary>
        /// 修改表格别名
        /// </summary>
        /// <param name="table">表格</param>
        /// <param name="newAliasName">新表格别名</param>
        public static void RenameTableAliasName(this ITable table, string newAliasName)
        {
            IClassSchemaEdit2 classSchemaEdit2 = table as IClassSchemaEdit2;
            classSchemaEdit2.AlterAliasName(newAliasName);
        }
        /// <summary>
        /// 修改表格名称以及别名
        /// （修改成功的条件：①需要Advanced级别的License权限，②表格不能被其他程序锁定）
        /// </summary> 
        /// <param name="table">表格</param>
        ///<param name="newName">新表格名</param>
        ///<param name="newAliasName">新表格别名</param>
        ///<returns>修改成功返回True,否则False</returns>
        public static bool RenameTableName(this ITable table, string newName, string newAliasName = null)
        {
            IDataset ds = table as IDataset;
            bool isRename = false;
            string oldAliasName = (table as IObjectClass).AliasName, oldName = ds.Name;
            try
            {
                if (!string.IsNullOrEmpty(newAliasName))
                    RenameTableAliasName(table, newAliasName);

                if (ds.CanRename())
                {
                    ds.Rename(newName);
                    isRename = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"修改表格名称失败（{oldName}，别名：{oldAliasName}，改为：{newName}，别名：{newAliasName}）：\r\n{ex.Message}");
            }
            return isRename;
        }
        #endregion


        /// <summary>
        /// 通过IDataStatistics获取表格指定字段所有值（唯一值）
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fieldName">字段名</param>
        /// <param name="whereClause">条件语句</param>
        /// <returns></returns>
        public static List<string> GetFieldValuesByDataStatistics(this ITable table, string fieldName, string whereClause = null)
        {
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.SubFields = fieldName;
            queryFilter.WhereClause = whereClause;
            ICursor cursor = table.Search(queryFilter, true);
            IDataStatistics dataStatistics = new DataStatisticsClass();
            dataStatistics.Field = fieldName;
            dataStatistics.Cursor = cursor;
            System.Collections.IEnumerator enumerator = dataStatistics.UniqueValues;
            enumerator.Reset();
            var uniqueValueList = new List<string>();
            while (enumerator.MoveNext())
            {
                uniqueValueList.Add(enumerator.Current?.ToString());
            }
            uniqueValueList.Sort();
            return uniqueValueList;
        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="table"></param>
        /// <param name="sql"></param>
        public static void ExecuteSql(this ITable table, string sql)
        {
            var workspace = ((IDataset)table).Workspace;
            var workspaceProperties = (IWorkspaceProperties)workspace;
            var workspaceProperty = workspaceProperties.get_Property(esriWorkspacePropertyGroupType.esriWorkspacePropertyGroup,
                (int)esriWorkspacePropertyType.esriWorkspacePropCanExecuteSQL);
            if (!workspaceProperty.IsSupported)
                throw new Exception("当前数据源不支持执行Workspace.ExecuteSQL的方式处理数据");
            workspace.ExecuteSQL(sql);
        }
        /// <summary>
        /// 记录(row)为空时，抛出包含具体提示信息的异常
        /// </summary>
        /// <param name="row"></param>
        /// <param name="table"></param>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public static void CheckNullToThrowException(this ITable table, IRow row, string whereClause)
        {
            if (row != null) return;

            if (string.IsNullOrEmpty(whereClause))
                throw new Exception($"在{(table as IDataset)?.Name}表格中，找不到记录！");
            else
                throw new Exception($"在{(table as IDataset)?.Name}表格中，找不到“{whereClause}”的记录！");
        }
    }
}
