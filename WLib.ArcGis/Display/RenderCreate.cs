/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using WLib.ArcGis.Data;

namespace WLib.ArcGis.Display
{
    /// <summary>
    /// 提供设置图层或数据的渲染（Renderer）的方法
    /// </summary>
    public static class RenderCreate
    {
        #region 矢量数据渲染
        /// <summary>
        /// 要素图层简单渲染：用指定填充颜色和边线颜色渲染图层
        /// </summary>
        /// <param name="geoLayer">图层</param>
        /// <param name="mainColor">主颜色，即面图层的填充颜色，线图层的线条颜色，点图层的符号内部颜色</param>
        /// <param name="outlineColor">面或点的边线颜色，若为null，则设置边线颜色为RGB：128, 138, 135</param>
        /// <param name="transparency">图层的透明度，0为不透明，100为全透明</param>
        /// <param name="widthOrSize">面/线图层的线宽，或点图层点的大小</param>
        public static IFeatureRenderer SimpleRenderer(this IGeoFeatureLayer geoLayer, IColor mainColor, IColor outlineColor = null, short transparency = 0, double widthOrSize = 1)
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

            ILayerEffects layerEffects = (ILayerEffects)geoLayer;
            layerEffects.Transparency = transparency;

            return geoLayer.Renderer = new SimpleRendererClass { Symbol = symbol };
        }
        /// <summary>
        ///  要素图层简单渲染：用指定填充颜色字符串RRGGBB渲染图层，使用默认的边线颜色（灰色),可设置透明度
        /// </summary>
        /// <param name="geoLayer">图层</param>
        /// <param name="mainColor">主颜色字符串RRGGBB,如"ff0000"为红色，主颜色即多边形图层的填充颜色，线图层的线条颜色，点图层的符号颜色</param>
        /// <param name="outlineColor">面或点的边线颜色字符串RRGGBB，若为null，则设置边线颜色为RGB：128, 138, 135</param>
        /// <param name="transparency">图层的透明度，0为不透明，100为全透明</param>
        /// <param name="widthOrSize">面/线图层的线宽，或点图层点的大小</param>
        public static IFeatureRenderer SimpleRenderer(this IGeoFeatureLayer geoLayer, string mainColor, string outlineColor = null, short transparency = 0, double widthOrSize = 1)
        {
            IColor lineColor = outlineColor == null ? null : ColorCreate.GetIColor(outlineColor);
            return SimpleRenderer(geoLayer, ColorCreate.GetIColor(mainColor), lineColor, transparency, widthOrSize);
        }

        /// <summary>
        /// 要素图层分级渲染（ClassBreakRender）：根据数字字段的值分组渲染图层
        /// </summary>
        /// <param name="geoFeatureLayer">操作图层</param>
        /// <param name="fieldName">操作字段名</param>
        /// <param name="breakCount">分级数量</param>
        /// <param name="outLineColor">分组符号的外框颜色</param>
        public static IFeatureRenderer ClassBreakRenderer(this IGeoFeatureLayer geoFeatureLayer, string fieldName, int breakCount, IColor outLineColor = null)
        {
            //获取该字段的最大值、最小值
            var statisticsResults = geoFeatureLayer.FeatureClass.Statistics(fieldName, null);
            double max = statisticsResults.Maximum;
            double min = statisticsResults.Minimum;

            //设置分级数，字段
            IClassBreaksRenderer cbRender = new ClassBreaksRendererClass();
            cbRender.MinimumBreak = min;//最小值
            cbRender.Field = fieldName;//分级字段
            cbRender.BreakCount = breakCount;//分级数量

            //设置每一级的分段范围，符号
            var lineSymbol = SymbolCreate.GetSimpleLineSymbol(outLineColor);//新建边线符号
            for (int i = 0; i < breakCount; i++)
            {
                var color = ColorCreate.GetIColor(0, 250 / breakCount * (breakCount - i), 0);
                cbRender.set_Break(i, (max - min) * (i + 1) / breakCount + min);
                cbRender.set_Symbol(i, new SimpleFillSymbolClass { Outline = lineSymbol, Color = color });
            }
            return geoFeatureLayer.Renderer = (IFeatureRenderer)cbRender;
        }
        /// <summary>
        /// 要素图层分级渲染（ClassBreakRender）：根据数字字段的值分组渲染图层
        /// </summary>
        /// <param name="geoFeatureLayer">操作图层</param>
        /// <param name="fieldName">操作字段名</param>
        /// <param name="breakCount">分级数量</param>
        /// <param name="outlineColorStr">分组符号的外框颜色字符串RRGGBB，若为null，则设置边线颜色为RGB：128, 138, 135</param>
        public static IFeatureRenderer ClassBreakRenderer(this IGeoFeatureLayer geoFeatureLayer, string fieldName, int breakCount, string outlineColorStr = null)
        {
            IColor lineColor = outlineColorStr == null ? null : ColorCreate.GetIColor(outlineColorStr);
            return ClassBreakRenderer(geoFeatureLayer, fieldName, breakCount, lineColor);
        }

