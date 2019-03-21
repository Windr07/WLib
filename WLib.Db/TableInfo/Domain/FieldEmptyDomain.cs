/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Db.TableInfo.Domain
{
    /// <summary>
    /// 无限制值域
    /// </summary>
    public class FieldEmptyDomain : IFieldDomain
    {
        /// <summary>
        /// 字段值域类型
        /// </summary>
        public EFieldDomainType FieldDomianType => EFieldDomainType.None;

        /// <summary>
        /// 判断输入值是否符合值域要求
        /// </summary>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool CheckValue(object value, out string message)
        {
            message = string.Empty;
            return true;
        }
    }
}
