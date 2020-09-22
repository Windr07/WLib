/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;

namespace WLib.Database.TableInfo
{
    /// <summary>
    /// 字典表
    /// </summary>
    public class DictionaryTable
    {
        /// <summary>
        /// 字典表名
        /// </summary>
        public string DictionaryName { get; set; }
        /// <summary>
        /// 编码和名称
        /// </summary>
        public Dictionary<string, string> CodeNameDict { get; set; }


        /// <summary>
        /// 字典表
        /// </summary>
        public DictionaryTable() => this.CodeNameDict = new Dictionary<string, string>();
        /// <summary>
        /// 字典表
        /// </summary>
        /// <param name="dictionaryName"></param>
        public DictionaryTable(string dictionaryName) : this() => this.DictionaryName = dictionaryName;
    }
}
