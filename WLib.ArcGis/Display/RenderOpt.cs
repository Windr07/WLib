/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using stdole;

namespace WLib.ArcGis.Display
{
    /// <summary>
    /// 图层渲染操作
    /// </summary>
    public static class RenderOpt
    {
        #region 设置图层样式（Renderer）
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
                    symbol = (ISymbol)SymbolCreate.GetSimpleFillSymbol(mainColor, outlineColor, widthOrSize);
                    break;
                case esriGeometryType.esriGeometryPoint:
                case esriGeometryType.esriGeometryMultipoint:
                    symbol = (ISymbol)SymbolCreate.GetSimpleMarkerSymbol(mainColor, outlineColor, widthOrSize);
                    break;
                case esriGeometryType.esriGeometryLine:
                case esriGeometryType.esriGeometryPolyline:
                    symbol = (ISymbol)SymbolCreate.GetSimpleLineSymbol(mainColor, widthOrSize);
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
            IColor lineColor = outlineColorStr == null ? ColorCreate.GetIColor(128, 138, 135) : ColorCreate.GetIColor(outlineColorStr);
            SetLayerRenderer(geoLayer, ColorCreate.GetIColor(mainColorStr), lineColor, transparency, widthOrSize);
        }
        #endregion


       
    }
}
