using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WLib.ArcGis.GeoDatabase.Fields;
using WLib.ArcGis.GeoDatabase.WorkSpace;
using WLib.Data;

namespace WLib.WinCtrls.Dev.ArcGisCtrl
{
    /// <summary>
    /// 查看图层字段信息的窗口
    /// </summary>
    public partial class FieldInfoForm : XtraForm
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
            if (this.workspaceSelector1.Workspace != null)
            {
                var names = this.workspaceSelector1.Workspace.GetFeatureClassNames().ToArray();
                this.listBoxLayers.Items.AddRange(names);
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
                var txtPath = AppDomain.CurrentDomain.BaseDirectory + "fields.txt";
                var dataTable = this.gridControl1.DataSource as DataTable;
                var tableToText = dataTable.ToText("\t", true);
                File.WriteAllText(txtPath, tableToText, Encoding.Default);
                Process.Start(txtPath);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ListBoxLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var index = this.listBoxLayers.SelectedIndex;
                if (index < 0)
                    return;

                var name = this.listBoxLayers.SelectedItem.ToString();
                var featureClass = this.workspaceSelector1.Workspace.GetFeatureClassByName(name);
                var dataTable = featureClass.Fields.ToGFieldItems().ToDataTable();
                this.gridControl1.DataSource = null;
                this.gridControl1.DataSource = dataTable;
                Marshal.ReleaseComObject(featureClass);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}