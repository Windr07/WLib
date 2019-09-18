/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using WLib.Attributes.Description;

namespace WLib.Database.DbBase
{
    /// <summary>
    /// 数据库类型枚举（数据提供程序枚举）
    /// </summary>
    public enum EDbProviderType : byte
    {
        [Description("System.Data.SqlClient")]
        SqlServer,
        [Description("MySql.Data.MySqlClient")]
        MySql,
        [Description("System.Data.SQLite")]
        SqLite,
        [Description("Oracle.DataAccess.Client")]
        Oracle,
        [Description("System.Data.ODBC")]
        Odbc,
        [Description("System.Data.OleDb")]
        OleDb,
        [Description("FirebirdSql.Data.Firebird")]
        Firebird,
        [Description("Npgsql")]
        PostgreSql,
        [Description("IBM.Data.DB2.iSeries")]
        Db2,
        [Description("IBM.Data.Informix")]
        Informix,
        [Description("System.Data.SqlServerCe")]
        SqlServerCe
    }
}
