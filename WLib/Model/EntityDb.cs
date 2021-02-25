/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020/10
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using WLib.Database.DbBase;

namespace WLib.Model
{
    /// <summary>
    /// 实体数据库
    /// </summary>
    [Serializable]
    public class EntityDb : List<EntityGroup>
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public EDbProviderType DbType { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 数据库说明
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// 实体数据库
        /// </summary>
        public EntityDb() { }
        /// <summary>
        /// 实体数据库
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="dbType">数据库类型</param>
        public EntityDb(string connectionString, EDbProviderType dbType)
        {
            this.ConnectionString = connectionString;
            this.DbType = dbType;
        }
        /// <summary>
        /// 实体数据库
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="dbType">数据库类型</param>
        public EntityDb(string connectionString, EDbProviderType dbType, string name, string description = null) : this(connectionString, dbType)
        {
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// 输出数据库名称<see cref="Name"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Name;
    }
}
