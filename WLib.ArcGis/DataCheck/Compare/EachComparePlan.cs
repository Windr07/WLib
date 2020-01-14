using System;
using System.Collections.Generic;

namespace WLib.ArcGis.DataCheck.Compare
{
    /// <summary>
    /// 两个表格或图层之间对比的方案
    /// </summary>
    [Serializable]
    public class EachComparePlan : ComparePlan
    {
        /// <summary>
        /// 表格1路径
        /// </summary>
        public string Table1Path { get; set; }
        /// <summary>
        /// 表格2路径
        /// </summary>
        public string Table2Path { get; set; }
        /// <summary>
        /// 表格1和表格2之间的对比项
        /// </summary>
        public Dictionary<CompareItem, CompareItem> ComparePairs = new Dictionary<CompareItem, CompareItem>();


        /// <summary>
        /// 两个表格或图层之间对比的方案
        /// </summary>
        public EachComparePlan()
        {
        }
        /// <summary>
        /// 两个表格或图层之间对比的方案
        /// </summary>
        /// <param name="table1Path"></param>
        /// <param name="table2Path"></param>
        public EachComparePlan(string table1Path, string table2Path)
        {
            Table1Path = table1Path;
            Table2Path = table2Path;
        }
    }
}
