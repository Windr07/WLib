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
    public static class OSEnvironment
    {
        /// <summary>
        /// Windows操作系统版本
        /// </summary>
        private static EWinVersion _winVersion;

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
        public static bool Is64Bit => GetIs64Bit();
        /// <summary>
        /// Windows操作系统版本信息
        /// </summary>
        public static string OSName => _winVersion.ToString();
        /// <summary>
        /// 检查操作系统版本
        /// </summary>
        private static EWinVersion GetVersion()
        {
            Version ver = Environment.OSVersion.Version;
            int major = ver.Major;  //主版本
            int minor = ver.Minor;  //次版本
            switch (major)
            {
                case 4 when minor == 0: return EWinVersion.Win95;
                case 4 when minor == 10: return EWinVersion.Win98;
                case 4 when minor == 90: return EWinVersion.WinMe;
                case 5 when minor == 0: return EWinVersion.Win2000;
                case 5 when minor == 1: return EWinVersion.XP;
                case 6 when minor == 0: return EWinVersion.Vista;
                case 6 when minor == 1: return EWinVersion.Win7;
                case 6 when minor == 2: return EWinVersion.Win8或以上;
                default: return EWinVersion.UnKnown;
            }
        }
        /// <summary>
        /// 判断系统是否为64位系统
        /// </summary>
        /// <returns></returns>
        private static bool GetIs64Bit()
        {
            IsWow64Process(Process.GetCurrentProcess().Handle, out var retVal);
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
