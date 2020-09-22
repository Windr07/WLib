/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.ArcGis.DataCheck.Compare
{
    /// <summary>
    /// 表示一个对比项信息
    /// </summary>
    [Serializable]
    public abstract class CompareItem
    {
        /// <summary>
        /// 不满足对比要求时的表述信息
        /// </summary>
        public string Description { get; set; }
    }
}
