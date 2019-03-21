/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Db.TableInfo.FieldDomain
{
    /// <summary>
    /// 对数值类型字段的值域限制方式枚举
    /// （大于，大于等于，小于，小于等于等）
    /// </summary>
    public enum EFieldValueRanage
    {
        /// <summary>
        /// 大于指定值
        /// </summary>
        大于,
        /// <summary>
        /// 大于等于指定值
        /// </summary>
        大于等于,
        /// <summary>
        /// 小于指定值
        /// </summary>
        小于,
        /// <summary>
        /// 小于等于指定值
        /// </summary>
        小于等于,
        /// <summary>
        /// 等于指定值
        /// </summary>
        等于,
    }
}
