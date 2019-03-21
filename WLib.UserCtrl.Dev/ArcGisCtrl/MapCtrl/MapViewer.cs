using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using WLib.ArcGis.Carto;
using WLib.ArcGis.Carto.Map;
using WLib.ArcGis.Display;
using WLib.ArcGis.GeoDb.FeatClass;
using WLib.ArcGis.GeoDb.Fields;
using WLib.ArcGis.GeoDb.WorkSpace;
using WLib.UserCtrls.Dev.ArcGisCtrl.Base;
using WLib.UserCtrls.FileViewer;

namespace WLib.UserCtrls.Dev.ArcGisCtrl
{
    /// <summary>
    /// 地图及其图层/表格控制、鹰眼图、导航工具的组合控件
    /// </summary>
    public partial class MapViewer : UserControl
    {
        private ILayerEffectProperties _effectLayer = new CommandsEnvironmentClass();//卷帘
        private IToolbarMenu _toolBarMenu;//地图右键菜单接口
        private ILayer _toccSelectedLayer;//在TOC控件选择的图层
        private AttributeForm _attributeForm;
        private MeasureTools _measureTool;//自定义测量工具对象
        private QueryForm _queryForm;//按属性查询窗体
        private Dictionary<string, ToolStripMenuItem[]> _layer2FieldsMenuItems;//key：图层名，value：图层的字段菜单列表
        private int _areaPointCount;//面积计算记录勾画的多边形拐点的个数

        /// <summary>
        /// 
        /// </summary>
        public readonly MapDocLoader MxdLaoder;
        /// <summary>
        /// 通过移除菜单，移除图层后的事件处理
        /// </summary>
        public event EventHandler LayerRemoved;
        /// <summary>
        /// 通过添加按钮，添加图层后的事件处理
        /// </summary>
        public event EventHandler LayerAdded;
        /// <summary>
        /// ActiveView状态改变事件，此处用于获得图层添加事件
        /// </summary>
        public IActiveViewEvents_Event MapActiveViewEvents;


        /// <summary>
        /// 坐标信息栏
        /// </summary>
        public BarStaticItem BarSiCurCoors = new BarStaticItem();
        /// <summary>
        /// 比例尺信息栏
        /// </summary>
        public BarStaticItem BarSiScaleInfo = new BarStaticItem();
        /// <summary>
        /// 工具提示栏
        /// </summary>
        public BarStaticItem BarSiTips = new BarStaticItem { Caption = "就绪" };


