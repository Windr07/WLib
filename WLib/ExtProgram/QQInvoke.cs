/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2016/12/20 23:46:27
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using Microsoft.Win32;

namespace WLib.ExtProgram
{
    /// <summary>
    /// 启动QQ聊天
    /// </summary>
    public class QQInvoke
    {
        /// <summary>
        /// 32位系统 Tecent QQ 注册表位置
        /// </summary>
        public string OpenKey32 = @"SOFTWARE\Tencent\QQ2009";
        /// <summary>
        /// 64位系统 Tecent QQ 注册表位置
        /// </summary>
        public string OpenKey64 = @"SOFTWARE\Wow6432Node\Tencent\QQ2009";

        /// <summary>
        /// "找不到QQ的安装路径"
        /// </summary>
        public string NotfoundInstallPathMsg = "找不到QQ的安装路径";
        /// <summary>
        /// "您的电脑未安装Tecent QQ"
        /// </summary>
        public string UninstallMsg = "您的电脑未安装Tecent QQ";

        /// <summary>
        /// 获取注册表项
        /// </summary>
        /// <returns></returns>
        private RegistryKey GetRegisteKey()
        {
            RegistryKey regkey = null;
            try
            {
                regkey = Registry.LocalMachine.OpenSubKey(OpenKey32);
            }
            catch
            {
                try
                {
                    regkey = Registry.LocalMachine.OpenSubKey(OpenKey64);
                }
                catch { }
            }
            return regkey;
        }

        /// <summary>
        /// 打开与指定QQ号聊天的窗口
        /// </summary>
        /// <param name="qqNumber">QQ号</param>
        public void OpenQQ(string qqNumber)
        {
            string arg = "tencent://message/?uin=" + qqNumber.Trim() + "";

            RegistryKey regkey = GetRegisteKey();
            if (regkey != null)
            {
                var installPath = regkey.GetValue("Install", "").ToString();
                installPath += @"\Bin\Timwp.exe";
                if (System.IO.File.Exists(installPath))
                    System.Diagnostics.Process.Start(installPath, arg);
                else
                    throw new Exception(NotfoundInstallPathMsg);
            }
            else
            {
                throw new Exception(UninstallMsg);
            }
        }
        /// <summary>
        /// 打开QQ群聊天窗口（首先得加入该群）
        /// </summary>
        /// <param name="qqGroupIdNumber">QQ群号加密串（把QQ群拖到桌面上，将自动建立一个快捷方式，查看快捷方式属性，在"目标"框中能找到该加密串）</param>
        public void OpenQQGroup(string qqGroupIdNumber)
        {
            //首先得加入该群
            //"uin:"后边要加有空格，或者指定QQ号
            //示例：string arg = "/uin: /quicklunch:9C4244BF1D02E529B7F306DDE07810758DA4C2A6CC8C185E003C3625E0894632F3184A18E29708D6";
            string arg = "/uin: /quicklunch:" + qqGroupIdNumber;

            RegistryKey regkey = GetRegisteKey();
            if (regkey != null)
            {
                var installPath = regkey.GetValue("Install", "").ToString();
                installPath += @"\Bin\QQScLauncher.exe";
                if (System.IO.File.Exists(installPath))
                    System.Diagnostics.Process.Start(installPath, arg);
                else
                    throw new Exception(NotfoundInstallPathMsg);
            }
            else
            {
                throw new Exception(UninstallMsg);
            }
        }
    }
}
