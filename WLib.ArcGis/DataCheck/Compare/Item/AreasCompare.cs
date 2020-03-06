namespace WLib.ArcGis.DataCheck.Compare
{
    /// <summary>
    /// 面积对比信息
    /// </summary>
    public class AreasCompare : CompareItem
    {
        /// <summary>
        /// True对比总面积，False对比每一条记录的面积
        /// </summary>
        public bool CompareSumArea { get; set; }
        /// <summary>
        /// 允许的面积容差
        /// </summary>
        public double Tolerance { get; set; }
    }
}
