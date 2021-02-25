/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using WLib.Database.TableInfo;

namespace WLib.WinCtrls.Dev.EditCtrl
{
    /// <summary>
    /// 为<see cref=" RowDataEditForm"/>的SaveEditEvent事件提供数据（一条记录中被编辑的字段值）
    /// </summary>
    public class SaveEditEventArgs : EventArgs
    {
        /// <summary>
        /// 一条记录中被编辑的字段值
        /// </summary>
        public List<EditItem> NewEditItems { get; set; }
        /// <summary>
        /// 数据操作状态，0为编辑，1为新增
        /// </summary>
        public EOptState OptState { get; set; }
        /// <summary>
        /// 保存之后是否允许后续的保存操作，默认true
        /// </summary>
        public bool EnableSaveOperation { get; set; } = true;
        /// <summary>
        /// 保存之后是否关闭数据编辑界面，默认false
        /// </summary>
        public bool CloseView { get; set; }
        /// <summary>
        /// 为<see cref=" RowDataEditForm"/>的SaveEditEvent事件提供数据（一条记录中被编辑的字段值）
        /// </summary>
        public SaveEditEventArgs() { }
        /// <summary>
        /// 为<see cref=" RowDataEditForm"/>的SaveEditEvent事件提供数据（一条记录中被编辑的字段值）
        /// </summary>
        /// <param name="items">被编辑的一条记录</param>
        /// <param name="optState">数据操作状态，0为编辑，1为新增</param>
        /// <param name="enableSaveOperation">保存之后是否允许后续的保存操作</param>
        /// <param name="closeView">保存之后是否关闭数据编辑界面</param>
        public SaveEditEventArgs(List<EditItem> items, EOptState optState, bool enableSaveOperation = true, bool closeView = false)
        {
            this.NewEditItems = items;
            this.OptState = optState;
            this.EnableSaveOperation = enableSaveOperation;
            this.CloseView = closeView;
        }
    }
}
