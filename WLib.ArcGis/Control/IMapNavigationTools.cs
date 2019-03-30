/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/3/27 17:25:23
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Controls;
using WLib.ArcGis.Control.MapAssociation;

namespace WLib.ArcGis.Control
{
    /// <summary>
    /// 地图导航条
    /// </summary>
    public interface IMapNavigationTools
    {
        /// <summary>
        /// 当前使用的地图导航工具
        /// </summary>
        EMapTools CurrentTool { get; }
        /// <summary>
        /// 地图导航工具条所绑定的地图控件
        /// </summary>
        AxMapControl MapControl { get; set; }
    }
}
