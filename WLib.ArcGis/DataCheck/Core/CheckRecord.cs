/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.ArcGis.DataCheck.Core
{
    /// <summary>
    /// 检查结果记录
    /// </summary>
    [Serializable]
    public class CheckRecord
    {
        public string 检查名 { get; set; }
        public EErrorLevel 错误级别 { get; set; }
        public string 错误描述 { get; set; }

        /// <summary>
        /// 检查结果记录
        /// </summary>
        public CheckRecord() { }
        /// <summary>
        /// 检查结果记录
        /// </summary>
        /// <param name="检查名">检查项名称</param>
        /// <param name="错误描述">错误描述</param>
        /// <param name="错误级别">错误级别</param>
        public CheckRecord(string 检查名, string 错误描述, EErrorLevel 错误级别 = EErrorLevel.普通)
        {
            this.检查名 = 检查名;
            this.错误描述 = 错误描述;
            this.错误级别 = 错误级别;
        }
    }
}
