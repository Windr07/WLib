/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using WLib.Db.Base;

namespace WLib.Db
{
    /// <summary>
    /// 提供连接和使用SQL操作数据库(数据源)的方法
    /// （释放对象的同时自动释放连接）
    /// </summary>
    public class DbHelper : IDisposable
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        private DbConnection _connection;
        /// <summary>
        /// 表示一组方法，这些方法用于创建提供程序对数据源类的实现的实例
        /// </summary>
        private readonly DbProviderFactory _providerFactory;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 数据库连接
        /// </summary>
        protected DbConnection Connection
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
        /// 提供连接和使用SQL操作数据库(数据源)的方法
        /// （释放对象的同时自动释放连接）
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="providerType">数据库类型</param>
        public DbHelper(string connectionString, EDbProviderType providerType)
        {
            ConnectionString = connectionString;
            _providerFactory = ProviderFactory.GetDbProviderFactory(providerType);
            if (_providerFactory == null)
                throw new ArgumentException($"无法加载数据源类型为“{providerType.ToString()}”的数据库操作对象(ProviderFactory)");
        }


        /// <summary>
        /// 连接数据源，执行SQL语句并返回状态值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExcNonQuery(string sql)
        {
            OnBeforeExcute("Excute None Query", sql);
            DbCommand dbCommand = _providerFactory.CreateCommand();
            dbCommand.Connection = Connection;
            dbCommand.CommandText = sql;
            return dbCommand.ExecuteNonQuery();
        }
        /// <summary>
        /// 连接数据源，执行SQL语句并返回状态值
        /// </summary>
        /// <param name="sqlFormat"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int ExcNonQuery(string sqlFormat, params object[] args)
        {
            return ExcNonQuery(string.Format(sqlFormat, args));
        }
        /// <summary>
        /// 连接数据源，执行多条SQL语句并返回状态值
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public void ExcNonQuery(IEnumerable<string> sqls)
        {
            OnBeforeExcute("Excute None Query", sqls.Aggregate((a, b) => a + "," + b));
            DbCommand dbCommand = _providerFactory.CreateCommand();
            dbCommand.Connection = Connection;
            foreach (var sql in sqls)
            {
                dbCommand.CommandText = sql;
                dbCommand.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// 连接数据源，执行SQL语句并返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql)
        {
            OnBeforeExcute("Get Data Table", sql);
            DbDataAdapter adapter = _providerFactory.CreateDataAdapter();
            DbCommand command = _providerFactory.CreateCommand();
            DataSet dataSet = new DataSet();
            command.Connection = Connection;
            command.CommandText = sql;
            adapter.SelectCommand = command;
            adapter.Fill(dataSet);
            return dataSet.Tables[0];
        }
        /// <summary>
        /// 连接数据源，执行SQL语句并返回DataTable
        /// </summary>
        /// <param name="sqlFormat"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string sqlFormat, params object[] args)
        {
            return GetDataTable(string.Format(sqlFormat, args));
        }
        /// <summary>
        /// 连接数据源，执行查询得到第一行第一列的值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object ExcScalar(string sql)
        {
            OnBeforeExcute("Excute Scalar", sql);
            DbCommand command = _providerFactory.CreateCommand();
            command.Connection = Connection;
            command.CommandText = sql;
            return command.ExecuteScalar();
        }
        /// <summary>
        /// 连接数据源，执行查询得到第一行第一列的值（查询结果为空时返回default(T)，一般是null或0）
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public T ExcScalar<T>(string sql)
        {
            var obj = ExcScalar(sql);
            if (obj == null)
                return default(T);
            try
            {
                return (T)obj;
            }
            catch (Exception ex) { throw new Exception($"将ExcuteScalar方法执行SQL查询的结果“{obj}”({obj.GetType()})强制转换为类型“{typeof(T)}”的数据失败：{ex.Message}"); }
        }
        /// <summary>
        /// 连接数据源，执行查询得到第一行第一列的值（查询结果为空时返回default(T)，一般是null或0）
        /// </summary>
        /// <param name="sqlFormat"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public T ExcScalar<T>(string sqlFormat, params object[] args)
        {
            return ExcScalar<T>(string.Format(sqlFormat, args));
        }


        /// <summary>
        /// 释放对象的同时，关闭连接并释放资源
        /// </summary>
        public void Dispose()
        {
            Close();
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            _connection?.Close();
        }


        #region 事件
        /// <summary>
        /// 执行SQL语句之前的事件
        /// </summary>
        public EventHandler<BeforeExcuteEventArgs> BeforeExcute;
        /// <summary>
        /// 触发执行SQL语句之前的事件处理
        /// </summary>
        /// <param name="description">执行的sql操作的描述</param>
        /// <param name="sql"></param>
        protected void OnBeforeExcute(string description, string sql)
        {
            BeforeExcute?.Invoke(this, new BeforeExcuteEventArgs(description, sql));
        }
        #endregion
    }
}
