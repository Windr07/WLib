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
using WLib.UserCtrls.Viewer;

namespace WLib.UserCtrls.Dev.ArcGisCtrl
{
    /// <summary>
    /// 地图及其图层/表格控制、鹰眼图、导航工具的组合控件
    /// </summary>
    public partial class MapViewer : UserControl, IViewer
    {
        private ILayerEffectProperties _effectLayer = new CommandsEnvironmentClass();//卷帘
        private IMapDocument _mapDoc;//地图文档
        private IToolbarMenu _pToolBarMenu;//地图右键菜单接口
        private ILayer _toccSelectedLayer;//在TOC控件选择的图层
        private AttributeDevForm _attributeForm;
        private MeasureTools _measureTool;//自定义测量工具对象
        private QueryDevForm _queryForm;//按属性查询窗体
        private Dictionary<string, ToolStripMenuItem[]> _layer2FieldsMenuItems;//key：图层名，value：图层的字段菜单列表
        private int _areaPointCount;//面积计算记录勾画的多边形拐点的个数

        /// <summary>
        /// 加载在表格列表中的表格
        /// </summary>
        private List<ITable> _tables;
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
        public BarStaticItem barSICurCoors;
        /// <summary>
        /// 比例尺信息栏
        /// </summary>
        public BarStaticItem barSIScaleInfo;
        /// <summary>
        /// 工具提示栏
        /// </summary>
        public BarStaticItem barSITips;
        /// <summary>
        /// 包含显示地图、页面布局等页面的TabControl控件
        /// </summary>
        public DevExpress.XtraTab.XtraTabControl ViewerTabControl => xtcForm;

        /// <summary>
        /// 初始化地图及其图层/表格控制、鹰眼图、导航工具的组合控件
        /// </summary>
        public MapViewer()
        {
            InitializeComponent();
            _tables = new List<ITable>();
            barSICurCoors = new BarStaticItem();
            barSIScaleInfo = new BarStaticItem();
            barSITips = new BarStaticItem();
            barSITips.Caption = "就绪";
            measureResultLabel.Text = "";
            _layer2FieldsMenuItems = new Dictionary<string, ToolStripMenuItem[]>();

            LayerRemoved = (obj, evt) =>
            {
                if (_attributeForm != null && !_attributeForm.IsDisposed && _attributeForm.Layer != null)
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
                    GotoLayerTOC();
                }
                else if (item is ITable table)
                {
                    AddTable(table);
                    GoToMapView();
                    GotoTableListBox();
                }
            };

            _pToolBarMenu = new ToolbarMenuClass();//工具栏菜单类
            _pToolBarMenu.AddItem(new ControlsMapViewMenuClass(), 0, 0, false, esriCommandStyles.esriCommandStyleMenuBar);
            _pToolBarMenu.AddItem(new ControlsMapPanToolClass(), 0, 0, false, esriCommandStyles.esriCommandStyleMenuBar);
            _pToolBarMenu.AddItem(new ControlsMapZoomOutToolClass(), 0, 0, false, esriCommandStyles.esriCommandStyleMenuBar);
            _pToolBarMenu.AddItem(new ControlsMapZoomInToolClass(), 0, 0, false, esriCommandStyles.esriCommandStyleMenuBar);
            _pToolBarMenu.SetHook(axMapControlMainMap.Object);
        }

