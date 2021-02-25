## 地理处理/空间分析

* `WLib.ArcGis.Analysis.OnClass.SpatialAnalysisOpt`类提供常见地理处理和空间分析操作方法（扩展方法）；

* 目前包含的分析功能有：融合、合并、裁剪、相交、联合、要素转点

```C#
IFeatureClass featureClass = FeatureClassEx.FromPath(@"c:\map.gdb\land");
IFeatureClass featureClass2 = FeatureClassEx.FromPath(@"c:\gis\region.shp");

IFeatureClass intersectFeatureClass = featureClass.Intersect(featureClass2, "c:\gis", "intersectResult");//图层相交
IFeatureClass clipFeatureClass = featureClass.Clip(featureClass2, "c:\gis", "clipResult");//图层裁剪
IFeatureClass mergeFeatureClass = featureClass.Merge(featureClass2, "c:\gis", "mergeResult");//图层合并
IFeatureClass dissolveFeatureClass = featureClass.Dissolve(overlayClass,交"c:\gis","RegionType","Minimum.FieldName1,Average.FieldName2", "dissolveResult");//图层融合
IFeatureClass unionFeatureClass = featureClass.Union(overlayClass, "c:\gis", "unionResult");//图层联合
IFeatureClass pointFeatureClass = featureClass.PolygonClassToPoint("c:\gis", "pointResult");//图层转点
```

- 上述示例仅展示部分方法，更多方法及其注释请调用时查看，或参阅[源码]()。