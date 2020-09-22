/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Plugins.Interface
{
    /// <summary>
    /// 列表项的基本信息
    /// <para>Id、Name、Text</para>
    /// </summary>
    public interface IItemBase
    {
        /// <summary>
        /// ID
        /// </summary>
        string Id { get; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        string Text { get; set; }
    }
}
