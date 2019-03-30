/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using stdole;
using WLib.ArcGis.Carto.Element;
using WLib.ArcGis.Carto.Map;
using WLib.ArcGis.Display;

namespace WLib.ArcGis.Carto
{
    //1、ArcGIS中，标注(Label)是动态创建的，注记(Annotation)是作为图形/要素存储的
    //2、Shp文件不支持注记
    //3、保存在地图上的注记是IGraphicsLayer->IGraphicsContainer->IElement
    //   获取IGraphicsLayer：
    //4、保存在数据库的注记图层是IFeatureClass，加到地图中是IFeatureLayer/IAnnotationLayer，注记记录是IFeature/IAnnotationFeature.Annotation->IElement

    /// <summary>
    /// 标注与注记的操作
    /// </summary>
    public static class LabelAnnoOpt
    {

        #region 标注转地图文档注记
        /// <summary>
        /// 将地图上指定图层的标注转注记，存储在地图中
        /// (保存在地图上的注记是IGraphicsLayer->IGraphicsContainer->IElement)
        /// </summary>
        /// <param name="map">执行标注转注记的地图</param>
        /// <param name="layerNames">需要将标注转注记的图层名称</param>
        /// <param name="whichFeatures">标示将哪些要素生成注记的枚举（所有要素/当前范围的要素/选择的要素）</param>
        public static void ConvertLabelsToMapAnnotation(this IMap map, string[] layerNames,
             esriLabelWhichFeatures whichFeatures = esriLabelWhichFeatures.esriVisibleFeatures)
        {
            List<IGeoFeatureLayer> geoFeatureLayerList = new List<IGeoFeatureLayer>();
            for (int i = 0; i < map.LayerCount; i++)
            {
                if (map.get_Layer(i) is IGeoFeatureLayer geoFeatureLayer && layerNames.Contains(geoFeatureLayer.Name))
                    geoFeatureLayerList.Add(geoFeatureLayer);
            }
            ConvertLabelsToMapAnnotation(map, geoFeatureLayerList.ToArray(), whichFeatures);
        }
        /// <summary>
        /// 将地图上指定图层的标注转注记，存储在地图中
        /// (保存在地图上的注记是IGraphicsLayer->IGraphicsContainer->IElement)
        /// </summary>
        /// <param name="map">执行标注转注记的地图</param>
        /// <param name="layerIndexs">需要将标注转注记的图层索引（注意这些图层必须是map中的图层，索引不能超出范围）</param>
        /// <param name="whichFeatures">标示将哪些要素生成注记的枚举（所有要素/当前范围的要素/选择的要素）</param>
        public static void ConvertLabelsToMapAnnotation(this IMap map, int[] layerIndexs,
            esriLabelWhichFeatures whichFeatures = esriLabelWhichFeatures.esriVisibleFeatures)
        {
            List<IGeoFeatureLayer> geoFeatureLayerList = new List<IGeoFeatureLayer>();
            for (int i = 0; i < layerIndexs.Length; i++)
            {
                if (map.get_Layer(i) is IGeoFeatureLayer geoFeatureLayer)
                    geoFeatureLayerList.Add(geoFeatureLayer);
            }
            ConvertLabelsToMapAnnotation(map, geoFeatureLayerList.ToArray(), whichFeatures);
        }
        /// <summary>
        /// 将地图上指定图层的标注转注记，存储在地图中
        /// (保存在地图上的注记是IGraphicsLayer->IGraphicsContainer->IElement)
        /// </summary>
        /// <param name="map">执行标注转注记的地图</param>
        /// <param name="geoFeatureLayer">需要将标注转注记的图层（注意这些图层必须是map中的图层）</param>
        /// <param name="whichFeatures">标示将哪些要素生成注记的枚举（所有要素/当前范围的要素/选择的要素）</param>
        public static void ConvertLabelsToMapAnnotation(this IMap map, IGeoFeatureLayer[] geoFeatureLayer,
            esriLabelWhichFeatures whichFeatures = esriLabelWhichFeatures.esriVisibleFeatures)
        {
            IConvertLabelsToAnnotation convertLabelsToAnnotation = new ConvertLabelsToAnnotationClass();
            ITrackCancel trackCancel = new CancelTrackerClass();

            //设置标注转注记的参数：①地图、②注记存储位置(数据库注记/地图注记)、③哪些要素生成注记（所有要素/当前范围的要素/选择的要素）
            //④是否生成地位注记、⑥取消操作、⑦异常事件处理
            convertLabelsToAnnotation.Initialize(map, esriAnnotationStorageType.esriMapAnnotation,
                whichFeatures, true, trackCancel, null);

            //添加要进行转换的图层
            for (int i = 0; i < geoFeatureLayer.Length; i++)
            {
                convertLabelsToAnnotation.AddFeatureLayer(geoFeatureLayer[i],
                    geoFeatureLayer[i].Name + "_Anno", null, null, false, false, false, false, true, "");
            }
            //执行转换，生成注记
            convertLabelsToAnnotation.ConvertLabels();

            //隐藏标注
            for (int i = 0; i < geoFeatureLayer.Length; i++)
            {
                geoFeatureLayer[i].DisplayAnnotation = false;
            }

            //刷新地图
            IActiveView activeView = map as IActiveView;
            activeView.Refresh();
        }
        /// <summary>
        /// 将地图上的标注转注记，存储在地图中
        /// (保存在地图上的注记是IGraphicsLayer->IGraphicsContainer->IElement)
        /// </summary>
        /// <param name="map">执行标注转注记的地图</param>
        /// <param name="whichFeatures">标示哪些要素生成注记的枚举（所有要素/当前范围的要素/选择的要素）</param>
        public static void ConvertLabelsToMapAnnotation(this IMap map,
            esriLabelWhichFeatures whichFeatures = esriLabelWhichFeatures.esriVisibleFeatures)
        {
            IConvertLabelsToAnnotation convertLabelsToAnnotation = new ConvertLabelsToAnnotationClass();
            ITrackCancel trackCancel = new CancelTrackerClass();

            //设置标注转注记的参数：①地图、②注记存储位置(数据库注记/地图注记)、③哪些要素生成注记（所有要素/当前范围的要素/选择的要素）
            //④是否生成地位注记、⑥取消操作、⑦异常事件处理
            convertLabelsToAnnotation.Initialize(map, esriAnnotationStorageType.esriMapAnnotation,
                whichFeatures, true, trackCancel, null);

            //添加要进行转换的图层
            IGeoFeatureLayer geoFeatureLayer;
            for (int i = 0; i < map.LayerCount; i++)
            {
                geoFeatureLayer = map.get_Layer(i) as IGeoFeatureLayer;
                if (geoFeatureLayer != null)
                {
                    convertLabelsToAnnotation.AddFeatureLayer(geoFeatureLayer,
                        geoFeatureLayer.Name + "_Anno", null, null, false, false, false, false, true, "");
                }
            }
            //执行转换，生成注记
            convertLabelsToAnnotation.ConvertLabels();

            //隐藏标注
            for (int i = 0; i < map.LayerCount; i++)
            {
                geoFeatureLayer = map.get_Layer(i) as IGeoFeatureLayer;
                geoFeatureLayer.DisplayAnnotation = false;
            }

            //刷新地图
            IActiveView activeView = map as IActiveView;
            activeView.Refresh();
        }
        #endregion


