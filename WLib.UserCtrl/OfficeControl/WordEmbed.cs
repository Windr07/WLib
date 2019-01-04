using System;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using WLib.WindowsAPI;

namespace WLib.UserCtrls.OfficeControl
{
    public class WordEmbed : IOfficeBase
    {
        public WordEmbed()
        {
        }
        private Document _document;
        private ApplicationClass _wdClass;
        private IntPtr _wordWnd = IntPtr.Zero;
        private object _nothing = System.Reflection.Missing.Value;

        public bool HasFileLoaded => _document != null;

        public string FileName => _document?.FullName;

        /// <summary>
        /// 加载word文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// /// <param name="handleId">父窗口句柄ID</param>
        public void LoadFile(string fileName, int handleId, bool readOnly = true)
        {
            try
            {
                if (_wdClass == null)
                {
                    _wdClass = new ApplicationClass();
                }

                //wdClass.CommandBars.AdaptiveMenus = false;
                _wdClass.DocumentBeforeClose += new ApplicationEvents4_DocumentBeforeCloseEventHandler(wdClass_DocumentBeforeClose);
                //wdClass.docum
                if (_document != null)
                {
                    _wdClass.Documents.Close(ref _nothing, ref _nothing, ref _nothing);
                }
                ///加载文档
                if (_wordWnd == IntPtr.Zero)
                {
                    IntPtr t1 = WinApi.FindWindow("Opusapp", null);
                    IntPtr t2 = WinApi.FindWindowEx(System.IntPtr.Zero, System.IntPtr.Zero, null, _wdClass.Caption);
                    //wordWnd = WinAPI.FindWindow("Opusapp", null);
                    _wordWnd = WinApi.FindWindowEx(System.IntPtr.Zero, System.IntPtr.Zero, null, _wdClass.Caption);
                    WinApi.SetParent(_wordWnd.ToInt32(), handleId);

                    object filename = fileName;
                    object newTemplate = false;
                    object docType = 0;
                    object oReadOnly = false;
                    object isVisible = true;
                    if (_wdClass != null && _wdClass.Documents != null)
                    {
                        _document = _wdClass.Documents.Open(ref filename, ref _nothing,
                            ref oReadOnly, ref _nothing, ref _nothing,
                            ref _nothing, ref _nothing, ref _nothing,
                            ref _nothing, ref _nothing, ref _nothing,
                            ref isVisible, ref _nothing, ref _nothing,
                            ref _nothing, ref _nothing);
                    }
                    if (_document == null)
                    {
                        //throw new Exception("Word文档加载失败！");
                    }
                    else
                    {
                        if (readOnly)
                            _document.Protect(WdProtectionType.wdAllowOnlyReading, Password: "");
                    }
                }
                SetWordStyle(handleId);
                //SetCommandBars();//工具栏就不隐藏了
            }
            catch (Exception ex)
            {
                if (_wordWnd != IntPtr.Zero)
                {
                    WinApi.SendMessage(_wordWnd, WinApi.WM_CLOSE, 0, 0);
                    _wordWnd = IntPtr.Zero;
                }
                ////COM.LIB.LOG.LogOperator.WriteLog(ex.ToString());
            }
        }

        /// <summary>
        /// 设置打开word文档的样式 
        /// </summary>
        /// <param name="handleId"></param>
        private void SetWordStyle(int handleId)
        {
            try
            {
                if (_wdClass != null)
                {
                    //wdClass.ActiveWindow.DisplayHorizontalScrollBar = false;
                    //wdClass.ActiveWindow.DisplayLeftScrollBar = false;
                    //wdClass.ActiveWindow.DisplayRightRuler = false;
                    //wdClass.ActiveWindow.DisplayRulers = false;
                    //wdClass.ActiveWindow.DisplayScreenTips = false;
                    //wdClass.ActiveWindow.DisplayVerticalRuler = false;
                    //wdClass.ActiveWindow.DisplayVerticalScrollBar = false;
                    //wdClass.ActiveWindow.ActivePane.DisplayRulers = false;
                    //wdClass.ActiveWindow.ActivePane.DisplayVerticalRuler = false;

                    //wdClass.ActiveWindow.Thumbnails = true;

                    _wdClass.ActiveWindow.ActivePane.View.Type = WdViewType.wdPrintView;
                    //wdClass.ActiveWindow.EnvelopeVisible = true;

                    _document.Activate();
                    _wdClass.Visible = true;
                    _wdClass.ActiveWindow.Activate();
                    //wdClass.Activate();
                    //wdClass.MouseAvailable = true;
                    WinApi.SetWindowPos(_wordWnd, handleId, 0, 0,
                        Form.FromHandle((IntPtr)handleId).Bounds.Width,
                        Form.FromHandle((IntPtr)handleId).Bounds.Height,
                        SetWindowPosFlags.SWP_NOZORDER |
                        SetWindowPosFlags.SWP_NOMOVE |
                        SetWindowPosFlags.SWP_DRAWFRAME |
                        SetWindowPosFlags.SWP_NOSIZE);
                    OnResize(handleId);
                    //wdClass.MouseAvailable
                    //WinAPI.SetCapture(wordWnd);
                }
            }
            catch
            {
                ////COM.LIB.LOG.LogOperator.WriteLog(ex.ToString());
            }
        }