        /// <summary>
        /// 初始化地图及其图层/表格控制、鹰眼图、导航工具的组合控件
        /// </summary>
        public MapViewer()
        {
            InitializeComponent();
            MxdLaoder = new MapDocLoader(axMapControlMainMap, axPageLayoutControl);
            _measureTool = new MeasureTools(axMapControlMainMap);
            measureResultLabel.Text = "";
            _layer2FieldsMenuItems = new Dictionary<string, ToolStripMenuItem[]>();
            LayerRemoved = (obj, evt) =>
            {
                if (_attributeForm != null && !_attributeForm.IsDisposed && _attributeForm.FeatLayer != null)
                    _attributeForm.Clear();
            };
            MapActiveViewEvents = axMapControlMainMap.Map as IActiveViewEvents_Event;
            //向map/PageLayout中添加数据，包括图层、表格等都会触发ItemAdded事件
            MapActiveViewEvents.ItemAdded += (item) =>
            {
                if (item is ILayer layer)
                {
                    LayerAdded?.Invoke(layer, new EventArgs());
                    GoToMapView();
                    GotoLayerToc();
                }
                else if (item is ITable table)
                {
                    this.tableListBox1.AddTable(table);
                    GoToMapView();
                    GotoTableListBox();
                }
            };

            _toolBarMenu = new ToolbarMenuClass();//工具栏菜单类
            _toolBarMenu.AddItem(new ControlsMapViewMenuClass(), 0, 0, false, esriCommandStyles.esriCommandStyleMenuBar);
            _toolBarMenu.AddItem(new ControlsMapPanToolClass(), 0, 0, false, esriCommandStyles.esriCommandStyleMenuBar);
            _toolBarMenu.AddItem(new ControlsMapZoomOutToolClass(), 0, 0, false, esriCommandStyles.esriCommandStyleMenuBar);
            _toolBarMenu.AddItem(new ControlsMapZoomInToolClass(), 0, 0, false, esriCommandStyles.esriCommandStyleMenuBar);
            _toolBarMenu.SetHook(axMapControlMainMap.Object);
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
            axPageLayoutControl.Visible = true;
        }
        /// <summary>
        /// 显示图层目录列表（TOCControl）
        /// </summary>
        public void GotoLayerToc()
        {
            axTOCControlLayerTree.Visible = true;
            tableListBox1.Visible = false;
            axTOCControlLayerTree.Dock = DockStyle.Fill;
            groupControlDOC.Text = "图层控制";
            simpleButtonCollapsed.Enabled = true;
            simpleButtonExpand.Enabled = true;
        }
        /// <summary>
        /// 显示图层目录列表（ImageListBox）
        /// </summary>
        public void GotoTableListBox()
        {
            axTOCControlLayerTree.Visible = false;
            tableListBox1.Visible = true;
            tableListBox1.Dock = DockStyle.Fill;
            groupControlDOC.Text = "表格列表";
            simpleButtonCollapsed.Enabled = false;
            simpleButtonExpand.Enabled = false;
        }
        /// <summary>
        /// 定位查询获得的第一个图斑
        /// </summary>
        /// <param name="featureLayer">查询图层</param>
        /// <param name="whereClause">查询条件</param>
        public void LocationFirstFeature(IFeatureLayer featureLayer, string whereClause)
        {
            GoToMapView();
            axMapControlMainMap.MapZoomToAndSelectFirst(featureLayer, whereClause);
        }
        /// <summary>
        /// 显示指定图层指定字段的标注
        /// </summary>
        public void ShowLabels(IGeoFeatureLayer geoLayer, string fieldName, string fontName = "宋体", int size = 12)
        {
            ShowFieldLabelMenuItems(geoLayer, fontName, size);
            if (_layer2FieldsMenuItems.ContainsKey(geoLayer.Name))
            {
                var menuItems = _layer2FieldsMenuItems[geoLayer.Name];
                var menuItem = menuItems.FirstOrDefault(v => v.Tag.ToString().Equals(fieldName));
                menuItem.PerformClick();
            }
        }
        /// <summary>
        /// 启用或触发工具栏（地图导航条）上的工具
        /// 
        /// </summary>
        /// <param name="toolIndex">地图导航条上的工具顺序，序号从0开始</param>
        public void ToolOnClick(int toolIndex)
        {
            SimpleButton btn = null;
            switch (toolIndex)
            {
                case 0: btn = btnFullExtent; break;
                case 1: btn = btnZoomIn; break;
                case 2: btn = btnZoomOut; break;
                case 3: btn = btnPan; break;
                case 4: btn = btnPreView; break;
                case 5: btn = btnNextView; break;
                case 6: btn = btnLenMeasure; break;
                case 7: btn = btnAreaMeasure; break;
                case 8: btn = btnSwipe; break;
                case 9: btn = btnIdentify; break;
                case 10: btn = btnSelection; break;
            }
            bubbleBarNavigationBtn_Click(btn, null);
        }


        /// <summary>
        /// 更新鹰眼图及布局控件的地图
        /// </summary>
        public void UpDateEagleMap()
        {
            //主地图更换地图后，鹰眼中的地图也跟随之更换
            axMapControlEagleMap.Map = new ESRI.ArcGIS.Carto.Map();
            for (int i = 0; i < axMapControlMainMap.LayerCount; i++)//添加主地图控件中的所有图层到鹰眼控件中
            {
                axMapControlEagleMap.AddLayer(axMapControlMainMap.get_Layer(axMapControlMainMap.LayerCount - (i + 1)));
            }

            //主地图更换地图后，布局控件的地图随之更换
            CopyMapToPageLayout();
        }
        /// <summary>
        /// 全图
        /// </summary>
        public void ZoomToWhole()
        {
            axMapControlMainMap.Extent = axMapControlMainMap.FullExtent;
        }


