/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2/12
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Envir.ArcGis.CheckVersion
{
    /// <summary>
    /// 检查、获取ArcGIS10.1及其子程序安装路径
    /// </summary>
    public class CheckArcGis101Install : CheckArcGisInstall
    {
        /// <summary>
        /// 检查、获取ArcGIS10.1及其子程序安装路径
        /// </summary>
        public CheckArcGis101Install() : base("10.1", EArcGisVersion.ArcGIS101)
        {
        }
    }
}
