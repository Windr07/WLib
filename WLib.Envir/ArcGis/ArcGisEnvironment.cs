/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2/12
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using WLib.Envir.ArcGis.CheckVersion;

namespace WLib.Envir.ArcGis
{
    /// <summary>
    /// ArcGIS环境
    /// </summary>
    public static class ArcGisEnvironment
    {
        /// <summary>
        /// 检查、获取ArcGIS及其子程序安装路径
        /// </summary>
        private static ICheckArcGisInstall _checkArcGisInstall;
        /// <summary>
        /// 检查、获取ArcGIS及其子程序安装路径
        /// </summary>
        public static ICheckArcGisInstall CheckArcGisInstall => _checkArcGisInstall ?? (_checkArcGisInstall = GetCheckArcGisInstall(EArcGisProductType.Any));
        /// <summary>
        /// 当前安装的ArcGIS版本
        /// </summary>
        public static EArcGisVersion Version => CheckArcGisInstall.Version;


        /// <summary>
        /// 获取ArcGIS指定产品的安装路径
        /// </summary>
        /// <param name="eType">ArcGIS产品类别</param>
        /// <returns></returns>
        public static string GetInstallPath(EArcGisProductType eType = EArcGisProductType.Desktop)
        {
            return CheckArcGisInstall?.GetInstallPath(eType);
        }
        /// <summary>
        /// 获取ICheckArcGisInstall对象，可通过ICheckArcGisInstall.Version获得当前系统安装的ArcGIS版本
        /// </summary>
        /// <param name="eType">ArcGIS产品类别</param>
        /// <returns></returns>
        public static ICheckArcGisInstall GetCheckArcGisInstall(EArcGisProductType eType = EArcGisProductType.Desktop)
        {
            var checkers = new ICheckArcGisInstall[]
            {
                new CheckArcGis93Install(),
                new CheckArcGis100Install(),
                new CheckArcGis101Install(),
                new CheckArcGis102Install(),
                new CheckArcGis103Install(),
                new CheckArcGis104Install(),
                new CheckArcGis105Install(),
                new CheckArcGis106Install(),
                new CheckArcGis107Install(),
            };
            foreach (var checker in checkers)
            {
                if (checker.IsIntall(eType, out _))
                    return checker;
            }
            return null;
        }
    }
}
