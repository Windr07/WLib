using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using WLib.ArcGis.Display;
using WLib.UserCtrls.Dev.ArcGisCtrl.Base;
using WLib.UserCtrls.FileViewer;

namespace WLib.UserCtrls.Dev.ArcGisCtrl
{
    public partial class MapViewerSimple : UserControl, IFileViewer
    {
        public AxMapControl AxMapControlMainMap;
        public AxTOCControl AxTocControl1;
        private AxLicenseControl _axLicenseControl1;

        private bool _mPreLeftShow = true;
        public BarStaticItem BarSiCurCoors = null;
        public BarStaticItem BarSiScaleInfo = null;
        public MapViewerSimple()
        {
            InitializeComponent();
            BarSiCurCoors = new BarStaticItem();
            BarSiScaleInfo = new BarStaticItem();


        }

        private void MapViewer_Load(object sender, EventArgs e)
        {
            //地图导航默认平移
            ICommand command = null;
            command = new ControlsMapPanTool();
            if (command != null)
            {
                command.OnCreate(AxMapControlMainMap.Object);
                if (command is ITool)
                    AxMapControlMainMap.CurrentTool = command as ITool;
                else
                    command.OnClick();
            }
        }

        ~MapViewerSimple()
        {
            AxMapControlMainMap.Dispose();
        }

        public void LoadFile(string filename)
        {
            AxMapControlMainMap.LoadMxFile(filename);
        }

        public void Close()
        {
            AxMapControlMainMap.Dispose();
        }

        public void sBtnExpend_Click(object sender, EventArgs e)
        {
            if (_mPreLeftShow)
            {
                _mPreLeftShow = false;
                panelControl1.Dock = DockStyle.None;
                panelControl1.Size = sBtnExpend.Size + new Size(2, 2); ;
                panelControl1.Location = AxMapControlMainMap.Location;
                //new Size(4, 4);
                splitterControl1.Visible = false;
                sBtnExpend.Text = ">";
            }
            else
            {
                _mPreLeftShow = true;
                //panelControl1.Size = new System.Drawing.Size(simpleButton1.Width + 4, axMapControl1.Height);
                panelControl1.Dock = DockStyle.Left;
                panelControl1.Size = new Size(196, 484);
                splitterControl1.Visible = true;
                sBtnExpend.Text = "<";
            }
        }

        private void axMapControlMainMap_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            int statusFlag = CommonLib.MapMouseFlag;
            if (statusFlag <= 1) return;

            AxMapControlMainMap.MousePointer = esriControlsMousePointer.esriPointerCrosshair;//鼠标指针:十字状
            IPoint pPoint = AxMapControlMainMap.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
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
                        symbol = RenderOpt.GetSimpleMarkerSymbol("ff0000");
                        break;
                    case 12:
                        CommonLib.MapGeometry = AxMapControlMainMap.TrackLine();
                        symbol = RenderOpt.GetSimpleLineSymbol("ff0000");
                        break;
                    case 13:
                        CommonLib.MapGeometry = AxMapControlMainMap.TrackPolygon();
                        symbol = RenderOpt.GetSimpleFillSymbol("99ccff", "ff0000");
                        break;
                    case 14:
                        CommonLib.MapGeometry = AxMapControlMainMap.TrackRectangle();
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
                    AxMapControlMainMap.DrawShape(CommonLib.MapGeometry, ref symbol);
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

        private void axMapControlMainMap_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            #region 主地图上移动鼠标时，状态栏显示当前坐标、比例尺等信息
            //将当前鼠标位置的点转换为地图上的坐标
            IPoint point = AxMapControlMainMap.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
            //显示当前比例尺
            BarSiScaleInfo.Caption = " 比例尺：  1/" + ((long)AxMapControlMainMap.MapScale).ToString();
            //显示当前坐标
            BarSiCurCoors.Caption = " 当前坐标:  X=" + e.mapX.ToString() + "，  Y=" + e.mapY.ToString();
            #endregion
        }
        #region 地图导航条（Navigation）
        private void barBtnSelectElement_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region 方法一：利用arcgis的command
            CommonLib.MapMouseFlag = 1;
            BarButtonItem barBtn = e.Item as BarButtonItem;
            ICommand command = null;
            switch (barBtn.Name)
            {
                //case "BtnItemPoint":  //指针（SelectElment）
                //    command = new ESRI.ArcGIS.Controls.ControlsPa
                //    break;
                case "barBtnFullExtent":  //全屏
                    command = new ControlsMapFullExtentCommand();
                    break;
                case "barBtnZoomIn":      //放大
                    command = new ControlsMapZoomInTool();
                    break;
                case "barBtnZoomOut":     //缩小
                    command = new ControlsMapZoomOutTool();
                    break;
                case "barBtnPan":         //平移
                    command = new ControlsMapPanTool();
                    break;
                case "barBtnPreView":     //上一视图
                    command = new ControlsMapZoomToLastExtentBackCommand();
                    break;
                case "barBtnNextView":    //下一视图
                    command = new ControlsMapZoomToLastExtentForwardCommand();
                    break;
                case "barBtnIdentify":    //属性查看
                    command = new ControlsMapIdentifyTool();
                    break;
                case "barBtnSelectFeature":    //选择
                    command = new ControlsGlobeSelectFeaturesTool();
                    break;
                case "barBtnClearSelection":    //清空
                    command = new ControlsClearSelectionCommand();
                    break;
                case "barBtnZoomToTarget":   //坐标跳转
                    command = new ControlsMapGoToCommand();
                    break;
                default:
                    break;
            }
            if (command != null)
            {
                command.OnCreate(AxMapControlMainMap.Object);
                if (command is ITool)
                    AxMapControlMainMap.CurrentTool = command as ITool;
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
