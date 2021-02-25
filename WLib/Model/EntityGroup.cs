/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020/10
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace WLib.Model
{
    /// <summary>
    /// 实体数据分组
    /// </summary>
    [Serializable]
    public class EntityGroup : List<EntityInfo>
    {
        /// <summary>
        /// 实体数据分组名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 子数据分组
        /// </summary>
        public List<EntityGroup> SubGroups { get; set; } = new List<EntityGroup>();


        /// <summary>
        /// 实体数据分组
        /// </summary>
        public EntityGroup() { }
        /// <summary>
        /// 实体数据分组
        /// </summary>
        /// <param name="groupName">实体数据分组名称</param>
        public EntityGroup(string groupName) => GroupName = groupName;
        /// <summary>
        /// 实体数据分组
        /// </summary>
        /// <param name="groupName">实体数据分组名称</param>
        /// <param name="entityTypes">该类别包含的实体类类型</param>
        public EntityGroup(string groupName, params Type[] entityTypes) : this(groupName)
            => AddRange(entityTypes.Select(entityType => new EntityInfo(entityType)));
    }
}
