/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace WLib.UserCtrls.Dev.ArcGisCtrl
{
    /// <summary>
    /// 表格(ArcGIS ITable)列表控件
    /// </summary>
    public partial class TableListBox : UserControl
    {
        /// <summary>
        /// 加载在表格列表中的表格
        /// </summary>
        public List<ITable> Tables { get; }
        /// <summary>
        /// 属性表窗口
        /// </summary>
        public AttributeForm AttributeForm { get; set; }

        
        /// <summary>
        /// 表格(ArcGIS ITable)列表控件
        /// </summary>
        public TableListBox()
        {
            InitializeComponent();
            Tables = new List<ITable>();
        }
        /// <summary>
        /// 表格(ArcGIS ITable)列表控件
        /// </summary>
        /// <param name="attributeForm">属性表窗口</param>
        public TableListBox(AttributeForm attributeForm)
        {
            InitializeComponent();
            AttributeForm = attributeForm;
            Tables = new List<ITable>();
        }


        /// <summary>
        /// 添加表格
        /// </summary>
        /// <param name="table"></param>
        public void AddTable(ITable table)
        {
            Tables.Add(table);
            string tableName;
            if (table is IObjectClass objectClass)
                tableName = objectClass.AliasName;
            else
                tableName = ((IDataset)table).Name;
            tableBox.Items.Add(new ImageListBoxItem(tableName, 0));
        }
        /// <summary>
        /// 添加多个表格
        /// </summary>
        /// <param name="tables"></param>
        public void AddTables(IEnumerable<ITable> tables)
        {
            Tables.AddRange(tables);
            var tableNamesItems = tables.Select(v => ((IObjectClass)v).AliasName).Select(v => new ImageListBoxItem(v, 0)).ToArray();
            tableBox.Items.AddRange(tableNamesItems);
        }
        /// <summary>
        /// 移除指定表格
        /// </summary>
        /// <param name="table"></param>
        public void RemoveTable(ITable table)
        {
            int index = Tables.IndexOf(table);
            Tables.Remove(table);
            tableBox.Items.RemoveAt(index);
        }
        /// <summary>
        /// 移除指定表格
        /// </summary>
        /// <param name="tableName"></param>
        public void RemoveTable(string tableName)
        {
            var table = Tables.FirstOrDefault(v => ((IObjectClass)v).AliasName == tableName);
            if (table != null)
                RemoveTable(table);
        }
        /// <summary>
        /// 清空表格列表
        /// </summary>
        public void ClearTable()
        {
            Tables.Clear();
            tableBox.Items.Clear();
        }


        private void 打开表格属性表TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tableBox.SelectedIndex < 0)
                return;
            string tableName = tableBox.SelectedItem.ToString();
            var table = Tables.FirstOrDefault(v => ((IObjectClass)v).AliasName == tableName);
            if (table != null)
            {
                if (AttributeForm == null || AttributeForm.IsDisposed)
                    AttributeForm = new AttributeForm();
                else
                    AttributeForm.Activate();//之前已打开，则给予焦点，置顶。
                if (!AttributeForm.Visible)
                    AttributeForm.Show(this);

                AttributeForm.LoadAttribute(table);
            }
        }
        private void 移除表格ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveTable(tableBox.SelectedItem.ToString());
        }
        private void imagelistBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enable = tableBox.SelectedIndex > -1;
            打开表格属性表TToolStripMenuItem.Enabled = enable;
            移除表格ToolStripMenuItem.Enabled = enable;
        }
        private void imagelistBoxTables_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                tableBox.SelectedIndex = tableBox.IndexFromPoint(e.Location);
        }
        private void imagelistBoxTables_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && tableBox.SelectedIndex > -1)
                cMenuStripTableList.Show(tableBox, e.Location);
        }
    }
}
