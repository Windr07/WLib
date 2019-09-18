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
        /// <summary>
        /// 分组的进度信息
        /// </summary>
        private List<string> _messageList;
        /// <summary>
        /// 是否在进度信息中追加当前时间
        /// </summary>
        public bool AppendTime { get; set; }
        /// <summary>
        /// 全部进度信息
        /// </summary>
        public string AllMessage
        {
            get
            {
                var sb = new StringBuilder();
                _messageList.ForEach(v => sb.AppendLine(v));
                return sb.ToString();
            }
        }
        /// <summary>
        /// 反序输出的全部进度信息
        /// </summary>
        public string AllMessageReverse
        {
            get
            {
                var sb = new StringBuilder();
                for (int i = _messageList.Count - 1; i >= 0; i--)
                    sb.AppendLine(_messageList[i]);

                return sb.ToString();
            }
        }
        /// <summary>
        /// 进度信息发生变化的事件
        /// </summary>
        public event EventHandler<ProMsgChangedEventArgs> MessageChanged;
        /// <summary>
        /// 进度信息
        /// </summary>
        public ProgressMsgs(bool appendTime = true)
        {
            AppendTime = appendTime;
            _messageList = new List<string>();
        }


        /// <summary>
        /// 清除全部的进度信息
        /// </summary>
        public void Clear()
        {
            _messageList.Clear();
        }
        /// <summary>
        /// 设置当前进度信息
        /// </summary>
        /// <param name="curMessage">当前进度信息</param>
        /// <returns></returns>
        public void Info(string curMessage)
        {
            curMessage = AppendTime ? $"{DateTime.Now:yyyy/MM/dd,HH:mm:ss}\t{curMessage}" : curMessage;
            _messageList.Add(curMessage);
            MessageChanged?.Invoke(this, new ProMsgChangedEventArgs(curMessage));
        }
    }
}
