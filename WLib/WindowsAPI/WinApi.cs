/*---------------------------------------------------------------- 
// auth： Unknown
// date： None
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Runtime.InteropServices;

namespace WLib.WindowsAPI
{
    //API函数参考手册：http://www.office-cn.net/t/api/api_content.htm
    //                 http://www.baike.com/wiki/API%E5%87%BD%E6%95%B0?prd=fenleishequ_jiaodiantuijian_zuocitiao
    //微软API官网：    https://msdn.microsoft.com/library
    //Windows API官网：https://msdn.microsoft.com/zh-cn/library/windows/desktop/ff818516(v=vs.85).aspx

    /// <summary>
    /// 提供Windows API调用
    /// <para>参考：http://www.office-cn.net/t/api/api_content.htm </para>
    /// </summary>
    public class WinApi
    {
        /// <summary>
        /// 查找拥有指定窗口类名或窗口名的最顶层窗口，返回窗口句柄
        /// </summary>
        /// <param name="lpClassName">窗口类名</param>
        /// <param name="lpWindowName">窗口标题</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 查找拥有指定窗口类名或窗口名的第一个子窗口/控件，返回窗口/控件句柄
        /// </summary>
        /// <param name="hwndParent">父窗口句柄，查找此窗口的所有子窗口（如果hwndParent为0，则以桌面为父窗口，查找桌面窗口的所有子窗口）</param>
        /// <param name="hwndChildAfter">子窗口句柄，按照z-index的顺序从hwndChildAfter向后开始搜索子窗体</param>
        /// <param name="lpszClass">窗口/控件类名</param>
        /// <param name="lpszWindow">窗口/控件标题</param>
        /// <returns>控件句柄</returns>
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        /// <summary>
        /// 结束进程的方法
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpdwProcessId"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        /// <summary>
        /// 设置某一窗口的父窗口
        /// </summary>
        /// <param name="hWndChild"></param>
        /// <param name="hWndNewParent"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(int hWndChild, int hWndNewParent);

        /// <summary>
        /// 设置窗口的位置，尺寸
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hWndInstanceAfter"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInstanceAfter,
            int x, int y, int cx, int cy, SetWindowPosFlags uFlags);

        /// <summary>
        /// 改变指定窗口的位置和大小
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <param name="bRePaint"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y,
            int nWidth, int nHeight, bool bRePaint);

        /// <summary>
        /// 该函数重画指定菜单的菜单条。如果系统创建窗口以后菜单条被修改，则必须调用此函数来画修改了的菜单条。
        /// </summary>
        /// <param name="hMenu"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern Int32 DrawMenuBar(IntPtr hMenu);

        /// <summary>
        /// 该函数允许应用程序为复制或修改而访问窗口菜单（系统菜单或控制菜单）。
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="bRevert"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern Int32 GetSystemMenu(IntPtr hWnd, bool bRevert);

        /// <summary>
        /// 该函数从指定菜单删除一个菜单项或分离一个子菜单
        /// </summary>
        /// <param name="hMenu"></param>
        /// <param name="nPosition"></param>
        /// <param name="wFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern Int32 RemoveMenu(Int32 hMenu, Int32 nPosition, Int32 wFlags);

        /// <summary>
        /// 返回菜单中菜单项的数量
        /// </summary>
        /// <param name="hMenu"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetMenuItemCount")]
        public static extern Int32 GetMenuItemCount(Int32 hMenu);

        /// <summary>
        /// 将指定的消息发送到一个或多个窗口。此函数直到窗口程序处理完消息再返回
        /// </summary>
        /// <param name="hWnd">窗口的句柄</param>
        /// <param name="wMsg">发送的消息</param>
        /// <param name="wParam">附加的消息特定信息</param>
        /// <param name="lParam">附加的消息特定信息</param>   
        /// <returns>：返回值指定消息处理的结果，依赖于所发送的消息</returns>
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, uint wParam, uint lParam);

        /// <summary>
        /// 将鼠标捕获设置到指定的窗口 
        /// Sets the mouse capture to the specified window belonging to the current thread.
        /// SetCapture captures mouse input either when the mouse is over the capturing window, 
        /// or when the mouse button was pressed while the mouse was over the capturing window and the button is still down. 
        /// Only one window at a time can capture the mouse.If the mouse cursor is over a window created by another thread, 
        /// the system will direct mouse input to the specified window only if a mouse button is down.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern Int32 SetCapture(IntPtr hWnd);

        /// <summary>
        /// 将创建指定窗口的线程设置到前台，并且激活该窗口
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// 确定指定进程是否运行在64位操作系统的32环境（Wow64模拟器）下。
        /// </summary>
        /// <param name="hProcess">进程句柄</param>
        /// <param name="lpSystemInfo">判断结果</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool lpSystemInfo);


        public const int WM_CLOSE = 0x0010;

        public const int MF_BYPOSITION = 0x400;
        public const int MF_REMOVE = 0x1000;
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002; //模拟鼠标左键按下 
        public const int MOUSEEVENTF_LEFTUP = 0x0004; //模拟鼠标左键抬起

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bvk, byte bScan, uint dwFlags, uint dwExtraInfo);
        public const int KEYEVENT_UP = 0x02;
        public const int KEYEVENT_DOWN = 0x0;

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public const int WM_KEYDOWN = 0X100;
        public const int WM_KEYUP = 0X101;
    }
    [Flags]
    public enum SetWindowPosFlags : uint
    {
        // ReSharper disable InconsistentNaming
        /// <summary>
        ///     If the calling thread and the thread that owns the window are attached targetFileName different input queues, the system posts the request targetFileName the thread that owns the window. This prevents the calling thread sourcesFileName blocking its execution while other threads process the request.
        /// </summary>
        SWP_ASYNCWINDOWPOS = 0x4000,
        /// <summary>
        ///     Prevents generation of the WM_SYNCPAINT message.
        /// </summary>
        SWP_DEFERERASE = 0x2000,
        /// <summary>
        ///     Draws a frame (defined in the window's class description) around the window.
        /// </summary>
        SWP_DRAWFRAME = 0x0020,
        /// <summary>
        ///     Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
        /// </summary>
        SWP_FRAMECHANGED = 0x0020,
        /// <summary>
        ///     Hides the window.
        /// </summary>
        SWP_HIDEWINDOW = 0x0080,
        /// <summary>
        ///     Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
        /// </summary>
        SWP_NOACTIVATE = 0x0010,

        /// <summary>
        ///     Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
        /// </summary>
        SWP_NOCOPYBITS = 0x0100,
        /// <summary>
        ///     Retains the current position (ignores X and Y parameters).
        /// </summary>
        SWP_NOMOVE = 0x0002,
        /// <summary>
        ///     Does not change the owner window's position in the Z order.
        /// </summary>
        SWP_NOOWNERZORDER = 0x0200,
        /// <summary>
        ///     Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
        /// </summary>
        SWP_NOREDRAW = 0x0008,
        /// <summary>
        ///     Same as the SWP_NOOWNERZORDER flag.
        /// </summary>
        SWP_NOREPOSITION = 0x0200,
        /// <summary>
        ///     Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
        /// </summary>
        SWP_NOSENDCHANGING = 0x0400,
        /// <summary>
        ///     Retains the current size (ignores the cx and cy parameters).
        /// </summary>
        SWP_NOSIZE = 0x0001,
        /// <summary>
        ///     Retains the current Z order (ignores the hWndInsertAfter parameter).
        /// </summary>
        SWP_NOZORDER = 0x0004,
        /// <summary>
        ///     Displays the window.
        /// </summary>
        SWP_SHOWWINDOW = 0x0040,
        // ReSharper restore InconsistentNaming
    }

}

