using System;
using System.Collections.Generic;

namespace WLib.ArcGis.DataCheck.Compare
{
    /// <summary>
    /// 单个表格自身信息对比的方案
    /// </summary>
    [Serializable]
    public class SelfComparePlan : ComparePlan
    {
        /// <summary>
        /// 表格1路径
        /// </summary>
        public string TablePath { get; set; }
        /// <summary>
        /// 对比项
        /// </summary>
        public List<CompareItem> CompareItems = new List<CompareItem>();
    }
}
