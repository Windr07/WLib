/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/16 14:59:55
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace WLib.Envir.ArcGis.CheckVersion
{
    /// <summary>
    /// 检查、获取ArcGIS10.6及其子程序安装路径
    /// </summary>
    public class CheckArcGis106Install : CheckArcGisInstall
    {
        /// <summary>
        /// 检查、获取ArcGIS10.6及其子程序安装路径
        /// </summary>
        public CheckArcGis106Install() : base("10.6", EArcGisVersion.ArcGIS106)
        {
        }
    }
}
