/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Db.TableInfo.FieldDomain
{
    /// <summary>
    /// 字段值域
    /// </summary>
    public interface IFieldDomain
    {
        /// <summary>
        /// 字段值域类型（无限制、范围、字典表）
        /// </summary>
        EFieldDomainType FieldDomianType { get; }
        /// <summary>
        /// 判断输入值是否符合值域要求
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="message">判断结果信息</param>
        /// <returns></returns>
        bool CheckValue(object value, out string message);
    }
}
