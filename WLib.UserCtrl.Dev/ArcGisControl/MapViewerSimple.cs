using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using WLib.ArcGis.Display;
using WLib.UserCtrls.Viewer;

namespace WLib.UserCtrls.Dev.ArcGisControl
{
    public partial class MapViewerSimple : UserControl, IViewer
    {
        public ESRI.ArcGIS.Controls.AxMapControl axMapControlMainMap;
        public ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;

        private bool m_preLeftShow = true;
        public DevExpress.XtraBars.BarStaticItem barSICurCoors = null;
        public DevExpress.XtraBars.BarStaticItem barSIScaleInfo = null;
        public MapViewerSimple()
        {
            InitializeComponent();
            barSICurCoors = new BarStaticItem();
            barSIScaleInfo = new BarStaticItem();


        }

        private void MapViewer_Load(object sender, EventArgs e)
        {
            //地图导航默认平移
            ESRI.ArcGIS.SystemUI.ICommand command = null;
            command = new ESRI.ArcGIS.Controls.ControlsMapPanTool();
            if (command != null)
            {
                command.OnCreate(axMapControlMainMap.Object);
                if (command is ITool)
                    axMapControlMainMap.CurrentTool = command as ESRI.ArcGIS.SystemUI.ITool;
                else
                    command.OnClick();
            }
        }

        ~MapViewerSimple()
        {
            axMapControlMainMap.Dispose();
        }

        public void LoadFile(string filename)
        {
            axMapControlMainMap.LoadMxFile(filename);
        }

        public void Close()
        {
            axMapControlMainMap.Dispose();
        }

        public void sBtnExpend_Click(object sender, EventArgs e)
        {
            if (m_preLeftShow)
            {
                m_preLeftShow = false;
                panelControl1.Dock = DockStyle.None;
                panelControl1.Size = this.sBtnExpend.Size + new Size(2, 2); ;
                panelControl1.Location = this.axMapControlMainMap.Location;
                //new Size(4, 4);
                splitterControl1.Visible = false;
                sBtnExpend.Text = ">";
            }
            else
            {
                m_preLeftShow = true;
                //panelControl1.Size = new System.Drawing.Size(simpleButton1.Width + 4, axMapControl1.Height);
                panelControl1.Dock = DockStyle.Left;
                panelControl1.Size = new Size(196, 484);
                splitterControl1.Visible = true;
                sBtnExpend.Text = "<";
            }
        }

        private void axMapControlMainMap_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            int statusFlag = CommonLib.MapMouseFlag;
            if (statusFlag <= 1) return;

            axMapControlMainMap.MousePointer = esriControlsMousePointer.esriPointerCrosshair;//鼠标指针:十字状
            IPoint pPoint = axMapControlMainMap.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
            #region 11-19：空间查询(画点、线、面)
            if (statusFlag >= 11 && statusFlag <= 19)
            {
                object symbol = null;
                switch (statusFlag)
                {
                    case 11:
                        IPoint point = new ESRI.ArcGIS.Geometry.Point();
                        point.X = e.mapX;
                        point.Y = e.mapY;
                        CommonLib.MapGeometry = point;
                        symbol = RenderOpt.GetSimpleMarkerSymbol((IColor) "ff0000");
                        break;
                    case 12:
                        CommonLib.MapGeometry = axMapControlMainMap.TrackLine();
                        symbol = RenderOpt.getSimpleLineSymbol("ff0000");
                        break;
                    case 13:
                        CommonLib.MapGeometry = axMapControlMainMap.TrackPolygon();
                        symbol = RenderOpt.GetSimpleFillSymbol("99ccff", "ff0000");
                        break;
                    case 14:
                        CommonLib.MapGeometry = axMapControlMainMap.TrackRectangle();
                        symbol = RenderOpt.GetSimpleFillSymbol("99ccff", "ff0000");
                        break;
                    default:
                        CommonLib.MapGeometry = null;
                        break;
                }

                if (CommonLib.MapGeometry != null)
                {
                    //axMapControlMainMap.Map.SelectByShape(CommonLib.MapGeometry, null, false);
                    //axMapControlMainMap.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    axMapControlMainMap.DrawShape(CommonLib.MapGeometry, ref symbol);
                }
            }
            #endregion
            #region
            //#region 绘制图形
            //if (CommonLib.MapMouseFlag >= 1 && CommonLib.MapMouseFlag <= 4)
            //{
            //    //初始化Color
            //    IRgbColor rgbColor1 = new RgbColor();
            //    rgbColor1.Red = 255;
            //    rgbColor1.Green = 255;
            //    rgbColor1.Blue = 0;

            //    //初始化Symbol
            //    object symbol = null;
            //    if (geometry.GeometryType == esriGeometryType.esriGeometryPolyline) //线
            //    {
            //        ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
            //        simpleLineSymbol.Color = rgbColor1;
            //        simpleLineSymbol.Width = 5;
            //        symbol = simpleLineSymbol;
            //    }
            //    else
            //    {
            //        ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol();
            //        simpleFillSymbol.Color = rgbColor1;
            //        symbol = simpleFillSymbol;
            //    }