        /// <summary>
        /// 关闭（销毁）控件
        /// </summary>
        public void Close()
        {
            Dispose();
        }
        /// <summary>
        /// 展开/收缩图层的图例（最后需要执行toc的Update方法）
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="isExpand"></param>
        public void ExpandLegend(ILayer layer, bool isExpand)
        {
            ILegendInfo legendInfo = (ILegendInfo)layer;
            int iLegendGroupCount = legendInfo.LegendGroupCount;
            for (int i = 0; i < iLegendGroupCount; i++)
            {
                var legendGroup = legendInfo.get_LegendGroup(i);
                legendGroup.Visible = isExpand;
            }
            axMapControlMainMap.Update();
        }


        /// <summary>
        /// 主地图控件中的地图复制到布局控件中
        /// </summary>
        private void CopyMapToPageLayout()
        {
            object copyFromMap = axMapControlMainMap.Map;
            object copyToMap = axPageLayoutControl.ActiveView.FocusMap;
            IObjectCopy pObjectCopy = new ObjectCopyClass();
            pObjectCopy.Overwrite(pObjectCopy.Copy(copyFromMap), copyToMap);
        }
        /// <summary>
        /// 地图鼠标状态恢复到平移状态
        /// </summary>
        private void MapMouseToDefault()
        {
            CommonLib.MapMouseFlag = 1;
            ICommand command = new ControlsMapPanTool();
            command.OnCreate(axMapControlMainMap.Object);
            axMapControlMainMap.CurrentTool = command as ITool;
        }


        #region 主地图事件
        //主地图：鼠标点击（绘制图形、查询、数据编辑）
        private void axMapControlMainMap_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            int statusFlag = CommonLib.MapMouseFlag;
            if (e.button == 2)
            {
                if (statusFlag <= 7)
                    _toolBarMenu.PopupMenu(e.x, e.y, axMapControlMainMap.hWnd);
            }
            else if (e.button == 4)
            {
                esriControlsMousePointer pointer = axMapControlMainMap.MousePointer;
                axMapControlMainMap.MousePointer = esriControlsMousePointer.esriPointerPan;
                axMapControlMainMap.Pan();
                axMapControlMainMap.MousePointer = pointer;
            }

            if (statusFlag <= 1) return;
            axMapControlMainMap.MousePointer = esriControlsMousePointer.esriPointerCrosshair;//鼠标指针:十字状
            IPoint pPoint = axMapControlMainMap.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);

            #region 8-9:测距离、测面积
            if (statusFlag >= 8 && statusFlag <= 9)
            {
                measureResultPanel.Visible = true;
                switch (statusFlag)
                {
                    case 8: //测距离
                        if (e.button == 1)
                        {
                            if (!_measureTool.IsSurveying)
                                _measureTool.LengthStart(axMapControlMainMap.ToMapPoint(e.x, e.y));
                            else
                                _measureTool.AddPoint(axMapControlMainMap.ToMapPoint(e.x, e.y));
                        }
                        else
                        {
                            if (_measureTool.IsSurveying)
                            {
                                object lineSymbolObj = RenderOpt.GetSimpleLineSymbol("ff0000");
                                axMapControlMainMap.DrawShape(_measureTool.SurveyEnd(axMapControlMainMap.ToMapPoint(e.x, e.y)), ref lineSymbolObj);
                                measureResultLabel.Text = "总长度：" + _measureTool.TotalLength.ToString("F2") + "米" + Environment.NewLine + Environment.NewLine;
                                measureResultLabel.Text += "当前长度: " + _measureTool.CurrentLength.ToString("F2") + "米";
                                measureResultLabel.Refresh();
                                _measureTool.SurveyEnd(axMapControlMainMap.ToMapPoint(e.x, e.y));
                            }
                        }
                        break;
                    case 9: //测面积
                        if (e.button == 1)
                        {
                            if (!_measureTool.IsSurveying)
                            {
                                _measureTool.AreaStart(axMapControlMainMap.ToMapPoint(e.x, e.y));
                                _areaPointCount = 1;
                            }
                            else
                            {
                                _measureTool.AddPoint(axMapControlMainMap.ToMapPoint(e.x, e.y));
                                _areaPointCount++;
                            }
                        }
                        else
                        {
                            if (_measureTool.IsSurveying && _areaPointCount > 1)
                            {
                                object fillSymbolObj = RenderOpt.GetSimpleFillSymbol("99ccff", "ff0000");
                                axMapControlMainMap.DrawShape(_measureTool.SurveyEnd(axMapControlMainMap.ToMapPoint(e.x, e.y)), ref fillSymbolObj);
                                measureResultLabel.Text = "面积：" + _measureTool.Area.ToString("#########.##") + "平方米";
                                measureResultLabel.Refresh();
                                _measureTool.SurveyEnd(axMapControlMainMap.ToMapPoint(e.x, e.y));
                            }
                        }
                        break;
                }
            }
            #endregion

