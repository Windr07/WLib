/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/3
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using WLib.ArcGis.Carto.Element;
using WLib.ArcGis.Display;

namespace WLib.ArcGis.Control.MapAssociation
{
    /// <summary>
    /// 地图控件绘制点、线、面、圆、矩形、文本等元素的操作
    /// </summary>
    public class MapCtrlDrawElement : IMapCtrlAssociation
    {
        /// <summary>
        /// 地图控件
        /// </summary>
        public AxMapControl MapControl { get; }
        /// <summary>
        /// 在地图中绘制的元素类别
        /// </summary>
        public EDrawElementType DrawElementType { get; set; }

        /// <summary>
        /// 填充样式，表示所绘制的多边形、圆形、矩形元素的样式
        /// </summary>
        public IFillSymbol FillSymbol { get; set; }
        /// <summary>
        /// 线条样式，表示所绘制的线条元素的样式
        /// </summary>
        public ILineSymbol LineSymbol { get; set; }
        /// <summary>
        /// 标记样式，表示所绘制的点元素的样式
        /// </summary>
        public IMarkerSymbol MarkerSymbol { get; set; }
        /// <summary>
        /// 文本样式，表示所绘制的文本元素的样式
        /// </summary>
        public ITextSymbol TextSymbol { get; set; }


        /// <summary>
        /// 地图控件绘制点、线、面元素操作
        /// </summary>
        /// <param name="mapControl"></param>
        public MapCtrlDrawElement(AxMapControl mapControl)
        {
            MapControl = mapControl;
            DrawElementType = EDrawElementType.None;
            MapControl.OnMouseDown += MapControl_OnMouseDown;
            FillSymbol = new SimpleFillSymbolClass(); //RenderOpt.GetSimpleFillSymbol("ff000000", "ff0000ff");
             LineSymbol = new SimpleLineSymbolClass();//RenderOpt.GetSimpleLineSymbol("ff000000");
            MarkerSymbol = new SimpleMarkerSymbolClass();//RenderOpt.GetSimpleMarkerSymbol("ff0000ff");
            TextSymbol = new TextSymbolClass();
        }


        private void MapControl_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (DrawElementType == EDrawElementType.None || e.button != 1)
                return;

            MapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            IGraphicsContainer graphicsContainer = (IGraphicsContainer)MapControl.Map;

            #region 绘制元素
            switch (DrawElementType)
            {
                case EDrawElementType.Point:
                    var markerElement = graphicsContainer.CreatePointElement(MapControl.ToMapPoint(e.x, e.y));
                    markerElement.Symbol = MarkerSymbol;
                    break;
                case EDrawElementType.Polyline:
                    var lineElement = graphicsContainer.CreatePolylineElement((IPolyline)MapControl.TrackLine());
                    lineElement.Symbol = LineSymbol;
                    break;
                case EDrawElementType.Polygon:
                    var polygonElement = graphicsContainer.CreatePolygonElement((IPolygon)MapControl.TrackPolygon());
                    polygonElement.Symbol = FillSymbol;
                    break;
                case EDrawElementType.Circle:
                    var circleElement = graphicsContainer.CreateCircleElement(MapControl.TrackCircle());
                    circleElement.Symbol = FillSymbol;
                    break;
                case EDrawElementType.Rectangle:
                    var rectangleElement = graphicsContainer.CreateRectangleElement(MapControl.TrackRectangle());
                    rectangleElement.Symbol = FillSymbol;
                    break;
                case EDrawElementType.Text:
                    var textElement = graphicsContainer.CreateTextElement(MapControl.ToMapPoint(e.x, e.y));
                    textElement.Symbol = TextSymbol;
                    break;
            }
            #endregion

            ((IActiveView)graphicsContainer).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
    }
}
