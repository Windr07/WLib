/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using System.Windows.Forms;

namespace WLib.WinCtrls.Extension
{
    /// <summary>
    /// 提供弹出打印设置、打印预览窗口、设置纸张类型等方法
    /// </summary>
    public static class PrintOpt
    {
        /// <summary>
        /// 设置纸张类型
        /// </summary>
        /// <param name="printDocument1">打印文档</param>
        /// <param name="paperName">纸张类型的名称，例如"A3"</param>
        public static void SetPaperSize(this PrintDocument printDocument1, string paperName)
        {
            //设置纸张大小
            PaperSize pageSize = null;
            foreach (PaperSize tmpPageSize in printDocument1.PrinterSettings.PaperSizes)
            {
                if (tmpPageSize.PaperName.Equals(paperName))
                {
                    pageSize = tmpPageSize;
                    break;
                }
            }
            printDocument1.DefaultPageSettings.PaperSize = pageSize;
        }
        /// <summary>
        /// 显示打印预览窗体（ShowDialog），且打印预览窗体可用鼠标滚轮操纵
        /// </summary>
        /// <param name="printPreviewDlg"></param>
        /// <param name="printDocument1"></param>
        /// <param name="zoom">缩放比例</param>
        /// <param name="width">窗体宽度</param>
        /// <param name="height">窗体高度</param>
        public static void ShowDialogEx(this PrintPreviewDialog printPreviewDlg, PrintDocument printDocument1, double zoom = 1.0, int width = 800, int height = 560)
        {
            if (printPreviewDlg == null)
                printPreviewDlg = new PrintPreviewDialog();

            printPreviewDlg.PrintPreviewControl.MouseWheel -= PrintPreviewControl_MouseWheel;
            printPreviewDlg.PrintPreviewControl.MouseWheel += PrintPreviewControl_MouseWheel;

            if (_position == null || _setPositionMethod == null)
            {
                Type type = typeof(PrintPreviewControl);
                _position = type.GetField("position", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.ExactBinding);
                _setPositionMethod = type.GetMethod("SetPositionNoInvalidate", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.ExactBinding);
            }

            printPreviewDlg.Document = printDocument1;
            printPreviewDlg.PrintPreviewControl.Zoom = zoom;
            printPreviewDlg.Width = width;
            printPreviewDlg.Height = height;
            printPreviewDlg.ShowDialog();
        }
        /// <summary>
        /// 打印预览中，设置可以通过鼠标滚轮操纵
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PrintPreviewControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!SystemInformation.MouseWheelPresent)
                return;

            var printPreviewControl = sender as PrintPreviewControl;

            float amount = Math.Abs(e.Delta) / SystemInformation.MouseWheelScrollDelta;
            amount *= SystemInformation.MouseWheelScrollLines;
            amount *= 12;//Row height  
            amount *= (float)printPreviewControl.Zoom;//Zoom Rate  

            int scrollAmount = e.Delta < 0 ? (int)amount : -(int)amount;

            Point curPos = (Point)(_position.GetValue(printPreviewControl));
            _setPositionMethod.Invoke(printPreviewControl, new object[] { new Point(curPos.X + 0, curPos.Y + scrollAmount) });
        }
        /// <summary>
        /// 打印预览窗口中，滚动条位置
        /// </summary>
        private static FieldInfo _position;
        /// <summary>
        /// 打印预览窗口中，设置滚动栏位置的方法
        /// </summary>
        private static MethodInfo _setPositionMethod;
    }
}
