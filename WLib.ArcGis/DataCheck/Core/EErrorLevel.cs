namespace WLib.ArcGis.DataCheck.Core
{
    /// <summary>
    /// 检查错误信息级别
    /// </summary>
    public enum EErrorLevel
    {
        /// <summary>
        /// 正常检查结果信息
        /// </summary>
        普通 = 0,
        /// <summary>
        /// 提示信息
        /// </summary>
        提示 = 1,
        /// <summary>
        /// 警告信息
        /// </summary>
        警告 = 2,
        /// <summary>
        /// 程序报错信息
        /// </summary>
        异常 = 3,
    }
}
