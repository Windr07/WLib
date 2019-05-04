# WLib
个人整理优化的C＃.NET + ArcObject / ArcEngine常用代码库，多以静态方法或扩展方法对C#和ArcGIS常用操作进行分类封装，目标是尽量少些重复代码，提高ArcGIS桌面端或Web后端开发效率

## 简介
解决方案包含8个项目（代码库），核心库为[WLib]()和[WLib.ArcGis]()

代码库|说明|内容
--|---|--
WLib|C#基础库|DbHelper、Windows API、硬件信息获取、压缩与解压缩、反射、DataTable转对象（简单ORM）等
WLib.ArcGis|ArcGIS基础库|优化和封装的ArcObject代码集，内容包含GP调用、拓扑、空间查询、地图查询、标注、注记、元素增删改查、制图出图、样式、专题渲染、坐标系判读转换、点线面几何创建、控件组合联动、许可初始化、shp/gdb/mdb/sde/sql/oledb连接和表格/矢量/栅格数据增删改查等
WLib.Envir|环境检测库|获取安装的.NET版本、ArcGIS版本、Windows操作系统版本
WLib.Files.Excel|excel读写库|使用[NPOI](https://github.com/tonyqus/npoi/)读写xls和xlsx、使用[AppLibrary.dll](http://www.pudn.com/Download/item/id/2192742.html) 库的简单xls操作、使用官方库[Microsoft.Office.Interop.Excel]()操作xls和xlsx
 WLib.Files.Word|word读写库|使用官方库[Microsoft.Office.Interop.Word]()读写doc、docx
 WLib.Gdal|GDAL帮助库|开源GIS库[GDAL(Geospatial Data Abstraction Library)](https://www.gdal.org/)的Helper代码
 WLib.UserCtrls|WinForm控件库|一些自定义的WinForm和ArcEngine控件
 WLib.UserCtrls.Dev|DevExpress WinForm控件库|一些自定义的DevExpress WinForm和ArcEngine控件
 
 ## 环境
 ### 开发环境
* Visual Studio 2017
* .NET Framework 4.0


 ### 调用组件
 引用的第三方dll已包含到[DLL](DLL)文件夹中
 * AppLibrary.dll 轻量级开源的Excel读写库
 * [GDAL](https://www.gdal.org/)  著名开源GIS开发库
 * [SharpZipLib](https://www.nuget.org/packages/SharpZipLib/) 文件压缩和解压缩库
 * [NPOI]()
 * [oracle.dataaccess]()
 * [System.Data.SQLite]()
 * [Newtonsoft.Json]()
 * [itextsharp]()
 
 
  ## 使用
 添加相关引用：
  ```cSharp
using WLib.ArcGis.Data;
using WLib.ArcGis.GeoDatabase.FeatClass;
using WLib.ArcGis.GeoDatabase.WorkSpace;
using WLib.Database;
using WLib.Database.DbBase;
```
使用示例：
```cSharp
private void Test()
 {
double sumRiverLength = 0.0;
FeatClassFromPath.FromPath(@"c:\River.shp").QueryFeatures(@"XZQDM = '440000'", feature => sumRiverLength +=feature.ToDouble("RiverLength"));

 var workspace = GetWorkspace.GetWorkSpace(@"c:\World.mdb"); 
FeatClassFromPath.FromPath(@"c:\World.mdb\river").CopyStruct(workspace, 
"NewRiver", "河流");

var connString = ConnStringHelper.Dbf_OleDb4(@"c:\River.dbf");

new DbHelper(connString, EDbProviderType.OleDb).ExcNonQuery(@"update 
River set Name = 'Pearl River' where RiverCode ='003'");

var connString2 = ConnStringHelper.Access_OleDb4(@"c:\World.mdb");
 }
```
