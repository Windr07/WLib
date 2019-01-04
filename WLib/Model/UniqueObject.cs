/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Model
{
    /// <summary>
    /// 放入列表中的唯一值对象
    /// </summary>
    public class UniqueObject : IComparable<UniqueObject>
    {
        /// <summary>
        /// 序号（序号应≥0）
        /// </summary>
        public int Index = -1;
        /// <summary>
        /// 唯一值，通常为ID、编码、代码或图幅号等
        /// </summary>
        public string Code;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Comment;

        /// <summary>
        /// 放入列表中的唯一值对象
        /// </summary>
        public UniqueObject()
        {

        }
        /// <summary>
        /// 放入列表中的唯一值对象
        /// </summary>
        /// <param name="code">唯一值（通常为ID、编码、代码或图幅号等）</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        public UniqueObject(string code, string name)
        {
            this.Code = code;
            this.Name = name;
        }
        /// <summary>
        /// 放入列表中的唯一值对象
        /// </summary>
        /// <param name="code">唯一值（通常为ID、编码、代码或图幅号等）</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        public UniqueObject(string code, string name, int index)
        {
            this.Code = code;
            this.Name = name;
            this.Index = index;
        }
        /// <summary>
        /// 放入列表中的唯一值对象
        /// </summary>
        /// <param name="code">唯一值（通常为ID、编码、代码或图幅号等）</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        /// <param name="comment">备注信息</param>
        public UniqueObject(string code, string name, int index, string comment)
        {
            this.Code = code;
            this.Name = name;
            this.Index = index;
            this.Comment = comment;
        }

        /// <summary>
        /// 对象的文本显示
        /// </summary>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Name))
                return Index == -1 ? Code : $"{Index}、{Code}";
            else
                return Index == -1 ? $"{Name} - {Code}" : $"{Index}、{Name} - {Code}";
        }
        /// <summary>
        /// 根据序号(UniqueObject.Index)排序
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(UniqueObject other)
        {
            UniqueObject value = (UniqueObject)other;
            return value.Index;
        }
    }
}
