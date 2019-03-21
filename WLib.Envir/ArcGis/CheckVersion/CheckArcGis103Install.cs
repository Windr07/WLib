/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2/12
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Envir.ArcGis.CheckVersion
{
    /// <summary>
    /// 检查、获取ArcGIS10.3及其子程序安装路径
    /// </summary>
    public class CheckArcGis103Install : CheckArcGisInstall
    {
        /// <summary>
        /// 检查、获取ArcGIS10.3及其子程序安装路径
        /// </summary>
        public CheckArcGis103Install() : base("10.3", EArcGisVersion.ArcGIS103)
        {
        }
    }
}
