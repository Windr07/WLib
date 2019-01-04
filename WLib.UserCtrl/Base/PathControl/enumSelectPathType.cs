/*---------------------------------------------------------------- 
// auth： HuangWenlong
// date： 2016/12/22 17:28:29
// desc： None
// mdfy:  None
// ver.:  V1.0.0
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YYGISLib.UserControls.PathControl
{
    /// <summary>
    /// 路径类别（文件夹路径、打开文件路径、保存文件路径、自定义路径选择）
    /// </summary>
    public enum enumSelectPathType
    {
        /// <summary>
        /// 文件夹
        /// </summary>
        Folder,
        /// <summary>
        /// 打开文件
        /// </summary>
        OpenFile,
        /// <summary>
        /// 保存文件
        /// </summary>
        SaveFile,
        /// <summary>
        /// 自定义
        /// </summary>
        Customize,
    }
}
