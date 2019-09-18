/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/7
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using Newtonsoft.Json;
using System;
using WLib.Plugins.Interface;

namespace WLib.Plugins.Model
{
    /// <summary>
    /// 插件项
    /// <para>通常对应一个按钮、菜单项</para>
    /// </summary>
    /// <typeparam name="T">插件对应的命令</typeparam>
    [Serializable]
    public class PluginItem : IPluginItem
    {
        /// <summary>
        /// 插件ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 插件名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 插件标题
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 插件索引
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 插件对应的命令类型所在程序集文件路径
        /// </summary>
        public string AssemblyPath { get; set; }
        /// <summary>
        /// 插件对应的命令类型完全限定名（命名空间 + 类型名）
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 图标文件路径
        /// </summary>
        public string IconPath { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Tips { get; set; }
        /// <summary>
        /// 快捷键
        /// </summary>
        public string ShortcutKeys { get; set; }
        /// <summary>
        /// 是否在该插件前面加入分隔符
        /// </summary>
        public bool AppendSplit { get; set; }
        /// <summary>
        /// 是否在界面上显示
        /// </summary>
        public bool Visible { get; set; } = true;
        /// <summary>
        /// 插件调用类型
        /// <para>在单击时调用插件、在视图加载时调用插件、在视图关闭时调用插件、自定义插件调用时机</para>
        /// </summary>
        public EPluginInvokeType InvokType { get; set; } = EPluginInvokeType.OnClick;
        /// <summary>
        /// 插件对应的命令
        /// </summary>
        [JsonIgnore]
        public ICommand Command { get; set; }


        /// <summary>
        /// 创建插件项，创建GUID赋值插件项Id
        /// </summary>
        public PluginItem() => Id = Guid.NewGuid().ToString();
        /// <summary>
        /// 将插件关联到指定命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static PluginItem FromCommand(ICommand cmd)
        {
            var type = cmd.GetType();
            return new PluginItem
            {
                Id = Guid.NewGuid().ToString(),
                Name = cmd.Name,
                Text = cmd.Text,
                AssemblyPath = type.Assembly.Location,
                TypeName = type.FullName,
                Tips = cmd.Description,
                Visible = true,
                InvokType = EPluginInvokeType.OnClick,
                Command = cmd,
            };
        }
        /// <summary>
        /// 输出<see cref="Text"/>的值
        /// </summary>
        /// <returns></returns>
        public override string ToString() => AppendSplit ? Text + "（|）" : Text;
    }
}
