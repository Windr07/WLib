/////////////////////////////////////////////
//嵌入office的窗口进行文件预览、编辑等操作
//当前只添加对word和excel的支持
//使用者电脑需要安装对应的office（版本问题未测试）
////////////////////////////////////////////

using System.Windows.Forms;
using WLib.UserCtrls.Viewer;

namespace WLib.UserCtrls.OfficeControl
{
    /// <summary>
    /// 
    /// </summary>
    public partial class OfficeEmbed : UserControl, IViewer
    {
        /// <summary>
        /// 
        /// </summary>
        IOfficeBase _officeBase;
        /// <summary>
        /// 
        /// </summary>
        public OfficeEmbed()
        {
            InitializeComponent();
            //Word();
        }
        /// <summary>
        /// 析构时执行OfficeBase的关闭方法
        /// </summary>
        ~OfficeEmbed()
        {
            if (_officeBase != null)
                _officeBase.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        public bool HasFileLoaded => _officeBase?.HasFileLoaded ?? false;

        /// <summary>
        /// 加载的文件的路径
        /// </summary>
        public string FileName => _officeBase?.FileName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public void LoadFile(string fileName)
        {
            LoadFile(fileName, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="readOnly"></param>
        public void LoadFile(string fileName, bool readOnly)
        {
            Close();
            System.IO.FileInfo file = new System.IO.FileInfo(fileName);
            string extension = file.Extension;
            this.Controls.Clear();

            switch (extension)
            {
                case ".doc":
                case ".docx":
                case ".dot":
                case ".rtf":
                    try
                    {
                        if (_officeBase != null && _officeBase.HasFileLoaded)
                            _officeBase.Close();
                    }
                    catch { }
                    finally
                    {
                        _officeBase = new WordEmbed();
                    }
                    break;
                case ".xls":
                case ".xlsx":
                    try
                    {
                        if (_officeBase != null && _officeBase.HasFileLoaded)
                            _officeBase.Close();
                    }
                    catch { }
                    finally
                    {
                        _officeBase = new ExcelEmbed();
                    }
                    break;
                default:
                    break;
            }

            _officeBase.LoadFile(fileName, this.Handle.ToInt32(), readOnly);
            this.SizeChanged += delegate
            {
                _officeBase.OnResize(this.Handle.ToInt32());
            };
        }
        /// <summary>
        /// 保存文档
        /// </summary>
        public void Save()
        {
            _officeBase.Save();
        }
        /// <summary>
        /// 关闭文档
        /// </summary>
        public void Close()
        {
            if (_officeBase != null)
            {
                try
                {
                    _officeBase.Close();
                }
                catch { }
            }
        }
    }
}
