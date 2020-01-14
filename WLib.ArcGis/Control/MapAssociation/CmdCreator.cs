using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;

namespace WLib.ArcGis.Control.MapAssociation
{
    /// <summary>
    /// <see cref="ICommand"/>对象创建器
    /// </summary>
    public static class CmdCreator
    {
        /// <summary>
        /// 根据<see cref="EMapTools"/>枚举创建ArcGIS的命令
        /// </summary>
        /// <param name="eTool"></param>
        /// <returns></returns>
        public static ICommand CreateCommand(EMapTools eTool)
        {
            ICommand command = null;
            switch (eTool)
            {
                case EMapTools.FullExtent: command = new ControlsMapFullExtentCommand(); break;
                case EMapTools.ZoomIn: command = new ControlsMapZoomInTool(); break;
                case EMapTools.ZoomOut: command = new ControlsMapZoomOutTool(); break;
                case EMapTools.Pan: command = new ControlsMapPanTool(); break;
                case EMapTools.PreView: command = new ControlsMapZoomToLastExtentBackCommand(); break;
                case EMapTools.Identify: command = new ControlsMapIdentifyTool(); break;
                case EMapTools.Selection: command = new ControlsSelectFeaturesToolClass(); break;
                case EMapTools.Swipe: command = new ControlsMapSwipeToolClass(); break;
            }
            return command;
        }
        /// <summary>
        /// 根据<see cref="EPageTools"/>枚举创建ArcGIS的命令
        /// </summary>
        /// <param name="eTool"></param>
        /// <returns></returns>
        public static ICommand CreateCommand(EPageTools eTool)
        {
            ICommand command = null;
            switch (eTool)
            {
                case EPageTools.FullExtent: command = new ControlsPageZoomWholePageCommand(); break;
                case EPageTools.ZoomIn: command = new ControlsPageZoomInTool(); break;
                case EPageTools.ZoomOut: command = new ControlsPageZoomOutTool(); break;
                case EPageTools.Pan: command = new ControlsPagePanTool(); break;
                case EPageTools.PreView: command = new ControlsPageZoomPageToLastExtentBackCommand(); break;
                case EPageTools.NextView: command = new ControlsPageZoomPageToLastExtentForwardCommand(); break;
            }
            return command;
        }
    }
}
