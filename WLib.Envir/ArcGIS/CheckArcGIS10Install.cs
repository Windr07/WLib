/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using Microsoft.Win32;

namespace WLib.Envir.ArcGIS
{
    /// <summary>
    /// 检查、获取ArcGIS及其子程序安装路径
    /// </summary>
    public class CheckArcGIS10Install : ICheckArcGisInstall
    {
        /// <summary>
        /// ArcGIS版本
        /// </summary>
        public EArcGisVersion Version => EArcGisVersion.ArcGIS100;


        /// <summary>
        /// 判断操作系统是否安装了ArcGIS10.0 Desktop
        /// </summary>
        /// <param name="installPath"></param>
        /// <returns></returns>
        public bool IsIntallDesktop(out string installPath)
        {
            RegistryKey regKey1 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\ESRI\Desktop10.0");
            RegistryKey regKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ESRI\CoreRuntime");

            installPath = GetInstallDirByRegistry(regKey1, regKey2);
            return System.IO.File.Exists(installPath);
        }
        /// <summary>
        /// 判断操作系统是否安装了ArcGIS10.0 Runtime
        /// </summary>
        /// <returns></returns>
        public bool IsIntallRumtime(out string installPath)
        {
            RegistryKey regKey1 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ESRI\Engine10.0\CoreRuntime");//@"SOFTWARE\ESRI\CoreRuntime"
            RegistryKey regKey2 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\ESRI\Engine10.0\CoreRuntime");

            installPath = GetInstallDirByRegistry(regKey1, regKey2);
            return System.IO.Directory.Exists(installPath);
        }
        /// <summary>
        /// 判断操作系统是否安装了ArcMap 10.0
        /// </summary>
        /// <returns></returns>
        public bool IsInstallArcMap(out string installPath)
        {
            RegistryKey regKey1 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\ESRI\Desktop10.0");
            RegistryKey regKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ESRI\CoreRuntime");

            installPath = GetInstallDirByRegistry(regKey1, regKey2);
            if (installPath == null)
                return false;

            installPath = System.IO.Path.Combine(installPath, @"Bin\ArcMap.exe");
            return System.IO.File.Exists(installPath);
        }
        /// <summary>
        /// 判断操作系统是否安装了ArcGIS10.0 License
        /// </summary>
        /// <returns></returns>
        public bool IsInstallLicense(out string installPath)
        {
            RegistryKey regKey1 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\ESRI\ArcGIS License Manager 10.0");
            RegistryKey regKey2 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\ESRI\DataLicenseServers");

            installPath = GetInstallDirByRegistry(regKey1, regKey2);
            if (installPath == null)
                return false;

            installPath = System.IO.Path.Combine(installPath, @"Bin\LSAdmin.exe");
            return System.IO.File.Exists(installPath);
        }
        /// <summary>
        ///  判断操作系统是否安装了ArcGIS Administrator 10.0
        /// </summary>
        /// <param name="installPath"></param>
        /// <returns></returns>
        public bool IsInstallAdmin(out string installPath)
        {
            RegistryKey regKey1 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\ESRI\ArcGIS");

            installPath = GetInstallDirByRegistry(regKey1);
            if (installPath != null)
                installPath = System.IO.Path.Combine(installPath, @"Bin\ArcGISAdmin.exe");

            return System.IO.File.Exists(installPath);
        }


        /// <summary>
        /// 获取ArcMap的安装路径
        /// </summary>
        /// <returns></returns>
        public string GetArcMapPath()
        {
            IsInstallArcMap(out var path);
            return path;
        }
        /// <summary>
        /// 获取License的安装路径
        /// </summary>
        /// <returns></returns>
        public string GetLicensePath()
        {
            IsInstallLicense(out var path);
            return path;
        }
        /// <summary>
        /// 获取ArcGIS Administrator的安装路径
        /// </summary>
        /// <returns></returns>
        public string GetAdminPath()
        {
            IsInstallAdmin(out var path);
            return path;
        }
        /// <summary>
        ///  获取ArcGIS Desktop的安装目录
        /// </summary>
        /// <returns></returns>
        public string GetDesktopPath()
        {
            IsIntallDesktop(out var path);
            return path;
        }
        /// <summary>
        /// 读注册表获取程序的安装路径
        /// （即：读多个注册表键，获取第一个不为null的InstallDir值）
        /// </summary>
        /// <param name="regKeys"></param>
        /// <returns></returns>
        protected string GetInstallDirByRegistry(params RegistryKey[] regKeys)//读注册表，获取程序的安装路径
        {
            foreach (var regKey in regKeys)
            {
                if (regKey != null)
                {
                    string installDir = (string)regKey.GetValue("InstallDir");
                    if (!string.IsNullOrEmpty(installDir) && installDir.Trim() != string.Empty)
                        return installDir;
                }
            }
            return null;
        }
    }
}
