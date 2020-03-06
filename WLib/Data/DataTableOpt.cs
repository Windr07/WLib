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
using System.Text;

namespace WLib.Data
{
    /// <summary>
    /// <see cref="DataTable"/>的创建、转置、连接、转换等操作
    /// </summary>
    public static class DataTableOpt
    {
        #region 构造表格
        /// <summary>
        /// 构造表格（DataTable）
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnNames">列</param>
        /// <returns></returns>
        public static DataTable InitDataTable(string tableName, params string[] columnNames)
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = tableName;
            foreach (var name in columnNames)
            {
                dataTable.Columns.Add(name);
            }
            return dataTable;
        }
        /// <summary>
        /// 构造表格（DataTable），表格列只读
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnNames">列</param>
        /// <returns></returns>
        public static DataTable InitReadOnlyDataTable(string tableName, params string[] columnNames)
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = tableName;
            foreach (var name in columnNames)
            {
                DataColumn dataColumn = new DataColumn(name);
                dataColumn.ReadOnly = true;
                dataTable.Columns.Add(dataColumn);
            }
            return dataTable;
        }
        #endregion


        #region 转置表格
        /// <summary>
        /// 转置DataTable，即将行列对换（注意dataTable的行数不能太多）
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <returns></returns>
        public static DataTable TransposeDataTable(this DataTable sourceTable)
        {
            const string STR_NAME = "字段";
            const string STR_VALUE = "值";
            DataTable newTable = new DataTable();
            DataColumn fieldNameCol = new DataColumn(STR_NAME, typeof(string));
            fieldNameCol.Caption = STR_NAME;
            fieldNameCol.ReadOnly = true;
            newTable.Columns.Add(fieldNameCol);

            if (sourceTable.Rows.Count == 1)
                newTable.Columns.Add(STR_VALUE, typeof(object));
            else
            {
                for (int i = 0; i < sourceTable.Rows.Count; i++)
                {
                    newTable.Columns.Add(STR_VALUE + (i + 1).ToString(), typeof(object));
                }
            }

            foreach (DataColumn col in sourceTable.Columns)
            {
                DataRow newRow = newTable.NewRow();
                newRow[STR_NAME] = col.ColumnName;
                for (int i = 0; i < sourceTable.Rows.Count; i++)
                {
                    newRow[i + 1] = sourceTable.Rows[i][col];
                }
                newTable.Rows.Add(newRow);
            }
            return newTable;
        }
        /// <summary>
        /// 转置DataTable指定行数据，即将行列对换
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public static DataTable TransposeDataTable(this DataRow dataRow)
        {
            return TransposeDataTable(dataRow.Table);
        }
        #endregion


        #region 表连接
        /// <summary>
        /// 执行表连接，返回连接后的新表
        /// <para>应当通过参数<paramref name="table2Prefix"/>对连接后的新表添加字段名前缀，防止存在同名的字段</para>
        /// <para>内连接、左连接：http://www.ruanyifeng.com/blog/2019/01/table-join.html </para>
        /// </summary>
        /// <param name="table1">要连接的第一个表</param>
        /// <param name="table2">要连接的第二个表</param>
        /// <param name="keyField1">第一个表的与第二个表进行相等连接字段</param>
        /// <param name="keyField2">第二个表的与第一个表进行相等连接字段</param>
        /// <param name="joinType">表连接类型，仅支持内连接“InnerJoin”、左连接“LeftJoin”</param>
        /// <param name="keyFieldType">进行相等连接字段的类型</param>
        /// <param name="table1Prefix">对<paramref name="table1"/>的全部字段的名称添加的前缀，值为null或空白则不添加前缀</param>
        /// <param name="table2Prefix">对<paramref name="table2"/>的全部字段的名称添加的前缀，值为null或空白则不添加前缀（注意，此时若存在同名字段会引发异常）</param>
        /// <returns>返回连接后的新表，新表的表名称为“表1,表2”</returns>
        public static DataTable Join(this DataTable table1, DataTable table2, string keyField1, string keyField2, string joinType = "InnerJoin",
            Type keyFieldType = null, string table1Prefix = null, string table2Prefix = null)
        {
            var newTable = table1.Clone();
            newTable.TableName = $"{table1.TableName},{table2.TableName}";
            if (!string.IsNullOrWhiteSpace(table1Prefix))
                foreach (DataColumn column in newTable.Columns) column.ColumnName = table1Prefix + column.ColumnName;

            if (string.IsNullOrWhiteSpace(table2Prefix)) table2Prefix = string.Empty;
            var addColumns = table2.Columns.Cast<DataColumn>().Select(v => new DataColumn(table2Prefix + v.ColumnName, v.DataType)).ToArray();
            newTable.Columns.AddRange(addColumns);

            var data = joinType.ToLower() == "innerjoin" ?
                InnerJoinToEnumerable(table1, table2, keyField1, keyField2, keyFieldType) :
                LeftJoinToEnumerable(table1, table2, keyField1, keyField2, keyFieldType);

            return newTable.FillData(data);
        }
        /// <summary>
        /// 内连接两个表（DataTable），返回连接结果表格的全部数据（一个代表全部行以及行内所有值的可枚举数）
        /// <para>内连接：http://www.ruanyifeng.com/blog/2019/01/table-join.html </para>
        /// <para>LINQ连接类型：https://blog.csdn.net/hyunbar/article/details/79452258 </para>
        /// </summary>
        /// <param name="table1">要连接的第一个表</param>
        /// <param name="table2">要连接的第二个表</param>
        /// <param name="keyField1">第一个表的与第二个表进行相等连接字段</param>
        /// <param name="keyField2">第二个表的与第一个表进行相等连接字段</param>
        /// <param name="keyFieldType">进行相等连接字段的类型</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<object>> InnerJoinToEnumerable(this DataTable table1, DataTable table2, string keyField1, string keyField2, Type keyFieldType = null)
        {
            //以下LINQ语句和方法需要引用System.Data.DataSetExtensions
            var rows1 = table1.AsEnumerable();
            var rows2 = table2.AsEnumerable();

            if (keyFieldType == null)
                return from row1 in rows1 join row2 in rows1 on row1[keyField1]?.ToString() equals row2[keyField2]?.ToString() select row1.ItemArray.Concat(row2.ItemArray);
            if (keyFieldType == typeof(int))
                return from row1 in rows1 join row2 in rows1 on row1.Field<int>(keyField1) equals row2.Field<int>(keyField2) select row1.ItemArray.Concat(row2.ItemArray);
            if (keyFieldType == typeof(string))
                return from row1 in rows1 join row2 in rows1 on row1.Field<string>(keyField1) equals row2.Field<string>(keyField2) select row1.ItemArray.Concat(row2.ItemArray);

            throw new Exception($"未实现的表连接字段的字段类型（{nameof(keyFieldType)}），目前仅支持null、int和string类型");
        }
        /// <summary>
        /// 左连接两个表（DataTable），返回连接结果表格的全部数据（一个代表全部行以及行内所有值的可枚举数）
        /// <para>左连接：http://www.ruanyifeng.com/blog/2019/01/table-join.html </para>
        /// <para>LINQ连接类型：https://blog.csdn.net/hyunbar/article/details/79452258 </para>
        /// </summary>
        /// <param name="table1">要连接的第一个表</param>
        /// <param name="table2">要连接的第二个表</param>
        /// <param name="keyField1">第一个表的与第二个表进行相等连接字段</param>
        /// <param name="keyField2">第二个表的与第一个表进行相等连接字段</param>
        /// <param name="keyFieldType">进行相等连接字段的类型</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<object>> LeftJoinToEnumerable(this DataTable table1, DataTable table2, string keyField1, string keyField2, Type keyFieldType = null)
        {
            //以下LINQ语句和方法需要引用System.Data.DataSetExtensions
            var rows1 = table1.AsEnumerable();
            var rows2 = table2.AsEnumerable();

            if (keyFieldType == null)
                return from row1 in rows1
                       join row2 in rows2
                       on row1[keyField1]?.ToString() equals row2[keyField2]?.ToString() into tmpTable2
                       from tmpRow2 in tmpTable2.DefaultIfEmpty(table2.NewRow())
                       select row1.ItemArray.Concat(tmpRow2.ItemArray);

            if (keyFieldType == typeof(int))
                return from row1 in rows1
                       join row2 in rows2
                       on row1.Field<int>(keyField1) equals row2.Field<int>(keyField2) into tmpTable2
                       from tmpRow2 in tmpTable2.DefaultIfEmpty(table2.NewRow())
                       select row1.ItemArray.Concat(tmpRow2.ItemArray);

            if (keyFieldType == typeof(string))
                return from row1 in rows1
                       join row2 in rows2
                       on row1.Field<string>(keyField1) equals row2.Field<string>(keyField2) into tmpTable2
                       from tmpRow2 in tmpTable2.DefaultIfEmpty(table2.NewRow())
                       select row1.ItemArray.Concat(tmpRow2.ItemArray);

            throw new Exception($"未实现的表连接字段的字段类型（{nameof(keyFieldType)}），目前仅支持null、int和string类型");
        }
        /// <summary>
        /// 将LINQ查询所得的数据填充到表格中
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static DataTable FillData(this DataTable dataTable, IEnumerable<IEnumerable<object>> data)
        {
            foreach (var obj in data)
            {
                var newRow = dataTable.NewRow();
                newRow.ItemArray = obj.ToArray();
                dataTable.Rows.Add(newRow);
            }
            return dataTable;
        }
        #endregion


        /// <summary>
        /// 将DataTable中的数据转成文本
        /// </summary>
        /// <param name="dataTable">需要将数据转成文本的DataTable</param>
        /// <param name="split">分隔符，用于分隔每一行的每一个值</param>
        /// <param name="containsHeader">是否包含表头</param>
        /// <returns></returns>
        public static string DataTableToText(this DataTable dataTable, string split, bool containsHeader = true)
        {
            StringBuilder sb = new StringBuilder();
            var cols = dataTable.Columns;
            if (containsHeader)
            {
                for (int i = 0; i < cols.Count - 1; i++)
                {
                    sb.Append(cols[i].ColumnName + split);
                }
                sb.Append(cols[cols.Count - 1].ColumnName + Environment.NewLine);
            }

            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < cols.Count - 1; i++)
                {
                    sb.Append(row[i] + split);
                }
                sb.Append(row[cols.Count - 1] + Environment.NewLine);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 将DataTable中每一列的列名(ColumnName)和列标题(Caption)对调
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns>对dataTable本身对调列表和列标题后返回，没有进行对象克隆</returns>
        public static DataTable SwitchColumnNameAndCaption(this DataTable dataTable)
        {
            foreach (DataColumn column in dataTable.Columns)
            {
                var tmp = column.ColumnName;
                column.ColumnName = column.Caption;
                column.Caption = tmp;
            }
            return dataTable;
        }
    }
}
