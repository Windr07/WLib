/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WLib.Progress.ProEventArgs;

namespace WLib.Progress
{
    /// <summary>
    /// 表示任务执行进程中的处理信息
    /// </summary>
    public class ProgressMessages
    {
        #region 进度信息
        private bool _messageWithTime;
        private string _progressMessage;
        private readonly List<string> _messages = new List<string>();


        /// <summary>
        /// 是否在获取进度信息时包含时间
        /// </summary>
        public bool MessageWithTime
        {
            get => _messageWithTime;
            set
            {
                ProgressGroupMessage.MessageWithTime = value;
                this._messageWithTime = value;
            }
        }
        /// <summary>
        /// 当前时刻的工作进度信息【每次赋值都将值加入消息列表，并触发MessageChanged事件】
        /// </summary>
        public virtual string ProgressMessage
        {
            get => _progressMessage;
            set
            {
                if (MessageWithTime)
                    _progressMessage = DateTime.Now.ToString("yyyy/MM/dd,HH:mm:ss") + " " + value;
                else
                    _progressMessage = value;

                _messages.Add(_progressMessage);
                OnMessageChanged(_progressMessage);
            }
        }
        /// <summary>
        /// 工作进程中的所有处理信息
        /// </summary>
        public virtual string Messages
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var msg in _messages)
                {
                    sb.AppendLine(msg);
                }

                if (ProgressGroupMessage.GroupCount > 0)
                {
                    sb.AppendLine();
                    sb.Append(ProgressGroupMessage.GetAllMessage());
                }
                return sb.ToString();
            }
        }
        /// <summary>
        /// 反序获取工作进程中的所有处理信息
        /// </summary>
        public virtual string ReverseMessages
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                for (var i = _messages.Count - 1; i >= 0; i--) { sb.AppendLine(_messages[i]); }

                if (ProgressGroupMessage.GroupCount > 0)
                {
                    sb.AppendLine();
                    sb.Append(ProgressGroupMessage.GetAllMessageReverse());
                }
                return sb.ToString();
            }
        }
        /// <summary>
        /// 进度信息（属性ProgressMessage）发生改变的事件
        /// </summary>
        public event EventHandler<MsgChangedEventArgs> MessageChanged;
        #endregion

        #region 结果信息
        /// <summary>
        /// 每个生成项，生成成功信息
        /// </summary>
        public List<string> SuccessInfo = new List<string>();
        /// <summary>
        /// 每个生成项，生成失败信息
        /// </summary>
        public List<string> ErrorInfo = new List<string>();
        /// <summary>
        /// 每个生成项，其他生成情况信息
        /// </summary>
        public List<string> OtherInfo = new List<string>();
        /// <summary>
        /// 所有成功、失败及跳过等生成结果的信息
        /// </summary>
        public virtual string ResultInfo
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("成功：" + SuccessInfo.Count);

                sb.AppendLine("失败：" + ErrorInfo.Count);
                for (int i = 0; i < ErrorInfo.Count; i++) { sb.AppendLine(ErrorInfo[i]); }

                sb.AppendLine("跳过：" + OtherInfo.Count);
                if (OtherInfo.Count == 1)
                    sb.AppendLine(OtherInfo[0]);
                else if (OtherInfo.Count > 1)
                    sb.AppendLine(OtherInfo.Aggregate((a, b) => a + "," + b));

                return sb.ToString();
            }
        }
        #endregion

        /// <summary>
        /// 分组进度信息
        /// </summary>
        public GroupMessages ProgressGroupMessage;

        /// <summary>
        /// 表示任务执行进程中的处理信息
        /// </summary>
        public ProgressMessages()
        {
            ProgressGroupMessage = new GroupMessages();
            MessageWithTime = false;
        }
        /// <summary>
        /// 清空处理信息
        /// </summary>
        public virtual void ClearMessage()
        {
            _messages.Clear();
            SuccessInfo.Clear();
            ErrorInfo.Clear();
            OtherInfo.Clear();
            ProgressGroupMessage.Clear();
        }
        /// <summary>
        /// 执行MessageChanged事件处理
        /// </summary>
        /// <param name="message"></param>
        public void OnMessageChanged(string message)
        {
            MessageChanged?.Invoke(this, new MsgChangedEventArgs(message));
        }

    }
}
