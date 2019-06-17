/*---------------------------------------------------------------- 
// auth： Unknown
// date： None
// desc： None
// mdfy:  Windragon
//----------------------------------------------------------------*/

namespace WLib.WinCtrls.FileViewer.Office
{
    /// <summary>
    /// Office文档的一些基础操作
    /// </summary>
    public interface IOfficeBase
    {
        /// <summary>
        /// 文档是否已加载
        /// </summary>
        bool HasFileLoaded { get; }
        /// <summary>
        /// 文档路径
        /// </summary>
        string FileName { get; }
        /// <summary>
        /// 加载文档
        /// </summary>
        void LoadFile(string fileName, int handleId, bool readOnly = true);
        /// <summary>
        /// 重置界面大小
        /// </summary>
        void OnResize(int handleId);
        /// <summary>
        /// 关闭文档
        /// </summary>
        void Close();
        /// <summary>
        /// 保存文档
        /// </summary>
        void Save();
    }
}
