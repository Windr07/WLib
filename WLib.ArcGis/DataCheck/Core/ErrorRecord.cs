namespace WLib.ArcGis.DataCheck.Core
{
    /// <summary>
    /// 错误记录类
    /// </summary>
    public class ErrorRecord
    {
        public string 检查名 { get; set; }
        public EErrorLevel 错误级别 { get; set; }
        public string 错误描述 { get; set; }

        /// <summary>
        /// 错误记录类
        /// </summary>
        public ErrorRecord()
        {
        }
        /// <summary>
        /// 错误记录类
        /// </summary>
        /// <param name="检查名">检查项名称</param>
        /// <param name="错误描述">错误描述</param>
        /// <param name="错误级别">错误级别</param>
        public ErrorRecord(string 检查名,  string 错误描述, EErrorLevel 错误级别 = EErrorLevel.普通)
        {
            this.检查名 = 检查名;
            this.错误描述 = 错误描述;
            this.错误级别 = 错误级别;
        }
    }
}