        /// <summary>
        ///  要素图层柱状图渲染（BarChartRenderer）：根据一个或多个数字字段的值配置柱状图渲染图层
        /// </summary>
        /// <param name="geoFeatureLayer"></param>
        /// <param name="fieldNameColorDict"></param>
        public static IFeatureRenderer BarCharRenderer(this IGeoFeatureLayer geoFeatureLayer, Dictionary<string, IColor> fieldNameColorDict)
        {
            //创建柱状符号
            IBarChartSymbol barChartSymbol = new BarChartSymbolClass { Width = 12 };

            //获取两个字段的最大值，设置柱状图各柱状符号
            double maxValue = 0;
            ISymbolArray symbolArray = (ISymbolArray)barChartSymbol;
            foreach (var pair in fieldNameColorDict)
            {
                var value = geoFeatureLayer.FeatureClass.Statistics(pair.Key, null, EStatisticsType.Maximum);
                if (value > maxValue)
                    maxValue = value;

                IFillSymbol fillSymbol = new SimpleFillSymbol { Color = pair.Value };
                symbolArray.AddSymbol((ISymbol)fillSymbol);
            }

            //设置ChartSymbol的最大值，以及符号尺寸最大值（像素单位）
            IChartSymbol chartSymbol = (IChartSymbol)barChartSymbol;
            IMarkerSymbol markerSymbol = (IMarkerSymbol)barChartSymbol;
            chartSymbol.MaxValue = maxValue;
            markerSymbol.Size = 60;

            //设置字段，依据字段的数据值，创建柱状图
            IChartRenderer chartRenderer = new ChartRendererClass();
            IRendererFields rendererFields = (IRendererFields)chartRenderer;
            foreach (var pair in fieldNameColorDict)
                rendererFields.AddField(pair.Key, pair.Key);

            //设置图层的背景颜色       
            chartRenderer.ChartSymbol = (IChartSymbol)barChartSymbol;
            chartRenderer.BaseSymbol = new SimpleFillSymbolClass { Color = ColorCreate.GetIColor(239, 228, 190) };

            //设置其他属性
            chartRenderer.UseOverposter = false;
            chartRenderer.CreateLegend();//创建符号图例
            chartRenderer.Label = "";

            return geoFeatureLayer.Renderer = chartRenderer as IFeatureRenderer;
        }
        /// <summary>
        /// 要素图层唯一值渲染（UniqueValueRenderer）：统计字段不重复值进行分组渲染图层
        /// </summary>
        /// <param name="geoFeatureLayer"></param>
        /// <param name="fieldName">唯一值字段</param>
        public static IFeatureRenderer UniqueValueRenderer(this IGeoFeatureLayer geoFeatureLayer, string fieldName)
        {
            ITable table = (ITable)geoFeatureLayer.FeatureClass;
            IQueryFilter queryFilter = new QueryFilter();
            queryFilter.AddField(fieldName);
            var cursor = table.Search(queryFilter, true);

            //获取字段中各要素属性唯一值
            IDataStatistics dataStatistics = new DataStatisticsClass { Field = fieldName, Cursor = cursor };
            var enumreator = dataStatistics.UniqueValues;
            var fieldCount = dataStatistics.UniqueValueCount;

            IUniqueValueRenderer uvRenderer = new UniqueValueRendererClass();
            uvRenderer.FieldCount = 1; //单值渲染
            uvRenderer.set_Field(0, fieldName); //渲染字段
            IEnumColors enumColor = GetRandomColorRamp(fieldCount).Colors;
            enumColor.Reset();

            while (enumreator.MoveNext())
            {
                var value = enumreator.Current?.ToString();
                if (value == null)
                    continue;

                IColor color = enumColor.Next();
                ISymbol symbol = GetDefaultSymbol(geoFeatureLayer.FeatureClass.ShapeType, color);
                uvRenderer.AddValue(value, fieldName, symbol);
            }
            return geoFeatureLayer.Renderer = (IFeatureRenderer)uvRenderer;
        }

