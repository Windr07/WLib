/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Delegates
{
    /// <summary>
    /// 提供移除委托绑定的方法
    /// </summary>
    public static class DelegateBinding
    {
        /// <summary>
        /// 移除委托绑定过的全部处理方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tDelegate"></param>
        public static void RemoveAll(this Delegate tDelegate)
        {
            Delegate[] handlers = tDelegate.GetInvocationList();
            if (handlers != null)
            {
                foreach (Delegate handler in handlers)
                    Delegate.RemoveAll(tDelegate, handler);
            }
        }
    }
}
