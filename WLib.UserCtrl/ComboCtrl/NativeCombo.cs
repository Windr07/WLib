/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/29 11:32:55
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Windows.Forms;

namespace WLib.WinCtrls.ComboCtrl
{
    internal class NativeCombo : NativeWindow
    {
        public event MouseEventHandler MouseDown;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x201) //WM_LBUTTONDOWN = 0x201
            {
                int x = m.LParam.ToInt32() & 0xffff;
                int y = m.LParam.ToInt32() >> 16;
                MouseDown?.Invoke(null, new MouseEventArgs(MouseButtons.Left, 1, x, y, 0));
            }
            base.WndProc(ref m);
        }
    }
}
