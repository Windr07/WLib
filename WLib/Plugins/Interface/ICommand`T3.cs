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
    /// <typeparam name="TInputData">传入给命令的参数</typeparam>
    /// <typeparam name="TStatusType">改变创建命令的对象的状态的类型</typeparam>
    /// <typeparam name="TStatusArgs">改变创建命令的对象的状态的参数</typeparam>
    public interface ICommand<TInputData, TStatusType, TStatusArgs> : ICommand<TInputData>
    {
        /// <summary>
        /// 改变创建命令的对象的状态的委托
        /// <para>例如命令是一个选择shp图层命令，调用命令选择shp图层后执行该委托，外部可绑定委托处理为：在主窗体的地图控件中加载所选shp图层</para>
        /// <para><see cref="TStatusType"/>表示改变创建命令的对象的状态的类型，例如此参数为枚举值，区分选择shp图层后要加载shp图层还是获取shp图层属性表</para>
        /// <para><see cref="TStatusArgs"/>表示改变创建命令的对象的状态的参数，例如选择shp图层后要加载shp图层，此参数为shp图层路径</para>
        /// </summary>
        new Action<TStatusType, TStatusArgs> ChangedStatus { get; }
    }
}
