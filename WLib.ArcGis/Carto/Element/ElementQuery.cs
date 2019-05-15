/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using ESRI.ArcGIS.Carto;

namespace WLib.ArcGis.Carto.Element
{
    /** 元素分类（以下只是元素分类，而不表示接口的继承关系）
     *  (1)图形元素GraphicElement（点、线、面、文本等元素）
     *  (2)框架元素FrameElement（大部分框架元素实现类也继承图形元素接口IGraphicElement）
     *      ①地图数据框  ②指北针、比例尺、图例等
     * Element:（https://desktop.arcgis.com/en/arcobjects/10.4/net/webframe.htm#IElement.htm）
     * (1)GraphicElement（https://desktop.arcgis.com/en/arcobjects/10.4/net/webframe.htm#IGraphicElement.htm）
     *      MarkerElement							显示标记的图形元素
     *      LineElement								显示线条的图形元素
     *      PolygonElement							显示多边形的图形元素
     *      RectangleElement						显示矩形的图形元素
     *      CircleElement							显示圆圈的图形元素
     *      EllipseElement							显示椭圆的图形元素
     *      MultiPatchElement						
     *      DataGraphTElement(esriCartoUI)			用于在ArcMap布局视图上显示和操作数据图形的图形元素的容器
     *      DisplacementLinkElement(esriEditorExt)	显示调整链接的图形元素
     *      GeoEllipseElement(esriDefenseSolutions)	显示GeoEllipses的图形元素
     *      GeoPolygonElement(esriDefenseSolutions)	用于显示GeoPolygons的图形元素
     *      GeoPolylineElement(esriDefenseSolutions)用于显示GeoPolylines的图形元素
     *      IdentityLinkElement(esriEditorExt)		显示身份链接的图形元素
     *      InkGraphic								墨迹图形对象
     *      MoleGroupElement(esriDefenseSolutions)	
     *      ParagraphTextElement					
     *      TextElement							    用于显示文本的图形元素
     *      PMFTitleTextElement						用于显示动态PMF标题的图形元素
     *      Text3DElement							用于显示3D文本图形元素
     * (2)FrameElement
     *      MapFrame                                用于显示地图的图形元素
     *      MapSurroundFrame                        用于显示地图要素的图形元素（管理MapSurround，与MapSurround一一对应）
     *          MapSurround                         地图环绕（指北针、比例尺、图例等）
     *              Legend                          图例
     *              NorthArrow                      指北针
     *              MapInset                        插图（缩略图）
     *              ScaleBar                        比例尺
     *              ScaleText                       比例尺文本
     *              OverView
     *                  MapTitle                    地图标题
     *      OleFrame(esriArcMapUI)		            OLE框架元素
     *      TableFrame(esriEditorExt)	            显示表格的图形元素
     *      GroupElement				            组图形元素，包含一组图形元素
     *      TemporalChartElement(esriTrackingAnalystUI)	控制时间图表的元素
     *      PictureElement				            图片图形元素
     *          BmpPictureElement			        显示BMP图片的图形元素
     *          EmfPictureElement			        显示Emf图片的图形元素
     *          GifPictureElement			        显示GIF图片的图形元素
     *          Jp2PictureElement			        显示JPEG2000图片的图形元素
     *          JpgPictureElement			        显示JPG图片的图形元素
     *          PngPictureElement			        显示PNG图片的图形元素
     *          TifPictureElement			        显示TIF图片的图形元素
     */

    /// <summary>
    /// 提供从图形容器或组元素获取元素的方法
    /// </summary>
    public static class ElementQuery
    {
        #region 获取单个元素
        /// <summary>
        /// 根据名称查找并返回第一个组合元素
        /// </summary>
        /// <param name="graphicsContainer">图形容器</param>
        /// <param name="elementName">组合元素名称</param>
        /// <returns></returns>
        public static IElement GetFirstGroupElement(this IGraphicsContainer graphicsContainer, string elementName)
        {
            graphicsContainer.Reset();
            IElement element;
            while ((element = graphicsContainer.Next()) != null)
            {
                if ((element as IGroupElement) == null) continue;
                if ((element as IElementProperties)?.Name == elementName)
                    break;
            }
            return element;
        }
        /// <summary>
        /// 根据名称查找并返回第一个元素，被查找的元素不是组合元素
        /// </summary>
        /// <param name="graphicsContainer">图形容器</param>
        /// <param name="elementName">元素名称</param>
        /// <returns></returns>
        public static IElement GetFirstElementByName(this IGraphicsContainer graphicsContainer, string elementName)
        {
            graphicsContainer.Reset();
            IElement element;
            while ((element = graphicsContainer.Next()) != null)
            {
                var tmpElementName = (element as IElementProperties)?.Name;
                if ((element as IGroupElement) != null)
                {
                    element = GetFirstElementByName(element as IGroupElement, elementName);
                    if (element != null && tmpElementName == elementName)
                        break;
                }
                else if (tmpElementName == elementName)
                    break;
            }
            return element;
        }
        /// <summary>
        /// 从组合元素中查找元素，被查找的元素不是组合元素
        /// </summary>
        /// <param name="groupElement"></param>
        /// <param name="elementName"></param>
        /// <returns></returns>
        public static IElement GetFirstElementByName(this IGroupElement groupElement, string elementName)
        {
            IElement resultElement = null;
            IEnumElement enumEle = groupElement.Elements;
            IElement tmpEle;
            while ((tmpEle = enumEle.Next()) != null)
            {
                if ((tmpEle as IGroupElement) != null)
                {
                    resultElement = GetFirstElementByName(tmpEle as IGroupElement, elementName);
                    if (resultElement != null) break;
                }
                else
                {
                    if ((tmpEle as IElementProperties)?.Name == elementName)
                    {
                        resultElement = tmpEle;
                        break;
                    }
                }
            }
            return resultElement;
        }
        #endregion