        #region 标注转数据库注记
        /// <summary>
        ///  将地图上的标注转注记，生成注记图层存储在数据库中
        ///  (保存在数据库的注记图层是IFeatureClass/IFeatureLayer/IAnnotationLayer->IFeature as IAnnotationFeature.Annotation->IElement)
        /// </summary>
        /// <param name="map">执行标注转注记的地图</param>
        /// <param name="featureLinked">是否关联要素（关联要素的注记必须与其所关联的要素类存储在同一地理数据库中）</param>
        /// <param name="whichFeatures">标示哪些要素生成注记的枚举（所有要素/当前范围的要素/选择的要素）</param>
        /// <param name="outWorkspace">保存注记的工作空间</param>
        /// <param name="suffix">注记图层名称后缀</param>
        public static void ConvertLabelsToGdbAnnotationLayers(this IMap map, bool featureLinked = false,
            esriLabelWhichFeatures whichFeatures = esriLabelWhichFeatures.esriVisibleFeatures, IWorkspace outWorkspace = null, string suffix = "_Anno")
        {
            IConvertLabelsToAnnotation convertLabelsToAnnotation = new ConvertLabelsToAnnotationClass();
            ITrackCancel pTrackCancel = new CancelTrackerClass();

            //设置标注转注记的参数：①地图、②注记存储位置(数据库注记/地图注记)、③哪些要素生成注记（所有要素/当前范围的要素/选择的要素）
            //④是否生成地位注记、⑥取消操作、⑦异常事件处理
            convertLabelsToAnnotation.Initialize(map, esriAnnotationStorageType.esriDatabaseAnnotation,
                whichFeatures, true, pTrackCancel, null);

            //添加要进行转换的图层
            IGeoFeatureLayer geoFeatureLayer;
            for (int i = 0; i < map.LayerCount; i++)
            {
                geoFeatureLayer = map.get_Layer(i) as IGeoFeatureLayer;
                if (geoFeatureLayer != null)
                {
                    IFeatureClass featureClass = geoFeatureLayer.FeatureClass;
                    IDataset dataset = featureClass as IDataset;

                    IFeatureWorkspace outFeatureWorkspace;
                    IFeatureDataset featureDataset;
                    if (featureLinked)//关联要素时，生成的注记必须存储在原地理数据库数据集中
                    {
                        outFeatureWorkspace = dataset.Workspace as IFeatureWorkspace;
                        featureDataset = featureClass.FeatureDataset;
                    }
                    else
                    {
                        outFeatureWorkspace = outWorkspace as IFeatureWorkspace;
                        featureDataset = null;
                    }

                    convertLabelsToAnnotation.AddFeatureLayer(geoFeatureLayer, geoFeatureLayer.Name + suffix,
                        outFeatureWorkspace, featureDataset, featureLinked, false, false, true, true, "");
                }
            }

            //执行转换，生成注记
            convertLabelsToAnnotation.ConvertLabels();
            IEnumLayer enumLayer = convertLabelsToAnnotation.AnnoLayers;

            //隐藏标注
            for (int i = 0; i < map.LayerCount; i++)
            {
                geoFeatureLayer = map.get_Layer(i) as IGeoFeatureLayer;
                geoFeatureLayer.DisplayAnnotation = false;
            }

            //添加注记图层到地图中
            map.AddLayers(enumLayer, true);

            //刷新地图
            IActiveView pActiveView = map as IActiveView;
            pActiveView.Refresh();
        }

