## 栅格数据操作

```C#
//打开指定路径下的栅格数据集
var rasterDataset = RasterEx.FromPath(@"c:\gis\xxx.tif");//bmp、tif、tiff、jpg、img路径或mdb、gdb、sde下的栅格集
var rasterDatasets = RasterEx.AllFromPath(@"c:\map.gdb");//打开指定目录或数据库下的栅格数据集

var rasterLayer = RasterEx.GetRasterLayer(@"c:\gis\xxx.tif");//获取栅格图层
rasterDataset.CreateRasterPyramid();//创建影像金字塔
```

* 上述示例仅展示部分方法，更多方法及其注释请调用时查看，或参阅[源码]()。
