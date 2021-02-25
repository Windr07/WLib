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
using System.IO;
using System.Linq;
using System.Text;
using WLib.Database.DbBase;
using WLib.Events;

namespace WLib.Database.ORM
{
    /// <summary>
    /// 根据数据库的表生成实体类代码
    /// <para>Generate entity code based on database tables</para>
    /// </summary>
    public abstract class DbToEntityCode
    {
        /// <summary>
        /// 数据库连接帮助对象
        /// </summary>
        public DbHelper DBHelper { get; set; }
        /// <summary>
        /// 是否生成注释
        /// </summary>
        public bool IncludeComments { get; set; } = true;
        /// <summary>
        /// 生成代码文件时，代码文件的扩展名。
        /// <para>一般跟具体的编程语言有关，例如C#为".cs"，Java为".java"</para>
        /// </summary>
        protected string CodeFileExtension { get; set; } = ".txt";
        /// <summary>
        /// 在将数据库的表生成为实体类代码过程中发生异常时触发的事件
        /// </summary>
        public event EventHandler<EventArgs<Exception>> ErrorOccurred;
        /// <summary>
        /// 要生成实体类代码的表格
        /// </summary>
        public string[] TableNames { get; protected set; }


        /// <summary>
        /// 根据数据库的表生成实体类代码
        /// <para>Generate entity code based on database tables</para>
        /// </summary>
        /// <param name="dbHelper"></param>
        protected DbToEntityCode(DbHelper dbHelper) => DBHelper = dbHelper;


        /// <summary>
        /// 将数据库的表生成为实体类代码
        /// <para>返回值中，key为表名称，value为对应的实体类代码</para>
        /// <para>请使用<see cref="ErrorOccurred"/>事件捕获生成过程中发生的异常</para>
        /// </summary>
        /// <param name="tableNames">要生成实体类代码的表格，赋值为null则默认所有表格都生成实体类代码</param>
        /// <returns></returns>
        public Dictionary<string, string> ToEntityCode(string[] tableNames = null)
        {
            TableNames = tableNames;
            if (TableNames == null)
            {
                try { TableNames = GetTableNames().ToArray(); }
                catch (Exception ex) { ErrorOccurred?.Invoke(this, new EventArgs<Exception>(new Exception(ex.Message, ex))); }
            }

            var nameCodeDict = new Dictionary<string, string>();//key为表名称，value为对应的实体类代码
            foreach (var tableName in TableNames)
            {
                Console.WriteLine(tableName + " Creating...");
                try
                {
                    var columns = GetDataColumns(tableName);
                    var code = CreateCodeByTable(tableName, columns.ToArray());
                    nameCodeDict.Add(tableName, code);
                }
                catch (Exception ex)
                {
                    ErrorOccurred?.Invoke(this, new EventArgs<Exception>(new Exception($"[{tableName}] - Failed to generate entity class code: {ex.Message}", ex)));
                }
            }
            return nameCodeDict;
        }
        /// <summary>
        /// 将数据库的表生成为实体类代码，存在到指定目录下
        /// <para>请使用<see cref="ErrorOccurred"/>事件捕获生成过程中发生的异常</para>
        /// </summary>
        /// <param name="saveCodeDirectory">存储实体类代码的目录</param>
        /// <param name="encoding">代码的文本编码，未指定时将默认为UTF-8</param>
        public void ToEntityCode(string saveCodeDirectory, Encoding encoding = null)
        {
            try
            {
                Directory.CreateDirectory(saveCodeDirectory);
                foreach (var pair in ToEntityCode())
                    File.WriteAllText(Path.Combine(saveCodeDirectory, pair.Key + CodeFileExtension), pair.Value, encoding ?? Encoding.UTF8);
            }
            catch (Exception ex) { ErrorOccurred?.Invoke(this, new EventArgs<Exception>(new Exception(ex.Message, ex))); }
        }


