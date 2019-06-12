/*---------------------------------------------------------------- 
// auth： Unknown
// date： 2018
// desc： None
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System;

namespace WLib.Files.Excel.MSExcel.ExcelBind
{
    public class Workbook
    {
        private readonly object _workbook;
        public Workbook(object workbook)
        {
            _workbook = workbook;
        }

        public Worksheets Worksheets
        {
            get
            {
                object worksheets = _workbook.GetType().InvokeMember("Worksheets", System.Reflection.BindingFlags.GetProperty, null, _workbook, null);
                if (worksheets == null)
                    throw new Exception("获取工作表集合时失败!");
                else
                    return new Worksheets(worksheets);
            }
        }
        public void SaveAs(string fileName)
        {
            object[] parameters = new object[1] { fileName };
            _workbook.GetType().InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, _workbook, parameters);
        }
    }
}