/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Model
{
    /// <summary>
    /// 放入列表中的唯一值对象
    /// </summary>
    public class UniqueObjectEx : UniqueObject
    {
        /// <summary>
        /// 所在分类
        /// </summary>
        public string Classify;
        /// <summary>
        /// 与对象关联的用户定义数据
        /// </summary>
        public object Tag;

        /// <summary>
        /// 放入列表中的唯一值对象
        /// </summary>
        /// <param name="code">唯一值（通常为ID、编码、代码或图幅号等）</param>
        /// <param name="name">名称</param>
        public UniqueObjectEx(string code, string name)
            : base(code, name)
        {
        }
        /// <summary>
        /// 放入列表中的唯一值对象
        /// </summary>
        /// <param name="code">唯一值（通常为ID、编码、代码或图幅号等）</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        public UniqueObjectEx(string code, string name, int index)
            : base(code, name, index)
        {
        }
        /// <summary>
        /// 放入列表中的唯一值对象
        /// </summary>
        /// <param name="code">唯一值（通常为ID、编码、代码或图幅号等）</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        /// <param name="comment">备注信息</param>
        public UniqueObjectEx(string code, string name, int index, string comment)
            : base(code, name, index, comment)
        {
        }
        /// <summary>
        /// 放入列表中的唯一值对象
        /// </summary>
        /// <param name="code">唯一值（通常为ID、编码、代码或图幅号等）</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        /// <param name="comment">备注信息</param>
        /// <param name="classify">所在分类</param>
        public UniqueObjectEx(string code, string name, int index, string comment, string classify)
            : base(code, name, index, comment)
        {
            this.Classify = classify;
        }
    }
}
