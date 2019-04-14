/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Drawing;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using stdole;

namespace WLib.ArcGis.Display
{
    /// <summary>
    /// 提供设置图层渲染的方法
    /// </summary>
    public static class RenderCreate
    {
        #region 设置图层简单渲染（SimpleRenderer）
        /// <summary>
        /// 用指定填充颜色和边线颜色渲染图层
        /// </summary>
        /// <param name="geoLayer">图层</param>
        /// <param name="mainColor">主颜色，即面图层的填充颜色，线图层的线条颜色，点图层的符号内部颜色</param>
        /// <param name="outlineColor">面或点的边线颜色，若为null，则设置边线颜色为RGB：128, 138, 135</param>
        /// <param name="transparency">图层的透明度，0为不透明，100为全透明</param>
        /// <param name="widthOrSize">面/线图层的线宽，或点图层点的大小</param>
        public static void SetSimpleRenderer(this IGeoFeatureLayer geoLayer, IColor mainColor, IColor outlineColor = null, short transparency = 0, double widthOrSize = 1)
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
            geoLayer.Renderer = new SimpleRendererClass { Symbol = symbol };

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
        public static void SetSimpleRenderer(this IGeoFeatureLayer geoLayer, string mainColorStr, string outlineColorStr = null, short transparency = 0, double widthOrSize = 1)
        {
            IColor lineColor = outlineColorStr == null ? ColorCreate.GetIColor(128, 138, 135) : ColorCreate.GetIColor(outlineColorStr);
            SetSimpleRenderer(geoLayer, ColorCreate.GetIColor(mainColorStr), lineColor, transparency, widthOrSize);
        }
        #endregion


