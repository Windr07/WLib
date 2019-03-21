/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2/12
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Envir.ArcGis.CheckVersion
{
    /// <summary>
    /// 检查、获取ArcGIS10.2及其子程序安装路径
    /// </summary>
    public class CheckArcGis102Install : CheckArcGisInstall
    {
        /// <summary>
        /// 检查、获取ArcGIS10.2及其子程序安装路径
        /// </summary>
        public CheckArcGis102Install() : base("10.2", EArcGisVersion.ArcGIS102)
        {
        }
    }
}
