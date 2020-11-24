# WLib

## 简介

* WLib是一组对C#.NET和ArcGIS Engine开发常用代码进行封装的基础库和控件库；


## 主要内容

* **通用.NET功能**
  1. 数据库连接查询：[DbHelper](WLib/Doc/02数据库连接.md)
  2. 部分常用Windows API调用
  3. [计算机硬件信息获取](WLib/Doc/09计算机硬件信息获取)
  4. [压缩与解压缩](WLib/Doc/08压缩与解压缩)
  5. [部分反射操作（类型与程序集扩展）](WLib/Doc/10类型与程序集扩展)
  6. [DataTable对象扩展、数据转换](WLib/Doc/06DataTable扩展操作)
  7. [插件接口](WLib/Doc/03插件功能.md)
  8. [进度操作（数据处理模板）](WLib/Doc/07数据处理模板)
  9. [软件注册](WLib/Doc/05软件注册.md)
  10. [软件更新](WLib/Doc/04软件更新)
* **基于ArcGIS Engine的GIS数据读写、空间分析**
  1. [工作空间](WLib/Doc/AE-Workspace-工作空间读写.md)、[矢量](WLib/Doc/AE-FeatureClass-矢量数据读写.md)、[栅格](WLib/Doc/AE-Raster-栅格数据读写.md)、[表格数据读写](WLib/Doc/AE-Table-表格读写.md)
  2. [GP工具调用](WLib/Doc/AE-GPTool.md)
  3. [拓扑的判断、获取、创建、删除](WLib/Doc/ArcEngine-创建拓扑.md)
  4. [空间查询](WLib/Doc/AE-SpatialSearch-空间查询.md)
  5. [部分地理处理 / 空间分析](WLib/Doc/AE-SpatialAnalysis-空间分析.md)
  6. [地图和图层查询](WLib/Doc/)
  7. [标注、注记、元素增删改查](WLib/Doc/)
  8. [制图出图](WLib/Doc/)
  9. [样式、专题渲染](WLib/Doc/)
  10. [坐标系判读转换](WLib/Doc/)
  11. [点线面构建等几何操作](WLib/Doc/)
  12. [控件组合联动](WLib/Doc/)
  13. [许可初始化](WLib/Doc/)
* **环境检测**
  1. [获取安装的.NET版本](WLib/Doc/)
  2. [获取安装的ArcGIS版本](WLib/Doc/)
  3. [获取Windows操作系统版本](WLib/Doc/)

* **文件IO操作**

1. Excel读写
   2. Word读写
   3. pdf操作

* **WinForm控件库**
  * **GIS控件**
    1. 地图展示组合控件（MapViewer）
    2. 属性表控件
    3. 按属性查询控件
    4. 地图导航工具栏
    5. 工作空间选择控件
    6. 样式选择控件
    7. 数据选择控件（AddData）
  * **非GIS控件**
    1. 环境检测控件
    2. 简化版的Windows资源管理器
    3. 目录树控件、文件/目录选择等对话框扩展
    4. 文件查看器（程序嵌入器）
    5. ListBox控件扩展
    6. 消息框
    7. 路径选择框
    8. 插件管理控件
    9. 窗体查询
    10. WinForm的全局异常处理
    11. 实体数据编辑控件
    12. 注册机

## 项目说明

解决方案包含若干个项目（代码库），核心库为[WLib](WLib/Doc/)和[WLib.ArcGis](WLib/Doc/)

| 代码库                       | 说明                             | 内容                                                         |
| ---------------------------- | -------------------------------- | :----------------------------------------------------------- |
| WLib                         | 通用基础库                       | DbHelper、Windows API、硬件信息获取、压缩与解压缩、反射、DataTable转对象（简单ORM）、插件功能等 |
| WLib.ArcGis                  | ArcGIS基础库                     | 封装的ArcObject代码集，内容包含GP调用、拓扑、空间查询、地图查询、标注、注记、元素增删改查、制图出图、样式、专题渲染、坐标系判读转换、点线面几何创建、控件组合联动、许可初始化、shp/gdb/mdb/sde/sql/oledb连接和表格/矢量/栅格数据增删改查、数据统计、数据转换、类型转换等 |
| WLib.Envir                   | 环境检测库                       | 检测.NET版本、ArcGIS版本、Windows操作系统版本的代码          |
| WLib.Files                   | excel、Word、pdf读写库           | 包含[NPOI](WLib/Doc/https://github.com/tonyqus/npoi/)、[AppLibrary.dll](WLib/Doc/http://www.pudn.com/Download/item/id/2192742.html) 、[Microsoft.Office.Interop.Excel](WLib/Doc/)操作xls或xlsx；使用[Microsoft.Office.Interop.Word](WLib/Doc/)读写doc和docx；使用[itextsharp](WLib/Doc/)操作pdf文档 |
| WLib.Gdal                    | GDAL帮助库                       | 开源GIS库[GDAL(Geospatial Data Abstraction Library)](WLib/Doc/https://www.gdal.org/)的Helper代码 |
| WLib.WinCtrls                | WinForm控件库                    | 一些自定义的WinForm控件库                                    |
| WLib.WinCtrls.ArcGisCtrl     | WinForm+ArcGIS控件库             | 一些自定义的基于ArcEngine的WinForm控件库                     |
| WLib.WinCtrls.Dev            | DevExpress WinForm控件库         | 一些自定义的DevExpressWinForm                                |
| WLib.WinCtrls.Dev.ArcGisCtrl | DevExpress WinForm +ArcGIS控件库 | 一些自定义的基于ArcEngine的DevExpress WinForm控件            |

## 使用

下载源码，或通过nuget引用：

```nuget
Install-Package WLib
Install-Package WLib.ArcGis
Install-Package WLib.Envir
```

 ## 环境

 ### 开发环境

* Visual Studio 2019
* .NET Framework 4.0（以WLib.WinCtrls开头的4各项目库为.NET Framework 4.6）

* ArcGIS Engine 10.2（包含"ArcGis"关键字的项目，下载源码修改项目引用可切换为其他ArcGIS 版本）


 ### 调用组件

 部分项目引用了若干第三方开源代码库，包括但不限于：

 * AppLibrary.dll 轻量级开源的Excel读写库
 * [GDAL](WLib/Doc/https://www.gdal.org/)  开源GIS开发库
 * [SharpZipLib](WLib/Doc/https://www.nuget.org/packages/SharpZipLib/) 文件压缩和解压缩库
 * [NPOI](WLib/Doc/https://github.com/tonyqus/npoi)
 * [System.Data.SQLite](WLib/Doc/https://sqlite.org/index.html)
 * [Json.NET](WLib/Doc/https://www.newtonsoft.com/json)
 * [itextsharp](WLib/Doc/https://github.com/itext/itextsharp)
 * [Dapper](WLib/Doc/https://github.com/StackExchange/Dapper)

  ## 简单示例

以下为简单使用示例，更多说明请参阅[WIKI](WLib/Doc/ ../../wikis)

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
    IWorkspace workspace = WorkspaceEx.GetWorkSpace(@"c:\World.mdb");
    workspace.GetFeatureClassByName("River").CopyStruct(workspace, "NewRiver", "河流");

    //---------示例3：数据查询----------
    //数据库连接和SQL查询的方式获取shp、mdb、gdb、dbf数据
    DbHelper dbHelper1 = DbHelper.GetShpMdbGdbHelper(@"c:\River.dbf");
    System.Data.DataTable dataTable = dbHelper1.GetDataTable(@"select * from River where RiverName =  'Pearl River'");
```



