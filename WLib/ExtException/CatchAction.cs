/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2/23
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WLib.ExtException
{
    /// <summary>
    /// 表示异常处理方式
    /// </summary>
    public class CatchAction : ICatchAction
    {
        /// <summary>
        /// 异常处理方式
        /// </summary>
        public Action<Exception> ExceptionAction;
        /// <summary>
        /// 表示异常处理方式
        /// </summary>
        /// <param name="exceptionAction"></param>
        public CatchAction(Action<Exception> exceptionAction)
        {
            ExceptionAction = exceptionAction;
        }
        /// <summary>
        /// 执行异常处理
        /// </summary>
        /// <param name="ex"></param>
        public void Invoke(Exception ex)
        {
            ExceptionAction(ex);
        }
    }
}
