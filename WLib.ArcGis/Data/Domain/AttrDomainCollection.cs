/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WLib.ArcGis.Data.Domain
{
    /// <summary>
    /// 属性域集合
    /// </summary>
    public class AttrDomainCollection : List<AttrDomain>
    {
        /// <summary>
        /// 根据属性域名获取属性域
        /// </summary>
        /// <param name="domain">属性域名称或属性域描述</param>
        /// <returns></returns>
        public AttrDomain this[string domain]
        {
            get
            {
                return this.FirstOrDefault(v => v.Name.Equals(domain) || v.Description.Equals(domain));
            }
        }
        /// <summary>
        /// 判断集合中是否包含指定的属性域
        /// </summary>
        /// <param name="domainName"></param>
        /// <returns></returns>
        public bool Contains(string domainName)
        {
            var domain = this.FirstOrDefault(v => v.Name == domainName);
            return domain != null;
        }
    }
}
