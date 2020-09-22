/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using WLib.Plugins.Interface;

namespace WLib.Plugins.Model
{
    /// <summary>
    /// 应用软件系统信息
    /// </summary>
    [Serializable]
    public class SystemInfo : ISystemInfo
    {
        /// <summary>
        /// 应用软件的ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 应用软件的名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 应用软件的标题
        /// </summary>
        public string Text { get; set; }
      

        /// <summary>
        /// 应用软件系统信息
        /// </summary>
        public SystemInfo() { }
        /// <summary>
        /// 应用软件系统信息
        /// </summary>
        /// <param name="appName">应用软件的名称</param>
        /// <param name="appId">应用软件的ID</param>
        public SystemInfo(string appName, string appId)
        {
            Name = appName;
            Text = appName;
            Id = appId;
        }
        /// <summary>
        /// 输出<see cref="Text"/>的值
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Text;
    }
}
