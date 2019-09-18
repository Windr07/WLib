/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/6
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using WLib.ExtProgress.Core;

namespace WLib.ExtProgress.ProEventArgs
{
    /// <summary>
    /// 为<see cref="ProgressOperation{TData, TMsgGroup}.ProgressChanged"/>事件提供数据（进度值信息）
    /// </summary>
    public class ProChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 当前进度值
        /// </summary>
        public int CurValue { get; }
        /// <summary>
        /// 总进度值
        /// </summary>
        public int MaxValue { get; }
        /// <summary>
        /// 完成进度占总进度的比值（0-1之间）
        /// </summary>
        public double Rate => MaxValue == 0 ? 0 : CurValue / MaxValue;
        /// <summary>
        /// 与当前进度相关的信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 为<see cref="ProgressOperation{TData, TMsgGroup}.ProgressChanged"/>事件提供数据（进度值信息）
        /// </summary>
        /// <param name="curValue">当前进度值</param>
        /// <param name="maxValue">总进度值</param>
        public ProChangedEventArgs(int curValue, int maxValue)
        {
            CurValue = curValue;
            MaxValue = maxValue;
        }
        /// <summary>
        /// 为<see cref="ProgressOperation{TData, TMsgGroup}.ProgressChanged"/>事件提供数据（进度值信息）
        /// </summary>
        /// <param name="curValue">当前进度值</param>
        /// <param name="maxValue">总进度值</param>
        /// <param name="msg">与当前进度相关的信息</param>
        public ProChangedEventArgs(int curValue, int maxValue, string msg) : this(curValue, maxValue) => Message = msg;
    }
}