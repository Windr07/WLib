using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WLib.ArcGis.GeoDatabase.Fields;
using WLib.Data;
using WLib.WinCtrls.GridViewCtrl;

namespace WLib.WinCtrls.ArcGisCtrl
{
    /// <summary>
    /// 查看图层字段信息的窗口
    /// </summary>
    public partial class FieldInfoForm : Form
    {
        /// <summary>
        /// 查看图层字段信息的窗口
        /// </summary>
        public FieldInfoForm()
        {
            InitializeComponent();
        }

        private void WorkspaceSelector1_AfterSelectPath(object sender, EventArgs e)
        {
            this.listBoxLayers.Items.Clear();
            if (this.workspaceSelector1.FeatureClassNames != null)
            {
                this.listBoxLayers.Items.AddRange(this.workspaceSelector1.FeatureClassNames);
                if (this.listBoxLayers.Items.Count > 0)
                    this.listBoxLayers.SelectedIndex = 0;
            }
        }

        private void btnGetFieldInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listBoxLayers.SelectedIndex < 0)
                {
                    MessageBox.Show("请先选择数据源和图层！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var dataTable = this.dataGridView1.DataSource as DataTable;
                var txtPath = AppDomain.CurrentDomain.BaseDirectory + "fields.txt";
                var tableToText = dataTable.DataTableToText("\t", true);
                File.WriteAllText(txtPath, tableToText, Encoding.Default);
                Process.Start(txtPath);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ListBoxLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.listBoxLayers.SelectedIndex < 0)
                    return;
                var featureClass = this.workspaceSelector1.FeatureClasses[this.listBoxLayers.SelectedIndex];
                var fieldItems = featureClass.Fields.ToFieldItemExs();
                var dataTable = fieldItems.ConvertToDataTable();
                this.dataGridView1.DataSource = null;
                this.dataGridView1.DataSource = dataTable;
                this.dataGridView1.ShowRowNumber();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
