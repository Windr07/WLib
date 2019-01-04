/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Data;
using System.Data.OleDb;

namespace WLib.Files.Excel.MSExcel
{
    /// <summary>
    /// 提供通过OleDB连接和查询Excel表的方法
    /// </summary>
    public class MsExcelDbHelper
    {
        private string _excelFilePath;
        private OleDbConnection _connection;

        /// <summary>
        /// 提供通过OleDB连接和查询Excel表的方法
        /// </summary>
        public MsExcelDbHelper()
        {

        }
        /// <summary>
        /// 提供通过OleDB连接和查询Excel表的方法
        /// </summary>
        /// <param name="excelFilePath"></param>
        public MsExcelDbHelper(string excelFilePath)
        {
            this.ExcelFilePath = excelFilePath;
        }

        /// <summary>
        /// 进行操作的Excel文件路径
        /// </summary>
        public string ExcelFilePath
        {
            get { return _excelFilePath; }
            set
            {
                _excelFilePath = value;
                if (_connection != null)
                    _connection.Close();
                _connection = null;
            }
        }

        public OleDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    string connectionString = "";
                    string fileType = System.IO.Path.GetExtension(ExcelFilePath);
                    if (string.IsNullOrEmpty(fileType)) return null;
                    if (fileType == ".xls")
                        connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + ExcelFilePath + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=2\"";
                    else
                        connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + ExcelFilePath + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=2\"";

                    _connection = new OleDbConnection(connectionString);
                    _connection.Open();
                }
                else if (_connection.State == System.Data.ConnectionState.Closed)
                {
                    _connection.Open();
                }
                else if (_connection.State == System.Data.ConnectionState.Broken)
                {
                    _connection.Close();
                    _connection.Open();
                }
                return _connection;
            }
        }

        /// <summary> 
        /// 执行无参数的SQL语句 
        /// </summary> 
        /// <param name="sql">SQL语句（eg:select * from [sheet1$]）</param> 
        /// <returns>返回受SQL语句影响的行数</returns> 
        public int ExecuteSql(string sql)
        {
            OleDbCommand cmd = new OleDbCommand(sql, Connection);
            int result = cmd.ExecuteNonQuery();
            _connection.Close();
            return result;
        }

        /// <summary> 
        /// 执行有参数的SQL语句 
        /// </summary> 
        /// <param name="sql">SQL语句（eg:select * from [sheet1$]）</param>
        /// <param name="values">参数集合</param>
        /// <returns>返回受SQL语句影响的行数</returns>
        public int ExecuteSql(string sql, params OleDbParameter[] values)
        {
            OleDbCommand cmd = new OleDbCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            int result = cmd.ExecuteNonQuery();
            _connection.Close();
            return result;
        }

        /// <summary> 
        /// 返回单个值无参数的SQL语句 
        /// </summary> 
        /// <param name="sql">SQL语句（eg:select * from [sheet1$]）</param> 
        /// <returns>返回受SQL语句查询的行数</returns> 
        public object GetScalar(string sql)
        {
            OleDbCommand cmd = new OleDbCommand(sql, Connection);
            object result = cmd.ExecuteScalar();
            _connection.Close();
            return result;
        }

        /// <summary> 
        /// 返回单个值有参数的SQL语句 
        /// </summary> 
        /// <param name="sql">SQL语句（eg:select * from [sheet1$]）</param> 
        /// <param name="parameters">参数集合</param> 
        /// <returns>返回受SQL语句查询的行数</returns> 
        public object GetScalar(string sql, params OleDbParameter[] parameters)
        {
            OleDbCommand cmd = new OleDbCommand(sql, Connection);
            cmd.Parameters.AddRange(parameters);
            object result = cmd.ExecuteScalar();
            _connection.Close();
            return result;
        }

        /// <summary> 
        /// 执行查询无参数SQL语句 
        /// </summary> 
        /// <param name="sql">SQL语句（eg:select * from [sheet1$]）</param> 
        /// <returns>返回数据集</returns> 
        public DataSet GetDataSet(string sql)
        {
            OleDbDataAdapter da = new OleDbDataAdapter(sql, Connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            _connection.Close();
            return ds;
        }

        /// <summary> 
        /// 执行查询有参数SQL语句 
        /// </summary> 
        /// <param name="sql">SQL语句（eg:select * from [sheet1$]）</param> 
        /// <param name="parameters">参数集合</param> 
        /// <returns>返回数据集</returns> 
        public DataSet GetDataset(string sql, params OleDbParameter[] parameters)
        {
            OleDbDataAdapter da = new OleDbDataAdapter(sql, Connection);
            da.SelectCommand.Parameters.AddRange(parameters);
            DataSet ds = new DataSet();
            da.Fill(ds);
            _connection.Close();
            return ds;
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            Connection.Close();
        }
    }
}
