## 地理处理/空间分析

* `WLib.ArcGis.Analysis.OnClass.SpatialAnalysisOpt`类提供常见地理处理和空间分析操作方法（含扩展方法）；

* 目前包含的分析功能有：融合、合并、裁剪、相交、联合、要素转点

```C#
IFeatureClass intersectFeatureClass = featureClass.Intersect(overlayClass, "c:\gis", "result");
```

