/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020/10
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Model
{
    /// <summary>
    /// 实体数据信息
    /// </summary>
    [Serializable]
    public class EntityInfo
    {
        /// <summary>
        /// 实体数据类型
        /// </summary>
        public Type EntityType { get; set; }
        /// <summary>
        /// 数据过滤条件
        /// </summary>
        public string Filter { get; set; }
        /// <summary>
        /// 实体数据名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 实体数据描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 实体数据信息
        /// </summary>
        public EntityInfo() { }
        /// <summary>
        /// 实体数据信息
        /// </summary>
        /// <param name="entityType">实体数据类型</param>
        public EntityInfo(Type entityType)
        {
            EntityType = entityType;
            Name = EntityType.Name.Contains("_") ? EntityType.Name.Split('_')[1] : EntityType.Name;
        }
        /// <summary>
        /// 实体数据信息
        /// </summary>
        /// <param name="entityType">实体数据类型</param>
        /// <param name="filter">数据过滤条件</param>
        public EntityInfo(Type entityType, string filter) : this(entityType) => Filter = filter;
        /// <summary>
        /// 实体数据信息
        /// </summary>
        /// <param name="entityType">实体数据类型</param>
        /// <param name="filter">数据过滤条件</param>
        /// <param name="name">实体数据名称</param>
        public EntityInfo(Type entityType, string filter, string name) : this(entityType, filter) => Name = name;
        /// <summary>
        /// 实体数据信息
        /// </summary>
        /// <param name="entityType">实体数据类型</param>
        /// <param name="filter">数据过滤条件</param>
        /// <param name="name">实体数据名称</param>
        /// <param name="description">实体数据描述</param>
        public EntityInfo(Type entityType, string filter, string name, string description) : this(entityType, filter, name) => Description = description;
    }
}
