/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Model
{
    /// <summary>
    /// 放入列表中的对象
    /// </summary>
    /// <typeparam name="T">与对象关联的用户定义数据</typeparam>
    public class ItemObject<T> : ItemObject
    {
        /// <summary>
        /// 与对象关联的用户定义数据
        /// </summary>
        public T Tag { get; set; }

        /// <summary>
        /// 放入列表中的对象
        /// </summary>
        /// <param name="code">唯一值编码（通常为ID、编码、代码或图幅号等）</param>
        /// <param name="name">名称</param>
        public ItemObject(string code, string name)
            : base(code, name)
        {
        }
        /// <summary>
        /// 放入列表中的对象
        /// </summary>
        /// <param name="code">唯一值编码（通常为ID、编码、代码或图幅号等）</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        public ItemObject(string code, string name, int index)
            : base(code, name, index)
        {
        }
        /// <summary>
        /// 放入列表中的对象
        /// </summary>
        /// <param name="code">唯一值编码（通常为ID、编码、代码或图幅号等）</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        /// <param name="classify">所在分类</param>
        public ItemObject(string code, string name, int index, string classify)
            : base(code, name, index, classify)
        {
        }
        /// <summary>
        /// 放入列表中的对象
        /// </summary>
        /// <param name="code">唯一值编码（通常为ID、编码、代码或图幅号等）</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        /// <param name="classify">所在分类</param>
        /// <param name="tag">与对象关联的用户定义数据</param>
        public ItemObject(string code, string name, int index, string classify, T tag)
            : base(code, name, index, classify)
        {
            this.Tag = tag;
        }
    }
}
