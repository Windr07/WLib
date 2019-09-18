using System;
using System.Collections.Generic;
using WLib.Model;
using WLib.Plugins.Interface;

namespace WLib.Plugins.Model
{
    /// <summary>
    /// 插件方案
    /// </summary>
    [Serializable]
    public class PluginPlan : IPluginPlan
    {
        /// <summary>
        /// 插件方案ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 插件方案名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 插件方案标题
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 是否选用此方案
        /// </summary>
        public bool Selected { get; set; }
        /// <summary>
        /// 可加载插件的视图
        /// </summary>
        public IList<IPluginView> Views { get; set; } = new List<IPluginView>();
        /// <summary>
        /// 插件方案序列化后的字符串
        /// </summary>
        public string StrPluginPlan { get; set; }


        /// <summary>
        /// 创建插件方案，创建GUID赋值插件方案Id
        /// </summary>
        public PluginPlan() => Id = Guid.NewGuid().ToString();
        /// <summary>
        /// 复制插件方案
        /// </summary>
        /// <param name="newPlanName">新插件方案名称</param>
        /// <returns></returns>
        public PluginPlan Copy(string newPlanName = null)
        {
            var pluginPlan = this.CopyBySerialize();
            pluginPlan.Id = Guid.NewGuid().ToString();
            pluginPlan.Name = newPlanName ?? this.Name + " - 副本";
            pluginPlan.Text = pluginPlan.Name;
            return pluginPlan;
        }
        /// <summary>
        /// 输出<see cref="Text"/>的值
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Selected ? Text + "(选用)" : Text;
      
    }
}
