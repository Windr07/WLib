/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;

namespace WLib.ArcGis.GeoDatabase.Table
{
    //数据库方式连接dbf文件，常用两种方式：
    //
    //1、OleDb连接dbf
    //  (1)连接字符参考：Provider=MICROSOFT.JET.OLEDB.4.0;Data Source=dbf文件所在目录;Extended Properties=dBase IV;
    //  (2)查询语句：查询的sql语句，注意sql语句必须包含select xxx from [dbfPath]的部分，其中[dbfPath]就是dbf文件全路径
    //  (3)注意事项：连接的dbf文件，要求文件名长度要小于8个字符，否则连接失败
    //
    //2、ODBC连接dbf
    //  (1)确认安装了Microsoft Visual FoxPro Driver(vfpodbc.dll)
    //       控制面板 --> 管理工具 --> 数据源(ODBC) --> 驱动程序 --> Microsoft Visual FoxPro Driver
    //  (2)确认dbf数据库中有数据,并且数据路径正确无误
    //  (3)连接字符串参考：Driver={Microsoft Visual FoxPro Driver};SourceType=DBF;SourceDB=dbf文件所在目录;Exclusive=YES;NULL=NO;Collate=Machine;BACKGROUNDFETCH=YES;DELETED=NO;

    /// <summary>
    /// 提供OleDb连接dbf文件执行查询的方法
    /// </summary>
    [Obsolete("该类仅用作参考，请改用 WLib.Database.DbHelper类及WLib.Database.DbHelper.GetShpMdbGdbHelper方法创建对dbf或其他ArcGIS数据的连接和相关查询操作")]
    public class OleDbfHelper
    {
        private string _filePath;
        private string _fileDirectory;
        private string _connstr;

        /// <summary>
        /// dbf文件全路径
        /// （注意：OleDb连接dbf时有个大坑：dbf文件名称长度不能超过8）
        /// </summary>
        public string FilePath
        {
            get => _filePath;
            set
            {
                var name = Path.GetFileNameWithoutExtension(value);
                if (name != null && name.Length > 8)
                    throw new Exception("通过OleDb连接dbf时，限定dbf文件名称长度不能超过8个字符，请更改文件名称或者使用其他方式连接dbf文件");

                _filePath = value;
                _fileDirectory = Path.GetDirectoryName(_filePath);
                _connstr = $"Provider=MICROSOFT.JET.OLEDB.4.0;Data Source={_fileDirectory};Extended Properties=dBase IV;";
            }
        }
        /// <summary>
        /// 提供OleDb连接dbf文件执行查询的方法
        /// </summary>
        /// <param name="filePath"></param>
        public OleDbfHelper(string filePath)
        {
            FilePath = filePath;
        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql">查询的sql语句，注意sql语句必须包含select xxx from [dbfPath]的部分，其中[dbfPath]就是dbf文件全路径</param>
        public void ExecuteSql(string sql)
        {
            using (OleDbConnection conn = new OleDbConnection(_connstr))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// 执行查询，根据SQL得到DataSet
        /// </summary>
        /// <param name="sql">查询的sql语句，注意sql语句必须包含select xxx from [dbfPath]的部分，其中[dbfPath]就是dbf文件全路径</param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql)
        {
            using (OleDbConnection conn = new OleDbConnection(_connstr))
            {
                conn.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter odat = new OleDbDataAdapter(sql, conn);
                odat.Fill(ds);
                return ds;
            }
        }
        /// <summary>
        /// 执行查询，根据SQL得到DataTable
        /// </summary>
        /// <param name="sql">查询的sql语句，注意sql语句必须包含select xxx from [dbfPath]的部分，其中[dbfPath]就是dbf文件全路径</param>
        /// <returns></returns>
        public DataTable GetDataTab(string sql)
        {
            using (OleDbConnection conn = new OleDbConnection(_connstr))
            {
                conn.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter odat = new OleDbDataAdapter(sql, conn);
                odat.Fill(ds);
                return ds.Tables[0];
            }
        }
        /// <summary>
        /// 执行查询，根据SQL得到第一行第一列
        /// </summary>
        /// <param name="sql">查询的sql语句，注意sql语句必须包含select xxx from [dbfPath]的部分，其中[dbfPath]就是dbf文件全路径</param>
        /// <returns></returns>
        public object GetScalar(string sql)
        {
            using (OleDbConnection conn = new OleDbConnection(_connstr))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                return cmd.ExecuteScalar();
            }
        }
        /// <summary>
        /// 执行查询，根据SQL得到DataReader
        /// </summary>
        /// <param name="sql">查询的sql语句，注意sql语句必须包含select xxx from [dbfPath]的部分，其中[dbfPath]就是dbf文件全路径</param>
        /// <returns></returns>
        public DbDataReader GetDataReader(string sql)
        {
            using (OleDbConnection conn = new OleDbConnection(_connstr))
            {
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                OleDbDataReader odr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return odr;
            }
        }
        /// <summary>
        /// 使用指定的架构名称字符串和指定的限制值字符串数组，返回此Connection 的数据源的架构信息。
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="restrictionValues"></param>
        /// <returns></returns>
        public DataTable GetSchema(string collectionName, string[] restrictionValues)
        {
            using (OleDbConnection conn = new OleDbConnection(_connstr))
            {
                conn.Open();
                DataTable dt = conn.GetSchema(collectionName, restrictionValues);
                return dt;
            }
        }
    }
}
