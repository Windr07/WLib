/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/7/25
// desc： 参考：https://www.cnblogs.com/yuefei/p/4062998.html
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Drawing;

namespace WLib.WinCtrls.ListCtrl
{
    /// <summary>
    /// <see cref="ImageListBoxEx"/>控件中的每一项
    /// </summary>
    public class ListBoxItem : IDisposable
    {
        /// <summary>
        /// 列表项的图标
        /// </summary>
        [System.ComponentModel.DefaultValue(typeof(System.Drawing.Image), "null")]
        public Image Image { get; set; }
        /// <summary>
        /// 列表项关联的数据
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 列表项显示的标题
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// 列表项显示的子内容
        /// </summary>
        public string[] Details { get; set; }
        /// <summary>
        /// 是否已获取焦点
        /// </summary>
        public bool IsFocus { get; set; }


        public ListBoxItem() { }
        public ListBoxItem(string caption) => Caption = caption;
        public ListBoxItem(string caption, Image image) : this(caption) => Image = image;
        public ListBoxItem(string caption, Image image, string[] details) : this(caption, image) => Details = details;
        public ListBoxItem(string caption, Image image, string[] details, object value) : this(caption, image, details) => Value = value;
        public void Dispose() => Image = null;
    }
}
