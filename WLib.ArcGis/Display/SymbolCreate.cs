/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/3/28 16:51:15
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Display;
using stdole;

namespace WLib.ArcGis.Display
{
    /// <summary>
    /// 提供创建符号(Symbol)的方法
    /// </summary>
    public static class SymbolCreate
    {
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
            markerSymbol.OutlineColor = outlineColor ?? ColorCreate.GetIColor(128, 138, 135);
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
            return GetSimpleMarkerSymbol(ColorCreate.GetIColor(rrggbbtt), ColorCreate.GetIColor(outlineColorRrggbbtt), pointSize, markerStyle);
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
            simpleLineSymbol.Color = color ?? ColorCreate.GetIColor(128, 138, 135);
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
            return GetSimpleLineSymbol(ColorCreate.GetIColor(rrggbbtt), lineWidth, style);
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
            return GetSimpleFillSymbol(ColorCreate.GetIColor(fillColorRrggbbtt), ColorCreate.GetIColor(lineColorRrggbbtt), lineWidth, style);
        }


        /// <summary>
        /// 获取文本符号ITextSymbol
        /// </summary>
        /// <param name="colorRrggbbtt">文本颜色，6位颜色值RRGGBB，如"ff0000"为红色；
        /// 或8位颜色值RRGGBBTT，如"ff0000ff"为红色不透明(最后两位00表示透明，ff表示不透明)</param>
        /// <param name="fontName"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public static ITextSymbol GetTextSymbol(string colorRrggbbtt, string fontName, double fontSize)
        {
            ITextSymbol textSymbol = new TextSymbolClass();
            textSymbol.Font = new StdFontClass { Name = fontName } as IFontDisp;
            textSymbol.Size = fontSize;
            textSymbol.Color = ColorCreate.GetIColor(colorRrggbbtt);
            return textSymbol;
        }
    }
}
