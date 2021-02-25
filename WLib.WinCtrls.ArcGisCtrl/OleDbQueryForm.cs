/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WLib.ArcGis.GeoDatabase.Fields;
using WLib.ArcGis.GeoDatabase.WorkSpace;
using WLib.Database;
using WLib.WinCtrls.MessageCtrl;

namespace WLib.WinCtrls.ArcGisCtrl
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
            {
                if (cbLayerAsTable.Checked && this.workspaceSelector1.WorkspaceType == EWorkspaceType.ShapeFile)
                    sql = sql.Replace("表名称", $"{this.workspaceSelector1.PathOrConnStr}\\{tableName}.dbf");
                else
                    sql = sql.Replace("表名称", tableName);
            }
            if (listBoxFields.SelectedItems.Count > 0)
            {
                var str = listBoxFields.SelectedItems.Cast<string>().Aggregate((a, b) => a + "," + b);
                sql = sql.Replace("列1, 列2", str).Replace("列1", listBoxFields.SelectedItems[0].ToString());
            }

            var splitC = this.tabControl1.SelectedTab.Controls.OfType<SplitContainer>().First();
            var sqlTextBox = splitC.Panel1.Controls.OfType<TextBox>().First();
            sqlTextBox.Text += Environment.NewLine + sql;
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
            var splitC = this.tabControl1.SelectedTab.Controls.OfType<SplitContainer>().First();
            var sqlTextBox = splitC.Panel1.Controls.OfType<TextBox>().First();
            sqlTextBox.Clear();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            var splitC = this.tabControl1.SelectedTab.Controls.OfType<SplitContainer>().First();
            var dataGridView = splitC.Panel2.Controls.OfType<DataGridView>().First();
            var sqlTextBox = splitC.Panel1.Controls.OfType<TextBox>().First(v => (string)v.Tag == "sql");
            var conTextBox = splitC.Panel1.Controls.OfType<TextBox>().First(v => (string)v.Tag == "con");
            dataGridView.Columns.Clear();
            dataGridView.DataSource = null;
            Application.DoEvents();

            var path = workspaceSelector1.PathOrConnStr;
            DbHelper dbHelper = null;
            try
            {
                dbHelper = DbHelper.GetShpMdbGdbHelper(path);
                conTextBox.Text = dbHelper.ConnectionString;
                var sql = sqlTextBox.Text.Trim();
                var dataTable = dbHelper.GetDataTable(sql);
                if (dataTable.Columns.Contains("SHAPE"))
                    dataTable.Columns.Remove("SHAPE");
                dataGridView.Columns.Clear();
                dataGridView.DataSource = dataTable;
            }
            catch (Exception ex) { MessageBoxEx.ShowError(ex); }
            finally { dbHelper?.Close(); }
        }

        private void 删除查询页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.TabPages.Remove(this.tabControl1.SelectedTab);
        }

        private void 新建查询页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var splitC = new SplitContainer() { Size = new Size(300, 300), Dock = DockStyle.Fill, SplitterDistance = 143, };
            splitC.Orientation = Orientation.Horizontal;
            var sqlBox = new TextBox() { Tag = "sql", Dock = DockStyle.Fill, Font = new Font("Arial Narrow", 12), Multiline = true, ForeColor = SystemColors.HotTrack, ScrollBars = ScrollBars.Both };
            var conBox = new TextBox() { Tag = "con", Dock = DockStyle.Top, Font = new Font("Arial Narrow", 10), ReadOnly = true };
            conBox.SendToBack();//置于底层
            splitC.Panel1.Controls.Add(sqlBox);
            splitC.Panel1.Controls.Add(conBox);
            splitC.Panel2.Controls.Add(new DataGridView() { Dock = DockStyle.Fill });

            var tabPage = new TabPage("新查询页");
            tabPage.Controls.Add(splitC);
            this.tabControl1.TabPages.Add(tabPage);
            this.tabControl1.SelectedTab = tabPage;
        }
    }
}
