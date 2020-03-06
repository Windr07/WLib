namespace WLib.ArcGis.DataCheck.Compare
{
    /// <summary>
    /// 在数据对比时，数据查询或匹配失败错误消息处理类型
    /// </summary>
    public enum ENoneMatchErrorType
    {
        /// <summary>
        /// 以异常信息处理
        /// </summary>
        Error,
        /// <summary>
        /// 以警告信息处理
        /// </summary>
        Warnning,
        /// <summary>
        /// 以提示信息处理
        /// </summary>
        Tips,
        /// <summary>
        /// 忽略，不进行处理
        /// </summary>
        Ignore,
    }
}
