/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/7
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Plugins.Interface
{
    /// <summary>
    /// 表示一项功能命令
    /// </summary>
    public interface ICommand : IItemBase
    {
        /// <summary>
        /// 功能分组
        /// </summary>
        string Category { get; }
        /// <summary>
        /// 提示信息
        /// </summary>
        string ToolTip { get; }
        /// <summary>
        /// 功能描述
        /// </summary>
        string Description { get; }
        /// <summary>
        /// 是否启用该命令
        /// </summary>
        bool Enable { get; }
        /// <summary>
        /// 传入给命令的参数
        /// </summary>
        object InputData { get; set; }
        /// <summary>
        /// 调用命令的插件视图
        /// </summary>

        IPluginView View { get; set; }


        /// <summary>
        /// 调用命令
        /// </summary>
        /// <param name="caller">调用命令的对象，可空</param>
        void Invoke(object caller);
    }
}
