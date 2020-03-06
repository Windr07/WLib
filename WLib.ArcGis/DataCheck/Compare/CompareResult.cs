using WLib.ArcGis.DataCheck.Core;

namespace WLib.ArcGis.DataCheck.Compare
{
    /// <summary>
    /// 表格或图层数据对比操作结果
    /// </summary>
    public class CompareResult : ErrorRecord
    {
        public string 字段1 { get; set; }
        public string 字段值1 { get; set; }
        public string 字段2 { get; set; }
        public string 字段值2 { get; set; }

        /// <summary>
        /// 表格或图层数据对比操作结果
        /// </summary>
        public CompareResult(string 检查名, string 字段1, string 字段值1, string 字段2, string 字段值2, string 错误描述)
        {
            this.检查名 = 检查名;
            this.字段1 = 字段1;
            this.字段值1 = 字段值1;
            this.字段2 = 字段2;
            this.字段值2 = 字段值2;
            this.错误描述 = 错误描述;
        }
        /// <summary>
        /// 表格或图层数据对比操作结果
        /// </summary>
        public CompareResult(string 检查名, string 错误描述)
        {
            this.检查名 = 检查名;
            this.错误描述 = 错误描述;
        }
    }
}
