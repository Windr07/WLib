/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Db.DbBase
{
    /// <summary>
    /// 数据库类型枚举（数据提供程序枚举）
    /// </summary>
    public enum EDbProviderType : byte
    {
        SqlServer,
        MySql,
        SqLite,
        Oracle,
        Odbc,
        OleDb,
        Firebird,
        PostgreSql,
        Db2,
        Informix,
        SqlServerCe
    }
}
