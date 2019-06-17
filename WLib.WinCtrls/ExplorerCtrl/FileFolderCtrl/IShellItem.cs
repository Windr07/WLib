/*---------------------------------------------------------------- 
// auth： https://github.com/code-mirage/FolderBrowserDialog
// date： 
// desc： Visual Studio 2017 default FolderBrowserDialog not practical The component will be overridden
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System.Runtime.InteropServices;

namespace WLib.WinCtrls.ExplorerCtrl.FileFolderCtrl
{

    [ComImport]
    [Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IShellItem
    {
        void BindToHandler(); // not fully defined
        void GetParent(); // not fully defined
        void GetDisplayName([In] SIGDN sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);
        void GetAttributes();  // not fully defined
        void Compare();  // not fully defined
    }
}
