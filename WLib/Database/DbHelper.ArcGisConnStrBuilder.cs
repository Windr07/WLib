/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/27 11:31:17
// desc： 构建连接ArcGIS的shp/gdb/mdb/sde等数据的连接字符串，以便通过ADO.NET使用sql操作相关GIS数据
// mdfy:  None
// sorc:  https://gitee.com/windr07/WLib
//        https://github.com/Windr07/WLib
//----------------------------------------------------------------*/

using System;
using System.IO;

namespace WLib.Database
{
    /** OLEDB方式连接ArcGIS数据注意事项：
     * http://resources.esri.com/help/9.3/ArcGISEngine/dotnet/0ec7f577-5dbd-4a60-b1f3-d5ef4a1426e4.htm
     *      1、需要ESRI OLE DB提供程序，该程序是Desktop的一部分，使用ESRI OLE DB需要安装ArcGIS Desktop
     *      2、不同数据源使用的SQL语句细节上有不同的要求，例如查询语句的区别：
     *          mdb: select [fieldName] from City
     *          gdb: select  fieldName  from City
     *          shp: select "fieldName" from City
     *          dbf: select  fieldName  from c:\data\City.dbf
     *      3、不同数据源可进行的操作的区别：
     *          mdb：可执行select,update,insert,delete操作
     *          gdb：可执行select,update,insert,delete操作
     *          sde：未测试（update,insert,delete操作应该需要先注册表格）
     *          shp：只能执行select操作
     *          dbf：可执行select操作，update,insert,delete操作不稳定
     *      4、查询完成后要及时关闭连接，否则因数据占用等原因后续查询出错
     */

