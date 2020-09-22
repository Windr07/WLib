/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020/06/15
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;

namespace WLib.Web.Update
{
    /// <summary>
    /// 文件信息，包括文件名、相对路径、文件MD5值等
    /// </summary>
    [Serializable]
    public class FileMd5Info
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件路径或相对路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 文件MD5值
        /// </summary>
        public string MD5 { get; set; }
        /// <summary>
        /// 文件变更状态
        /// </summary>
        public Dictionary<DateTime, WatcherChangeTypes> UpdateStates { get; set; }


        /// <summary>
        /// 文件信息，包括文件名、相对路径、文件MD5值等
        /// </summary>
        public FileMd5Info() => UpdateStates = new Dictionary<DateTime, WatcherChangeTypes>();
        /// <summary>
        /// 文件信息，包括文件名、相对路径、文件MD5值等
        /// </summary>
        /// <param name="path">相对路径</param>
        /// <param name="md5">文件MD5值</param>
        public FileMd5Info(string path, string md5) : this()
        {
            Name = System.IO.Path.GetFileName(path);
            Path = path;
            MD5 = md5;
        }
        /// <summary>
        /// 文件信息，包括文件名、相对路径、文件MD5值等
        /// </summary>
        /// <param name="path">相对路径</param>
        /// <param name="md5">文件MD5值</param>
        /// <param name="dateTime">文件变更状态的时间</param>
        /// <param name="updateState">文件变更状态</param>
        public FileMd5Info(string path, string md5, DateTime dateTime, WatcherChangeTypes updateState) : this( path, md5)
        {
            UpdateStates.Add(dateTime, updateState);
        }
    }
}
