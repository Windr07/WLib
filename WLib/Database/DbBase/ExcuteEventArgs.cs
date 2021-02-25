/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Data;

namespace WLib.Database.DbBase
{
    /// <summary>
    /// 执行SQL查询前后应进行的操作事件的处理参数
    /// </summary>
    public class ExcuteEventArgs : EventArgs
    {
        /// <summary>
        /// SQL操作名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 表示连接到数据源时执行的 SQL 语句，并由访问关系数据库的 .NET Framework 数据提供程序实现。
        /// </summary>
        public IDbCommand Command { get; set; }
        /// <summary>
        /// 执行SQL查询前后应进行的操作事件的处理参数
        /// </summary>
        /// <param name="name">SQL操作名称</param>
        /// <param name="cmd">表示连接到数据源时执行的 SQL 语句，并由访问关系数据库的 .NET Framework 数据提供程序实现。</param>
        public ExcuteEventArgs(string name, IDbCommand cmd)
        {
            Name = name;
            Command = cmd;
        }
    }
}
