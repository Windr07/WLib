/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/8
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Windows.Forms;

namespace WLib.WinForm
{
    /// <summary>
    /// WinForm程序异常操作
    /// </summary>
    public static class AppExceptionOpt
    {
        /// <summary>
        /// 设置WinForm全局异常处理方法
        /// </summary>
        /// <param name="exceptionHandler">异常处理操作</param>
        public static void ExceptionControl(Action<Exception> exceptionHandler)
        {
            //设置应用程序处理异常方式：ThreadException处理  
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            //处理UI线程异常  
            Application.ThreadException += (sender, e) =>
                exceptionHandler(e.Exception);

            //处理非UI线程异常  
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                exceptionHandler(e.ExceptionObject as Exception);
        }
    }
}
