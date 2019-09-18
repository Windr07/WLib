/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/6
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WLib.ExtProgress.ProEventArgs;

namespace WLib.ExtProgress.Core
{
    /// <summary>
    /// 进度信息
    /// </summary>
    /// <typeparam name="TGroup">进度信息的分组，
    /// <see cref="TGroup"/>必须实现等值比较<see cref="IEquatable{T}"/>且具有无参构造函数，<see cref="TGroup"/>可以为值类型</typeparam>
    public class ProgressMsgs<TGroup> : ProgressMsgs, IProgressMsgs<TGroup> where TGroup : IEquatable<TGroup>, new()
    {
        /// <summary>
        /// 分组的进度信息
        /// </summary>
        private Dictionary<TGroup, List<string>> _groupMessageDict;
        /// <summary>
        /// 进度信息的默认分组
        /// </summary>
        public TGroup DefaultGroup { get; }
        /// <summary>
        /// 进度信息的分组
        /// </summary>
        public TGroup[] Groups => _groupMessageDict.Keys.ToArray();
        /// <summary>
        /// 全部进度信息
        /// </summary>
        public new string AllMessage
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var pair in _groupMessageDict)
                {
                    sb.AppendLine(pair.Key + ":");
                    foreach (var v in pair.Value)
                        sb.AppendLine(v);
                }
                return sb.ToString();
            }
        }
        /// <summary>
        /// 反序输出的全部进度信息
        /// </summary>
        public new string AllMessageReverse
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var par in _groupMessageDict)
                {
                    sb.AppendLine(par.Key + ":");
                    foreach (var v in par.Value.ToArray().Reverse())
                        sb.AppendLine(v);
                }
                return sb.ToString();
            }
        }
        /// <summary>
        /// 进度信息发生变化的事件
        /// </summary>
        public new event EventHandler<ProMsgChangedEventArgs<TGroup>> MessageChanged;
        /// <summary>
        /// 进度信息
        /// </summary>
        public ProgressMsgs(bool appendTime = true)
        {
            AppendTime = appendTime;
            _groupMessageDict = new Dictionary<TGroup, List<string>>();
            DefaultGroup = new TGroup();
            _groupMessageDict.Add(DefaultGroup, new List<string>());
        }


        /// <summary>
        /// 清除全部的进度信息
        /// </summary>
        public new void Clear()
        {
            _groupMessageDict.Clear();
        }
        /// <summary>
        /// 设置当前进度信息
        /// </summary>
        /// <param name="curMessage">当前进度信息</param>
        /// <param name="curGroup">当前进度信息所属的分组</param>
        /// <returns></returns>
        public void Info(string curMessage, TGroup curGroup)
        {
            curMessage = AppendTime ? $"{DateTime.Now:yyyy/MM/dd,HH:mm:ss}\t{curMessage}" : curMessage;
            if (_groupMessageDict.ContainsKey(curGroup))
                _groupMessageDict[curGroup].Add(curMessage);
            else
                _groupMessageDict.Add(curGroup, new List<string>(new[] { curMessage }));

            MessageChanged?.Invoke(this, new ProMsgChangedEventArgs<TGroup>(curGroup, curMessage));
        }
        /// <summary>
        /// 设置当前进度信息
        /// </summary>
        /// <param name="curMessage">当前进度信息</param>
        /// <returns></returns>
        public new void Info(string curMessage)
        {
            curMessage = AppendTime ? $"{DateTime.Now:yyyy/MM/dd,HH:mm:ss}\t{curMessage}" : curMessage;
            _groupMessageDict[DefaultGroup].Add(curMessage);

            MessageChanged?.Invoke(this, new ProMsgChangedEventArgs<TGroup>(DefaultGroup, curMessage));
        }
    }
}
