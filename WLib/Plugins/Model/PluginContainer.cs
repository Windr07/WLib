/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/7
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using WLib.Plugins.Enum;
using WLib.Plugins.Interface;

namespace WLib.Plugins.Model
{
    /// <summary>
    /// 插件容器
    /// <para>通常对应一个工具栏、菜单栏</para>
    /// </summary>
    [Serializable]
    public class PluginContainer : IPluginContainer
    {
        /// <summary>
        /// 插件容器ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 插件容器名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 插件容器标题
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Visible { get; set; } = true;
        /// <summary>
        /// 插件容器的快捷键
        /// </summary>
        public string ShortcutKeys { get; set; }
        /// <summary>
        /// 插件容器类别
        /// <para>包括菜单、工具条、Ribbon菜单、快捷菜单等类别</para>
        /// </summary>
        public EPluginContainerType Type { get; set; }
        /// <summary>
        /// 插件容器包含的子容器
        /// <para>例如菜单包含的子菜单</para>
        /// </summary>
        public IList<IPluginContainer> SubContainers { get; set; } = new List<IPluginContainer>();
        /// <summary>
        /// 插件容器包含的插件
        /// </summary>
        public IList<IPluginItem> Plugins { get; set; } = new List<IPluginItem>();


        /// <summary>
        /// 创建插件容器，创建GUID赋值插件容器Id
        /// <para>通常对应一个工具栏、菜单栏</para>
        /// </summary>
        public PluginContainer() => Id = Guid.NewGuid().ToString();
        /// <summary>
        /// 创建插件容器，创建GUID赋值插件容器Id，设定插件容器名称和类型
        /// </summary>
        /// <param name="name">插件容器名称</param>
        /// <param name="type">插件容器类别</param>
        public PluginContainer(string name, EPluginContainerType type) : this()
        {
            Text = Name = name;
            Type = type;
        }
        /// <summary>
        /// 创建插件容器，创建GUID赋值插件容器Id，设定插件容器名称和类型
        /// </summary>
        /// <param name="name">插件容器名称</param>
        /// <param name="text">插件容器标题</param>
        /// <param name="type">插件容器类别</param>
        public PluginContainer(string name, string text, EPluginContainerType type) : this()
        {
            Name = name;
            Text = text;
            Type = type;
        }
        /// <summary>
        /// 创建插件容器，创建GUID赋值插件容器Id，设定插件类型
        /// </summary>
        /// <param name="type"></param>
        public PluginContainer(EPluginContainerType type) : this(System.Enum.GetName(typeof(EPluginContainerType), type), type) { }
        /// <summary>
        /// 输出<see cref="Text"/>的值
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Text;

    }
}
