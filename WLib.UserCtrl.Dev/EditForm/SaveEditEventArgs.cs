/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace WLib.UserCtrls.Dev.EditForm
{
    /// <summary>
    /// 为YYGISLib.UserDevControls.EditForm的SaveEditEvent事件提供数据（一条记录中被编辑的字段值）
    /// </summary>
    public class SaveEditEventArgs : EventArgs
    {
        /// <summary>
        /// 一条记录中被编辑的字段值
        /// </summary>
        public List<EditItem> NewEditItemList { get; set; }
        /// <summary>
        /// 为YYGISLib.UserDevControls.EditForm的SaveEditEvent事件提供数据（一条记录中被编辑的字段值）
        /// </summary>
        /// <param name="item">被编辑的一条记录</param>
        public SaveEditEventArgs(List<EditItem> item)
        {
            this.NewEditItemList = item;
        }
    }
}