        /// <summary>
        ///  将指定图层的标注转注记，生成注记图层存储在数据库中
        ///  (保存在数据库的注记图层是IFeatureClass/IFeatureLayer/IAnnotationLayer->IFeature as IAnnotationFeature.Annotation->IElement)
        /// </summary>
        /// <param name="map">执行标注转注记的地图</param>
        /// <param name="layer">执行标注转注记的图层</param>
        /// <param name="featureLinked">是否关联要素（关联要素的注记必须与其所关联的要素类存储在同一地理数据库中）</param>
        /// <param name="whichFeatures"></param>
        /// <parparam name="whichFeatures">标示哪些要素生成注记的枚举（所有要素/当前范围的要素/选择的要素）</parparam>
        public static void ConvertLabelsToGdbAnnotationSingleLayer(this IMap map, ILayer layer, bool featureLinked = false,
            esriLabelWhichFeatures whichFeatures = esriLabelWhichFeatures.esriVisibleFeatures)
        {
            IConvertLabelsToAnnotation convertLabelsToAnnotation = new ConvertLabelsToAnnotationClass();
            ITrackCancel pTrackCancel = new CancelTrackerClass();

            //设置标注转注记的参数：①地图、②注记存储位置(数据库注记/地图注记)、③哪些要素生成注记（所有要素/当前范围的要素/选择的要素）
            //④是否生成地位注记、⑥取消操作、⑦异常事件处理
            convertLabelsToAnnotation.Initialize(map, esriAnnotationStorageType.esriDatabaseAnnotation,
                whichFeatures, true, pTrackCancel, null);

            IGeoFeatureLayer geoFeatureLayer = layer as IGeoFeatureLayer;
            if (geoFeatureLayer != null)
            {
                IFeatureClass featureClass = geoFeatureLayer.FeatureClass;
                IDataset dataset = featureClass as IDataset;
                IFeatureWorkspace featureWorkspace = dataset.Workspace as IFeatureWorkspace;
                try
                {
                    convertLabelsToAnnotation.AddFeatureLayer(geoFeatureLayer, geoFeatureLayer.Name + "_Anno",
                        featureWorkspace, featureClass.FeatureDataset, featureLinked, false, false, true, true, "");
                }
                catch (Exception ex) { throw new Exception("标注转注记操作出错：" + ex.ToString()); }

                //执行转换，生成注记
                convertLabelsToAnnotation.ConvertLabels();
                IEnumLayer enumLayer = convertLabelsToAnnotation.AnnoLayers;

                //隐藏标注
                geoFeatureLayer.DisplayAnnotation = false;

                //添加注记图层到地图中
                map.AddLayers(enumLayer, true);

                //刷新地图
                IActiveView pActiveView = map as IActiveView;
                pActiveView.Refresh();
            }
        }

