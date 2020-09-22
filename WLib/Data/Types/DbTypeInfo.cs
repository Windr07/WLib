using System;
using System.Data;

namespace WLib.Data.Types
{
    /// <summary>
    /// 数据库类型和C#类型关系信息
    /// </summary>
    public class DbTypeInfo
    {
        /// <summary>
        /// C#类型
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string DbTypeName { get; set; }
        /// <summary>
        /// 类型标题
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// 对应的.NET类型
        /// </summary>
        public DbType DbType { get; set; }


        /// <summary>
        /// 数据库类型和C#类型关系信息
        /// </summary>
        public DbTypeInfo() {  }
        /// <summary>
        /// 数据库类型和C#类型关系信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dbType"></param>
        /// <param name="dbTypeName"></param>
        /// <param name="caption"></param>
        public DbTypeInfo(Type type, DbType dbType, string dbTypeName, string caption)
        {
            Type = type;
            DbType = dbType;
            DbTypeName = dbTypeName;
            Caption = caption;
        }
    }

}