            //    axMapControlMainMap.DrawShape(geometry, ref symbol);
            //}
            //else if (CommonLib.MapMouseFlag == 5)
            //{
            //    //初始化Color
            //    IRgbColor rgbColor2 = new RgbColor();
            //    rgbColor2.Red = 255;
            //    rgbColor2.Green = 0;
            //    rgbColor2.Blue = 0;

            //    //初始化Symbol
            //    object symbol = null;
            //    ITextSymbol textSymbol = new TextSymbol();
            //    textSymbol.Color = rgbColor2;
            //    symbol = textSymbol;

            //    axMapControlMainMap.DrawText(geometry, "顶你个肺", ref symbol);
            //}
            //#endregion

            //#region 空间选择
            //if (CommonLib.MapMouseFlag >= 11 && CommonLib.MapMouseFlag <= 15)
            //{
            //    axMapControlMainMap.Map.SelectByShape(geometry, null, false);
            //}
            //axMapControlMainMap.Refresh();

            //#endregion
            #endregion
        }

        private void axMapControlMainMap_OnMouseMove(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent e)
        {
            #region 主地图上移动鼠标时，状态栏显示当前坐标、比例尺等信息
            //将当前鼠标位置的点转换为地图上的坐标
            IPoint point = axMapControlMainMap.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
            //显示当前比例尺
            barSIScaleInfo.Caption = " 比例尺：  1/" + ((long)this.axMapControlMainMap.MapScale).ToString();
            //显示当前坐标
            barSICurCoors.Caption = " 当前坐标:  X=" + e.mapX.ToString() + "，  Y=" + e.mapY.ToString();
            #endregion
        }
        #region 地图导航条（Navigation）
        private void barBtnSelectElement_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region 方法一：利用arcgis的command
            CommonLib.MapMouseFlag = 1;
            DevExpress.XtraBars.BarButtonItem barBtn = e.Item as DevExpress.XtraBars.BarButtonItem;
            ESRI.ArcGIS.SystemUI.ICommand command = null;
            switch (barBtn.Name)
            {
                //case "BtnItemPoint":  //指针（SelectElment）
                //    command = new ESRI.ArcGIS.Controls.ControlsPa
                //    break;
                case "barBtnFullExtent":  //全屏
                    command = new ESRI.ArcGIS.Controls.ControlsMapFullExtentCommand();
                    break;
                case "barBtnZoomIn":      //放大
                    command = new ESRI.ArcGIS.Controls.ControlsMapZoomInTool();
                    break;
                case "barBtnZoomOut":     //缩小
                    command = new ESRI.ArcGIS.Controls.ControlsMapZoomOutTool();
                    break;
                case "barBtnPan":         //平移
                    command = new ESRI.ArcGIS.Controls.ControlsMapPanTool();
                    break;
                case "barBtnPreView":     //上一视图
                    command = new ESRI.ArcGIS.Controls.ControlsMapZoomToLastExtentBackCommand();
                    break;
                case "barBtnNextView":    //下一视图
                    command = new ESRI.ArcGIS.Controls.ControlsMapZoomToLastExtentForwardCommand();
                    break;
                case "barBtnIdentify":    //属性查看
                    command = new ESRI.ArcGIS.Controls.ControlsMapIdentifyTool();
                    break;
                case "barBtnSelectFeature":    //选择
                    command = new ESRI.ArcGIS.Controls.ControlsGlobeSelectFeaturesTool();
                    break;
                case "barBtnClearSelection":    //清空
                    command = new ESRI.ArcGIS.Controls.ControlsClearSelectionCommand();
                    break;
                case "barBtnZoomToTarget":   //坐标跳转
                    command = new ESRI.ArcGIS.Controls.ControlsMapGoToCommand();
                    break;
                default:
                    break;
            }
            if (command != null)
            {
                command.OnCreate(axMapControlMainMap.Object);
                if (command is ITool)
                    axMapControlMainMap.CurrentTool = command as ESRI.ArcGIS.SystemUI.ITool;
                else
                    command.OnClick();
            }
            #endregion

            #region 方法二：先建立一个ArcGIS的ToolbarControl控件，将相应功能添加进去，然后模拟鼠标点击调用该功能
            //DevComponents.DotNetBar.BubbleButton button = sender as DevComponents.DotNetBar.BubbleButton;
            //ESRI.ArcGIS.SystemUI.ICommand command = null;
            //command = axToolbarControl1.CommandPool.get_Command(1);   //模拟ToolbarControl控件的第一按钮被点击
            //command.OnClick();
            #endregion
        }

        #endregion

        private void axMapControlMainMap_OnDoubleClick(object sender, IMapControlEvents2_OnDoubleClickEvent e)//双击省图的选中要素追溯其包括的县级图斑，或者双击县级数据库中的图斑，追溯其对应的省级图斑
        {
            //if (axMapControlMainMap.LayerCount == 0) return;
        }

    }
}
