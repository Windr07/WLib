/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;

namespace WLib.Db.TableInfo.FieldDomain
{
    /// <summary>
    /// 通过编码值限制的字段取值范围
    /// </summary>
    public class FieldDictDomain : IFieldDomain
    {
        /// <summary>
        /// 字段值域类型（无限制、范围、字典表）
        /// </summary>
        public EFieldDomainType FieldDomianType { get; }

        /// <summary>
        /// 编码值
        /// </summary>
        public Dictionary<string, string> CodeValueDict { get; set; }

        /// <summary>
        /// 通过编码值限制的字段取值范围
        /// </summary>
        public FieldDictDomain()
        {
            FieldDomianType = EFieldDomainType.Dictionary;
            CodeValueDict = new Dictionary<string, string>();
        }
        /// <summary>
        /// 添加编码值
        /// </summary>
        /// <param name="code"></param>
        /// <param name="value"></param>
        public void Add(string code, string value)
        {
            CodeValueDict.Add(code, value);
        }
        /// <summary>
        /// 判断输入值是否符合值域要求
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="message">判断结果信息</param>
        /// <returns></returns>
        public bool CheckValue(object value, out string message)
        {
            bool result = CodeValueDict.ContainsValue(value.ToString());
            message = result ? "" : "不在值域范围";
            return result;
        }
    }
}
