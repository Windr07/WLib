/*---------------------------------------------------------------- 
// auth： HuangWenlong
// date： 2017/1/8 18:20:42
// desc： None
// mdfy:  None
// ver.:  V1.0.0
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using YYGISLib.OS;

namespace YYGISLib.ExtProgram
{
    public class CheckArcGISInstall
    {
        /// <summary>
        /// 获取ArcGIS10.0 Desktop的安装目录（找不到或未安装时返回null或""）
        /// </summary>
        /// <returns></returns>
        public static string GetDesktopInstallPath()
        {
            string installDir = null;
            if (OSCheck.Is64Bit())
            {
                installDir = ReadRegistry(@"SOFTWARE\Wow6432Node\ESRI\Desktop10.0");//64位
            }
            else
            {
                installDir = ReadRegistry("SOFTWARE\\ESRI\\CoreRuntime");//32位
            }
            return installDir;
        }
        /// <summary>
        /// 判断操作系统是否安装了ArcGIS10.0 Runtime
        /// </summary>
        /// <returns></returns>
        public static bool CheckRutime10(out string installDir)
        {
            RegistryKey installDirRegKey1 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ESRI\Engine10.0\CoreRuntime");//@"SOFTWARE\ESRI\CoreRuntime"
            RegistryKey installDirRegKey2 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\ESRI\Engine10.0\CoreRuntime");

            installDir = null;
            bool checkResult = false;
            if (installDirRegKey1 != null)
            {
                installDir = installDirRegKey1.GetValue("InstallDir", "").ToString();
                checkResult = true;
            }
            else if (installDirRegKey2 != null)
            {
                installDir = installDirRegKey2.GetValue("InstallDir", "").ToString();
                checkResult = true;
            }
            return checkResult;
        }
        /// <summary>
        /// 判断操作系统是否安装了ArcMap 10.0
        /// </summary>
        /// <returns></returns>
        public static bool CheckArcMap(out string installDir)
        {
            bool checkResult = false;
            installDir = null;
            if (OSCheck.Is64Bit())
            {
                //获得ArcGIS的安装路径
                string sInstall = ReadRegistry(@"SOFTWARE\Wow6432Node\ESRI\Desktop10.0");//64位
                string arcMapPath = sInstall + @"Bin\ArcMap.exe";
                if (File.Exists(arcMapPath))
                {
                    checkResult = true;
                    installDir = arcMapPath;
                }
            }
            else
            {
                //取得ArcGIS安装路径
                string sInstall = ReadRegistry("SOFTWARE\\ESRI\\CoreRuntime");//32位
                string arcMapPath = sInstall + @"Bin\ArcMap.exe";
                if (File.Exists(arcMapPath))
                {
                    checkResult = true;
                    installDir = arcMapPath;
                }
            }
            return checkResult;
        }
        /// <summary>
        /// 判断操作系统是否安装了ArcGIS10.0 License
        /// </summary>
        /// <returns></returns>
        public static bool CheckLicense10(out string installDir)
        {
            bool checkResult = false;
            installDir = null;
            RegistryKey installDirRegKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\ESRI\DataLicenseServers");
            if (installDirRegKey != null)
            {
                checkResult = true;
                installDir = installDirRegKey.GetValue("InstallDir", "").ToString();
            }
            return checkResult;
        }

        /// <summary>
        /// 判断能否找到ArcGIS License程序路径
        /// </summary>
        /// <returns></returns>
        public static bool FindArcGISLicenseInstallPath(out string installPath)
        {
            bool isExist = false;
            installPath = null;
            string[] installPaths = new string[]
            {
                @"C:\Program Files (x86)\ArcGIS\License10.0\bin\LSAdmin.exe",
                @"D:\Program Files (x86)\ArcGIS\License10.0\bin\LSAdmin.exe",
                @"E:\Program Files (x86)\ArcGIS\License10.0\bin\LSAdmin.exe",
                @"F:\Program Files (x86)\ArcGIS\License10.0\bin\LSAdmin.exe"
            };
            foreach (var path in installPaths)
            {
                if (System.IO.File.Exists(path))
                {
                    isExist = true;
                    installPath = path;
                    break;
                }
            }
            return isExist;
        }
        /// <summary>
        /// 判断能否找到ArcGIS Administrator程序路径
        /// </summary>
        /// <returns></returns>
        public static bool FindArcGISAdministratorInstallPath(out string installPath)
        {
            bool isExist = false;
            installPath = null;
            string[] installPaths = new string[]
            {
                @"C:\Program Files (x86)\Common Files\ArcGIS\bin\ArcGISAdmin.exe",
                @"D:\Program Files (x86)\Common Files\ArcGIS\bin\ArcGISAdmin.exe",
                @"E:\Program Files (x86)\Common Files\ArcGIS\bin\ArcGISAdmin.exe",
                @"F:\Program Files (x86)\Common Files\ArcGIS\bin\ArcGISAdmin.exe",
            };
            foreach (var path in installPaths)
            {
                if (System.IO.File.Exists(path))
                {
                    isExist = true;
                    installPath = path;
                    break;
                }
            }
            return isExist;
        }

        /// <summary>
        /// 读注册表，获取程序的安装路径
        /// </summary>
        /// <param name="sKey"></param>
        /// <returns></returns>
        private static string ReadRegistry(string sKey)//读注册表，获取程序的安装路径
        {
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sKey, false);
            if (rk == null) return "";
            return (string)rk.GetValue("InstallDir");
        }

    }
}
