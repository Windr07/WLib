/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/1/1
// desc： 构建连接字符串，也可直接参考https://www.connectionstrings.com/
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Database
{
    /// <summary>
    /// 提供构建简单常用的连接字符串的方法
    /// <para>连接字符串可参考</para>
    /// <para>①https://www.connectionstrings.com/ </para>
    /// <para>②https://blog.csdn.net/Xiongchao99/article/details/51670139 </para>
    /// <para>③https://www.connectionstrings.com/net-framework-data-provider-for-ole-db/ </para>
    /// </summary>
    public partial class DbHelper
    {
        /// <summary>
        ///  构建OLEDB.4.0连接Access的连接字符串
        /// </summary>
        /// <param name="filePath">Access数据库mdb文件路径</param>
        /// <param name="userId">连接数据库的用户名</param>
        /// <param name="password">连接数据库的密码</param>
        /// <example>
        /// Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\mydatabase.mdb;Jet OLEDB:System Database=system.mdw;User ID=myUsername;Password=myPassword;
        /// </example>
        /// <returns></returns>
        public static string Access_OleDb4(string filePath, string userId = null, string password = null)
        {
            var conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={filePath};";
            if (!string.IsNullOrWhiteSpace(userId)) conStr += $"User Id={userId};";
            if (!string.IsNullOrWhiteSpace(password)) conStr += $"Password={password};";
            return conStr;
        }
        /// <summary>
        /// 构建OLEDB.12.0连接Access的连接字符串
        /// </summary>
        /// <param name="filePath">Access数据库mdb或accdb文件路径</param>
        /// <param name="password">连接数据库的密码</param>
        /// <example>
        /// Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\myFolder\myAccessFile.mdb;Jet OLEDB:Database Password=MyDbPassword;
        /// </example>
        /// <returns></returns>
        public static string Access_OleDb12(string filePath, string password = null)
        {
            var conStr = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};";
            if (!string.IsNullOrWhiteSpace(password)) conStr += $"Jet OLEDB:Database Password={password};";
            return conStr;
        }
        /// <summary>
        /// 构建OLEDB.4.0连接dbf文件的连接字符串（此dbf文件为GIS的dbf文件）
        /// <code>
        /// //OleDb连接dbf（注意dbf文件名称长度不能超过8，否则出错）：
        /// new DbHelper(ConnStringHelper.Dbf_OleDb4("d:\tmp")).GetDataTable("select * from d:\tmp\a.dbf");
        /// </code>
        /// </summary>
        /// <param name="dbfDirectory">dbf文件所在文件夹目录（eg:d:\tmp）</param>
        /// <param name="extendedProperties">扩展属性，默认为："dBase IV"</param>
        /// <example>
        ///  var dbHelper = new DbHelper("Provider=MICROSOFT.JET.OLEDB.4.0;Data Source=d:\tmp;Extended Properties=dBase IV;");
        ///  var dataTable = dbHelper.GetDataTable("select * from d:\tmp\a.dbf");
        /// </example>
        /// <returns></returns>
        public static string Dbf_OleDb4(string dbfDirectory, string extendedProperties = "dBase IV")
        {
            return $"Provider=MICROSOFT.JET.OLEDB.4.0;Data Source={dbfDirectory};Extended Properties={extendedProperties};";
        }
        /// <summary>
        /// 构建OLEDB.4.0连接xls，或OLEDB.12.0链接xlsx文件的连接字符串，
        /// 查询语句示例：select * from [sheet1$]
        /// </summary>
        /// <param name="filePath">Excel文件(xls或xlsx)的全路径</param>
        /// <param name="hdr">HeaDer Row，填写"YES"表示首行为列名（字段名），否则应填写"NO"表示不含列名，此时程序按F1,F2...依次命名字段</param>
        /// <param name="imex">IMport EXport mode, 0为写入，1为只读，2为读取/修改/更新</param>
        public static string Excel_OleDb(string filePath, string hdr = "YES", int imex = 2)
        {
            if (!System.IO.File.Exists(filePath))
                throw new Exception($"filePath应为Excel文件路径，参数“{filePath}”不是已存在且可读的文件路径！");

            hdr = !string.IsNullOrWhiteSpace(hdr) && hdr.Trim().ToUpper() == "YES" ? "YES" : "NO";
            imex = imex < 0 || imex > 2 ? 2 : imex;

            var extension = System.IO.Path.GetExtension(filePath);
            if (extension != ".xls" && extension != ".xlsx")
                throw new Exception($"filePath应为Excel文件路径，参数“{filePath}”指定的不是可识别的Excel文件格式！");

            if (extension == ".xls")
                return $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={filePath};Extended Properties='Excel 8.0;HDR={hdr};IMEX={imex}'";
            else
                return $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties='Excel 12.0;HDR={hdr};IMEX={imex}'";
        }
        /// <summary>
        ///  构建连接Oracle的连接字符串（连接Oracle需要安装Oracle客户端，引用oracle.dataaccess.dll）
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        public static string Oracle(string dataSource, string userId, string password)
        {
            return $"Data Source={dataSource}; User ID={userId};Password={password};";
        }
        /// <summary>
        ///  构建连接SQL Server的连接字符串（通过.NET自带的System.Data.SqlClient组件连接sqlServer的连接字符串）
        /// </summary>
        /// <param name="dataSource">服务器地址</param>
        /// <param name="iniCatalog">数据库名称</param>
        /// <param name="userId">用户名</param>
        /// <param name="password">密码</param>
        /// <example>Data Source=myServerAddress;Initial Catalog=myDataBase;User Id=myUsername;Password=myPassword;</example>
        /// <returns></returns>
        public static string SqlServer(string dataSource, string iniCatalog, string userId, string password)
        {
            return $"Data Source={dataSource};Initial Catalog={iniCatalog};User Id={userId};Password={password};";
        }
        /// <summary>
        /// 构建ADO.NET连接MySql的连接字符串
        /// （using MySql.Data.MySqlClient; 引用MySql.Data.dll，需在MySQL官网下载驱动程序：https://dev.mysql.com/downloads/connector/net/）
        /// </summary>
        /// <param name="serverAddress">服务器地址</param>
        /// <param name="database">数据库名称</param>
        /// <param name="userId">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static string MySql(string serverAddress, string database, string userId, string password)
        {
            return $"Server={serverAddress};Database={database};Uid={userId};Pwd={password};";
        }
        /// <summary>
        /// 构建连接Sqlite的连接字符串
        /// （需添加引用System.Data.SQLite.dll）
        /// （参考：https://www.cnblogs.com/luxiaoxun/p/3784729.html）
        /// </summary>
        /// <param name="filePath">sqlite数据库文件路径</param>
        /// <returns></returns>
        public static string Sqlite(string filePath)
        {
            return $"Data Source={filePath};Version=3;";
        }
        /// <summary>
        /// 构建连接存储在内存中的Sqlite的连接字符串
        /// （需添加引用System.Data.SQLite.dll）
        /// （参考：https://www.sqlite.org/inmemorydb.html）
        /// </summary>
        /// <returns></returns>
        public static string Sqlite_InMemory()
        {
            return $"Data Source=:memory:;Version=3;New=True;";
        }
    }
}
