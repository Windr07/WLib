/*---------------------------------------------------------------- 
// auth： Unknown
// date： None
// desc： None
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System.Windows.Forms;

namespace WLib.ThreadHelper
{
    /// <summary>
    /// 执行ControlOpt.Invoke()方法时的传参，在另一个线程修改主线程创建的控件
    /// </summary>
    public class ControlInvokeParam
    {
        /// <summary>
        /// 获取主线程界面控件
        /// </summary>
        public Control ControlContainer { get; }

        /// <summary>
        /// 获取主线程界面里面的控件，即要修改的控件
        /// </summary>
        public Control ControlTarget { get; }

        /// <summary>
        /// 获取调用的字符串，属性名称或方法的名称
        /// </summary>
        public string InvokeString { get; }

        /// <summary>
        /// 调用的值，属性的值或方法的参数
        /// </summary>
        public object[] InvokeValues { get; set; }

        /// <summary>
        /// 初始化调用参数
        /// </summary>
        /// <param name="container">主线程界面控件</param>
        /// <param name="target">主线程界面里面的控件，即要修改的控件</param>
        /// <param name="invokeStr">属性名称或方法的名称</param>
        /// <param name="values">属性的值或方法的参数</param>
        public ControlInvokeParam(Control container, Control target, string invokeStr, object[] values = null)
        {
            ControlContainer = container;
            ControlTarget = target;
            InvokeString = invokeStr;
            InvokeValues = values;
        }
    }
}
