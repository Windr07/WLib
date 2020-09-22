/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/10
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using WLib.ExtProgress.Core;

namespace WLib.ExtProgress
{
    /// <summary>
    /// 可进行进度控制和日志记录的操作
    /// </summary>
    /// <typeparam name="TInput">进度操作所需的数据</typeparam>
    /// <typeparam name="TResult">进度操作的结果</typeparam>
    public abstract class ProLogOperation<TInput, TResult> : ProgressOperation<TInput, ProLogGroup, TResult>
    {
        /// <summary>
        /// 可进行进度控制和日志记录的操作
        /// </summary>
        protected ProLogOperation() : base() { }
        /// <summary>
        /// 可进行进度控制和日志记录的操作
        /// </summary>
        /// <param name="name">操作的名称</param>
        protected ProLogOperation(string name) : base(name) { }
        /// <summary>
        /// 可进行进度控制和日志记录的操作
        /// </summary>
        /// <param name="name">操作的名称</param>
        /// <param name="inputData">进度操作所需的数据</param>
        protected ProLogOperation(string name, TInput inputData) : base(name, inputData) { }
        /// <summary>
        /// 可进行进度控制和日志记录的操作
        /// </summary>
        /// <param name="name">操作的名称</param>
        /// <param name="description">对操作的描述</param>
        protected ProLogOperation(string name, TInput inputData, string description) : base(name, inputData, description) { }


        /// <summary>
        /// 设置当前进度信息
        /// </summary>
        public string Info { set => Msgs.Info(value, ProLogGroup.Info); }
        /// <summary>
        /// 设置当前调试信息
        /// </summary>
        public string Debug { set => Msgs.Info(value, ProLogGroup.Debug); }
        /// <summary>
        /// 设置当前警告信息
        /// </summary>
        public string Warnning { set => Msgs.Info(value, ProLogGroup.Warnning); }
        /// <summary>
        /// 设置当前错误信息
        /// </summary>
        public string Error { set => Msgs.Info(value, ProLogGroup.Error); }
    }
}
