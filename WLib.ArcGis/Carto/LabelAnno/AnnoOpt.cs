/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/12 11:13:29
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using stdole;
using WLib.ArcGis.Carto.Element;
using WLib.ArcGis.Carto.Map;

namespace WLib.ArcGis.Carto.LabelAnno
{
    /// <summary>
    /// 提供设置注记的方法
    /// </summary>
    public static class AnnoOpt
    {
        /// <summary>
        /// 设置存储在地图上的注记的字体和大小
        /// </summary>
        /// <param name="map"></param>
        /// <param name="graphicsLayerName"></param>
        /// <param name="fontName">注记字体（此值为""、空白字符或null，则不改变注记字体）</param>
        /// <param name="size">注记大小（此值小于等于0，则不改变注记大小）</param>
        public static void SetAnnotationFontOnMap(this IMap map, string graphicsLayerName, string fontName = null, int size = 0)
        {
            IGraphicsLayer graphicsLayer = map.GetGraphicsLayer(graphicsLayerName);
            IGraphicsContainer graphicContainer = graphicsLayer as IGraphicsContainer;
            var txtElements = graphicContainer.GetTextElements();
            foreach (var txtElement in txtElements)
            {
                ITextSymbol txtSymbol = txtElement.Symbol;
                //注意不能使用下面这一句，直接设置txtElement.Symbol.Size = size 是无效的，原因未知
                //txtElement.Symbol.Size = size;

                if (size > 0)
                    txtSymbol.Size = size;

                if (!string.IsNullOrEmpty(fontName) && fontName.Trim() != string.Empty)
                {
                    System.Drawing.Font font = new System.Drawing.Font(fontName, (float)txtSymbol.Size);
                    IFontDisp fontDisp = ESRI.ArcGIS.ADF.COMSupport.OLE.GetIFontDispFromFont(font) as IFontDisp;
                    txtSymbol.Font = fontDisp;
                }
                txtElement.Symbol = txtSymbol;
                graphicContainer.UpdateElement(txtElement as IElement);
            }
        }
    }
}
