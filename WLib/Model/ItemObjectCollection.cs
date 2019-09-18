/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017/3/6 11:14:41
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;

namespace WLib.Model
{
    /// <summary>
    /// 放入列表中的对象集合，提供排序方法
    /// </summary>
    public class ItemObjectCollection : List<ItemObject>
    {
        /// <summary>
        /// 列表名
        /// </summary>
        public string ListName { get; set; }
        /// <summary>
        /// 放入列表中的对象集合，提供排序方法
        /// </summary>
        /// <param name="listName">列表名</param>
        public ItemObjectCollection(string listName)
        {
            this.ListName = listName;
        }


        /// <summary>
        /// 添加一个包含编码、名称的列表项对象
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        public void Add(string code, string name)
        {
            this.Add(new ItemObject(code, name));
        }
        /// <summary>
        /// 添加一个包含编码、名称、序号的列表项对象
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        public void Add(string code, string name, int index)
        {
            this.Add(new ItemObject(code, name, index));
        }
        /// <summary>
        /// 添加一个包含编码、名称、序号、所在分类的列表项对象
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        /// <param name="classify">所在分类</param>
        public void Add(string code, string name, int index, string classify)
        {
            this.Add(new ItemObject(code, name, index, classify));
        }
        /// <summary>
        /// 添加一个包含编码、名称、序号、所在分类、关联数据的列表项对象
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        /// <param name="classify">所在分类</param>
        /// <param name="tag">与对象关联的用户定义数据</param>
        public void Add<T>(string code, string name, int index, string classify, T tag)
        {
            this.Add(new ItemObject<T>(code, name, index, classify, tag));
        }


        /// <summary>
        /// 根据名称排序
        /// </summary>
        public void SortByName()
        {
            this.Sort((x, y) =>
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
            this.Sort((x, y) =>
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
            this.Sort((x, y) =>
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
        public ItemObject[] GetClassify(string classify)
        {
            return this.Where(v => v.Classify == classify).ToArray();
        }
        /// <summary>
        /// 重新设置序号（一般用在排序之后）
        /// </summary>
        public void ResetIndex()
        {
            for (int i = 0; i < this.Count; i++)
                this[i].Index = i + 1;
        }
    }
}