        /// <summary>
        /// 查询数据库，获取其中的全部非系统表格的表名
        /// </summary>
        /// <param name="dbHelper"></param>
        /// <returns></returns>
        public IEnumerable<string> GetTableNames()
        {
            var dbHelper = DBHelper;
            DataTable dataTable = null;
            var type = dbHelper.ProviderType;
            switch (type)
            {
                case EDbProviderType.SqlServer:
                    dataTable = dbHelper.GetDataTable(@"SELECT NAME FROM SYSOBJECTS WHERE XTYPE='U'"); //XTYPE = 'U':表示所有用户表; 'S':表示所有系统表;
                    return dataTable.Rows.Cast<DataRow>().Select(row => row[0].ToString());
                case EDbProviderType.MySql:
                    var sql = @"select table_name from information_schema.tables";
                    if (!string.IsNullOrEmpty(dbHelper.Database)) sql += $" where table_schema='{dbHelper.Database}'";
                    dataTable = dbHelper.GetDataTable(sql);
                    return dataTable.Rows.Cast<DataRow>().Select(row => row[0].ToString());
                case EDbProviderType.SqLite:
                    dataTable = dbHelper.GetDataTable(@"SELECT name FROM sqlite_master WHERE type='table'");
                    return dataTable.Rows.Cast<DataRow>().Select(row => row[0].ToString());
                case EDbProviderType.Oracle:
                    dataTable = dbHelper.GetDataTable(@"SELECT table_name FROM user_tables");
                    return dataTable.Rows.Cast<DataRow>().Select(row => row[0].ToString());
                case EDbProviderType.Odbc:
                    throw new NotImplementedException();
                case EDbProviderType.OleDb:
                    if (dbHelper.ConnectionString.Contains(".mdb") || dbHelper.ConnectionString.Contains(".accdb"))
                    {
                        dataTable = dbHelper.GetSchema("Tables", new[] { null, null, null, "TABLE" });
                        return dataTable.Rows.Cast<DataRow>().Select(row => row["TABLE_NAME"].ToString());
                    }
                    throw new NotImplementedException();
                case EDbProviderType.Firebird:
                    throw new NotImplementedException();
                case EDbProviderType.PostgreSql:
                    dataTable = dbHelper.GetDataTable(@"SELECT tablename FROM pg_tables WHERE tablename NOT LIKE 'pg%' AND tablename NOT LIKE 'sql_%'");
                    return dataTable.Rows.Cast<DataRow>().Select(row => row[0].ToString());
                case EDbProviderType.Db2:
                    throw new NotImplementedException();
                case EDbProviderType.Informix:
                    throw new NotImplementedException();
                case EDbProviderType.SqlServerCe:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 查询数据库指定表，获取该表的字段信息
        /// </summary>
        /// <param name="dbHelper"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        protected IEnumerable<DataColumn> GetDataColumns(string tableName)
        {
            var dbHelper = DBHelper;
            string sql;
            var type = dbHelper.ProviderType;
            switch (type)
            {
                case EDbProviderType.SqlServer: sql = $"select top 1 * from [{tableName}]"; break;
                case EDbProviderType.MySql: sql = $"select * from `{tableName.ToUpper()}` limit 1"; break;
                case EDbProviderType.SqLite: sql = $"select * from [{tableName}] limit 1"; break;
                case EDbProviderType.Oracle: sql = $"select * from \"{tableName}\" where rownum = 1"; break;
                case EDbProviderType.Odbc: throw new NotImplementedException();
                case EDbProviderType.OleDb: sql = $"select top 1 * from [{tableName}]"; break;
                case EDbProviderType.Firebird: throw new NotImplementedException();
                case EDbProviderType.PostgreSql: sql = $"select * from [{tableName}] limit 1"; break;
                case EDbProviderType.Db2: throw new NotImplementedException();
                case EDbProviderType.Informix: throw new NotImplementedException();
                case EDbProviderType.SqlServerCe: throw new NotImplementedException();
                default: throw new NotImplementedException();
            }
            var dataTable = dbHelper.GetDataTable(sql);
            return dataTable.Columns.Cast<DataColumn>();
        }
        /// <summary>
        /// 根据表格字段生成实体代码
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        protected abstract string CreateCodeByTable(string tableName, DataColumn[] columns);
    }
}
