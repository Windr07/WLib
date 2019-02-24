/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2/23
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Windows.Forms;
using WLib.ArcGis.Control;
using WLib.ArcGis.Control.MapAssociation;

namespace WLib.UserCtrls.Dev.ArcGisCtrl.Viewer
{
    /// <summary>
    /// 地图及其图层/表格控制、鹰眼图、导航工具的组合控件
    /// </summary>
    public partial class MapViewer : UserControl
    {
        /// <summary>
        /// 地图文档与主地图控件的关联操作
        /// </summary>
        public readonly MapCtrlDocument DocHelper;
        /// <summary>
        /// TOC控件与主地图控件的关联操作
        /// </summary>
        public readonly MapCtrlTocEx TocHelper;
        /// <summary>
        /// 主地图控件的右键菜单操作
        /// </summary>
        public readonly MapCtrlMenu MenuHelper;
        /// <summary>
        /// 鹰眼图与主地图控件的关联操作
        /// </summary>
        public readonly MapCtrlEagleMap EagleMapHelper;
        /// <summary>
        /// 页面布局控件与主地图控件的关联操作
        /// </summary>
        public readonly MapCtrlPageLayoutSyn PageLayoutHelper;
        /// <summary>
        /// ActiveView状态改变事件，此处用于设置图层或表格添加事件
        /// </summary>
        public IActiveViewEvents_Event MapActiveViewEvents;
        /// <summary>
        /// 初始化地图及其图层/表格控制、鹰眼图、导航工具的组合控件
        /// </summary>
        public MapViewer()
        {
            InitializeComponent();

            this.mapNavigationTools2.Parent = this.MainMapControl;
            DocHelper = new MapCtrlDocument(MainMapControl);
            TocHelper = new MapCtrlTocEx(TocControl, MainMapControl, GoToMapView);
            MenuHelper = new MapCtrlMenu(MainMapControl);
            EagleMapHelper = new MapCtrlEagleMap(MainMapControl, EagleMapControl);
            PageLayoutHelper = new MapCtrlPageLayoutSyn(MainMapControl, PageLayoutControl);
            MapActiveViewEvents = (IActiveViewEvents_Event)MainMapControl.Map;
            MapActiveViewEvents.ItemAdded += (item) => //向map/PageLayout中添加数据（图层、表格等）都会触发ItemAdded事件
            {
                if (item is ILayer)
                {
                    GoToMapView();
                    GoToLayerToc();
                }
                else if (item is ITable table)
                {
                    this.tableListBox.AddTable(table);
                    GoToMapView();
                    GoToTableListBox();
                }
            };
        }


        /// <summary>
        /// 将当前标签页设为地图页面
        /// </summary>
        public void GoToMapView()
        {
            ViewerTabControl.SelectedTabPage = xtpMapView;
        }
        /// <summary>
        /// 将当前标签页设为页面布局页面
        /// </summary>
        public void GoToPageView()
        {
            ViewerTabControl.SelectedTabPage = xtpPageLayout;
            PageLayoutControl.Visible = true;
        }
        /// <summary>
        /// 显示图层目录列表（TOCControl）
        /// </summary>
        public void GoToLayerToc()
        {
            TocControl.Visible = btnCollapsed.Enabled = btnExpand.Enabled = true;
            tableListBox.Visible = false;
            TocControl.Dock = DockStyle.Fill;
            tocGroupControl.Text = @"图层控制";
        }
        /// <summary>
        /// 显示图层目录列表（ImageListBox）
        /// </summary>
        public void GoToTableListBox()
        {
            TocControl.Visible = btnCollapsed.Enabled = btnExpand.Enabled = false;
            tableListBox.Visible = true;
            tableListBox.Dock = DockStyle.Fill;
            tocGroupControl.Text = @"表格列表";
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
            GoToMapView();
            MainMapControl.MapZoomToAndSelectFirst(featureLayer, whereClause);
        }


        private void axMapControlMainMap_OnFullExtentUpdated(object sender, IMapControlEvents2_OnFullExtentUpdatedEvent e)//主地图：刷新地图
        {
            MainMapControl.Refresh();
        }

        private void layerControlButtons_Click(object sender, EventArgs e)//图层控制栏中的按钮的事件（添加数据、切换图层/表格、折叠所有图层、展开所有图层）
        {
            var btnName = ((SimpleButton)sender).Name;
            if (btnName == this.btnSwitchContent.Name)
            {
                if (TocControl.Visible)
                    GoToTableListBox();
                else
                    GoToLayerToc();
                tocGroupControl.Update();
            }
            else if (btnName == this.btnCollapsed.Name) TocHelper.ExpandLegend(false);
            else if (btnName == this.btnExpand.Name) TocHelper.ExpandLegend(true);
            else if (btnName == this.btnAddData.Name) DocHelper.AddData();
        }
    }
}
