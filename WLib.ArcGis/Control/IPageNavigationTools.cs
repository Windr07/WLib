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
    /// 页面布局导航条
    /// </summary>
    public interface IPageNavigationTools
    {
        /// <summary>
        /// 页面布局导航工具类别
        /// </summary>
        EPageTools CurrentTool { get; }
        /// <summary>
        /// 页面导航工具条所绑定的页面布局控件
        /// </summary>
        AxPageLayoutControl PageLayoutControl { get; set; }
    }
}
