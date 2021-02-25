## 表格数据操作

```C#
//打开指定路径下的表格
ITable table = TableEx.FromPath(@"c:\world.gdb\population");//打开gdb数据库下，名称为population的表格

//创建表格
IWorkspace workspace = WorkspaceEx.GetOrCreateWorkspace(@"c:\tmp.gdb");//在指定位置下创建工作空间
ITable tableOne = (workspace as IFeatureWorkspace).CreateTable("TableOne");//在工作空间下创建表格
ITable tableTwo = (workspace as IFeatureWorkspace).CreateTable(tableOne, "TableTwo");//复制表格

//查询记录或值
List<IRow> rows = table.QueryRows("ProjectType = 3", row.get_Value(row.Fields.FindField("ProjectName"));
IRow row = table.QueryFirstRow("ProjectT = 'testProject'");
int count = table.QueryCount();
List<object> values = table.GetUniqueValues("ProjectName");
List<object> values2 = table.QueryValues("FID");

//新增记录
table.InsertRows(10, (row, index) => //插入十条记录，分别对Number字段赋值为1至10
                 row.set_Value(row.Fields.FindField("Number"), index + 1));

//删除记录
table.DeleteRows("FID = 233");

//修改记录
table.UpdateRows("model like 'P%'"， row => 
	row.set_Value(row.Fields.FindField("model"), "Plus"));

//其他操作示例
table.Rename("GreatProject");//重命名
string sourcePath = table.GetSourcePath();//获取存储路径
bool isLock = table.IsLock(out string message);//判断表格是否被占用

```

* 上述示例仅展示部分方法，更多方法及其注释请调用时查看，或参阅[源码]()。


