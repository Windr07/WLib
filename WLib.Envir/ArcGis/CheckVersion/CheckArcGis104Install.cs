/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2/12
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Envir.ArcGis.CheckVersion
{
    /// <summary>
    /// 检查、获取ArcGIS10.4及其子程序安装路径
    /// </summary>
    public class CheckArcGis104Install : CheckArcGisInstall
    {
        /// <summary>
        /// 检查、获取ArcGIS10.4及其子程序安装路径
        /// </summary>
        public CheckArcGis104Install() : base("10.4", EArcGisVersion.ArcGIS104)
        {
        }
    }
}
