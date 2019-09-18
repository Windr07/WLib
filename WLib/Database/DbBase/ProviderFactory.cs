﻿/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Data.Common;
using WLib.Attributes.Description;

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
            ProviderNameDict = EnumDescriptionHelper.GetValueAndDescriptionDict<EDbProviderType>();
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
