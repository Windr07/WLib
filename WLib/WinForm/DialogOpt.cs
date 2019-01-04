/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Windows.Forms;

namespace WLib.WinForm
{
    /// <summary>
    /// 对话框操作
    /// </summary>
    public class DialogOpt
    {
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
            var saveFileDlg = new SaveFileDialog
            {
                Filter = filter,
                Title = title,
                FileName = fileName,
                InitialDirectory = initDir
            };
            return saveFileDlg.ShowDialog() == DialogResult.OK ? saveFileDlg.FileName : null;
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
        public static string ShowOpenFileDialog(string filter, string title = null, string fileName = null,
            string initDir = null, bool multiSelect = false)
        {
            var openFileDlg = new OpenFileDialog
            {
                Filter = filter,
                Title = title,
                FileName = fileName,
                InitialDirectory = initDir,
                Multiselect = multiSelect
            };
            return openFileDlg.ShowDialog() == DialogResult.OK ? openFileDlg.FileName : null;
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
            var fbDialog = new FolderBrowserDialog
            {
                Description = description,
                ShowNewFolderButton = showNewFolderButton,
                SelectedPath = selectPath
            };
            return fbDialog.ShowDialog() == DialogResult.OK ? fbDialog.SelectedPath : null;
        }
    }
}
