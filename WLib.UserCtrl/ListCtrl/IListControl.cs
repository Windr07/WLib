/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/3/27 15:53:29
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// 
        /// </summary>
        IList SelectedItems { get; }
        /// <summary>
        /// 
        /// </summary>
        IList Items { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        void AddItems(IEnumerable<object> items);
        /// <summary>
        /// 
        /// </summary>
        event EventHandler SelectedIndexChanged;
    }
}
