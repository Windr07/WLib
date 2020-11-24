## DataTable扩展操作

示例1：表连接、表数据转JSON、文本

```C#
using WLib.Data;
```

```C#
var stTable = DataTableOpt.CreateTable("学生信息表", "ID","Name","Sex","Age","ClassID");//创建一个字符串字段的表
var clsTable = DataTableOpt.CreateTable("班级信息表","ID","Class");
//TODO:
//填充表格数据...
//...
var stClsTable = stTable.Join(clsTable, "ClassID", "ID","LeftJoin");//对表格进行左连接
var stClsTable2 = DataTableOpt.Transpose(stClsTable);//转置表格，即将行列对换

var json = stClsTable.ToJSON();
var text = stClsTable.ToText("\t");
```

示例2：表格转对象、对象转表格

```C#
class Student
{
    public string ID {get; set;}
    public string Name {get; set;}
    public string Sex {get; set;}
    public string Age {get; set;}
    public string ClassID {get; set;}
}
```

```C#
var students = stTable.ToObject<Student>();
var newTable = students.ToDataTable("学生信息表2");
```



