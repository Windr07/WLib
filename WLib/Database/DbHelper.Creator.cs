/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using WLib.Database.DbBase;

namespace WLib.Database
{
    /// <summary>
    /// 提供构建操作数据库(数据源)的帮助类的方法
    /// </summary>
    public partial class DbHelper
    {
        /// <summary>
        /// 构建操作数据库(数据源)的帮助类
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="providerType">数据库类型</param>
        /// <param name="commandTimeOut">执行一条命令的超时时间（以秒为单位）</param>
        public static DbHelper GetDbHelper(string connectionString, EDbProviderType providerType, int commandTimeOut = 30)
             => new DbHelper(connectionString, providerType, commandTimeOut);
        /// <summary>
        /// 构建OLEDB方式操作数据库的帮助类
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="providerType">数据库类型</param>
        /// <param name="commandTimeOut">执行一条命令的超时时间（以秒为单位）</param>
        public static DbHelper GetOleDbHelper(string connectionString, int commandTimeOut = 30)
             => new DbHelper(connectionString, EDbProviderType.OleDb, commandTimeOut);
        /// <summary>
        ///  构建OLEDB.4.0操作Access的帮助类
        /// </summary>
        /// <param name="filePath">Access数据库mdb文件路径</param>
        /// <param name="userId">连接数据库的用户名</param>
        /// <param name="password">连接数据库的密码</param>
        /// <example>
        /// Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\mydatabase.mdb;Jet OLEDB:System Database=system.mdw;User ID=myUsername;Password=myPassword;
        /// </example>
        /// <returns></returns>
        public static DbHelper Access4Helper(string filePath, string userId = null, string password = null)
            => new DbHelper(Access_OleDb4(filePath, userId, password), EDbProviderType.OleDb);
        /// <summary>
        /// 构建OLEDB.12.0操作Access的帮助类
        /// </summary>
        /// <param name="filePath">Access数据库mdb或accdb文件路径</param>
        /// <param name="password">连接数据库的密码</param>
        /// <example>
        /// Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\myFolder\myAccessFile.mdb;Jet OLEDB:Database Password=MyDbPassword;
        /// </example>
        /// <returns></returns>
        public static DbHelper Access12Helper(string filePath, string password = null)
            => new DbHelper(Access_OleDb12(filePath, password), EDbProviderType.OleDb);
        /// <summary>
        /// 构建OLEDB.4.0操作dbf文件的帮助类（此dbf文件为GIS的dbf文件）
        /// <para>
        /// //OleDb连接dbf（注意dbf文件名称长度不能超过8，否则出错）：
        /// new DbHelper(ConnStringHelper.Dbf_OleDb4("d:\tmp")).GetDataTable("select * from d:\tmp\a.dbf");
        /// </para>
        /// </summary>
        /// <param name="dbfDirectory">dbf文件所在文件夹目录（eg:d:\tmp）</param>
        /// <param name="extendedProperties">扩展属性，默认为："dBase IV"</param>
        /// <example>
        ///  var dbHelper = new DbHelper("Provider=MICROSOFT.JET.OLEDB.4.0;Data Source=d:\tmp;Extended Properties=dBase IV;");
        ///  var dataTable = dbHelper.GetDataTable("select * from d:\tmp\a.dbf");
        /// </example>
        /// <returns></returns>
        public static DbHelper Dbf4Helper(string dbfDirectory, string extendedProperties = "dBase IV")
            => new DbHelper(Dbf_OleDb4(dbfDirectory, extendedProperties), EDbProviderType.OleDb);
        /// <summary>
        /// 构建OLEDB.4.0操作xls，或OLEDB.12.0链接xlsx文件的帮助类，
        /// <para>查询语句示例：select * from [sheet1$]</para>
        /// </summary>
        /// <param name="filePath">Excel文件(xls或xlsx)的全路径</param>
        /// <param name="hdr">HeaDer Row，填写"YES"表示首行为列名（字段名），否则应填写"NO"表示不含列名，此时程序按F1,F2...依次命名字段</param>
        /// <param name="imex">IMport EXport mode, 0为写入，1为只读，2为读取/修改/更新</param>
        public static DbHelper ExcelHelper(string filePath, string hdr = "YES", int imex = 2)
            => new DbHelper(Excel_OleDb(filePath, hdr, imex), EDbProviderType.OleDb);
        /// <summary>
        ///  构建操作Oracle的帮助类（连接Oracle需要安装Oracle客户端，引用oracle.dataaccess.dll）
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        public static DbHelper OracleHelper(string dataSource, string userId, string password)
            => new DbHelper(Oracle(dataSource, userId, password), EDbProviderType.Oracle);
        /// <summary>
        ///  构建操作SQL Server的帮助类（通过.NET自带的System.Data.SqlClient组件连接sqlServer的帮助类）
        /// </summary>
        /// <param name="dataSource">服务器地址</param>
        /// <param name="iniCatalog">数据库名称</param>
        /// <param name="userId">用户名</param>
        /// <param name="password">密码</param>
        /// <example>Data Source=myServerAddress;Initial Catalog=myDataBase;User Id=myUsername;Password=myPassword;</example>
        /// <returns></returns>
        public static DbHelper SqlServerHelper(string dataSource, string iniCatalog, string userId, string password)
            => new DbHelper(SqlServer(dataSource, iniCatalog, userId, password), EDbProviderType.SqlServer);
        /// <summary>
        /// 构建ADO.NET操作MySql的帮助类
        /// （using MySql.Data.MySqlClient; 引用MySql.Data.dll，需在MySQL官网下载驱动程序：https://dev.mysql.com/downloads/connector/net/）
        /// </summary>
        /// <param name="serverAddress">服务器地址</param>
        /// <param name="database">数据库名称</param>
        /// <param name="userId">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static DbHelper MySqlHelper(string serverAddress, string database, string userId, string password)
            => new DbHelper(MySql(serverAddress, database, userId, password), EDbProviderType.MySql);
        /// <summary>
        /// 构建操作Sqlite的帮助类
        /// （需添加引用System.Data.SQLite.dll）
        /// （参考：https://www.cnblogs.com/luxiaoxun/p/3784729.html）
        /// </summary>
        /// <param name="filePath">sqlite数据库文件路径</param>
        /// <returns></returns>
        public static DbHelper SqliteHelper(string filePath)
            => new DbHelper(Sqlite(filePath), EDbProviderType.SqLite);
        /// <summary>
        /// 构建操作存储在内存中的Sqlite的帮助类
        /// （需添加引用System.Data.SQLite.dll）
        /// （参考：https://www.sqlite.org/inmemorydb.html）
        /// </summary>
        /// <returns></returns>
        public static DbHelper SqliteInMemoryHelper()
            => new DbHelper(Sqlite_InMemory(), EDbProviderType.SqLite);
    }
}
