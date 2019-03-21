/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Envir.ArcGis.CheckVersion
{
    /// <summary>
    /// 检查、获取ArcGIS10.0及其子程序安装路径
    /// </summary>
    public class CheckArcGis100Install : CheckArcGisInstall
    {
        /// <summary>
        /// 检查、获取ArcGIS10.0及其子程序安装路径
        /// </summary>
        public CheckArcGis100Install() : base("10.0", EArcGisVersion.ArcGIS100)
        {
        }
    }
}
