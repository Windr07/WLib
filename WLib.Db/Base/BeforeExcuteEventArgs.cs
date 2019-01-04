/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Db.Base
{
    /// <summary>
    /// 执行SQL语句之前应进行的操作事件的处理参数
    /// </summary>
    public class BeforeExcuteEventArgs : EventArgs
    {
        /// <summary>
        /// SQL操作名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// SQL语句
        /// </summary>
        public string Sql { get; set; }
        /// <summary>
        /// 执行SQL语句之前应进行的操作事件的处理参数
        /// </summary>
        /// <param name="name">SQL操作名称</param>
        /// <param name="sql">SQL语句</param>
        public BeforeExcuteEventArgs(string name, string sql)
        {
            Name = name;
            Sql = sql;
        }
    }
}
