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
    public class RenderOpt
    {
        /// <summary>
        /// 线宽，作用于此类中的方法
        /// </summary>
        public static double LineWidth = 1;
        /// <summary>
        /// 点的大小，作用于此类中的方法
        /// </summary>
        public static double PointSize = 6;


        #region 设置图层样式
        /// <summary>
        /// 用指定填充颜色和边线颜色渲染图层
        /// </summary>
        /// <param name="geoLayer">图层</param>
        /// <param name="mainColor">主颜色，即多边形图层的填充颜色，线图层的线条颜色，点图层的符号内部颜色</param>
        /// <param name="lineColor">边线颜色</param>
        /// <param name="transparency">透明度，0为不透明，100为全透明</param>
        public static void SetLayerRenderer(IGeoFeatureLayer geoLayer, IColor mainColor, IColor lineColor, short transparency)
        {
            switch (geoLayer.FeatureClass.ShapeType)
            {
                case esriGeometryType.esriGeometryPolygon:
                    IFillSymbol fillSymbol = new SimpleFillSymbolClass();
                    fillSymbol.Color = mainColor;
                    ILineSymbol pLineSymbol = new SimpleLineSymbolClass();
                    pLineSymbol.Color = lineColor;
                    pLineSymbol.Width = LineWidth;
                    fillSymbol.Outline = pLineSymbol;
                    ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
                    pSimpleRenderer.Symbol = (ISymbol)fillSymbol;
                    geoLayer.Renderer = pSimpleRenderer as IFeatureRenderer;
                    break;
                case esriGeometryType.esriGeometryPoint:
                case esriGeometryType.esriGeometryMultipoint:
                    ISimpleMarkerSymbol markerSymbol = new SimpleMarkerSymbolClass();
                    markerSymbol.Style = esriSimpleMarkerStyle.esriSMSDiamond;
                    markerSymbol.Color = mainColor;
                    markerSymbol.Size = PointSize;
                    markerSymbol.Outline = true;
                    markerSymbol.OutlineColor = lineColor;
                    markerSymbol.OutlineSize = 1;
                    ISimpleRenderer renderer = new SimpleRendererClass();
                    renderer.Symbol = (ISymbol)markerSymbol;
                    geoLayer.Renderer = renderer as IFeatureRenderer;
                    break;
                case esriGeometryType.esriGeometryLine:
                case esriGeometryType.esriGeometryPolyline:
                    ISimpleLineSymbol lineSymbol = new SimpleLineSymbolClass();
                    lineSymbol.Color = mainColor;
                    lineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
                    lineSymbol.Width = LineWidth;
                    ISimpleRenderer lineRenderer = new SimpleRendererClass();
                    lineRenderer.Symbol = (ISymbol)lineSymbol;
                    geoLayer.Renderer = lineRenderer as IFeatureRenderer;
                    break;
            }
            ILayerEffects layerEffects = geoLayer as ILayerEffects;
            layerEffects.Transparency = transparency;
        }
        /// <summary>
        /// 用指定填充颜色渲染图层，使用默认的边线颜色（灰色）,可设置透明度
        /// </summary>
        /// <param name="geoLayer">图层</param>
        /// <param name="mainColor">主颜色，即多边形图层的填充颜色，线图层的线条颜色，点图层的符号颜色</param>
        /// <param name="transparency">透明度，0为不透明，100为全透明</param>
        public static void SetLayerRenderer(IGeoFeatureLayer geoLayer, IColor mainColor, short transparency)
        {
            SetLayerRenderer(geoLayer, mainColor, GetIColor(128, 138, 135), transparency);
        }
        /// <summary>
        ///  用指定填充颜色渲染图层，使用默认的边线颜色（灰色）
        /// </summary>
        /// <param name="geoLayer">图层</param>
        /// <param name="mainColor">填充颜色</param>
        public static void SetLayerRenderer(IGeoFeatureLayer geoLayer, IColor mainColor)
        {
            SetLayerRenderer(geoLayer, mainColor, GetIColor(128, 138, 135), 0);
        }
        /// <summary>
        ///  用指定填充颜色字符串RRGGBB渲染图层，使用默认的边线颜色（灰色）
        /// </summary>
        /// <param name="geoLayer">图层</param>
        /// <param name="mainColorStr">主颜色字符串RRGGBB,如"ff0000"为红色，主颜色即多边形图层的填充颜色，线图层的线条颜色，点图层的符号颜色</param>
        public static void SetLayerRenderer(IGeoFeatureLayer geoLayer, string mainColorStr)
        {
            IColor fillColor = GetIColor(mainColorStr);
            SetLayerRenderer(geoLayer, fillColor, GetIColor(128, 138, 135), 0);
        }
        /// <summary>
        ///  用指定填充颜色字符串RRGGBB渲染图层，使用默认的边线颜色（灰色),可设置透明度
        /// </summary>
        /// <param name="geoLayer">图层</param>
        /// <param name="mainColorStr">主颜色字符串RRGGBB,如"ff0000"为红色，主颜色即多边形图层的填充颜色，线图层的线条颜色，点图层的符号颜色</param>
        /// <param name="transparency">透明度，0为不透明，100为全透明</param>
        public static void SetLayerRenderer(IGeoFeatureLayer geoLayer, string mainColorStr, short transparency)
        {
            IColor fillColor = GetIColor(mainColorStr);
            SetLayerRenderer(geoLayer, fillColor, GetIColor(128, 138, 135), transparency);
        }
        #endregion


        #region 获取IColor
        /// <summary>
        /// 获得颜色IColor
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="transparency">透明度</param>
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
        /// <param name="rrggbb">颜色值RRGGBB，如"ff0000"为红色</param>
        /// <returns></returns>
        public static IColor GetIColor(string rrggbb)
        {
            IRgbColor rgbColor = new RgbColor();
            rgbColor.Red = int.Parse(rrggbb.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            rgbColor.Green = int.Parse(rrggbb.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            rgbColor.Blue = int.Parse(rrggbb.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            return rgbColor;
        }
        #endregion


        #region 获取各类Symbol
        /// <summary>
        /// 获取ISimpleMarkerSymbol
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static ISimpleMarkerSymbol GetSimpleMarkerSymbol(IColor color)
        {
            ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
            simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSDiamond;   //设置符号类型：钻石
            simpleMarkerSymbol.Color = color;
            simpleMarkerSymbol.Size = PointSize;       //设置大小
            return simpleMarkerSymbol;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rrggbb"></param>
        /// <returns></returns>
        public static ISimpleMarkerSymbol GetSimpleMarkerSymbol(string rrggbb)
        {
            return GetSimpleMarkerSymbol(GetIColor(rrggbb));
        }
        /// <summary>
        /// 获取ISimpleLineSymbol
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static ISimpleLineSymbol GetSimpleLineSymbol(IColor color)
        {
            ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
            simpleLineSymbol.Color = color;
            simpleLineSymbol.Width = LineWidth;       //设置线宽
            simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDashDot;//设置线型
            return simpleLineSymbol;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rrggbb"></param>
        /// <returns></returns>
        public static ISimpleLineSymbol GetSimpleLineSymbol(string rrggbb)
        {
            return GetSimpleLineSymbol(GetIColor(rrggbb));
        }
        /// <summary>
        /// 获取ISimpleFillSymbol
        /// </summary>
        /// <param name="fillColor">填充色</param>
        /// <param name="lineColor">边线色</param>
        /// <returns></returns>
        public static ISimpleFillSymbol GetSimpleFillSymbol(IColor fillColor, IColor lineColor)
        {
            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol();
            simpleFillSymbol.Outline = GetSimpleLineSymbol(lineColor); //外边线
            simpleFillSymbol.Color = fillColor;     //设置填充色
            simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;  //设置填充方式
            return simpleFillSymbol;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fillColorRrggbb"></param>
        /// <param name="lineColorRrggbb"></param>
        /// <returns></returns>
        public static ISimpleFillSymbol GetSimpleFillSymbol(string fillColorRrggbb, string lineColorRrggbb)
        {
            return GetSimpleFillSymbol(GetIColor(fillColorRrggbb), GetIColor(lineColorRrggbb));
        }
        #endregion

    }
}
