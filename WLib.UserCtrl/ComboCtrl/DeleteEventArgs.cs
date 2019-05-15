/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/29 11:30:45
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.UserCtrls.ComboCtrl
{
    public class DeleteEventArgs : EventArgs
    {
        public int Index { get; }
        public bool IsDelete { get; set; }
        public DeleteEventArgs(int index) => this.Index = index;
    }
}
