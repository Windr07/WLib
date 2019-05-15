using System;

namespace WLib.ExtProgress.ProEventArgs
{
    /// <summary>
    /// 为YYGISLib.Progress.ProgressOperation的ProgressChanged事件提供数据（进度值信息）
    /// </summary>
    public class ProgressChangedEventArgs : EventArgs
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
        /// 为ProgressOperation的ProgressChanged事件提供数据
        /// </summary>
        /// <param name="curValue">当前进度值</param>
        /// <param name="maxValue">总进度值</param>
        public ProgressChangedEventArgs(int curValue, int maxValue)
        {
            this.CurValue = curValue;
            this.MaxValue = maxValue;
        }
        /// <summary>
        /// 为ProgressOperation的ProgressChanged事件提供数据
        /// </summary>
        /// <param name="curValue">当前进度值</param>
        /// <param name="maxValue">总进度值</param>
        /// <param name="msg">与当前进度相关的信息</param>
        public ProgressChangedEventArgs(int curValue, int maxValue, string msg)
        {
            this.CurValue = curValue;
            this.MaxValue = maxValue;
            this.Message = msg;
        }
    }
}