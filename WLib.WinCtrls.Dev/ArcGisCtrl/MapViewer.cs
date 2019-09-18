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
using WLib.WinCtrls.ArcGisCtrl;
using WLib.WinCtrls.Dev.ListCtrl;

namespace WLib.WinCtrls.Dev.ArcGisCtrl
{
    /// <summary>
    /// 地图及其图层/表格控制、鹰眼图、导航工具的组合控件
    /// </summary>
    public partial class MapViewer : UserControl
    {
        /// <summary>
        /// MapViewer控件功能的分类管理
        /// </summary>
        public readonly MapViewerManager Manger;
        /// <summary>
        /// 初始化地图及其图层/表格控制、鹰眼图、导航工具的组合控件
        /// </summary>
        public MapViewer()
        {
            InitializeComponent();

            this.TableListBox.ListCtrl = new ImageListBoxControlEx();
            this.TableListBox.Dock = DockStyle.Fill;
            this.MainMapControl.Controls.Add(this.MapNavigationTools);
            this.MapNavigationTools.Parent = this.MainMapControl;
            this.MapNavigationTools.MapControl = this.MainMapControl;
            this.Manger = new MapViewerManager(MainMapControl, EagleMapControl, TocControl, PageLayoutControl, SwitchView);

            ((IActiveViewEvents_Event)MainMapControl.Map).ItemAdded += item =>
            { if (item is ITable table) this.TableListBox.AddTable(table); };
        }


        /// <summary>
        /// 显示指定的界面视图
        /// </summary>
        /// <param name="eTypes"></param>
        public void SwitchView(EViewActionType[] eTypes)
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
            var btnName = ((Button)sender).Name;
            if (btnName == this.btnSwitchContent.Name)
            {
                SwitchView(new[] { TocControl.Visible ? EViewActionType.TableList : EViewActionType.LayerToc });
                tocGroupControl.Update();
            }
            else if (btnName == this.btnCollapsed.Name) Manger.TocHelper.ExpandLegend(false);
            else if (btnName == this.btnExpand.Name) Manger.TocHelper.ExpandLegend(true);
            else if (btnName == this.btnAddData.Name) Manger.DocHelper.AddData();
        }
    }
}
