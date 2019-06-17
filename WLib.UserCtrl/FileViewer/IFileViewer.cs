/*---------------------------------------------------------------- 
// auth： XiaoJiaMing?
// date： None
// desc： 几种查看文件内容的（嵌入）控件的共同接口
//        共同需要用的功能包括：打开文件、关闭文件、隐藏控件
// mdfy:  Windragon
//----------------------------------------------------------------*/

namespace WLib.WinCtrls.FileViewer
{
    /// <summary>
    /// 文件查看器，即几种查看文件内容的控件的共同接口
    /// </summary>
    public interface IFileViewer
    {
        /// <summary>
        /// 加载文件
        /// </summary>
        /// <param name="file"></param>
        void LoadFile(string file);
        /// <summary>
        /// 关闭文件
        /// </summary>
        void Close();
        /// <summary>
        /// 控件是否可视
        /// </summary>
        bool Visible { get; set; }
    }
}
