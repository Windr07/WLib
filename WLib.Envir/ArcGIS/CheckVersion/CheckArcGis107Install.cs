/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/16 15:00:51
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Envir.ArcGis.CheckVersion
{
    /// <summary>
    /// 检查、获取ArcGIS10.7及其子程序安装路径
    /// </summary>
    public class CheckArcGis107Install : CheckArcGisInstall
    {
        /// <summary>
        /// 检查、获取ArcGIS10.7及其子程序安装路径
        /// </summary>
        public CheckArcGis107Install() : base("10.7", EArcGisVersion.ArcGIS107)
        {
        }
    }
}
