using System.IO;
using System.Windows.Forms;

namespace WLib.UserCtrls.Viewer
{
    public partial class TxtViewer : UserControl, IViewer
    {
        public TxtViewer()
        {
            InitializeComponent();
        }

        public void LoadFile(string file)
        {
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(file);
                richTextBox1.Text = sr.ReadToEnd();
                sr.Close();
            }
            catch { }
            finally
            {
                sr?.Close();
            }
        }

        public void Close()
        {
        }
    }
}
