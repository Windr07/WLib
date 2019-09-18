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
        /// 调用命令
        /// </summary>
        /// <param name="caller"></param>
        public abstract void Invoke(object caller);
        /// <summary>
        /// 改变创建命令的对象的状态的委托
        /// <para>例如命令是一个选择shp图层命令，调用命令选择shp图层后执行该委托，可绑定委托处理为：在主窗体的地图控件中加载所选shp图层</para>
        /// </summary>
        public Action ChangedStatus { get; }
    }
}
