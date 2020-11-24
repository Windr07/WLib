### 栅格数据操作

```C#
//打开指定路径下的栅格数据集
var rasterDataset = RasterEx.FromPath(@"c:\gis\xxx.tif");//bmp、tif、tiff、jpg、img路径或mdb、gdb、sde下的栅格集
var rasterDatasets = RasterEx.AllFromPath(@"c:\map.gdb");//打开指定目录或数据库下的栅格数据集

var rasterLayer = RasterEx.GetRasterLayer(@"c:\gis\xxx.tif");//获取栅格图层
rasterDataset.CreateRasterPyramid();//创建影像金字塔
```

* 上述为示例代码，详情请在调用时或源码中查看方法注释。

* 上述示例方法包含多个重载方法，更多实现方法请查看[源码]()。