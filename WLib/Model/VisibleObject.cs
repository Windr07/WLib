/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020/9
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.Model
{
    /// <summary>
    /// 表示可查看的对象（例如列表中的一项）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class VisibleObject<T>
    {
        /// <summary>
        /// 显示的文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 实际的对象值
        /// </summary>
        public T Value { get; set; }


        /// <summary>
        /// 表示可查看的对象（例如列表中的一项）
        /// </summary>
        public VisibleObject()
        {

        }
        /// <summary>
        /// 表示可查看的对象（例如列表中的一项）
        /// </summary>
        /// <param name="value">实际的对象值</param>
        public VisibleObject(T value)
        {
            Value = value;
            Text = value.ToString();
        }
        /// <summary>
        /// 表示可查看的对象（例如列表中的一项）
        /// </summary>
        /// <param name="text">显示的文本</param>
        /// <param name="value">实际的对象值</param>
        public VisibleObject(string text, T value)
        {
            Text = text;
            Value = value;
        }

        /// <summary>
        /// 输出显示的文本（<see cref="Text"/>）
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Text;
    }
}
