/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Envir.ArcGIS
{
    /// <summary>
    /// 检查、获取ArcGIS及其子程序安装路径
    /// </summary>
    public interface ICheckArcGisInstall
    {
        /// <summary>
        /// ArcGIS 版本
        /// </summary>
        EArcGisVersion Version { get; }


        /// <summary>
        /// 判断操作系统是否安装了ArcGIS Desktop
        /// </summary>
        /// <param name="installPath"></param>
        /// <returns></returns>
        bool IsIntallDesktop(out string installPath);
        /// <summary>
        /// 判断操作系统是否安装了ArcGIS Runtime
        /// </summary>
        /// <param name="installPath"></param>
        /// <returns></returns>
        bool IsIntallRumtime(out string installPath);
        /// <summary>
        /// 判断操作系统是否安装了ArcMap
        /// </summary>
        /// <param name="installPath"></param>
        /// <returns></returns>
        bool IsInstallArcMap(out string installPath);
        /// <summary>
        /// 判断操作系统是否安装了ArcGIS License
        /// </summary>
        /// <param name="installPath"></param>
        /// <returns></returns>
        bool IsInstallLicense(out string installPath);
        /// <summary>
        /// 判断操作系统是否安装了ArcGIS Administrator
        /// </summary>
        /// <param name="installPath"></param>
        /// <returns></returns>
        bool IsInstallAdmin(out string installPath);


        /// <summary>
        /// 获取ArcGIS Desktop的安装路径
        /// </summary>
        /// <returns></returns>
        string GetDesktopPath();
        /// <summary>
        /// 获取ArcMap的安装路径
        /// </summary>
        /// <returns></returns>
        string GetArcMapPath();
        /// <summary>
        /// 获取License的安装路径
        /// </summary>
        /// <returns></returns>
        string GetLicensePath();
        /// <summary>
        /// 获取ArcGIS Administrator的安装路径
        /// </summary>
        /// <returns></returns>
        string GetAdminPath();
    }
}
