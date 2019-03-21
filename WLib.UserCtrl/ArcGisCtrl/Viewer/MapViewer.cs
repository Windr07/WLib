/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2/23
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Windows.Forms;
using WLib.ArcGis.Control;
using WLib.ArcGis.Control.MapAssociation;

namespace WLib.UserCtrls.ArcGisCtrl.Viewer
{
    /// <summary>
    /// 地图及其图层/表格控制、鹰眼图、导航工具的组合控件
    /// </summary>
    public partial class MapViewer : UserControl
    {
        /// <summary>
        /// 主地图控件的右键菜单操作
        /// </summary>
        public readonly MapCtrlMenu MenuHelper;
        /// <summary>
        ///主地图控件与TOC控件的关联操作
        /// </summary>
        public readonly MapCtrlTocEx TocHelper;
        /// <summary>
        /// 主地图控件与地图文档的关联操作
        /// </summary>
        public readonly MapCtrlDocument DocHelper;
        /// <summary>
        /// 主地图控件与鹰眼地图的关联操作
        /// </summary>
        public readonly MapCtrlEagleMap EagleMapHelper;
        /// <summary>
        /// 主地图控件与页面布局控件的关联操作
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

            this.mapNavigationTools1.Parent = this.MainMapControl;
            this.mapNavigationTools1.MapControl = this.MainMapControl;
            this.tableListBox1.Dock = DockStyle.Fill;
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
                    this.tableListBox1.AddTable(table);
                    GoToMapView();
                    GoTotableListBox();
                }
            };
        }


        /// <summary>
        /// 将当前标签页设为地图页面
        /// </summary>
        public void GoToMapView()
        {
            ViewerTabControl.SelectedTab = xtpMapView;
        }
        /// <summary>
        /// 将当前标签页设为页面布局页面
        /// </summary>
        public void GoToPageView()
        {
            ViewerTabControl.SelectedTab = xtpPageLayout;
            PageLayoutControl.Visible = true;
        }
        /// <summary>
        /// 显示图层目录列表（TOCControl）
        /// </summary>
        public void GoToLayerToc()
        {
            TocControl.Visible = btnCollapsed.Enabled = btnExpand.Enabled = true;
            tableListBox1.Visible = false;
            TocControl.Dock = DockStyle.Fill;
            tocGroupControl.Text = @"图层控制";
        }
        /// <summary>
        /// 显示图层目录列表（ImageListBox）
        /// </summary>
        public void GoTotableListBox()
        {
            TocControl.Visible = btnCollapsed.Enabled = btnExpand.Enabled = false;
            tableListBox1.Visible = true;
            tableListBox1.Dock = DockStyle.Fill;
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

      
        private void layerControlButtons_Click(object sender, EventArgs e)//图层控制栏中的按钮的事件（添加数据、切换图层/表格、折叠所有图层、展开所有图层）
        {
            var btnName = ((Button)sender).Name;
            if (btnName == this.btnSwitchContent.Name)
            {
                if (TocControl.Visible)
                    GoTotableListBox();
                else
                    GoToLayerToc();
                tocGroupControl.Update();
            }
            else if (btnName == this.btnCollapsed.Name) TocHelper.ExpandLegend(false);
            else if (btnName == this.btnExpand.Name) TocHelper.ExpandLegend(true);
            else if (btnName == this.btnAddData.Name) DocHelper.AddData();
        }

        private void axMapControlMainMap_OnFullExtentUpdated(object sender, IMapControlEvents2_OnFullExtentUpdatedEvent e)//主地图：刷新地图
        {
            MainMapControl.Refresh();
        }

        //private void MainMapControl_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)//
        //{

        //}
    }
}
