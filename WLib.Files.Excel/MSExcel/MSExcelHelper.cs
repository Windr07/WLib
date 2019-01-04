/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Files.Excel.MSExcel
{
    //Microsoft.office.interop.excel.dll 每个版本号对应OFFICE的一个版本
    //每个版本的dll都是通过调用相应版本EXCEL内核中的接口，来完成C#中读写EXCEL的
    //(1) 1985年：Excel 1.0 　　
    //(2) 1993年：Excel 5.0——Office 4.2 　　
    //(3) 1995年：Excel 7.0(Excel 95)　
    //(4) 1997年：Excel 8.0(Excel 97)　
    //(5) 1999年：Excel 9.0(Excel 2000)　
    //(6) 2001年：Excel 10(Excel XP/2002)——Office XP/2002 
    //(7) 2003年：Excel 2003(Excel XP/2003)——  11.0　　
    //(8) 2007年：Excel 2007(Excel XP/2007) —— 12.0
    //(9) 2010年：Excel 2010 —— 13.0
    //(10) 2012年:Excel 2013 ——14.0

    //解决版本兼容性问题：
    //1.引用高版本的的Excel.dll组件，可引用14.0.0 防止客户安装高版本如Office不能导出。
    //（DLL组件可以兼容低版本，不能兼容高版本）
    //2.右键DLL属性，将引用的Excel.dll组件，嵌入互操作类型为True，特定版本=false（注意.NET Framework4.0或以上才能设置互操作类型）
    //  嵌入互操作类型改成True后，生成时可能现有调用Excel的代码会报错，引用Microsoft.CSharp 命名空间，可以解决此问题。
    //3.引用Excel 14.0.0 DLL组件方法，vs2012 右键添加引用->程序集->扩展->Microsoft.Office.Interop.Excel
    //Excel.dll     http://files.cnblogs.com/files/ichk/Microsoft.Office.Interop.Excel.rar

    /// <summary>
    /// Excel操作类（引用微软的dll）
    /// </summary>
    public class MsExcelHelper
    {
        /// <summary>
        /// 获取单元格
        /// </summary>
        /// <param name="ws">worksheet</param>
        /// <param name="sRow">指定行</param>
        /// <param name="sCol">指定列</param>
        /// <returns></returns>
        public static Microsoft.Office.Interop.Excel.Range GetRange(Microsoft.Office.Interop.Excel.Worksheet ws, int sRow, int sCol)
        {
            return ws.get_Range(ws.Cells[sRow, sCol], ws.Cells[sRow, sCol]);
        }

        /// <summary>
        /// 获取单元格
        /// </summary>
        /// <param name="ws">worksheet</param>
        /// <param name="sRow">开始行</param>
        /// <param name="sCol">开始列</param>
        /// <param name="eRow">结束行</param>
        /// <param name="eCol">结束列</param>
        /// <returns></returns>
        public static Microsoft.Office.Interop.Excel.Range GetRange(Microsoft.Office.Interop.Excel.Worksheet ws, int sRow, int sCol, int eRow, int eCol)
        {
            return ws.get_Range(ws.Cells[sRow, sCol], ws.Cells[eRow, eCol]);
        }

        /// <summary>
        /// 设置单元格边框
        /// </summary>
        /// <param name="ws">worksheet</param>
        /// <param name="sRow">开始行</param>
        /// <param name="sCol">开始列</param>
        /// <param name="eRow">结束行</param>
        /// <param name="eCol">结束列</param>
        public static void SetBorderLine(Microsoft.Office.Interop.Excel.Worksheet ws, int sRow, int sCol, int eRow, int eCol)
        {
            Microsoft.Office.Interop.Excel.Range range = ws.get_Range(ws.Cells[sRow, sCol], ws.Cells[eRow, eCol]);
            range.Cells.Borders.LineStyle = 1;
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="ws">worksheet</param>
        /// <param name="sRow">开始行</param>
        /// <param name="sCol">开始列</param>
        /// <param name="eRow">结束行</param>
        /// <param name="eCol">结束列</param>
        public static void MergeCells(Microsoft.Office.Interop.Excel.Worksheet ws, int sRow, int sCol, int eRow, int eCol)
        {
            ws.get_Range(ws.Cells[sRow, sCol], ws.Cells[eRow, eCol]).Merge(Type.Missing);
        }

        /// <summary>
        /// 合并单元格并填写值
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="value">单元格的值</param>
        /// <param name="sRow">开始行</param>
        /// <param name="sCol">开始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endCol">结束列</param>
        public static void MergeCells(Microsoft.Office.Interop.Excel.Worksheet worksheet, object value, int sRow, int sCol, int endRow, int endCol)
        {
            Microsoft.Office.Interop.Excel.Range range = worksheet.get_Range(worksheet.Cells[sRow, sCol], worksheet.Cells[endRow, endCol]);
            range.Merge();
            range.Value = value;
        }

        /// <summary>
        /// 合并单元格，填写值并设置边框
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="value">单元格的值</param>
        /// <param name="sRow">开始行</param>
        /// <param name="sCol">开始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endCol">结束列</param>
        public static void MergeCells2(Microsoft.Office.Interop.Excel.Worksheet worksheet, object value, int sRow, int sCol, int endRow, int endCol)
        {
            Microsoft.Office.Interop.Excel.Range range = worksheet.get_Range(worksheet.Cells[sRow, sCol], worksheet.Cells[endRow, endCol]);
            range.Merge();
            range.Value = value;
            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
        }


        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="sheet">worksheet</param>
        /// <param name="rowIndex">行数</param>
        public static void DeleteRows(Microsoft.Office.Interop.Excel.Worksheet sheet, int rowIndex)
        {
            Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sheet.Rows[rowIndex, Type.Missing];
            range.Delete(Microsoft.Office.Interop.Excel.XlDeleteShiftDirection.xlShiftUp);
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="sheet">worksheet</param>
        /// <param name="rowIndex">行数</param>
        public static void InsertRows(Microsoft.Office.Interop.Excel.Worksheet sheet, int rowIndex)
        {
            Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sheet.Rows[rowIndex, Type.Missing];
            //object Range.Insert(object shift, object copyorigin);
            //shift: Variant类型，可选。指定单元格的调整方式。可以为下列 XlInsertShiftDirection 常量之一：
            //xlShiftToRight 或 xlShiftDown。如果省略该参数，Microsoft Excel 将根据区域形状确定调整方式。
            range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
        }

        /// <summary>
        /// 设置字符串添加下划线
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="row">单元格行号</param>
        /// <param name="col">单元格列号</param>
        /// <param name="start">添加下划线的字符段的开头位置索引</param>
        /// <param name="length">添加下划线的字符串长度</param>
        public static void InsertUnderLine(Microsoft.Office.Interop.Excel.Worksheet ws, int row, int col, int start, int length)
        {
            Microsoft.Office.Interop.Excel.Range range = ws.get_Range(ws.Cells[row, col], ws.Cells[row, col]);
            Microsoft.Office.Interop.Excel.Characters chars = range.Characters[start, length];
            chars.Font.Underline = true;
        }
    }
}