            #region 11-19：空间查询(画点、线、面)
            else if (statusFlag >= 11 && statusFlag <= 19)
            {
                object symbol = null;
                switch (statusFlag)
                {
                    case 11:
                        CommonLib.MapGeometry = new PointClass { X = e.mapX, Y = e.mapY };
                        symbol = RenderOpt.GetSimpleMarkerSymbol("ff0000");
                        break;
                    case 12:
                        CommonLib.MapGeometry = axMapControlMainMap.TrackLine();
                        symbol = RenderOpt.GetSimpleLineSymbol("ff0000");
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
                    axMapControlMainMap.DrawShape(CommonLib.MapGeometry, ref symbol);
            }
            #endregion
        }
        //主地图：鼠标移动(状态栏、测量)
        private void axMapControlMainMap_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            #region 主地图上移动鼠标时，状态栏显示当前坐标、比例尺等信息
            //将当前鼠标位置的点转换为地图上的坐标
            IPoint point = axMapControlMainMap.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
            //显示当前比例尺
            BarSiScaleInfo.Caption = " 比例尺：  1/" + ((long)axMapControlMainMap.MapScale).ToString();
            //显示当前坐标
            BarSiCurCoors.Caption = " 当前坐标:  X=" + e.mapX.ToString() + ",  Y=" + e.mapY.ToString();
            #endregion

