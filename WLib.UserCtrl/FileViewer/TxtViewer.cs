/*---------------------------------------------------------------- 
// auth： XiaoJiaMing?
// date： None
// desc： None
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System.IO;
using System.Windows.Forms;

namespace WLib.WinCtrls.FileViewer
{
    /// <summary>
    /// 读取文本的控件
    /// </summary>
    public partial class TxtViewer : UserControl, IFileViewer
    {
        /// <summary>
        /// 读取文本的控件
        /// </summary>
        public TxtViewer()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 加载文件
        /// </summary>
        /// <param name="filePath"></param>
        public void LoadFile(string filePath)
        {
            richTextBox1.Text = File.ReadAllText(filePath);
        }
        /// <summary>
        /// 关闭文件
        /// </summary>
        public void Close()
        {
            richTextBox1.Clear();
        }
    }
}
