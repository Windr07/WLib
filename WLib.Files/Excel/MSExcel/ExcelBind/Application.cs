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
    /// <summary>
    /// Excel后期绑定的操作类
    /// </summary>
    public class Application
    {
        private readonly object _excelApp;
        public Application()
        {
            Type objExcelType = Type.GetTypeFromProgID("Excel.Application");
            if (objExcelType == null)
            {
                throw new Exception("未发现Excel程序");
            }
            _excelApp = Activator.CreateInstance(objExcelType);
            if (_excelApp == null)
            {
                throw new Exception("启用Excel程序失败!");
            }
        }

        public void Open(string excelFileName)
        {
            //检查文件是否存在
            if (!System.IO.File.Exists(excelFileName))
                throw new Exception("要打开的文件" + excelFileName + "不存在!");
            //打开文件
            if (_excelApp != null)
            {
                object[] parameters = new object[1] { excelFileName };
            }
        }

        public void SaveAs(string excelFileName)
        {
        }

        public bool Visible
        {
            get
            {
                object objVisible =
                    _excelApp.GetType().InvokeMember("Visible", BindingFlags.GetProperty, null, _excelApp, null);
                if (objVisible is Boolean)
                    return (bool)objVisible;
                else
                    throw new Exception("调用方法失败!");
            }
            set
            {
                object[] parameters = new object[1] { value };
                _excelApp.GetType().InvokeMember("Visible", BindingFlags.SetProperty, null, _excelApp, parameters);
            }
        }

        public Workbooks Workbooks
        {
            get
            {
                object workbooks = _excelApp.GetType().InvokeMember("Workbooks", BindingFlags.GetProperty, null, _excelApp, null);
                if (workbooks == null)
                    throw new Exception("查询工作簿时失败!");
                else
                    return new Workbooks(workbooks);
            }
        }
    }
}

