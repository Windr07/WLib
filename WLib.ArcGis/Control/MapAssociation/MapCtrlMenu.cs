/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/3
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;

namespace WLib.ArcGis.Control.MapAssociation
{
    /// <summary>
    /// 地图控件与常用右键菜单操作
    /// </summary>
    public class MapCtrlMenu: IMapCtrlAssociation
    {
        /// <summary>
        /// 地图右键菜单接口
        /// </summary>
        public IToolbarMenu ToolBarMenu { get; }
        /// <summary>
        /// 地图控件
        /// </summary>
        public AxMapControl MapControl { get; }
        /// <summary>
        /// 地图控件的常用右键菜单
        /// </summary>
        /// <param name="mapCtrl"></param>
        public MapCtrlMenu(AxMapControl mapCtrl)
        {
            MapControl = mapCtrl;
            ToolBarMenu = new ToolbarMenuClass(); //工具栏菜单类
            ToolBarMenu.AddItem(new ControlsMapViewMenuClass(), 0, 0, false, esriCommandStyles.esriCommandStyleMenuBar);
            ToolBarMenu.AddItem(new ControlsMapPanToolClass(), 0, 0, false, esriCommandStyles.esriCommandStyleMenuBar);
            ToolBarMenu.AddItem(new ControlsMapZoomOutToolClass(), 0, 0, false, esriCommandStyles.esriCommandStyleMenuBar);
            ToolBarMenu.AddItem(new ControlsMapZoomInToolClass(), 0, 0, false, esriCommandStyles.esriCommandStyleMenuBar);
            ToolBarMenu.SetHook(MapControl.Object);
            MapControl.OnMouseDown += (sender, e) =>
            {
                if (e.button == 2)
                    ToolBarMenu.PopupMenu(e.x, e.y, MapControl.hWnd);
            };
        }
    }
}
