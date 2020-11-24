## 拓扑

以下代码演示如何创建、验证拓扑，以及获取拓扑的图形结果信息。

```C#
//以下示例代码实现了：
//1、在c盘创建一个文件地理数据库tmp.gdb，在内部创建一个要素数据集
//2、将c:\lake.shp图层导入到该要素数据集，然后创建拓扑，验证图斑重叠

var sourceClass = FeatureClassEx.FromPath(@"c:\lake.shp");//打开指定路径下的要素类
var workspace = WorkspaceEx.GetOrCreateWorkspace(@"c:\tmp.gdb");//在指定位置下创建工作空间
var featureDataset = workspace.CreateFeatureDataset("TDataset", sourceClass.GetSpatialRef());//创建要素数据集
var newClass = featureDataset.CreateFeatureClass(sourceClass, "lake");//将要素类导入到数据集中

var topology = featureDataset.CreateTopology("Topology");//创建拓扑
var topoRule = topology.AddRule(esriTopologyRuleType.esriTRTAreaNoOverlap, newClass);//添加拓扑规则
topology.Validate();//验证拓扑

foreach (var errorFeature in topology.GetTopoErrorFeatures(topoRule))
    Console.WriteLine($"图斑【{errorFeature.OriginOID}】与图斑【{errorFeature.DestinationOID}】重叠！");
```

上述代码包含实现在` WLib.ArcGis`库的多个扩展方法，使用前应添加以下引用：

```C#
using WLib.ArcGis.Analysis.Topology;
using WLib.ArcGis.GeoDatabase.FeatClass;
using WLib.ArcGis.GeoDatabase.FeatDataset;
using WLib.ArcGis.GeoDatabase.WorkSpace;
```

* 上述为示例代码，详情请在调用时或源码中查看方法注释。

* 上述示例方法包含多个重载方法，更多实现方法请查看[源码]()。