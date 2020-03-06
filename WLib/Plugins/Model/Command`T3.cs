/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/7
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using WLib.Plugins.Interface;

namespace WLib.Plugins.Model
{
    /// <summary>
    /// 表示一项功能命令
    /// </summary>
    /// <typeparam name="TInputData">传入给命令的参数</typeparam>
    /// <typeparam name="TCmdEventType">命令内部与外部视图对象交互操作的类型</typeparam>
    /// <typeparam name="TViewEventType">外部视图对象的事件类型</typeparam>
    [Serializable]
    public abstract class Command<TInputData, TCmdEventType, TViewEventType> : Command<TInputData>, ICommand<TInputData, TCmdEventType, TViewEventType>
    {
        /// <summary>
        /// 命令内部与外部调用对象交互的操作
        /// <para><see cref="TCmdEventType"/>命令内部与外部调用对象交互操作的类型，例如此参数为枚举值，表示操作类型有选择加载图层、定位图斑、显示属性表等</para>
        /// <para><see cref="EventArgs"/>命令内部与外部调用对象交互操作的参数，例如加载图层事件的图层路径、定位图斑事件的图斑ID、显示属性表事件的图层名等</para>
        /// </summary>
        public Action<TCmdEventType, EventArgs> CommandAction { get; set; }
        /// <summary>
        /// 调用命令的插件视图
        /// </summary>
        public new IPluginView<TViewEventType> View { get; set; }
        /// <summary>
        /// 进度操作的输入数据
        /// </summary>
        IPluginView ICommand.View { get => this.View; set => this.View = (IPluginView<TViewEventType>)value; }
    }
}
