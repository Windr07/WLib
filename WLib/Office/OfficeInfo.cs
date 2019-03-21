/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace WLib.Office
{
    /// <summary>
    /// Microsoft Office软件信息
    /// </summary>
    public class OfficeInfo
    {
        /// <summary>
        /// 已安装的Office版本列表
        /// </summary>
        private static List<EOfficeVersion> _officeVersions = null;
        /// <summary>
        /// 已安装的Office版本的安装路径列表
        /// </summary>
        private static List<String> _officeInstallPaths = null;

        /// <summary>
        /// 已安装的Office版本列表
        /// </summary>
        public static EOfficeVersion[] OfficeVersions
        {
            get
            {
                if (_officeVersions == null)
                    GetOfficeVersion();
                return _officeVersions.ToArray();
            }
        }
        /// <summary>
        /// 已安装的Office版本的安装路径列表
        /// </summary>
        public static String[] OfficeInstallPaths
        {
            get
            {
                if (_officeInstallPaths == null)
                    GetOfficeVersion();
                return _officeInstallPaths.ToArray();
            }
        }


        /// <summary>
        /// 获取已安装的Office版本
        /// （对_officeVersion和_officeInstallPaths赋值）
        /// </summary>
        /// <returns></returns>
        private static void GetOfficeVersion()
        {
            if (_officeVersions == null)
            {
                _officeVersions = new List<EOfficeVersion>();
                _officeInstallPaths = new List<string>();

                string path = null;
                try
                {
                    //office97
                    var name = @"SOFTWARE\Microsoft\Office\8.0\Common\InstallRoot";
                    var strKeyName = "OfficeBin";
                    if (GetRegValue(name, strKeyName, ref path))
                    {
                        _officeVersions.Add(EOfficeVersion.Office97);
                        _officeInstallPaths.Add(path);
                    }

                    //Office2000
                    name = @"SOFTWARE\Microsoft\Office\9.0\Common\InstallRoot";
                    strKeyName = "Path";
                    if (GetRegValue(name, strKeyName, ref path))
                    {
                        _officeVersions.Add(EOfficeVersion.Office2000);
                        _officeInstallPaths.Add(path);
                    }

                    //OfficeXP
                    name = @"SOFTWARE\Microsoft\Office\10.0\Common\InstallRoot";
                    strKeyName = "Path";
                    if (GetRegValue(name, strKeyName, ref path))
                    {
                        _officeVersions.Add(EOfficeVersion.OfficeXP);
                        _officeInstallPaths.Add(path);
                    }

                    //Office2003
                    name = @"SOFTWARE\Microsoft\Office\11.0\Common\InstallRoot";
                    strKeyName = "Path";
                    if (GetRegValue(name, strKeyName, ref path))
                    {
                        _officeVersions.Add(EOfficeVersion.Office2003);
                        _officeInstallPaths.Add(path);
                    }

                    //Office2007
                    name = @"SOFTWARE\Microsoft\Office\12.0\Common\InstallRoot";
                    strKeyName = "Path";
                    if (GetRegValue(name, strKeyName, ref path))
                    {
                        _officeVersions.Add(EOfficeVersion.Office2007);
                        _officeInstallPaths.Add(path);
                    }

                    //Office2010
                    name = @"SOFTWARE\Microsoft\Office\14.0\Common\InstallRoot";
                    strKeyName = "Path";
                    if (GetRegValue(name, strKeyName, ref path))
                    {
                        _officeVersions.Add(EOfficeVersion.Office2010);
                        _officeInstallPaths.Add(path);
                    }

                    //Office2013
                    name = @"SOFTWARE\Microsoft\Office\15.0\Common\InstallRoot";
                    strKeyName = "Path";
                    if (GetRegValue(name, strKeyName, ref path))
                    {
                        _officeVersions.Add(EOfficeVersion.Office2013);
                        _officeInstallPaths.Add(path);
                    }

                    //Office2016
                    name = @"SOFTWARE\Microsoft\Office\16.0\Common\InstallRoot";
                    strKeyName = "Path";
                    if (GetRegValue(name, strKeyName, ref path))
                    {
                        _officeVersions.Add(EOfficeVersion.Office2016);
                        _officeInstallPaths.Add(path);
                    }
                }
                catch (System.Security.SecurityException ex)
                {
                    throw new System.Security.SecurityException("您没有读取注册表的权限", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("读取注册表出错!" + ex.Message);
                }
            }
        }
        /// <summary>
        /// 查找注册表获取注册表项的字符串值，返回值确定是否能找到此项的值。
        /// </summary>
        /// <param name="name">要打开的子项的名称或路径</param>
        /// <param name="strKeyName">要检索的值的名称</param>
        /// <param name="value">注册表值</param>
        /// <returns></returns>
        private static bool GetRegValue(string name, string strKeyName, ref string value)
        {
            bool isSuccess = false;
            Microsoft.Win32.RegistryKey regSubKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(name, false);

            object objResult = regSubKey?.GetValue(strKeyName);
            if (objResult != null)
            {
                Microsoft.Win32.RegistryValueKind regValueKind = regSubKey.GetValueKind(strKeyName);
                if (regValueKind == Microsoft.Win32.RegistryValueKind.String)
                {
                    value = objResult.ToString();
                    isSuccess = true;
                }
            }
            regSubKey?.Close();

            return isSuccess;
        }
    }
}
