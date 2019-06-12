/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/5/10
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Web
{
    /// <summary>
    /// 传输进度事件参数
    /// </summary>
    public class TransferProgressEventArgs : EventArgs
    {
        /// <summary>
        /// 当前进度值
        /// </summary>
        public long CurrentValue { get; set; }
        /// <summary>
        /// 总进度值
        /// </summary>
        public long TotalValue { get; set; }
        /// <summary>
        /// 传输进度值百分比，即：当前进度值÷总进度值×100%
        /// </summary>
        public double Rate => CurrentValue * 1.0 / TotalValue * 100;

        /// <summary>
        /// 传输进度事件参数
        /// </summary>
        public TransferProgressEventArgs()
        {

        }
        /// <summary>
        /// 传输进度事件参数
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="totalValue"></param>
        public TransferProgressEventArgs(long currentValue, long totalValue)
        {
            CurrentValue = currentValue;
            TotalValue = totalValue;
        }
    }
}
