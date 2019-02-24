/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace WLib.ArcGis.Display
{
    /// <summary>
    /// 图层渲染操作
    /// </summary>
    public static class RenderOpt
    {
        #region 设置图层样式
        /// <summary>
        /// 用指定填充颜色和边线颜色渲染图层
        /// </summary>
        /// <param name="geoLayer">图层</param>
        /// <param name="mainColor">主颜色，即面图层的填充颜色，线图层的线条颜色，点图层的符号内部颜色</param>
        /// <param name="lineColor">面或点的边线颜色，若为null，则设置边线颜色为RGB：128, 138, 135</param>
        /// <param name="transparency">图层的透明度，0为不透明，100为全透明</param>
        /// <param name="size">面/线图层的线宽，或点图层点的大小</param>
        public static void SetLayerRenderer(this IGeoFeatureLayer geoLayer, IColor mainColor, IColor lineColor = null, short transparency = 0, double size = 1)
        {
            switch (geoLayer.FeatureClass.ShapeType)
            {
                case esriGeometryType.esriGeometryPolygon:
                    IFillSymbol fillSymbol = new SimpleFillSymbolClass();
                    fillSymbol.Color = mainColor;
                    ILineSymbol pLineSymbol = new SimpleLineSymbolClass();
                    pLineSymbol.Color = lineColor ?? GetIColor(128, 138, 135);
                    pLineSymbol.Width = size;
                    fillSymbol.Outline = pLineSymbol;
                    ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
                    pSimpleRenderer.Symbol = (ISymbol)fillSymbol;
                    geoLayer.Renderer = (IFeatureRenderer)pSimpleRenderer;
                    break;
                case esriGeometryType.esriGeometryPoint:
                case esriGeometryType.esriGeometryMultipoint:
                    ISimpleMarkerSymbol markerSymbol = new SimpleMarkerSymbolClass();
                    markerSymbol.Style = esriSimpleMarkerStyle.esriSMSDiamond;
                    markerSymbol.Color = mainColor;
                    markerSymbol.Size = size;
                    markerSymbol.Outline = true;
                    markerSymbol.OutlineColor = lineColor ?? GetIColor(128, 138, 135);
                    markerSymbol.OutlineSize = 1;
                    ISimpleRenderer renderer = new SimpleRendererClass();
                    renderer.Symbol = (ISymbol)markerSymbol;
                    geoLayer.Renderer = (IFeatureRenderer)renderer;
                    break;
                case esriGeometryType.esriGeometryLine:
                case esriGeometryType.esriGeometryPolyline:
                    ISimpleLineSymbol lineSymbol = new SimpleLineSymbolClass();
                    lineSymbol.Color = mainColor;
                    lineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
                    lineSymbol.Width = size;
                    ISimpleRenderer lineRenderer = new SimpleRendererClass();
                    lineRenderer.Symbol = (ISymbol)lineSymbol;
                    geoLayer.Renderer = (IFeatureRenderer)lineRenderer;
                    break;
            }
            ILayerEffects layerEffects = geoLayer as ILayerEffects;
            layerEffects.Transparency = transparency;
        }

        /// <summary>
        ///  用指定填充颜色字符串RRGGBB渲染图层，使用默认的边线颜色（灰色),可设置透明度
        /// </summary>
        /// <param name="geoLayer">图层</param>
        /// <param name="mainColorStr">主颜色字符串RRGGBB,如"ff0000"为红色，主颜色即多边形图层的填充颜色，线图层的线条颜色，点图层的符号颜色</param>
        /// <param name="lineColorStr">面或点的边线颜色，若为null，则设置边线颜色为RGB：128, 138, 135</param>
        /// <param name="transparency">图层的透明度，0为不透明，100为全透明</param>
        /// <param name="size"></param>
        public static void SetLayerRenderer(this IGeoFeatureLayer geoLayer, string mainColorStr, string lineColorStr = null, short transparency = 0, double size = 1)
        {
            IColor lineColor = lineColorStr == null ? GetIColor(128, 138, 135) : GetIColor(lineColorStr);
            SetLayerRenderer(geoLayer, GetIColor(mainColorStr), lineColor, transparency, size);
        }
        #endregion


        #region 获取IColor
        /// <summary>
        /// 获得颜色IColor
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="transparency">色彩透明度（0透明 - 255不透明）</param>
        /// <returns></returns>
        public static IColor GetIColor(int red, int green, int blue, byte transparency = 255)
        {
            IRgbColor rgbColor = new RgbColorClass();
            rgbColor.Red = red;
            rgbColor.Green = green;
            rgbColor.Blue = blue;
            rgbColor.Transparency = transparency;
            return rgbColor;
        }
        /// <summary>
        /// 获得颜色IColor
        /// </summary>
        /// <param name="rrggbbtt">6位颜色值RRGGBB，如"ff0000"为红色；
        /// 或8位颜色值RRGGBBTT，如"ff0000ff"为红色不透明(最后两位00表示透明，ff表示不透明)</param>
        /// <returns></returns>
        public static IColor GetIColor(string rrggbbtt)
        {
            IRgbColor rgbColor = new RgbColor();
            rgbColor.Red = int.Parse(rrggbbtt.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            rgbColor.Green = int.Parse(rrggbbtt.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            rgbColor.Blue = int.Parse(rrggbbtt.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            if (rrggbbtt.Length == 8)
                rgbColor.Transparency = byte.Parse(rrggbbtt.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);

            return rgbColor;
        }
        #endregion


        #region 获取各类Symbol
        /// <summary>
        /// 获取简单点符号SimpleMarkerSymbol
        /// </summary>
        /// <param name="color">点内部颜色</param>
        /// <param name="pointSize">点样式的大小</param>
        /// <returns></returns>
        public static ISimpleMarkerSymbol GetSimpleMarkerSymbol(IColor color, double pointSize = 6)
        {
            ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
            simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSDiamond;   //设置符号类型：钻石
            simpleMarkerSymbol.Color = color;
            simpleMarkerSymbol.Size = pointSize;       //设置大小
            return simpleMarkerSymbol;
        }
        /// <summary>
        /// 获取简单点符号SimpleMarkerSymbol
        /// </summary>
        /// <param name="rrggbb">点内部颜色字符串RRGGBB,如"ff0000"为红色</param>
        /// <param name="pointSize">点的大小</param>
        /// <returns></returns>
        public static ISimpleMarkerSymbol GetSimpleMarkerSymbol(string rrggbb, double pointSize = 6)
        {
            return GetSimpleMarkerSymbol(GetIColor(rrggbb), pointSize);
        }
        /// <summary>
        /// 获取简单的线符号SimpleLineSymbol
        /// </summary>
        /// <param name="color">线条的颜色</param>
        /// <param name="lineWidth">线条的宽度</param>
        /// <returns></returns>
        public static ISimpleLineSymbol GetSimpleLineSymbol(IColor color, double lineWidth = 1)
        {
            ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
            simpleLineSymbol.Color = color;
            simpleLineSymbol.Width = lineWidth;       //设置线宽
            simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDashDot;//设置线型
            return simpleLineSymbol;
        }
        /// <summary>
        /// 获取简单的线符号SimpleLineSymbol
        /// </summary>
        /// <param name="rrggbb">线条的颜色字符串RRGGBB,如"ff0000"为红色</param>
        /// <param name="lineWidth">线条的宽度</param>
        /// <returns></returns>
        public static ISimpleLineSymbol GetSimpleLineSymbol(string rrggbb, double lineWidth = 1)
        {
            return GetSimpleLineSymbol(GetIColor(rrggbb), lineWidth);
        }
        /// <summary>
        /// 获取简单的填充符号ISimpleFillSymbol
        /// </summary>
        /// <param name="fillColor">填充色</param>
        /// <param name="lineColor">边线色</param>
        /// <param name="lineWidth">边线的宽度</param>
        /// <returns></returns>
        public static ISimpleFillSymbol GetSimpleFillSymbol(IColor fillColor, IColor lineColor, double lineWidth = 1)
        {
            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol();
            simpleFillSymbol.Outline = GetSimpleLineSymbol(lineColor, lineWidth); //外边线
            simpleFillSymbol.Color = fillColor;     //设置填充色
            simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;  //设置填充方式
            return simpleFillSymbol;
        }
        /// <summary>
        /// 获取简单的填充符号ISimpleFillSymbol
        /// </summary>
        /// <param name="fillColorRrggbbtt">填充色，6位颜色值RRGGBB，如"ff0000"为红色；
        /// 或8位颜色值RRGGBBTT，如"ff0000ff"为红色不透明(最后两位00表示透明，ff表示不透明)</param>
        /// <param name="lineColorRrggbbtt">边线色，6位颜色值RRGGBB，如"ff0000"为红色；
        /// 或8位颜色值RRGGBBTT，如"ff0000ff"为红色不透明(最后两位00表示透明，ff表示不透明)</param>
        /// <param name="lineWidth">边线的宽度</param>
        /// <returns></returns>
        public static ISimpleFillSymbol GetSimpleFillSymbol(string fillColorRrggbbtt, string lineColorRrggbbtt, double lineWidth = 1)
        {
            return GetSimpleFillSymbol(GetIColor(fillColorRrggbbtt), GetIColor(lineColorRrggbbtt), lineWidth);
        }
        #endregion
    }
}
