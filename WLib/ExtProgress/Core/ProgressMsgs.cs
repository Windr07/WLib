/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/6
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;
using WLib.ExtProgress.ProEventArgs;

namespace WLib.ExtProgress.Core
{
    /// <summary>
    /// 进度信息
    /// </summary>
    public class ProgressMsgs : IProgressMsgs
    {
        #region 接口属性
        /// <summary>
        /// 分组的进度信息
        /// </summary>
        private List<string> _messageList = new List<string>();
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 功能描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 功能代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 程序名称
        /// </summary>
        public string AssemblyName { get; set; }
        /// <summary>
        /// 程序版本
        /// </summary>
        public string AssemblyVersion { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// 全部处理信息
        /// </summary>
        public string AllMessage { get; set; }
        /// <summary>
        /// 是否在进度信息中追加当前时间
        /// </summary>
        public bool AppendTime { get; set; } = true;
        /// <summary>
        /// 追加当前时间的时间格式，默认"yyyy/MM/dd,HH:mm:ss"
        /// </summary>
        public string TimeFormat { get; set; } = "yyyy/MM/dd,HH:mm:ss.ff";
        #endregion


        #region 接口事件、方法
        /// <summary>
        /// 进度信息发生变化的事件
        /// </summary>
        public event EventHandler<ProMsgChangedEventArgs> MessageChanged;
        /// <summary>
        /// 执行<see cref="MessageChanged"/>事件处理
        /// </summary>
        /// <param name="curMessage"></param>
        protected virtual void OnMessageChanged(string curMessage) => MessageChanged?.Invoke(this, new ProMsgChangedEventArgs(curMessage));

        /// <summary>
        /// 全部进度信息
        /// </summary>
        public virtual string GetAllMessage()
        {
            var sb = new StringBuilder();
            _messageList.ForEach(v => sb.AppendLine(v));
            return sb.ToString();
        }
        /// <summary>
        /// 反序输出的全部进度信息
        /// </summary>
        public virtual string GetAllMessageReverse()
        {
            var sb = new StringBuilder();
            for (int i = _messageList.Count - 1; i >= 0; i--)
                sb.AppendLine(_messageList[i]);
            return sb.ToString();
        }
        /// <summary>
        /// 清除全部的进度信息
        /// </summary>
        public virtual void Clear() => _messageList.Clear();
        /// <summary>
        /// 设置当前进度信息
        /// </summary>
        /// <param name="curMessage">当前进度信息</param>
        /// <returns></returns>
        public virtual void Info(string curMessage)
        {
            curMessage = AppendTime ? $"{DateTime.Now.ToString(TimeFormat)}\t{curMessage}" : curMessage;
            _messageList.Add(curMessage);
            MessageChanged?.Invoke(this, new ProMsgChangedEventArgs(curMessage));
        }
        /// <summary>
        /// 设置当前进度信息
        /// </summary>
        /// <param name="curMessage">当前进度信息</param>
        /// <param name="appendTime">是否在进度信息中追加当前时间，该条进度信息将忽略<see cref="AppendTime"/>属性的影响</param>
        public virtual void Info(string curMessage, bool appendTime)
        {
            curMessage = appendTime ? $"{DateTime.Now.ToString(TimeFormat)}\t{curMessage}" : curMessage;
            _messageList.Add(curMessage);
            MessageChanged?.Invoke(this, new ProMsgChangedEventArgs(curMessage));
        }
        #endregion


        #region 其他方法
        /// <summary>
        /// 输出"Id - Name"
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Id} - {Name}";
        #endregion


        /// <summary>
        /// 进度信息
        /// </summary>
        /// <param name="appendTime"></param>
        public ProgressMsgs() { }
        /// <summary>
        /// 进度信息
        /// </summary>
        /// <param name="appendTime"></param>
        public ProgressMsgs(bool appendTime = true) => AppendTime = appendTime;
    }
}