        #region 获取元素列表
        /// <summary>
        /// 从组合元素中获取所有子元素（包括所有层级的子元素，但不包括组合元素本身）
        /// </summary>
        /// <param name="groupElement">组合元素</param>
        /// <returns></returns>
        public static List<IElement> GetElements(this IGroupElement groupElement)
        {
            List<IElement> elementList = new List<IElement>();
            IEnumElement enumEle = groupElement.Elements;
            IElement tmpEle;
            while ((tmpEle = enumEle.Next()) != null)
            {
                if ((tmpEle as IGroupElement) != null)
                    elementList.AddRange(GetElements(tmpEle as IGroupElement));
                else
                    elementList.Add(tmpEle);
            }
            return elementList;
        }
        /// <summary>
        ///获取所有元素（包括组合元素的子元素，但不包括组合元素本身）
        /// </summary>
        /// <param name="graphicsContainer">图形容器</param>
        /// <returns></returns>
        public static List<IElement> GetElements(this IGraphicsContainer graphicsContainer)
        {
            List<IElement> elements = new List<IElement>();
            graphicsContainer.Reset();
            IElement element;
            while ((element = graphicsContainer.Next()) != null)
            {
                if ((element as IGroupElement) != null)
                    elements.AddRange(GetElements(element as IGroupElement));
                else
                    elements.Add(element);
            }
            return elements;
        }
        /// <summary>
        /// 查找符合指定名称的所有元素
        /// </summary>
        /// <param name="graphicsContainer">图形容器</param>
        /// <param name="elementName">元素名称</param>
        /// <returns></returns>
        public static List<IElement> GetElementsByName(this IGraphicsContainer graphicsContainer, string elementName)
        {
            List<IElement> elements = new List<IElement>();
            graphicsContainer.Reset();
            IElement element;
            while ((element = graphicsContainer.Next()) != null)
            {
                if ((element as IGroupElement) != null)
                    elements.AddRange(GetElementsByName(element as IGroupElement, elementName));

                else if ((element as IElementProperties)?.Name == elementName)
                    elements.Add(element);
            }
            return elements;
        }
        /// <summary>
        /// 查找包含指定关键字（部分元素名或元素文本）的所有元素
        /// </summary>
        /// <param name="graphicsContainer">图形容器</param>
        /// <param name="keyword">元素关键字，即部分元素名或元素文本</param>
        /// <returns></returns>
        public static List<IElement> GetElementsByKeyword(this IGraphicsContainer graphicsContainer, string keyword)
        {
            var elements = new List<IElement>();
            graphicsContainer.Reset();
            IElement element;
            while ((element = graphicsContainer.Next()) != null)
            {
                if (element is IGroupElement groupElement)
                    elements.AddRange(GetElementsByKeyword(groupElement, keyword));

                else if (element is IElementProperties elementProperties && elementProperties.Name.Contains(keyword))
                    elements.Add(element);

                else if (element is ITextElement textElement && textElement.Text.Contains(keyword))
                    elements.Add(element);
            }
            return elements;
        }
        /// <summary>
        /// 查找符合指定名称的所有元素
        /// </summary>
        /// <param name="groupElement">组合元素</param>
        /// <param name="elementName">元素名称</param>
        /// <returns></returns>
        public static List<IElement> GetElementsByName(this IGroupElement groupElement, string elementName)
        {
            List<IElement> elements = new List<IElement>();
            IEnumElement enumEle = groupElement.Elements;
            IElement tmpEle;
            while ((tmpEle = enumEle.Next()) != null)
            {
                if ((tmpEle as IGroupElement) != null)
                    elements.AddRange(GetElementsByName(tmpEle as IGroupElement, elementName));

                else if ((tmpEle as IElementProperties)?.Name == elementName)
                    elements.Add(tmpEle);
            }
            return elements;
        }
        /// <summary>
        /// 查找包含指定关键字（部分元素名或元素文本）的所有元素
        /// </summary>
        /// <param name="groupElement">组合元素</param>
        /// <param name="keyword">元素关键字，即部分元素名或元素文本</param>
        /// <returns></returns>
        public static List<IElement> GetElementsByKeyword(this IGroupElement groupElement, string keyword)
        {
            var elements = new List<IElement>();
            IEnumElement enumElement = groupElement.Elements;
            IElement element;
            while ((element = enumElement.Next()) != null)
            {
                if (element is IGroupElement tmpGroupElement)
                    elements.AddRange(GetElementsByKeyword(tmpGroupElement, keyword));

                else if (element is IElementProperties elementProperties && elementProperties.Name.Contains(keyword))
                    elements.Add(element);

                else if (element is ITextElement textElement && textElement.Text.Contains(keyword))
                    elements.Add(element);
            }
            return elements;
        }
        /// <summary>
        /// 查找多个名称的元素，返回名称与对应元素的键值对
        /// </summary>
        /// <param name="graphicsContainer">图形容器</param>
        /// <param name="elementNames">元素名称数组，注意不能有相同的元素名称</param>
        /// <returns></returns>
        public static Dictionary<string, List<IElement>> GetElementsByNames(this IGraphicsContainer graphicsContainer, params string[] elementNames)
        {
            Dictionary<string, List<IElement>> dict = elementNames.ToDictionary(name => name, name => new List<IElement>());
            graphicsContainer.Reset();
            IElement element;
            while ((element = graphicsContainer.Next()) != null)
            {
                string curEleName = (element as IElementProperties)?.Name;
                if (curEleName != null && dict.ContainsKey(curEleName))
                    dict[curEleName].Add(element);
            }
            return dict;
        }
        #endregion


