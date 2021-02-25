/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
// sorc:  https://gitee.com/windr07/WLib
//        https://github.com/Windr07/WLib
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using WLib.Database.DbBase;

namespace WLib.Database
{
    /// <summary>
    /// 实现<see cref="IDbConnection"/>，并且提供连接、使用SQL操作不同类型的数据库(数据源)的方法；
    /// <para>同时提供构建各类连接字符串的方法；</para>
    /// </summary>
    public partial class DbHelper : IDbConnection
    {
        #region 私有成员
        private const string Excute_None_Query = "Excute None Query";
        private const string Get_Data_Set = "Get Data Set";
        private const string Excute_Scalar = "Excute Scalar";

        /// <summary>
        /// 数据库连接
        /// </summary>
        private DbConnection _connection;
        /// <summary>
        /// 表示一组方法，这些方法用于创建提供程序对数据源类的实现的实例
        /// </summary>
        private readonly DbProviderFactory _providerFactory;
        #endregion


        #region 属性事件
        /// <summary>
        /// 数据库类型枚举（数据提供程序枚举）
        /// </summary>
        public EDbProviderType ProviderType { get; set; }
        /// <summary>
        /// 执行一条命令的超时时间（以秒为单位）
        /// </summary>
        public int CommandTimeOut { get; set; }
        /// <summary>
        /// 连接数据库的超时时间（以秒为单位）
        /// </summary>
        public int ConnectionTimeOut => Connection.ConnectionTimeout;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 数据库连接
        /// </summary>
        public DbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = _providerFactory.CreateConnection();
                    if (_connection == null)
                        throw new Exception("通过ProviderFactory创建数据库连接失败！");

