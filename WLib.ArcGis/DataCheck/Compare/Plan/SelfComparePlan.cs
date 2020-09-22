/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.ArcGis.DataCheck.Compare.Plan
{
    /// <summary>
    /// 单个表格或图层自身信息的对比的方案
    /// </summary>
    [Serializable]
    public abstract class SelfComparePlan : ComparePlan
    {
        /// <summary>
        /// 显示对比方案名称
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Name;
    }
}
