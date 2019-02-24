/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2/12
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Envir.ArcGis.CheckVersion
{
    /// <summary>
    /// 检查、获取ArcGIS9.3及其子程序安装路径
    /// </summary>
    public class CheckArcGis93Install : CheckArcGisInstall
    {
        /// <summary>
        /// 检查、获取ArcGIS9.3及其子程序安装路径
        /// </summary>
        public CheckArcGis93Install() : base("9.3", EArcGisVersion.ArcGIS93)
        {
        }
    }
}