        #endregion


        #region 标注操作
        /// <summary>
        /// 设置标注大小
        /// </summary>
        /// <param name="geoLayer">要设置标注的图层(IFeatureLayer as IGeoFeatureLayer)</param>
        /// <param name="size">标注的大小</param>
        public static void SetLabelSize(this IGeoFeatureLayer geoLayer, int size)
        {
            IAnnotateLayerPropertiesCollection annotateLyrProColl = geoLayer.AnnotationProperties;
            IAnnotateLayerProperties annoPros = null;
            IElementCollection placeElements = null;
            IElementCollection unplaceElements = null;
            for (int i = 0; i < annotateLyrProColl.Count; i++)
            {
                annotateLyrProColl.QueryItem(i, out annoPros, out placeElements, out unplaceElements);
                if (annoPros is ILabelEngineLayerProperties labelEngine)
                {
                    labelEngine.Symbol.Size = size;
                    break;
                }
            }
        }
        /// <summary>
        /// 显示指定图层指定字段的标注
        /// </summary>
        /// <param name="geoLayer">要设置注标注的图层(IFeatureLayer as IGeoFeatureLayer)</param>
        /// <param name="fieldName">显示标注的字段</param>
        /// <param name="fontName">标注的字体</param>
        /// <param name="size">标注的大小</param>
        public static void ShowLabel(this IGeoFeatureLayer geoLayer, string fieldName, string fontName = "宋体", int size = 12)
        {
            //标注属性集
            IAnnotateLayerPropertiesCollection annotateLyrProColl = geoLayer.AnnotationProperties;
            annotateLyrProColl.Clear();

            //普通标准属性（另一个是Maplex标准属性）  
            ILabelEngineLayerProperties labelEngine = new LabelEngineLayerPropertiesClass();
            labelEngine.Expression = $"[{fieldName}]";

            //字体
            IFontDisp fontDisp = new StdFont() { Name = fontName, Bold = false } as IFontDisp;

            //标注符号
            ITextSymbol textSymbol = new TextSymbolClass();
            textSymbol.Color = ColorCreate.GetIColor(0, 0, 0);
            textSymbol.Font = fontDisp;
            textSymbol.Size = size;
            labelEngine.Symbol = textSymbol;

            //设置同名标注：默认为移除同名标注，应设为每个要素放置一个标注
            IBasicOverposterLayerProperties basicOverpLyrPro = labelEngine.BasicOverposterLayerProperties as IBasicOverposterLayerProperties;
            basicOverpLyrPro.NumLabelsOption = esriBasicNumLabelsOption.esriOneLabelPerShape;//每个要素放置一个标注

            annotateLyrProColl.Add(labelEngine as IAnnotateLayerProperties);
            geoLayer.DisplayAnnotation = true;
        }
        /// <summary>
        /// 隐藏标注
        /// </summary>
        /// <param name="geoLayer">要设置标注的图层(IFeatureLayer as IGeoFeatureLayer)</param>
        public static void HideLabel(this IGeoFeatureLayer geoLayer)
        {
            geoLayer.DisplayAnnotation = false;
        }
        #endregion


