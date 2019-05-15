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
    public class HardwareInfo
    {
        /// <summary>
        /// 获取系统所在盘符（eg:"c:\"）
        /// </summary>
        /// <returns></returns>
        public static string GetOsLetter()
        {
            return Environment.GetEnvironmentVariable("systemdrive");
        }
        /// <summary>
        /// 获取CPU的ID
        /// </summary>
        /// <returns></returns>
        public static string GetCpuId()
        {
            return GetHardWareInfo("Win32_Processor", "ProcessorId");
        }
        /// <summary>
        /// 获取BIOS序列号
        /// </summary>
        /// <returns></returns>
        public static string GetBiosSerialNumber()
        {
            return GetHardWareInfo("Win32_BIOS", "SerialNumber");
        }
        /// <summary>
        /// 获取主板序列号
        /// </summary>
        /// <returns></returns>
        public static string GetBaseBoardSerialNumber()
        {
            return GetHardWareInfo("Win32_BaseBoard", "SerialNumber");
        }
        /// <summary>
        /// 获取硬盘ID 
        /// </summary>
        /// <returns></returns>
        public static string GetDiskId()
        {
            return GetHardWareInfo("Win32_DiskDrive", "Model");
        }
        /// <summary>
        /// 获取磁盘驱动器序列号
        /// </summary>
        /// <returns></returns>
        public static string GetDiskSerialNumber()
        {
            return GetHardWareInfo("Win32_PhysicalMedia", "SerialNumber");
        }
        /// <summary>
        /// 取得设备硬盘的卷标号
        /// </summary>
        /// <returns></returns>
        public static string GetDiskVolumeSerialNumber()
        {
            string osLetter = GetOsLetter();//系统所在盘符，一般为"c:"
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject($"win32_logicaldisk.deviceid=\"{osLetter}\"");
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
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        System.Array ar = (System.Array)(mo.Properties["IpAddress"].Value);
                        return ar.GetValue(0).ToString();
                    }
                }
            }
            catch { }
            return "";
        }
        /// <summary>
        /// 获取总物理内存
        /// </summary>
        /// <returns></returns>
        public static string GetPhysicalMemory()
        {
            return GetHardWareInfo("Win32_ComputerSystem", "TotalPhysicalMemory");
        }
        /// <summary>
        ///  获取网卡mac地址
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddress()
        {
            string mac = "";
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (var o in moc)
            {
                var mo = (ManagementObject) o;
                if ((bool)mo["IPEnabled"])
                {
                    mac = mo["MacAddress"].ToString();
                    break;
                }
            }
            return mac;
        }


        /// <summary>
        /// 获取计算机硬件信息
        /// </summary>
        /// <param name="typePath"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetHardWareInfo(string typePath, string key)
        {
            try
            {
                ManagementClass managementClass = new ManagementClass(typePath);
                ManagementObjectCollection managementObjColl = managementClass.GetInstances();
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
