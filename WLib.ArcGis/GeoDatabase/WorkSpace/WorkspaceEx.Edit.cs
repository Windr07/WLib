/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using ESRI.ArcGIS.Geodatabase;

namespace WLib.ArcGis.GeoDatabase.WorkSpace
{
    /// <summary>
    /// 工作空间编辑操作
    /// </summary>
    public static partial class WorkspaceEditEx
    {
        /// <summary>
        /// 开始工作空间编辑，执行完指定操作后保存并停止编辑
        /// </summary>
        /// <param name="workspace">需要执行编辑的工作空间</param>
        /// <param name="action">具体的编辑操作</param>
        /// <param name="withUndoEdit">是否在编辑过程中支持回滚（撤消/重做）操作，远程数据库不支持此功能</param>
        /// <remarks></remarks>
        public static void StartEdit(this IWorkspace workspace, Action action, bool withUndoEdit = false)
        {
            IWorkspaceEdit workspaceEdit = (IWorkspaceEdit)workspace;
            workspaceEdit.StartEditing(withUndoEdit);
            workspaceEdit.StartEditOperation();

            action();

            workspaceEdit.StopEditOperation();
            workspaceEdit.StopEditing(true);
        }
    }
}
