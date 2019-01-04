using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using WLib.UserCtrls.Viewer;

namespace WLib.UserCtrls.Dev.ArcGisControl
{
    public partial class PagelayoutViewer : UserControl,IViewer
    {
        public PagelayoutViewer()
        {
            InitializeComponent();
        }
        private void PagelayoutViewer_Load(object sender, EventArgs e)
        {
            //地图导航默认平移
            ESRI.ArcGIS.SystemUI.ICommand command = null;
            command = new ESRI.ArcGIS.Controls.ControlsPagePanTool();
            if (command != null)
            {
                command.OnCreate(axPageLayoutControl1.Object);
                if (command is ITool)
                    axPageLayoutControl1.CurrentTool = command as ESRI.ArcGIS.SystemUI.ITool;
                else
                    command.OnClick();
            }
        }
        ~PagelayoutViewer()
        {
            axPageLayoutControl1.Dispose();
        }

        public void LoadFile(string file)
        {
            axPageLayoutControl1.LoadMxFile(file);
        }

        public void Close()
        {
            axPageLayoutControl1.Dispose();
        }

        private void axPageLayoutControl1_OnMouseDown(object sender, IPageLayoutControlEvents_OnMouseDownEvent e)
        {
        }

        private void axPageLayoutControl1_OnMouseMove(object sender, IPageLayoutControlEvents_OnMouseMoveEvent e)
        {

        }

        private void axPageLayoutControl1_OnDoubleClick(object sender, IPageLayoutControlEvents_OnDoubleClickEvent e)
        {

        }

        private void barBtnSelectElement_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CommonLib.MapMouseFlag = 1;
            DevExpress.XtraBars.BarButtonItem barBtn = e.Item as DevExpress.XtraBars.BarButtonItem;
            ESRI.ArcGIS.SystemUI.ICommand command = null;
            switch (barBtn.Name)
            {
                case "barBtnWholePageExtent":  //全屏
                    command = new ESRI.ArcGIS.Controls.ControlsPageZoomWholePageCommand();
                    break;
                case "barBtnPagePan":         //平移
                    command = new ESRI.ArcGIS.Controls.ControlsPagePanTool();
                    break;
                case "barBtnZoomIn":      //放大
                    command = new ESRI.ArcGIS.Controls.ControlsPageZoomInTool();
                    break;
                case "barBtnZoomOut":     //缩小
                    command = new ESRI.ArcGIS.Controls.ControlsPageZoomOutTool();
                    break;
                case "barBtnPreView":     //上一视图
                    command = new ESRI.ArcGIS.Controls.ControlsPageZoomPageToLastExtentBackCommand();
                    break;
                case "barBtnNextView":    //下一视图
                    command = new ESRI.ArcGIS.Controls.ControlsPageZoomPageToLastExtentForwardCommand();
                    break;
                default:
                    break;
            }
            if (command != null)
            {
                command.OnCreate(axPageLayoutControl1.Object);
                if (command is ITool)
                    axPageLayoutControl1.CurrentTool = command as ESRI.ArcGIS.SystemUI.ITool;
                else
                    command.OnClick();
            }
        }

    }
}
