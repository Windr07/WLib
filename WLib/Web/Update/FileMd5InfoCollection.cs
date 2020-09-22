/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020/06/15
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace WLib.Web.Update
{
    /// <summary>
    /// 包含MD5信息的多个文件信息集合，以及该文件集合的版本信息
    /// </summary>
    [Serializable]
    public class FileMd5InfoCollection
    {
        /// <summary>
        /// 文件集的版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        ///  包含MD5信息的多个文件信息集合
        /// </summary>
        public List<FileMd5Info> FileInfos { get; set; }
    }
}
