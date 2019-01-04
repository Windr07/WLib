/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Data;
using System.Text;

namespace WLib.Data
{
    /// <summary>
    /// DataTable的操作
    /// </summary>
    public class DataTableOpt
    {
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


        /// <summary>
        /// 将DataTable中的数据转成文本
        /// </summary>
        /// <param name="dataTable">需要将数据转成文本的DataTable</param>
        /// <param name="split">分隔符，用于分隔每一行的每一个值</param>
        /// <param name="containsHeader">是否包含表头</param>
        /// <returns></returns>
        public static string DataTableToText(DataTable dataTable, string split, bool containsHeader = true)
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
        /// 转置DataTable，即将行列对换（注意dataTable的行数不能太多）
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <returns></returns>
        public static DataTable TransposeDataTable(DataTable sourceTable)
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
        public static DataTable TransposeDataTable(DataRow dataRow)
        {
            return TransposeDataTable(dataRow.Table);
        }

    }
}
