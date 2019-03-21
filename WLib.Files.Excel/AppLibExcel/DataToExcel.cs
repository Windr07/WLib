/*---------------------------------------------------------------- 
// auth： Unknown
// date： None
// desc： None
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;

namespace WLib.Files.Excel.AppLibExcel
{
    /// <summary>
    /// 提供将表数据写入Excel的操作
    /// </summary>
    public static class DataToExcel
    {
        /// <summary>
        /// 构造函数，将DataSet里面的数据写到Excel中，DataTable名称对应sheet的名称
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <param name="path"></param>
        /// <param name="fileName">Excel的名称</param>
        public static void DataSetToExcel(this DataSet ds, string path, string fileName)
        {
            AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
            doc.FileName = fileName;
            foreach (DataTable dt in ds.Tables)
            {
                AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add(dt.TableName);
                AppLibrary.WriteExcel.Cells cells = sheet.Cells;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    AppLibrary.WriteExcel.XF xf = doc.NewXF();
                    xf.Font.FontName = "宋体";
                    xf.Font.Bold = true;
                    xf.Font.Height = (ushort)(220);
                    xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
                    xf.VerticalAlignment = AppLibrary.WriteExcel.VerticalAlignments.Centered;
                    cells.Add(1, i + 1, dt.Columns[i].ColumnName, xf);
                    
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (Convert.IsDBNull(dt.Rows[j][i])) continue;
                        cells.Add(j + 2, i + 1, dt.Rows[j][i]);
                    }
                }

            }
            doc.Save(path, true);
        }
        /// <summary>
        /// 导出表格的所有数据到Excel,DataTable名称对应sheet的名称
        /// </summary>
        /// <param name="dt">DataTable对象，DataTable名称对应sheet的名称，如果名称为空则sheet名称为Sheet1</param>
        /// <param name="path">Excel保存目录</param>
        /// <param name="fileName">Excel文件名，包含扩展名</param>
        public static void DataTableToExcel(this DataTable dt, string path, string fileName)
        {
            AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
            doc.FileName = fileName;
            string sheetName = dt.TableName;
            if (string.IsNullOrEmpty(sheetName))
                sheetName = "Sheet1";
            AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add(sheetName);
            AppLibrary.WriteExcel.Cells cells = sheet.Cells;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                AppLibrary.WriteExcel.XF xf = doc.NewXF();
                xf.Font.FontName = "宋体";
                xf.Font.Bold = true;
                xf.Font.Height = (ushort)(220);
                xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
                xf.VerticalAlignment = AppLibrary.WriteExcel.VerticalAlignments.Centered;
                cells.Add(1, i + 1, dt.Columns[i].ColumnName, xf);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (Convert.IsDBNull(dt.Rows[j][i])) continue;
                    cells.Add(j + 2, i + 1, dt.Rows[j][i]);
                }
            }
            doc.Save(path, true);
        }
        /// <summary>
        /// 导出表格的指定字段名称数据到Excel,DataTable名称对应sheet的名称
        /// </summary>
        /// <param name="dt">DataTable对象，DataTable名称对应sheet的名称，如果名称为空则sheet名称为Sheet1</param>
        /// <param name="path">Excel保存目录</param>
        /// <param name="fileName">Excel文件名，包含扩展名</param>
        /// <param name="fieldNames">指定字段名称</param>
        public static void DataTableToExcel(this DataTable dt, string path, string fileName,List<string> fieldNames)
        {
            AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
            doc.FileName = fileName;
            string sheetName = dt.TableName;
            if (string.IsNullOrEmpty(sheetName))
                sheetName = "Sheet1";
            AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add(sheetName);
            AppLibrary.WriteExcel.Cells cells = sheet.Cells;
            for (int i = 0; i < fieldNames.Count; i++)
            {
                AppLibrary.WriteExcel.XF xf = doc.NewXF();
                xf.Font.FontName = "宋体";
                xf.Font.Bold = true;
                xf.Font.Height = (ushort)(220);
                xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
                xf.VerticalAlignment = AppLibrary.WriteExcel.VerticalAlignments.Centered;
                cells.Add(1, i + 1, fieldNames[i], xf);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (Convert.IsDBNull(dt.Rows[j][fieldNames[i]])) continue;
                    cells.Add(j + 2, i + 1, dt.Rows[j][fieldNames[i]]);
                }
            } 
            doc.Save(path, true);
        }
    }
}
