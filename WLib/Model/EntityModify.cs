/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020/10
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace WLib.Model
{
    /// <summary>
    /// 记录对数据的增、删、改的情况
    /// </summary>
    [Serializable]
    public class EntityModify
    {
        /// <summary>
        /// 被修改的记录的ID（编号）
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 被修改的实体
        /// </summary>
        public object Enitity { get; set; }
        /// <summary>
        /// 数据修改类型
        /// <para>新增、删除、修改</para>
        /// </summary>
        public EModifyType ModifyType { get; set; }
        /// <summary>
        /// 被修改的记录的字段
        /// </summary>
        public List<string> FieldNames { get; set; }


        /// <summary>
        /// 记录对数据的增、删、改的情况
        /// </summary>
        public EntityModify() => FieldNames = new List<string>();
        /// <summary>
        /// 记录对数据的增、删、改的情况
        /// </summary>
        /// <param name="id">被修改的记录的ID（编号）</param>
        /// <param name="fieldName">被修改的记录的字段</param>
        /// <param name="modifyType"> 数据修改类型（新增、删除、修改）</param>
        /// <param name="entity">被修改的实体</param>
        public EntityModify(int id, string fieldName, EModifyType modifyType, object entity) : this()
        {
            Id = id;
            FieldNames.Add(fieldName);
            ModifyType = modifyType;
            Enitity = entity;
        }
    }
}
