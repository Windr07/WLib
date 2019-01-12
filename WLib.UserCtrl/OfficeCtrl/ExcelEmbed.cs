/*---------------------------------------------------------------- 
// auth： Unknown
// date： None
// desc： None
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using WLib.WindowsAPI;

namespace WLib.UserCtrls.OfficeCtrl
{
    public class ExcelEmbed : IOfficeBase
    {
        private int _handleId = 0;
        private ApplicationClass _exClass;
        private Workbook _wkBook;
        private IntPtr _exWnd = IntPtr.Zero;
        public bool HasFileLoaded => _wkBook != null;
        public string FileName => _wkBook?.FullName;

        public ExcelEmbed()
        {

        }
        /// <summary>
        /// 加载Excel文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="handleId">父窗口句柄ID</param>
        /// <param name="readOnly"></param>
        public void LoadFile(string fileName, int handleId, bool readOnly = true)
        {
            try
            {
                if (_exClass == null)
                {
                    _exClass = new ApplicationClass();
                }
                _handleId = handleId;
                //exClass.CommandBars.AdaptiveMenus = false;
                _exClass.WorkbookBeforeClose += new AppEvents_WorkbookBeforeCloseEventHandler(exClass_WorkbookBeforeClose);
                if (_wkBook != null)
                {
                    _exClass.Workbooks.Close();
                }
                ///加载文档
                if (_exWnd == IntPtr.Zero)
                {
                    _exWnd = (IntPtr)_exClass.Application.Hwnd;
                    //exWnd = WinAPI.FindWindow("XLMAIN", null);
                    WinApi.SetParent(_exWnd.ToInt32(), handleId);
                    //exClass.Workbooks.Add(true);
                    object missingValue = System.Reflection.Missing.Value;
                    #region
                    //
                    // 摘要:
                    //     Opens a workbook.
                    //
                    // 参数:
                    //   AddToMru:
                    //     Optional Object. True to add this workbook to the list of recently used files.
                    //     The default value is False.
                    //
                    //   IgnoreReadOnlyRecommended:
                    //     Optional Object. True to have Microsoft Excel not display the read-only recommended
                    //     message (if the workbook was saved with the Read-Only Recommended option).
                    //
                    //   Format:
                    //     Optional Object. If Microsoft Excel is opening a text file, this argument
                    //     specifies the delimiter character, as shown in the following table. If this
                    //     argument is omitted, the current delimiter is used.
                    //
                    //   ReadOnly:
                    //     Optional Object. True to open the workbook in read-only mode.
                    //
                    //   Password:
                    //     Optional Object. A string that contains the password required to open a protected
                    //     workbook. If this argument is omitted and the workbook requires a password,
                    //     the user is prompted for the password.
                    //
                    //   Editable:
                    //     Optional Object. If the file is a Microsoft Excel 4.0 add-in, this argument
                    //     is True to open the add-in so that it’s a visible window. If this argument
                    //     is False or omitted, the add-in is opened as hidden, and it cannot be unhidden.
                    //     This option doesn't apply to add-ins created in Microsoft Excel 5.0 or later.
                    //     If the file is an Excel template, use True to open the specified template
                    //     for editing or False to open a new workbook based on the specified template.
                    //     The default value is False.
                    //
                    //   CorruptLoad:
                    //     Optional Object. Can be one of the following constants: xlNormalLoad, xlRepairFile,
                    //     and xlExtractData. The default behavior if no value is specified is usually
                    //     normal, but may be safe load or data recovery if Excel has already attempted
                    //     to open the file. The first attempt is normal. If Excel stops operating while
                    //     opening the file, the second attempt is safe load. If Excel again stops operating,
                    //     the next attempt is data recovery.
                    //
                    //   UpdateLinks:
                    //     Optional Object. Specifies the way links in the file are updated. If this
                    //     argument is omitted, the user is prompted to specify how links will be updated.
                    //     Otherwise, this argument is one of the values listed in the following table.If
                    //     Microsoft Excel is opening a file in the WKS, WK1, or WK3 format and the
                    //     UpdateLinks argument is 2, Microsoft Excel generates charts from the graphs
                    //     attached to the file. If the argument is 0, no charts are created.
                    //
                    //   Filename:
                    //     Required String. The file name of the workbook to be opened.
                    //
                    //   Origin:
                    //     Optional Object. If the file is a text file, this argument indicates where
                    //     it originated (so that code pages and Carriage Return/Line Feed (CR/LF) can
                    //     be mapped correctly). Can be one of the following Microsoft.Office.Interop.Excel.XlPlatform
                    //     constants: xlMacintosh, xlWindows, or xlMSDOS. If this argument is omitted,
                    //     the current operating system is used.
                    //
                    //   Converter:
                    //     Optional Object. The index of the first file converter to try when opening
                    //     the file. The specified file converter is tried first; if this converter
                    //     doesn’t recognize the file, all other converters are tried. The converter
                    //     index consists of the row numbers of the converters returned by the Microsoft.Office.Interop.Excel._Application.this[System.Object,System.Object]
                    //     property.
                    //
                    //   WriteResPassword:
                    //     Optional Object. A string that contains the password required to write to
                    //     a write-reserved workbook. If this argument is omitted and the workbook requires
                    //     a password, the user will be prompted for the password.
                    //
                    //   Notify:
                    //     Optional Object. If the file cannot be opened in read/write mode, this argument
                    //     is True to add the file to the file notification list. Microsoft Excel will
                    //     open the file as read-only, poll the file notification list, and then notify
                    //     the user when the file becomes available. If this argument is False or omitted,
                    //     no notification is requested, and any attempts to open an unavailable file
                    //     will fail.
                    //
                    //   Delimiter:
                    //     Optional Object. If the file is a text file and the Format argument is 6,
                    //     this argument is a string that specifies the character to be used as the
                    //     delimiter. For example, use Chr(9) for tabs, use "," for commas, use ";"
                    //     for semicolons, or use a custom character. Only the first character of the
                    //     string is used.
                    //
                    //   Local:
                    //     Optional Object. True saves files against the language of Microsoft Excel
                    //     (including control panel settings). False (default) saves files against the
                    //     language of Visual Basic for Applications (VBA) (which is typically U.S.
                    //     English unless the VBA project where Workbooks.Open is run from is an old
                    //     internationalized XL5/95 VBA project).
                    #endregion
                    object filename = fileName;
                    object updateLinks = true;
                    object format = missingValue;
                    object password = missingValue;
                    object writeResPassword = missingValue;
                    object ignoreReadOnlyRecommended = true;
                    object origin = missingValue;
                    object delimiter = missingValue;
                    object editable = true;
                    object notify = missingValue;
                    object converter = missingValue;
                    object addToMru = missingValue;
                    object local = missingValue;
                    object corruptLoad = missingValue;
                    //object newTemplate = false;
                    //object docType = 0;
                    //object isVisible = true;
                    if (_exClass != null && _exClass.Workbooks != null)
                    {
                        _wkBook = _exClass.Workbooks.Open(fileName,
                            updateLinks,
                            readOnly,
                            format,
                            password,
                            writeResPassword,
                            ignoreReadOnlyRecommended,
                            origin,
                            delimiter,
                            editable,
                            notify,
                            converter,
                            addToMru,
                            local,
                            corruptLoad);

                        _wkBook.BeforeSave += new WorkbookEvents_BeforeSaveEventHandler(wkBook_BeforeSave);
                        _wkBook.SheetChange += new WorkbookEvents_SheetChangeEventHandler(wkBook_SheetChange);
                        _wkBook.BeforeClose += new WorkbookEvents_BeforeCloseEventHandler(wkBook_BeforeClose);
                    }
                    if (_wkBook == null)
                    {
                        //throw new Exception("Word文档加载失败！");
                    }
                    else
                    {
                        if (readOnly)
                            _wkBook.Protect();
                    }
                }
                SetExcelStyle(handleId);
                //wkBook.SendForReview();
                //wkBook.SendForReview();
                // wkBook.SheetBeforeDoubleClick+=new WorkbookEvents_SheetBeforeDoubleClickEventHandler(wkBook_SheetBeforeDoubleClick);
                //wkBook.EndReview();
                //wkBook.Activate();
                // wkBook.CanCheckIn();
                // wkBook.CheckIn();
                //wkBook.ExclusiveAccess();
                //wkBook.ForwardMailer();
                // wkBook.NewWindow();
                //wkBook.Route();
                // wkBook.Worksheets.Application.ActivateMicrosoftApp();
                //SetCommandBars();                
                //((Worksheet)exClass.Workbooks[1].Worksheets.get_Item(1)).get_Range("A1", "B2").Value = "";
                // WinAPI.SendMessage(exWnd, WM_KEYDOWN, 65, 0);                
                // WinAPI.SendMessage(exWnd, WM_KEYUP, 65, 0);
                //WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                //System.Threading.Thread.Sleep(100);
                // WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                //IntPtr sheetWnd = WinAPI.FindWindow("XLDESK", null);//"XLDESK"
                //WinAPI.SendMessage(sheetWnd, WM_KEYDOWN, 65, 0);
                //WinAPI.SendMessage(sheetWnd, WM_KEYUP, 65, 0);
            }
            catch (Exception ex)
            {
                if (_exWnd != IntPtr.Zero)
                {
                    WinApi.SendMessage(_exWnd, WinApi.WM_CLOSE, 0, 0);
                    _exWnd = IntPtr.Zero;
                }
                ////COM.LIB.LOG.LogOperator.WriteLog(ex.ToString());
            }

        }
        /// <summary>
        /// 设置打开Excel文档的样式 
        /// </summary>
        /// <param name="handleId"></param>
        private void SetExcelStyle(int handleId)
        {
            if (_exClass != null)
            {
                //exClass.ActiveWindow.DisplayHorizontalScrollBar = false;
                //exClass.ActiveWindow.DisplayFormulas = false;
                //exClass.ActiveWindow.DisplayGridlines = false;
                //exClass.ActiveWindow.DisplayHeadings = false;
                //exClass.ActiveWindow.DisplayOutline = false;
                //exClass.ActiveWindow.DisplayRightToLeft = false;
                //exClass.ActiveWindow.DisplayZeros = false;
                //exClass.ActiveWindow.DisplayVerticalScrollBar = false;     
                // exClass.ActiveWorkbook.AutoUpdateSaveChanges = true;
                //exClass.AutoCorrect =
                //exClass.AutoFormatAsYouTypeReplaceHyperlinks = true;
                // exClass.AskToUpdateLinks = true;
                //exClass.CalculationInterruptKey = XlCalculationInterruptKey.xlAnyKey;
                //exClass.ActiveWindow.View = XlWindowView.xlNormalView;
                // exClass.Workbooks[0].Activate();

                _exClass.Visible = true;
                //wkBook.IsAddin = false;
                //wkBook.HasRoutingSlip = true;
                //wkBook.AutoUpdateFrequency = 1;
                //wkBook.Activate();
                _wkBook.InactiveListBorderVisible = false;
                WinApi.SetWindowPos(_exWnd, handleId, 1, 1,
                    Form.FromHandle((IntPtr)handleId).Bounds.Width,
                    Form.FromHandle((IntPtr)handleId).Bounds.Height,
                    SetWindowPosFlags.SWP_NOZORDER |
                    SetWindowPosFlags.SWP_NOMOVE |
                    SetWindowPosFlags.SWP_DRAWFRAME |
                    SetWindowPosFlags.SWP_NOSIZE);

                OnResize(handleId);
            }
        }
        ///// <summary>
        ///// 设置word上面工具栏
        ///// </summary>
        //private void SetCommandBars()
        //{
        //    if (exClass != null)
        //    {
        //        int count = exClass.CommandBars.Count;
        //        for (int j = 1; j <= count; j++)
        //        {
        //            string menuName = exClass.CommandBars[j].Name;
        //            if (menuName == "Standard")
        //            {
        //                int count_control = exClass.ActiveWindow.Application.CommandBars[j].Controls.Count;
        //                for (int k = 1; k <= 2; k++)
        //                {
        //                    exClass.ActiveWindow.Application.CommandBars[j].Controls[k].Enabled = false;
        //                }
        //            }
        //            if (menuName == "Menu Bar")
        //            {
        //                exClass.ActiveWindow.Application.CommandBars[j].Enabled = false;
        //            }
        //            menuName = "";
        //        }
        //        ///移除按钮
        //        int hMenu = WinAPI.GetSystemMenu(exWnd, false);
        //        if (hMenu > 0)
        //        {
        //            int menuItemCount = WinAPI.GetMenuItemCount(hMenu);
        //            for (int m = 1; m <= 8; m++)
        //            {
        //                WinAPI.RemoveMenu(hMenu, menuItemCount - m, WinAPI.MF_REMOVE | WinAPI.MF_BYPOSITION);
        //            }
        //            WinAPI.DrawMenuBar(exWnd);
        //        }
        //    }
        //}

        /// <summary>
        /// 保存打开的文件
        /// </summary>
        public void Save()
        {
            //if (wkBook != null)
            //{
            //    wkBook.Save();
            //}
        }
        /// <summary>
        /// 关闭打开的文件
        /// </summary>
        public void Close()
        {
            //Save();
            //exClass.Workbooks.Close();
            try
            {
                //if (wkBook != null)
                //{
                //    wkBook.Application.EnableEvents = false;
                //    wkBook.Saved = true;
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(wkBook);
                //}
                if (_exClass != null)
                {
                    //wkBook.Application.EnableEvents = false;
                    //wkBook.Saved = true;
                    _exClass.EnableEvents = false;
                    System.Collections.IEnumerator wks = _exClass.Workbooks.GetEnumerator();
                    wks.Reset();
                    while (wks.MoveNext())
                    {
                        (wks.Current as WorkbookClass).Saved = true;
                    }
                    _exClass.Workbooks.Close();
                    _exClass.Quit();
                    GC.Collect();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(_wkBook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(_exClass);
                    int lpdwProcessId;
                    WinApi.GetWindowThreadProcessId(new IntPtr(_exClass.Hwnd), out lpdwProcessId);
                    System.Diagnostics.Process.GetProcessById(lpdwProcessId).Kill();
                    _exClass = null;
                }
            }
            catch (System.Exception ex)
            {
                ////COM.LIB.LOG.LogOperator.WriteLog(ex.ToString());
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                if (_exWnd != IntPtr.Zero)
                {
                    WinApi.SendMessage(_exWnd, WinApi.WM_CLOSE, 0, 0);
                    _exWnd = IntPtr.Zero;
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
            int statusHeight = SystemInformation.ToolWindowCaptionHeight;
            WinApi.MoveWindow(
                _exWnd,
                -2 * borderWidth,
                -2 * borderHeight - captionHeight,
                Form.FromHandle((IntPtr)handleId).Bounds.Width + 4 * borderWidth,
                Form.FromHandle((IntPtr)handleId).Bounds.Height + captionHeight + 4 * borderHeight + statusHeight,
                true);
        }
        /// <summary>
        ///文档关闭前
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="bCancel"></param>
        private void exClass_wkBookBeforeClose(Workbook doc, ref bool bCancel)
        {
            Save();//确保文档关闭前能保存
            bCancel = true;
        }


        #region 事件
        private void exClass_WorkbookBeforeClose(Workbook wkBook, ref bool bCancel)
        {
            wkBook.Saved = true;
        }

        void wkBook_BeforeClose(ref bool cancel)
        {
            _wkBook.Saved = true;
        }

        void wkBook_SheetChange(object sh, Range target)
        {
            MessageBox.Show("禁止修改文档");
            _wkBook.Application.EnableEvents = false;
            _wkBook.Application.Undo();
            _wkBook.Application.EnableEvents = true;
        }

        void wkBook_BeforeSave(bool saveAsUi, ref bool cancel)
        {
            if (!saveAsUi)
            {
                MessageBox.Show("禁止覆盖原文件");
                cancel = true;
            }
        }
        #endregion
    }

}
