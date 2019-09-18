/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/7
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Plugins.Interface
{
    /// <summary>
    /// 插件项
    /// <para>通常对应一个按钮、菜单项</para>
    /// </summary>
    /// <typeparam name="T">插件对应的命令</typeparam>
    public interface IPluginItem : IItemBase
    {
        /// <summary>
        /// 插件索引
        /// </summary>
        int Index { get; set; }
        /// <summary>
        /// 插件对应的命令类型所在程序集文件路径
        /// </summary>
        string AssemblyPath { get; set; }
        /// <summary>
        /// 插件对应的命令类型完全限定名（命名空间 + 类型名）
        /// </summary>
        string TypeName { get; set; }
        /// <summary>
        /// 图标文件路径
        /// </summary>
        string IconPath { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        string Tips { get; set; }
        /// <summary>
        /// 快捷键
        /// </summary>
        string ShortcutKeys { get; set; }
        /// <summary>
        /// 是否在该插件后面加入分隔符
        /// </summary>
        bool AppendSplit { get; set; }
        /// <summary>
        /// 是否在界面上显示
        /// </summary>
        bool Visible { get; set; }
        /// <summary>
        /// 插件调用类型
        /// <para>在单击时调用插件、在视图加载时调用插件、在视图关闭时调用插件、自定义插件调用时机</para>
        /// </summary>
        EPluginInvokeType InvokType { get; set; }
        /// <summary>
        /// 插件对应的命令
        /// </summary>
        ICommand Command { get; set; }
    }
}
