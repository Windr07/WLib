using System;
using System.Windows.Forms;
using WLib.ArcGis.Analysis.Gp;
using WLib.WinCtrls.Dev.Extension;

namespace WLib.Samples.WinForm.Dev
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                imageCollection_Nav.ExportImageCollectionImages(Environment.CurrentDirectory + @"\Photo\");
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"错误：", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRunIntersect_Click(object sender, EventArgs e)
        {
            try
            {
                var dbPath = @"F:\工作事项\11、耕地质量系列项目\测试数据\440111白云区江高镇雄丰村、五丰村、中八村2014年高标准基本农田建设项目DBSJK.gdb";
                var path1 = dbPath + @"\XMGXDY;";
                var path2 = dbPath + @"\ZDZWZJCGSXB";
                var path3 = dbPath + @"\JZZWZJCGSXB";
                var resultPath = @"";
                new GpHelper(true).RunTool(GpHelper.Intersect(path1, resultPath), out _, out _);
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"错误：", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
