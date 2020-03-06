using System;
using System.Collections.Generic;

namespace WLib.ArcGis.DataCheck.Core
{
    /// <summary>
    /// 数据检查方案接口
    /// </summary>
    public interface ICheckScheme : INodeProperty
    {
        /// <summary>
        /// 创建人
        /// </summary>
        string PersonName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }
        /// <summary>
        /// 数据源列表
        /// </summary>
        IList<ICheckGroup> CheckGroups { get; }
    }
}
