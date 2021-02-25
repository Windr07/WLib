/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Management;

namespace WLib.Computer
{
    /// <summary> 
    /// 计算机的硬件信息
    /// </summary> 
    public class Hardware
    {
        private static string _osDrive;
        private static string _cpuId;
        private static string _biosSerialNumber;
        private static string _baseBoardSerialNumber;
        private static string _diskId;
        private static string _diskSerialNumber;

        /// <summary>
        /// 获取系统所在盘符（e.g."c:\"）
        /// </summary>
        /// <returns></returns>
        public static string OsDriver => _osDrive ?? (_osDrive = Environment.GetEnvironmentVariable("systemdrive"));
        /// <summary>
        /// 获取CPU的ID
        /// </summary>
        /// <returns></returns>
        public static string CpuId => _cpuId ?? (_cpuId = GetHardWareInfo("Win32_Processor", "ProcessorId"));
        /// <summary>
        /// 获取BIOS序列号
        /// </summary>
        /// <returns></returns>
        public static string BiosSerialNumber => _biosSerialNumber ?? (_biosSerialNumber = GetHardWareInfo("Win32_BIOS", "SerialNumber"));
        /// <summary>
        /// 获取主板序列号
        /// </summary>
        /// <returns></returns>
        public static string BaseBoardSerialNumber => _baseBoardSerialNumber ?? (_baseBoardSerialNumber = GetHardWareInfo("Win32_BaseBoard", "SerialNumber"));
        /// <summary>
        /// 获取硬盘ID 
        /// </summary>
        /// <returns></returns>
        public static string DiskId => _diskId ?? (_diskId = GetHardWareInfo("Win32_DiskDrive", "Model"));
        /// <summary>
        /// 获取磁盘驱动器序列号
        /// </summary>
        /// <returns></returns>
        public static string DiskSerialNumber => _diskSerialNumber ?? (_diskSerialNumber = GetHardWareInfo("Win32_PhysicalMedia", "SerialNumber"));
        /// <summary>
        /// 获取总物理内存
        /// </summary>
        /// <returns></returns>
        public static string PhysicalMemory => GetHardWareInfo("Win32_ComputerSystem", "TotalPhysicalMemory");
        /// <summary>
        /// 取得设备硬盘的卷标号
        /// </summary>
        /// <returns></returns>
        public static string GetDiskVolumeSerialNumber()
        {
            var disk = new ManagementObject($"win32_logicaldisk.deviceid=\"{OsDriver}\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }
        /// <summary>
        /// 获取IP地址   
        /// </summary>
        /// <returns></returns>
        public static string GetIpAddress()
        {
            try
            {
                var managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
                var managementObjColl = managementClass.GetInstances();
                foreach (ManagementObject mo in managementObjColl)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        Array ar = (Array)(mo.Properties["IpAddress"].Value);
                        return ar.GetValue(0).ToString();
                    }
                }
            }
            catch { }
            return null;
        }
        /// <summary>
        ///  获取网卡mac地址
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddress()
        {
            var managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var managementObjColl = managementClass.GetInstances();
            foreach (ManagementObject mo in managementObjColl)
            {
                if ((bool)mo["IPEnabled"])
                    return mo["MacAddress"].ToString();
            }
            return null;
        }


        /// <summary>
        /// 获取计算机硬件信息
        /// </summary>
        /// <param name="typePath"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetHardWareInfo(string typePath, string key)
        {
            try
            {
                var managementClass = new ManagementClass(typePath);
                var managementObjColl = managementClass.GetInstances();
                PropertyDataCollection properties = managementClass.Properties;
                foreach (PropertyData property in properties)
                {
                    if (property.Name == key)
                    {
                        foreach (var managementBaseObject in managementObjColl)
                        {
                            var managementObject = (ManagementObject)managementBaseObject;
                            return managementObject.Properties[property.Name].Value.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }
    }
}
