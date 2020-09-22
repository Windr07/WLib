/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;

namespace WLib.Database.DbBase
{
    /// <summary>
    /// 提供获取指定类型数据库的操作、数据提供程序名的方法
    /// </summary>
    internal class ProviderFactory
    {
        /// <summary>
        /// 数据库类型(key)及对应数据提供程序名(value)
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
            ProviderFactoryDict = new Dictionary<EDbProviderType, DbProviderFactory>();

            ProviderNameDict = new Dictionary<EDbProviderType, string>();
            var enumType = typeof(EDbProviderType);
            foreach (var name in Enum.GetNames(enumType))
            {
                var fieldInfo = enumType.GetField(name);
                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                ProviderNameDict.Add((EDbProviderType)Enum.Parse(enumType, name), attributes[0].Description);
            }
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
                ProviderFactoryDict.Add(providerType, DbProviderFactories.GetFactory(ProviderNameDict[providerType]));
            //在.net core中使用下面这行代码
            //ProviderFactoryDict.Add(providerType, GetDbProviderFactory2(providerType));
            return ProviderFactoryDict[providerType];
        }
        /// <summary>
        /// 根据数据库类型获取所对应的数据库操作实例
        /// <para>.NET Core中没有提供DbProviderFactories类及其GetFactory方法，使用本方法代替</para>
        /// </summary>
        /// <param name="providerType"></param>
        /// <returns></returns>
        internal static DbProviderFactory GetDbProviderFactory2(EDbProviderType providerType)
        {
            switch (providerType)
            {
                case EDbProviderType.SqlServer: return GetFactory("System.Data.SqlClient.SqlClientFactory, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
                case EDbProviderType.SqlServerCe: return GetFactory("System.Data.SqlServerCe.SqlCeProviderFactory, System.Data.SqlServerCe, Culture=neutral, PublicKeyToken=89845dcd8080cc91");
                case EDbProviderType.MySql: return GetFactory("MySql.Data.MySqlClient.MySqlClientFactory, MySql.Data, Culture=neutral, PublicKeyToken=c5687fc88969c44d");
                case EDbProviderType.SqLite: return GetFactory("System.Data.SQLite.SQLiteFactory, System.Data.SQLite", "Microsoft.Data.Sqlite.SqliteFactory, Microsoft.Data.Sqlite");
                case EDbProviderType.Oracle:
                    return GetFactory("Oracle.ManagedDataAccess.Client.OracleClientFactory, Oracle.ManagedDataAccess, Culture=neutral, PublicKeyToken=89b483f429c47342",
"Oracle.DataAccess.Client.OracleClientFactory, Oracle.DataAccess");
                case EDbProviderType.Firebird: return GetFactory("FirebirdSql.Data.FirebirdClient.FirebirdClientFactory, FirebirdSql.Data.FirebirdClient");
                case EDbProviderType.PostgreSql: return GetFactory("Npgsql.NpgsqlFactory, Npgsql, Culture=neutral, PublicKeyToken=5d8b90d52f46fda7");
                case EDbProviderType.OleDb: return GetFactory("System.Data.OleDb.OleDbFactory, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
                case EDbProviderType.Odbc:
                case EDbProviderType.Db2:
                case EDbProviderType.Informix:
                default: return null;
            }
        }
        /// <summary>
        /// 根据数据提供程序的类型名，查找类型，获取所对应的数据库操作实例
        /// </summary>
        /// <param name="assemblyQualifiedNames"></param>
        /// <returns></returns>
        private static DbProviderFactory GetFactory(params string[] assemblyQualifiedNames)
        {
            Type type = null;
            foreach (var assemblyName in assemblyQualifiedNames)
            {
                type = Type.GetType(assemblyName);
                if (type != null)
                    break;
            }
            if (type == null)
                throw new ArgumentException($"Could not load the DbProviderFactory by assemblyName: {assemblyQualifiedNames[0]}");

            return (DbProviderFactory)type.GetField("Instance").GetValue(null);
        }

    }
}
