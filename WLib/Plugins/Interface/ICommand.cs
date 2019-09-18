/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/7
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

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
        /// 调用命令
        /// </summary>
        /// <param name="caller">调用命令的对象，可空</param>
        void Invoke(object caller);
        /// <summary>
        /// 改变创建命令的对象的状态的委托
        /// <para>例如命令是一个选择shp图层命令，调用命令选择shp图层后执行该委托，可绑定委托处理为：在主窗体的地图控件中加载所选shp图层</para>
        /// </summary>
        Action ChangedStatus { get; }
    }
}