        /// <summary>
        /// 根据几何类型，获得该类型的简单样式符号（Simple Symbol）
        /// </summary>
        /// <param name="geometryType">几何类型</param>
        /// <param name="color">设置简单样式符合的颜色</param>
        /// <returns></returns>
        private static ISymbol GetDefaultSymbol(esriGeometryType geometryType, IColor color)
        {
            switch (geometryType)
            {
                case esriGeometryType.esriGeometryLine:
                case esriGeometryType.esriGeometryPolyline:
                    return SymbolCreate.GetSimpleLineSymbol(color, 3) as ISymbol;
                case esriGeometryType.esriGeometryPoint:
                    return SymbolCreate.GetSimpleMarkerSymbol(color, null, 6, esriSimpleMarkerStyle.esriSMSCircle) as ISymbol;
                case esriGeometryType.esriGeometryPolygon:
                    return SymbolCreate.GetSimpleFillSymbol(color) as ISymbol;
            }
            return null;
        }
        /// <summary>
        /// 构建随机色带
        /// </summary>
        /// <param name="size">表示要构建多少种色彩</param>
        /// <returns></returns>
        private static IRandomColorRamp GetRandomColorRamp(int size)
        {
            var randomColorRamp = new RandomColorRampClass
            {
                StartHue = 10,
                EndHue = 300,
                MaxSaturation = 100,
                MinSaturation = 0,
                MaxValue = 100,
                MinValue = 0,
                Size = size
            };
            randomColorRamp.CreateRamp(out _);
            return randomColorRamp;
        }
        #endregion


        #region 栅格数据渲染
        /// <summary>
        /// 栅格图层分级渲染
        /// </summary>
        /// <param name="rasterLayer">栅格图层</param>
        /// <param name="classifyCount">分级数，即分成多少级别进行渲染</param>
        /// <param name="fromColor">开始色彩</param>
        /// <param name="toColor">结束色彩</param>
        public static IRasterRenderer ClassifyRenderer(this IRasterLayer rasterLayer, int classifyCount = 3, IColor fromColor = null, IColor toColor = null)
        {
            return rasterLayer.Renderer = ClassifyRenderer(rasterLayer.Raster, classifyCount, fromColor, toColor);
        }
        /// <summary>
        /// 栅格数据分级渲染
        /// </summary>
        /// <param name="raster">栅格</param>
        /// <param name="classifyCount">分级数，即分成多少级别进行渲染</param>
        /// <param name="fromColor">开始色彩</param>
        /// <param name="toColor">结束色彩</param>
        /// <returns></returns>
        public static IRasterRenderer ClassifyRenderer(this IRaster raster, int classifyCount = 3, IColor fromColor = null, IColor toColor = null)
        {
            IRasterClassifyColorRampRenderer classifyRenderer = new RasterClassifyColorRampRendererClass();
            IRasterRenderer rasterRenderer = (IRasterRenderer)classifyRenderer;

            rasterRenderer.Raster = raster;
            classifyRenderer.ClassCount = classifyCount;
            rasterRenderer.Update();

            IAlgorithmicColorRamp colorRamp = new AlgorithmicColorRampClass();
            colorRamp.Size = classifyCount;
            if (fromColor != null) colorRamp.FromColor = fromColor;
            if (toColor != null) colorRamp.ToColor = toColor;
            colorRamp.CreateRamp(out var ok);

            IFillSymbol fillSymbol = new SimpleFillSymbolClass();
            for (int i = 0; i < classifyRenderer.ClassCount; i++)
            {
                fillSymbol.Color = colorRamp.get_Color(i);
                classifyRenderer.set_Symbol(i, (ISymbol)fillSymbol);
                classifyRenderer.set_Label(i, Convert.ToString(i));
            }
            return rasterRenderer;
        }

