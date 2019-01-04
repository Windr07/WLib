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
        private readonly object _mRange;
        public Range(object range)
        {
            _mRange = range;
        }

        public object Value
        {
            get
            {
                object result = _mRange.GetType().InvokeMember("Value", System.Reflection.BindingFlags.GetProperty, null, _mRange, null);
                return result;
            }
            set
            {
                object[] parameters = new Object[1] { value };
                _mRange.GetType().InvokeMember("Value", System.Reflection.BindingFlags.SetProperty, null, _mRange, parameters);
            }
        }
    }
}