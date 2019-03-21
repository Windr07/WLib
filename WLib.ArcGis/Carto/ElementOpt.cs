/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using ESRI.ArcGIS.Carto;

namespace WLib.ArcGis.Carto
{
    //Element包括:
    //1、Graphic element（图形元素)：地图上的点、线、面等元素
    //2、Frame element（框架元素)
    //  （1）MapFrame（地图数据框）
    //  （2）MapSurroundFrame：指北针、比例尺、图例等

    /// <summary>
    /// 获取元素的操作
    /// </summary>
    public static class ElementOpt
    {
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
            List<IElement> elementList = new List<IElement>();
            graphicsContainer.Reset();
            IElement element;
            while ((element = graphicsContainer.Next()) != null)
            {
                if ((element as IGroupElement) != null)
                    elementList.AddRange(GetElements(element as IGroupElement));
                else
                    elementList.Add(element);
            }
            return elementList;
        }
        /// <summary>
        /// 查找符合指定名称的所有元素
        /// </summary>
        /// <param name="graphicsContainer">图形容器</param>
        /// <param name="elementName">元素名称</param>
        /// <returns></returns>
        public static List<IElement> GetElementsByName(this IGraphicsContainer graphicsContainer, string elementName)
        {
            List<IElement> elementList = new List<IElement>();
            graphicsContainer.Reset();
            IElement element;
            while ((element = graphicsContainer.Next()) != null)
            {
                if ((element as IGroupElement) != null)
                    elementList.AddRange(GetElementsByName(element as IGroupElement, elementName));

                else if ((element as IElementProperties)?.Name == elementName)
                    elementList.Add(element);
            }
            return elementList;
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
    }
}
