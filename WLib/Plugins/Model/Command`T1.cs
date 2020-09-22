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
    [Serializable]
    public abstract class Command<TInputData> : Command, ICommand<TInputData>
    {
        /// <summary>
        /// 传入给命令的参数
        /// </summary>
        public new TInputData InputData { get; set; }
    }
}
