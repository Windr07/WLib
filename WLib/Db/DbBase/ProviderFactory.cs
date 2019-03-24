/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;

namespace WLib.Db.DbBase
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
            foreach (var enumName in System.Enum.GetNames(enumType))//获取EDbProviderType枚举的Description特性，即数据提供程序名
            {
                var attributes = (DescriptionAttribute[])enumType.GetField(enumName).GetCustomAttributes(typeof(DescriptionAttribute), false);
                ProviderNameDict.Add((EDbProviderType)System.Enum.Parse(enumType, enumName), attributes[0].Description);
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
            return ProviderFactoryDict[providerType];
        }
    }
}