            #region 主地图上移动鼠标时，测量结果动态变化
            if (axMapControlMainMap.CurrentTool == null)
            {
                switch (CommonLib.MapMouseFlag)
                {
                    case 8://测距离
                        if (_measureTool.IsSurveying)
                        {
                            _measureTool.MoveTo(axMapControlMainMap.ToMapPoint(e.x, e.y));
                            measureResultLabel.Text = "总长度：" + _measureTool.TotalLength.ToString("#########.##") + "米" + Environment.NewLine + Environment.NewLine;
                            measureResultLabel.Text += "当前长度:" + _measureTool.CurrentLength.ToString("#########.##") + "米";
                            measureResultLabel.Refresh();
                        }
                        break;
                    case 9://测面积
                        if (_measureTool.IsSurveying)
                        {
                            _measureTool.MoveTo(axMapControlMainMap.ToMapPoint(e.x, e.y));
                            measureResultLabel.Text = "面积：" + _measureTool.Area.ToString("#########.##") + "平方米";
                            measureResultLabel.Refresh();
                        }
                        break;
                }
            }
            #endregion
        }
        //主地图：更换地图后
        private void axMapControlMainMap_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            UpDateEagleMap();
        }
        //主地图：地图范围（Extent）变化
        private void axMapControlMainMap_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            #region 主地图地图范围Extent变化时，鹰眼中的地图范围Extent也跟随着变化
            IEnvelope envelope = (IEnvelope)e.newEnvelope;  //得到主地图的新范围
            IGraphicsContainer graphicsContainer = axMapControlEagleMap.Map as IGraphicsContainer;
            IActiveView activeView = graphicsContainer as IActiveView;
            graphicsContainer.DeleteAllElements();  //在绘制前，清除axMapControlEagleMap中的任何图形元素
            IRectangleElement rectangleElement = new RectangleElement() as IRectangleElement;
            IElement element = rectangleElement as IElement;
            element.Geometry = envelope;
            //产生一个线符号对象,设置鹰眼中的红线框  
            ILineSymbol lineSymbol = new SimpleLineSymbol();
            lineSymbol.Width = 1.6;
            lineSymbol.Color = RenderOpt.GetIColor(255, 0, 0);
            //设置填充符号的属性
            IFillSymbol fillSymbol = new SimpleFillSymbol();
            fillSymbol.Color = RenderOpt.GetIColor(255, 0, 0, 0);
            fillSymbol.Outline = lineSymbol;
            IFillShapeElement fillShapeElement = element as IFillShapeElement;
            fillShapeElement.Symbol = fillSymbol;
            graphicsContainer.AddElement((IElement)fillShapeElement, 0);
            //刷新 
            envelope.Expand(2, 2, true);
            activeView.Extent = envelope;
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            #endregion
        }
        //主地图：刷新地图
        private void axMapControlMainMap_OnFullExtentUpdated(object sender, IMapControlEvents2_OnFullExtentUpdatedEvent e)
        {
            axMapControlMainMap.Refresh();
        }
        //主地图：屏幕更新完毕
        private void axMapControlMainMap_OnAfterScreenDraw(object sender, IMapControlEvents2_OnAfterScreenDrawEvent e)
        {
            #region 布局控件：主地图屏幕更新完毕，布局控件与之关联
            IActiveView pActiveView = axPageLayoutControl.ActiveView.FocusMap as IActiveView;
            IDisplayTransformation pDisplayTransformation = pActiveView.ScreenDisplay.DisplayTransformation;
            pDisplayTransformation.VisibleBounds = axMapControlMainMap.Extent;
            axPageLayoutControl.ActiveView.Refresh();
            //copyMapToPageLayout();
            #endregion
        }
        #endregion


        #region 鹰眼图事件
        //鹰眼地图：左键拖动矩形框、右键绘制矩形框
        private void axMapControlEagleMap_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (axMapControlEagleMap.Map.LayerCount == 0) return;
            //按下鼠标左键拖动矩形框
            if (e.button == 1)
            {
                IPoint point = new ESRI.ArcGIS.Geometry.Point();
                point.PutCoords(e.mapX, e.mapY);
                IEnvelope envelope = axMapControlMainMap.Extent;
                envelope.CenterAt(point);
                axMapControlMainMap.Extent = envelope;
                axMapControlMainMap.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            //按下鼠标右键绘制矩形框
            else if (e.button == 2)
            {
                IEnvelope envelope = axMapControlEagleMap.TrackRectangle();

                //计算新显示框范围
                double newWidth = 0;
                double newHeight = 0;
                if (envelope.IsEmpty)
                    return;
                if (envelope.Width / envelope.Height > axMapControlMainMap.ActiveView.Extent.Width / axMapControlMainMap.ActiveView.Extent.Height)
                {//宽相同
                    newWidth = envelope.Width;
                    newHeight = envelope.Width * axMapControlMainMap.ActiveView.Extent.Height / axMapControlMainMap.ActiveView.Extent.Width;
                }
                else//高相同 
                {
                    newHeight = envelope.Height;
                    newWidth = envelope.Height * axMapControlMainMap.ActiveView.Extent.Width / axMapControlMainMap.ActiveView.Extent.Height;
                }

                double midX = (envelope.XMin + envelope.XMax) / 2;
                double midY = (envelope.YMin + envelope.YMax) / 2;
                double xmi = midX - newWidth / 2;
                double xma = midX + newWidth / 2;
                double ymi = midY - newHeight / 2;
                double yma = midY + newHeight / 2;
                envelope.PutCoords(xmi, ymi, xma, yma);

                axMapControlMainMap.Extent = envelope;
                axMapControlMainMap.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
        }
        //鹰眼地图：回复鼠标指针状态
        private void axMapControlEagleMap_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            axMapControlEagleMap.MousePointer = esriControlsMousePointer.esriPointerDefault;
        }
        //鹰眼地图：左键拖动矩形框
        private void axMapControlEagleMap_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            // 如果不是左键按下就直接返回
            if (e.button != 1) return;
            axMapControlEagleMap.MousePointer = esriControlsMousePointer.esriPointerSizeAll;
            IPoint pPoint = new PointClass();
            pPoint.PutCoords(e.mapX, e.mapY);
            axMapControlMainMap.CenterAt(pPoint);
            axMapControlEagleMap.CenterAt(pPoint);
            axMapControlMainMap.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            axMapControlEagleMap.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }
        #endregion


        #region TOC控件及其按钮、菜单事件
        private void simpleButtonSwitchContent_Click(object sender, EventArgs e)//TOC切换到Table列表
        {
            if (axTOCControlLayerTree.Visible)
                GotoTableListBox();
            else
                GotoLayerToc();
            groupControlDOC.Update();
        }
        private void simpleButtonCollapsed_Click(object sender, EventArgs e)//TOCC图层文件收起
        {
            UID uid = new UIDClass();
            uid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";
            IEnumLayer layers = axMapControlMainMap.Map.get_Layers(uid, true);
            layers.Reset();
            ILayer layer = null;
            while ((layer = layers.Next()) != null)
            {
                ILegendInfo legendInfo = layer as ILegendInfo;
                int legendGroupCount = legendInfo.LegendGroupCount;
                for (int i = 0; i < legendGroupCount; i++)
                {
                    var lGroup = legendInfo.get_LegendGroup(i);
                    lGroup.Visible = false;
                }
            }
            axTOCControlLayerTree.Update();
        }
        private void simpleButtonExpand_Click(object sender, EventArgs e)//TOCC图层文件展开
        {
            UID uid = new UIDClass();
            uid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";
            IEnumLayer layers = axMapControlMainMap.Map.get_Layers(uid, true);
            layers.Reset();
            ILayer layer = null;
            while ((layer = layers.Next()) != null)
            {
                ILegendInfo pLegendInfo = layer as ILegendInfo;
                int iLegendGroupCount = pLegendInfo.LegendGroupCount;
                for (int i = 0; i < iLegendGroupCount; i++)
                {
                    var lGroup = pLegendInfo.get_LegendGroup(i);
                    lGroup.Visible = true;
                }
            }
            axTOCControlLayerTree.Update();
        }
        private void sBtnAddData_Click(object sender, EventArgs e)//TOCC添加数据(Add Data)
        {
            MxdLaoder.AddData();
        }
        private void axTOCControlLayerTree_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)//TOCC左键设置图例，右键弹出菜单
        {
            esriTOCControlItem pItem = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap pMap = null;
            object unk = null, data = null;
            axTOCControlLayerTree.HitTest(e.x, e.y, ref pItem, ref pMap, ref _toccSelectedLayer, ref unk, ref data);
            if (e.button == 1)
            {
                if (pItem == esriTOCControlItem.esriTOCControlItemLegendClass)
                {
                    //取得图例
                    ILegendClass legendClass = ((ILegendGroup)unk).get_Class((int)data);
                    var symbolSelectorForm = new SymbolSelectorDevForm(legendClass, _toccSelectedLayer);
                    if (symbolSelectorForm.ShowDialog() == DialogResult.OK)
                    {
                        GoToMapView();
                        legendClass.Symbol = symbolSelectorForm.PSymbol; //设置新的符号
                        axMapControlMainMap.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);//更新主Map控件和图层控件
                    }
                    _effectLayer.SwipeLayer = _toccSelectedLayer;
                }
            }
            else if (e.button == 2)
            {

                if (pItem == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    cMenuStripTocc.Show(axTOCControlLayerTree, new System.Drawing.Point(e.x, e.y));
                    ShowFieldLabelMenuItems(_toccSelectedLayer);
                }
            }
        }
        private void 缩放到图层toolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoToMapView();
            if (_toccSelectedLayer != null)
            {
                IEnvelope envelope = _toccSelectedLayer.AreaOfInterest;
                axMapControlMainMap.Extent = envelope;
            }
        }
        private void 打开属性表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ITable table = _toccSelectedLayer as ITable;
            if (table != null)
            {
                if (_attributeForm == null || _attributeForm.IsDisposed)
                {
                    _attributeForm = new AttributeForm();
                    _attributeForm.FeatureLocation += _attributeForm_FeatureLocation;
                }
                else
                {
                    _attributeForm.FeatureLocation -= _attributeForm_FeatureLocation;
                    _attributeForm.FeatureLocation += _attributeForm_FeatureLocation;
                    _attributeForm.Activate();//之前已打开，则给予焦点，置顶。
                }
                if (!_attributeForm.Visible)
                    _attributeForm.Show(this);

                IFeatureLayerDefinition featureLyrDef = _toccSelectedLayer as IFeatureLayerDefinition;
                string whereClause = featureLyrDef?.DefinitionExpression;
                _attributeForm.LoadAttribute(_toccSelectedLayer as IFeatureLayer, whereClause);
            }
        }
        private void 定义查询IToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_toccSelectedLayer is IFeatureLayer featureLayer)
            {
                if (_queryForm == null || _queryForm.IsDisposed)
                {
                    _queryForm = new QueryForm(featureLayer.FeatureClass as ITable, false);
                    _queryForm.Query += (sender2, e2) =>
                    {
                        if (featureLayer is IFeatureLayerDefinition featureLyrDef)
                        {
                            featureLyrDef.DefinitionExpression = _queryForm.WhereClause;
                            移除定义查询CToolStripMenuItem.Visible = true;
                        }
                    };
                }
                _queryForm.Show(this);
            }
        }
        private void 移除定义查询CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_toccSelectedLayer is IFeatureLayer featureLayer)
            {
                if (featureLayer is IFeatureLayerDefinition featureLyrDef)
                {
                    featureLyrDef.DefinitionExpression = null;
                    移除定义查询CToolStripMenuItem.Visible = false;
                }
            }
        }
        private void _attributeForm_FeatureLocation(object sender, FeatureLocationEventArgs e)//定位查询获得的第一个图斑
        {
            LocationFirstFeature(e.LocationLayer, e.WhereClause);
        }
        private void ShowFieldLabelMenuItems(ILayer setFieldLabelLayer, string fontName = "宋体", int size = 12)//将图层的字段名作为菜单显示，用于设置标注
        {
            ITable table = setFieldLabelLayer as ITable;
            if (table == null)
                return;

            if (!_layer2FieldsMenuItems.ContainsKey(setFieldLabelLayer.Name))
            {
                var namesDict = table.GetFieldNameAndAliasName();
                if (namesDict.ContainsKey("SHAPE"))
                    namesDict.Remove("SHAPE");
                List<ToolStripMenuItem> menuItems = new List<ToolStripMenuItem>();
                foreach (var namePair in namesDict)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem(namePair.Value);
                    item.Tag = namePair.Key;
                    item.Click += (sender2, e2) =>
                    {
                        GoToMapView();
                        item.Checked = !item.Checked;
                        if (setFieldLabelLayer is IGeoFeatureLayer geoFeatureLayer)
                        {
                            if (item.Checked)
                            {
                                geoFeatureLayer.ShowLabel(namePair.Key.ToString(), fontName, size);
                                var subItems = (item.OwnerItem as ToolStripMenuItem).DropDownItems.OfType<ToolStripMenuItem>();
                                foreach (ToolStripMenuItem subItem in subItems)
                                {
                                    subItem.Checked = false;
                                }
                                item.Checked = true;
                            }
                            else
                                geoFeatureLayer.DisplayAnnotation = false;
                            axMapControlMainMap.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                            axMapControlEagleMap.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                        }
                    };
                    menuItems.Add(item);
                }
                _layer2FieldsMenuItems.Add(setFieldLabelLayer.Name, menuItems.ToArray());
            }
            //绑定图层的字段列表
            显示标注AToolStripMenuItem.DropDownItems.Clear();
            显示标注AToolStripMenuItem.DropDownItems.AddRange(_layer2FieldsMenuItems[setFieldLabelLayer.Name]);
        }
        private void 移除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControlMainMap.Map.DeleteLayer(_toccSelectedLayer);
            LayerRemoved(_toccSelectedLayer, new EventArgs());
        }
        #endregion


        #region 地图导航条事件
        private void bubbleBarNavigationBtn_Click(object sender, EventArgs e)//主地图地图导航条：Click
        {
            CommonLib.MapMouseFlag = 1;
            SimpleButton button = sender as SimpleButton;
            ICommand command = null;
            switch (button.Name)
            {
                case "bubbleButtonFullExtent": command = new ControlsMapFullExtentCommand(); break;//全屏
                case "bubbleButtonZoomIn": command = new ControlsMapZoomInTool(); break;  //放大
                case "bubbleButtonZoomOut": command = new ControlsMapZoomOutTool(); break;  //缩小
                case "bubbleButtonPan": command = new ControlsMapPanTool(); break;      //平移
                case "bubbleButtonPreView": command = new ControlsMapZoomToLastExtentBackCommand(); break;//上一视图
                case "bubbleButtonNextView": command = new ControlsMapZoomToLastExtentForwardCommand(); break;  //下一视图
                default:
                    axMapControlMainMap.CurrentTool = null;
                    CommonLib.MapMouseFlag = 10;
                    _effectLayer.SwipeLayer = _toccSelectedLayer;
                    if (button.Name == "bubbleButtonLenMeasure")
                        CommonLib.MapMouseFlag = 8;
                    else if (button.Name == "bubbleButtonAreaMeasure")
                        CommonLib.MapMouseFlag = 9;
                    else if (button.Name == "bubbleButtonIdentify")
                        command = new ControlsMapIdentifyTool();
                    else if (button.Name == "bubbleButtonSwipe")
                        command = new ControlsMapSwipeToolClass();
                    else if (button.Name == "bubbleButtonSelection")
                        command = new ControlsSelectFeaturesToolClass();
                    break;
            }
            if (command != null)
            {
                command.OnCreate(axMapControlMainMap.Object);
                if (command is ITool tool)
                    axMapControlMainMap.CurrentTool = tool;
                else
                    command.OnClick();
            }
        }
        private void pageLayoutNavigationBtn_Click(object sender, EventArgs e) //布局地图导航条：Click
        {
            CommonLib.MapMouseFlag = 1;

            SimpleButton button = sender as SimpleButton;
            ICommand command = null;
            switch (button.Name)
            {
                case "fullExtentPageLayout": command = new ControlsPageZoomWholePageCommand(); break; //全屏
                case "zoomInPageLayout": command = new ControlsPageZoomInTool(); break;  //放大
                case "zoomOutPageLayout": command = new ControlsPageZoomOutTool(); break;  //缩小
                case "panPageLayout": command = new ControlsPagePanTool(); break;  //平移
                case "preViewPageLayout": command = new ControlsPageZoomPageToLastExtentBackCommand(); break; //上一视图
                case "nextViewPageLayout": command = new ControlsPageZoomPageToLastExtentForwardCommand(); break;  //下一视图
            }
            if (command != null)
            {
                command.OnCreate(axPageLayoutControl.Object);
                if (command is ITool tool)
                    axPageLayoutControl.CurrentTool = tool;
                else
                    command.OnClick();
            }
        }
        private void bubbleBarNavigationBtn_MouseMove(object sender, MouseEventArgs e) //主地图导航条、布局视图导航条：MouseMove（状态栏显示ToolTip）
        {
            SimpleButton button = sender as SimpleButton;
            BarSiTips.Caption = button.ToolTip;
        }
        #endregion


        private void measureResultClose_Click(object sender, EventArgs e)//测量结果清空、关闭
        {
            MapMouseToDefault();
            measureResultPanel.Visible = false;
            measureResultLabel.Text = "";
            axMapControlMainMap.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }
    }
}
