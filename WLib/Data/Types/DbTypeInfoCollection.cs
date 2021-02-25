/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020/9
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace WLib.Data.Types
{
    /// <summary>
    /// 数据库类型和C#类型关系信息集合
    /// </summary>
    public class DbTypeInfoCollection : List<DbTypeInfo>
    {
        /// <summary>
        /// 根据数据库类型，在集合中获取类型关系信息，找不到则返回null
        /// </summary>
        /// <param name="dbTypeName"></param>
        /// <returns></returns>
        public DbTypeInfo this[string dbTypeName]
        {
            get
            {
                dbTypeName = dbTypeName.ToLower();
                var result = this.FirstOrDefault(v => dbTypeName == v.DbTypeName.ToLower());
                if (result == null)
                    result = this.FirstOrDefault(v => dbTypeName.Contains(v.DbTypeName.ToLower()));
                return result;
            }
        }
        /// <summary>
        /// 根据C#类型，在集合中获取类型关系信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DbTypeInfo[] this[Type type] => this.Where(v => type.Equals(v.Type)).ToArray();
        /// <summary>
        /// 根据.NET类型，在集合中获取类型关系信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DbTypeInfo[] this[DbType type] => this.Where(v => type.Equals(v.DbType)).ToArray();


        /// <summary>
        /// 数据库类型和C#类型关系信息集合
        /// </summary>
        public DbTypeInfoCollection() { }
    }
}