        #region 注记操作
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
        #endregion


        #region *未整理的标注和注记操作
        //TODO: 未整理的标注和注记操作
        ///// <summary>
        ///// 显示指定图层指定字段的标注
        ///// </summary>
        ///// <param name="geoLayer"></param>
        ///// <param name="fieldName"></param>
        ///// <param name="fontName"></param>
        ///// <param name="size"></param>
        //public static void ShowLabel(IGeoFeatureLayer geoLayer, string fieldName, string fontName = "宋体", int size = 12)
        //{
        //    IAnnotateLayerPropertiesCollection annotateLyrProColl = geoLayer.AnnotationProperties;
        //    annotateLyrProColl.Clear();

        //    //字体
        //    IFontDisp fontDisp = new StdFont()
        //    {
        //        Name = fontName,
        //        Bold = false
        //    } as IFontDisp;

        //    //符号
        //    IColor color = YYGISLib.ArcGisHelper.Map.SymbolRender.getIColor(0, 0, 0);
        //    ITextSymbol pTextSymbol = new TextSymbolClass()
        //    {
        //        Color = color,
        //        Font = fontDisp,
        //        Size = size
        //    };

        //    //用来控制标注和要素的相对位置关系       
        //    ILineLabelPosition lineLabelPosition = new LineLabelPositionClass()
        //    {
        //        Parallel = false,  //标注是否水平
        //        Perpendicular = true,//标注是否垂直
        //        InLine = true
        //    };

        //    //用来控制标注冲突         
        //    ILineLabelPlacementPriorities linePlace = new LineLabelPlacementPrioritiesClass()
        //    {
        //        AboveStart = 5, //让above 和start的优先级为a5        
        //        BelowAfter = 4
        //    };


        //    IBasicOverposterLayerProperties basicOverposterlayerProps = new BasicOverposterLayerPropertiesClass();
        //    switch (geoLayer.FeatureClass.ShapeType)//判断图层类型  
        //    {
        //        case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
        //            basicOverposterlayerProps.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolygon;
        //            break;
        //        case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
        //            basicOverposterlayerProps.FeatureType = esriBasicOverposterFeatureType.esriOverposterPoint;
        //            break;
        //        case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
        //            basicOverposterlayerProps.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolyline;
        //            break;
        //    }
        //    basicOverposterlayerProps.NumLabelsOption = esriBasicNumLabelsOption.esriOneLabelPerShape;//每个要素放置一个标注

        //    //创建标注对象        
        //    ILabelEngineLayerProperties lableEngine = new LabelEngineLayerPropertiesClass()
        //    {
        //        Symbol = pTextSymbol,
        //        BasicOverposterLayerProperties = basicOverposterlayerProps,
        //        IsExpressionSimple = true,
        //        Expression = string.Format("[{0}]", fieldName)
        //    };

        //    //设置标注的参考比例尺      
        //    IAnnotateLayerTransformationProperties annoLyrTranProperties = lableEngine as IAnnotateLayerTransformationProperties;
        //    //annoLyrTranProperties.ReferenceScale = 2500000;

        //    //设置标注可见的最大最小比例尺       
        //    IAnnotateLayerProperties annoPros = lableEngine as IAnnotateLayerProperties;
        //    //annoPros.AnnotationMaximumScale = 25000000;
        //    //annoPros.AnnotationMinimumScale = 1;
        //    annoPros.WhereClause = null;//属性  设置过y滤条件t     

        //    annotateLyrProColl.Add(annoPros);

        //    //显示标注
        //    geoLayer.DisplayAnnotation = true;
        //}