        //公有方法

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
        public void GotoLayerTOC()
        {
            axTOCControlLayerTree.Visible = true;
            imagelistBoxTables.Visible = false;
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
            imagelistBoxTables.Visible = true;
            imagelistBoxTables.Dock = DockStyle.Fill;
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
                case 0: btn = bubbleButtonFullExtent; break;
                case 1: btn = bubbleButtonZoomIn; break;
                case 2: btn = bubbleButtonZoomOut; break;
                case 3: btn = bubbleButtonPan; break;
                case 4: btn = bubbleButtonPreView; break;
                case 5: btn = bubbleButtonNextView; break;
                case 6: btn = bubbleButtonLenMeasure; break;
                case 7: btn = bubbleButtonAreaMeasure; break;
                case 8: btn = bubbleButtonSwipe; break;
                case 9: btn = bubbleButtonIdentify; break;
                case 10: btn = bubbleButtonSelection; break;
            }
            bubbleBarNavigationBtn_Click(btn, null);
        }

        /// <summary>
        /// 新建地图文档
        /// </summary>
        /// <param name="fileName">地图文档路径</param>
        /// <param name="loadMapAfterBuilt">是否将地图文档加载到地图控件</param>
        public IMapDocument NewMap(string fileName, bool loadMapAfterBuilt)
        {
            return NewMap(fileName, "图层", loadMapAfterBuilt);
        }
        /// <summary>
        /// 新建地图文档
        /// </summary>
        /// <param name="fileName">地图文档路径</param>
        /// <param name="mapName">地图名称</param>
        /// <param name="loadMapAfterBuilt">是否将地图文档加载到地图控件</param>
        /// <returns></returns>
        public IMapDocument NewMap(string fileName, string mapName, bool loadMapAfterBuilt)
        {
            IMapDocument pMapDoc = new MapDocumentClass();
            pMapDoc.New(fileName);
            pMapDoc.Map[0].Name = mapName;
            if (loadMapAfterBuilt)
                axMapControlMainMap.LoadMxFile(fileName);
            return pMapDoc;
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        public void AddData()
        {
            ControlsAddDataCommandClass addDataCmd = new ControlsAddDataCommandClass();
            addDataCmd.OnCreate(axMapControlMainMap.Object);
            addDataCmd.OnClick();
        }
        /// <summary>
        /// 保存地图
        /// </summary>
        public void Save()
        {
            if (_mapDoc == null)
            {
                _mapDoc = new MapDocumentClass();
                SaveFileDialog sv = new SaveFileDialog();
                sv.Title = "保存地图文档";
                sv.FileName = "地图文档";
                sv.Filter = "地图文档(*.mxd)|*.mxd";
                if (sv.ShowDialog() == DialogResult.OK)
                    SaveAs(sv.FileName);
            }
            else
            {
                _mapDoc.ReplaceContents(axMapControlMainMap.Map as IMxdContents);
                _mapDoc.ReplaceContents(axPageLayoutControl.PageLayout as IMxdContents);
                _mapDoc.Save();
                _mapDoc.Close();
            }
        }
        /// <summary>
        /// 另存为新地图文档
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveAs(string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
            if (_mapDoc == null)
                _mapDoc = new MapDocumentClass();
            _mapDoc.New(fileName);
            _mapDoc.ReplaceContents(axMapControlMainMap.Map as IMxdContents);
            _mapDoc.ReplaceContents(axPageLayoutControl.PageLayout as IMxdContents);
            _mapDoc.Save();
            _mapDoc.Close();
        }
        /// <summary>
        /// 刷新地图
        /// </summary>
        public void RefreshMap()
        {
            axMapControlMainMap.Refresh();
            axMapControlMainMap.Update();
        }


        /// <summary>
        /// 更新鹰眼图及布局控件的地图
        /// </summary>
        public void UpDateEagleMap()
        {
            //主地图更换地图后，鹰眼中的地图也跟随之更换
            axMapControlEagleMap.Map = new Map();
            for (int i = 0; i < axMapControlMainMap.LayerCount; i++)//添加主地图控件中的所有图层到鹰眼控件中
            {
                axMapControlEagleMap.AddLayer(axMapControlMainMap.get_Layer(axMapControlMainMap.LayerCount - (i + 1)));
            }

            //主地图更换地图后，布局控件的地图随之更换
            copyMapToPageLayout();
        }
        /// <summary>
        /// 全图
        /// </summary>
        public void ZoomToWhole()
        {
            axMapControlMainMap.Extent = axMapControlMainMap.FullExtent;
        }
        /// <summary>
        /// 清空图层并保存
        /// </summary>
        public void ClearLayers()
        {
            IMapDocument pMapDoc = new MapDocumentClass();
            string strfilename = axMapControlMainMap.DocumentFilename;
            pMapDoc.Open(axMapControlMainMap.DocumentFilename, "");
            pMapDoc.Map[0].ClearLayers();
            pMapDoc.Save();
            pMapDoc.Close();
            axMapControlMainMap.LoadMxFile(strfilename);
        }
        /// <summary>
        /// 加载地图
        /// </summary>
        /// <param name="mxdFile"></param>
        public void LoadFile(string mxdFile)
        {
            if (_mapDoc != null && !File.Exists(_mapDoc.DocumentFilename))
                _mapDoc.Close();

            _mapDoc = new MapDocumentClass();
            _mapDoc.Open(mxdFile);

            axMapControlMainMap.LoadMxFile(mxdFile);
        }
        /// <summary>
        /// 打开指定路径或数据库（shp/gdb/mdb/sde等）中的要素类全部加载到地图中
        /// </summary>
        /// <param name="strConOrPath">mdb文件路径，或shp文件夹路径，或gdb文件夹路径，或数据库连接字符串</param>
        public void LoadDbFile(string strConOrPath)
        {
            var workspace = GetWorkspace.GetWorkSpace(strConOrPath);
            var featureClasses = workspace.GetFeatureClasses();
            featureClasses = featureClasses.SortByGeometryType();

            for (int i = featureClasses.Count - 1; i >= 0; i--)//将要素类作为图层加入地图控件中
            {
                axMapControlMainMap.AddLayer(new FeatureLayerClass()
                {
                    FeatureClass = featureClasses[i],
                    Name = featureClasses[i].AliasName
                });
            }
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
        /// <param name="pLayer"></param>
        /// <param name="bExpand"></param>
        public void ExpandLegend(ILayer pLayer, bool bExpand)
        {
            ILegendInfo legendInfo = pLayer as ILegendInfo;
            int iLegendGroupCount = legendInfo.LegendGroupCount;
            for (int i = 0; i < iLegendGroupCount; i++)
            {
                var legendGroup = legendInfo.get_LegendGroup(i);
                legendGroup.Visible = bExpand;
            }
            axMapControlMainMap.Update();
        }


        /// <summary>
        /// 添加表格
        /// </summary>
        /// <param name="table"></param>
        public void AddTable(ITable table)
        {
            _tables.Add(table);
            string tableName = null;
            if (table is IObjectClass objectClass)
                tableName = objectClass.AliasName;
            else
                tableName = ((IDataset)table).Name;
            imagelistBoxTables.Items.Add(new ImageListBoxItem(tableName, 17));
        }
        /// <summary>
        /// 添加多个表格
        /// </summary>
        /// <param name="tables"></param>
        public void AddTables(IEnumerable<ITable> tables)
        {
            _tables.AddRange(tables);
            var tableNamesItems = tables.Select(v => ((IObjectClass)v).AliasName).Select(v => new ImageListBoxItem(v, 17)).ToArray();
            imagelistBoxTables.Items.AddRange(tableNamesItems);
        }
        /// <summary>
        /// 移除指定表格
        /// </summary>
        /// <param name="table"></param>
        public void RemoveTable(ITable table)
        {
            int index = _tables.IndexOf(table);
            _tables.Remove(table);
            imagelistBoxTables.Items.RemoveAt(index);
        }

        /// <summary>
        /// 移除指定表格
        /// </summary>
        /// <param name="tableName"></param>
        public void RemoveTable(string tableName)
        {
            var table = _tables.FirstOrDefault(v => ((IObjectClass)v).AliasName == tableName);
            if (table != null)
                RemoveTable(table);
        }
        /// <summary>
        /// 清空表格列表
        /// </summary>
        public void ClearTable()
        {
            _tables.Clear();
            imagelistBoxTables.Items.Clear();
        }


        #region 主地图事件
        //主地图：鼠标点击（绘制图形、查询、数据编辑）
        private void axMapControlMainMap_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            int statusFlag = CommonLib.MapMouseFlag;
            if (e.button == 2)
            {
                if (statusFlag <= 7)
                    _pToolBarMenu.PopupMenu(e.x, e.y, axMapControlMainMap.hWnd);
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

            #region 10:信息查看（Identify）
            else if (statusFlag == 10)
            {
                //m_identityPoint = axMapControlMainMap.ToMapPoint(e.x, e.y);
                //this.textEditIdentifyPoint.Text = string.Format("{0}, {1}", m_identityPoint.X.ToString("########.####"), m_identityPoint.Y.ToString("########.####"));
                //CommonLib.progressBarShow("请稍候，正在地图识别查询中……");
                //this.getIdentifyResults();
                //this.displayIdentifyResults();
                //CommonLib.progressBarClose();
            }
            #endregion

            #region 11-19：空间查询(画点、线、面)
            else if (statusFlag >= 11 && statusFlag <= 19)
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
                {
                    //axMapControlMainMap.Map.SelectByShape(CommonLib.MapGeometry, null, false);
                    //axMapControlMainMap.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    axMapControlMainMap.DrawShape(CommonLib.MapGeometry, ref symbol);
                }
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
            barSIScaleInfo.Caption = " 比例尺：  1/" + ((long)axMapControlMainMap.MapScale).ToString();
            //显示当前坐标
            barSICurCoors.Caption = " 当前坐标:  X=" + e.mapX.ToString() + ",  Y=" + e.mapY.ToString();
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
                GotoLayerTOC();
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
                ILegendInfo pLegendInfo = layer as ILegendInfo;
                int iLegendGroupCount = pLegendInfo.LegendGroupCount;
                ILegendGroup pLGroup;
                for (int i = 0; i < iLegendGroupCount; i++)
                {
                    pLGroup = pLegendInfo.get_LegendGroup(i);
                    pLGroup.Visible = false;
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
                ILegendGroup pLGroup;
                for (int i = 0; i < iLegendGroupCount; i++)
                {
                    pLGroup = pLegendInfo.get_LegendGroup(i);
                    pLGroup.Visible = true;
                }
            }
            axTOCControlLayerTree.Update();
        }
        private void sBtnAddData_Click(object sender, EventArgs e)//TOCC添加数据(Add Data)
        {
            AddData();
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
                    var _symbolSelectorForm = new SymbolSelectorDevForm(legendClass, _toccSelectedLayer);
                    if (_symbolSelectorForm.ShowDialog() == DialogResult.OK)
                    {
                        GoToMapView();
                        legendClass.Symbol = _symbolSelectorForm.pSymbol; //设置新的符号
                        axMapControlMainMap.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);//更新主Map控件和图层控件
                    }
                    _effectLayer.SwipeLayer = _toccSelectedLayer;
                }
            }
            else if (e.button == 2)
            {

                if (pItem == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    System.Drawing.Point point = new System.Drawing.Point();
                    point.X = e.x;
                    point.Y = e.y;
                    cMenuStripTocc.Show(axTOCControlLayerTree, point);
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
                    _attributeForm = new AttributeDevForm();
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
                string whereClause = featureLyrDef == null ? null : featureLyrDef.DefinitionExpression;
                _attributeForm.LoadAttribute(_toccSelectedLayer as IFeatureLayer, whereClause);
            }
        }
        private void 定义查询IToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureLayer featureLayer = _toccSelectedLayer as IFeatureLayer;
            if (featureLayer != null)
            {
                if (_queryForm == null || _queryForm.IsDisposed)
                {
                    _queryForm = new QueryDevForm(featureLayer.FeatureClass as ITable, false);
                    _queryForm.Query += (sender2, e2) =>
                    {
                        IFeatureLayerDefinition featureLyrDef = featureLayer as IFeatureLayerDefinition;
                        if (featureLyrDef != null)
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
            IFeatureLayer featureLayer = _toccSelectedLayer as IFeatureLayer;
            if (featureLayer != null)
            {
                IFeatureLayerDefinition featureLyrDef = featureLayer as IFeatureLayerDefinition;
                if (featureLyrDef != null)
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
                        var geoFeatureLayer = setFieldLabelLayer as IGeoFeatureLayer;
                        if (geoFeatureLayer != null)
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

        #region 表格列表及其菜单事件
        private void 打开表格属性表TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imagelistBoxTables.SelectedIndex < 0)
                return;
            string tableName = imagelistBoxTables.SelectedItem.ToString();
            var table = _tables.FirstOrDefault(v => (v as IObjectClass).AliasName == tableName);
            if (table != null)
            {
                if (_attributeForm == null || _attributeForm.IsDisposed)
                    _attributeForm = new AttributeDevForm();
                else
                    _attributeForm.Activate();//之前已打开，则给予焦点，置顶。
                if (!_attributeForm.Visible)
                    _attributeForm.Show(this);

                _attributeForm.LoadAttribute(table);
            }
        }
        private void 移除表格ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tableName = imagelistBoxTables.SelectedItem.ToString();
            RemoveTable(tableName);
        }
        private void imagelistBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enable = imagelistBoxTables.SelectedIndex > -1;
            打开表格属性表TToolStripMenuItem.Enabled = enable;
            移除表格ToolStripMenuItem.Enabled = enable;
        }
        private void imagelistBoxTables_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                imagelistBoxTables.SelectedIndex = imagelistBoxTables.IndexFromPoint(e.Location);
        }
        private void imagelistBoxTables_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && imagelistBoxTables.SelectedIndex > -1)
                cMenuStripTableList.Show(imagelistBoxTables, e.Location);
        }
        #endregion

        #region 地图导航条事件

        private void bubbleBarNavigationBtn_Click(object sender, EventArgs e)//主地图地图导航条：Click
        {
            #region 方法一：利用arcgis的command
            CommonLib.MapMouseFlag = 1;

            SimpleButton button = sender as SimpleButton;
            ICommand command = null;
            switch (button.Name)
            {
                case "bubbleButtonFullExtent":  //全屏
                    command = new ControlsMapFullExtentCommand();
                    break;
                case "bubbleButtonZoomIn":      //放大
                    command = new ControlsMapZoomInTool();
                    break;
                case "bubbleButtonZoomOut":     //缩小
                    command = new ControlsMapZoomOutTool();
                    break;
                case "bubbleButtonPan":         //平移
                    command = new ControlsMapPanTool();
                    break;
                case "bubbleButtonPreView":     //上一视图
                    command = new ControlsMapZoomToLastExtentBackCommand();
                    break;
                case "bubbleButtonNextView":    //下一视图
                    command = new ControlsMapZoomToLastExtentForwardCommand();
                    break;
                case "bubbleButtonLenMeasure":  //距离测量
                    //command = new ESRI.ArcGIS.Controls.ControlsMapMeasureTool();
                    axMapControlMainMap.CurrentTool = null;
                    CommonLib.MapMouseFlag = 8;
                    if (_measureTool == null)
                    {
                        _measureTool = new MeasureTools();
                        _measureTool.Map = axMapControlMainMap;
                    }
                    break;
                case "bubbleButtonAreaMeasure": //面积测量
                    //command = new ESRI.ArcGIS.Controls.ControlsMapMeasureTool();
                    axMapControlMainMap.CurrentTool = null;
                    CommonLib.MapMouseFlag = 9;
                    if (_measureTool == null)
                    {
                        _measureTool = new MeasureTools();
                        _measureTool.Map = axMapControlMainMap;
                    }
                    break;
                case "bubbleButtonIdentify":    //属性查看
                    command = new ControlsMapIdentifyTool();
                    axMapControlMainMap.CurrentTool = null;
                    CommonLib.MapMouseFlag = 10;
                    break;
                case "bubbleButtonSwipe":    //卷帘工具
                    _effectLayer.SwipeLayer = _toccSelectedLayer;
                    command = new ControlsMapSwipeToolClass();
                    axMapControlMainMap.CurrentTool = null;
                    CommonLib.MapMouseFlag = 10;
                    break;
                case "bubbleButtonSelection":  //选择工具
                    _effectLayer.SwipeLayer = _toccSelectedLayer;
                    command = new ControlsSelectFeaturesToolClass();
                    axMapControlMainMap.CurrentTool = null;
                    CommonLib.MapMouseFlag = 10;
                    break;
                default:
                    break;
            }
            if (command != null)
            {
                command.OnCreate(axMapControlMainMap.Object);
                if (command is ITool)
                    axMapControlMainMap.CurrentTool = command as ITool;
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

        private void pageLayoutNavigationBtn_Click(object sender, EventArgs e) //布局地图导航条：Click
        {
            CommonLib.MapMouseFlag = 1;

            SimpleButton button = sender as SimpleButton;
            ICommand command = null;
            switch (button.Name)
            {
                case "fullExtentPageLayout":  //全屏
                    command = new ControlsPageZoomWholePageCommand();
                    break;
                case "zoomInPageLayout":      //放大
                    command = new ControlsPageZoomInTool();
                    break;
                case "zoomOutPageLayout":     //缩小
                    command = new ControlsPageZoomOutTool();
                    break;
                case "panPageLayout":         //平移
                    command = new ControlsPagePanTool();
                    break;
                case "preViewPageLayout":     //上一视图
                    command = new ControlsPageZoomPageToLastExtentBackCommand();
                    break;
                case "nextViewPageLayout":    //下一视图
                    command = new ControlsPageZoomPageToLastExtentForwardCommand();
                    break;
                default:
                    break;
            }
            if (command != null)
            {
                command.OnCreate(axPageLayoutControl.Object);
                if (command is ITool)
                    axPageLayoutControl.CurrentTool = command as ITool;
                else
                    command.OnClick();
            }
        }

        private void bubbleBarNavigationBtn_MouseMove(object sender, MouseEventArgs e) //主地图导航条、布局视图导航条：MouseMove（状态栏显示ToolTip）
        {
            SimpleButton button = sender as SimpleButton;
            barSITips.Caption = button.ToolTip;
        }
        #endregion


        private void measureResultClose_Click(object sender, EventArgs e)//测量结果清空、关闭
        {
            mapMouseToDefault();
            measureResultPanel.Visible = false;
            measureResultLabel.Text = "";
            axMapControlMainMap.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void copyMapToPageLayout()//主地图控件中的地图复制到布局控件（axPageLayoutControl1）中
        {
            object copyFromMap = axMapControlMainMap.Map;
            object copyToMap = axPageLayoutControl.ActiveView.FocusMap;
            IObjectCopy pObjectCopy = new ObjectCopyClass();
            pObjectCopy.Overwrite(pObjectCopy.Copy(copyFromMap), copyToMap);
        }

        private void mapMouseToDefault()//地图鼠标状态恢复到平移状态
        {
            CommonLib.MapMouseFlag = 1;
            ICommand command = new ControlsMapPanTool();
            if (command != null)
            {
                command.OnCreate(axMapControlMainMap.Object);
                //if (command is ITool)
                axMapControlMainMap.CurrentTool = command as ITool;
                //else
                //    command.OnClick();
            }
        }
    }
}
