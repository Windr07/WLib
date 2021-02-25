

## 标注、注记、元素

* ArcGIS中，标注(Label)是动态创建的，注记(Annotation)则是固定存储在地图或数据库的一类图形或要素
* Shp文件不支持保存注记
* 注记可作为图形（Graphics->Element）保存在地图上(即mxd文件中)；也可做为图层保存在数据库(mdb、gdb、sde等)中：
  1. 保存在地图上的注记是IGraphicsLayer->IGraphicsContainer->IElement
  2. 保存在数据库的注记图层是IFeatureClass，加到地图中是IFeatureLayer/IAnnotationLayer，注记记录是IFeature/IAnnotationFeature.Annotation->IElement

### 标注

`WLib.ArcGis.Carto.LabelAnno.LabelOpt`提供标注相关扩展方法：

```C#
IGeoFeatureLayer geoFeatureLayer = featureLayer as IGeoFeatureLayer;
geoFeatureLayer.SetLabelSize(12);//设置标注大小
geoFeatureLayer.ShowLabel("SHAPE_Area", "Times New Roman", 12);//显示标注，并且设置标注字体和大小
geoFeatureLayer.HideLabel();//隐藏标注
```

### 注记

`WLib.ArcGis.Carto.LabelAnno.AnnoOpt`提供标注相关扩展方法：

```C#
map.SetAnnotationFontOnMap("LayerName1", "宋体", "12");//设置存储在地图上的注记的字体和大小
graphicsLayer.SetAnnotationFontOnMap("宋体", "12");//设置注记图层注记的字体和大小
```

### 标注转注记

`WLib.ArcGis.Carto.LabelAnno.LabelAnnoConvert`提供标注转注记相关扩展方法：

```C#
//将地图上指定名称图层的标注转注记，存储在地图中
map.ConvertLabelsToMapAnnotation(new []{"LayerName1"});
//将地图上指定索引图层的标注转注记，存储在地图中
map.ConvertLabelsToMapAnnotation(new []{1,2});
//将地图上的全部图层的标注转注记，存储在地图中
map.ConvertLabelsToMapAnnotation(esriLabelWhichFeatures.esriVisibleFeatures);

//将地图上的标注转注记，生成注记图层存储在数据库中
map.ConvertLabelsToGdbAnnotation(false, esriLabelWhichFeatures.esriVisibleFeatures, outWorkspace);
//将指定图层的标注转注记，生成注记图层存储在数据库中
map.ConvertLabelsToGdbAnnotation(layer);
```

### 元素操作

```C#
var graphicsContainer = map as IGraphicsContainer;

//创建元素
var fillShapeElement = graphicsContainer.CreateElement(polygonGeometry, EDrawElementType.Polygon);
var textElement = graphicsContainer.CreateElement(pointGeometry, EDrawElementType.Text);//创建文本元素
var fillShapeElement = graphicsContainer.CreateElement(polygonGeometry, EDrawElementType.Circle);//创建圆形元素
var fillShapeElement = graphicsContainer.CreateElement(envelopeGeometry, EDrawElementType.Rectangle);//创建矩形元素
var markerElement = graphicsContainer.CreateElement(pointGeometry, EDrawElementType.Point);//创建点元素
var lineElement = graphicsContainer.CreateElement(polylineGeometry, EDrawElementType.Polyline);//创建线元素

//查询元素
var element1 = graphicsContainer.GetFirstGroupElement("group Element Name");//根据名称查找并返回第一个组合元素
var element2 = graphicsContainer.GetFirstElement("element Name");//根据名称查找并返回第一个元素
var elements = graphicsContainer.GetElements();//获取所有元素
var textElements = graphicsContainer.GetTextElements();//获取所有文本元素
var mapFrames = graphicsContainer.GetMapFrames();//获取所有地图数据框

```

### 元素分类

```C#
 /** 元素分类（以下只是元素分类，而不表示接口的继承关系）
     *  (1)图形元素GraphicElement（点、线、面、文本等元素）
     *  (2)框架元素FrameElement（大部分框架元素实现类也继承图形元素接口IGraphicElement）
     *      ①地图数据框  ②指北针、比例尺、图例等
     * Element:（https://desktop.arcgis.com/en/arcobjects/10.4/net/webframe.htm#IElement.htm）
     * (1)GraphicElement（https://desktop.arcgis.com/en/arcobjects/10.4/net/webframe.htm#IGraphicElement.htm）
     *      MarkerElement							显示标记的图形元素
     *      LineElement								显示线条的图形元素
     *      PolygonElement							显示多边形的图形元素
     *      RectangleElement						显示矩形的图形元素
     *      CircleElement							显示圆圈的图形元素
     *      EllipseElement							显示椭圆的图形元素
     *      MultiPatchElement						
     *      DataGraphTElement(esriCartoUI)			用于在ArcMap布局视图上显示和操作数据图形的图形元素的容器
     *      DisplacementLinkElement(esriEditorExt)	显示调整链接的图形元素
     *      GeoEllipseElement(esriDefenseSolutions)	显示GeoEllipses的图形元素
     *      GeoPolygonElement(esriDefenseSolutions)	用于显示GeoPolygons的图形元素
     *      GeoPolylineElement(esriDefenseSolutions)用于显示GeoPolylines的图形元素
     *      IdentityLinkElement(esriEditorExt)		显示身份链接的图形元素
     *      InkGraphic								墨迹图形对象
     *      MoleGroupElement(esriDefenseSolutions)	
     *      ParagraphTextElement					
     *      TextElement							    用于显示文本的图形元素
     *      PMFTitleTextElement						用于显示动态PMF标题的图形元素
     *      Text3DElement							用于显示3D文本图形元素
     * (2)FrameElement
     *      MapFrame                                用于显示地图的图形元素
     *      MapSurroundFrame                        用于显示地图要素的图形元素（管理MapSurround，与MapSurround一一对应）
     *          MapSurround                         地图环绕（指北针、比例尺、图例等）
     *              Legend                          图例
     *              NorthArrow                      指北针
     *              MapInset                        插图（缩略图）
     *              ScaleBar                        比例尺
     *              ScaleText                       比例尺文本
     *              OverView
     *                  MapTitle                    地图标题
     *      OleFrame(esriArcMapUI)		            OLE框架元素
     *      TableFrame(esriEditorExt)	            显示表格的图形元素
     *      GroupElement				            组图形元素，包含一组图形元素
     *      TemporalChartElement(esriTrackingAnalystUI)	控制时间图表的元素
     *      PictureElement				            图片图形元素
     *          BmpPictureElement			        显示BMP图片的图形元素
     *          EmfPictureElement			        显示Emf图片的图形元素
     *          GifPictureElement			        显示GIF图片的图形元素
     *          Jp2PictureElement			        显示JPEG2000图片的图形元素
     *          JpgPictureElement			        显示JPG图片的图形元素
     *          PngPictureElement			        显示PNG图片的图形元素
     *          TifPictureElement			        显示TIF图片的图形元素
     */
```

- 上述示例仅展示部分方法，更多方法及其注释请调用时查看，或参阅[源码]()。