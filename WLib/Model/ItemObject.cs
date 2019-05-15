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
    /// 放入列表中的对象
    /// </summary>
    public class ItemObject : IComparable<ItemObject>
    {
        /// <summary>
        /// 序号（序号应≥0）
        /// </summary>
        public int Index { get; set; } = -1;
        /// <summary>
        /// 唯一值，通常为ID、编码、代码或图幅号等
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 所在分类
        /// </summary>
        public string Classify { get; set; }


        /// <summary>
        /// 放入列表中的对象
        /// </summary>
        public ItemObject()
        {

        }
        /// <summary>
        /// 放入列表中的对象
        /// </summary>
        /// <param name="code">唯一值（通常为ID、编码、代码或图幅号等）</param>
        /// <param name="name">名称</param>
        public ItemObject(string code, string name)
        {
            this.Code = code;
            this.Name = name;
        }
        /// <summary>
        /// 放入列表中的对象
        /// </summary>
        /// <param name="code">唯一值（通常为ID、编码、代码或图幅号等）</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        public ItemObject(string code, string name, int index)
        {
            this.Code = code;
            this.Name = name;
            this.Index = index;
        }
        /// <summary>
        /// 放入列表中的对象
        /// </summary>
        /// <param name="code">唯一值（通常为ID、编码、代码或图幅号等）</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        /// <param name="classify">所在分类</param>
        public ItemObject(string code, string name, int index, string classify)
        {
            this.Code = code;
            this.Name = name;
            this.Index = index;
            this.Classify = classify;
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
        /// 根据序号<see cref="Index"/>排序
        /// </summary>
        /// <param name="itemObject"></param>
        /// <returns></returns>
        public int CompareTo(ItemObject itemObject)
        {
            return itemObject.Index;
        }
    }
}
