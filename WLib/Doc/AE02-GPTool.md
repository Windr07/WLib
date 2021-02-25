## GP工具调用

ArcEngine的GP工具高度封装，但报错机制极不友好，接口调用的坑太多，`WLib` 库通过` WLib.ArcGis.Analysis.Gp.GpHelper`对象协助对GP工具的使用。

### 使用示例

调用相交工具：

```C#
//将a.shp和b.shp图层进行相交，生成结果为resultClass，对应保存为result.shp图层；resultMessage为工具运行结果的信息或异常信息
var intersect = GpHelper.Intersect(@"c:\a.shp;c:\b.shp", @"c:\result.shp");
if(GpHelper.RunTool(intersect, out IFeatureClass resultClass, out string resultMessage))
    Console.WriteLine("Run successfully!");
else 
    Console.WriteLine(resultMessage);
```

若不需要获取结果，代码可简化为：

```C#
GpHelper.RunTool(GpHelper.Intersect(@"c:\a.shp;c:\b.shp", @"c:\result.shp"), out _); 
```

`GpHelper.RunTool`方法支持所有类型的GP工具，对于`GpHelper`类未封装的工具，用ArcEngine原生的接口创建即可。

```C#
Dissolve dissolve =  new Dissolve()//融合工具
{
    in_features = @"c:\a.shp",
    out_feature_class = @"c:\result.shp",
    multi_part = "SINGLE_PART"
};
GpHelper.RunTool(dissolve, out _);//运行融合工具
```

###  GP工具运行失败的检查

`GpHelper`类中提供一些静态方法，用于在GP工具调用失败时查找原因或给用户输出提示消息：

```C#
//检查路径及数据是否符合GP工具规范，包括路径长度、特殊字符、小数点、空格的限制，文件或图层占用限制
//符合则规范返回null,否则返回提示信息
string message = GpHelper.CheckClassValidate(@"c:\a.shp");
Console.WriteLine(message);
```

其他检查方法请参考[`GpHelper.Check.cs`]()文件中的方法。

以下是GP工具调用成功条件的不完全总结：

```C#
 	/** GP工具调用成功条件总结（可调用GpHelper.CheckClassValidate()等方法进行检验）
     * 1、运行问题：部分GP工具要先在ArcMap上运行一遍
     * 2、补丁问题：若是ArcGIS 10.0，要安装SP5
     * 3、权限问题：
     *   1)Lincense控件设置：Products为权限最高的ArcInfo(在License控件中不要同时选中其他项)， 并选择扩展项：Spatial Analyst
     *   2)LicenseInitializer类设置：只能设置esriLicenseProductCode.esriLicenseProductCodeAdvanced，或者esriLicenseProductCodeDesktop，不要再加其他权限
     * 4、参数问题：(具体参考ArcGIS帮助文档)
     *   1)参数为whereClause：sql语句不能有误且要与数据库类型适配，例如：
     *     mdb: select [fieldName] from City
     *     shp: select "fieldName" from City
     *     gdb: select  fieldName  from City
     *   2)参数为图层或表名：开头不能是数字，不能加上".shp"，或不能有小数点/空格/其他特殊字符
     *   3)参数为路径：路径不能太深，确保输入路径的对象存在，输出目录存在但输出位置不能有同名文件/图层/表等对象
     *   4)参数为路径：一些GP工具要求路径中不能有小数点、空格或其他特殊字符
     *   5)输入输出参数有些情况不能为featureClass或featureLayer，有些情况则不能为路径
     * 5、锁定问题：GP操作和输入、输出的文件/要素类是否被锁定
     * 6、坐标系问题：输入各个图层坐标系是否一致（除坐标系名称外，具体坐标系参数也必须一致，是否Unknown坐标系）
     * 7、数据问题：不能有空几何等情况，注意使用几何修复
     *  （ArcToolbox -> Datamanagement tools -> features -> check geometry / repair geometry）
     * 8、特殊情况：安装Desktop和SDK后，再安装AE Runtime，则GP工具调用失败且程序直接崩溃，卸载Runtime后，GP工具调用成功
     * 超出边界问题：坐标系统的XY属性域过小
     * 
     * GP工具效率低、问题多、成功率低，无法调试，能使用其他方式实现功能则最好不用GP工具
     * GIS数据操作优选：SQL和.NET > ArcEngine一般接口 > GP工具
     */
```

- 上述示例仅展示部分方法，更多方法及其注释请调用时查看，或参阅[源码]()。









