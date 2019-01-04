/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data.Common;

namespace WLib.Db.Base
{
    /// <summary>
    /// 提供获取指定类型数据库的操作、数据提供程序名的方法
    /// </summary>
    internal class ProviderFactory
    {
        /// <summary>
        /// 数据库类型及对应数据提供程序名
        /// </summary>
        private static readonly Dictionary<EDbProviderType, string> ProviderNameDict;
        /// <summary>
        /// 数据库类型及对应数据库的操作实例
        /// </summary>
        private static readonly Dictionary<EDbProviderType, DbProviderFactory> ProviderFactoryDict;

        /// <summary>
        /// 提供获取指定类型数据库的操作、获取数据提供程序名的方法
        /// </summary>
        static ProviderFactory()
        {
            ProviderFactoryDict = new Dictionary<EDbProviderType, DbProviderFactory>(20);
            ProviderNameDict = new Dictionary<EDbProviderType, string>();

            ProviderNameDict.Add(EDbProviderType.SqlServer, "System.Data.SqlClient");
            ProviderNameDict.Add(EDbProviderType.OleDb, "System.Data.OleDb");
            ProviderNameDict.Add(EDbProviderType.Odbc, "System.Data.ODBC");
            ProviderNameDict.Add(EDbProviderType.Oracle, "Oracle.DataAccess.Client");
            ProviderNameDict.Add(EDbProviderType.MySql, "MySql.Data.MySqlClient");
            ProviderNameDict.Add(EDbProviderType.SqLite, "System.Data.SQLite");
            ProviderNameDict.Add(EDbProviderType.Firebird, "FirebirdSql.Data.Firebird");
            ProviderNameDict.Add(EDbProviderType.PostgreSql, "Npgsql");
            ProviderNameDict.Add(EDbProviderType.Db2, "IBM.Data.DB2.iSeries");
            ProviderNameDict.Add(EDbProviderType.Informix, "IBM.Data.Informix");
            ProviderNameDict.Add(EDbProviderType.SqlServerCe, "System.Data.SqlServerCe");
        }
        /// <summary>
        /// 根据数据库类型获取所对应的数据提供程序名
        /// </summary>
        /// <param name="providerType">数据库类型</param>
        /// <returns></returns>
        internal static string GetProviderName(EDbProviderType providerType)
        {
            return ProviderNameDict[providerType];
        }
        /// <summary>
        /// 根据数据库类型获取所对应的数据库操作实例
        /// </summary>
        /// <param name="providerType"></param>
        /// <returns></returns>
        internal static DbProviderFactory GetDbProviderFactory(EDbProviderType providerType)
        {
            if (!ProviderFactoryDict.ContainsKey(providerType))
                ProviderFactoryDict.Add(providerType, ImportDbProviderFactory(providerType));
            return ProviderFactoryDict[providerType];
        }
        /// <summary>
        /// 根据数据库类型获取所对应的数据库操作实例
        /// </summary>
        /// <param name="providerType"></param>
        /// <returns></returns>
        private static DbProviderFactory ImportDbProviderFactory(EDbProviderType providerType)
        {
            string providerName = ProviderNameDict[providerType];
            DbProviderFactory factory = null;
            try
            {
                factory = DbProviderFactories.GetFactory(providerName);
            }
            catch (ArgumentException ex)
            {
                factory = null;
            }
            return factory;
        }
    }
}
