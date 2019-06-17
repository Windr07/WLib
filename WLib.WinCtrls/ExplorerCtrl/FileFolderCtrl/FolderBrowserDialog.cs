/*---------------------------------------------------------------- 
// auth： https://github.com/code-mirage/FolderBrowserDialog
// date： 
// desc： Visual Studio 2017 default FolderBrowserDialog not practical The component will be overridden
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WLib.WinCtrls.ExplorerCtrl.FileFolderCtrl
{
    /// <summary>
    /// 提供只能选择文件夹的打开文件对话框（OpenFileDialog, Open Folder）
    /// </summary>
    [Description("提供一個Vista樣式的選擇文件夹對話框")]
    [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
    public class FolderBrowserDialog : Component
    {
        /// <summary>
        /// 提供只能选择文件夹的打开文件对话框（OpenFileDialog, Open Folder）
        /// </summary>
        public FolderBrowserDialog()
        {
        }
        /// <summary>
        /// 选择或设置目录路径
        /// </summary>
        public string DirectoryPath { get; set; }
        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public DialogResult ShowDialog(IWin32Window owner)
        {
            IntPtr hwndOwner = owner?.Handle ?? GetActiveWindow();
            IFileOpenDialog dialog = (IFileOpenDialog)new FileOpenDialog();

            try
            {
                IShellItem item;
                if (!string.IsNullOrEmpty(DirectoryPath))
                {
                    uint atts = 0;
                    if (SHILCreateFromPath(DirectoryPath, out var idl, ref atts) == 0)
                        if (SHCreateShellItem(IntPtr.Zero, IntPtr.Zero, idl, out item) == 0)
                            dialog.SetFolder(item);
                }
                dialog.SetOptions(FOS.FOS_PICKFOLDERS | FOS.FOS_FORCEFILESYSTEM);
                uint hr = dialog.Show(hwndOwner);
                if (hr == ERROR_CANCELLED)
                    return DialogResult.Cancel;

                if (hr != 0)
                    return DialogResult.Abort;
                dialog.GetResult(out item);
                item.GetDisplayName(SIGDN.SIGDN_FILESYSPATH, out var path);
                DirectoryPath = path;
                return DialogResult.OK;
            }
            finally
            {
                Marshal.ReleaseComObject(dialog);
            }
        }


        [DllImport("shell32.dll")]
        private static extern int SHILCreateFromPath([MarshalAs(UnmanagedType.LPWStr)] string pszPath, out IntPtr ppIdl, ref uint rgflnOut);
        [DllImport("shell32.dll")]
        private static extern int SHCreateShellItem(IntPtr pidlParent, IntPtr psfParent, IntPtr pidl, out IShellItem ppsi);
        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();
        private const uint ERROR_CANCELLED = 0x800704C7;
    }
}
