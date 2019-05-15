/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2/23
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Windows.Forms;
using WLib.ArcGis.Control.MapAssociation;

namespace WLib.UserCtrls.ArcGisCtrl
{
    /// <summary>
    /// 地图及其图层/表格树、鹰眼图、导航工具、页面布局视图的组合控件
    /// </summary>
    public partial class MapViewer : UserControl
    {
        /// <summary>
        /// MapViewer控件功能的分类管理
        /// </summary>
        public readonly MapViewerManager Manger;
        /// <summary>
        /// 初始化地图及其图层/表格树、鹰眼图、导航工具、页面布局视图的组合控件
        /// </summary>
        public MapViewer()
        {
            InitializeComponent();

            TableListBox.Dock = DockStyle.Fill;
            MainMapControl.Controls.Add(MapNavigationTools);
            MapNavigationTools.Parent = MainMapControl;
            MapNavigationTools.MapControl = MainMapControl;
            Manger = new MapViewerManager(MainMapControl, EagleMapControl, TocControl, PageLayoutControl, SwitchView);

            ((IActiveViewEvents_Event)MainMapControl.Map).ItemAdded += item =>
                { if (item is ITable table) TableListBox.AddTable(table); };
        }


        /// <summary>
        /// 显示指定的界面视图
        /// </summary>
        /// <param name="eTypes"></param>
        public void SwitchView(params EViewActionType[] eTypes)
        {
            foreach (var eType in eTypes)
            {
                switch (eType)
                {
                    case EViewActionType.MainMap:
                        ViewerTabControl.SelectedTab = xtpMapView;
                        break;
                    case EViewActionType.PageLayout:
                        ViewerTabControl.SelectedTab = xtpPageLayout;
                        PageLayoutControl.Visible = true;
                        break;
                    case EViewActionType.LayerToc:
                        TocControl.Visible = btnCollapsed.Enabled = btnExpand.Enabled = true;
                        TableListBox.Visible = false;
                        lblTocTips.Text = @"图层控制";
                        break;
                    case EViewActionType.TableList:
                        TocControl.Visible = btnCollapsed.Enabled = btnExpand.Enabled = false;
                        TableListBox.Visible = true;
                        lblTocTips.Text = @"表格列表";
                        break;
                    case EViewActionType.EagleMap:
                        splitContainer2.Panel1Collapsed = false;
                        break;
                }
            }
        }

        /// <summary>
        /// 图层控制栏中的按钮的事件（添加数据、切换图层/表格、折叠所有图层、展开所有图层）
        /// </summary>
        private void layerControlButtons_Click(object sender, EventArgs e)
        {
            var btnName = ((Button)sender).Name;
            if (btnName == btnSwitchContent.Name)
            {
                SwitchView(TocControl.Visible ? EViewActionType.TableList : EViewActionType.LayerToc);
                tocGroupControl.Update();
            }
            else if (btnName == btnCollapsed.Name) Manger.TocHelper.ExpandLegend(false);
            else if (btnName == btnExpand.Name) Manger.TocHelper.ExpandLegend(true);
            else if (btnName == btnAddData.Name) Manger.DocHelper.AddData();
        }
    }
}
