using System;
using System.Runtime.InteropServices;

//À´Ô´(Source by)£º WilsonProgramming
namespace WLib.UserCtrls.ExplorerCtrl
{
    class SystemImageList
    {
        #region Member Variables

        // Used to store the handle of the system image list.
        private static IntPtr m_pImgHandle = IntPtr.Zero;

        // Flags whether we've already retrieved the image list handle or not.
        private static Boolean m_bImgInit = false;

        #endregion

        #region Constants

        // TreeView message constants.
        private const UInt32 TVSIL_NORMAL = 0;
        private const UInt32 TVM_SETIMAGELIST = 4361;

        #endregion

        #region Private Methods

        /// <summary>
        /// Retrieves the handle of the system image list.
        /// </summary>
        private static void InitImageList()
        {
            // Only initialize once.
            if (m_bImgInit)
                throw new Exception("The system image list handle has already been retrieved.");

            // Retrieve the info for a fake file so we can get the image list handle.
            ShellAPI.SHFILEINFO shInfo = new ShellAPI.SHFILEINFO();
            ShellAPI.SHGFI dwAttribs = 
                ShellAPI.SHGFI.SHGFI_USEFILEATTRIBUTES | 
                ShellAPI.SHGFI.SHGFI_SMALLICON |
                ShellAPI.SHGFI.SHGFI_SYSICONINDEX;
            m_pImgHandle = ShellAPI.SHGetFileInfo(".txt", ShellAPI.FILE_ATTRIBUTE_NORMAL, out shInfo, (uint)Marshal.SizeOf(shInfo), dwAttribs);
            
            // Make sure we got the handle.
            if (m_pImgHandle.Equals(IntPtr.Zero))
                throw new Exception("Unable to retrieve system image list handle.");

            // Only allow one initialization.
            m_bImgInit = true;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the image list for the TreeView to the system image list.
        /// </summary>
        /// <param name="tvwHandle">The window handle of the TreeView control</param>
        public static void SetTVImageList(IntPtr tvwHandle)
        {
            InitImageList();
            Int32 hRes = ShellAPI.SendMessage(tvwHandle, TVM_SETIMAGELIST, TVSIL_NORMAL, m_pImgHandle);
            if (hRes != 0)
                Marshal.ThrowExceptionForHR(hRes);
        }

        #endregion
    }
}
