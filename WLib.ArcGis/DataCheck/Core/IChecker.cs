namespace WLib.ArcGis.DataCheck.Core
{
    /// <summary>
    /// 数据检查规则接口
    /// </summary>
    public interface IChecker : INodeProperty
    {
        /// <summary>
        /// 规则说明
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// 数据检查项
        /// </summary>
        ICheckGroup CheckGroup { get; set; }
    }
}
