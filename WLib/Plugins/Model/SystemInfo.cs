using System.IO;
using WLib.Plugins.Interface;

namespace WLib.Plugins.Model
{
    /// <summary>
    /// 应用软件系统信息
    /// </summary>
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
        /// 应用软件程序集路径
        /// <para>即exe文件路径</para>
        /// </summary>
        public string AppPath { get; set; }
        /// <summary>
        /// 应用软件所在目录
        /// </summary>
        public string AppDir => Path.GetDirectoryName(AppPath);


        /// <summary>
        /// 应用软件系统信息
        /// </summary>
        public SystemInfo() { }
        /// <summary>
        /// 应用软件系统信息
        /// </summary>
        /// <param name="appAssemblyPath">应用软件主程序集路径（exe文件路径）</param>
        /// <param name="appName">应用软件的名称</param>
        /// <param name="appId">应用软件的ID</param>
        public SystemInfo(string appAssemblyPath, string appName, string appId)
        {
            AppPath = appAssemblyPath;
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
