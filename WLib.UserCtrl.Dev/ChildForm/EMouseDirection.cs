using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WLib.UserCtrls.Dev.ChildForm
{
    /// <summary>
    /// 表示鼠标的拖动方向
    /// </summary>
    internal enum EMouseDirection
    {
        /// <summary>
        /// 水平方向拖动，只改变窗体的宽度
        /// </summary>
        Herizontal,
        /// <summary>
        /// 垂直方向拖动，只改变窗体的高度 
        /// </summary>
        Vertical,
        /// <summary>
        /// 倾斜方向，同时改变窗体的宽度和高度 
        /// </summary>
        Declining,
        /// <summary>
        /// 不做标志，即不拖动窗体改变大小 
        /// </summary>
        None
    }
}