    /// <summary>
    /// 提供构建连接ArcGIS数据的连接字符串的方法
    /// <para>需要安装ESRI OLE DB提供程序（随安装ArcGIS Desktop提供），注意事项请参考本类所在文件的注释部分）</para>
    /// <para>http://resources.esri.com/help/9.3/ArcGISEngine/dotnet/0ec7f577-5dbd-4a60-b1f3-d5ef4a1426e4.htm </para>
    /// <para>var dbHelper = DbHelper.GetDbHelper(DbHelper.ShpMdbGdb(@""), DbBase.EDbProviderType.OleDb);</para>
    /// </summary>
    public partial class DbHelper
    {
        /// <summary>
        /// 构造OleDb连接shp/dbf/mdb/gdb的连接字符串
        /// <para>①shp只能查询，增删改属性表请操作dbf；mdb,gdb,dbf可以增删改查，但是增、删、改有很多坑暂不建议使用，建议只进行简单select查询</para>
        /// <para>②sourcePath参数可以是shp目录或shp/dbf/mdb/gdb文件路径，不包含.gdb后缀目录将认为是shp目录</para>
        /// </summary>
        /// <param name="sourcePath">shp目录或shp/dbf/mdb/gdb文件路径，不包含.gdb后缀目录将认为是shp目录</param>
        /// <param name="geometry">值为"WKB"和"OBJECT"之一</param>
        /// <returns></returns>
        public static string ShpMdbGdb(string sourcePath, string geometry = "WKB")
        {

            if (File.Exists(sourcePath))
            {
                var extension = Path.GetExtension(sourcePath);
                if (extension == ".shp") return Shp(sourcePath, geometry);
                if (extension == ".dbf") return Dbf(sourcePath);
                if (extension == ".mdb") return Mdb(sourcePath, geometry);
            }
            if (Directory.Exists(sourcePath))
            {
                return sourcePath.EndsWith(".gdb") ?
                    Gdb(sourcePath, geometry) :
                    ShpDir(sourcePath, geometry);
            }

            throw new Exception($"数据源路径“{sourcePath}”不存在，或该数据源不是shp/dbf/mdb/gdb数据！");
        }
        /// <summary>
        /// 构造ESRI OleDb连接Access *.mdb个人地理数据库的连接字符串
        /// </summary>
        /// <param name="mdbPath">Access数据库mdb文件路径</param>
        /// <param name="geometry">值为"WKB"和"OBJECT"之一</param>
        /// <returns></returns>
        public static string Mdb(string mdbPath, string geometry = "WKB")
        {
            return $"Provider=ESRI.GeoDB.OLEDB.1;Data Source={mdbPath};Extended Properties=workspacetype=esriDataSourcesGDB.AccessWorkspaceFactory.1;Geometry={geometry}";
        }
        /// <summary>
        /// 构造ESRI OleDb连接*.gdb文件地理数据库的连接字符串
        /// </summary>
        /// <param name="gdbDir">文件地理数据库gdb文件夹目录</param>
        /// <param name="geometry">值为"WKB"和"OBJECT"之一</param>
        /// <returns></returns>
        public static string Gdb(string gdbDir, string geometry = "WKB")
        {
            return $"Provider=ESRI.GeoDB.OLEDB.1;Data Source={gdbDir};Extended Properties=WorkspaceType=esriDataSourcesGDB.FileGDBWorkspaceFactory.1;Geometry={geometry}";
        }
        /// <summary>
        /// 构造ESRI OleDb连接Shapefile文件的连接字符串
        /// <para>shp只能查询，例如SELECT * FROM River.shp或SELECT * FROM River</para>
        /// </summary>
        /// <param name="shpPath">shp文件路径</param>
        /// <param name="geometry">值为"WKB"和"OBJECT"之一</param>
        /// <returns></returns>
        public static string Shp(string shpPath, string geometry = "WKB")
        {
            return $"Provider=ESRI.GeoDB.OLEDB.1;Data Source={Path.GetDirectoryName(shpPath)};Extended Properties=WorkspaceType=esriDataSourcesFile.ShapefileWorkspaceFactory.1;Geometry={geometry}";
        }
        /// <summary>
        /// 构造ESRI OleDb连接Shapefile文件的连接字符串
        /// <para>shp只能查询，例如SELECT * FROM River.shp或SELECT * FROM River</para>
        /// </summary>
        /// <param name="shpDir">shp文件所在目录</param>
        /// <param name="geometry">值为"WKB"和"OBJECT"之一</param>
        /// <returns></returns>
        public static string ShpDir(string shpDir, string geometry = "WKB")
        {
            return $"Provider=ESRI.GeoDB.OLEDB.1;Data Source={shpDir};Extended Properties=WorkspaceType=esriDataSourcesFile.ShapefileWorkspaceFactory.1;Geometry={geometry}";
        }
        /// <summary>
        /// 构造ESRI OleDb连接coverage文件的连接字符串
        /// </summary>
        /// <param name="coverageDir">coverage文件所在目录</param>
        /// <param name="geometry">值为"WKB"和"OBJECT"之一</param>
        /// <returns></returns>
        public static string Coverage(string coverageDir, string geometry = "WKB")
        {
            return $"Provider=ESRI.GeoDB.OleDB.1;Data Source={coverageDir};Extended Properties=workspacetype=esriDataSourcesFile.ArcInfoWorkspaceFactory.1;Geometry={geometry}";
        }
        /// <summary>
        /// 构造OleDb连接dbf文件的连接字符串
        /// <para>注意：OleDb连接dbf的BUG：dbf文件名称长度不能超过8</para>
        /// <para>注意：sql语句必须包含select xxx from dbfPath的部分，其中dbfPath就是dbf文件全路径，例如：select * from c:\data\test.dbf</para>
        /// </summary>
        /// <param name="dbfPath">dbf文件路径</param>
        /// <returns></returns>
        public static string Dbf(string dbfPath)
        {
            var name = Path.GetFileNameWithoutExtension(dbfPath);
            if (name != null && name.Length > 8)
                throw new Exception("通过OleDb连接dbf时，限定dbf文件名称长度不能超过8个字符，请更改文件名称或者使用其他方式连接dbf文件");

            var dbfDirectory = Path.GetDirectoryName(dbfPath);
            return $"Provider=MICROSOFT.JET.OLEDB.4.0;Data Source={dbfDirectory};Extended Properties=dBase IV;";
        }
        /// <summary>
        /// 构造OleDb连接dbf文件的连接字符串
        /// <para>注意：OleDb连接dbf的BUG：dbf文件名称长度不能超过8</para>
        /// <para>注意：sql语句必须包含select xxx from dbfPath的部分，其中dbfPath就是dbf文件全路径，例如：select * from c:\data\test.dbf</para>
        /// </summary>
        /// <param name="dbfDirectory">dbf文件所在目录</param>
        /// <returns></returns>
        public static string DbfDir(string dbfDirectory)
        {
            return $"Provider=MICROSOFT.JET.OLEDB.4.0;Data Source={dbfDirectory};Extended Properties=dBase IV;";
        }
        /// <summary>
        /// 构造ESRI OleDb连接SDE地理数据库的连接字符串
        /// </summary>
        /// <param name="serverName">服务器名称，e.g. ditu.test.com</param>
        /// <param name="dataSource">数据库名称，e.g. sde_test</param>
        /// <param name="userName">用户名，e.g. sa</param>
        /// <param name="password">密码，e.g. sa</param>
        /// <param name="instance">实例，即sde端口号，e.g. 5151</param>
        /// <param name="geometry">值为"WKB"和"OBJECT"之一</param>
        /// <param name="version">ArcSDE版本， e.g. SDE.DEFAULT</param>
        /// <returns></returns>
        public static string Sde(string serverName, string dataSource, string userName,
            string password, string instance = "5151", string geometry = "WKB", string version = "SDE.DEFAULT")
        {
            return $"Provider=ESRI.GeoDB.OLEDB.1;Location={serverName};Data Source={dataSource};User Id={userName};Password={password};" +
                   $"Extended Properties=WorkspaceType=esriDataSourcesGDB.SDEWorkspaceFactory.1;Geometry={geometry};Instance={instance};Version={version}";
        }
        /// <summary>
        /// 构造ESRI OleDb连接SDE地理数据库的连接字符串，通过读取连接文件（.sde）进行连接
        /// </summary>
        /// <param name="connectFile">e.g. C:\\Temp\\MySdeConnection.sde</param>
        /// <returns></returns>
        public static string SdeFile(string connectFile)
        {
            return $"Provider=ESRI.GeoDB.OleDB.1;Extended Properties=workspacetype=esriDataSourcesGDB.SdeWorkspaceFactory.1;ConnectionFile={connectFile}";
        }
    }
}
