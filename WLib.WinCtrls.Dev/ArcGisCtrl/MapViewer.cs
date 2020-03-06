/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2/23
// desc： 地图显示和操作的组合控件（因外部使用可能出现许可错误，因此还组合了License控件，外部不应该再使用License控件）
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Windows.Forms;
using WLib.ArcGis.Control.MapAssociation;
using WLib.WinCtrls.ArcGisCtrl;
using WLib.WinCtrls.ListCtrl;

namespace WLib.WinCtrls.Dev.ArcGisCtrl
{
    /// <summary>
    /// 地图及其图层/表格树、鹰眼图、导航工具、页面布局和许可控件的组合控件
    /// </summary>
    public partial class MapViewer : UserControl
    {
        /// <summary>
        /// MapViewer控件功能的分类管理
        /// </summary>
        public readonly MapViewerManager Manger;
        /// <summary>
        /// 地图及其图层/表格树、鹰眼图、导航工具、页面布局和许可控件的组合控件
        /// </summary>
        public MapViewer()
        {
            InitializeComponent();
            this.TableListBox.ListCtrl = new ImageListBox();
            this.TableListBox.Dock = DockStyle.Fill;
            this.MapNavigationTools.MapControl = MainMapControl;
            this.PageNavigationTools.PageLayoutControl = PageLayoutControl;

            this.Manger = new MapViewerManager(MainMapControl, EagleMapControl, TocControl, PageLayoutControl, new AttributeForm(), SwitchView);

            ((IActiveViewEvents_Event)MainMapControl.Map).ItemAdded += item =>
            { if (item is ITable table) this.TableListBox.AddTable(table); };
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
                        ViewerTabControl.SelectedTabPage = xtpMapView;
                        break;
                    case EViewActionType.PageLayout:
                        ViewerTabControl.SelectedTabPage = xtpPageLayout;
                        PageLayoutControl.Visible = true;
                        break;
                    case EViewActionType.LayerToc:
                        TocControl.Visible = btnCollapsed.Enabled = btnExpand.Enabled = true;
                        TableListBox.Visible = false;
                        TocControl.Dock = DockStyle.Fill;
                        tocGroupControl.Text = @"图层控制";
                        break;
                    case EViewActionType.TableList:
                        TocControl.Visible = btnCollapsed.Enabled = btnExpand.Enabled = false;
                        TableListBox.Visible = true;
                        TableListBox.Dock = DockStyle.Fill;
                        tocGroupControl.Text = @"表格列表";
                        break;
                    case EViewActionType.EagleMap:
                        splitContainerControl2.Collapsed = false;
                        break;
                }
            }
        }
        /// <summary>
        /// 图层控制栏中的按钮的事件（添加数据、切换图层/表格、折叠所有图层、展开所有图层）
        /// </summary>
        private void layerControlButtons_Click(object sender, EventArgs e)
        {
            var ctrlName = ((Control)sender).Name;
            if (ctrlName == this.btnSwitchContent.Name)
            {
                SwitchView(new[] { TocControl.Visible ? EViewActionType.TableList : EViewActionType.LayerToc });
                tocGroupControl.Update();
            }
            else if (ctrlName == this.btnCollapsed.Name) Manger.TocHelper.ExpandLegend(false);
            else if (ctrlName == this.btnExpand.Name) Manger.TocHelper.ExpandLegend(true);
            else if (ctrlName == this.btnAddData.Name) Manger.DocHelper.AddData();
        }
    }
}
