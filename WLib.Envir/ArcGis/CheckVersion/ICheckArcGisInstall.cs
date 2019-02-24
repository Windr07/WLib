/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Envir.ArcGis.CheckVersion
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
        /// 判断操作系统是否安装了ArcGIS指定产品
        /// </summary>
        /// <param name="productType">ArcGIS产品类别</param>
        /// <param name="installPath">安装路径</param>
        /// <returns></returns>
        bool IsIntall(EArcGisProductType productType, out string installPath);
        /// <summary>
        /// 获取ArcGIS指定产品的安装路径
        /// </summary>
        /// <returns></returns>
        string GetInstallPath(EArcGisProductType productType);
    }
}
