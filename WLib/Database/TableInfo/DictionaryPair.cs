using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WLib.Database.TableInfo
{
    /// <summary>
    /// 表示字典中的一个键值对
    /// </summary>
    public class DictionaryPair
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }


        /// <summary>
        /// 表示字典中的一个键值对
        /// </summary>
        public DictionaryPair() { }
        /// <summary>
        /// 表示字典中的一个键值对
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public DictionaryPair(string key, string value)
        {
            Key = key;
            Value = value;
        }


        /// <summary>
        /// 输出： Key + " - " + Value
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Key + " - " + Value;
    }
}
