/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/6
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace WLib.ExtProgress.Core
{
    /// <summary>
    /// 可进行进度控制的操作
    /// </summary>
    public interface IProgressOperation : IProgressEvents
    {
        #region 属性
        /// <summary>
        /// 进行操作的输入数据
        /// </summary>
        object InputData { get; set; }
        /// <summary>
        /// 操作的名称，用于标识操作
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 对操作的描述
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// 是否中止执行操作
        /// </summary>
        bool StopRunning { get; }
        /// <summary>
        /// 是否正在运行操作
        /// </summary>
        bool IsRunning { get; }
        /// <summary>
        /// 是否已暂停操作
        /// </summary>
        bool IsPause { get; }
        /// <summary>
        /// 是否已启用子线程且在运行阶段
        /// </summary>
        bool IsOnSubThread { get; }

        /// <summary>
        /// 操作开始时间
        /// </summary>
        DateTime StartTime { get; }
        /// <summary>
        /// 操作结束时间
        /// </summary>
        DateTime EndTime { get; }

        /// <summary>
        /// 进度信息
        /// </summary>
        IProgressMsgs Msgs { get; }
        /// <summary>
        /// 当前操作包含的子操作
        /// <para>这些子操作应在MainOperation中进行处理</para>
        /// </summary>
        List<IProgressOperation> SubProgressOperations { get; }
        #endregion


        #region 方法
        /// <summary>
        /// 清空消息，执行操作
        /// </summary>
        void Run();
        /// <summary>
        /// 清空消息，创建新线程并在线程中执行操作
        /// </summary>
        void RunByThread();
        /// <summary>
        /// 停止操作
        /// </summary>
        void Stop();
        /// <summary>
        /// 暂停操作（仅在RunByThread方法执行线程操作期间有效）
        /// </summary>
        void Pause();
        /// <summary>
        /// 继续操作（仅在Pause方法执行期间有效）
        /// </summary>
        void GoOn();
        /// <summary>
        /// 立即停止操作（仅在RunByThread方法执行线程操作期间有效）
        /// </summary>
        void Abort();
        #endregion
    }
}