        //public void showAnnotationByScale(IMap pMap)
        //{
        //    IFeatureLayer pFeaturelayer = pMap.get_Layer(0) as IFeatureLayer;
        //    IGeoFeatureLayer pGeoFeatureLayer = pFeaturelayer as IGeoFeatureLayer;
        //    //创建标注集接口，可以对标注进行添加、删除、查询、排序等操作
        //    IAnnotateLayerPropertiesCollection pAnnotateLayerPropertiesCollection = new AnnotateLayerPropertiesCollectionClass();
        //    pAnnotateLayerPropertiesCollection = pGeoFeatureLayer.AnnotationProperties;
        //    pAnnotateLayerPropertiesCollection.Clear();
        //    //创建标注的颜色
        //    IRgbColor pRgbColor = new RgbColorClass();
        //    pRgbColor.Red = 255;
        //    pRgbColor.Green = 0;
        //    pRgbColor.Blue = 0;
        //    //创建标注的字体样式
        //    ITextSymbol pTextSymbol = new TextSymbolClass();
        //    pTextSymbol.Color = pRgbColor;
        //    pTextSymbol.Size = 12;
        //    pTextSymbol.Font.Name = "宋体";
        //    //定义 ILineLabelPosition接口，用来管理line features的标注属性，指定标注和线要素的位置关系
        //    ILineLabelPosition pLineLabelPosition = new LineLabelPositionClass();
        //    pLineLabelPosition.Parallel = false;
        //    pLineLabelPosition.Perpendicular = true;
        //    pLineLabelPosition.InLine = true;
        //    //定义 ILineLabelPlacementPriorities接口用来控制标注冲突
        //    ILineLabelPlacementPriorities pLineLabelPlacementPriorities = new LineLabelPlacementPrioritiesClass();
        //    pLineLabelPlacementPriorities.AboveStart = 5;
        //    pLineLabelPlacementPriorities.BelowAfter = 4;
        //    //定义 IBasicOverposterLayerProperties 接口实现 LineLabelPosition 和 LineLabelPlacementPriorities对象的控制
        //    IBasicOverposterLayerProperties pBasicOverposterLayerProperties = new BasicOverposterLayerPropertiesClass();
        //    pBasicOverposterLayerProperties.LineLabelPlacementPriorities = pLineLabelPlacementPriorities;
        //    pBasicOverposterLayerProperties.LineLabelPosition = pLineLabelPosition;
        //    pBasicOverposterLayerProperties.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolygon;
        //    //创建标注对象
        //    ILabelEngineLayerProperties pLabelEngineLayerProperties = new LabelEngineLayerPropertiesClass();
        //    //设置标注符号
        //    pLabelEngineLayerProperties.Symbol = pTextSymbol;
        //    pLabelEngineLayerProperties.BasicOverposterLayerProperties = pBasicOverposterLayerProperties;
        //    //声明标注的Expression是否为Simple
        //    pLabelEngineLayerProperties.IsExpressionSimple = true;
        //    //设置标注字段
        //    pLabelEngineLayerProperties.Expression = "[DIQU]";
        //    //定义IAnnotateLayerTransformationProperties 接口用来控制feature layer的display of dynamic labels
        //    IAnnotateLayerTransformationProperties pAnnotateLayerTransformationProperties = pLabelEngineLayerProperties as IAnnotateLayerTransformationProperties;
        //    //设置标注参考比例尺
        //    pAnnotateLayerTransformationProperties.ReferenceScale = 500000;
        //    //定义IAnnotateLayerProperties接口，决定FeatureLayer动态标注信息
        //    IAnnotateLayerProperties pAnnotateLayerProperties = pLabelEngineLayerProperties as IAnnotateLayerProperties;
        //    //设置显示标注最大比例尺
        //    pAnnotateLayerProperties.AnnotationMaximumScale = 500000;
        //    //设置显示标注的最小比例
        //    pAnnotateLayerProperties.AnnotationMinimumScale = 25000000;
        //    //决定要标注的要素
        //    pAnnotateLayerProperties.WhereClause = "DIQU<>'宿州市'";
        //    //将创建好的标注对象添加到标注集对象中
        //    pAnnotateLayerPropertiesCollection.Add(pAnnotateLayerProperties);
        //    //声明标注是否显示
        //    pGeoFeatureLayer.DisplayAnnotation = true;
        //    //刷新视图
        //}

        //public void Test(IMap map, IGeoFeatureLayer geoFeatureLayer, bool boolLinkFeatureClass,)
        //{
        //    IAnnotateLayerPropertiesCollection personalMapAnnoPropsColl = new AnnotateLayerPropertiesCollection();
        //    IAnnotateLayerPropertiesCollection personalAnnotateLayerPropertiesCollection = geoFeatureLayer.AnnotationProperties;

