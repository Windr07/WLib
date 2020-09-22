/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Model
{
    /// <summary>
    /// 表示一个节点，通常用于将树节点映射到二维表中
    /// </summary>
    public class NodeObject
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 节点所属父节点的ID
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 节点类型
        /// </summary>
        public int NodeType { get; set; }
        /// <summary>
        /// 节点显示的文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 节点标识
        /// </summary>
        public int NodeTag { get; set; }
        /// <summary>
        /// 节点关联信息
        /// </summary>
        public object Tag { get; set; }


        /// <summary>
        /// 表示一个节点，通常用于将树节点映射到二维表中
        /// </summary>
        public NodeObject() { }
        /// <summary>
        /// 表示一个节点，通常用于将树节点映射到二维表中
        /// </summary>
        /// <param name="id">节点ID</param>
        /// <param name="parentId">节点所属父节点的ID</param>
        /// <param name="text">节点显示的文本</param>
        public NodeObject(int id, int parentId, string text)
        {
            Id = id;
            ParentId = parentId;
            Text = text;
        }
        /// <summary>
        /// 表示一个节点，通常用于将树节点映射到二维表中
        /// </summary>
        /// <param name="id">节点ID</param>
        /// <param name="parentId">节点所属父节点的ID</param>
        /// <param name="nodeType">节点类型</param>
        /// <param name="text">节点显示的文本</param>
        /// <param name="nodeTag">节点标识</param>
        /// <param name="tag">节点关联信息</param>
        public NodeObject(int id, int parentId, string text, int nodeType, int nodeTag, object tag)
        {
            Id = id;
            ParentId = parentId;
            NodeType = nodeType;
            Text = text;
            NodeTag = nodeTag;
            Tag = tag;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{ParentId}|{Id}：{Text}|{NodeType}|{NodeTag}";
    }
}
