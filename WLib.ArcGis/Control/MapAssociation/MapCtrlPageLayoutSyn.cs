using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace WLib.ArcGis.Control.MapAssociation
{
    /// <summary>
    /// 地图控件和页面布局控件的同步操作
    /// </summary>
    public class MapCtrlPageLayoutSyn
    {
        /// <summary>
        /// 地图控件
        /// </summary>
        public AxMapControl MapControl { get; }
        /// <summary>
        /// 页面布局控件
        /// </summary>
        public AxPageLayoutControl PageLayoutControl { get; }
        /// <summary>
        /// 地图控件和页面布局控件的同步操作
        /// </summary>
        /// <param name="mapControl">地图控件</param>
        /// <param name="pageLayoutControl">页面布局控件</param>
        public MapCtrlPageLayoutSyn(AxMapControl mapControl, AxPageLayoutControl pageLayoutControl)
        {
            MapControl = mapControl;
            PageLayoutControl = pageLayoutControl;
            MapControl.OnAfterScreenDraw += mapControl_OnAfterScreenDraw;
        }


        /// <summary>
        /// 主地图控件中的地图复制到布局控件中
        /// </summary>
        private void CopyMapToPageLayout()
        {
            object copyFromMap = MapControl.Map;
            object copyToMap = PageLayoutControl.ActiveView.FocusMap;
            IObjectCopy pObjectCopy = new ObjectCopyClass();
            pObjectCopy.Overwrite(pObjectCopy.Copy(copyFromMap), copyToMap);
        }

        //主地图屏幕更新完毕，布局控件与之关联
        private void mapControl_OnAfterScreenDraw(object sender, IMapControlEvents2_OnAfterScreenDrawEvent e)
        {
            IActiveView activeView = (IActiveView)PageLayoutControl.ActiveView.FocusMap;
            IDisplayTransformation displayTransformation = activeView.ScreenDisplay.DisplayTransformation;
            displayTransformation.VisibleBounds = MapControl.Extent;
            CopyMapToPageLayout();
            PageLayoutControl.ActiveView.Refresh();
        }
    }
}
