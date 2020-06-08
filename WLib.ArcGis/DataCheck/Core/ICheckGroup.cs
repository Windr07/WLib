using System.Collections.Generic;

namespace WLib.ArcGis.DataCheck.Core
{
    /// <summary>
    /// 数据检查项接口
    /// </summary>
    public interface ICheckGroup : INodeProperty
    {
        /// <summary>
        /// 数据检查数据源
        /// </summary>
        ICheckScheme CheckScheme { get; set; }
        /// <summary>
        /// 数据检查规则
        /// </summary>
        IList<IChecker> CheckItems { get; }
    }
}
