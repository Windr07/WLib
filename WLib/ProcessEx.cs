/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020/10
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Diagnostics;
using System.IO;

namespace WLib
{
    /// <summary>
    /// 提供进程操作的相关方法
    /// </summary>
    public static class ProcessEx
    {
        /// <summary>
        /// 以管理员身份运行程序
        /// </summary>
        /// <param name="path"></param>
        public static void StartByAdmin(string path)
        {
            ////当前用户是管理员的时候，直接启动应用程序，如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行
            ////获得当前登录的Windows用户标示
            //System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            //System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            ////判断当前登录用户是否为管理员
            //if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            //{
            //    Process.Start(path); //如果是管理员，则直接运行
            //    return;
            //}

            //创建启动对象
            var startInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Path.GetDirectoryName(path), //Environment.CurrentDirectory;
                FileName = path,
                Verb = "runas"//设置启动动作,确保以管理员身份运行
            };
            Process.Start(startInfo);
        }
    }
}
