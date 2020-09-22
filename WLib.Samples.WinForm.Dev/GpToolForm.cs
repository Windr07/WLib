using System;
using System.Windows.Forms;
using WLib.ArcGis.Analysis.Gp;
using WLib.WinCtrls.Dev.Extension;

namespace WLib.Samples.WinForm.Dev
{
    public partial class GpToolForm : DevExpress.XtraEditors.XtraForm
    {
        public GpToolForm()
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
                var dbPath = @"c:\a1.gdb";
                var path1 = dbPath + @"\a;";
                var path2 = dbPath + @"\b";
                var path3 = dbPath + @"\c";
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