        //    IAnnotateLayerProperties personalAnnotateLayerProperties;

        //    ILabelEngineLayerProperties2 personalLabelEngineLayerProperties;
        //    IClone symbolClone;
        //    ISymbolIdentifier2 symbolIdentifier;
        //    ISymbolCollection2 symbolCol = new SymbolCollectionClass();

        //    for (int propIndex = 0; propIndex < personalAnnotateLayerPropertiesCollection.Count; propIndex++)
        //    {
        //        IElementCollection placedElements = null;
        //        IElementCollection unplacedElements = null;
        //        personalAnnotateLayerPropertiesCollection.QueryItem(propIndex, out personalAnnotateLayerProperties, out placedElements, out unplacedElements);
        //        if (personalAnnotateLayerProperties != null)
        //        {
        //            personalMapAnnoPropsColl.Add(personalAnnotateLayerProperties);
        //            personalLabelEngineLayerProperties = personalAnnotateLayerProperties as ILabelEngineLayerProperties2;
        //            symbolClone = personalLabelEngineLayerProperties.Symbol as IClone;
        //            symbolCol.AddSymbol((ISymbol)symbolClone.Clone(), personalAnnotateLayerProperties.Class + " " + propIndex, out symbolIdentifier);
        //            personalLabelEngineLayerProperties.SymbolID = symbolIdentifier.ID;
        //        }
        //    }

        //    // 清空对象
        //    personalAnnotateLayerProperties = null;
        //    personalLabelEngineLayerProperties = null;

        //    // 得到创建annolayer的必要信息
        //    IFeatureClassDescription personalAnnoFeatureClassDesc;
        //    personalAnnoFeatureClassDesc = new AnnotationFeatureClassDescriptionClass();
        //    IObjectClassDescription personalAnnoObjectClassDesc = personalAnnoFeatureClassDesc as IObjectClassDescription;
        //    IFields personalFields = personalAnnoObjectClassDesc.RequiredFields;
        //    IField personalField = personalFields.get_Field(personalFields.FindField(personalAnnoFeatureClassDesc.ShapeFieldName));
        //    IGeometryDefEdit geomDefEdit = personalField.GeometryDef as IGeometryDefEdit;
        //    geomDefEdit.SpatialReference_2 = geoDataset.SpatialReference; // 重新设置空间参考 

        //    // 获得注记
        //    IMapOverposter mapOverposter =map as IMapOverposter;
        //    IOverposterProperties overposterProperties = mapOverposter.OverposterProperties;
        //    IAnnotationLayer personalAnnoLayer;

        //    // 创建AnnoLayer
        //    IAnnotationLayerFactory personalAnnotationLayerFactory = new FDOGraphicsLayerFactoryClass() as IAnnotationLayerFactory;

        //    // 得到标注
        //    IGraphicsLayerScale refScale = new GraphicsLayerScaleClass();
        //    refScale.ReferenceScale = map.MapScale;

        //    // 创建注记层AnnoLayer
        //    if (boolLinkFeatureClass)
        //    {
        //        personalAnnoLayer = personalAnnotationLayerFactory.CreateAnnotationLayer((IFeatureWorkspace)workspace, 
        //            personalFeatureClass.FeatureDataset, this.cbeAnnotationName.Text.Trim(), 
        //            (IGeometryDef)geomDefEdit, personalFeatureClass, personalMapAnnoPropsColl, refScale, 
        //            (ISymbolCollection)symbolCol, boolLinkFeatureClass, boolLinkFeatureClass, false, true,
        //            overposterProperties, string.Empty);
        //    }
        //    else
        //    {
        //        personalAnnoLayer = personalAnnotationLayerFactory.CreateAnnotationLayer((IFeatureWorkspace)workspace,
        //            personalFeatureClass.FeatureDataset, this.cbeAnnotationName.Text.Trim(), 
        //            (IGeometryDef)geomDefEdit, null, personalMapAnnoPropsColl, refScale,
        //            (ISymbolCollection)symbolCol, boolLinkFeatureClass, boolLinkFeatureClass, false, true, 
        //            overposterProperties, string.Empty);
        //    } 
        //}
        #endregion
    }
}
