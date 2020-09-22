/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Reflection;

namespace WLib.Events
{
    /// <summary>
    /// 提供获取、移除对象事件绑定的方法
    /// </summary>
    public static class EventBinding
    {
        /// <summary>
        /// 移除对象事件绑定过的全部事件处理方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">需要移除全部事件处理方法的对象</param>
        /// <param name="eventName">事件名</param>
        public static void RemoveEvent<T>(this T obj, string eventName)
        {
            Delegate[] handlers = GetObjectEventList(obj, eventName);
            if (handlers != null)
            {
                foreach (Delegate handler in handlers)
                    typeof(T).GetEvent(eventName).RemoveEventHandler(obj, handler);
            }
        }
        ///  <summary>     
        ///  获取对象事件绑定过的全部事件处理方法
        ///  </summary>     
        ///  <param name="obj">对象 </param>     
        ///  <param name="eventName">事件名</param>     
        ///  <returns>委托列 </returns>    
        public static Delegate[] GetObjectEventList(this object obj, string eventName)
        {
            FieldInfo fieldInfo = obj.GetType().GetField(eventName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
            if (fieldInfo == null)
                return null;

            object fieldValue = fieldInfo.GetValue(obj);
            if (fieldValue != null && fieldValue is Delegate)
            {
                Delegate objectDelegate = (Delegate)fieldValue;
                return objectDelegate.GetInvocationList();
            }
            return null;
        }
    }
}
