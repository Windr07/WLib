namespace WLib.ArcGis.Analysis.GpEnum
{
    /// <summary>
    /// 相交工具中，确定输入要素的哪些属性将传递到输出要素类
    /// </summary>
    public enum EIsJoinAttributes
    {
        /// <summary>
        /// 输入要素的所有属性都将传递到输出要素类
        /// </summary>
        ALL,
        /// <summary>
        /// 除 FID 外，输入要素的其余属性都将传递到输出要素类
        /// </summary>
        NO_FID,
        /// <summary>
        /// 只有输入要素的 FID 字段将传递到输出要素类
        /// </summary>
        ONLY_FID
    }
}
