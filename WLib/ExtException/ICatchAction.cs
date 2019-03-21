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
    public interface ICatchAction
    {
        /// <summary>
        /// 执行异常处理
        /// </summary>
        /// <param name="ex"></param>
        void Invoke(Exception ex);
    }
}
