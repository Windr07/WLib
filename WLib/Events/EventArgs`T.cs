/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Events
{
    /// <summary>
    /// 事件参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventArgs<T> : EventArgs
    {
        /// <summary>
        /// 事件参数数据
        /// </summary>
        public T Data { get; set; }


        /// <summary>
        /// 事件参数
        /// </summary>
        public EventArgs() { }
        /// <summary>
        /// 事件参数
        /// </summary>
        /// <param name="value"></param>
        public EventArgs(T value) => Data = value;
    }
}
