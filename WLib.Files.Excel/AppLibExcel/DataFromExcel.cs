using System;
using System.Data;
using System.Linq;

namespace WLib.Files.Excel.AppLibExcel
{
    /// <summary>
    /// 读取Excel
    /// </summary>
    public class DataFromExcel
    {
        readonly AppLibrary.ReadExcel.Workbook _workbook = null;
        public DataFromExcel(string filePath)
        {
            _workbook = AppLibrary.ReadExcel.Workbook.getWorkbook(filePath);
        }
        /// <summary>
        /// 按表获名称获取符合查询条件的数据行
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public DataRow[] GetSheetData(string sheetName, string whereClause)
        {
            DataTable dataTable = GetSheetData(sheetName);
            return dataTable.Select(whereClause);
        }
        /// <summary>
        /// 按表获名称获取所有数据
        /// </summary>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public DataTable GetSheetData(string sheetName)
        { 
            AppLibrary.ReadExcel.Sheet sheet = _workbook.Sheets.First(p => p.Name == sheetName);
            if (sheet == null)
                throw new Exception("找不到表格名为：" + sheetName + " 的表格，请检查数据");
            DataTable dataTable = ReadSheetData(sheet);
            return dataTable;
        }
        /// <summary>
        /// 根据表格的索引获取数据,索引从零开始
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <returns></returns>
        public DataTable GetSheetData(int sheetIndex)
        {
            AppLibrary.ReadExcel.Sheet sheet = _workbook.Sheets[sheetIndex];
            if (sheet == null)
                throw new Exception("文件中没有可用表格数据");
            DataTable dataTable = ReadSheetData(sheet);
            return dataTable;
        }

        /// <summary>
        /// 读取sheet数据转换为DataTable
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        private DataTable ReadSheetData(AppLibrary.ReadExcel.Sheet sheet)
        {
            DataTable dataTable = new DataTable();
            for (int iRow = 0; iRow < sheet.Rows; iRow++)
            {
                DataRow dr = null;
                if (iRow != 0)
                    dr = dataTable.NewRow();
                for (int iCol = 0; iCol < sheet.Columns; iCol++)
                {
                    AppLibrary.ReadExcel.Cell colCell = sheet.getCell(iCol, iRow);
                    if (iRow == 0)
                    {
                        dataTable.Columns.Add(colCell.Value.ToString());
                    }
                    else
                    {
                        dr[iCol] = colCell.Value;
                    }
                }
                if (dr != null)
                    dataTable.Rows.Add(dr);
            }
            return dataTable;
        }


    }
}
