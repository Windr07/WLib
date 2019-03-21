/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.GeoDb.Fields;
using WLib.UserCtrls.ArcGisCtrl;

namespace WLib.UserCtrls.Dev.ArcGisCtrl
{
    /// <summary>
    /// 按属性查询的窗体
    /// </summary>
    public partial class QueryForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 查询事件
        /// </summary>
        public event EventHandler Query;
        /// <summary>
        /// 构造的查询语句
        /// </summary>
        public string WhereClause { get => txtWhereClause.Text.Trim(); set => txtWhereClause.Text = value; }

        
        /// <summary>
        /// 按属性查询的窗体
        /// </summary>
        ///  <param name="searchTable">要查询的表格</param>
        /// <param name="isQueryAaliasName">确定查询的是字段别名还是字段名</param>
        /// <param name="queryFieldNames">显示在属性查询窗口的，要查询的字段</param>
        /// <param name="queryHandler">查询事件处理</param>
        public QueryForm(ITable searchTable, bool isQueryAaliasName = false, string[] queryFieldNames = null, EventHandler queryHandler = null)
        {
            InitForm(queryHandler);
            cmbTables.Properties.Items.Add(new TableAttributeQuery(searchTable, isQueryAaliasName, queryFieldNames));
        }
        /// <summary>
        /// 按属性查询的窗体
        /// </summary>
        ///  <param name="searchTables">要查询的表格</param>
        /// <param name="queryHandler">查询事件处理</param>
        public QueryForm(IEnumerable<ITable> searchTables, EventHandler queryHandler = null)
        {
            InitForm(queryHandler);
            cmbTables.Properties.Items.AddRange(searchTables.Select(v => new TableAttributeQuery(v, false, null)).ToArray());
        }
        /// <summary>
        /// 按属性查询的窗体
        /// </summary>
        ///  <param name="tableAttributeQueries">要查询的表格</param>
        /// <param name="queryHandler">查询事件处理</param>
        public QueryForm(IEnumerable<TableAttributeQuery> tableAttributeQueries, EventHandler queryHandler = null)
        {
            InitForm(queryHandler);
            cmbTables.Properties.Items.AddRange(tableAttributeQueries.ToArray());
        }
        /// <summary>
        /// 初始化窗体
        /// </summary>
        public void InitForm(EventHandler queryHandler)
        {
            InitializeComponent();

            Query += queryHandler;
            const string findFieldTips = @"按名称/别名查找字段";
            const string findValueTips = @"查找字段值";
            txtSearchFields.Text = findFieldTips;
            txtSearchValues.Text = findValueTips;
            txtSearchFields.MouseDown += delegate { if (txtSearchFields.Text == findFieldTips) txtSearchFields.Text = string.Empty; };
            txtSearchValues.MouseDown += delegate { if (txtSearchValues.Text == findValueTips) txtSearchValues.Text = string.Empty; };
            清空DToolStripMenuItem.Click += delegate { txtWhereClause.Text = string.Empty; };
            sBtnClear.Click += delegate { txtWhereClause.Text = string.Empty; };
            sBtnClose.Click += delegate { Close(); };
        }


        private void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            var attrQuery = (TableAttributeQuery)cmbTables.SelectedItem;
            listBoxFields.Items.AddRange(attrQuery.FieldItems);
            Text = $"按属性查询 - {attrQuery.TableName}";

            //var source = new AutoCompleteStringCollection();
            //source.AddRange(panel1.Controls.Cast<Control>().Select(v => v.Tag.ToString()).ToArray());
            //source.AddRange(attrQuery.FieldItems.Select(v => v.Name).ToArray());
            //txtWhereClause.AutoCompleteCustomSource = source;
        }

        private void symbolButton_Click(object sender, EventArgs e)//各类符号按钮
        {
            var symbol = $"{(sender as Control)?.Tag} ";
            var startIndex = txtWhereClause.SelectionStart;
            txtWhereClause.Text = txtWhereClause.Text.Insert(startIndex, symbol);
            txtWhereClause.SelectionStart = symbol == "='' " ? startIndex + 2 : startIndex + symbol.Length;
        }

        private void txtSearchFields_EditValueChanged(object sender, EventArgs e)//查找字段
        {
            listBoxFields.SelectedItem = ((TableAttributeQuery)cmbTables.SelectedItem).QueryField(txtSearchFields.Text);
        }

        private void txtSearchValues_EditValueChanged(object sender, EventArgs e)//查找唯一值
        {
            listBoxValues.SelectedItem = listBoxValues.Items.Cast<string>().FirstOrDefault(v => v.StartsWith(txtSearchValues.Text));
        }

        private void sBtnGetUniqueValue_Click(object sender, EventArgs e)//获取唯一值
        {
            if (listBoxFields.SelectedIndex < 0) return;
            listBoxValues.Items.Clear();
            listBoxValues.Items.AddRange(((TableAttributeQuery)cmbTables.SelectedItem).GetUnqiueValues((FieldItem)listBoxFields.SelectedItem));
        }

        private void listBoxFields_MouseDoubleClick(object sender, MouseEventArgs e)//双击字段，将字段名加入到查询语句中
        {
            if (listBoxFields.SelectedIndex < 0) return;

            var fieldItem = (FieldItem)listBoxFields.SelectedItem;
            var fieldName = ((TableAttributeQuery)cmbTables.SelectedItem).IsQueryAaliasName ? fieldItem.AliasName : fieldItem.Name;
            var value = $"{fieldName} ";
            var index = txtWhereClause.SelectionStart;
            txtWhereClause.Text = txtWhereClause.Text.Insert(index, value);
            txtWhereClause.SelectionStart = index + value.Length;
        }

        private void listBoxValues_MouseDoubleClick(object sender, MouseEventArgs e)//双击字段值，将字段值加入到查询语句中
        {
            if (listBoxValues.SelectedIndex < 0 && listBoxFields.SelectedIndex < 0)
                return;
            var fieldType = ((FieldItem)listBoxFields.SelectedItem).FieldType;
            var format = fieldType == esriFieldType.esriFieldTypeString ? "'{0}' " : "{0} ";
            var value = string.Format(format, listBoxValues.SelectedItem);
            var index = txtWhereClause.SelectionStart;
            txtWhereClause.Text = txtWhereClause.Text.Insert(index, value);
            txtWhereClause.SelectionStart = index + value.Length;
        }

        private void listBoxFields_MouseUp(object sender, MouseEventArgs e)//提起鼠标时，在指针位置弹出右键菜单
        {
            listBoxFields.SelectedIndex = listBoxFields.IndexFromPoint(new Point(e.X, e.Y));
            if (listBoxFields.SelectedIndex > -1 && e.Button == MouseButtons.Right)
            {
                等于EToolStripMenuItem.Tag = ((FieldItem)listBoxFields.SelectedItem).FieldType == esriFieldType.esriFieldTypeString ? "=''" : "=";
                cMenuStripFields.Show(listBoxFields, e.X, e.Y);
            }
        }

        private void sBtnApply_Click(object sender, EventArgs e)//应用
        {
            try
            {
                Query?.Invoke(this, new EventArgs());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
