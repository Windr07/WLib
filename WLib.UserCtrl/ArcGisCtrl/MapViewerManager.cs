/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/3/31 12:54:16
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WLib.ArcGis.Control;
using WLib.ArcGis.Control.MapAssociation;

namespace WLib.UserCtrls.ArcGisCtrl
{
    /// <summary>
    /// 地图及其图层/表格树、鹰眼图、导航工具、页面布局视图的组合控件的管理类
    /// </summary>
    public class MapViewerManager
    {
        /// <summary>
        /// 将视图切换到主地图控件所在视图的操作
        /// </summary>
        protected Action<EViewActionType[]> SwitchView;
        /// <summary>
        /// 主地图控件
        /// </summary>
        protected AxMapControl MainMapControl;

        /// <summary>
        /// 主地图控件的右键菜单操作
        /// </summary>
        public MapCtrlMenu MenuHelper { get; }
        /// <summary>
        ///主地图控件与TOC控件的关联操作
        /// </summary>
        public MapCtrlToc TocHelper { get; }
        /// <summary>
        /// 主地图控件与地图文档的关联操作
        /// </summary>
        public MapCtrlDocument DocHelper { get; }
        /// <summary>
        /// 主地图控件与鹰眼地图的关联操作
        /// </summary>
        public MapCtrlEagleMap EagleMapHelper { get; }
        /// <summary>
        /// 主地图控件绘制点、线、面元素操作
        /// </summary>
        public MapCtrlDrawElement DrawElementHelper { get; }
        /// <summary>
        /// 主地图控件与页面布局控件的关联操作
        /// </summary>
        public MapCtrlPageLayoutSyn PageLayoutHelper { get; }
        /// <summary>
        /// ActiveView状态改变事件，此处用于设置图层或表格添加事件
        /// </summary>
        public IActiveViewEvents_Event MapActiveViewEvents;


        /// <summary>
        /// 地图及其图层/表格树、鹰眼图、导航工具、页面布局视图的组合控件的管理类
        /// </summary>
        /// <param name="mainMapControl">主地图控件</param>
        /// <param name="eagleMapControl">鹰眼图地图控件</param>
        /// <param name="tocControl">图层树控件</param>
        /// <param name="pageLayoutControl">页面布局控件</param>
        /// <param name="switchView">显示指定的界面视图的操作</param>
        public MapViewerManager(AxMapControl mainMapControl, AxMapControl eagleMapControl,
            AxTOCControl tocControl, AxPageLayoutControl pageLayoutControl, Action<EViewActionType[]> switchView)
        {
            SwitchView = switchView;
            MainMapControl = mainMapControl;
            DocHelper = new MapCtrlDocument(mainMapControl);
            TocHelper = new MapCtrlToc(tocControl, mainMapControl, new AttributeForm(), switchView);
            MenuHelper = new MapCtrlMenu(mainMapControl);
            EagleMapHelper = new MapCtrlEagleMap(mainMapControl, eagleMapControl);
            DrawElementHelper = new MapCtrlDrawElement(mainMapControl);
            PageLayoutHelper = new MapCtrlPageLayoutSyn(mainMapControl, pageLayoutControl);

            mainMapControl.OnFullExtentUpdated += delegate { mainMapControl.Refresh(); };//主地图：刷新地图
            ((IActiveViewEvents_Event)mainMapControl.Map).ItemAdded += item => //向map/PageLayout中添加数据（图层、表格等）都会触发ItemAdded事件
            {
                if (item is ILayer)
                    SwitchView(new[] { EViewActionType.MainMap, EViewActionType.LayerToc });
                else if (item is ITable)
                    SwitchView(new[] { EViewActionType.MainMap, EViewActionType.TableList });
            };
        }


        /// <summary>
        /// 全图
        /// </summary>
        public void ZoomToWhole()
        {
            MainMapControl.Extent = MainMapControl.FullExtent;
        }
        /// <summary>
        /// 刷新地图
        /// </summary>
        public void RefreshMap()
        {
            MainMapControl.Refresh();
            MainMapControl.Update();
        }
        /// <summary>
        /// 定位查询获得的第一个图斑
        /// </summary>
        /// <param name="featureLayer">查询图层</param>
        /// <param name="whereClause">查询条件</param>
        public void LocationFirstFeature(IFeatureLayer featureLayer, string whereClause)
        {
            SwitchView(new[] { EViewActionType.MainMap });
            MainMapControl.MapZoomToAndSelectFirst(featureLayer, whereClause);
        }
    }
}
