/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/1/1
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Db
{
    /// <summary>
    /// 提供构建简单常用的连接字符串的方法
    /// <seealso cref="https://www.connectionstrings.com"/>
    /// </summary>
    public class ConnStringHelper
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
        /// 构建OLEDB.4.0连接dbf文件的连接字符串
        /// <code>
        /// //OleDb连接dbf（注意dbf文件名称长度不能超过8，否则出错）：
        /// new DbHelper(ConnStringHelper.Dbf_OleDb4("d:\tmp")).GetDataTab("select * from d:\tmp\a.dbf");
        /// </code>
        /// </summary>
        /// <param name="dbfDirectory">dbf文件所在文件夹目录（eg:d:\tmp）</param>
        /// <param name="extendedProperties">扩展属性，默认为："dBase IV"</param>
        /// <example>
        ///  var dbHelper = new DbHelper("Provider=MICROSOFT.JET.OLEDB.4.0;Data Source=d:\tmp;Extended Properties=dBase IV;");
        ///  var dataTable = dbHelper.GetDataTab("select * from d:\tmp\a.dbf");
        /// </example>
        /// <returns></returns>
        public static string Dbf_OleDb4(string dbfDirectory, string extendedProperties = "dBase IV")
        {
            return $"Provider=MICROSOFT.JET.OLEDB.4.0;Data Source={dbfDirectory};Extended Properties={extendedProperties};";
        }


        /// <summary>
        /// 构建OLEDB.4.0连接Excel(xls或xlsx)文件的连接字符串，
        /// 查询语句示例：select * from [sheet1$]
        /// </summary>
        /// <param name="filePath">Excel文件(xls或xlsx)的全路径</param>
        /// <param name="hdr"> HeaDer Row，表示Excel表的首行是列名(字段名)，只能填写"YES"表示首行为列名，或"NO"表示不含列名，程序按F1,F2...依次命名字段</param>
        /// <param name="imex">IMport EXport mode, 0为写入，1为只读，2为读取/修改/更新</param>
        public static string Excel_OleDb4(string filePath, string hdr = "YES", int imex = 2)
        {
            if (!System.IO.File.Exists(filePath))
                throw new Exception($"DataSource应为Excel文件路径，参数“{filePath}”不是已存在且可读的文件路径！");

            string extension = System.IO.Path.GetExtension(filePath);
            if (extension != ".xls" && extension != ".xlsx")
                throw new Exception($"DataSource应为Excel文件路径，参数“{filePath}”指定的不是可识别的Excel文件格式！");

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
        /// <param name="dataSource"></param>
        /// <param name="iniCatalog"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <example>Data Source=myServerAddress;Initial Catalog=myDataBase;User Id=myUsername;Password=myPassword;</example>
        /// <returns></returns>
        public static string SqlServer(string dataSource, string iniCatalog, string userId, string password)
        {
            return $"Data Source = {dataSource};Initial Catalog = {iniCatalog};User Id = {userId};Password = {password};";
        }
    }
}
