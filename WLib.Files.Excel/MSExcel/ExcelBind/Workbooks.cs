/*---------------------------------------------------------------- 
// auth： Unknown
// date： 2018
// desc： None
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System;
using System.Reflection;

namespace WLib.Files.Excel.MSExcel.ExcelBind
{
    public class Workbooks
    {
        private readonly object _workbooks;
        private string _excelFilePath;

        public Workbooks(object workbooks)
        {
            _workbooks = workbooks;
        }

        public void Open(string excelFilePath)
        {
            _excelFilePath = excelFilePath;
            object[] parameters = new object[] { excelFilePath };
            _workbooks.GetType().InvokeMember("Open", BindingFlags.InvokeMethod, null, _workbooks, parameters);
        }

        public Workbook this[int index]
        {
            get
            {
                object[] parameters = new object[1] { index };
                object workbook = _workbooks.GetType().InvokeMember("Item", BindingFlags.GetProperty, null, _workbooks, parameters);
                if (workbook == null)
                    throw new Exception("获取工作薄时出现错误!");
                else
                    return new Workbook(workbook);
            }
        }
    }
}