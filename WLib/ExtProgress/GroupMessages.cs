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
using System.Windows.Forms;
using WLib.ExtProgress.ProEventArgs;

namespace WLib.ExtProgress
{
    /// <summary>
    /// 通过键值对将信息分组
    /// </summary>
    public class GroupMessages
    {
        /// <summary>
        /// 分组信息
        /// </summary>
        protected Dictionary<string, List<string>> GroupMessageDict { get;}
        /// <summary>
        /// 是否在获取信息时包含时间
        /// </summary>
        public bool MessageWithTime;
        /// <summary>
        /// 设置指定分组的新信息（分组不存在则创建），并触发MessageChanged事件
        /// </summary>
        /// <param name="groupName">分组名称</param>
        /// <returns></returns>
        public string this[string groupName]
        {
            set
            {
                string curMsg = MessageWithTime ? System.DateTime.Now.ToString("yyyy/MM/dd,HH:mm:ss") + " " + value : value;

                if (GroupMessageDict.ContainsKey(groupName))
                {
                    GroupMessageDict[groupName].Add(curMsg);
                }
                else
                {
                    List<string> values = new List<string>();
                    values.Add(curMsg);
                    GroupMessageDict.Add(groupName, values);
                }
                Application.DoEvents();
                MessageChanged(this, new GroupMsgChangedEventArgs(groupName, curMsg));
            }
        }
        /// <summary>
        /// 分组数
        /// </summary>
        public int GroupCount => GroupMessageDict.Keys.Count;

        /// <summary>
        /// 分组信息（GroupMessages）发生变化的事件。
        /// </summary>
        public event EventHandler<GroupMsgChangedEventArgs> MessageChanged;

        /// <summary>
        /// 通过键值对将信息分组
        /// </summary>
        /// <param name="withTime"></param>
        public GroupMessages(bool withTime = false)
        {
            GroupMessageDict = new Dictionary<string, List<string>>();
            MessageWithTime = withTime;
            MessageChanged = new EventHandler<GroupMsgChangedEventArgs>((object sender, GroupMsgChangedEventArgs e) => { });
        }
        /// <summary>
        /// 判断是否存在指定的分组
        /// </summary>
        /// <param name="groupName">分组名称</param>
        /// <returns></returns>
        public bool ContainsKey(string groupName)
        {
            return GroupMessageDict.ContainsKey(groupName);
        }
        /// <summary>
        /// 获取指定分组的信息（必须已存在指定分组，否则抛出异常）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string[] GetGroupMessage(string key)
        {
            return GroupMessageDict[key].ToArray();
        }
        /// <summary>
        /// 获取所有分组的全部信息
        /// </summary>
        /// <returns></returns>
        public virtual string GetAllMessage()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var par in GroupMessageDict)
            {
                sb.AppendLine(par.Key + ":");
                foreach (var v in par.Value)
                {
                    sb.AppendLine(v);
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 反序分组内的信息，获取所有分组的全部信息
        /// </summary>
        /// <returns></returns>
        public virtual string GetAllMessageReverse()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var par in GroupMessageDict)
            {
                sb.AppendLine(par.Key + ":");
                foreach (var v in par.Value.ToArray().Reverse())
                {
                    sb.AppendLine(v);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 清空所有分组信息
        /// </summary>
        public void Clear()
        {
            GroupMessageDict.Clear();
        }
    }
}
