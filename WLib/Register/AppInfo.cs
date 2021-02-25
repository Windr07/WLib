/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020/9
// desc： None
// mdfy:  None
// sorc:  https://gitee.com/windr07/WLib
//        https://github.com/Windr07/WLib
//----------------------------------------------------------------*/

using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;

namespace WLib.Register
{
    /// <summary>
    /// 应用程序信息
    /// </summary>
    public class AppInfo
    {
        /// <summary>
        /// 软件ID
        /// </summary>
        [Description("软件ID")]
        public string Id { get; set; }
        /// <summary>
        /// 软件名称
        /// </summary>
        [Description("软件名称")]
        public string Name { get; set; }
        /// <summary>
        /// 软件标识或简称
        /// </summary>
        [Description("软件简称")]
        public string Key { get; set; }
        /// <summary>
        /// 软件版本
        /// </summary>
        [Description("软件版本")]
        public string Version { get; set; }
        /// <summary>
        /// 软件供应商
        /// </summary>
        [Description("软件供应商")]
        public string Company { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        [Description("备注")]
        public string Comment { get; set; }


        /// <summary>
        /// 应用程序信息
        /// </summary>
        public AppInfo() { }
        /// <summary>
        /// 应用程序信息
        /// </summary>
        /// <param name="id">软件ID</param>
        /// <param name="name">软件名称</param>
        /// <param name="version">软件版本</param>
        /// <param name="key">软件标识或简称，若为null则与软件名称<paramref name="name"/>相同</param>
        /// <param name="comment">备注信息</param>
        public AppInfo(string id, string name, string company, string key = null, string version = "v1.0.0.0", string comment = null)
        {
            Id = id;
            Name = name;
            Version = version;
            Company = company;
            Key = key ?? name;
            Comment = comment;
        }


        /// <summary>
        /// 输出软件名称和软件版本
        /// </summary>
        /// <returns></returns>
        public override string ToString() => string.IsNullOrWhiteSpace(Version) ? Name : $"{Name} {Version}";


        /// <summary>
        /// 获取默认应用程序域中的进程可执行文件的程序集信息
        /// <para>程序集信息来自：</para>
        /// <para>      项目 -> Properties -> AssemblyInfo.cs </para>
        /// <para>或者：项目 -> 属性 -> 应用程序 -> 程序集信息</para>
        /// </summary>
        /// <returns></returns>
        public static AppInfo FromEntryAssembly() => FromAssembly(Assembly.GetEntryAssembly());
        /// <summary>
        /// 获取程序集信息
        /// </summary>
        /// <param name="assemblyPath">程序集文件名或路径</param>
        /// <returns></returns>
        public static AppInfo FromAssembly(string assemblyPath) => FromAssembly(Assembly.LoadFrom(assemblyPath));
        /// <summary>
        /// 获取程序集信息
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static AppInfo FromAssembly(Assembly assembly)
        {
            var id = ((GuidAttribute)Attribute.GetCustomAttribute(assembly, typeof(GuidAttribute))).Value;
            var name = ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyTitleAttribute))).Title;
            var company = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCompanyAttribute))).Company;
            var version = assembly.GetName().Version.ToString();
            return new AppInfo
            {
                Id = id,
                Name = name,
                Version = version,
                Key = name,
                Company = company
            };
        }
    }
}
