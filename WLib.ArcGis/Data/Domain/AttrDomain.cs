/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace WLib.ArcGis.Data.Domain
{
    /// <summary>
    /// 属性域（ArcGIS数据库中的属性域）
    /// </summary>
    public class AttrDomain
    {
        /// <summary>
        /// 属性域名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 属性域描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 属性域的编码值
        /// </summary>
        public Dictionary<string, string> CodeValues { get; set; }
        /// <summary>
        /// 属性域（ArcGIS数据库中的属性域）
        /// </summary>
        public AttrDomain(string domainName, string domainDescription)
        {
            Name = domainName;
            Description = domainDescription;
            CodeValues = new Dictionary<string, string>();
        }


        /// <summary>
        /// 向属性域添加编码值
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="description">描述</param>
        public void Add(string code, string description)
        {
            CodeValues.Add(code, description);
        }
        /// <summary>
        /// 根据属性域的编码，获取属性域的值，找不到则返回null
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            if (CodeValues.Keys.Contains(key))
                return CodeValues[key];
            else
                return null;
        }
        /// <summary>
        /// 判断输入值，若为属性域值则返回该属性域值，若为属性域的编码则返回此编码对应的值，否则返回null
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public string GetValue2(string inputString)
        {
            if (string.IsNullOrEmpty(inputString) || inputString.Trim() == string.Empty)
                return null;

            //应该判断字典表的每个编码值的值，例如输入值为"曾孙子"，符合编码值"曾孙子或外曾孙子"
            inputString = inputString.Trim();
            var value = CodeValues.Values.FirstOrDefault(v => v.Contains(inputString));
            return value ?? GetValue(inputString);
        }
        /// <summary>
        /// 判断输入值，若为属性域编码则返回该属性域编码，若为属性域的值则返回此值对应的编码，找不到则返回null
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public string GetCode(string inputString)
        {
            if (string.IsNullOrEmpty(inputString) || inputString.Trim() == string.Empty)
                return null;
            inputString = inputString.Trim();
            if (CodeValues.Keys.Contains(inputString))
                return inputString;
            //应该判断字典表的每个编码值的值，例如输入值为"曾孙子"，符合编码值"曾孙子或外曾孙子"
            var value = CodeValues.Values.FirstOrDefault(v => v.Contains(inputString));
            if (value != null)
                return CodeValues.FirstOrDefault(v => v.Value == value).Key;
            else
                return null;
        }
        /// <summary>
        /// 将属性域数据转成DataTable
        /// </summary>
        /// <param name="codeColumn">编码字段名</param>
        /// <param name="descColumn">描述字段名</param>
        /// <returns></returns>
        public DataTable ConvertToDataTable(string codeColumn = "编码", string descColumn = "描述")
        {
            DataTable dataTable = new DataTable(Description);
            dataTable.Columns.Add(new DataColumn(codeColumn, typeof(string)));
            dataTable.Columns.Add(new DataColumn(descColumn, typeof(string)));
            foreach (var pair in CodeValues)
            {
                dataTable.Rows.Add(pair.Key, pair.Value);
            }
            return dataTable;
        }
    }
}