                    _connection.ConnectionString = ConnectionString;
                    _connection.Open();
                }
                else if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }
                else if (_connection.State == ConnectionState.Broken)
                {
                    _connection.Close();
                    _connection.Open();
                }

                return _connection;
            }
        }
        /// <summary>
        /// 执行SQL查询之前的事件
        /// </summary>
        public EventHandler<ExcuteEventArgs> PreExcute;
        /// <summary>
        /// 执行SQL查询之后的事件
        /// </summary>
        public EventHandler<ExcuteEventArgs> AfterExcute;
        /// <summary>
        /// 触发执行SQL查询之前的事件处理
        /// </summary>
        /// <param name="description">执行的sql操作的描述</param>
        /// <param name="cmd"></param>
        protected void OnPreExcute(string description, IDbCommand cmd) => PreExcute?.Invoke(this, new ExcuteEventArgs(description, cmd));
        /// <summary>
        /// 触发执行SQL查询之后的事件处理
        /// </summary>
        /// <param name="description">执行的sql操作的描述</param>
        /// <param name="cmd"></param>
        protected void OnAfterExcute(string description, IDbCommand cmd) => AfterExcute?.Invoke(this, new ExcuteEventArgs(description, cmd));
        #endregion


        #region 构造函数
        /// <summary>
        /// 提供连接和使用SQL操作不同类型的数据库(数据源)的方法
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="providerType">数据库类型</param>
        /// <param name="commandTimeOut">执行一条命令的超时时间（以秒为单位）</param>
        protected DbHelper(string connectionString, EDbProviderType providerType, int commandTimeOut = 30)
        {
            ConnectionString = connectionString;
            ProviderType = providerType;
            CommandTimeOut = commandTimeOut;
            _providerFactory = ProviderFactory.GetDbProviderFactory(providerType);
            if (_providerFactory == null)
                throw new ArgumentException($"无法加载数据源类型为“{providerType.ToString()}”的数据库操作对象(ProviderFactory)");
        }
        #endregion


        #region 同步查询方法
        /// <summary>
        /// 连接数据源，执行SQL语句并返回状态值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExcNonQuery(string sql) => ExcNonQuery(sql, null);
        /// <summary>
        /// 连接数据源，执行多条SQL语句
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public void ExcNonQuery(IEnumerable<string> sqls)
        {
            DbCommand dbCommand = _providerFactory.CreateCommand();
            dbCommand.Connection = Connection;
            dbCommand.CommandTimeout = CommandTimeOut;
            foreach (var sql in sqls)
            {
                OnPreExcute(Excute_None_Query, dbCommand);
                dbCommand.CommandText = sql;
                dbCommand.ExecuteNonQuery();
                OnAfterExcute(Excute_None_Query, dbCommand);
            }
        }
        /// <summary>
        /// 连接数据源，执行SQL语句并返回状态值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbParameters"></param>
        /// <returns></returns>
        public int ExcNonQuery(string sql, params DbParameter[] dbParameters)
        {
            DbCommand dbCommand = _providerFactory.CreateCommand();
            dbCommand.Connection = Connection;
            dbCommand.CommandTimeout = CommandTimeOut;
            dbCommand.CommandText = sql;
            if (dbParameters != null)
                dbCommand.Parameters.AddRange(dbParameters);

            OnPreExcute(Excute_None_Query, dbCommand);
            int lineCnt = dbCommand.ExecuteNonQuery();
            OnAfterExcute(Excute_None_Query, dbCommand);
            return lineCnt;
        }

        /// <summary> 
        /// 连接数据源，执行SQL语句并返回Dataset
        /// </summary> 
        /// <param name="sql">SQL语句</param> 
        /// <returns>返回数据集</returns> 
        public DataSet GetDataset(string sql)
        {
            return GetDataset(sql, null);
        }
        /// <summary> 
        /// 连接数据源，执行SQL语句并返回Dataset
        /// </summary> 
        /// <param name="sql">SQL语句</param> 
        /// <param name="dbParameters">参数集合</param> 
        /// <returns>返回数据集</returns> 
        public DataSet GetDataset(string sql, params DbParameter[] dbParameters)
        {
            DbDataAdapter adapter = _providerFactory.CreateDataAdapter();
            DbCommand dbCommand = _providerFactory.CreateCommand();
            dbCommand.Connection = Connection;
            dbCommand.CommandTimeout = CommandTimeOut;
            dbCommand.CommandText = sql;
            if (dbParameters != null)
                dbCommand.Parameters.AddRange(dbParameters);
            adapter.SelectCommand = dbCommand;

            OnPreExcute(Get_Data_Set, dbCommand);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            OnAfterExcute(Get_Data_Set, dbCommand);
            return dataSet;
        }

        /// <summary>
        /// 连接数据源，执行SQL语句并返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql)
        {
            return GetDataset(sql).Tables[0];
        }
        /// <summary>
        /// 连接数据源，执行SQL语句并返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="tableName">表格名称</param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql, string tableName)
        {
            var dataTable = GetDataset(sql).Tables[0];
            dataTable.TableName = tableName;
            return dataTable;
        }
        /// <summary>
        /// 连接数据源，执行SQL语句并返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbParameters"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql, params DbParameter[] dbParameters)
        {
            return GetDataset(sql, dbParameters).Tables[0];
        }

        /// <summary>
        /// 连接数据源，执行查询得到第一行第一列的值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object ExcScalar(string sql)
        {
            return ExcScalar(sql, null);
        }
        /// <summary>
        /// 连接数据源，执行查询得到第一行第一列的值，强制转换成指定类型的值
        /// （查询结果为空时返回default(T)，一般是null或0）
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"></exception>
        public T ExcScalar<T>(string sql)
        {
            var obj = ExcScalar(sql);
            if (obj == null || obj == DBNull.Value)
                return default(T);
            try
            {
                return (T)obj;
            }
            catch (Exception ex) { throw new InvalidCastException($"将ExcuteScalar方法执行SQL查询的结果“{obj}”({obj.GetType()})强制转换为类型“{typeof(T)}”的数据失败：{ex.Message}"); }
        }
        /// <summary>
        /// 连接数据源，执行查询得到第一行第一列的值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbParameters"></param>
        /// <returns></returns>
        public object ExcScalar(string sql, params DbParameter[] dbParameters)
        {
            DbCommand command = _providerFactory.CreateCommand();
            command.Connection = Connection;
            command.CommandTimeout = CommandTimeOut;
            command.CommandText = sql;
            if (dbParameters != null)
                command.Parameters.AddRange(dbParameters);

            OnPreExcute(Excute_Scalar, command);
            var @object = command.ExecuteScalar();
            OnAfterExcute(Excute_Scalar, command);
            return @object;
        }
        #endregion


        #region  异步查询方法
        /// <summary>
        /// 异步操作：连接数据源，执行SQL语句并返回状态值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public Task<int> ExcNonQueryAsync(string sql) => Task.Factory.StartNew(() => ExcNonQuery(sql));
        /// <summary>
        /// 异步操作：连接数据源，执行多条SQL语句
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public Task ExcNonQueryAsync(IEnumerable<string> sqls) => Task.Factory.StartNew(() => ExcNonQuery(sqls));
        /// <summary>
        /// 异步操作：连接数据源，执行SQL语句并返回状态值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbParameters"></param>
        /// <returns></returns>
        public Task<int> ExcNonQueryAsync(string sql, params DbParameter[] dbParameters) => Task.Factory.StartNew(() => ExcNonQuery(sql, dbParameters));

        /// <summary> 
        /// 异步操作：连接数据源，执行SQL语句并返回Dataset
        /// </summary> 
        /// <param name="sql">SQL语句</param> 
        /// <returns>返回数据集</returns> 
        public Task<DataSet> GetDatasetAsync(string sql) => Task.Factory.StartNew(() => GetDataset(sql));
        /// <summary> 
        /// 异步操作：连接数据源，执行SQL语句并返回Dataset
        /// </summary> 
        /// <param name="sql">SQL语句</param> 
        /// <param name="dbParameters">参数集合</param> 
        /// <returns>返回数据集</returns> 
        public Task<DataSet> GetDatasetAsync(string sql, params DbParameter[] dbParameters) => Task.Factory.StartNew(() => GetDataset(sql, dbParameters));

        /// <summary>
        /// 异步操作：连接数据源，执行SQL语句并返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public Task<DataTable> GetDataTableAsync(string sql) => Task.Factory.StartNew(() => GetDataTable(sql));
        /// <summary>
        /// 异步操作：连接数据源，执行SQL语句并返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="tableName">表格名称</param>
        /// <returns></returns>
        public Task<DataTable> GetDataTableAsync(string sql, string tableName) => Task.Factory.StartNew(() => GetDataTable(sql, tableName));
        /// <summary>
        /// 异步操作：连接数据源，执行SQL语句并返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbParameters"></param>
        /// <returns></returns>
        public Task<DataTable> GetDataTableAsync(string sql, params DbParameter[] dbParameters) => Task.Factory.StartNew(() => GetDataTable(sql, dbParameters));

        /// <summary>
        /// 异步操作：连接数据源，执行查询得到第一行第一列的值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public Task<object> ExcScalarAsync(string sql) => Task.Factory.StartNew(() => ExcScalar(sql));
        /// <summary>
        /// 异步操作：连接数据源，执行查询得到第一行第一列的值，强制转换成指定类型的值
        /// （查询结果为空时返回default(T)，一般是null或0）
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"></exception>
        public Task<T> ExcScalarAsync<T>(string sql) => Task.Factory.StartNew(() => ExcScalar<T>(sql));
        /// <summary>
        /// 异步操作：连接数据源，执行查询得到第一行第一列的值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbParameters"></param>
        /// <returns></returns>
        public Task<object> ExcScalarAsync(string sql, params DbParameter[] dbParameters) => Task.Factory.StartNew(() => ExcScalar(sql, dbParameters));
        #endregion


        #region 其他方法
        /// <summary>
        /// 获取数据源架构信息
        /// 参数<paramref name="collectionName"/>可以通过：
        /// <para><see cref="System.Data.OleDb.OleDbMetaDataCollectionNames"/></para>
        /// <para>或<see cref="System.Data.SqlClient.SqlClientMetaDataCollectionNames"/></para>
        /// <para>或<see cref="System.Data.Odbc.OdbcMetaDataCollectionNames"/></para>
        /// <para>等对象中提供的常数列表进行设置</para>
        /// e.g. 获取Access数据库的全部用户表的名称等信息为：
        /// <code>var dataTable = dbHelper.GetSchema("Tables", new string[] { null, null, null, "TABLE" });</code>
        /// </summary>
        /// <returns></returns>
        public DataTable GetSchema(string collectionName, string[] restrictionValues = null)
        {
            return restrictionValues == null ?
                Connection.GetSchema(collectionName) :
                Connection.GetSchema(collectionName, restrictionValues);
        }
        #endregion


        #region 实现IDbConnection接口
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close() => _connection?.Close();

        public int ConnectionTimeout => Connection.ConnectionTimeout;

        public string Database => Connection.Database;

        public ConnectionState State => Connection.State;

        public IDbTransaction BeginTransaction() => Connection.BeginTransaction();

        public IDbTransaction BeginTransaction(IsolationLevel il) => Connection.BeginTransaction(il);

        public IDbCommand CreateCommand() => Connection.CreateCommand();

        public void Dispose() => _connection?.Dispose();

        public void ChangeDatabase(string databaseName) => Connection.ChangeDatabase(databaseName);

        public void Open() => _connection?.Open();
        #endregion
    }
}
