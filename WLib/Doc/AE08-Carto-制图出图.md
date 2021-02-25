## 制图和出图

### 导出地图

​	`WLib.ArcGis.Carto.MapExport.MapExportToPic`中提供了将地图导出为图片的扩展方法

```C#
var mapDoc = MapDocEx.OpenMapDoc(@"c:\myMap.mxd");//打开地图文档
var pageLayout = mapDoc.PageLayout;//获取地图文档中的页面布局对象
pageLayout.ExportToPicture(@"c:\myMap.jpg");//导出地图为图片,支持jpg,tiff,bmp,emf,png,gif,pdf,eps,ai,svg
```

### 制图和出图

* 简单的制图出图可以通过`WLib.ArcGis.Carto.MapExport.MapExportHelper`对象实现，该对象继承自[`WLib.ExtProgress.ProLogOperation`](07数据处理模板.md)；

* 批量的制图出图可以通过`WLib.ArcGis.Carto.MapExport.BatchMapExportOpt`对象实现

* 在此之前通过出图配置对象来`WLib.ArcGis.Carto.MapExport.MapExportInfo`对地图进行各项配置。之后可通过`WLib.WinCtrls.ProgressViewCtrl.ProgressViewManager`来辅助实现与界面UI的交互。使用示例如下：

```C#
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WLib.ArcGis.Carto.MapExport;
using WLib.ArcGis.Carto.MapExport.Base;
using WLib.WinCtrls.Dev.ProgressViewCtrl;
using WLib.WinCtrls.MessageCtrl;

namespace Sample
{
	public partial class DemoForm: Form
    {
        public ProgressViewManager ProViewManager { get; } = new ProgressViewManager() { MessageAppend = true };
        
        public static void BatchExportMap()
        {
            //设置各个专题图的图幅信息
            var cfgs = new List<MapExportInfo>();
            foreach (var mapName in thematicMaps)
            {
                var layerInfos = 专题图绑定.Where(v => !string.IsNullOrWhiteSpace(v.绑定的图层) && v.专题图名称 == mapName).
                    Select(v => new LayerInfo(v.图层名称, v.绑定的数据库, v.绑定的图层)).ToArray();

                var cfg = new MapExportInfo();
                cfg.CfgName = $"{regionName}{mapPreName}{mapName}";
                cfg.TemplateMxdPath = Path.Combine(TemplateMapDir, mapName + ".mxd");
                cfg.ExportDirectory = outpuDir;
                cfg.ExportFileName = $"{mapPreName}{mapName}";
                cfg.Elements.Add("标题", EPageElementType.Text, $"{mapPreName}{mapName}");
                cfg.Elements.Add("填写时间", EPageElementType.Text, strDateTime);
                cfg.Elements.Add("填写单位", EPageElementType.Text, company);
                cfg.MapFrames.Add(null, 0, null, layerInfos);
                if (exportPic) cfg.ExportPictures.Add(dpi, true, ".jpg");
                cfgs.Add(cfg);
            }

            //创建出图操作，执行批量出图
            ProViewManager.Opt = new BatchMapExportOpt("专题图编制", cfgs);
            ProViewManager.BindEvent(this, this.progressBarControl1, this.textBox1, null, ChangeViews);
            ProViewManager.Run();
        }
    }
}
```

- 上述示例仅展示部分方法，更多方法及其注释请调用时查看，或参阅[源码]()。