/*---------------------------------------------------------------- 
// auth： Windragon
//        Windr07
// date： 2020/12
// desc： None
// mdfy:  None
// sorc:  https://gitee.com/windr07/WLib
//        https://github.com/Windr07/WLib
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using WLib.Database.DbBase;

namespace WLib.Database.ORM
{
    /// <summary>
    /// 根据数据库的表生成C#实体类代码
    /// <para>Generate C# entity class code based on database tables</para>
    /// </summary>
    [Serializable]
    public class DbToCSharpEntityCode : DbToEntityCode
    {
        /// <summary>
        /// 实体类的命名空间
        /// </summary>
        public string NameSpace { get; set; } = "MyLib.Model";
        /// <summary>
        /// 引用的命名空间
        /// </summary>
        public List<string> Usings { get; set; } = new List<string>();
        /// <summary>
        /// 在实体类名中加入的特性
        /// </summary>
        public List<string> ClassAttributes { get; set; } = new List<string>();
        /// <summary>
        /// 在实体类的每个属性（字段）中加入的特性
        /// </summary>
        public List<string> PropertyAttributes { get; set; } = new List<string>();
        /// <summary>
        /// 继承的父类或接口，多个则以逗号隔开
        /// </summary>
        public string Inherits { get; set; }
        /// <summary>
        /// <see cref="DataColumn"/>中的基本类型的类型名和对应的C#关键字
        /// <para>参考：https://docs.microsoft.com/zh-cn/dotnet/api/system.data.datacolumn.datatype?view=net-5.0</para>
        /// </summary>
        public static readonly Dictionary<string, string> BaseTypeNameAndKeywords = new Dictionary<string, string>
        {
            {"String","string" },
            {"Byte","byte" },
            {"Int16","short" },
            {"Int32","int" },
            {"Int64","long" },
            {"Single","float" },
            {"Double","double" },
            {"Decimal","decimal" },
            {"Boolean","bool" },
            {"Byte[]","byte[]" },
        };


        /// <summary>
        /// 根据数据库的表生成C#实体类代码
        /// <para>Generate C# entity class code based on database tables</para>
        /// </summary>
        public DbToCSharpEntityCode() : base(null) => CodeFileExtension = ".cs";
        /// <summary>
        /// 根据数据库的表生成C#实体类代码
        /// <para>Generate C# entity class code based on database tables</para>
        /// </summary>
        /// <param name="dbHelper"></param>
        public DbToCSharpEntityCode(DbHelper dbHelper) : base(dbHelper) => CodeFileExtension = ".cs";
        /// <summary>
        /// 根据数据库的表生成C#实体类代码
        /// <para>Generate C# entity class code based on database tables</para>
        /// </summary>
        public DbToCSharpEntityCode(string connectionString, EDbProviderType dbType, int commandTimeOut = 30) :
            base(connectionString, dbType, commandTimeOut) => CodeFileExtension = ".cs";


        /// <summary>
        /// 根据表格字段生成C#的实体类代码
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        protected override string CreateCodeByTable(string tableName, DataColumn[] columns)
        {
            var sb = new StringBuilder();
            Usings.ForEach(@using => sb.AppendLine($"using {@using};"));//引用

            sb.AppendLine();
            sb.AppendLine($"namespace {NameSpace}");//命名空间
            sb.AppendLine("{");

            if (IncludeComments)//类注释
            {
                sb.AppendLine("\t/// <summary>");
                sb.AppendLine($"\t/// {tableName}");
                sb.AppendLine("\t/// </summary>");
            }
            ClassAttributes.ForEach(classAttribute => sb.AppendLine($"\t[{classAttribute}]"));//类特性

            var inherits = string.IsNullOrWhiteSpace(Inherits) ? string.Empty : " : " + Inherits;
            sb.AppendLine($"\tpublic class {tableName}{inherits}");
            sb.AppendLine("\t{");

            foreach (var column in columns)//属性
            {
                if (IncludeComments)
                {
                    sb.AppendLine("\t\t/// <summary>");
                    sb.AppendLine($"\t\t/// {column.Caption}");
                    sb.AppendLine("\t\t/// </summary>");
                }
                PropertyAttributes.ForEach(propertyAttribute => sb.AppendLine($"\t\t[{propertyAttribute}]"));

                var colTypeName = column.DataType.Name;
                var typeName = BaseTypeNameAndKeywords.ContainsKey(colTypeName) ? BaseTypeNameAndKeywords[colTypeName] : colTypeName;
                sb.AppendLine($"\t\tpublic {typeName} {column.ColumnName} {{ get; set; }}");
            }
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
