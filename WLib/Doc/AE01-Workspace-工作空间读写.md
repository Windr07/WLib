## 工作空间或数据库操作

* `WLib.ArcGis`库的核心是封装ArcEngine接口，简化GIS相关的操作。
* ArcEngine中，通常仅对shp或数据库图层常见的读写即涉及大量接口，通过`WLib.ArcGis`库的扩展方法可简化至一两个方法来实现。
* 通过`WorkspaceEx.GetWorkSpace`即可打开和操作各类常见GIS数据库，参考下方示例（更多方法及使用说明请查看``WorkspaceEx`类及其方法注释）。

```C#
//打开工作空间
var workspace = WorkspaceEx.GetWorkSpace(@"c:\gis.mdb");//打开mdb数据库
var workspace2 = WorkspaceEx.GetWorkSpace(@"c:\gis");//打开shp工作空间（shp文件所在目录）
var workspace3 = WorkspaceEx.GetWorkSpace(
    @"SERVER=ditu.com;INSTANCE=5151;DATABASE=sde;USER=sa;PASSWORD=sa;VERSION=dbo.DEFAULT");//打开SDE

//创建工作空间
var workapace4 = WorkspaceEx.CreateWorkspace(EWorkspaceType.FileGDB, "c:\gis", "map.mdb");//创建mdb数据库
var workspace5 = WorkspaceEx.GetOrCreateWorkspace(@"c:\WorldMap.gdb");//创建或打开gdb数据库

//开始编辑
workspace1.StartEdit(()=>
{
    //TODO: edit
}, false);//开始工作空间编辑，执行完指定操作后保存并停止编辑

//在工作空间中操作表格、要素类、数据集
var datasets = workspace.GetDatasets(esriDatasetType.esriDTFeatureClass);
if(!workspace.IsFeatureClassExsit("RoadLayer"))//判断要素类“RoadLayer”是否存在，不存在则创建
{
    var spatialRef = SpatialRefOpt.CreateSpatialRef(
    	esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_37);//创建坐标系
	var featureClass = workspace.CreateFeatureClass("RoadLayer",spatialRef, esriGeometryType.esriGeometryPolyline);
}
var tables = workspace.GetTables();//获取全部表格
```

* 上述示例仅展示部分方法，更多方法及其注释请调用时查看，或参阅[源码]()。


