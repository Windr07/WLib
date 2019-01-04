/*---------------------------------------------------------------- 
// auth： Unknown
// date： 2018
// desc： None
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System;

namespace WLib.Files.Excel.MSExcel.ExcelBind
{
    public class Worksheets
    {
        private readonly object _oWorksheets;

        public Worksheets(object worksheets)
        {
            _oWorksheets = worksheets;
        }

        public Worksheet this[int index]
        {
            get
            {
                object[] parameters = new object[1] { index };
                object worksheet = _oWorksheets.GetType().InvokeMember("Item", System.Reflection.BindingFlags.GetProperty, null, _oWorksheets, parameters);
                if (worksheet == null)
                    throw new Exception("获取工作表时出现错误!");
                else
                    return new Worksheet(worksheet);
            }
        }

    }
}