/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/7
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;

namespace WLib.Plugins.Interface
{
    /// <summary>
    /// 应用软件信息及其拥有的插件方案
    /// </summary>
    public interface IPluginPlanSystem
    {
        /// <summary>
        /// 应用软件信息
        /// </summary>
        ISystemInfo SysInfo { get; set; }
        /// <summary>
        /// 应用软件拥有的插件方案
        /// </summary>
        IList<IPluginPlan> Plans { get; set; }
        /// <summary>
        /// 选用的插件方案
        /// </summary>
        IPluginPlan SelectedPlan { get; }
    }
}
