## 地图和图层查询

* `WLib.ArcGis.Carto.Map.MapDocEx`类提供地图文档的创建、打开、获取地图等对`IMapDocument`的扩展方法；

* `WLib.ArcGis.Carto.Map.MapEx`类提供在地图中查询图层、表格、获取选中要素等对`IMap`的扩展方法；

```C#
//打开或创建地图文档
var mapDocument = MapDocEx.OpenOrCreateMapDoc(@"c:\gis\map.mxd");

//打开地图文档中指定名称的地图
var map = mapDocument.GetMap("Layers");
```

```C#
//从地图中获取多个图层
var featureLayers = map.GetFeatureLayers();
var featureLayers2 = map.GetFeatureLayersWithoutAnno();
var graphicsLayers = map.GetGraphicsLayers();
var layers = map.GetLayersByUid(LayerUid.IACFeatureLayer);

//从地图中获取指定名称的图层
var layer = map.GetFeatureLayer("layerName");
var layer = map.GetFeatureLayer2("layerName");
var layer = map.GetFeatureLayerByKeyword("layerName");
var layer = map.GetFeatureLayerWithOutAnno("layerName");
var layer = map.GetFeatureLayer("layerName");
var layer = map.GetGraphicsLayer("layerName");

//从地图中获取指定名称的图层
var layer = map.GetTable("table1");
var layer = map.GetTables("table1");

//获取地图指定图层中选中的要素
var features = map.GetSelectionFeatures("layerName");
```

```C#
//设置地图为固定比例尺模式，并设定其比例尺
map.SetMapScale(1000);
```

- 上述示例仅展示部分方法，更多方法及其注释请调用时查看，或参阅[源码]()。