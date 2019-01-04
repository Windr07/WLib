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
    public class AttrDomainCollection : IEnumerable
    {
        /// <summary>
        /// 属性域集合
        /// </summary>
        protected List<AttrDomain> Domains = new List<AttrDomain>();

        /// <summary>
        /// 添加属性域
        /// </summary>
        /// <param name="domain"></param>
        public void Add(AttrDomain domain)
        {
            Domains.Add(domain);
        }

        /// <summary>
        /// 添加属性域集合
        /// </summary>
        /// <param name="domains"></param>
        public void AddRange(IEnumerable<AttrDomain> domains)
        {
            this.Domains.AddRange(domains);
        }

        /// <summary>
        /// 根据属性域名获取属性域
        /// </summary>
        /// <param name="domain">属性域名称或属性域描述</param>
        /// <returns></returns>
        public AttrDomain this[string domain]
        {
            get
            {
                return Domains.FirstOrDefault(v => v.Name.Equals(domain) || v.Description.Equals(domain));
            }
        }

        /// <summary>
        /// 获取或设置指定索引处的属性域
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public AttrDomain this[int index]
        {
            get => Domains[index];
            set => Domains[index] = value;
        }

        /// <summary>
        /// 判断集合中是否包含指定的属性域
        /// </summary>
        /// <param name="domainName"></param>
        /// <returns></returns>
        public bool Contains(string domainName)
        {
            var domain = Domains.FirstOrDefault(v => v.Name == domainName);
            return domain != null;
        }

        /// <summary>
        /// 判断集合中是否包含指定的属性域
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public bool Contains(AttrDomain domain)
        {
            return Domains.Contains(domain);
        }

        /// <summary>
        /// 实现IEnumerable方法，允许foreach迭代
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < Domains.Count; i++)
            {
                yield return Domains[i];
            }
        }
    }
}
