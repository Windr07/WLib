# WLib
* WLib是一组对C#.NET和ArcGIS Engine开发常用代码进行封装优化的基础库和控件库；

* 以减少重复代码，提高ArcGIS桌面端或Web后端开发效率为目标。

## 简介
解决方案包含7个项目（代码库），核心库为[WLib]()和[WLib.ArcGis]()

| 代码库            | 说明                     | 内容                                                         |
| ----------------- | ------------------------ | :----------------------------------------------------------- |
| WLib              | 通用基础库               | DbHelper、Windows API、硬件信息获取、压缩与解压缩、反射、DataTable转对象（简单ORM）、插件功能等 |
| WLib.ArcGis       | ArcGIS基础库             | 优化和封装的ArcObject代码集，内容包含GP调用、拓扑、空间查询、地图查询、标注、注记、元素增删改查、制图出图、样式、专题渲染、坐标系判读转换、点线面几何创建、控件组合联动、许可初始化、shp/gdb/mdb/sde/sql/oledb连接和表格/矢量/栅格数据增删改查、数据统计、数据转换、类型转换等 |
| WLib.Envir        | 环境检测库               | 检测.NET版本、ArcGIS版本、Windows操作系统版本的代码          |
| WLib.Files        | excel、Word、pdf读写库   | 包含[NPOI](https://github.com/tonyqus/npoi/)、[AppLibrary.dll](http://www.pudn.com/Download/item/id/2192742.html) 、[Microsoft.Office.Interop.Excel]()操作xls或xlsx；使用[Microsoft.Office.Interop.Word]()读写doc和docx；使用[itextsharp]()操作pdf文档 |
| WLib.Gdal         | GDAL帮助库               | 开源GIS库[GDAL(Geospatial Data Abstraction Library)](https://www.gdal.org/)的Helper代码 |
| WLib.WinCtrls     | WinForm控件库            | 一些自定义的WinForm和ArcEngine控件                           |
| WLib.WinCtrls.Dev | DevExpress WinForm控件库 | 一些自定义的DevExpress WinForm和ArcEngine控件                |

## 引用

nuget

```nuget
Install-Package WLib
Install-Package WLib.ArcGis
Install-Package WLib.Envir
```

 ## 环境

 ### 开发环境
* Visual Studio 2019

* .NET Framework 4.0

  （WLib.WinCtrls.Dev库为.NET Framework 4.6）


 ### 调用组件
 引用的第三方dll已包含到[DLL](DLL)文件夹中，也可通过Nuget引入
 * AppLibrary.dll 轻量级开源的Excel读写库
 * [GDAL](https://www.gdal.org/)  开源GIS开发库
 * [SharpZipLib](https://www.nuget.org/packages/SharpZipLib/) 文件压缩和解压缩库
 * [NPOI](https://github.com/tonyqus/npoi)
 * [System.Data.SQLite](https://sqlite.org/index.html)
 * [Json.NET](https://www.newtonsoft.com/json)
 * [itextsharp](https://github.com/itext/itextsharp)

  ## 使用示例

以下为简单使用示例，更多说明请参阅[WIKI](https://gitee.com/windr07/WLib/wikis)

```cSharp
    //使用以下对象需引用：
    //using WLib.ArcGis.Data;
    //using WLib.ArcGis.GeoDatabase.FeatClass;
    //using WLib.ArcGis.GeoDatabase.WorkSpace;
    //using WLib.Database;
    //using WLib.Database.DbBase;

    //---------示例1：获得区域内的河流的总长度----------
    //1、直接根据图层的路径获得 IFeatureClass 对象
    IFeatureClass featureClass = FeatureClassEx.FromPath(@"c:\World.mdb\River");//获取河流图层
    //2、通过QueryFeatures扩展方法，查询图层中的数据
    double sumRiverLength = 0.0;//计算河流的总长度
    featureClass.QueryFeatures(@"XZQDM = '440000'", feature => sumRiverLength += feature.ToDouble("RiverLength"));


    //---------示例2：复制图层----------
    var workspace = WorkspaceEx.GetWorkSpace(@"c:\World.mdb");
    workspace.GetFeatureClassByName("River").CopyStruct(workspace, "NewRiver", "河流");

    //---------示例3：数据查询----------
    //数据库连接和SQL查询的方式获取shp、mdb、gdb、dbf数据
    DbHelper dbHelper1 = DbHelper.GetShpMdbGdbHelper(@"c:\River.dbf");
    System.Data.DataTable dataTable = dbHelper1.GetDataTable(@"select * from River where RiverName =  'Pearl River'");
```



