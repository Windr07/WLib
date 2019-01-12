/*---------------------------------------------------------------- 
// auth： XiaoJiaMing?
// date： None
// desc： 几种查看文件内容的（嵌入）控件的共同接口
//        共同需要用的功能包括：打开文件、关闭文件、隐藏控件
// mdfy:  Windragon
//----------------------------------------------------------------*/

namespace WLib.UserCtrls.Viewer
{
    public interface IViewer
    {
        void LoadFile(string file);

        void Close();

        bool Visible { get; set; }
    }
}