        ///// <summary>
        ///// 设置word上面工具栏
        ///// </summary>
        //private void SetCommandBars()
        //{
        //    if (wdClass != null)
        //    {
        //        int count = wdClass.CommandBars.Count;
        //        for (int j = 1; j <= count; j++)
        //        {
        //            string menuName = wdClass.CommandBars[j].Name;
        //            if (menuName == "Standard")
        //            {
        //                int count_control = wdClass.ActiveWindow.Application.CommandBars[j].Controls.Count;
        //                for (int k = 1; k <= 2; k++)
        //                {
        //                    wdClass.ActiveWindow.Application.CommandBars[j].Controls[k].Enabled = false;
        //                }
        //            }
        //            if (menuName == "Menu Bar")
        //            {
        //                wdClass.ActiveWindow.Application.CommandBars[j].Enabled = false;
        //            }
        //            menuName = "";
        //        }
        //        ///移除按钮
        //        int hMenu = WinAPI.GetSystemMenu(wordWnd, false);
        //        if (hMenu > 0)
        //        {
        //            int menuItemCount = WinAPI.GetMenuItemCount(hMenu);
        //            for (int m = 1; m <= 8; m++)
        //            {
        //                WinAPI.RemoveMenu(hMenu, menuItemCount - m, WinAPI.MF_REMOVE | WinAPI.MF_BYPOSITION);
        //            }
        //            WinAPI.DrawMenuBar(wordWnd);
        //        }
        //    }

        //}

        /// <summary>
        /// 保存打开的文件
        /// </summary>
        public void Save()
        {
            if (_document != null)//只做查看，不须更改
            {
                //document.Save();
            }
        }

        /// <summary>
        /// 关闭打开的文件
        /// </summary>
        public void Close()
        {
            try
            {
                //Save();

                object saveChanges = false;
                Object missingValue = System.Reflection.Missing.Value;
                _document.Saved = true;

                //document.Close(ref MissingValue, ref MissingValue, ref MissingValue);//此处出错（不知为何）
                _wdClass.Quit(saveChanges, Type.Missing, Type.Missing);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(_wdClass);
                GC.Collect();
                int lpdwProcessId;
                WinApi.GetWindowThreadProcessId(_wordWnd, out lpdwProcessId);
                System.Diagnostics.Process.GetProcessById(lpdwProcessId).Kill();
                _wdClass = null;
            }
            catch (System.Exception ex)
            {
                ////COM.LIB.LOG.LogOperator.WriteLog(ex.ToString());
            }
            finally
            {
                if (_wordWnd != IntPtr.Zero)
                {
                    WinApi.SendMessage(_wordWnd, WinApi.WM_CLOSE, 0, 0);
                    _wordWnd = IntPtr.Zero;
                }
                GC.Collect();
            }
        }
        /// <summary>
        /// 当大小发生改变时
        /// </summary>
        public void OnResize(int handleId)
        {
            int borderWidth = SystemInformation.Border3DSize.Width;
            int borderHeight = SystemInformation.Border3DSize.Height;
            int captionHeight = SystemInformation.CaptionHeight;
            int statusHeight = 8;
            //int statusHeight = SystemInformation.ToolWindowCaptionHeight;
            WinApi.MoveWindow(
                _wordWnd,
                -2 * borderWidth,
                -2 * borderHeight - captionHeight,
                Form.FromHandle((IntPtr)handleId).Bounds.Width + 4 * borderWidth,
                Form.FromHandle((IntPtr)handleId).Bounds.Height + captionHeight + 4 * borderHeight + statusHeight,
                false);
            //true);
        }
        /// <summary>
        ///文档关闭前
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="bCancel"></param>
        private void wdClass_DocumentBeforeClose(Document doc, ref bool bCancel)
        {
            Save();//确保文档关闭前能保存
            bCancel = true;
        }

    }

}