        /// <summary>
        /// 栅格图层拉伸渲染
        /// </summary>
        /// <param name="rasterLayer">栅格图层</param>
        /// <param name="formColor">开始色彩</param>
        /// <param name="toColor">结束色彩</param>
        public static IRasterRenderer StretchRenderer(this IRasterLayer rasterLayer, IColor formColor, IColor toColor)
        {
            return rasterLayer.Renderer = StretchRenderer(rasterLayer.Raster, formColor, toColor);
        }
        /// <summary>
        /// 栅格数据拉伸渲染
        /// </summary>
        /// <param name="raster">栅格</param>
        /// <param name="fromColor">开始色彩</param>
        /// <param name="toColor">结束色彩</param>
        /// <returns></returns>
        public static IRasterRenderer StretchRenderer(this IRaster raster, IColor fromColor, IColor toColor)
        {
            IAlgorithmicColorRamp colorRamp = new AlgorithmicColorRampClass();
            colorRamp.Size = 255;
            colorRamp.FromColor = fromColor;
            colorRamp.ToColor = toColor;
            colorRamp.CreateRamp(out var createColorRamp);

            IRasterStretchColorRampRenderer stretchRenderer = new RasterStretchColorRampRendererClass();
            IRasterRenderer rasterRenderer = (IRasterRenderer)stretchRenderer;

            rasterRenderer.Raster = raster;
            rasterRenderer.Update();
            stretchRenderer.BandIndex = 0;
            stretchRenderer.ColorRamp = colorRamp;

            IRasterStretch stretchType = (IRasterStretch)rasterRenderer;
            stretchType.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_StandardDeviations;
            stretchType.StandardDeviationsParam = 2;
            return rasterRenderer;
        }

        /// <summary>
        /// 栅格数据唯一值渲染
        /// </summary>
        /// <param name="rasterDataset">栅格数据集</param>
        /// <returns></returns>
        public static IRasterRenderer UniqueValueRenderer(this IRasterDataset rasterDataset)
        {
            IRaster2 raster = (IRaster2)rasterDataset.CreateDefaultRaster();
            ITable rasterTable = raster.AttributeTable;
            if (rasterTable == null) return null;

            int tableRows = rasterTable.RowCount(null);
            IRandomColorRamp colorRamp = new RandomColorRampClass();
            colorRamp.Size = tableRows;
            colorRamp.Seed = 100;
            colorRamp.CreateRamp(out var createColorRamp);
            if (createColorRamp == false) return null;

            IRasterUniqueValueRenderer uvRenderer = new RasterUniqueValueRendererClass();
            IRasterRenderer rasterRenderer = (IRasterRenderer)uvRenderer;
            rasterRenderer.Raster = rasterDataset.CreateDefaultRaster();
            rasterRenderer.Update();

            //Set the renderer properties.
            uvRenderer.HeadingCount = 1;
            uvRenderer.set_Heading(0, "All Data Value");
            uvRenderer.set_ClassCount(0, tableRows);
            uvRenderer.Field = "Value";
            for (int i = 0; i < tableRows; i++)
            {
                var row = rasterTable.GetRow(i);
                uvRenderer.AddValue(0, i, Convert.ToByte(row.get_Value(1)));
                uvRenderer.set_Label(0, i, Convert.ToString(row.get_Value(1)));  // Assuming the raster is 8-bit.
                var fillSymbol = new SimpleFillSymbolClass();
                fillSymbol.Color = colorRamp.get_Color(i);
                uvRenderer.set_Symbol(0, i, fillSymbol);
            }
            return rasterRenderer;
        }
        #endregion


    
    }
}
