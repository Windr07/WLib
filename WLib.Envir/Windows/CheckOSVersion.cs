/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WLib.Envir.Windows
{
    /* *.NET提供的获取操作系统信息的方法：
    * System.OperatingSystem os = System.Environment.OSVersion;
    * Console.WriteLine("OS版本：" + os.ToString());
    * PlatformID platformID = os.Platform;    //获取操作系统ID
    * Version ver = os.Version;               //获取操作系统版本
    */

    /// <summary>
    /// 提供获取操作系统版本和位数的信息
    /// </summary>
    public static class CheckOSVersion
    {
        /// <summary>
        /// Windows操作系统版本信息
        /// </summary>
        private static string _osName;
        /// <summary>
        /// Windows操作系统版本
        /// </summary>
        private static EWinVersion _winVersion;
        /// <summary>
        /// 是否为64位系统
        /// </summary>
        private static bool _is64Bit;

        /// <summary>
        /// Windows操作系统版本信息
        /// </summary>
        public static string OSName
        {
            get
            {
                if (_osName == null)
                    _osName = GetOSName();
                return _osName;
            }
        }
        /// <summary>
        /// Windows操作系统版本
        /// </summary>
        public static EWinVersion WinVersion
        {
            get
            {
                if (_winVersion == EWinVersion.UnKnown)
                    _winVersion = GetVersion();
                return _winVersion;
            }
        }
        /// <summary>
        /// 是否为64位系统
        /// </summary>
        public static bool Is64Bit
        {
            get
            {
                _is64Bit = GetIs64Bit();
                return _is64Bit;
            }
        }

        /// <summary>
        /// 检查操作系统版本
        /// </summary>
        private static EWinVersion GetVersion()
        {
            Version ver = System.Environment.OSVersion.Version;
            int major = ver.Major;  //主版本
            int minor = ver.Minor;  //次版本
            EWinVersion winVersion = EWinVersion.UnKnown;

            if (major == 4 && minor == 0)
                winVersion = EWinVersion.Win95;
            else if (major == 4 && minor == 10)
                winVersion = EWinVersion.Win98;
            else if (major == 4 && minor == 90)
                winVersion = EWinVersion.WinMe;
            else if (major == 5 && minor == 0)
                winVersion = EWinVersion.Win2000;
            else if (major == 5 && minor == 1)
                winVersion = EWinVersion.XP;
            else if (major == 6 && minor == 0)
                winVersion = EWinVersion.Vista;
            else if (major == 6 && minor == 1)
                winVersion = EWinVersion.Win7;
            else if (major == 6 && minor == 2)
                winVersion = EWinVersion.Win8或以上;
            else
                winVersion = EWinVersion.UnKnown;

            return winVersion;
        }
        /// <summary>
        /// 检查操作系统版本
        /// </summary>
        private static string GetOSName()
        {
            Version ver = System.Environment.OSVersion.Version;
            int major = ver.Major;  //主版本
            int minor = ver.Minor;  //次版本
            string strName = string.Empty;

            if (major == 4 && minor == 0)
                strName = "Win 95";
            else if (major == 4 && minor == 10)
                strName = "Win 98";
            else if (major == 4 && minor == 90)
                strName = "Win Me";
            else if (major == 5 && minor == 0)
                strName = "Win 2000";
            else if (major == 5 && minor == 1)
                strName = "Win XP";
            else if (major == 6 && minor == 0)
                strName = "Win Vista";
            else if (major == 6 && minor == 1)
                strName = "Win 7";
            else if (major == 6 && minor == 2)
                strName = "Win 8/Win8.1/Win10";
            else
                strName = "未知";

            return strName;
        }
        /// <summary>
        /// 判断系统是否为64位系统
        /// </summary>
        /// <returns></returns>
        private static bool GetIs64Bit()
        {
            bool retVal;
            IsWow64Process(Process.GetCurrentProcess().Handle, out retVal);
            return retVal;
        }

        /// <summary>
        /// 判断系统是32位还是64位的
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpSystemInfo"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool lpSystemInfo);
    }
}
