## 样式和专题渲染

### 颜色转换

```C#
IColor icolor;
Color color1 = iColor.ToColor();//将ArcGIS Engine中的IColor接口转换至.NET中的Color

IRgbColor rgbColor = icolor as IRgbColor;
Color color2 = rgbColor.ToColor();//将ArcGIS Engine中的IRgbColor接口转换至.NET中的Color结构

Color color3 = Color.Blue;
IColor iColor2 = color3.ToIColor();//将.NET中的Color结构转换至于ArcGIS Engine中的IColor接口
```

### IColor颜色构建

```C#
var iColor1 = ColorCreate.GetIColor(255,255,255);
var iColor2 = ColorCreate.GetIColor("ff0000");
```

### Render图层渲染

```C#
var geoFeatureLayer = featureLayer as IGeoFeatureLayer ;

//
geoFeatureLayer.SimpleRenderer(
	mainColor:ColorCreate.GetIColor("ff0000"), 
	outlineColor:ColorCreate.GetIColor("00ff00"),
	transparency: 0,
	widthOrSize: 1);

//
geoFeatureLayer.ClassBreakRenderer();

//
geoFeatureLayer.UniqueValueRenderer();



```



### Symbol符号创建

```C#

```

- 上述示例仅展示部分方法，更多方法及其注释请调用时查看，或参阅[源码]()。

