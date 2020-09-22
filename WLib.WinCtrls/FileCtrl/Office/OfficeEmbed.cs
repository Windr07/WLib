/*---------------------------------------------------------------- 
// auth： Unknown
// date： None
// desc： 嵌入office的窗口进行文件预览、编辑等操作
//        当前只添加对word和excel的支持
//        使用者电脑需要安装对应的office（版本问题未测试）
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System.Windows.Forms;

namespace WLib.WinCtrls.FileCtrl.Office
{
    /// <summary>
    /// Office程序嵌入面板
    /// </summary>
    public partial class OfficeEmbed : UserControl, IFileViewer
    {
        /// <summary>
        /// Office文档的一些基础操作
        /// </summary>
        IOfficeBase _officeBase;
        /// <summary>
        /// 文档是否已加载
        /// </summary>
        public bool HasFileLoaded => _officeBase?.HasFileLoaded ?? false;
        /// <summary>
        /// 加载的文档的路径
        /// </summary>
        public string FileName => _officeBase?.FileName;


        /// <summary>
        /// Office程序嵌入面板
        /// </summary>
        public OfficeEmbed()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 析构时执行OfficeBase的关闭方法
        /// </summary>
        ~OfficeEmbed()
        {
            _officeBase?.Close();
        }


        /// <summary>
        /// 加载文档
        /// </summary>
        /// <param name="fileName"></param>
        public void LoadFile(string fileName)
        {
            LoadFile(fileName, true);
        }
        /// <summary>
        /// 加载文档
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="readOnly"></param>
        public void LoadFile(string fileName, bool readOnly)
        {
            try
            {
                this.Controls.Clear();
                if (_officeBase != null && _officeBase.HasFileLoaded)
                    _officeBase.Close();
                switch (new System.IO.FileInfo(fileName).Extension)
                {
                    case ".doc":
                    case ".docx":
                    case ".dot":
                    case ".rtf":
                        _officeBase = new WordBase();
                        break;
                    case ".xls":
                    case ".xlsx":
                        _officeBase = new ExcelBase();
                        break;
                }
                if (_officeBase != null)
                {
                    _officeBase.LoadFile(fileName, this.Handle.ToInt32(), readOnly);
                    this.SizeChanged += (sender2, e2) => _officeBase.OnResize(this.Handle.ToInt32());
                }
            }
            catch { }
        }
        /// <summary>
        /// 保存文档
        /// </summary>
        public void Save()
        {
            _officeBase?.Save();
        }
        /// <summary>
        /// 关闭文档
        /// </summary>
        public void Close()
        {
            _officeBase?.Close();
        }
    }
}
