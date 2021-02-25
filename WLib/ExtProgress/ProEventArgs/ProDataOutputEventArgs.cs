/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.ExtProgress.ProEventArgs
{
    /// <summary>
    /// 为<see cref="WLib.ExtProgress.Core.IProgressEvents.DataOutput"/>事件提供数据（当前进度下的输出数据）
    /// </summary>
    public class ProDataOutputEventArgs : EventArgs
    {
        /// <summary>
        /// 当前进度下的输出数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 为<see cref="WLib.ExtProgress.Core.IProgressEvents.DataOutput"/>事件提供数据（当前进度下的输出数据）
        /// </summary>
        /// <param name="data">当前进度下的输出数据</param>
        public ProDataOutputEventArgs(object data) => Data = data;
    }
}
