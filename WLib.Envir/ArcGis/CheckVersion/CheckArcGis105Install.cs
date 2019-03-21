/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2/12
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Envir.ArcGis.CheckVersion
{
    /// <summary>
    /// 检查、获取ArcGIS10.5及其子程序安装路径
    /// </summary>
    public class CheckArcGis105Install : CheckArcGisInstall
    {
        /// <summary>
        /// 检查、获取ArcGIS10.5及其子程序安装路径
        /// </summary>
        public CheckArcGis105Install() : base("10.5", EArcGisVersion.ArcGIS105)
        {
        }
    }
}
