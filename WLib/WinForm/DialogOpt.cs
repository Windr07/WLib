/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Windows.Forms;

namespace WLib.WinForm
{
    /// <summary>
    /// 对话框操作
    /// </summary>
    public static class DialogOpt
    {
        /// <summary>
        /// 弹出文件对话框，返回所选的文件路径，未选择时返回null
        /// </summary>
        /// <param name="dialog"></param>
        /// <param name="filter"></param>
        /// <param name="title"></param>
        /// <param name="fileName"></param>
        /// <param name="initDir"></param>
        /// <returns></returns>
        public static string ShowDialog(this FileDialog dialog, string filter, string title = null, string fileName = null, string initDir = null)
        {
            dialog.Filter = filter;
            dialog.Title = title;
            dialog.FileName = fileName;
            dialog.InitialDirectory = initDir;
            return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
        }
        /// <summary>
        /// 弹出选择目录对话框，返回所选的目录路径，未选择时返回null
        /// </summary>
        /// <param name="dialog"></param>
        /// <param name="selectPath"></param>
        /// <param name="description"></param>
        /// <param name="showNewFolderButton"></param>
        /// <returns></returns>
        public static string ShowDialog(this FolderBrowserDialog dialog, string selectPath = null, string description = null, bool showNewFolderButton = true)
        {
            dialog.Description = description;
            dialog.ShowNewFolderButton = showNewFolderButton;
            dialog.SelectedPath = selectPath;
            return dialog.ShowDialog() == DialogResult.OK ? dialog.SelectedPath : null;
        }


        /// <summary>
        /// 弹出保存文件对话框，返回所选的文件路径，未选择时返回null
        /// </summary>
        /// <param name="filter">文件格式</param>
        /// <param name="title">标题</param>
        /// <param name="fileName">文件名或文件路径</param>
        /// <param name="initDir">初始目录</param>
        /// <returns></returns>
        public static string ShowSaveFileDialog(string filter, string title = null, string fileName = null, string initDir = null)
        {
            if (!string.IsNullOrWhiteSpace(initDir) && !System.IO.Path.IsPathRooted(initDir))
                initDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, initDir);

            var dialog = new SaveFileDialog
            {
                Filter = filter,
                Title = title,
                FileName = fileName,
                InitialDirectory = initDir
            };
            return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
        }
        /// <summary>
        /// 弹出打开文件对话框，返回所选的文件路径，未选择时返回null
        /// </summary>
        /// <param name="filter">文件格式</param>
        /// <param name="title">标题</param>
        /// <param name="fileName">文件名或文件路径</param>
        /// <param name="initDir">初始目录</param>
        /// <param name="multiSelect">多选</param>
        /// <returns></returns>
        public static string ShowOpenFileDialog(string filter, string title = null, string fileName = null, string initDir = null, bool multiSelect = false)
        {
            if (!string.IsNullOrWhiteSpace(initDir) && !System.IO.Path.IsPathRooted(initDir))
                initDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, initDir);

            var dialog = new OpenFileDialog
            {
                Filter = filter,
                Title = title,
                FileName = fileName,
                InitialDirectory = initDir,
                Multiselect = multiSelect
            };
            return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
        }
        /// <summary>
        /// 弹出选择目录对话框，返回所选的目录路径，未选择时返回null
        /// </summary>
        /// <param name="selectPath">初始目录路径</param>
        /// <param name="description">描述</param>
        /// <param name="showNewFolderButton">是否显示新建文件夹按钮</param>
        /// <returns></returns>
        public static string ShowFolderBrowserDialog(string selectPath = null, string description = null, bool showNewFolderButton = true)
        {
            var dialog = new FolderBrowserDialog
            {
                Description = description,
                ShowNewFolderButton = showNewFolderButton,
                SelectedPath = selectPath
            };
            return dialog.ShowDialog() == DialogResult.OK ? dialog.SelectedPath : null;
        }
    }
}
