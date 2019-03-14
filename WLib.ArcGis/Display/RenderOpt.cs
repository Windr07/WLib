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
        /// <param name="outlineColor">面或点的边线颜色，若为null，则设置边线颜色为RGB：128, 138, 135</param>
        /// <param name="transparency">图层的透明度，0为不透明，100为全透明</param>
        /// <param name="widthOrSize">面/线图层的线宽，或点图层点的大小</param>
        public static void SetLayerRenderer(this IGeoFeatureLayer geoLayer, IColor mainColor, IColor outlineColor = null, short transparency = 0, double widthOrSize = 1)
        {
            ISymbol symbol = null;
            switch (geoLayer.FeatureClass.ShapeType)
            {
                case esriGeometryType.esriGeometryPolygon:
                    symbol = (ISymbol)GetSimpleFillSymbol(mainColor, outlineColor, widthOrSize);
                    break;
                case esriGeometryType.esriGeometryPoint:
                case esriGeometryType.esriGeometryMultipoint:
                    symbol = (ISymbol)GetSimpleMarkerSymbol(mainColor, outlineColor, widthOrSize);
                    break;
                case esriGeometryType.esriGeometryLine:
                case esriGeometryType.esriGeometryPolyline:
                    symbol = (ISymbol)GetSimpleLineSymbol(mainColor, widthOrSize);
                    break;
            }
            ISimpleRenderer simpleRenderer = new SimpleRendererClass { Symbol = symbol };
            geoLayer.Renderer = (IFeatureRenderer)simpleRenderer;

            ILayerEffects layerEffects = (ILayerEffects)geoLayer;
            layerEffects.Transparency = transparency;
        }
        /// <summary>
        ///  用指定填充颜色字符串RRGGBB渲染图层，使用默认的边线颜色（灰色),可设置透明度
        /// </summary>
        /// <param name="geoLayer">图层</param>
        /// <param name="mainColorStr">主颜色字符串RRGGBB,如"ff0000"为红色，主颜色即多边形图层的填充颜色，线图层的线条颜色，点图层的符号颜色</param>
        /// <param name="outlineColorStr">面或点的边线颜色，若为null，则设置边线颜色为RGB：128, 138, 135</param>
        /// <param name="transparency">图层的透明度，0为不透明，100为全透明</param>
        /// <param name="widthOrSize">面/线图层的线宽，或点图层点的大小</param>
        public static void SetLayerRenderer(this IGeoFeatureLayer geoLayer, string mainColorStr, string outlineColorStr = null, short transparency = 0, double widthOrSize = 1)
        {
            IColor lineColor = outlineColorStr == null ? GetIColor(128, 138, 135) : GetIColor(outlineColorStr);
            SetLayerRenderer(geoLayer, GetIColor(mainColorStr), lineColor, transparency, widthOrSize);
        }
        #endregion


        #region 获取IColor
        /// <summary>
        /// 获得颜色IColor
        /// </summary>
        /// <param name="red">红色颜色分量，取值范围为0-255</param>
        /// <param name="green">绿色颜色分量，取值范围为0-255</param>
        /// <param name="blue">蓝色颜色分量，取值范围为0-255</param>
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


        #region 创建点、线、面的简单样式（Symbol）
        /// <summary>
        /// 获取简单点符号SimpleMarkerSymbol
        /// </summary>
        /// <param name="color">点内部颜色</param>
        /// <param name="outlineColor">点符号的边线</param>
        /// <param name="pointSize">点样式的大小</param>
        /// <param name="markerStyle">设置符号样式：默认为菱形形状</param>
        /// <returns></returns>
        public static ISimpleMarkerSymbol GetSimpleMarkerSymbol(IColor color, IColor outlineColor = null,
            double pointSize = 6, esriSimpleMarkerStyle markerStyle = esriSimpleMarkerStyle.esriSMSDiamond)
        {
            ISimpleMarkerSymbol markerSymbol = new SimpleMarkerSymbolClass();
            markerSymbol.Style = markerStyle;
            markerSymbol.Color = color;
            markerSymbol.Size = pointSize;
            markerSymbol.Outline = true;
            markerSymbol.OutlineColor = outlineColor ?? GetIColor(128, 138, 135);
            markerSymbol.OutlineSize = 1;
            return markerSymbol;
        }
        /// <summary>
        /// 获取简单点符号SimpleMarkerSymbol
        /// </summary>
        /// <param name="rrggbbtt">点符号的颜色，6位颜色值RRGGBB，如"ff0000"为红色；
        /// 或8位颜色值RRGGBBTT，如"ff0000ff"为红色不透明(最后两位00表示透明，ff表示不透明)</param>
        /// <param name="outlineColorRrggbbtt">点符号的边线颜色，6位颜色值RRGGBB，如"ff0000"为红色；
        /// 或8位颜色值RRGGBBTT，如"ff0000ff"为红色不透明(最后两位00表示透明，ff表示不透明)</param>
        /// <param name="pointSize">点的大小</param>
        /// <param name="markerStyle">设置符号样式：默认为菱形形状</param>
        /// <returns></returns>
        public static ISimpleMarkerSymbol GetSimpleMarkerSymbol(string rrggbbtt, string outlineColorRrggbbtt = null,
            double pointSize = 6, esriSimpleMarkerStyle markerStyle = esriSimpleMarkerStyle.esriSMSDiamond)
        {
            return GetSimpleMarkerSymbol(GetIColor(rrggbbtt), GetIColor(outlineColorRrggbbtt), pointSize, markerStyle);
        }
        /// <summary>
        /// 获取简单的线符号SimpleLineSymbol
        /// </summary>
        /// <param name="color">线条的颜色</param>
        /// <param name="lineWidth">线条的宽度</param>
        /// <param name="style">线条样式，默认为实线</param>
        /// <returns></returns>
        public static ISimpleLineSymbol GetSimpleLineSymbol(IColor color, double lineWidth = 1,
            esriSimpleLineStyle style = esriSimpleLineStyle.esriSLSSolid)
        {
            ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
            simpleLineSymbol.Color = color ?? GetIColor(128, 138, 135);
            simpleLineSymbol.Width = lineWidth;       //设置线宽
            simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;//设置线型
            return simpleLineSymbol;
        }
        /// <summary>
        /// 获取简单的线符号SimpleLineSymbol
        /// </summary>
        /// <param name="rrggbbtt">线条的颜色字符串RRGGBB,如"ff0000"为红色</param>
        /// <param name="lineWidth">线条的宽度</param>
        /// <param name="style">线条样式，默认为实线</param>
        /// <returns></returns>
        public static ISimpleLineSymbol GetSimpleLineSymbol(string rrggbbtt, double lineWidth = 1,
            esriSimpleLineStyle style = esriSimpleLineStyle.esriSLSSolid)
        {
            return GetSimpleLineSymbol(GetIColor(rrggbbtt), lineWidth, style);
        }
        /// <summary>
        /// 获取简单的填充符号ISimpleFillSymbol
        /// </summary>
        /// <param name="fillColor">填充色</param>
        /// <param name="lineColor">边线色</param>
        /// <param name="lineWidth">边线的宽度</param>
        /// <param name="style">填充样式，默认为实心填充</param>
        /// <returns></returns>
        public static ISimpleFillSymbol GetSimpleFillSymbol(IColor fillColor, IColor lineColor = null,
            double lineWidth = 1, esriSimpleFillStyle style = esriSimpleFillStyle.esriSFSSolid)
        {
            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol();
            simpleFillSymbol.Outline = GetSimpleLineSymbol(lineColor, lineWidth); //外边线
            simpleFillSymbol.Color = fillColor;     //设置填充色
            simpleFillSymbol.Style = style;  //设置填充方式
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
        public static ISimpleFillSymbol GetSimpleFillSymbol(string fillColorRrggbbtt, string lineColorRrggbbtt, double lineWidth = 1, esriSimpleFillStyle style = esriSimpleFillStyle.esriSFSSolid)
        {
            return GetSimpleFillSymbol(GetIColor(fillColorRrggbbtt), GetIColor(lineColorRrggbbtt), lineWidth, style);
        }
        #endregion
    }
}