        #region 待完善
        /// <summary>
        /// ClassBreakRender着色法：根据数字字段的值分组，每一个分组使用一个符号
        /// </summary>
        /// <param name="geoFeatureLayer">操作图层</param>
        /// <param name="fieldName">操作字段名</param>
        /// <param name="breakCount"></param>
        /// <param name="lineColor"></param>
        public static void SetClassBreakRender(IGeoFeatureLayer geoFeatureLayer, string fieldName, int breakCount, IColor lineColor)
        {
            IClassBreaksRenderer CBRender = new ClassBreaksRendererClass();

            //获取该字段的最大值、最小值
            double max = ComputeFieldValue(geoFeatureLayer, fieldName, 1);//1表示获取最大值
            double min = ComputeFieldValue(geoFeatureLayer, fieldName, 3);//3表示获取最小值

            //设置分级数，字段
            CBRender.MinimumBreak = min;
            CBRender.Field = fieldName;
            CBRender.BreakCount = breakCount;

            //新建边线符号
            ILineSymbol lineSymbol = new SimpleLineSymbolClass();
            lineSymbol.Color = lineColor;
            lineSymbol.Width = 1;

            //设置每一级，分段范围，符号
            for (int i = 0; i < breakCount; i++)
            {   
                ISimpleFillSymbol fillSymbol = new SimpleFillSymbolClass();
                fillSymbol.Outline = lineSymbol;
                fillSymbol.Color = ColorCreate.GetIColor(0, 250 / breakCount * (breakCount - i), 0);
                CBRender.set_Break(i, (max - min) * (i + 1) / breakCount + min);
                CBRender.set_Symbol(i, (ISymbol)fillSymbol);

            }
            geoFeatureLayer.Renderer = (IFeatureRenderer)CBRender;
        }
        /// <summary>
        /// ChartRenderer着色法：根据数字字段的值配置图表，每一个图表使用一个符号
        /// </summary>
        /// <param name="geoFeatureLayer"></param>
        /// <param name="fieldName1"></param>
        /// <param name="fieldName2"></param>
        /// <param name="fillColor1"></param>
        /// <param name="fillColor2"></param>
        public static void SetBarCharRender(IGeoFeatureLayer geoFeatureLayer, string fieldName1, string fieldName2, IColor fillColor1, IColor fillColor2)
        {
            //创建柱状符号
            IBarChartSymbol barChartSymbol = new BarChartSymbolClass();
            barChartSymbol.Width = 12;
            //获取符号接口
            IChartSymbol chartSymbol = (IChartSymbol)barChartSymbol;
            IMarkerSymbol markerSymbol = (IMarkerSymbol)barChartSymbol;
            //获取两个字段的最大值
            double max, max1, max2;
            max1 = ComputeFieldValue(geoFeatureLayer, fieldName1, 1);
            max2 = ComputeFieldValue(geoFeatureLayer, fieldName2, 1);
            if (max2 > max1)
                max = max2;
            else
                max = max1;

            //设置ChartSymbol的最大值，以及符号尺寸最大值（像素单位）
            chartSymbol.MaxValue = max;
            markerSymbol.Size = 60;

            //依据柱状符号，获得符号数组接口，
            ISymbolArray symbolArray = (ISymbolArray)barChartSymbol;
            IFillSymbol fillSymbol;

            //设置第一个柱状图的符号
            fillSymbol = new SimpleFillSymbol();
            fillSymbol.Color = fillColor1;
            symbolArray.AddSymbol((ISymbol)fillSymbol);
            //设置第二个柱状图的符号
            fillSymbol = new SimpleFillSymbolClass();
            fillSymbol.Color = fillColor2;
            symbolArray.AddSymbol((ISymbol)fillSymbol);

            //创建ChartRenderer接口
            IChartRenderer chartRenderer = new ChartRendererClass();
            //设置ChartRenderer中的字段，依据这两个字段的数据值，创建柱状图
            IRendererFields rendererFields = chartRenderer as IRendererFields;
            rendererFields.AddField(fieldName1, fieldName1);
            rendererFields.AddField(fieldName2, fieldName2);

            //将pBarChartSymbol 赋值给pChartRenderer的属性
            chartRenderer.ChartSymbol = (IChartSymbol)barChartSymbol;
            //设置图层的背景颜色       
            fillSymbol = new SimpleFillSymbolClass();
            fillSymbol.Color = ColorCreate.GetIColor(239, 228, 190);
            chartRenderer.BaseSymbol = (ISymbol)fillSymbol;
            //设置 ChartRenderer的其他属性
            chartRenderer.UseOverposter = false;
            chartRenderer.CreateLegend();
            // 创建符号图例
            chartRenderer.Label = "Population by Gender ";
            geoFeatureLayer.Renderer = chartRenderer as IFeatureRenderer;

        }
        /// <summary>
        /// UniqueValueRenderer着色法：根据不同的唯一值用一个符号来显示要素
        /// </summary>
        /// <param name="geoFeatureLayer"></param>
        /// <param name="fieldName"></param>
        public static void SetUniqueValueRender(IGeoFeatureLayer geoFeatureLayer, string fieldName)
        {
            IUniqueValueRenderer unqueValueR = new UniqueValueRendererClass();
            ITable pTable = (ITable)geoFeatureLayer;
            int fieldIndex = pTable.FindField(fieldName);

            unqueValueR.FieldCount = 1;
            unqueValueR.set_Field(0, fieldName);
            IRandomColorRamp pColorRamp = new RandomColorRampClass();
            pColorRamp.StartHue = 0;
            pColorRamp.MinValue = 99;

            pColorRamp.MinSaturation = 15;
            pColorRamp.EndHue = 360;
            pColorRamp.MaxValue = 100;
            pColorRamp.MaxSaturation = 30;
            pColorRamp.Size = 100;
            bool ok = true;
            pColorRamp.CreateRamp(out ok);
            IEnumColors pEnumRamp = pColorRamp.Colors;
            IQueryFilter pQueryFilter = new QueryFilterClass();
            pQueryFilter.AddField(fieldName);
            ICursor pCursor = pTable.Search(pQueryFilter, true);
            IRow pNextRow = pCursor.NextRow();

            while (pNextRow != null)
            {
                IRowBuffer pNextRowBuffer = pNextRow;
                string codeValue = pNextRowBuffer.get_Value(fieldIndex).ToString();
                var pNextUniqueColor = pEnumRamp.Next();
                if (pNextUniqueColor == null)
                {
                    pEnumRamp.Reset();
                    pNextUniqueColor = pEnumRamp.Next();
                }
                IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
                pFillSymbol.Color = pNextUniqueColor;
                unqueValueR.AddValue(codeValue, fieldName, (ISymbol)pFillSymbol);
                pNextRow = pCursor.NextRow();
                geoFeatureLayer.Renderer = (IFeatureRenderer)unqueValueR;
            }
        }
        /// <summary>
        /// 获得字段统计值，state参数：0-获取值的数量，1-获取最大值，2-获取平均值，3-获取最小值，4-获得和值(sum)
        /// </summary>
        /// <param name="geoFeatureLayer">统计的图层</param>
        /// <param name="fieldName">统计的字段</param>
        /// <param name="state">获得的统计量的类型，0-获取值的数量，1-获取最大值，2-获取平均值，3-获取最小值，4-获得和值(sum)</param>
        /// <returns></returns>
        public static double ComputeFieldValue(IGeoFeatureLayer geoFeatureLayer, string fieldName, int state)
        {
            ITable table = (ITable)geoFeatureLayer;
            ICursor cursor = table.Search(null, true);

            IDataStatistics dataStaitcs = new DataStatisticsClass { Cursor = cursor, Field = fieldName };
            IStatisticsResults statisticsResults = dataStaitcs.Statistics;

            double Value = 0;
            if (state == 0)
                Value = statisticsResults.Count;
            else if (state == 1)
                Value = statisticsResults.Maximum;
            else if (state == 2)
                Value = statisticsResults.Mean;
            else if (state == 3)
                Value = statisticsResults.Minimum;
            else if (state == 4)
                Value = statisticsResults.Sum;
            return Value;
        }
        #endregion
    }
}
