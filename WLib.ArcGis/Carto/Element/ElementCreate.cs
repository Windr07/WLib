/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/3
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using stdole;
using System;
using WLib.ArcGis.Display;

namespace WLib.ArcGis.Carto.Element
{
    /** Element元素分类，请参考：
     *  (1)<see cref="ElementQuery"/>.cs的注释部分
     *  (2)官方文档：https://desktop.arcgis.com/en/arcobjects/10.4/net/webframe.htm#IElement.htm
     */

    /// <summary>
    /// 提供创建元素的方法
    /// </summary>
    public static class ElementCreate
    {
        /// <summary>
        /// 在图形容器中创建元素
        /// </summary>
        /// <param name="graphicsContainer">图形容器，新建的元素将添加到该图形容器中</param>
        /// <param name="geometry">几何图形</param>
        /// <param name="eType">元素类型（点、线、面、文本等）</param>
        /// <returns></returns>
        public static IElement CreateElement(this IGraphicsContainer graphicsContainer, IGeometry geometry, EDrawElementType eType)
        {
            IElement element = null;
            switch (eType)
            {
                case EDrawElementType.Point:
                    if (!(geometry is IPoint)) throw new ArgumentException($"参数{nameof(geometry)}不是点({typeof(IPoint)})，无法由此创建点元素");
                    element = new MarkerElementClass();
                    break;
                case EDrawElementType.Polyline:
                    if (!(geometry is IPolyline)) throw new ArgumentException($"参数{nameof(geometry)}不是折线({typeof(IPolyline)})，无法由此创建线元素");
                    element = new LineElementClass();
                    break;
                case EDrawElementType.Polygon:
                    if (!(geometry is IPolygon)) throw new ArgumentException($"参数{nameof(geometry)}不是多边形({typeof(IPolygon)})，无法由此创建多边形元素");
                    element = new PolygonElementClass();
                    break;
                case EDrawElementType.Circle:
                    if (!(geometry is IPolygon)) throw new ArgumentException($"参数{nameof(geometry)}不是多边形({typeof(IPolygon)})，无法由此创建圆形元素");
                    element = new PolygonElementClass();
                    break;
                case EDrawElementType.Rectangle:
                    if (!(geometry is IEnvelope)) throw new ArgumentException($"参数{nameof(geometry)}不是矩形框({typeof(IEnvelope)})，无法由此创建矩形元素");
                    element = new RectangleElementClass();
                    break;
                case EDrawElementType.Text:
                    if (!(geometry is IPoint)) throw new ArgumentException($"参数{nameof(geometry)}不是点({typeof(IPoint)})，无法由此创建文本元素");
                    element = new TextElementClass();
                    break;
            }
            element.Geometry = geometry;
            graphicsContainer.AddElement(element, 0);
            return element;
        }


        /// <summary>
        /// 在图形容器中创建点元素
        /// </summary>
        /// <param name="graphicsContainer">图形容器，新建的元素将添加到该图形容器中</param>
        /// <param name="point">点图形，用于创建点元素</param>
        /// <returns></returns>
        public static IMarkerElement CreatePointElement(this IGraphicsContainer graphicsContainer, IPoint point)
        {
            IMarkerElement markerElement = new MarkerElementClass();
            IElement element = (IElement)markerElement;
            element.Geometry = point;

            graphicsContainer.AddElement(element, 0);
            return markerElement;
        }
        /// <summary>
        /// 在图形容器中创建线元素
        /// </summary>
        /// <param name="graphicsContainer">图形容器，新建的元素将添加到该图形容器中</param>
        /// <param name="polyline">线图形，用于创建线元素</param>
        /// <returns></returns>
        public static ILineElement CreatePolylineElement(this IGraphicsContainer graphicsContainer, IPolyline polyline)
        {
            ILineElement lineElement = new LineElementClass();
            IElement element = (IElement)lineElement;
            element.Geometry = polyline;

            graphicsContainer.AddElement(element, 0);
            return lineElement;
        }
        /// <summary>
        /// 在图形容器中创建多边形元素
        /// </summary>
        /// <param name="graphicsContainer">图形容器，新建的元素将添加到该图形容器中</param>
        /// <param name="polygon">多边形图形，用于创建多边形元素</param>
        /// <returns></returns>
        public static IFillShapeElement CreatePolygonElement(this IGraphicsContainer graphicsContainer, IPolygon polygon)
        {
            IFillShapeElement polygonElement = new PolygonElementClass();
            IElement element = (IElement)polygonElement;
            element.Geometry = polygon;

            graphicsContainer.AddElement(element, 0);
            return polygonElement;
        }
        /// <summary>
        /// 在图形容器中创建圆形元素
        /// </summary>
        /// <param name="graphicsContainer">图形容器，新建的元素将添加到该图形容器中</param>
        /// <param name="circle">圆形，用于创建圆形元素</param>
        /// <returns></returns>
        public static IFillShapeElement CreateCircleElement(this IGraphicsContainer graphicsContainer, IGeometry circle)
        {
            IFillShapeElement circleElement = new CircleElementClass();
            IElement element = (IElement)circleElement;
            element.Geometry = circle;

            graphicsContainer.AddElement(element, 0);
            return circleElement;
        }
        /// <summary>
        /// 在图形容器中创建矩形元素
        /// </summary>
        /// <param name="graphicsContainer">图形容器，新建的元素将添加到该图形容器中</param>
        /// <param name="envelope">矩形，用于创建矩形元素</param>
        /// <returns></returns>
        public static IFillShapeElement CreateRectangleElement(this IGraphicsContainer graphicsContainer, IEnvelope envelope)
        {
            IFillShapeElement rectangleEmenet = new RectangleElementClass();
            IElement element = (IElement)rectangleEmenet;
            element.Geometry = envelope;

            graphicsContainer.AddElement(element, 0);
            return rectangleEmenet;
        }

        /// <summary>
        /// 在图形容器中创建文本元素
        /// </summary>
        /// <param name="graphicsContainer">图形容器，新建的元素将添加到该图形容器中</param>
        /// <param name="point">点图形，用于创建文本元素，标示文本元素的位置</param>
        /// <param name="text">文本元素显示的文本</param>
        /// <param name="fontName">文本的字体</param>
        /// <param name="fontSize">文本的字体大小</param>
        /// <param name="color">文本的颜色，6位颜色值RRGGBB，如"ff0000"为红色；
        /// 或8位颜色值RRGGBBTT，如"ff0000ff"为红色不透明(最后两位00表示透明，ff表示不透明)</param>
        /// <returns></returns>
        public static ITextElement CreateTextElement(this IGraphicsContainer graphicsContainer, IPoint point,
            string text = "文本1", string fontName = "宋体", int fontSize = 10, string color = "000000")
        {
            ITextElement textElement = new TextElementClass();
            textElement.Symbol = SymbolCreate.GetTextSymbol(color, fontName, fontSize);
            textElement.Text = text;

            IElement element = (IElement)textElement;
            element.Geometry = point;

            graphicsContainer.AddElement(element, 0);
            return textElement;
        }
    }
}
