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
    /// <typeparam name="TInputData">传入给命令的参数</typeparam>
    public interface ICommand<TInputData> : ICommand
    {
        /// <summary>
        /// 传入给命令的参数
        /// </summary>
        new TInputData InputData { get; set; }
    }
}
