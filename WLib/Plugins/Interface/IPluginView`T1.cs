using System;

namespace WLib.Plugins.Interface
{
    /// <summary>
    /// 可加载插件的视图
    /// <para>实现该接口的类型一般是窗口或网页，可通过<see cref="PluginHelper"/>类进行插件管理</para>
    /// </summary>
    /// <typeparam name="TEventType">插件视图事件的类型</typeparam>
    public interface IPluginView<TEventType> : IPluginView
    {
        /// <summary>
        /// 插件视图事件
        /// <para><see cref="TEventType"/>插件视图事件的类型，例如此参数为枚举值，表示事件类型有选择工作空间已重载、配置已重置等</para>
        /// <para><see cref="EventArgs"/>插件视图事件的参数</para>
        /// </summary>
        Action<TEventType, EventArgs> ViewAction { get; set; }
    }     
}
