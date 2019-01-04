/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2016/12/16 14:47:52
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using Microsoft.Win32;

namespace WLib.Envir.DotNet
{
    /* 注意：.NET Framework 4.5 是与版本 1.1、2.0 和 3.5 并行运行的，并且是取代版本 4 的就地更新。 
     * 对于以版本 1.1、2.0 和 3.5 为目标的应用程序，你可以在目标计算机上安装适当的 .NET Framework 版本以在其最佳环境中运行该应用程序。
     * （http://msdn.microsoft.com/zh-cn/library/ff602939.aspx ）  
     * 1、Windows的.net framework安装:
          Windows XP： 默认已安装2.0
          Windows7： 默认已安装3.5（3.5sp1需在“启用或关闭windows功能”中安装，4.0需单独安装）
          Windows8:  默认已安装4.5（3.5sp1需在“启用或关闭windows功能”中安装，4.0需单独安装）
          Windows10: 默认已安装4.6（3.5sp1需在“启用或关闭windows功能”中安装，4.0需单独安装）
     *    关于操作系统自带.net版本：http://www.cnblogs.com/tough/p/5103713.html
     *    或者 https://blogs.msdn.microsoft.com/astebner/2007/03/14/mailbag-what-version-of-the-net-framework-is-included-in-what-version-of-the-os/ 
     * 2、Windows8安装.Net Framework3.5有以下三种方法：
          （1）自动联网安装：需要在控制面板→启动或关闭windows功能中设置，勾选.NET Framework3.5(包括.NET 2.0和3.0部分)，等待windows联网安装。
          （2）手动本地安装：使用网上的.net 3.5安装包进行安装，并在控制面板→启动或关闭windows功能中，勾选.NET Framework3.5(包括.NET 2.0和3.0部分)   
          （3）在win8系统ISO文件中安装：载入ISO文件，cmd中，输入dism.exe /online /enable-feature /featurename:NetFX3 /Source:F:\sources\sxs
           下载地址(64位)：http://www.ddooo.com/softdown/25627.htm 
           下载地址(32位)：http://www.bkill.com/download/21488.html 
     */
    public class CheckDotNet
    {
        /// <summary>
        /// 获取已安装的.NET Framework版本信息
        /// </summary>
        /// <returns></returns>
        public static DotNetFrameworkInfo[] GetAllNetFrameworkInfo()
        {
            RegistryKey ndpKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "").
                OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\");

            List<DotNetFrameworkInfo> infoList = new List<DotNetFrameworkInfo>();
            foreach (string versionKeyName in ndpKey.GetSubKeyNames())
            {
                if (versionKeyName.StartsWith("v"))
                {
                    RegistryKey versionKey = ndpKey.OpenSubKey(versionKeyName);
                    string install = versionKey.GetValue("Install", "").ToString();
                    string version = versionKey.GetValue("Version", "").ToString();
                    string installPath = versionKey.GetValue("InstallPath", "").ToString();
                    string sp = versionKey.GetValue("SP", "").ToString();
                    if (!string.IsNullOrEmpty(install))
                    {
                        infoList.Add(new DotNetFrameworkInfo(versionKeyName, version, installPath, sp));
                    }
                    else
                    {
                        foreach (string subKeyName in versionKey.GetSubKeyNames())
                        {
                            RegistryKey subVersionKey = versionKey.OpenSubKey(subKeyName);
                            DotNetFrameworkInfo info = GetV4NetFrameworkInfo(subVersionKey);
                            if (info != null)
                                infoList.Add(info);
                        }
                    }
                }
            }

            ndpKey.Close();
            return infoList.ToArray();
        }
        /// <summary>
        /// 获取.net 4.0或以上的版本信息
        /// </summary>
        /// <param name="versionKey"></param>
        /// <returns></returns>
        private static DotNetFrameworkInfo GetV4NetFrameworkInfo(RegistryKey versionKey)
        {
            DotNetFrameworkInfo info = null;
            string install = versionKey.GetValue("Install", "").ToString();
            if (!string.IsNullOrEmpty(install))
            {
                string keyName = versionKey.Name;
                string name =
                    $"v{versionKey.GetValue("Version", "").ToString()} {keyName.Substring(keyName.LastIndexOf('\\') + 1)}";
                string installPath = versionKey.GetValue("InstallPath", "").ToString();
                string sp = versionKey.GetValue("SP", "").ToString();
                info = new DotNetFrameworkInfo(name, name, installPath, sp);
            }
            return info;
        }
    }
}
