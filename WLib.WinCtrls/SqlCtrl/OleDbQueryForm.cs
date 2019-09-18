/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Linq;
using System.Windows.Forms;
using WLib.ArcGis.GeoDatabase.WorkSpace;
using WLib.ArcGis.GeoDatabase.Fields;
using WLib.Database;
using WLib.Database.DbBase;

namespace WLib.WinCtrls.SqlCtrl
{
    /// <summary>
    /// 通过OleDb连接和使用sql查询mdb/shp/gdb等数据的窗体
    /// </summary>
    public partial class OleDbQueryForm : Form
    {
        /// <summary>
        /// 通过OleDb连接和使用sql查询mdb/shp/gdb等数据的窗体
        /// </summary>
        public OleDbQueryForm()
        {
            InitializeComponent();
        }


        private void workspaceSelector1_AfterSelectPath(object sender, EventArgs e)
        {
            listBoxTables.Items.Clear();
            if (workspaceSelector1.FeatureClassNames != null)
                listBoxTables.Items.AddRange(workspaceSelector1.FeatureClassNames);
            if (workspaceSelector1.TableNames != null)
                listBoxTables.Items.AddRange(workspaceSelector1.TableNames);
            if (listBoxTables.Items.Count > 0)
                listBoxTables.SelectedIndex = 0;
        }

        private void cmbSql_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSql.SelectedIndex < 0) return;

            var sql = cmbSql.SelectedItem.ToString();
            var tableName = listBoxTables.SelectedItem?.ToString();
            if (tableName != null)
                sql = sql.Replace("表名称", tableName);

            if (listBoxFields.SelectedItems.Count > 0)
            {
                var str = listBoxFields.SelectedItems.Cast<string>().Aggregate((a, b) => a + "," + b);
                sql = sql.Replace("列1, 列2", str).Replace("列1", listBoxFields.SelectedItems[0].ToString());
            }

            richTextBox1.Text += Environment.NewLine + sql;
        }

        private void listBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxFields.Items.Clear();
            var name = listBoxTables.SelectedItem?.ToString();
            if (name == null) return;

            var featureClass = workspaceSelector1.GetFeatureClassByName(name);
            if (featureClass != null)
                listBoxFields.Items.AddRange(featureClass.GetFieldsNames().ToArray());
            else
            {
                var table = workspaceSelector1.GetTableByName(name);
                if (table != null)
                    listBoxFields.Items.AddRange(table.GetFieldsNames().ToArray());
            }

            if (cmbSql.SelectedIndex == -1)
                cmbSql.SelectedIndex = 0;
            else
                cmbSql_SelectedIndexChanged(null, null);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            var path = workspaceSelector1.PathOrConnStr;
            string connString = null;
            DbHelper dbHelper = null;
            try
            {
                switch (workspaceSelector1.WorkspaceType)
                {
                    case EWorkspaceType.ShapeFile: connString = DbHelper.ShpDir(path); break;
                    case EWorkspaceType.FileGDB: connString = DbHelper.Gdb(path); break;
                    case EWorkspaceType.Access: connString = DbHelper.Mdb(path); break;
                }
                dbHelper = DbHelper.GetDbHelper(connString, EDbProviderType.OleDb);
                var sql = richTextBox1.Text.Trim();
                var dataTable = dbHelper.GetDataTable(sql);
                if (dataTable.Columns.Contains("SHAPE"))
                    dataTable.Columns.Remove("SHAPE");
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { dbHelper?.Close(); }
        }
    }
}
