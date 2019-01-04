/*---------------------------------------------------------------- 
// auth： Unknown
// date： None
// desc： None
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System.Text;

namespace WLib.Envir.DotNet.CheckDotNetEx
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

    /// <summary>
    /// 获取.net Framework版本信息
    /// </summary>
    class CheckDotNetEx
    {
        /// <summary>
        /// 获取OS、CLR、Encoding和.net Framework版本信息
        /// </summary>
        public static string Check()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("      OS Version: {0}\r\n",System.Environment.OSVersion);
            sb.AppendFormat("     CLR Version: {0}  ( {1} )\r\n", System.Environment.Version, RuntimeFramework.CurrentFramework);
            sb.AppendFormat("Default Encoding: {0}\r\n", Encoding.Default);
            sb.AppendLine();
            sb.AppendLine("Available Frameworks:");
            foreach (var frame in RuntimeFramework.AvailableFrameworks)
                sb.AppendLine("  " + frame);
            return sb.ToString();

        }

        /// <summary>
        /// 获取已安装的.net Framework版本信息
        /// </summary>
        /// <returns></returns>
        public static string CheckDotNetVersion()
        {
            string result = string.Empty;
            foreach (var frame in RuntimeFramework.AvailableFrameworks)
                result += frame + "\r\n";
            return result;
        }
    }
}
