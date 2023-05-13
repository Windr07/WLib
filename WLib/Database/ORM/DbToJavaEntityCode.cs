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
    /// 根据数据库的表生成Java实体类代码
    /// <para>Generate Java entity class code based on database tables</para>
    /// </summary>
    [Serializable]
    public class DbToJavaEntityCode : DbToEntityCode
    {
        /// <summary>
        /// 实体类所在的包的名称
        /// </summary>
        public string Package { get; set; } = "spring.code.example.entity";
        /// <summary>
        /// 导入的包
        /// </summary>
        public List<string> Imports { get; set; } = new List<string>();
        //{
        //    "java.io.Serializable",
        //    "java.util.Date",
        //    "lombok.Data;",
        //    "com.baomidou.mybatisplus.annotation.IdType;",
        //    "com.baomidou.mybatisplus.annotation.TableId;",
        //    "com.baomidou.mybatisplus.annotation.TableName;",
        //    "com.baomidou.mybatisplus.extension.activerecord.Model;",
        //    "com.fasterxml.jackson.annotation.JsonFormat;",
        //    "io.swagger.annotations.Api;",
        //    "io.swagger.annotations.ApiModel;",
        //    "io.swagger.annotations.ApiModelProperty;",
        //};
        /// <summary>
        /// 在实体类名中加入的注解
        /// </summary>
        public List<string> ClassAnnotations { get; set; } = new List<string>();
        //{
        //    "Data",
        //    "ApiModel",
        //    "Api(tags = \"\")",
        //    "TableName(\"\")",
        //};
        /// <summary>
        /// 在实体类的每个属性（字段）中加入的注解
        /// </summary>
        public List<string> PropertyAnnotations { get; set; } = new List<string>();
        //{
        //    "ApiModelProperty(value = \"Name\", name = \"Name\")",
        //};
        /// <summary>
        /// 继承的父类
        /// </summary>
        public string Extends { get; set; } = "Model<{tableName}>";
        /// <summary>
        /// 实现的接口
        /// </summary>
        public string Implements { get; set; } = "Serializable";

        /// <summary>
        /// <see cref="DataColumn"/>中的基本类型的类型名和对应的Java类型名
        /// </summary>
        [Obsolete("该对应关系还有待验证")]
        public static readonly Dictionary<string, string> BaseTypeNameAndJavaTypes = new Dictionary<string, string>
        {
            {"String","String" },
            {"Byte","Byte" },
            {"Byte[]","Byte[]" },
            {"SByte","Byte" },
            {"Guid","String" },
            {"Int16","Short" },
            {"Int32","Integer" },
            {"Int64","Long" },
            {"UInt16","Short" },
            {"UInt32","Integer" },
            {"UInt64","Long" },
            {"Single","Float" },
            {"Double","Double" },
            {"Decimal","Double" },
            {"Boolean","Boolean" },
            {"DateTime","Date" },
            {"TimeSpan","Timestamp" },
        };

        /// <summary>
        /// 根据数据库的表生成Java实体类代码
        /// <para>Generate Java entity class code based on database tables</para>
        /// </summary>
        /// <param name="dbHelper"></param>
        public DbToJavaEntityCode() : base(null) => CodeFileExtension = ".java";
        /// <summary>
        /// 根据数据库的表生成Java实体类代码
        /// <para>Generate Java entity class code based on database tables</para>
        /// </summary>
        /// <param name="dbHelper"></param>
        public DbToJavaEntityCode(DbHelper dbHelper) : base(dbHelper) => CodeFileExtension = ".java";
        /// <summary>
        /// 根据数据库的表生成Java实体类代码
        /// <para>Generate Java entity class code based on database tables</para>
        /// </summary>
        public DbToJavaEntityCode(string connectionString, EDbProviderType dbType, int commandTimeOut = 30) :
            base(connectionString, dbType, commandTimeOut) => CodeFileExtension = ".java";


        /// <summary>
        /// 根据表格字段生成Java的实体类代码
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        protected override string CreateCodeByTable(string tableName, DataColumn[] columns)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"package {Package};");//包
            sb.AppendLine();

            Imports.ForEach(import => sb.AppendLine($"import {import};"));//引用（导入的包）
            sb.AppendLine();

            if (IncludeComments)//类注释
            {
                sb.AppendLine("/**");
                sb.AppendLine("*");
                sb.AppendLine("*@author");
                sb.AppendLine("*");
                sb.AppendLine("*/");
            }
            ClassAnnotations.ForEach(classAnnotation => sb.AppendLine($"@{classAnnotation}"));//类注解

            var extends = string.IsNullOrWhiteSpace(Extends) ? string.Empty : " extends " + Extends;
            var implements = string.IsNullOrWhiteSpace(Implements) ? string.Empty : " implements " + Implements;
            sb.AppendLine($"public class {tableName}{extends}{implements} {{");

            foreach (var column in columns)//私有字段
            {
                if (IncludeComments)
                {
                    sb.AppendLine("\t/**");
                    sb.AppendLine($"\t* {column.Caption}");
                    sb.AppendLine("\t*/");
                }
                PropertyAnnotations.ForEach(propertyAnnotation => sb.AppendLine($"\t@{propertyAnnotation}"));//属性注解
                sb.AppendLine($"\tprivate {BaseTypeNameAndJavaTypes[column.DataType.Name]} {column.ColumnName};");
                sb.AppendLine();
            }
            sb.AppendLine();

            foreach (var column in columns)//get和set方法
            {
                var name = column.ColumnName;
                var name2 = column.ColumnName.Substring(0, 1).ToUpper() + column.ColumnName.Substring(1);
                var typeName = BaseTypeNameAndJavaTypes[column.DataType.Name];
                sb.AppendLine($"\tpublic void set{name2}({typeName} {name}){{ this.{name} = {name}; }}");
                sb.AppendLine($"\tpublic {typeName} get{name2}(){{ return this.{name}; }}");
                sb.AppendLine();
            }
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
