/*---------------------------------------------------------------- 
// auth： Unknown
// date： 2018
// desc： None
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System;

namespace WLib.Files.Excel.MSExcel.ExcelBind
{
    public class Worksheet
    {
        private readonly object _oWorksheet;
        public Worksheet(object worksheet)
        {
            _oWorksheet = worksheet;
        }

        public Range this[int row, int col]
        {
            get
            {
                object[] parameters = new Object[2] { row, col };
                object cells = _oWorksheet.GetType().InvokeMember("Cells", System.Reflection.BindingFlags.GetProperty, null, _oWorksheet, parameters);
                if (cells == null)
                    throw new Exception("获取单元格失败!");
                else
                    return new Range(cells);
            }
        }
        public Range this[int row, string col]
        {
            get
            {
                object[] parameters = new Object[2] { row, col };
                object cells = _oWorksheet.GetType().InvokeMember("Cells", System.Reflection.BindingFlags.GetProperty, null, _oWorksheet, parameters);
                if (cells == null)
                    throw new Exception("获取单元格失败!");
                else
                    return new Range(cells);
            }
        }
    }
}