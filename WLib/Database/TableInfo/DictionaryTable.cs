/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace WLib.Database.TableInfo
{
    /// <summary>
    /// 字典表
    /// </summary>
    [Serializable]
    public class DictionaryTable : List<DictionaryPair>
    {
        /// <summary>
        /// 字典表名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 字典表别名
        /// </summary>
        public string AliasName { get; set; }
        /// <summary>
        /// 获取全部键
        /// </summary>
        public IEnumerable<string> Keys { get { foreach (var pair in this) yield return pair.Key; } }
        /// <summary>
        /// 获取全部值
        /// </summary>
        public IEnumerable<string> Values { get { foreach (var pair in this) yield return pair.Value; } }
        /// <summary>
        /// 获取或设置与指定的键关联的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get => this.Find(v => v.Key == key).Value;
            set
            {
                var pair = this.Find(v => v.Key == key);
                if (pair == null) Add(key, value);
                else pair.Value = value;
            }
        }


        /// <summary>
        /// 字典表
        /// </summary>
        public DictionaryTable() : base() { }
        /// <summary>
        /// 字典表
        /// </summary>
        /// <param name="name">字典表名</param>
        public DictionaryTable(string name) : this() => this.AliasName = this.Name = name;
        /// <summary>
        /// 字典表
        /// </summary>
        /// <param name="name">字典表名</param>
        /// <param name="aliasName">字典表别名</param>
        public DictionaryTable(string name, string aliasName) : this(name) => this.AliasName = aliasName;
        /// <summary>
        /// 字典表
        /// </summary>
        /// <param name="name">字典表名</param>
        /// <param name="aliasName">字典表别名</param>
        /// <param name="keyValuePairs">编码和名称</param>
        public DictionaryTable(string name, string aliasName, IEnumerable<KeyValuePair<string, string>> keyValuePairs) : this(name, aliasName) => AddRange(keyValuePairs);


        /// <summary>
        /// 添加一对键值对
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, string value)
        {
            if (this.ContainsKey(key))
                throw new Exception("已存在重复的键：" + key);
            Add(new DictionaryPair(key, value));
        }
        /// <summary>
        /// 添加一对多个键值对
        /// </summary>
        /// <param name="keyValuePairs"></param>
        public void AddRange(IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            foreach (var pair in keyValuePairs)
                Add(pair.Key, pair.Value);
        }
        /// <summary>
        /// 判断字典中是否包含指定键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key) => this.Any(v => v.Key == key);
        /// <summary>
        /// 判断字典中是否包含指定值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ContainsValue(string value) => this.Any(v => v.Value == value);
        /// <summary>
        /// 获取与指定键关联的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(string key, out string value)
        {
            var pair = this.Find(v => v.Key == key);
            if (pair == null)
            {
                value = null;
                return false;
            }
            else
            {
                value = pair.Key;
                return true;
            }
        }
        /// <summary>
        /// 获取与指定值关联的第一个键
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool TryGetKey(string value, out string key)
        {
            var pair = this.Find(v => v.Value == value);
            if (pair == null)
            {
                key = null;
                return false;
            }
            else
            {
                key = pair.Key;
                return true;
            }
        }
        /// <summary>
        /// 将<see cref="DictionaryTable"/>转成<see cref="Dictionary{TKey, TValue} "/>
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> CopyToDictionary()
        {
            var dict = new Dictionary<string, string>();
            foreach (var pair in this)
                dict.Add(pair.Key, pair.Value);
            return dict;
        }
    }
}
