using System;

namespace WLib.ThreadHelper
{
    /// <summary>
    /// 从其它线程修改主线程创建的控件；如调用控件方法、修改控件属性。
    /// </summary>
    public class ControlOpt
    {
        public delegate void MultiThreaSetValueHandler(ControlInvokeParam paras, EControlInvokeType eType);

        /// <summary>
        ///  从其它线程修改主线程创建的控件；调用控件方法或修改控件属性。
        /// </summary>
        /// <param name="paras">调用参数</param>
        /// <param name="eType">调用类型，表示是要修改属性还是要调用方法</param>
        public static void Invoke(ControlInvokeParam paras, EControlInvokeType eType)
        {
            if (paras.ControlContainer.InvokeRequired)
            {
                MultiThreaSetValueHandler prodelega = new MultiThreaSetValueHandler(Invoke);
                paras.ControlContainer.Invoke(prodelega, paras, eType);
            }
            else
            {
                Type type = paras.ControlTarget.GetType();
                if (eType == EControlInvokeType.SetProperty)
                {
                    var propertyInfo = type.GetProperty(paras.InvokeString);
                    propertyInfo.SetValue(paras.ControlTarget, paras.InvokeValues[0], null);
                }
                else if (eType == EControlInvokeType.InvokeMethod)
                {
                    var methodInfo = type.GetMethod(paras.InvokeString);
                    methodInfo.Invoke(paras.ControlTarget, paras.InvokeValues);
                }
            }
        }


    }
}
