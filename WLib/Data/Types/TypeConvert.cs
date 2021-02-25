/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020/9
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Data;
using System.Linq;

namespace WLib.Data.Types
{
    /// <summary>
    /// 常见数据库类型和C#类型的转换
    /// <para>可参考：https://www.cnblogs.com/XuYiHe/p/12093214.html </para>
    /// </summary>
    public static class TypeConvert
    {
        /// <summary>
        /// 常见数据库类型和C#类型关系信息
        /// </summary>
        public static readonly DbTypeInfoCollection DbTypeInfos = new DbTypeInfoCollection()
        {
            new DbTypeInfo(typeof(bool), DbType.Boolean, "bool", "布尔型"),
            new DbTypeInfo(typeof(bool), DbType.Boolean, "bit", "布尔型"),

            new DbTypeInfo(typeof(byte), DbType.Byte, "byte", "8位无符号整数"),
            new DbTypeInfo(typeof(byte), DbType.Byte, "tinyint", "8位无符号整数"),
            new DbTypeInfo(typeof(sbyte), DbType.SByte, "sbyte", "8位有符号整数"),
            new DbTypeInfo(typeof(short), DbType.Int16, "short", "短整数"),
            new DbTypeInfo(typeof(short), DbType.Int16, "smallint", "短整数"),
            new DbTypeInfo(typeof(ushort), DbType.UInt16, "ushort", "无符号短整数"),

            new DbTypeInfo(typeof(int), DbType.Int32, "int", "整数"),
            new DbTypeInfo(typeof(uint), DbType.UInt32, "uint", "无符号整数"),
            new DbTypeInfo(typeof(long), DbType.Int64, "long", "长整数"),
            new DbTypeInfo(typeof(long), DbType.Int64, "bigint", "长整数"),
            new DbTypeInfo(typeof(ulong), DbType.UInt64, "ulong", "无符号长整数"),
            
            new DbTypeInfo(typeof(float), DbType.Single, "float", "浮点数"),
            new DbTypeInfo(typeof(float), DbType.Single, "real", "浮点数"),
            new DbTypeInfo(typeof(double), DbType.Double, "double", "双精度浮点数"),
            new DbTypeInfo(typeof(decimal), DbType.Decimal, "decimal", "小数"),
            new DbTypeInfo(typeof(decimal), DbType.Decimal, "numeric ", "数值"),
            new DbTypeInfo(typeof(decimal), DbType.Decimal, "number", "数值"),
            new DbTypeInfo(typeof(decimal), DbType.Currency, "currency", "货币"),
            new DbTypeInfo(typeof(decimal), DbType.Currency, "money", "货币"),
            new DbTypeInfo(typeof(decimal), DbType.Currency, "smallmoney", "货币"),

            new DbTypeInfo(typeof(string), DbType.String, "string", "字符串"),
            new DbTypeInfo(typeof(string), DbType.AnsiStringFixedLength, "char", "字符串"),
            new DbTypeInfo(typeof(string), DbType.StringFixedLength, "nchar", "字符串"),
            new DbTypeInfo(typeof(string), DbType.AnsiString, "varchar", "字符串"),
            new DbTypeInfo(typeof(string), DbType.AnsiString, "varchar2", "字符串"),
            new DbTypeInfo(typeof(string), DbType.String, "nvarchar", "字符串"),
            new DbTypeInfo(typeof(string), DbType.AnsiString, "text", "文本"),
            new DbTypeInfo(typeof(string), DbType.String, "ntext", "文本"),

            new DbTypeInfo(typeof(object), DbType.Object, "object", "对象"),
            new DbTypeInfo(typeof(object), DbType.Object, "variant", "对象"),
            new DbTypeInfo(typeof(object), DbType.Object, "blob", "对象"),
            new DbTypeInfo(typeof(byte[]), DbType.Binary, "byte[]", "二进制流"),
            new DbTypeInfo(typeof(byte[]), DbType.Binary, "bytes", "二进制流"),
            new DbTypeInfo(typeof(byte[]), DbType.Binary, "binary", "二进制流"),
            new DbTypeInfo(typeof(byte[]), DbType.Binary, "varbinary", "二进制流"),
            new DbTypeInfo(typeof(byte[]), DbType.Binary, "image", "二进制流"),
            new DbTypeInfo(typeof(byte[]), DbType.Binary, "timestamp", "二进制流"),
            new DbTypeInfo(typeof(DateTime), DbType.DateTime, "date", "日期"),
            new DbTypeInfo(typeof(DateTime), DbType.DateTime, "datetime", "时间"),
            new DbTypeInfo(typeof(DateTime), DbType.DateTime, "smalldatetime", "时间"),
            new DbTypeInfo(typeof(DateTime), DbType.DateTime, "time", "时间"),
            new DbTypeInfo(typeof(Guid), DbType.Guid, "guid", "GUID"),
            new DbTypeInfo(typeof(Guid), DbType.Guid, "uniqueidentifier", "GUID"),

            new DbTypeInfo(typeof(string), DbType.Xml, "xml", "XML"),
            new DbTypeInfo(typeof(decimal), DbType.VarNumeric, "varnumeric", "变长数值"),
            new DbTypeInfo(typeof(DateTime), DbType.Time, "sqltime", "时间"),
            new DbTypeInfo(typeof(DateTime), DbType.DateTime2, "datetime2", "公元时间"),
            new DbTypeInfo(typeof(DateTime), DbType.DateTimeOffset, "datetimeoffset", "公元时间（含时区）"),
        };


        /// <summary>
        /// 根据数据库类型，获取对应的C#类型，找不到则返回null
        /// </summary>
        /// <param name="dbTypeName">数据库类型</param>
        /// <returns></returns>
        public static Type GetType(string dbTypeName) => DbTypeInfos[dbTypeName]?.Type;
        /// <summary>
        /// 根据.NET数据类型枚举，获取对应的C#类型，找不到则返回null
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static Type GetType(DbType dbType) => DbTypeInfos[dbType].FirstOrDefault()?.Type;
    }
}
