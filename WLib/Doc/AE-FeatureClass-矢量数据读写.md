### 矢量数据读写

`WLib.ArcGis.GeoDatabase.FeatClass.FeatureClassEx`类提供大量扩展方法，以此实现在`FeatureClass`对象中直接增删改查要素，屏蔽ArcEngine的大量接口。

```C#
//打开指定路径下的要素类
var featureClass = FeatureClassEx.FromPath(@"c:\gis.gdb\lake");//打开单个图层，可以是shp，或mdb、gdb、dwg下的图层
var featureClasses = FeatureClassEx.AllFromPath(@"c:\gis.gdb");//打开数据库下的全部图层
var featureClasses2 = FeatureClassEx.AllFromPath(
    @"SERVER=ditu.com;INSTANCE=5151;DATABASE=sde;USER=sa;PASSWORD=sa;VERSION=dbo.DEFAULT");

//创建要素类
//在c:\map.mdb下创建名为river，坐标系为WGS84（4326）的线图层
var featureClass = FeatureClassEx.CreateToPath(@"c:\map.mdb\river", esriGeometryType.esriGeometryPolyline,
     SpatialRefOpt.CreateSpatialRef(4326, ESrType.Geographic));

//查询要素或图斑
var totalLen = featureClass.QueryFeatures("RoadType = 16", feature => (feature.Shape as IPolyline).Length);
var geometries = featureClass.QueryGeometries("Name = 'Mississippi'");
var geometry = featureClass.QueryUnionGeometry();
var values = featureClass.QueryValues("Name", "Continent = 'Oceania'");

//新增要素
featureClass.InsertOneFeature(featureBuffer =>
{
    featureBuffer.set_Value(featureBuffer.Fields.FindField("Area"), "30.67");
    featureBuffer.set_Value(featureBuffer.Fields.FindField("Volume"), "30000");
    featureBuffer.Shape = CreateLakeArea();
});

//删除要素
featureClass.DeleteFeatures("FID = 233");

//修改要素
featureClass.UpdateFeatures("model like 'P%'"， feature=> 
	featureBuffer.set_Value(featureBuffer.Fields.FindField("model"), "Plus"));

//其他操作示例
featureClass.Rename("GreatLake");//重命名
string sourcePath = featureClass.GetSourcePath();//获取存储路径
IFeatureClass newFeatureClass = featureClass.CopyTo(@"c:\gis\lake.shp");//复制到指定位置
```

* 上述为示例代码，方法的详细信息请在调用时或源码中查看方法注释。

* 上述示例方法包含多个重载方法，更多实现方法请查看[源码]()。