        #region 获取文本元素列表
        /// <summary>
        ///获取所有文本
        /// </summary>
        /// <param name="groupElement"></param>
        /// <returns></returns>
        public static List<ITextElement> GetTextElements(this IGroupElement groupElement)
        {
            List<ITextElement> txtElementList = new List<ITextElement>();
            IEnumElement enumEle = groupElement.Elements;
            IElement tmpEle;
            while ((tmpEle = enumEle.Next()) != null)
            {
                if ((tmpEle as IGroupElement) != null)
                    txtElementList.AddRange(GetTextElements(tmpEle as IGroupElement));
                else if ((tmpEle as ITextElement) != null)
                    txtElementList.Add(tmpEle as ITextElement);
            }
            return txtElementList;
        }
        /// <summary>
        ///获取所有文本
        /// </summary>
        /// <param name="graphicsContainer">图形容器</param>
        /// <returns></returns>
        public static List<ITextElement> GetTextElements(this IGraphicsContainer graphicsContainer)
        {
            List<ITextElement> txtElementList = new List<ITextElement>();
            graphicsContainer.Reset();
            IElement element;
            while ((element = graphicsContainer.Next()) != null)
            {
                if ((element as IGroupElement) != null)
                    txtElementList.AddRange(GetTextElements(element as IGroupElement));
                else if ((element as ITextElement) != null)
                    txtElementList.Add(element as ITextElement);
            }
            return txtElementList;
        }
        #endregion


        #region 获取地图框元素
        /// <summary>
        /// 获取所有地图数据框
        /// </summary>
        /// <param name="graphicsContainer">图形容器</param>
        /// <returns></returns>
        public static List<IMapFrame> GetMapFrames(this IGraphicsContainer graphicsContainer)
        {
            List<IMapFrame> mapFrames = new List<IMapFrame>();
            graphicsContainer.Reset();
            for (IElement element = graphicsContainer.Next(); element != null; element = graphicsContainer.Next())
            {
                if (element is IMapFrame frame)
                    mapFrames.Add(frame);
                if (element is IGroupElement groupElement)
                {
                    var mapFrameElements = GetElements(groupElement).Where(v => v is IMapFrame);
                    if (mapFrameElements.Any())
                    {
                        mapFrames.AddRange(mapFrameElements.Cast<IMapFrame>().ToArray());
                    }
                }
            }
            return mapFrames;
        }
        /// <summary>
        /// 查找第一个符合指定名称的地图数据框
        /// </summary>
        /// <param name="graphicsContainer">图形容器</param>
        /// <param name="mapName">地图(数据框)名称</param>
        /// <returns></returns>
        public static IMapFrame GetMapFrame(this IGraphicsContainer graphicsContainer, string mapName)
        {
            IMapFrame mapFrame = null;
            graphicsContainer.Reset();
            for (IElement element = graphicsContainer.Next(); element != null; element = graphicsContainer.Next())
            {
                if (element is IGroupElement)
                    element = GetFirstElementByName(element as IGroupElement, mapName);

                if (element is IMapFrame frame)
                {
                    if (frame.Map.Name == mapName)
                    {
                        mapFrame = frame;
                        break;
                    }
                }
            }
            return mapFrame;
        }
        /// <summary>
        /// 查找地图所属的地图数据框
        /// </summary>
        /// <param name="graphicsContainer">图形容器</param>
        /// <param name="map">地图</param>
        /// <returns></returns>
        public static IMapFrame GetMapFrame(this IGraphicsContainer graphicsContainer, IMap map)
        {
            return graphicsContainer.FindFrame(map) as IMapFrame;
        }
        #endregion
    }
}
