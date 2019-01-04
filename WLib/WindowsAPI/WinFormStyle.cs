using System;
using System.Runtime.InteropServices;

namespace WLib.WindowsAPI
{
    /// <summary>
    /// 提供改变窗体样式和动画效果的Windows API调用
    /// </summary>
    public class WinFormStyle
    {
        #region 拖动
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        /// <summary>
        /// 向系统发送窗体拖动的消息
        /// </summary>
        /// <param name="hwnd">窗体句柄</param>
        public static void FormPan(IntPtr hwnd)
        {
            ReleaseCapture();
            SendMessage(hwnd, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        #endregion

        #region 登陆窗体动画效果
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        const int AW_HOR_POSITIVE = 0x0001;
        const int AW_HOR_NEGATIVE = 0x0002;
        const int AW_VER_POSITIVE = 0x0004;
        const int AW_VER_NEGATIVE = 0x0008;
        const int AW_CENTER = 0x0010;
        const int AW_HIDE = 0x10000;
        const int AW_ACTIVATE = 0x20000;
        const int AW_SLIDE = 0x40000;
        const int AW_BLEND = 0x80000;


        /// <summary>
        /// 窗体由中心点放大
        /// </summary>
        /// <param name="hwnd"></param>
        public static void WindowZoomIn(IntPtr hwnd)
        {
            AnimateWindow(hwnd, 1000, AW_CENTER | AW_ACTIVATE);
        }
        /// <summary>
        /// 窗体由中心向四周缩小
        /// </summary>
        /// <param name="hwnd"></param>
        public static void WindowZoomOut(IntPtr hwnd)
        {
            AnimateWindow(hwnd, 1000, AW_ACTIVATE | AW_HIDE | AW_CENTER);
        }
        #endregion

        #region 窗体边框阴影效果变量
        public static void ShadowStyle(IntPtr hwnd)
        {
            SetClassLong(hwnd, GCL_STYLE, GetClassLong(hwnd, GCL_STYLE) | CS_DropSHADOW); //API函数加载，实现窗体边框阴影效果
        } 
        const int CS_DropSHADOW = 0x20000;
        const int GCL_STYLE = (-26);
        //声明Win32 API
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);
        #endregion

    }
}
