/*---------------------------------------------------------------- 
// auth： Unknown
// date： 2018
// desc： None
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System;

namespace WLib.Files.Excel.MSExcel.ExcelBind
{
    public class Range
    {
        private readonly object _range;
        public Range(object range)
        {
            _range = range;
        }

        public object Value
        {
            get
            {
                object result = _range.GetType().InvokeMember("Value", System.Reflection.BindingFlags.GetProperty, null, _range, null);
                return result;
            }
            set
            {
                object[] parameters = new Object[] { value };
                _range.GetType().InvokeMember("Value", System.Reflection.BindingFlags.SetProperty, null, _range, parameters);
            }
        }
    }
}