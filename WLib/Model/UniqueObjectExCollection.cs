/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017/3/6 11:14:41
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WLib.Model
{
    /// <summary>
    /// 唯一值对象集合，提供排序方法
    /// </summary>
    public class UniqueObjectExCollection : IEnumerable
    {
        /// <summary>
        /// 列表名
        /// </summary>
        public string ListName;
        /// <summary>
        /// 唯一值对象集合
        /// </summary>
        public List<UniqueObjectEx> UniqueObjectExs;

        /// <summary>
        /// 唯一值对象列表，提供排序方法
        /// </summary>
        /// <param name="listName">列表名</param>
        public UniqueObjectExCollection(string listName)
        {
            this.ListName = listName;
            UniqueObjectExs = new List<UniqueObjectEx>();
        }
        /// <summary>
        /// 添加一个唯一值对象
        /// </summary>
        /// <param name="item"></param>
        public void Add(UniqueObjectEx item)
        {
            UniqueObjectExs.Add(item);
        }
        /// <summary>
        /// 添加一个包含编码、名称的唯一值对象
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        public void Add(string code, string name)
        {
            UniqueObjectExs.Add(new UniqueObjectEx(code, name));
        }
        /// <summary>
        /// 添加一个包含编码、名称、序号的唯一值对象
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        public void Add(string code, string name, int index)
        {
            UniqueObjectExs.Add(new UniqueObjectEx(code, name, index));
        }
        /// <summary>
        /// 添加一个包含编码、名称、序号、所在分组的唯一值对象
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        /// <param name="classify">所在分组</param>
        public void Add(string code, string name, int index, string classify)
        {
            UniqueObjectExs.Add(new UniqueObjectEx(code, name, index, null, classify));
        }
        /// <summary>
        /// 添加一个包含编码、名称、序号、所在分组、备注信息的唯一值对象
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        /// <param name="classify">所在分组</param>
        /// <param name="comment">备注信息</param>
        public void Add(string code, string name, int index, string classify, string comment)
        {
            UniqueObjectExs.Add(new UniqueObjectEx(code, name, index, comment, classify));
        }
        /// <summary>
        /// 添加多个唯一值对象
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(IEnumerable<UniqueObjectEx> items)
        {
            UniqueObjectExs.AddRange(items);
        }
        /// <summary>
        /// 清空备注
        /// </summary>
        public void ClearComment()
        {
            UniqueObjectExs.ForEach(v => v.Comment = null);
        }
        /// <summary>
        /// 实现IEnumerable方法，允许foreach迭代
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < UniqueObjectExs.Count; i++)
            {
                yield return UniqueObjectExs[i];
            }
        }

        /// <summary>
        /// 根据名称排序
        /// </summary>
        public void SortByName()
        {
            UniqueObjectExs.Sort((x, y) =>
            {
                if (x.Name == y.Name)
                    return 0;
                else if (x.Name.CompareTo(y.Name) > 0)
                    return 1;
                else
                    return -1;
            });
        }
        /// <summary>
        /// 根据编码排序
        /// </summary>
        public void SortByCode()
        {
            UniqueObjectExs.Sort((x, y) =>
            {
                if (x.Code == y.Code)
                    return 0;
                else if (x.Code.CompareTo(y.Code) > 0)
                    return 1;
                else
                    return -1;
            });
        }
        /// <summary>
        /// 根据分组、名称排序
        /// </summary>
        public void SortByClassifyName()
        {
            UniqueObjectExs.Sort((x, y) =>
            {
                if (x.Classify == y.Classify && x.Name == y.Name)
                    return 0;
                else if (x.Classify == y.Classify && x.Name.CompareTo(y.Name) > 0)
                    return 1;
                else if (x.Classify.CompareTo(y.Classify) > 0)
                    return 1;
                else
                    return -1;
            });
        }
        /// <summary>
        /// 获取分组
        /// </summary>
        /// <returns></returns>
        public UniqueObjectEx[] GetClassify(string classify)
        {
            return UniqueObjectExs.Where(v => v.Classify == classify).ToArray();
        }

        /// <summary>
        /// 重新设置序号（一般用在排序之后）
        /// </summary>
        public void ResetIndex()
        {
            for (int i = 0; i < UniqueObjectExs.Count; i++)
            {
                UniqueObjectExs[i].Index = i + 1;
            }
        }
    }
}
