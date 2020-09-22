/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Model
{
    /// <summary>
    /// 表示具有唯一值编码的对象
    /// </summary>
    [Serializable]
    public class CodeObject : IComparable<CodeObject>
    {
        /// <summary>
        /// 序号（序号应≥0）
        /// </summary>
        public int Index { get; set; } = -1;
        /// <summary>
        /// 唯一值编码，通常为ID、编码、代码或图幅号等
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 所在分类
        /// </summary>
        public string Classify { get; set; }


        /// <summary>
        /// 表示具有唯一值编码的对象
        /// </summary>
        public CodeObject()
        {

        }
        /// <summary>
        /// 表示具有唯一值编码的对象
        /// </summary>
        /// <param name="code">唯一值编码（通常为ID、编码、代码或图幅号等）</param>
        /// <param name="name">名称</param>
        public CodeObject(string code, string name)
        {
            this.Code = code;
            this.Name = name;
        }
        /// <summary>
        /// 表示具有唯一值编码的对象
        /// </summary>
        /// <param name="code">唯一值编码（通常为ID、编码、代码或图幅号等）</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        public CodeObject(string code, string name, int index)
        {
            this.Code = code;
            this.Name = name;
            this.Index = index;
        }
        /// <summary>
        /// 表示具有唯一值编码的对象
        /// </summary>
        /// <param name="code">唯一值编码（通常为ID、编码、代码或图幅号等）</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        /// <param name="classify">所在分类</param>
        public CodeObject(string code, string name, int index, string classify)
        {
            this.Code = code;
            this.Name = name;
            this.Index = index;
            this.Classify = classify;
        }


        /// <summary>
        /// 对象的文本显示
        /// </summary>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Name))
                return Index == -1 ? Code : $"{Index}、{Code}";
            else
                return Index == -1 ? $"{Name} - {Code}" : $"{Index}、{Name} - {Code}";
        }
        /// <summary>
        /// 根据序号<see cref="Index"/>排序
        /// </summary>
        /// <param name="itemObject"></param>
        /// <returns></returns>
        public int CompareTo(CodeObject itemObject)
        {
            return itemObject.Index;
        }
    }
}
