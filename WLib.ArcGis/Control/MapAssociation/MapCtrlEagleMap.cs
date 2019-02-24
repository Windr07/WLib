using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using WLib.ArcGis.Display;

namespace WLib.ArcGis.Control.MapAssociation
{
    /// <summary>
    /// 鹰眼图和主地图的关联操作
    /// </summary>
    public class MapCtrlEagleMap
    {
        /// <summary>
        /// 主地图控件
        /// </summary>
        public AxMapControl MainMapControl { get; }
        /// <summary>
        /// 鹰眼图控件
        /// </summary>
        public AxMapControl EagleMapControl { get; }
        /// <summary>
        /// 鹰眼图和主地图的关联操作
        /// </summary>
        /// <param name="mainMapControl"></param>
        /// <param name="eagleMapControl"></param>
        public MapCtrlEagleMap(AxMapControl mainMapControl, AxMapControl eagleMapControl)
        {
            MainMapControl = mainMapControl;
            MainMapControl.OnMapReplaced += MainMapControl_OnMapReplaced;
            MainMapControl.OnExtentUpdated += MainMapControl_OnExtentUpdated;

            EagleMapControl = eagleMapControl;
            EagleMapControl.OnMouseDown += EagleMapControl_OnMouseDown;
            EagleMapControl.OnMouseUp += EagleMapControl_OnMouseUp;
            EagleMapControl.OnMouseMove += EagleMapControl_OnMouseMove;
        }


        /// <summary>
        /// 更新鹰眼图及布局控件的地图
        /// </summary>
        public void UpDateEagleMap()
        {
            EagleMapControl.Map = new Map();
            for (int i = 0; i < MainMapControl.LayerCount; i++)//添加主地图控件中的所有图层到鹰眼控件中
            {
                EagleMapControl.AddLayer(MainMapControl.get_Layer(MainMapControl.LayerCount - (i + 1)));
            }
        }


        private void MainMapControl_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)//主地图：范围变化，鹰眼图跟随变化
        {
            //在绘制前，清除axMapControlEagleMap中的任何图形元素
            var graphicsContainer = (IGraphicsContainer)EagleMapControl.Map;
            graphicsContainer.DeleteAllElements();

            //主地图地图范围变化时，鹰眼中的地图范围也跟随着变化
            var rectangleElement = (IRectangleElement)new RectangleElement();
            var element = (IElement)rectangleElement;
            var envelope = (IEnvelope)e.newEnvelope;  //得到主地图的新范围
            element.Geometry = envelope;

            //设置鹰眼中的红线框
            var fillShapeElement = (IFillShapeElement)element;
            fillShapeElement.Symbol = RenderOpt.GetSimpleFillSymbol("ff000000", "ff0000", 1.6);
            graphicsContainer.AddElement((IElement)fillShapeElement, 0);

            //刷新 
            envelope.Expand(2, 2, true);
            var activeView = (IActiveView)graphicsContainer;
            activeView.Extent = envelope;
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void MainMapControl_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)//主地图：更换后鹰眼地图也相应更新
        {
            UpDateEagleMap();
        }

        private void EagleMapControl_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)//鹰眼图：左键拖动矩形框、右键绘制矩形框
        {
            if (EagleMapControl.Map.LayerCount == 0) return;
            if (e.button == 1) //按下鼠标左键拖动矩形框
            {
                IPoint point = new Point();
                point.PutCoords(e.mapX, e.mapY);
                IEnvelope envelope = MainMapControl.Extent;
                envelope.CenterAt(point);
                MainMapControl.Extent = envelope;
                MainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            else if (e.button == 2)//按下鼠标右键绘制矩形框
            {
                IEnvelope envelope = EagleMapControl.TrackRectangle();
                IEnvelope extent = MainMapControl.ActiveView.Extent;

                //计算新显示框范围
                double newWidth;
                double newHeight;
                if (envelope.IsEmpty)
                    return;
                if (envelope.Width / envelope.Height > extent.Width / extent.Height)
                {//宽相同
                    newWidth = envelope.Width;
                    newHeight = envelope.Width * extent.Height / extent.Width;
                }
                else//高相同 
                {
                    newHeight = envelope.Height;
                    newWidth = envelope.Height * extent.Width / extent.Height;
                }

                double midX = (envelope.XMin + envelope.XMax) / 2;
                double midY = (envelope.YMin + envelope.YMax) / 2;
                double xmi = midX - newWidth / 2;
                double xma = midX + newWidth / 2;
                double ymi = midY - newHeight / 2;
                double yma = midY + newHeight / 2;
                envelope.PutCoords(xmi, ymi, xma, yma);

                MainMapControl.Extent = envelope;
                MainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
        }

        private void EagleMapControl_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)//鹰眼图：回复鼠标指针状态
        {
            EagleMapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
        }

        private void EagleMapControl_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e) //鹰眼图：左键拖动矩形框
        {
            // 如果不是左键按下就直接返回
            if (e.button != 1) return;
            EagleMapControl.MousePointer = esriControlsMousePointer.esriPointerSizeAll;
            IPoint point = new PointClass();
            point.PutCoords(e.mapX, e.mapY);
            MainMapControl.CenterAt(point);
            EagleMapControl.CenterAt(point);
            MainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            EagleMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }
    }
}
