using System;
using System.Collections.Generic;

namespace WLib.UserCtrls.Dev.ChildForm
{
    internal class CommonUtility
    {  
        /// <summary>
        /// 类型记录列表，用于防止打开同类型的窗体
        /// </summary>
        internal static List<Type> ListFormType = new List<Type>(); 
    }
}
