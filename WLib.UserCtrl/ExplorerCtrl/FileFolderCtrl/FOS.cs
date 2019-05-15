/*---------------------------------------------------------------- 
// auth： https://github.com/code-mirage/FolderBrowserDialog
// date： 
// desc： Visual Studio 2017 default FolderBrowserDialog not practical The component will be overridden
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System;

namespace WLib.UserCtrls.ExplorerCtrl.FileFolderCtrl
{
    [Flags]
    public enum FOS
    {
        FOS_ALLNONSTORAGEITEMS = 0x80,
        FOS_ALLOWMULTISELECT = 0x200,
        FOS_CREATEPROMPT = 0x2000,
        FOS_DEFAULTNOMINIMODE = 0x20000000,
        FOS_DONTADDTORECENT = 0x2000000,
        FOS_FILEMUSTEXIST = 0x1000,
        FOS_FORCEFILESYSTEM = 0x40,
        FOS_FORCESHOWHIDDEN = 0x10000000,
        FOS_HIDEMRUPLACES = 0x20000,
        FOS_HIDEPINNEDPLACES = 0x40000,
        FOS_NOCHANGEDIR = 8,
        FOS_NODEREFERENCELINKS = 0x100000,
        FOS_NOREADONLYRETURN = 0x8000,
        FOS_NOTESTFILECREATE = 0x10000,
        FOS_NOVALIDATE = 0x100,
        FOS_OVERWRITEPROMPT = 2,
        FOS_PATHMUSTEXIST = 0x800,
        FOS_PICKFOLDERS = 0x20,
        FOS_SHAREAWARE = 0x4000,
        FOS_STRICTFILETYPES = 4
    }
}
