/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Db.TableInfo.Domain
{
    /// <summary>
    /// 字段值域类型
    /// </summary>
    public enum EFieldDomainType
    {
        /// <summary>
        /// 范围
        /// </summary>
        Range,
        /// <summary>
        /// 字典表
        /// </summary>
        Dictionary,
        /// <summary>
        /// 无值域限制
        /// </summary>
        None
    }
}
