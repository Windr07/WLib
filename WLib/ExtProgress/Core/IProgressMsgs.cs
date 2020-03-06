/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/6
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using WLib.ExtProgress.ProEventArgs;

namespace WLib.ExtProgress.Core
{
    /// <summary>
    /// 进度信息
    /// </summary>
    public interface IProgressMsgs
    {
        #region 属性
        /// <summary>
        /// ID
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 功能描述
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// 功能代码
        /// </summary>
        string Code { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        DateTime StartTime { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        DateTime EndTime { get; set; }
        /// <summary>
        /// 程序名称
        /// </summary>
        string AssemblyName { get; set; }
        /// <summary>
        /// 程序版本
        /// </summary>
        string AssemblyVersion { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        string Error { get; set; }
        /// <summary>
        /// 全部处理信息
        /// </summary>
        string AllMessage { get; set; }
        /// <summary>
        /// 是否在进度信息中追加当前时间
        /// </summary>
        bool AppendTime { get; set; }
        /// <summary>
        /// 追加当前时间的时间格式，默认"yyyy/MM/dd,HH:mm:ss"
        /// </summary>
        string TimeFormat { get; set; }
        #endregion


        #region 事件、方法
        /// <summary>
        /// 进度信息发生变化的事件
        /// </summary>
        event EventHandler<ProMsgChangedEventArgs> MessageChanged;
        /// <summary>
        /// 全部进度信息
        /// </summary>
        string GetAllMessage();
        /// <summary>
        /// 反序输出的全部进度信息
        /// </summary>
        string GetAllMessageReverse();
        /// <summary>
        /// 设置当前进度信息
        /// </summary>
        /// <param name="curMessage">当前进度信息</param>
        /// <returns></returns>
        void Info(string curMessage);
        /// <summary>
        /// 设置当前进度信息
        /// </summary>
        /// <param name="curMessage">当前进度信息</param>
        /// <param name="appendTime">是否在进度信息中追加当前时间，该条进度信息将忽略<see cref="AppendTime"/>属性的影响</param>
        /// <returns></returns>
        void Info(string curMessage, bool appendTime);
        /// <summary>
        /// 清除全部的进度信息
        /// </summary>
        void Clear();
        #endregion
    }
}
