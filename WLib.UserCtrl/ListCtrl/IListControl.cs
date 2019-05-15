/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/3/27 15:53:29
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WLib.UserCtrls.ListCtrl
{
    /// <summary>
    /// 列表控件
    /// </summary>
    public interface IListControl
    {
        /// <summary>
        /// 指定的位置和控件停靠的方式
        /// </summary>
        DockStyle Dock { get; set; }
        /// <summary>
        /// 选定的项的集合
        /// </summary>
        IList SelectedItems { get; }
        /// <summary>
        /// 列表控件项的集合
        /// </summary>
        IList Items { get; }
        /// <summary>
        /// 向列表控件添加多个项
        /// </summary>
        /// <param name="items"></param>
        void AddItems(IEnumerable<object> items);
        /// <summary>
        /// 选定项发生改变的事件
        /// </summary>
        event EventHandler SelectedIndexChanged;
    }
}
