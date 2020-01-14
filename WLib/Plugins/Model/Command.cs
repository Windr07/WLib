/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/7
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.ComponentModel;
using WLib.Plugins.Interface;

namespace WLib.Plugins.Model
{
    /// <summary>
    /// 表示一项功能命令
    /// </summary>
    [Serializable]
    public abstract class Command : ICommand
    {
        /// <summary>
        /// 命令ID
        /// </summary>
        [Description("命令ID")]
        public virtual string Id { get; set; }
        /// <summary>
        /// 命令名称
        /// </summary>
        [Description("命令名称")]
        public virtual string Name { get; set; }
        /// <summary>
        /// 命令标题
        /// </summary>
        [Description("命令标题")]
        public virtual string Text { get; set; }
        /// <summary>
        /// 功能分组
        /// </summary>
        [Description("功能分组")]
        public virtual string Category { get; protected set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        [Description("提示信息")]
        public virtual string ToolTip { get; protected set; }
        /// <summary>
        /// 功能描述
        /// </summary>
        [Description("功能描述")]
        public virtual string Description { get; protected set; }
        /// <summary>
        /// 是否启用该命令
        /// </summary>
        [Description("启用命令")]
        public virtual bool Enable { get; protected set; } = true;
        /// <summary>
        /// 传入给命令的参数
        /// </summary>
        public object InputData { get; set; }
        /// <summary>
        /// 调用插件命令的插件视图
        /// </summary>
        public IPluginView View { get; set; }


        /// <summary>
        /// 调用命令
        /// </summary>
        /// <param name="caller">调用命令的对象，可空</param>
        public abstract void Invoke(object caller);
    }
}
