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
        private readonly object _oWorkbooks;
        private string _excelFileName;

        public Workbooks(object workbooks)
        {
            _oWorkbooks = workbooks;
        }

        public void Open(string excelFileName)
        {
            try
            {
                _excelFileName = excelFileName;
                object[] parameters = new object[1] { excelFileName };
                _oWorkbooks.GetType().InvokeMember("Open", BindingFlags.InvokeMethod, null, _oWorkbooks, parameters);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Workbook this[int index]
        {
            get
            {
                object[] parameters = new object[1] { index };
                object workbook = _oWorkbooks.GetType().InvokeMember("Item", BindingFlags.GetProperty, null, _oWorkbooks, parameters);
                if (workbook == null)
                    throw new Exception("获取工作薄时出现错误!");
                else
                    return new Workbook(workbook);
            }
        }
    }
}