/*---------------------------------------------------------------- 
// auth： Unknown
// date： None
// desc： None
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using WLib.WindowsAPI;

namespace WLib.WinCtrls.FileViewer.Office
{
    /// <summary>
    /// Word文档基础操作
    /// </summary>
    public class WordBase : IOfficeBase
    {
        /// <summary>
        /// Word文档
        /// </summary>
        private Document _document;
        /// <summary>
        /// Word应用程序
        /// </summary>
        private ApplicationClass _wordAppClass;
        /// <summary>
        /// Word程序顶层窗口句柄
        /// </summary>
        private IntPtr _wordWnd = IntPtr.Zero;
        /// <summary>
        /// 当前Word程序面板是否已了加载Word文档
        /// </summary>
        public bool HasFileLoaded => _document != null;
        /// <summary>
        /// 当前加载的Word文档路径
        /// </summary>
        public string FileName => _document?.FullName;


        /// <summary>
        /// 加载word文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// /// <param name="handleId">父窗口句柄ID</param>
        /// <param name="readOnly"></param>
        public void LoadFile(string fileName, int handleId, bool readOnly = true)
        {
            try
            {
                if (_wordAppClass == null)
                    _wordAppClass = new ApplicationClass();

                //wdClass.CommandBars.AdaptiveMenus = false;
                _wordAppClass.DocumentBeforeClose += wdClass_DocumentBeforeClose;
                //wdClass.docum

                object _nothing = System.Reflection.Missing.Value;
                if (_document != null)
                {
                    _wordAppClass.Documents.Close(ref _nothing, ref _nothing, ref _nothing);
                }
                //加载文档
                if (_wordWnd == IntPtr.Zero)
                {
                    _wordWnd = WinApi.FindWindowEx(IntPtr.Zero, IntPtr.Zero, null, _wordAppClass.Caption);
                    WinApi.SetParent(_wordWnd.ToInt32(), handleId);

                    object filename = fileName;
                    object oReadOnly = false;
                    object isVisible = true;
                    if (_wordAppClass?.Documents != null)
                    {
                        _document = _wordAppClass.Documents.Open(ref filename, ref _nothing,
                            ref oReadOnly, ref _nothing, ref _nothing,
                            ref _nothing, ref _nothing, ref _nothing,
                            ref _nothing, ref _nothing, ref _nothing,
                            ref isVisible, ref _nothing, ref _nothing,
                            ref _nothing, ref _nothing);
                    }
                    if (_document != null)
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
                if (_wordAppClass != null)
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

                    _wordAppClass.ActiveWindow.ActivePane.View.Type = WdViewType.wdPrintView;
                    //wdClass.ActiveWindow.EnvelopeVisible = true;

                    _document.Activate();
                    _wordAppClass.Visible = true;
                    _wordAppClass.ActiveWindow.Activate();
                    //wdClass.Activate();
                    //wdClass.MouseAvailable = true;
                    WinApi.SetWindowPos(_wordWnd, handleId, 0, 0,
                        Control.FromHandle((IntPtr)handleId).Bounds.Width,
                        Control.FromHandle((IntPtr)handleId).Bounds.Height,
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
                _wordAppClass.Quit(saveChanges, Type.Missing, Type.Missing);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(_wordAppClass);
                GC.Collect();
                int lpdwProcessId;
                WinApi.GetWindowThreadProcessId(_wordWnd, out lpdwProcessId);
                System.Diagnostics.Process.GetProcessById(lpdwProcessId).Kill();
                _wordAppClass = null;
            }
            catch (Exception ex)
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
                Control.FromHandle((IntPtr)handleId).Bounds.Width + 4 * borderWidth,
                Control.FromHandle((IntPtr)handleId).Bounds.Height + captionHeight + 4 * borderHeight + statusHeight,
                false);
            //true);
        }
        /// <summary>
        /// 设置word上面工具栏
        /// </summary>
        private void SetCommandBars()
        {
            if (_wordAppClass != null)
            {
                int count = _wordAppClass.CommandBars.Count;
                for (int j = 1; j <= count; j++)
                {
                    string menuName = _wordAppClass.CommandBars[j].Name;
                    if (menuName == "Standard")
                    {
                        int count_control = _wordAppClass.ActiveWindow.Application.CommandBars[j].Controls.Count;
                        for (int k = 1; k <= 2; k++)
                        {
                            _wordAppClass.ActiveWindow.Application.CommandBars[j].Controls[k].Enabled = false;
                        }
                    }
                    if (menuName == "Menu Bar")
                    {
                        _wordAppClass.ActiveWindow.Application.CommandBars[j].Enabled = false;
                    }
                    menuName = "";
                }
                //移除按钮
                int hMenu = WinApi.GetSystemMenu(_wordWnd, false);
                if (hMenu > 0)
                {
                    int menuItemCount = WinApi.GetMenuItemCount(hMenu);
                    for (int m = 1; m <= 8; m++)
                    {
                        WinApi.RemoveMenu(hMenu, menuItemCount - m, WinApi.MF_REMOVE | WinApi.MF_BYPOSITION);
                    }
                    WinApi.DrawMenuBar(_wordWnd);
                }
            }

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
