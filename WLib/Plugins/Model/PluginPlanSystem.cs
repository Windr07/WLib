/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/7
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Linq;
using System.Collections.Generic;
using WLib.Plugins.Interface;
using Newtonsoft.Json;

namespace WLib.Plugins.Model
{
    /// <summary>
    /// 插件方案系统，即一个系统拥有的插件方案
    /// </summary>
    [Serializable]
    public class PluginPlanSystem : IPluginPlanSystem
    {
        /// <summary>
        /// 应用软件信息
        /// </summary>
        public ISystemInfo SysInfo { get; set; }
        /// <summary>
        /// 系统拥有的插件方案
        /// </summary>
        public IList<IPluginPlan> Plans { get; set; } = new List<IPluginPlan>();
        /// <summary>
        /// 选用的插件方案
        /// </summary>
        [JsonIgnore]
        public IPluginPlan SelectedPlan
        {
            get => Plans.FirstOrDefault(v => v.Selected);
            set
            {
                var index = Plans.IndexOf(value);
                if (index > -1)
                {
                    foreach (var plan in Plans) plan.Selected = false;
                    Plans[index].Selected = true;
                }
            }
        }


        /// <summary>
        /// 插件方案系统，即一个系统拥有的插件方案
        /// </summary>
        public PluginPlanSystem() { }
        /// <summary>
        /// 插件方案系统，即一个系统拥有的插件方案
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="appId"></param>
        /// <param name="plans"></param>
        public PluginPlanSystem(string appName, string appId, IList<IPluginPlan> plans)
        {
            SysInfo = new SystemInfo(appName, appId);
            Plans = plans;
        }
        /// <summary>
        /// 输出“标题”
        /// </summary>
        /// <returns></returns>
        public override string ToString() => SysInfo?.Text;
    }
}
