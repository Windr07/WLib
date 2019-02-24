/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2/12
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using Microsoft.Win32;
using System;

namespace WLib.Envir.ArcGis.CheckVersion
{
    /// <summary>
    /// 检查、获取ArcGIS及其子程序安装路径
    /// </summary>
    public abstract class CheckArcGisInstall : ICheckArcGisInstall
    {
        /// <summary>
        /// 版本标识
        /// </summary>
        protected virtual string VersionTag { get; }
        /// <summary>
        /// 判断操作系统是否安装了ArcGIS Desktop
        /// </summary>
        /// <param name="installPath"></param>
        /// <returns></returns>
        protected virtual bool IsIntallDesktop(out string installPath)
        {
            RegistryKey regKey1 = Registry.LocalMachine.OpenSubKey($@"SOFTWARE\Wow6432Node\ESRI\Desktop{VersionTag}");
            RegistryKey regKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ESRI\CoreRuntime");

            installPath = GetInstallDirByRegistry(regKey1, regKey2);
            return System.IO.File.Exists(installPath);
        }
        /// <summary>
        /// 判断操作系统是否安装了ArcGIS Runtime
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsIntallRumtime(out string installPath)
        {
            RegistryKey regKey1 = Registry.LocalMachine.OpenSubKey($@"SOFTWARE\ESRI\Engine{VersionTag}\CoreRuntime");//@"SOFTWARE\ESRI\CoreRuntime"
            RegistryKey regKey2 = Registry.CurrentUser.OpenSubKey($@"SOFTWARE\ESRI\Engine{VersionTag}\CoreRuntime");

            installPath = GetInstallDirByRegistry(regKey1, regKey2);
            return System.IO.Directory.Exists(installPath);
        }
        /// <summary>
        /// 判断操作系统是否安装了ArcMap
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsInstallArcMap(out string installPath)
        {
            RegistryKey regKey1 = Registry.LocalMachine.OpenSubKey($@"SOFTWARE\Wow6432Node\ESRI\Desktop{VersionTag}");
            RegistryKey regKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ESRI\CoreRuntime");

            installPath = GetInstallDirByRegistry(regKey1, regKey2);
            if (installPath == null)
                return false;

            installPath = System.IO.Path.Combine(installPath, @"Bin\ArcMap.exe");
            return System.IO.File.Exists(installPath);
        }
        /// <summary>
        /// 判断操作系统是否安装了ArcGIS License
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsInstallLicense(out string installPath)
        {
            RegistryKey regKey1 = Registry.LocalMachine.OpenSubKey($@"SOFTWARE\Wow6432Node\ESRI\ArcGIS License Manager {VersionTag}");
            RegistryKey regKey2 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\ESRI\DataLicenseServers");

            installPath = GetInstallDirByRegistry(regKey1, regKey2);
            if (installPath == null)
                return false;

            installPath = System.IO.Path.Combine(installPath, @"Bin\LSAdmin.exe");
            return System.IO.File.Exists(installPath);
        }
        /// <summary>
        ///  判断操作系统是否安装了ArcGIS Administrator
        /// </summary>
        /// <param name="installPath"></param>
        /// <returns></returns>
        protected virtual bool IsInstallAdmin(out string installPath)
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\ESRI\ArcGIS");

            installPath = GetInstallDirByRegistry(regKey);
            if (installPath != null)
                installPath = System.IO.Path.Combine(installPath, @"Bin\ArcGISAdmin.exe");

            return System.IO.File.Exists(installPath);
        }
        /// <summary>
        /// 读注册表获取程序的安装路径
        /// （即：读多个注册表键，获取第一个不为null的InstallDir值，找不到则返回null）
        /// </summary>
        /// <param name="regKeys"></param>
        /// <returns></returns>
        protected virtual string GetInstallDirByRegistry(params RegistryKey[] regKeys)//读注册表，获取程序的安装路径
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
        /// <summary>
        /// ArcGIS 版本
        /// </summary>
        public virtual EArcGisVersion Version { get; }
        /// <summary>
        /// 判断操作系统是否安装了ArcGIS指定产品
        /// </summary>
        /// <param name="productType"></param>
        /// <param name="installPath"></param>
        /// <returns></returns>
        public virtual bool IsIntall(EArcGisProductType productType, out string installPath)
        {
            switch (productType)
            {
                case EArcGisProductType.Any:
                    return IsInstallLicense(out installPath) ||
                           IsInstallAdmin(out installPath) ||
                           IsIntallDesktop(out installPath) ||
                           IsIntallRumtime(out installPath);
                case EArcGisProductType.License: return IsInstallLicense(out installPath);
                case EArcGisProductType.Aministrator: return IsInstallAdmin(out installPath);
                case EArcGisProductType.Desktop: return IsIntallDesktop(out installPath);
                case EArcGisProductType.ArcMap: return IsInstallArcMap(out installPath);
                case EArcGisProductType.ArcEngineRuntime: return IsIntallRumtime(out installPath);
                default: throw new Exception($"未实现判断{nameof(productType)}是否安装的功能，请联系系统管理员");
            }
        }
        /// <summary>
        /// 获取ArcGIS指定产品的安装路径
        /// </summary>
        /// <returns></returns>
        public virtual string GetInstallPath(EArcGisProductType productType)
        {
            IsIntall(productType, out var installPath);
            return installPath;
        }
        /// <summary>
        /// 检查、获取ArcGIS及其子程序安装路径
        /// </summary>
        /// <param name="versionTag">版本标识</param>
        /// <param name="version">ArcGIS 版本</param>
        protected CheckArcGisInstall(string versionTag, EArcGisVersion version)
        {
            this.VersionTag = versionTag;
            this.Version = version;
        }
    }
}
